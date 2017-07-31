using System;
using System.Drawing;
namespace TZK.Tank {
    sealed partial class Tank {

        /// <summary>
        /// Repaint current tank
        /// </summary>
        public void UpdateModel() {
            if (InvokeRequired) {
                Invoke(new Action(UpdateModel));
                return;
            }
            var graph = Graphics.FromImage(Img);
            graph.Clear(Color.Transparent);
            graph.DrawTest(Pens.Black, 0, 0, Width-1, Height-1);
            //graph.ScaleTransform(_koef, _koef);
            
            graph.TranslateTransform((float)Width/2, (float)Height/2);
            graph.ScaleTransform( _koef, _koef );
            graph.RotateTransform(-Angle);
            graph.TranslateTransform(-_tankCenter.X, -_tankCenter.Y);
            graph.DrawImage(_model, new Rectangle(0,0,_model.Width,_model.Height) );
            
            graph.DrawTest(Pens.BlueViolet,0, 0,_model.Width,_model.Height);

            graph.TranslateTransform( _tankAxis.X, _tankAxis.Y );
            graph.RotateTransform(-BarrelAngle);
            graph.TranslateTransform( -_barrelCenter.X, -_barrelCenter.Y );
            graph.DrawImage( _barrel, new Rectangle( 0, 0, _barrelSize.Width, _barrelSize.Height ) );
            Invalidate();

        }

        
    }
}