using ScriptEngine.Machine.Contexts;

namespace ostgui
{
    [ContextClass("ТфКоллекцияПодменю", "TfMenusCollection")]
    public class TfMenusCollection : AutoContext<TfMenusCollection>
    {

        public Terminal.Gui.MenuBar M_MenuBar;

        public Terminal.Gui.MenuBarItem[] M_Object
        {
            get { return M_MenuBar.Menus; }
            set { M_MenuBar.Menus = value; }
        }

        [ContextProperty("Количество", "Count")]
        public int Count
        {
            get { return M_Object.Length; }
        }

        [ContextMethod("Добавить", "Add")]
        public TfMenuBarItem Add(TfMenuBarItem p1)
        {
            Terminal.Gui.MenuBarItem[] MenuBarItem2 = new Terminal.Gui.MenuBarItem[M_Object.Length + 1];
            M_Object.CopyTo(MenuBarItem2, 0);
            MenuBarItem2[M_Object.Length] = p1.Base_obj.M_MenuBarItem;
            M_Object = MenuBarItem2;
            p1.M_MenuBar = M_MenuBar;
            return p1;
        }

        [ContextMethod("Очистить", "Clear")]
        public void Clear()
        {
            M_Object = new Terminal.Gui.MenuBarItem[0];
        }

        [ContextMethod("Получить", "Get")]
        public TfMenuBarItem Get(int p1)
        {
            return Utils.RevertEqualsObj(M_Object[p1]).dll_obj;
        }

        [ContextMethod("Удалить", "Remove")]
        public void Remove(TfMenuBarItem p1)
        {
            Terminal.Gui.MenuBarItem[] MenuBarItem2 = new Terminal.Gui.MenuBarItem[M_Object.Length - 1];
            int index = 0;
            for (int i = 0; i < M_Object.Length; i++)
            {
                Terminal.Gui.MenuBarItem MenuBarItem1 = M_Object[i];
                if (MenuBarItem1 != p1.Base_obj.M_MenuBarItem)
                {
                    MenuBarItem2[index] = MenuBarItem1;
                    index++;
                }
            }
            M_Object = MenuBarItem2;
        }

    }
}
