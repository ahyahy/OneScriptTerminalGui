using ScriptEngine.Machine.Contexts;

namespace ostgui
{
    public class Size
    {
        public TfSize dll_obj;
        public Terminal.Gui.Size M_Size;

        public Size()
        {
            M_Size = new Terminal.Gui.Size();
        }

        public Size(Terminal.Gui.Point p1)
        {
            M_Size = new Terminal.Gui.Size(p1);
        }

        public Size(Terminal.Gui.Size p1)
        {
            M_Size = p1;
        }

        public Size(int width, int height)
        {
            M_Size = new Terminal.Gui.Size(width, height);
        }

        public int Width
        {
            get { return M_Size.Width; }
            set { M_Size.Width = value; }
        }

        public int Height
        {
            get { return M_Size.Height; }
            set { M_Size.Height = value; }
        }

        public Size Subtract(Size p1, Size p2)
        {
            return new Size(Terminal.Gui.Size.Subtract(p1.M_Size, p2.M_Size));
        }

        public new string ToString()
        {
            return M_Size.ToString();
        }
    }

    [ContextClass("ТфРазмер", "TfSize")]
    public class TfSize : AutoContext<TfSize>
    {

        public TfSize()
        {
            Size Size1 = new Size();
            Size1.dll_obj = this;
            Base_obj = Size1;
        }

        public TfSize(TfPoint p1)
        {
            Size Size1 = new Size(p1.Base_obj.M_Point);
            Size1.dll_obj = this;
            Base_obj = Size1;
        }

        public TfSize(Size p1)
        {
            Size Size1 = p1;
            Size1.dll_obj = this;
            Base_obj = Size1;
        }

        public TfSize(int p1, int p2)
        {
            Size Size1 = new Size(p1, p2);
            Size1.dll_obj = this;
            Base_obj = Size1;
        }

        public Size Base_obj;

        [ContextProperty("Высота", "Height")]
        public int Height
        {
            get { return Base_obj.Height; }
            set { Base_obj.Height = value; }
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

        [ContextMethod("Вычесть", "Subtract")]
        public TfSize Subtract(TfSize p1, TfSize p2)
        {
            return new TfSize(Base_obj.Subtract(p1.Base_obj, p2.Base_obj));
        }

        [ContextMethod("Добавить", "Add")]
        public TfSize Add(TfSize p1, TfSize p2)
        {
            Terminal.Gui.Size size1 = Terminal.Gui.Size.Add(p1.Base_obj.M_Size, p2.Base_obj.M_Size);
            return new TfSize(size1.Width, size1.Height);
        }

    }
}
