using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GDIDrawer;
namespace ICA_9_10
{
    class Line
    {
        private static CDrawer _drawer = new CDrawer(1024, 768, true);
        private static Queue<Point> _points = new Queue<Point>();
        private static Color _color = Color.Empty;
        private static byte _width = 5;
        public Line()
        {

        }
        static void Render()
        {
            
            if (_points.Count % 2 == 1)
            {
                var t = _points.Dequeue();
                _drawer.AddLine(t.X, t.Y, t.X, t.Y, _color);
            }
            else
            {
                var t = _points.Dequeue();
                var t1 = _points.Dequeue();
                _drawer.AddLine(t.X, t.Y, t1.X, t1.Y, _color);
            }
        }
    }
}
