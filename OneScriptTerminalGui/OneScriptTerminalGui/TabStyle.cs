using ScriptEngine.Machine.Contexts;

namespace ostgui
{
    public class TabStyle
    {
        public TfTabStyle dll_obj;
        public Terminal.Gui.TabView.TabStyle M_TabStyle;

        public TabStyle()
        {
            M_TabStyle = new Terminal.Gui.TabView.TabStyle();
            Utils.AddToHashtable(M_TabStyle, this);
        }

        public new string ToString()
        {
            return M_TabStyle.ToString();
        }

        public bool TabsOnBottom
        {
            get { return M_TabStyle.TabsOnBottom; }
            set { M_TabStyle.TabsOnBottom = value; }
        }

        public bool ShowTopLine
        {
            get { return M_TabStyle.ShowTopLine; }
            set { M_TabStyle.ShowTopLine = value; }
        }

        public bool ShowBorder
        {
            get { return M_TabStyle.ShowBorder; }
            set { M_TabStyle.ShowBorder = value; }
        }
    }

    [ContextClass("ТфСтильВкладки", "TfTabStyle")]
    public class TfTabStyle : AutoContext<TfTabStyle>
    {

        public TfTabStyle()
        {
            TabStyle TabStyle1 = new TabStyle();
            TabStyle1.dll_obj = this;
            Base_obj = TabStyle1;
        }

        public TabStyle Base_obj;

        [ContextProperty("ВкладкиВнизу", "TabsOnBottom")]
        public bool TabsOnBottom
        {
            get { return Base_obj.TabsOnBottom; }
            set { Base_obj.TabsOnBottom = value; }
        }

        [ContextProperty("ПоказатьВерхнююГраницу", "ShowTopLine")]
        public bool ShowTopLine
        {
            get { return Base_obj.ShowTopLine; }
            set { Base_obj.ShowTopLine = value; }
        }

        [ContextProperty("ПоказатьГраницу", "ShowBorder")]
        public bool ShowBorder
        {
            get { return Base_obj.ShowBorder; }
            set { Base_obj.ShowBorder = value; }
        }

    }
}
