using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace ostgui
{
    public class TabPage
    {
        public TfTabPage dll_obj;
        public Terminal.Gui.TabView.Tab M_TabPage;

        public TabPage()
        {
            M_TabPage = new Terminal.Gui.TabView.Tab();
            Utils.AddToHashtable(M_TabPage, this);
        }

        public TabPage(string p1, View p2)
        {
            M_TabPage = new Terminal.Gui.TabView.Tab(p1, p2.M_View);
            Utils.AddToHashtable(M_TabPage, this);
        }

        public new string ToString()
        {
            return M_TabPage.ToString();
        }

        public string Text
        {
            get { return M_TabPage.Text.ToString(); }
            set { M_TabPage.Text = value; }
        }
    }

    [ContextClass("ТфВкладка", "TfTabPage")]
    public class TfTabPage : AutoContext<TfTabPage>
    {

        public TfTabPage()
        {
            TabPage TabPage1 = new TabPage();
            TabPage1.dll_obj = this;
            Base_obj = TabPage1;
        }

        public TfTabPage(string p1, IValue p2)
        {
            TabPage TabPage1 = new TabPage(p1, ((dynamic)p2).Base_obj);
            TabPage1.dll_obj = this;
            Base_obj = TabPage1;
        }

        public TabPage Base_obj;

        [ContextProperty("Текст", "Text")]
        public string Text
        {
            get { return Base_obj.Text; }
            set { Base_obj.Text = value; }
        }

        [ContextProperty("Элемент", "View")]
        public IValue View
        {
            get { return Utils.RevertEqualsObj(Base_obj.M_TabPage.View).dll_obj; }
            set { Base_obj.M_TabPage.View = ((dynamic)value).Base_obj.M_View; }
        }

    }
}
