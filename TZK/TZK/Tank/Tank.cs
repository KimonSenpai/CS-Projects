using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
//////////////////////////
//using System.Text.RegularExpressions;

namespace TZK.Tank {
    public sealed partial class Tank : UserControl {

        #region Fields

        private int _velocity;
        private float _tankRotSp;
        private float _barrelRotSp;
        private Size _tankSize, _barrelSize;

        public Size Size {
            get { return _tankSize; }
        }
        public Size _BarrelSize {
            get { return _barrelSize; }
        }
        private Point _tankCenter, _barrelCenter, _tankAxis;

        public PointF TankCenter { get { return _tankCenter; } }
        private Image _model;
       private Image _barrel;

        private readonly int _diagonal;
        private readonly float _koef;

        public float _Koef {
            get { return _koef; }
        }

        #endregion
        private readonly Bitmap Img;

        /// <param name="name">Name of model</param>
        /// <param name="diagonal">Diagonal of the object</param>
        public Tank(string name, int diagonal) {

            ReadSettings(name);
            
            _diagonal = diagonal;
            _koef =	Convert.ToSingle(
					_diagonal /
                    Math.Max(Math.Sqrt(_tankSize.Width * _tankSize.Width + _tankSize.Height * _tankSize.Height), 2*Convert.ToDouble(_barrelSize.Width - _barrelCenter.X + Math.Abs(_tankCenter.X - _tankAxis.X))));
            Height = Width = _diagonal;
            _center = new PointF(Width/2F, Height/2F);
            Img = new Bitmap(Width, Height);
            BackgroundImage = Img;

            InitializeComponent();
            UpdateModel();
        }


        /// <summary>
        /// Method, that gets data about tank from file Settings.xml
        /// </summary>
        /// <param name="name">Name of model</param>
        private void ReadSettings(string name) {
            XDocument xml = XDocument.Load("MODELS\\Settings.xml");

            var data = (from XElement n in xml.Root.Elements("TankModel")
                    where n.Attribute("name").Value == name
                    select n).First();
            

            #region TankSettings
            var tank = data.Element("Tank");

            _model = Image.FromFile(tank.Element("Path").Value);

            _velocity = Convert.ToInt32(tank.Element("Velocity").Value);
            _tankRotSp = Convert.ToSingle(tank.Element("RotatingSpeed").Value);

            _tankSize = new Size(
                Convert.ToInt32(tank.Element("Size").Element("Width").Value),
                Convert.ToInt32(tank.Element("Size").Element("Height").Value));

            _tankCenter = new Point(
                Convert.ToInt32(tank.Element("Center").Element("X").Value),
                Convert.ToInt32(tank.Element("Center").Element("Y").Value));

            _tankAxis = new Point(
                Convert.ToInt32(tank.Element("BarrelAxis").Element("X").Value),
                Convert.ToInt32(tank.Element("BarrelAxis").Element("Y").Value));
            #endregion

            #region BarrelSettings
            var barrel = data.Element("Barrel");

            _barrel = Image.FromFile(barrel.Element("Path").Value);

            _barrelRotSp = Convert.ToSingle(barrel.Element("RotatingSpeed").Value);

            _barrelSize = new Size(
                Convert.ToInt32(barrel.Element("Size").Element("Width").Value),
                Convert.ToInt32(barrel.Element("Size").Element("Height").Value));

            _barrelCenter = new Point(
                Convert.ToInt32(barrel.Element("Center").Element("X").Value),
                Convert.ToInt32(barrel.Element("Center").Element("Y").Value));
            #endregion
        }

        private PointF _center;
        public PointF Center {
            set {
                if (InvokeRequired) {
                    //AsyncCallback callback = new AsyncCallback(ar => );
                    Invoke(new Action<PointF>(loc => Center = loc), value);
                }
                else {
                    Point location = new Point();
                    _center = value;
                    location.X = Convert.ToInt32(_center.X) - Width / 2;
                    location.Y = Convert.ToInt32(_center.Y) - Height / 2;


                    Location = location;
                }
            }
            get { return _center; }
        }

        

        private float _angle;
        public float Angle {
            get {
                return _angle;
            }
            set {
                
                    _angle = value % 360f;
                
            }
        }

        private float _barrelAngle;
        public float BarrelAngle {
            get {
                return _barrelAngle;
            }
            set {
                if (Math.Abs(value - _barrelAngle) > 0) {
                    _barrelAngle = value % 360f;
                }
            }
        }

        public int Velocity {
            get { return _velocity; }
        }

        public float TankRotSp { get {return _tankRotSp;} }

        public float BarrelRotSp {
            get { return _barrelRotSp; }
        }


    }
}
