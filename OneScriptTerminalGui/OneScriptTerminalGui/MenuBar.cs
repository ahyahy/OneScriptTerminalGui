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
            return M_MenuBar.CloseMenu();
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
            menusCollection.M_MenuBar = Base_obj;
        }

        public MenuBar Base_obj;

        public TfAction LayoutComplete { get; set; }
        public TfAction LayoutStarted { get; set; }
        public TfAction DrawContentComplete { get; set; }
        public TfAction DrawContent { get; set; }
        public TfAction ShortcutAction { get; set; }

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

        [ContextProperty("ДобавленЭлемент", "Added")]
        public TfAction Added { get; set; }

        [ContextProperty("ДоступностьИзменена", "EnabledChanged")]
        public TfAction EnabledChanged { get; set; }

        [ContextProperty("КлавишаВызоваИзменена", "HotKeyChanged")]
        public TfAction HotKeyChanged { get; set; }

        [ContextProperty("КлавишаНажата", "KeyPress")]
        public TfAction KeyPress { get; set; }

        [ContextProperty("МышьНадЭлементом", "MouseEnter")]
        public TfAction MouseEnter { get; set; }

        [ContextProperty("МышьПокинулаЭлемент", "MouseLeave")]
        public TfAction MouseLeave { get; set; }

        [ContextProperty("Открыто", "MenuOpened")]
        public TfAction MenuOpened { get; set; }

        [ContextProperty("ПриВходе", "Enter")]
        public TfAction Enter { get; set; }

        [ContextProperty("ПриЗакрытии", "MenuClosing")]
        public TfAction MenuClosing { get; set; }

        [ContextProperty("ПриНажатииМыши", "MouseClick")]
        public TfAction MouseClick { get; set; }

        [ContextProperty("ПриОткрытии", "MenuOpening")]
        public TfAction MenuOpening { get; set; }

        [ContextProperty("ФокусируемостьИзменена", "CanFocusChanged")]
        public TfAction CanFocusChanged { get; set; }

        [ContextProperty("ЭлементАктивирован", "InitializedItem")]
        public TfAction InitializedItem { get; set; }

        [ContextProperty("ЭлементПокинут", "Leave")]
        public TfAction Leave { get; set; }

        [ContextProperty("ЭлементУдален", "Removed")]
        public TfAction Removed { get; set; }

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
