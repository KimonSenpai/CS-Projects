

using System.Drawing;

namespace TZK.Bullet {
    partial class Bullet {
        public void UpdateBullet(Image image) {
            var graph = Graphics.FromImage(Img);

            graph.Clear(Color.Transparent);

            graph.DrawTest( Pens.Black, 0, 0, _width - 1, _height - 1 );
            //graph.ScaleTransform(_koef, _koef);

            graph.TranslateTransform( (float)_width / 2, (float)_height / 2 );
            graph.ScaleTransform( _koef, _koef );
            graph.RotateTransform( -Angle );
            graph.TranslateTransform( -_bulletCenter.X, -_bulletCenter.Y );
            graph.DrawImage( _model, new Rectangle( 0, 0, _bulletSize.Width, _bulletSize.Height ) );

            graph.DrawTest( Pens.BlueViolet, 0, 0, _bulletSize.Width, _bulletSize.Height );

            var gr = Graphics.FromImage(image);
            gr.DrawImage(Img, Location);
        }
    }
}