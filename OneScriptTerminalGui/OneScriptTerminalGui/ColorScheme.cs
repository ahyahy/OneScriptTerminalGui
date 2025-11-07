using ScriptEngine.Machine.Contexts;

namespace ostgui
{
    public class ColorScheme
    {
        public TfColorScheme dll_obj;
        public Terminal.Gui.ColorScheme M_ColorScheme;

        public ColorScheme()
        {
            M_ColorScheme = new Terminal.Gui.ColorScheme();
            Utils.AddToHashtable(M_ColorScheme, this);
        }

        public ColorScheme(Terminal.Gui.ColorScheme p1)
        {
            M_ColorScheme = p1;
            Utils.AddToHashtable(M_ColorScheme, this);
        }

        public Attribute HotNormal
        {
            get { return Utils.RevertEqualsObj(M_ColorScheme.HotNormal); }
            set { M_ColorScheme.HotNormal = value.M_Attribute; }
        }

        public Attribute HotFocus
        {
            get { return Utils.RevertEqualsObj(M_ColorScheme.HotFocus); }
            set { M_ColorScheme.HotFocus = value.M_Attribute; }
        }

        public Attribute Normal
        {
            get { return Utils.RevertEqualsObj(M_ColorScheme.Normal); }
            set { M_ColorScheme.Normal = value.M_Attribute; }
        }

        public Attribute Disabled
        {
            get { return Utils.RevertEqualsObj(M_ColorScheme.Disabled); }
            set { M_ColorScheme.Disabled = value.M_Attribute; }
        }

        public Attribute Focus
        {
            get { return Utils.RevertEqualsObj(M_ColorScheme.Focus); }
            set { M_ColorScheme.Focus = value.M_Attribute; }
        }

        public new string ToString()
        {
            return M_ColorScheme.ToString();
        }
    }

    [ContextClass("ТфЦветоваяСхема", "TfColorScheme")]
    public class TfColorScheme : AutoContext<TfColorScheme>
    {

        public TfColorScheme()
        {
            ColorScheme ColorScheme1 = new ColorScheme();
            ColorScheme1.dll_obj = this;
            Base_obj = ColorScheme1;
        }

        public TfColorScheme(ostgui.ColorScheme p1)
        {
            ColorScheme ColorScheme1 = p1;
            ColorScheme1.dll_obj = this;
            Base_obj = ColorScheme1;
        }

        public ColorScheme Base_obj;

        [ContextProperty("ГорячийНормальный", "HotNormal")]
        public TfAttribute HotNormal
        {
            get { return Base_obj.HotNormal.dll_obj; }
            set { Base_obj.HotNormal = value.Base_obj; }
        }

        [ContextProperty("ГорячийФокус", "HotFocus")]
        public TfAttribute HotFocus
        {
            get { return Base_obj.HotFocus.dll_obj; }
            set { Base_obj.HotFocus = value.Base_obj; }
        }

        [ContextProperty("Нормальный", "Normal")]
        public TfAttribute Normal
        {
            get { return Base_obj.Normal.dll_obj; }
            set { Base_obj.Normal = value.Base_obj; }
        }

        [ContextProperty("Отключено", "Disabled")]
        public TfAttribute Disabled
        {
            get { return Base_obj.Disabled.dll_obj; }
            set { Base_obj.Disabled = value.Base_obj; }
        }

        [ContextProperty("Фокус", "Focus")]
        public TfAttribute Focus
        {
            get { return Base_obj.Focus.dll_obj; }
            set { Base_obj.Focus = value.Base_obj; }
        }

    }
}
