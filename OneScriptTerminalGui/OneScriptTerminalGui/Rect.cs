using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace ostgui
{
    public class Rect
    {
        public TfRect dll_obj;
        public Terminal.Gui.Rect M_Rect;

        public Rect()
        {
            M_Rect = new Terminal.Gui.Rect();
        }

        public Rect(Terminal.Gui.Point p1, Terminal.Gui.Size p2)
        {
            M_Rect = new Terminal.Gui.Rect(p1, p2);
        }

        public Rect(Terminal.Gui.Rect p1)
        {
            M_Rect = p1;
        }

        public Rect(int x, int y, int width, int height)
        {
            M_Rect = new Terminal.Gui.Rect(x, y, width, height);
        }

        public int X
        {
            get { return M_Rect.X; }
            set { M_Rect.X = value; }
        }

        public int Y
        {
            get { return M_Rect.Y; }
            set { M_Rect.Y = value; }
        }

        public int Width
        {
            get { return M_Rect.Width; }
            set { M_Rect.Width = value; }
        }

        public int Height
        {
            get { return M_Rect.Height; }
            set { M_Rect.Height = value; }
        }

        public int Left
        {
            get { return M_Rect.Left; }
        }

        public int Right
        {
            get { return M_Rect.Right; }
        }

        public int Top
        {
            get { return M_Rect.Top; }
        }

        public int Bottom
        {
            get { return M_Rect.Bottom; }
        }

        public bool Contains(Terminal.Gui.Rect p1)
        {
            return M_Rect.Contains(p1);
        }

        public bool Contains(Terminal.Gui.Point p1)
        {
            return M_Rect.Contains(p1);
        }

        public bool Contains(int p1, int p2)
        {
            return M_Rect.Contains(p1, p2);
        }

        public void Offset(int p1, int p2)
        {
            M_Rect.Offset(p1, p2);
        }

        public void Inflate(Terminal.Gui.Size p1)
        {
            M_Rect.Inflate(p1);
        }

        public ostgui.Rect Inflate(Rect p1, int p2, int p3)
        {
            return new Rect(Terminal.Gui.Rect.Inflate(p1.M_Rect, p2, p3));
        }

        public void Inflate(int p1, int p2)
        {
            M_Rect.Inflate(p1, p2);
        }

        public Rect FromLTRB(int p1, int p2, int p3, int p4)
        {
            return new Rect(Terminal.Gui.Rect.FromLTRB(p1, p2, p3, p4));
        }

        public new string ToString()
        {
            return M_Rect.ToString();
        }
    }

    [ContextClass("ТфПрямоугольник", "TfRect")]
    public class TfRect : AutoContext<TfRect>
    {

        public TfRect()
        {
            Rect Rect1 = new Rect();
            Rect1.dll_obj = this;
            Base_obj = Rect1;
        }

        public TfRect(TfPoint p1, TfSize p2)
        {
            Rect Rect1 = new Rect(p1.Base_obj.M_Point, p2.Base_obj.M_Size);
            Rect1.dll_obj = this;
            Base_obj = Rect1;
        }

        public TfRect(Rect p1)
        {
            Rect Rect1 = p1;
            Rect1.dll_obj = this;
            Base_obj = Rect1;
        }

        public TfRect(int x, int y, int width, int height)
        {
            Rect Rect1 = new Rect(x, y, width, height);
            Rect1.dll_obj = this;
            Base_obj = Rect1;
        }

        public Rect Base_obj;

        [ContextProperty("Верх", "Top")]
        public int Top
        {
            get { return Base_obj.Top; }
        }

        [ContextProperty("Высота", "Height")]
        public int Height
        {
            get { return Base_obj.Height; }
            set { Base_obj.Height = value; }
        }

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

        [ContextProperty("Лево", "Left")]
        public int Left
        {
            get { return Base_obj.Left; }
        }

        [ContextProperty("Низ", "Bottom")]
        public int Bottom
        {
            get { return Base_obj.Bottom; }
        }

        [ContextProperty("Право", "Right")]
        public int Right
        {
            get { return Base_obj.Right; }
        }

        [ContextProperty("Ширина", "Width")]
        public int Width
        {
            get { return Base_obj.Width; }
            set { Base_obj.Width = value; }
        }

        [ContextMethod("ВСтроку", "ToString")]
        public new string ToString()
        {
            return Base_obj.ToString();
        }

        [ContextMethod("Содержит", "Contains")]
        public IValue Contains(IValue p1, IValue p2 = null)
        {
            if (p1.GetType() == typeof(TfRect))
            {
                return ValueFactory.Create(Base_obj.Contains(((TfRect)p1).Base_obj.M_Rect));
            }
            else if (p1.GetType() == typeof(TfPoint))
            {
                return ValueFactory.Create(Base_obj.Contains(((TfPoint)p1).Base_obj.M_Point));
            }
            else if (p1.SystemType.Name == "Число" && p2 != null)
            {
                return ValueFactory.Create(Base_obj.Contains(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber())));
            }
            else
            {
                return ValueFactory.CreateNullValue();
            }
        }

    }
}
