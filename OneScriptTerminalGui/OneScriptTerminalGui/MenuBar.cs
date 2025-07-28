using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace ostgui
{
    public class MenuBar : View
    {
        public new TfMenuBar dll_obj;
        public Terminal.Gui.MenuBar M_MenuBar;

        public MenuBar()
        {
            M_MenuBar = new Terminal.Gui.MenuBar();
            base.M_View = M_MenuBar;
            OneScriptTerminalGui.AddToHashtable(M_MenuBar, this);

            M_MenuBar.MenuAllClosed += M_MenuBar_MenuAllClosed;
            M_MenuBar.MenuOpened += M_MenuBar_MenuOpened;
            M_MenuBar.MenuOpening += M_MenuBar_MenuOpening;
            M_MenuBar.MouseLeave += M_MenuBar_MouseLeave;
        }

        private void M_MenuBar_MouseLeave(Terminal.Gui.View.MouseEventArgs obj)
        {
            if (dll_obj.MouseLeave != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.MouseLeave);
                TfEventArgs1.flags = ValueFactory.Create((int)obj.MouseEvent.Flags);
                TfEventArgs1.view = OneScriptTerminalGui.RevertEqualsObj(M_MenuBar).dll_obj;
                TfEventArgs1.x = ValueFactory.Create(obj.MouseEvent.X);
                TfEventArgs1.y = ValueFactory.Create(obj.MouseEvent.Y);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(dll_obj.MouseLeave);
            }
        }

        private void M_MenuBar_MenuOpening(Terminal.Gui.MenuOpeningEventArgs obj)
        {
            if (dll_obj.MenuOpening != null)
            {
                obj.NewMenuBarItem = obj.CurrentMenu;
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.MenuOpening);
                TfEventArgs1.cancel = ValueFactory.Create(false);
                TfEventArgs1.cancel = ValueFactory.Create(obj.Cancel);
                TfEventArgs1.currentMenu = OneScriptTerminalGui.RevertEqualsObj(obj.CurrentMenu).dll_obj;
                TfEventArgs1.newMenuBarItem = OneScriptTerminalGui.RevertEqualsObj(obj.NewMenuBarItem).dll_obj;
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(dll_obj.MenuOpening);

                if (TfEventArgs1.Cancel)
                {
                    M_MenuBar.MenuOpening -= M_MenuBar_MenuOpening;
                    obj.NewMenuBarItem = ((TfMenuBarItem)TfEventArgs1.NewMenuBarItem).Base_obj.M_MenuBarItem;
                    M_MenuBar.MenuOpening += M_MenuBar_MenuOpening;
                }
            }
        }

        private void M_MenuBar_MenuOpened(Terminal.Gui.MenuItem obj)
        {
            if (dll_obj.MenuOpened != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.MenuOpened);
                TfEventArgs1.menuItem = OneScriptTerminalGui.RevertEqualsObj(obj).dll_obj;
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(dll_obj.MenuOpened);
            }
        }

        private void M_MenuBar_MenuAllClosed()
        {
            if (dll_obj.MenuAllClosed != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.MenuAllClosed);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(dll_obj.MenuAllClosed);
            }
        }

        public bool UseSubMenusSingleFrame
        {
            get { return M_MenuBar.UseSubMenusSingleFrame; }
            set { M_MenuBar.UseSubMenusSingleFrame = value; }
        }

        public bool UseKeysUpDownAsKeysLeftRight
        {
            get { return M_MenuBar.UseKeysUpDownAsKeysLeftRight; }
            set { M_MenuBar.UseKeysUpDownAsKeysLeftRight = value; }
        }

        public bool IsMenuOpen
        {
            get { return M_MenuBar.IsMenuOpen; }
        }

        public new bool Visible
        {
            get { return M_MenuBar.Visible; }
            set { M_MenuBar.Visible = value; }
        }

        public bool CloseMenu()
        {
            M_MenuBar.OnMenuAllClosed();
            return M_MenuBar.CloseMenu(true);
        }

        public void OpenMenu()
        {
            M_MenuBar.OpenMenu();
        }

        public int Key
        {
            get { return (int)M_MenuBar.Key; }
            set { M_MenuBar.Key = (Terminal.Gui.Key)value; }
        }

        public Terminal.Gui.MenuBarItem[] Menus
        {
            get { return M_MenuBar.Menus; }
            set { M_MenuBar.Menus = value; }
        }

        public ostgui.View LastFocused
        {
            get { return OneScriptTerminalGui.RevertEqualsObj(M_MenuBar.LastFocused); }
        }

        public string ShortcutDelimiter
        {
            get { return Terminal.Gui.MenuBar.ShortcutDelimiter.ToString(); }
            set { Terminal.Gui.MenuBar.ShortcutDelimiter = value; }
        }

        public new string ToString()
        {
            return M_MenuBar.ToString();
        }

        public new Toplevel GetTopSuperView()
        {
            return OneScriptTerminalGui.RevertEqualsObj(M_MenuBar.GetTopSuperView());
        }

        public int OpenIndex
        {
            get { return M_MenuBar.OpenIndex; }
            set { M_MenuBar.OpenIndex = value; }
        }
    }

    [ContextClass("ТфПанельМеню", "TfMenuBar")]
    public class TfMenuBar : AutoContext<TfMenuBar>
    {

        private TfMenusCollection menusCollection;

        public TfMenuBar()
        {
            MenuBar MenuBar1 = new MenuBar();
            MenuBar1.dll_obj = this;
            Base_obj = MenuBar1;

            menusCollection = new TfMenusCollection();
            menusCollection.M_MenuBar = Base_obj.M_MenuBar;
        }

        public TfAction LayoutComplete { get; set; }
        public TfAction LayoutStarted { get; set; }
        public TfAction DrawContentComplete { get; set; }
        public TfAction DrawContent { get; set; }
        public TfAction ShortcutAction { get; set; }
        public TfAction Added { get; set; }
        public TfAction InitializedItem { get; set; }
        public TfAction Initialized { get; set; }
        public TfAction MenuClosing { get; set; }
        public TfAction KeyPress { get; set; }
        public TfAction Removed { get; set; }
        public TfAction MouseClick { get; set; }
        public TfAction CanFocusChanged { get; set; }
        public TfAction Enter { get; set; }
        public TfAction Leave { get; set; }

        public MenuBar Base_obj;

        [ContextProperty("Данные", "Data")]
        public IValue Data
        {
            get { return OneScriptTerminalGui.RevertObj(Base_obj.Data); }
            set { Base_obj.Data = value; }
        }

        [ContextProperty("Добавлено", "IsAdded")]
        public bool IsAdded
        {
            get { return Base_obj.IsAdded; }
        }

        [ContextProperty("Доступность", "Enabled")]
        public bool Enabled
        {
            get { return Base_obj.Enabled; }
            set { Base_obj.Enabled = value; }
        }

        [ContextProperty("Игрек", "Y")]
        public TfPos Y
        {
            get { return new TfPos(Base_obj.Y); }
            set { Base_obj.Y = value.Base_obj; }
        }

        [ContextProperty("Идентификатор", "Id")]
        public string Id
        {
            get { return Base_obj.Id; }
            set { Base_obj.Id = value; }
        }

        [ContextProperty("Икс", "X")]
        public TfPos X
        {
            get { return new TfPos(Base_obj.X); }
            set { Base_obj.X = value.Base_obj; }
        }

        [ContextProperty("ИндексОткрываемого", "OpenIndex")]
        public int OpenIndex
        {
            get { return Base_obj.OpenIndex; }
            set { Base_obj.OpenIndex = value; }
        }

        [ContextProperty("Кадр", "Frame")]
        public TfRect Frame
        {
            get { return new TfRect(Base_obj.Frame.M_Rect.X, Base_obj.Frame.M_Rect.Y, Base_obj.Frame.M_Rect.Width, Base_obj.Frame.M_Rect.Height); }
            set { Base_obj.Frame = value.Base_obj; }
        }

        [ContextProperty("Клавиша", "Key")]
        public int Key
        {
            get { return Base_obj.Key; }
            set { Base_obj.Key = value; }
        }

        [ContextProperty("Метка", "Tag")]
        public IValue Tag
        {
            get { return Base_obj.Tag; }
            set { Base_obj.Tag = value; }
        }

        [ContextProperty("Открыто", "IsMenuOpen")]
        public bool IsMenuOpen
        {
            get { return Base_obj.IsMenuOpen; }
        }

        [ContextProperty("Отображать", "Visible")]
        public bool Visible
        {
            get { return Base_obj.Visible; }
            set { Base_obj.Visible = value; }
        }

        [ContextProperty("ПодМеню", "Menus")]
        public TfMenusCollection Menus
        {
            get { return menusCollection; }
        }

        [ContextProperty("Родитель", "SuperView")]
        public IValue SuperView
        {
            get { return OneScriptTerminalGui.RevertEqualsObj(Base_obj.SuperView.M_View).dll_obj; }
        }

        [ContextProperty("ЦветоваяСхема", "ColorScheme")]
        public TfColorScheme ColorScheme
        {
            get { return Base_obj.ColorScheme.dll_obj; }
            set { Base_obj.ColorScheme = value.Base_obj; }
        }

        [ContextProperty("ВидимостьИзменена", "VisibleChanged")]
        public TfAction VisibleChanged { get; set; }

        [ContextProperty("ВсеЗакрыты", "MenuAllClosed")]
        public TfAction MenuAllClosed { get; set; }

        [ContextProperty("ДоступностьИзменена", "EnabledChanged")]
        public TfAction EnabledChanged { get; set; }

        [ContextProperty("МенюОткрыто", "MenuOpened")]
        public TfAction MenuOpened { get; set; }

        [ContextProperty("МышьНадЭлементом", "MouseEnter")]
        public TfAction MouseEnter { get; set; }

        [ContextProperty("МышьПокинулаЭлемент", "MouseLeave")]
        public TfAction MouseLeave { get; set; }

        [ContextProperty("ПриОткрытии", "MenuOpening")]
        public TfAction MenuOpening { get; set; }

        [ContextMethod("ВерхнийРодитель", "GetTopSuperView")]
        public IValue GetTopSuperView()
        {
            return Base_obj.GetTopSuperView().dll_obj;
        }

        [ContextMethod("ВСтроку", "ToString")]
        public new string ToString()
        {
            return Base_obj.ToString();
        }

        [ContextMethod("Закрыть", "CloseMenu")]
        public bool CloseMenu()
        {
            return Base_obj.CloseMenu();
        }

        [ContextMethod("Открыть", "OpenMenu")]
        public void OpenMenu()
        {
            Base_obj.OpenMenu();
        }

        [ContextMethod("ТочкаНаЭлементе", "ScreenToView")]
        public TfPoint ScreenToView(int p1, int p2)
        {
            return new TfPoint(Base_obj.ScreenToView(p1, p2));
        }

        [ContextMethod("Удалить", "Remove")]
        public void Remove(TfMenuBarItem p1)
        {
            Menus.Remove(p1);
        }

        [ContextMethod("УдалитьВсе", "RemoveAll")]
        public void RemoveAll()
        {
            Menus.Clear();
        }

        [ContextMethod("ЦветВыделенного", "GetHotNormalColor")]
        public TfAttribute GetHotNormalColor()
        {
            return new TfAttribute(Base_obj.GetHotNormalColor());
        }

        [ContextMethod("ЦветОбычного", "GetNormalColor")]
        public TfAttribute GetNormalColor()
        {
            return new TfAttribute(Base_obj.GetNormalColor());
        }

        [ContextMethod("ЦветФокуса", "GetFocusColor")]
        public TfAttribute GetFocusColor()
        {
            return new TfAttribute(Base_obj.GetFocusColor());
        }

    }
}
