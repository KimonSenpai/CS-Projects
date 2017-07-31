using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using Timer = System.Timers.Timer;
namespace TZK.Bullet {
    public partial class Bullet /*: UserControl */{
        #region Fields

        private const double Interval = 30;
        protected int _velocity;
        protected Size _imageSize;
        private Image _model;
        private Size _bulletSize;
        private Point _bulletCenter;
        private float _koef;
        private Bitmap Img;
        private PointF _center;
        private Timer _timer;

        private readonly int _width, _height;

        private event Action _stop;

        #endregion
        public Bullet(float koef, PointF center, float angle, Action start, Action stop) {
            _koef = koef;
            Center = center;
            Angle = angle;
            ReadSettings();
            _timer = new Timer(Interval);
            _timer.Elapsed += Elapsed;
            
            Img = new Bitmap(
                _width = Convert.ToInt32((_imageSize.Width*Math.Abs(Math.Cos((double)angle)) + _imageSize.Height* Math.Abs( Math.Sin((double) angle ) ))*_koef), 
                _height = Convert.ToInt32( (_imageSize.Width * Math.Abs( Math.Sin( (double)angle ) ) + _imageSize.Height * Math.Abs( Math.Cos( (double)angle ) )) * _koef )
            );

            start();
            _stop += stop;
            _timer.Start();

        }
        private void ReadSettings() {
            XDocument xml = XDocument.Load( "MODELS\\Settings.xml" );

            var bullet = xml.Root.Elements( "Bullet" ).First();
                
            _model = Image.FromFile( bullet.Element( "Path" ).Value );

            _velocity = Convert.ToInt32( bullet.Element( "Velocity" ).Value );
            

            _bulletSize = new Size(
                Convert.ToInt32( bullet.Element( "Size" ).Element( "Width" ).Value ),
                Convert.ToInt32( bullet.Element( "Size" ).Element( "Height" ).Value ) );

            _bulletCenter = new Point(
                Convert.ToInt32( bullet.Element( "Center" ).Element( "X" ).Value ),
                Convert.ToInt32( bullet.Element( "Center" ).Element( "Y" ).Value ) );

        }
        public PointF Center {
            set {
                //if (InvokeRequired) {
                //    //AsyncCallback callback = new AsyncCallback(ar => );
                //    Invoke( new Action<PointF>( loc => Center = loc ), value );
                //}
                //else {
                    Point location = new Point();
                    _center = value;
                    location.X = Convert.ToInt32( _center.X ) - _width / 2;
                    location.Y = Convert.ToInt32( _center.Y ) - _height / 2;
                    
                    Location = location;
                //}
            }
            get { return _center; }
        }
        public float Angle { get; }
        public Point Location { get; private set; }

        private void Elapsed(object sender, EventArgs e) {
            Center = new PointF(
                Center.X + _velocity * (float) Math.Cos(-Angle * Math.PI / 180) * (float) Interval / 1000,
                Center.Y + _velocity * (float) Math.Sin(-Angle * Math.PI / 180) * (float) Interval / 1000);

        }

        public void Destroy() {
            _timer.Stop();
            _stop();
        }
    }
}
