using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace ostgui
{
    public class Border
    {
        public TfBorder dll_obj;
        public Terminal.Gui.Border M_Border;

        public Border()
        {
            M_Border = new Terminal.Gui.Border();
            Utils.AddToHashtable(M_Border, this);
        }

        public Border(Terminal.Gui.Border p1)
        {
            M_Border = p1;
            Utils.AddToHashtable(M_Border, this);
        }

        public int ActualHeight
        {
            get { return M_Border.ActualHeight; }
        }

        public int ActualWidth
        {
            get { return M_Border.ActualWidth; }
        }

        public string Title
        {
            get { return M_Border.Title.ToString(); }
            set { M_Border.Title = value; }
        }

        public bool DrawMarginFrame
        {
            get { return M_Border.DrawMarginFrame; }
            set { M_Border.DrawMarginFrame = value; }
        }

        public IValue Parent
        {
            get { return Utils.RevertEqualsObj(M_Border.Parent).dll_obj; }
        }

        public Point Effect3DOffset
        {
            get { return new Point(M_Border.Effect3DOffset); }
            set { M_Border.Effect3DOffset = value.M_Point; }
        }

        public int BorderStyle
        {
            get { return (int)M_Border.BorderStyle; }
            set { M_Border.BorderStyle = (Terminal.Gui.BorderStyle)value; }
        }

        public Thickness BorderThickness
        {
            get { return new Thickness(M_Border.BorderThickness.Left, M_Border.BorderThickness.Top, M_Border.BorderThickness.Right, M_Border.BorderThickness.Bottom); }
            set { M_Border.BorderThickness = value.M_Thickness; }
        }

        public Attribute Effect3DBrush
        {
            get { return new Attribute(M_Border.Effect3DBrush.Value); }
            set { M_Border.Effect3DBrush = value.M_Attribute; }
        }

        public int BorderBrush
        {
            get { return (int)M_Border.BorderBrush; }
            set { M_Border.BorderBrush = (Terminal.Gui.Color)value; }
        }

        public int Background
        {
            get { return (int)M_Border.Background; }
            set { M_Border.Background = (Terminal.Gui.Color)value; }
        }

        public bool Effect3D
        {
            get { return M_Border.Effect3D; }
            set { M_Border.Effect3D = value; }
        }

        public Thickness GetSumThickness()
        {
            Terminal.Gui.Thickness Thickness1 = M_Border.GetSumThickness();
            return new Thickness(Thickness1.Left, Thickness1.Top, Thickness1.Right, Thickness1.Bottom);
        }

        public void DrawFullContent()
        {
            M_Border.DrawFullContent();
        }

        public void DrawContent(View view = null, bool fill = true)
        {
            M_Border.DrawContent(view.M_View, fill);
        }

        public void DrawTitle(View p1)
        {
            M_Border.DrawTitle(p1.M_View);
        }

        public void DrawTitle(View p1, Rect p2)
        {
            M_Border.DrawTitle(p1.M_View, p2.M_Rect);
        }

        public new string ToString()
        {
            return M_Border.ToString();
        }
    }

    [ContextClass("ТфГраница", "TfBorder")]
    public class TfBorder : AutoContext<TfBorder>
    {

        public TfBorder()
        {
            Border Border1 = new Border();
            Border1.dll_obj = this;
            Base_obj = Border1;
        }

        public Border Base_obj;

        [ContextProperty("АктуальнаяВысота", "ActualHeight")]
        public int ActualHeight
        {
            get { return Base_obj.ActualHeight; }
        }

        [ContextProperty("АктуальнаяШирина", "ActualWidth")]
        public int ActualWidth
        {
            get { return Base_obj.ActualWidth; }
        }

        [ContextProperty("Заголовок", "Title")]
        public string Title
        {
            get { return Base_obj.Title; }
            set { Base_obj.Title = value; }
        }

        [ContextProperty("Смещение3D", "Effect3DOffset")]
        public TfPoint Effect3DOffset
        {
            get { return new TfPoint(Base_obj.Effect3DOffset); }
            set { Base_obj.Effect3DOffset = value.Base_obj; }
        }

        [ContextProperty("СтильГраницы", "BorderStyle")]
        public int BorderStyle
        {
            get { return Base_obj.BorderStyle; }
            set { Base_obj.BorderStyle = value; }
        }

        [ContextProperty("ТолщинаГраницы", "BorderThickness")]
        public TfThickness BorderThickness
        {
            get { return new TfThickness(Base_obj.BorderThickness); }
            set { Base_obj.BorderThickness = value.Base_obj; }
        }

        [ContextProperty("Цвет3D", "Effect3DBrush")]
        public TfAttribute Effect3DBrush
        {
            get { return new TfAttribute(Base_obj.Effect3DBrush); }
            set { Base_obj.Effect3DBrush = value.Base_obj; }
        }

        [ContextProperty("ЦветГраницы", "BorderBrush")]
        public int BorderBrush
        {
            get { return Base_obj.BorderBrush; }
            set { Base_obj.BorderBrush = value; }
        }

        [ContextProperty("ЦветФона", "Background")]
        public int Background
        {
            get { return Base_obj.Background; }
            set { Base_obj.Background = value; }
        }

        [ContextProperty("Эффект3D", "Effect3D")]
        public bool Effect3D
        {
            get { return Base_obj.Effect3D; }
            set { Base_obj.Effect3D = value; }
        }

        [ContextMethod("ВычислитьТолщину", "GetSumThickness")]
        public TfThickness GetSumThickness()
        {
            return new TfThickness(Base_obj.GetSumThickness());
        }

    }
}
