using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace ostgui
{

    [ContextClass("ТфЭлементыПунктаМеню", "TfMenuBarItemChildren")]
    public class TfMenuBarItemChildren : AutoContext<TfMenuBarItemChildren>
    {

        public ostgui.MenuBarItem M_MenuBarItem;

        public Terminal.Gui.MenuItem[] M_Object
        {
            get { return M_MenuBarItem.Children; }
            set { M_MenuBarItem.Children = value; }
        }

        [ContextProperty("Количество", "Count")]
        public int Count
        {
            get { return M_Object.Length; }
        }

        [ContextMethod("Добавить", "Add")]
        public void Add(IValue p1)
        {
            Terminal.Gui.MenuItem[] MenuItem2 = new Terminal.Gui.MenuItem[M_Object.Length + 1];
            M_Object.CopyTo(MenuItem2, 0);
            MenuItem2[M_Object.Length] = (Terminal.Gui.MenuItem)((dynamic)p1).Base_obj.M_MenuItem;
            M_Object = MenuItem2;
        }

        [ContextMethod("Очистить", "Clear")]
        public void Clear()
        {
            M_Object = new Terminal.Gui.MenuItem[0];
        }

        [ContextMethod("Удалить", "Remove")]
        public void Remove(IValue p1)
        {
            Terminal.Gui.MenuItem[] MenuItem2 = new Terminal.Gui.MenuItem[M_Object.Length - 1];
            int index = 0;
            for (int i = 0; i < M_Object.Length; i++)
            {
                Terminal.Gui.MenuItem MenuItem1 = M_Object[i];
                if (MenuItem1 != ((dynamic)p1).Base_obj.M_MenuItem)
                {
                    MenuItem2[index] = MenuItem1;
                    index++;
                }
            }
            M_Object = MenuItem2;
        }

        [ContextMethod("ЭлементПунктаМеню", "ItemMenuBarItem")]
        public TfMenuItem ItemMenuBarItem(int p1)
        {
            return OneScriptTerminalGui.RevertEqualsObj(M_Object[p1]).dll_obj;
        }

    }
}
