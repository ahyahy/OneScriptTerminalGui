using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library.ValueList;
using System.Collections;

namespace ostgui
{
    public class MenuBarItem : ostgui.MenuItem
    {
        public new TfMenuBarItem dll_obj;
        public Terminal.Gui.MenuBarItem M_MenuBarItem;
        public new System.Action Clicked;

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
        public Terminal.Gui.MenuBar M_MenuBar { get; set; }

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

        [ContextProperty("КлавишаВызова", "HotKey")]
        public string HotKey
        {
            get { return Base_obj.HotKey; }
        }

        [ContextProperty("Метка", "Tag")]
        public IValue Tag
        {
            get { return Base_obj.Tag; }
            set { Base_obj.Tag = value; }
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

        [ContextProperty("СочетаниеКлавишДействие", "ShortcutAction")]
        public TfAction ShortcutAction { get; set; }

        [ContextMethod("ДобавитьСочетаниеКлавиш", "AddShortcut")]
        public void AddShortcut(decimal p1)
        {
            OneScriptTerminalGui.AddToShortcutDictionary(p1, this);
        }

        [ContextMethod("ПолучитьСочетаниеКлавиш", "GetShortcut")]
        public ValueListImpl GetShortcut()
        {
            ValueListImpl ValueListImpl1 = new ValueListImpl();
            ArrayList ArrayList1 = OneScriptTerminalGui.GetFromShortcutDictionary(this);
            for (int i = 0; i < ArrayList1.Count; i++)
            {
                decimal shortcut = (decimal)ArrayList1[i];
                ValueListImpl1.Add(ValueFactory.Create(shortcut), OneScriptTerminalGui.instance.Keys.ToStringRu(shortcut));
            }
            if (ValueListImpl1.Count() > 0)
            {
                return ValueListImpl1;
            }
            return null;
        }

        [ContextMethod("УдалитьСочетаниеКлавиш", "RemoveShortcut")]
        public void RemoveShortcut(decimal p1)
        {
            OneScriptTerminalGui.RemoveFromShortcutDictionary(p1, this);
        }

    }
}
