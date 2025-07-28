using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace ostgui
{
    public class StatusBar : View
    {
        public new TfStatusBar dll_obj;
        public Terminal.Gui.StatusBar M_StatusBar;

        public StatusBar()
        {
            M_StatusBar = new Terminal.Gui.StatusBar();
            base.M_View = M_StatusBar;
            OneScriptTerminalGui.AddToHashtable(M_StatusBar, this);
        }

        public string ShortcutDelimiter
        {
            get { return Terminal.Gui.StatusBar.ShortcutDelimiter.ToString(); }
            set { Terminal.Gui.StatusBar.ShortcutDelimiter = value; }
        }

        public void AddItemAt(int p1, StatusItem p2)
        {
            M_StatusBar.AddItemAt(p1, p2.M_StatusItem);
        }

        public Terminal.Gui.StatusItem[] Items
        {
            get { return M_StatusBar.Items; }
            set { M_StatusBar.Items = value; }
        }

        public new string ToString()
        {
            return M_StatusBar.ToString();
        }

        public new Toplevel GetTopSuperView()
        {
            return OneScriptTerminalGui.RevertEqualsObj(M_StatusBar.GetTopSuperView());
        }
    }

    [ContextClass("ТфСтрокаСостояния", "TfStatusBar")]
    public class TfStatusBar : AutoContext<TfStatusBar>
    {

        private TfStatusBarItems statusBarItems;

        public TfStatusBar()
        {
            StatusBar StatusBar1 = new StatusBar();
            StatusBar1.dll_obj = this;
            Base_obj = StatusBar1;

            statusBarItems = new TfStatusBarItems();
            statusBarItems.M_StatusBar = Base_obj.M_StatusBar;
        }

        public TfAction HotKeyChanged { get; set; }
        public TfAction LayoutComplete { get; set; }
        public TfAction LayoutStarted { get; set; }
        public TfAction DrawContentComplete { get; set; }
        public TfAction DrawContent { get; set; }
        public TfAction Added { get; set; }
        public TfAction InitializedItem { get; set; }
        public TfAction Removed { get; set; }
        public TfAction KeyPress { get; set; }

        public StatusBar Base_obj;

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

        [ContextProperty("Метка", "Tag")]
        public IValue Tag
        {
            get { return Base_obj.Tag; }
            set { Base_obj.Tag = value; }
        }

        [ContextProperty("Отображать", "Visible")]
        public bool Visible
        {
            get { return Base_obj.Visible; }
            set { Base_obj.Visible = value; }
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

        [ContextProperty("Элементы", "Items")]
        public TfStatusBarItems Items
        {
            get { return statusBarItems; }
        }

        [ContextProperty("ВидимостьИзменена", "VisibleChanged")]
        public TfAction VisibleChanged { get; set; }

        [ContextProperty("ДоступностьИзменена", "EnabledChanged")]
        public TfAction EnabledChanged { get; set; }

        [ContextProperty("МышьНадЭлементом", "MouseEnter")]
        public TfAction MouseEnter { get; set; }

        [ContextProperty("МышьПокинулаЭлемент", "MouseLeave")]
        public TfAction MouseLeave { get; set; }

        [ContextProperty("ПриНажатииМыши", "MouseClick")]
        public TfAction MouseClick { get; set; }

        [ContextMethod("ВерхнийРодитель", "GetTopSuperView")]
        public IValue GetTopSuperView()
        {
            return Base_obj.GetTopSuperView().dll_obj;
        }

        [ContextMethod("ВставитьПоИндексу", "AddItemAt")]
        public void AddItemAt(int p1, TfStatusItem p2)
        {
            Base_obj.AddItemAt(p1, p2.Base_obj);
        }

        [ContextMethod("ВСтроку", "ToString")]
        public new string ToString()
        {
            return Base_obj.ToString();
        }

        [ContextMethod("ТочкаНаЭлементе", "ScreenToView")]
        public TfPoint ScreenToView(int p1, int p2)
        {
            return new TfPoint(Base_obj.ScreenToView(p1, p2));
        }

        [ContextMethod("Удалить", "Remove")]
        public void Remove(TfStatusItem p1)
        {
            Items.Remove(p1);
        }

        [ContextMethod("УдалитьВсе", "RemoveAll")]
        public void RemoveAll()
        {
            Items.Clear();
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
