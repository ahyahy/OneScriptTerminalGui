using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace ostgui
{
    public class MenuBarItem : ostgui.MenuItem
    {
        public new TfMenuBarItem dll_obj;
        public Terminal.Gui.MenuBarItem M_MenuBarItem;

        public MenuBarItem()
        {
            Clicked = delegate ()
            {
                if (dll_obj.Clicked != null)
                {
                    TfEventArgs TfEventArgs1 = new TfEventArgs();
                    TfEventArgs1.sender = dll_obj;
                    TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.Clicked);
                    OneScriptTerminalGui.Event = TfEventArgs1;
                    OneScriptTerminalGui.ExecuteEvent(dll_obj.Clicked);
                }
            };

            M_MenuBarItem = new Terminal.Gui.MenuBarItem();
            base.M_MenuItem = M_MenuBarItem;
            M_MenuBarItem.Action = Clicked;
            OneScriptTerminalGui.AddToHashtable(M_MenuBarItem, this);
        }

        public Terminal.Gui.MenuItem[] Children
        {
            get { return M_MenuBarItem.Children; }
            set { M_MenuBarItem.Children = value; }
        }

        public new string ToString()
        {
            return M_MenuBarItem.ToString();
        }
    }

    [ContextClass("ТфПунктМеню", "TfMenuBarItem")]
    public class TfMenuBarItem : AutoContext<TfMenuBarItem>
    {

        private TfMenuBarItemChildren menuBarItemChildren;

        public TfMenuBarItem()
        {
            MenuBarItem MenuBarItem1 = new MenuBarItem();
            MenuBarItem1.dll_obj = this;
            Base_obj = MenuBarItem1;

            menuBarItemChildren = new TfMenuBarItemChildren();
            menuBarItemChildren.M_MenuBarItem = Base_obj;
        }

        public MenuBarItem Base_obj;

        [ContextProperty("Данные", "Data")]
        public IValue Data
        {
            get { return OneScriptTerminalGui.RevertObj(Base_obj.Data); }
            set { Base_obj.Data = value; }
        }

        [ContextProperty("Заголовок", "Title")]
        public string Title
        {
            get { return Base_obj.Title; }
            set { Base_obj.Title = value; }
        }

        [ContextProperty("Подсказка", "Help")]
        public string Help
        {
            get { return Base_obj.Help; }
            set { Base_obj.Help = value; }
        }

        [ContextProperty("Элементы", "Children")]
        public TfMenuBarItemChildren Children
        {
            get { return menuBarItemChildren; }
        }

        [ContextProperty("Нажатие", "Clicked")]
        public TfAction Clicked { get; set; }

    }
}
