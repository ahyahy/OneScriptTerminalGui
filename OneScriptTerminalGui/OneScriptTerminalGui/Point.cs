using ScriptEngine.Machine.Contexts;

namespace ostgui
{
    public class Point
    {
        public TfPoint dll_obj;
        public Terminal.Gui.Point M_Point;

        public Point()
        {
            M_Point = new Terminal.Gui.Point();
        }

        public Point(Terminal.Gui.Size p1)
        {
            M_Point = new Terminal.Gui.Point(p1);
        }

        public Point(int x, int y)
        {
            M_Point = new Terminal.Gui.Point(x, y);
        }

        public Point(Terminal.Gui.Point p1)
        {
            M_Point = p1;
        }

        public int X
        {
            get { return M_Point.X; }
            set { M_Point.X = value; }
        }

        public int Y
        {
            get { return M_Point.Y; }
            set { M_Point.Y = value; }
        }

        public void Offset(int p1, int p2)
        {
            M_Point.Offset(p1, p2);
        }

        public ostgui.Point Subtract(Point p1, Size p2)
        {
            return new Point(Terminal.Gui.Point.Subtract(p1.M_Point, p2.M_Size));
        }

        public new string ToString()
        {
            return M_Point.ToString();
        }
    }

    [ContextClass("ТфТочка", "TfPoint")]
    public class TfPoint : AutoContext<TfPoint>
    {

        public TfPoint()
        {
            Point Point1 = new Point();
            Point1.dll_obj = this;
            Base_obj = Point1;
        }

        public TfPoint(TfSize p1)
        {
            Point Point1 = new Point(p1.Base_obj.M_Size);
            Point1.dll_obj = this;
            Base_obj = Point1;
        }

        public TfPoint(Point p1)
        {
            Point Point1 = p1;
            Point1.dll_obj = this;
            Base_obj = Point1;
        }

        public TfPoint(int p1, int p2)
        {
            Point Point1 = new Point(p1, p2);
            Point1.dll_obj = this;
            Base_obj = Point1;
        }

        public Point Base_obj;

        [ContextProperty("Игрек", "Y")]
        public int Y
        {
            get { return Base_obj.Y; }
            set { Base_obj.Y = value; }
        }

        [ContextProperty("Икс", "X")]
        public int X
        {
            get { return Base_obj.X; }
            set { Base_obj.X = value; }
        }

        [ContextMethod("ВСтроку", "ToString")]
        public new string ToString()
        {
            return Base_obj.ToString();
        }

    }
}
