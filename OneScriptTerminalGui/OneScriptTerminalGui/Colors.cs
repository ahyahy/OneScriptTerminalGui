using ScriptEngine.Machine.Contexts;

namespace ostgui
{
    public class Colors
    {
        public TfColors dll_obj;
        public Terminal.Gui.ColorScheme M_Colors;

        public Colors()
        {
            M_Colors = new Terminal.Gui.ColorScheme();
        }

        public ColorScheme TopLevel
        {
            get { return new ColorScheme(Terminal.Gui.Colors.TopLevel); }
            set { M_Colors = value.M_ColorScheme; }
        }

        public ColorScheme Dialog
        {
            get { return new ColorScheme(Terminal.Gui.Colors.Dialog); }
            set { M_Colors = value.M_ColorScheme; }
        }

        public ColorScheme Menu
        {
            get { return new ColorScheme(Terminal.Gui.Colors.Menu); }
            set { M_Colors = value.M_ColorScheme; }
        }

        public ColorScheme Base
        {
            get { return new ColorScheme(Terminal.Gui.Colors.Base); }
            set { M_Colors = value.M_ColorScheme; }
        }

        public ColorScheme Error
        {
            get { return new ColorScheme(Terminal.Gui.Colors.Error); }
            set { M_Colors = value.M_ColorScheme; }
        }

        public new string ToString()
        {
            return M_Colors.ToString();
        }
    }

    [ContextClass("ТфЦвета", "TfColors")]
    public class TfColors : AutoContext<TfColors>
    {

        public TfColors()
        {
            Colors Colors1 = new Colors();
            Colors1.dll_obj = this;
            Base_obj = Colors1;
        }

        public Colors Base_obj;

        [ContextProperty("Верхний", "TopLevel")]
        public TfColorScheme TopLevel
        {
            get { return new TfColorScheme(Base_obj.TopLevel); }
            set { Base_obj.TopLevel = value.Base_obj; }
        }

        [ContextProperty("Диалог", "Dialog")]
        public TfColorScheme Dialog
        {
            get { return new TfColorScheme(Base_obj.Dialog); }
            set { Base_obj.Dialog = value.Base_obj; }
        }

        [ContextProperty("Меню", "Menu")]
        public TfColorScheme Menu
        {
            get { return new TfColorScheme(Base_obj.Menu); }
            set { Base_obj.Menu = value.Base_obj; }
        }

        [ContextProperty("Основа", "Base")]
        public TfColorScheme Base
        {
            get { return new TfColorScheme(Base_obj.Base); }
            set { Base_obj.Base = value.Base_obj; }
        }

        [ContextProperty("Ошибка", "Error")]
        public TfColorScheme Error
        {
            get { return new TfColorScheme(Base_obj.Error); }
            set { Base_obj.Error = value.Base_obj; }
        }

    }
}
