using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZK {
    public static class Extentions {
        [System.Diagnostics.Conditional("DEBUG")]
        public static void DrawTest(this Graphics obj,Pen pen, int x, int y, int width, int height) {
            obj.DrawRectangle(pen, x, y, width, height);
            obj.DrawLine(pen,new Point(x,y),new Point(x+width,y+height));
            obj.DrawLine( pen, new Point( x+ width, y ), new Point( x , y + height ) );
        }
    }
}
