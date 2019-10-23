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
        public static CDrawer Drawer { get; } = new CDrawer(1024, 768, true);
        public static Color DefaultColor { get; set; } = Color.White;
        public Color _Color { get; private set; }
        public int Width { get; set; } = 5;
        private (ushort x, ushort y) _Point1;
        private (ushort x, ushort y) _Point2;

        private Line() => _Color = DefaultColor;
        public Line(Point p) : this() => _Point2 = _Point1 = ((ushort)p.X, (ushort)p.Y);
        public Line(Point p, Color color) : this(p) => _Color = color;
        public Line(Point p, int width) : this(p) => Width = width;
        public Line(Point p, int width, Color color) : this(p, width) => _Color = color;

        public Line(Point start, Point End) : this()
        {
            _Point1 = ((ushort)start.X, (ushort)start.Y);
            _Point2 = ((ushort)End.X, (ushort)End.Y);
        }
        public Line(Point start, Point End, Color color) : this(start, End) => _Color = color;
        public Line(Point start, Point End, int width) : this(start, End) => Width = width;
        public Line(Point start, Point End, int width, Color color) : this(start, End, width) => _Color = color;

        public Line((ushort x, ushort y) point) : this() => _Point1 = _Point2 = point;
        public Line(int x, int y) : this(((ushort)x, (ushort)y)) { }
        public Line(int x, int y, int width) : this(((ushort)x, (ushort)y)) => Width = width;

        public void Render()
        {
            Drawer.AddLine(_Point1.x, _Point1.y, _Point2.x, _Point2.y, _Color, (int)Width);
        }

    }
    public class LineGenerater
    {
        private static CDrawer _Drawer { get => Line.Drawer; }

        public static Queue<Point> _points { get; private set; } = new Queue<Point>();
        private static List<Line> Lines = new List<Line>();
        private static System.Timers.Timer Tim = new System.Timers.Timer() { AutoReset = true, Interval = 1 };
        private static Color _color = Color.Empty;
        private static byte _width = 5;
        static LineGenerater()
        {
            _Drawer.MouseMoveScaled += _Drawer_MouseMoveScaled;
            Tim.Elapsed += Tim_Elapsed;
            Tim.Start();
        }

        private static void Tim_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Generate();
        }

        private static void _Drawer_MouseMoveScaled(Point pos, CDrawer dr)
        {
            lock (_points)
            {
                _points.Enqueue(pos);
            }
        }

        static void Generate()
        {
            lock (_points)
            {
                while (_points.Count > 0)
                {

                    if (_points.Count % 2 == 1)
                    {
                        var t = new Line(_points.Dequeue(), _width, _color);
                        Lines.Add(t);
                        t.Render();
                    }
                    else
                    {
                        var t = new Line(_points.Dequeue(), _points.Dequeue(), _width, _color);
                        Lines.Add(t);
                        t.Render();
                    }
                }
            }
        }
    }
}
