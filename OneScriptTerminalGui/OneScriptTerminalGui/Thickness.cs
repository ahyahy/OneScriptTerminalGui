using ScriptEngine.Machine.Contexts;

namespace ostgui
{
    public class Thickness
    {
        public TfThickness dll_obj;
        public Terminal.Gui.Thickness M_Thickness;

        public Thickness(int p1)
        {
            M_Thickness = new Terminal.Gui.Thickness(p1);
        }

        public Thickness(int left, int top, int right, int bottom)
        {
            M_Thickness = new Terminal.Gui.Thickness(left, top, right, bottom);
        }

        public int Top
        {
            get { return M_Thickness.Top; }
            set { M_Thickness.Top = value; }
        }

        public int Left
        {
            get { return M_Thickness.Left; }
            set { M_Thickness.Left = value; }
        }

        public int Bottom
        {
            get { return M_Thickness.Bottom; }
            set { M_Thickness.Bottom = value; }
        }

        public int Right
        {
            get { return M_Thickness.Right; }
            set { M_Thickness.Right = value; }
        }

        public new string ToString()
        {
            return M_Thickness.ToString();
        }
    }

    [ContextClass("ТфТолщина", "TfThickness")]
    public class TfThickness : AutoContext<TfThickness>
    {

        public TfThickness(int p1)
        {
            Thickness Thickness1 = new Thickness(p1);
            Thickness1.dll_obj = this;
            Base_obj = Thickness1;
        }

        public TfThickness(int left, int top, int right, int bottom)
        {
            Thickness Thickness1 = new Thickness(left, top, right, bottom);
            Thickness1.dll_obj = this;
            Base_obj = Thickness1;
        }

        public TfThickness(ostgui.Thickness p1)
        {
            Thickness Thickness1 = p1;
            Thickness1.dll_obj = this;
            Base_obj = Thickness1;
        }

        public Thickness Base_obj;

        [ContextProperty("Верх", "Top")]
        public int Top
        {
            get { return Base_obj.Top; }
            set { Base_obj.Top = value; }
        }

        [ContextProperty("Лево", "Left")]
        public int Left
        {
            get { return Base_obj.Left; }
            set { Base_obj.Left = value; }
        }

        [ContextProperty("Низ", "Bottom")]
        public int Bottom
        {
            get { return Base_obj.Bottom; }
            set { Base_obj.Bottom = value; }
        }

        [ContextProperty("Право", "Right")]
        public int Right
        {
            get { return Base_obj.Right; }
            set { Base_obj.Right = value; }
        }

        [ContextMethod("ВСтроку", "ToString")]
        public new string ToString()
        {
            return Base_obj.ToString();
        }

    }
}
