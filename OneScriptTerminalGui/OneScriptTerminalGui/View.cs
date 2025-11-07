using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using Terminal.Gui;
using System.Collections;
using ScriptEngine.HostedScript.Library.ValueList;

namespace ostgui
{
    public class View : Responder
    {
        public TfView dll_obj;
        private Terminal.Gui.View m_View;

        public View(Terminal.Gui.View view = null)
        {
            if (view != null)
            {
                M_View = view;
                Utils.AddToHashtable(M_View, this);
            }
            else
            {
                M_View = new Terminal.Gui.View();
                Utils.AddToHashtable(M_View, this);
            }
        }
        public View(Terminal.Gui.Rect p1)
        {
            M_View = new Terminal.Gui.View(p1);
            Utils.AddToHashtable(M_View, this);
        }
        public View(int p1, int p2, string p3)
        {
            M_View = new Terminal.Gui.View(p1, p2, p3);
            Utils.AddToHashtable(M_View, this);
        }
        public View(Terminal.Gui.Rect p1, string p2, Terminal.Gui.Border p3)
        {
            M_View = new Terminal.Gui.View(p1, p2, p3);
            Utils.AddToHashtable(M_View, this);
        }
        public View(string p1, int p2, Terminal.Gui.Border p3)
        {
            M_View = new Terminal.Gui.View(p1, (Terminal.Gui.TextDirection)p2, p3);
            Utils.AddToHashtable(M_View, this);
        }

        public Terminal.Gui.View M_View
        {
            get { return m_View; }
            set
            {
                m_View = value;
                base.M_Responder = m_View;
                m_View.Added += M_View_Added;
                m_View.CanFocusChanged += M_View_CanFocusChanged;
                m_View.EnabledChanged += M_View_EnabledChanged;
                m_View.Enter += M_View_Enter;
                m_View.HotKeyChanged += M_View_HotKeyChanged;
                m_View.Initialized += M_View_Initialized;
                //m_View.KeyDown += M_View_KeyDown;
                m_View.KeyPress += M_View_KeyPress;
                //m_View.KeyUp += M_View_KeyUp;
                m_View.Leave += M_View_Leave;
                m_View.MouseClick += M_View_MouseClick;
                m_View.MouseEnter += M_View_MouseEnter;
                m_View.MouseLeave += M_View_MouseLeave;
                m_View.Removed += M_View_Removed;

                System.Action OnShortcutAction = delegate ()
                {
                    dynamic Sender = Utils.RevertEqualsObj(M_View).dll_obj;
                    TfAction action;
                    try
                    {
                        action = Sender.ShortcutAction;
                    }
                    catch
                    {
                        return;
                    }
                    if (action != null)
                    {
                        TfEventArgs TfEventArgs1 = new TfEventArgs();
                        TfEventArgs1.sender = Sender;
                        TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(action);
                        OneScriptTerminalGui.Event = TfEventArgs1;
                        OneScriptTerminalGui.ExecuteEvent(action);
                    }
                };
                try
                {
                    m_View.ShortcutAction = OnShortcutAction;
                }
                catch { }

                // Обеспечим данными событие мыши MouseEnter.
                Application.RootMouseEvent += delegate (MouseEvent me)
                {
                    if (Utils.noMouseEvent)
                    {
                        return;
                    }

                    Terminal.Gui.View host1 = me.View;
                    MouseEvent myme = new MouseEvent();
                    myme.Flags = me.Flags;
                    myme.Handled = me.Handled;
                    myme.View = me.View;
                    int meX = me.X;
                    int meY = me.Y;
                    Terminal.Gui.Point point;
                    try
                    {
                        point = host1.ScreenToView(me.X, me.Y);
                    }
                    catch
                    {
                        return;
                    }
                    int frameX = host1.Frame.X;
                    int frameY = host1.Frame.Y;
                    int frameWidth = host1.Frame.Width;
                    int frameHeight = host1.Frame.Height;
                    int x = point.X;
                    int y = point.Y;
                    if (me.View.GetType().ToString().Contains("+ContentView"))
                    {
                        if (meX >= (frameX + 1))
                        {
                            if (meY >= (frameY + 1))
                            {
                                if (meX < (frameX + frameWidth + 1))
                                {
                                    if (meY > (frameY + frameHeight + 1))
                                    {
                                        myme.X = x;
                                        myme.Y = y;
                                        if (Utils.lastMeX != x || Utils.lastMeY != y)
                                        {
                                            host1.OnMouseEnter(myme);
                                            Utils.lastMeX = x;
                                            Utils.lastMeY = y;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (me.View.GetType().ToString() == "Terminal.Gui.Window")
                    {
                        // Ничего не делаем.
                    }
                    else
                    {
                        if (meX >= frameX)
                        {
                            if (meY >= frameY)
                            {
                                if (meX < (frameX + frameWidth))
                                {
                                    if (meY < (frameY + frameHeight))
                                    {
                                        myme.X = x;
                                        myme.Y = y;
                                        if (Utils.lastMeX != x || Utils.lastMeY != y)
                                        {
                                            host1.OnMouseEnter(myme);
                                            Utils.lastMeX = x;
                                            Utils.lastMeY = y;
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
            }
        }

        private void M_View_Removed(Terminal.Gui.View obj)
        {
            dynamic Sender = Utils.RevertEqualsObj(M_View).dll_obj;
            TfAction action;
            try
            {
                action = Sender.Removed;
            }
            catch
            {
                return;
            }
            if (action != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = Sender;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(action);
                TfEventArgs1.view = Utils.RevertEqualsObj(obj).dll_obj;
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(action);
            }
        }

        private void M_View_MouseLeave(Terminal.Gui.View.MouseEventArgs obj)
        {
            dynamic Sender = Utils.RevertEqualsObj(M_View).dll_obj;
            if (Sender.GetType() == typeof(TfWindow))
            {
                return;
            }
            if (Sender.GetType() == typeof(TfMenuBar))
            {
                return;
            }
            if (Sender.GetType() == typeof(TfListView))
            {
                return;
            }
            TfAction action;
            try
            {
                action = Sender.MouseLeave;
            }
            catch
            {
                return;
            }
            if (action != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = Sender;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(action);
                TfEventArgs1.flags = ValueFactory.Create((int)obj.MouseEvent.Flags);
                TfEventArgs1.view = Utils.RevertEqualsObj(obj.MouseEvent.View).dll_obj;
                TfEventArgs1.x = ValueFactory.Create(obj.MouseEvent.X);
                TfEventArgs1.y = ValueFactory.Create(obj.MouseEvent.Y);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(action);
            }
        }

        private void M_View_MouseEnter(Terminal.Gui.View.MouseEventArgs obj)
        {
            dynamic Sender = Utils.RevertEqualsObj(M_View).dll_obj;
            if (Sender.GetType() == typeof(TfWindow))
            {
                return;
            }
            if (Sender.GetType() == typeof(TfListView))
            {
                return;
            }
            TfAction action;
            try
            {
                action = Sender.MouseEnter;
            }
            catch
            {
                return;
            }
            if (action != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = Sender;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(action);
                TfEventArgs1.flags = ValueFactory.Create((int)obj.MouseEvent.Flags);
                TfEventArgs1.view = Utils.RevertEqualsObj(obj.MouseEvent.View).dll_obj;
                TfEventArgs1.x = ValueFactory.Create(obj.MouseEvent.X);
                TfEventArgs1.y = ValueFactory.Create(obj.MouseEvent.Y);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(action);
            }
        }

        private void M_View_MouseClick(Terminal.Gui.View.MouseEventArgs obj)
        {
            dynamic Sender = Utils.RevertEqualsObj(M_View).dll_obj;
            if (Sender.GetType() == typeof(TfWindow))
            {
                return;
            }
            if (Sender.GetType() == typeof(TfListView))
            {
                return;
            }
            TfAction action;
            try
            {
                action = Sender.MouseClick;
            }
            catch
            {
                return;
            }
            if (action != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = Sender;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(action);
                TfEventArgs1.flags = ValueFactory.Create((int)obj.MouseEvent.Flags);
                if (obj.MouseEvent.View.GetType().ToString().Contains("+ContentView"))
                {
                    TfEventArgs1.view = Sender;
                }
                else
                {
                    TfEventArgs1.view = Utils.RevertEqualsObj(obj.MouseEvent.View).dll_obj;
                }
                TfEventArgs1.x = ValueFactory.Create(obj.MouseEvent.X);
                TfEventArgs1.y = ValueFactory.Create(obj.MouseEvent.Y);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(action);
            }
        }

        private void M_View_Leave(Terminal.Gui.View.FocusEventArgs obj)
        {
            dynamic Sender = Utils.RevertEqualsObj(M_View).dll_obj;
            if (Sender.GetType() == typeof(TfWindow))
            {
                return;
            }
            if (Sender.GetType() == typeof(TfListView))
            {
                return;
            }
            TfAction action;
            try
            {
                action = Sender.Leave;
            }
            catch
            {
                return;
            }
            if (action != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = Sender;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(action);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(action);
            }
        }

        private void M_View_KeyUp(Terminal.Gui.View.KeyEventEventArgs obj)
        {
            dynamic Sender = Utils.RevertEqualsObj(M_View).dll_obj;
            TfAction action;
            try
            {
                action = Sender.KeyUp;
            }
            catch
            {
                return;
            }
            if (action != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = Sender;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(action);
                TfEventArgs1.isAlt = ValueFactory.Create(obj.KeyEvent.IsAlt);
                TfEventArgs1.isCapslock = ValueFactory.Create(obj.KeyEvent.IsCapslock);
                TfEventArgs1.isCtrl = ValueFactory.Create(obj.KeyEvent.IsCtrl);
                TfEventArgs1.isNumlock = ValueFactory.Create(obj.KeyEvent.IsNumlock);
                TfEventArgs1.isScrolllock = ValueFactory.Create(obj.KeyEvent.IsScrolllock);
                TfEventArgs1.isShift = ValueFactory.Create(obj.KeyEvent.IsShift);
                TfEventArgs1.keyValue = ValueFactory.Create(obj.KeyEvent.KeyValue);
                TfEventArgs1.keyToString = ValueFactory.Create(obj.KeyEvent.Key.ToString());
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(action);
            }
        }

        private void M_View_KeyPress(Terminal.Gui.View.KeyEventEventArgs obj)
        {
            // Обработаем клавишу выхода для приложения.
            if (OneScriptTerminalGui.instance.QuitKey == Utils.ToDecimal(obj.KeyEvent.KeyValue))
            {
                Application.RequestStop(OneScriptTerminalGui.instance.Top.Base_obj.M_Toplevel);
                return;
            }

            dynamic Sender = Utils.RevertEqualsObj(M_View).dll_obj;
            if (Sender.GetType() == typeof(TfListView))
            {
                return;
            }
            // Обработаем клавиши вызова для панели меню.
            if (Sender.GetType() == typeof(TfToplevel))
            {
                TfMenuBar TfMenuBar1 = null;
                try
                {
                    TfMenuBar1 = ((TfToplevel)Sender).MenuBar;
                }
                catch { }
                if (TfMenuBar1 != null)
                {
                    if (((TfToplevel)Sender).MenuBar.IsMenuOpen)
                    {
                        TfMenuBar menuBar = ((TfToplevel)Sender).MenuBar;
                        Terminal.Gui.MenuBar m_menuBar = menuBar.Base_obj.M_MenuBar;
                        for (int i = 0; i < menuBar.Menus.Count; i++)
                        {
                            TfMenuBarItem TfMenuBarItem1 = menuBar.Menus.Get(i);
                            string hotKey = TfMenuBarItem1.HotKey.ToLower();
                            System.Char char1 = Convert.ToChar(hotKey.Substring(0, 1));
                            int num = Utils.ToInt32(char1);
                            if (num == (int)obj.KeyEvent.Key)
                            {
                                m_menuBar.CloseMenu(true);
                                m_menuBar.OpenIndex = i;
                                m_menuBar.OpenMenu();
                                m_menuBar.OpenIndex = 0;
                                TfMenuBarItem1.Base_obj.M_MenuBarItem.Action.Invoke();
                                break;
                            }
                        }
                    }
                }
            }

            // Здесь мы  в хэш таблице ищем сочетание клавиш и соответствующий объект.
            ArrayList ShortcutDictionaryValue = null;
            try
            {
                ShortcutDictionaryValue = Utils.RevertShortcut(Utils.ToDecimal((int)obj.KeyEvent.Key));
            }
            catch { }
            if (ShortcutDictionaryValue != null)
            {
                for (int i = 0; i < ShortcutDictionaryValue.Count; i++)
                {
                    dynamic shortcutObj = ShortcutDictionaryValue[i];
                    // Если shortcutObj это пункт меню
                    if (shortcutObj.GetType() == typeof(TfMenuBarItem))
                    {
                        Terminal.Gui.MenuBar menuBar = ((TfMenuBarItem)shortcutObj).M_MenuBar;
                        if (menuBar.Visible && menuBar.Enabled)
                        {
                            // Предотвратим повторы события, если клавиша не отпущена.
                            long nowEventTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
                            if ((nowEventTime - Utils.lastEventTime) > 190)
                            {
                                if (shortcutObj.ShortcutAction != null)
                                {
                                    TfEventArgs TfEventArgs1 = new TfEventArgs();
                                    TfEventArgs1.sender = shortcutObj;
                                    TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(shortcutObj.ShortcutAction);
                                    OneScriptTerminalGui.Event = TfEventArgs1;
                                    OneScriptTerminalGui.ExecuteEvent(shortcutObj.ShortcutAction);
                                }
                            }
                            else { }
                            Utils.lastEventTime = nowEventTime;
                        }
                    }
                    else if (shortcutObj.GetType() == typeof(TfStatusItem))
                    {
                        Terminal.Gui.StatusBar statusBar = ((TfStatusItem)shortcutObj).M_StatusBar;
                        if (statusBar.Visible && statusBar.Enabled)
                        {
                            // Предотвратим повторы события, если клавиша не отпущена.
                            long nowEventTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
                            if ((nowEventTime - Utils.lastEventTime) > 190)
                            {
                                if (shortcutObj.ShortcutAction != null)
                                {
                                    TfEventArgs TfEventArgs1 = new TfEventArgs();
                                    TfEventArgs1.sender = shortcutObj;
                                    TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(shortcutObj.ShortcutAction);
                                    OneScriptTerminalGui.Event = TfEventArgs1;
                                    OneScriptTerminalGui.ExecuteEvent(shortcutObj.ShortcutAction);
                                }
                            }
                            else { }
                            Utils.lastEventTime = nowEventTime;
                        }
                    }
                    else if (shortcutObj.GetType() == typeof(TfContextMenu))
                    {
                        // Предотвратим повторы события, если клавиша не отпущена.
                        long nowEventTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
                        if ((nowEventTime - Utils.lastEventTime) > 400)
                        {
                            if (shortcutObj.ShortcutAction != null)
                            {
                                TfContextMenu contextMenu = (TfContextMenu)shortcutObj;
                                TfAction act = contextMenu.ShortcutAction;
                                OneScriptTerminalGui.instance.TimerStartAndStop(act.Script, act.MethodName, 100);
                            }
                        }
                        else { }
                        Utils.lastEventTime = nowEventTime;
                    }
                    else
                    {
                        // Предотвратим повторы события, если клавиша не отпущена.
                        long nowEventTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
                        if ((nowEventTime - Utils.lastEventTime) > 400)
                        {
                            if (shortcutObj.ShortcutAction != null)
                            {
                                TfEventArgs TfEventArgs1 = new TfEventArgs();
                                TfEventArgs1.sender = shortcutObj;
                                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(shortcutObj.ShortcutAction);
                                OneScriptTerminalGui.Event = TfEventArgs1;
                                OneScriptTerminalGui.ExecuteEvent(shortcutObj.ShortcutAction);
                            }
                        }
                        else { }
                        Utils.lastEventTime = nowEventTime;
                    }
                }
            }
            TfAction action;
            try
            {
                action = Sender.KeyPress;
            }
            catch
            {
                return;
            }
            if (action != null)
            {
                if (Sender.HasFocus)
                {
                    TfEventArgs TfEventArgs1 = new TfEventArgs();
                    TfEventArgs1.sender = Sender;
                    TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(action);
                    TfEventArgs1.isAlt = ValueFactory.Create(obj.KeyEvent.IsAlt);
                    TfEventArgs1.isCapslock = ValueFactory.Create(obj.KeyEvent.IsCapslock);
                    TfEventArgs1.isCtrl = ValueFactory.Create(obj.KeyEvent.IsCtrl);
                    TfEventArgs1.isNumlock = ValueFactory.Create(obj.KeyEvent.IsNumlock);
                    TfEventArgs1.isScrolllock = ValueFactory.Create(obj.KeyEvent.IsScrolllock);
                    TfEventArgs1.isShift = ValueFactory.Create(obj.KeyEvent.IsShift);
                    TfEventArgs1.keyValue = ValueFactory.Create(obj.KeyEvent.KeyValue);
                    TfEventArgs1.keyToString = ValueFactory.Create(obj.KeyEvent.Key.ToString());
                    OneScriptTerminalGui.Event = TfEventArgs1;
                    OneScriptTerminalGui.ExecuteEvent(action);
                }
            }
        }

        private void M_View_KeyDown(Terminal.Gui.View.KeyEventEventArgs obj)
        {
            dynamic Sender = Utils.RevertEqualsObj(M_View).dll_obj;
            TfAction action;
            try
            {
                action = Sender.KeyDown;
            }
            catch
            {
                return;
            }
            if (action != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = Sender;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(action);
                TfEventArgs1.isAlt = ValueFactory.Create(obj.KeyEvent.IsAlt);
                TfEventArgs1.isCapslock = ValueFactory.Create(obj.KeyEvent.IsCapslock);
                TfEventArgs1.isCtrl = ValueFactory.Create(obj.KeyEvent.IsCtrl);
                TfEventArgs1.isNumlock = ValueFactory.Create(obj.KeyEvent.IsNumlock);
                TfEventArgs1.isScrolllock = ValueFactory.Create(obj.KeyEvent.IsScrolllock);
                TfEventArgs1.isShift = ValueFactory.Create(obj.KeyEvent.IsShift);
                TfEventArgs1.keyValue = ValueFactory.Create(obj.KeyEvent.KeyValue);
                TfEventArgs1.keyToString = ValueFactory.Create(obj.KeyEvent.Key.ToString());
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(action);
            }
        }

        private void M_View_Initialized(object sender, System.EventArgs e)
        {
            dynamic Sender = Utils.RevertEqualsObj(M_View).dll_obj;
            TfAction action;
            try
            {
                action = Sender.InitializedItem;
            }
            catch
            {
                return;
            }
            if (action != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = Sender;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(action);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(action);
            }
        }

        private void M_View_HotKeyChanged(Terminal.Gui.Key obj)
        {
            dynamic Sender = Utils.RevertEqualsObj(M_View).dll_obj;
            TfAction action;
            try
            {
                action = Sender.HotKeyChanged;
            }
            catch
            {
                return;
            }
            if (action != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = Sender;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(action);
                TfEventArgs1.keyValue = ValueFactory.Create((int)obj);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(action);
            }
        }

        private void M_View_Enter(Terminal.Gui.View.FocusEventArgs obj)
        {
            dynamic Sender = Utils.RevertEqualsObj(M_View).dll_obj;
            if (Sender.GetType() == typeof(TfListView))
            {
                return;
            }
            TfAction action;
            try
            {
                action = Sender.Enter;
            }
            catch
            {
                return;
            }
            if (action != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = Sender;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(action);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(action);
            }
        }

        private void M_View_EnabledChanged()
        {
            dynamic Sender = Utils.RevertEqualsObj(M_View).dll_obj;
            TfAction action;
            try
            {
                action = Sender.EnabledChanged;
            }
            catch
            {
                return;
            }
            if (action != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = Sender;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(action);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(action);
            }
        }

        private void M_View_CanFocusChanged()
        {
            dynamic Sender = Utils.RevertEqualsObj(M_View).dll_obj;
            TfAction action;
            try
            {
                action = Sender.CanFocusChanged;
            }
            catch
            {
                return;
            }
            if (action != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = Sender;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(action);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(action);
            }
        }

        private void M_View_Added(Terminal.Gui.View obj)
        {
            dynamic Sender = Utils.RevertEqualsObj(M_View).dll_obj;
            TfAction action;
            try
            {
                action = Sender.Added;
            }
            catch
            {
                return;
            }
            if (action != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = Sender;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(action);
                TfEventArgs1.view = Utils.RevertEqualsObj(obj).dll_obj;
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(action);
            }
        }

        public IValue Tag
        {
            get { return M_View.Tag; }
            set { M_View.Tag = value; }
        }

        public ostgui.Pos X
        {
            get { return new Pos(Terminal.Gui.Pos.X(M_View)); }
            set
            {
                if (M_View.GetType() != typeof(Terminal.Gui.Window))
                {
                    if (value.M_Pos.ToString().Contains("Pos.Absolute"))
                    {
                        M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Absolute;
                    }
                }
                M_View.X = value.M_Pos;
            }
        }

        public ostgui.Pos Y
        {
            get { return new Pos(Terminal.Gui.Pos.Y(M_View)); }
            set { M_View.Y = value.M_Pos; }
        }

        public string Text
        {
            get { return M_View.Text.ToString(); }
            set { M_View.Text = value; }
        }

        public ostgui.Dim Width
        {
            get { return Utils.RevertEqualsObj(M_View.Width); }
            set { M_View.Width = value.M_Dim; }
        }

        public ostgui.Dim Height
        {
            get { return Utils.RevertEqualsObj(M_View.Height); }
            set { M_View.Height = value.M_Dim; }
        }

        public ostgui.Pos Left
        {
            get { return new Pos(Terminal.Gui.Pos.Left(M_View)); }
        }

        public ostgui.Pos Right
        {
            get { return new Pos(Terminal.Gui.Pos.Right(M_View)); }
        }

        public ostgui.Pos Top
        {
            get { return new Pos(Terminal.Gui.Pos.Top(M_View)); }
        }

        public ostgui.Pos Bottom
        {
            get { return new Pos(Terminal.Gui.Pos.Bottom(M_View)); }
        }

        public int LayoutStyle
        {
            get { return (int)M_View.LayoutStyle; }
            set { M_View.LayoutStyle = (LayoutStyle)value; }
        }

        public bool AutoSize
        {
            get { return M_View.AutoSize; }
            set { M_View.AutoSize = value; }
        }

        public int VerticalTextAlignment
        {
            get { return (int)M_View.VerticalTextAlignment; }
            set { M_View.VerticalTextAlignment = (Terminal.Gui.VerticalTextAlignment)value; }
        }

        public int TextAlignment
        {
            get { return (int)M_View.TextAlignment; }
            set { M_View.TextAlignment = (TextAlignment)value; }
        }

        public int HotKey
        {
            get { return (int)M_View.HotKey; }
            set { M_View.HotKey = (Terminal.Gui.Key)value; }
        }

        public bool IsAdded
        {
            get { return M_View.IsAdded; }
        }

        public bool IgnoreBorderPropertyOnRedraw
        {
            get { return M_View.IgnoreBorderPropertyOnRedraw; }
            set { M_View.IgnoreBorderPropertyOnRedraw = value; }
        }

        public bool IsInitialized
        {
            get { return M_View.IsInitialized; }
            set { M_View.IsInitialized = value; }
        }

        public string Id
        {
            get { return M_View.Id.ToString(); }
            set { M_View.Id = value; }
        }

        public int TextDirection
        {
            get { return (int)M_View.TextDirection; }
            set { M_View.TextDirection = (Terminal.Gui.TextDirection)value; }
        }

        public bool ClearOnVisibleFalse
        {
            get { return M_View.ClearOnVisibleFalse; }
            set { M_View.ClearOnVisibleFalse = value; }
        }

        public bool WantContinuousButtonPressed
        {
            get { return M_View.WantContinuousButtonPressed; }
            set { M_View.WantContinuousButtonPressed = value; }
        }

        public bool WantMousePositionReports
        {
            get { return M_View.WantMousePositionReports; }
            set { M_View.WantMousePositionReports = value; }
        }

        public int TabIndex
        {
            get { return M_View.TabIndex; }
            set { M_View.TabIndex = value; }
        }

        public Rune HotKeySpecifier
        {
            get { return M_View.HotKeySpecifier; }
            set { M_View.HotKeySpecifier = value; }
        }

        public bool PreserveTrailingSpaces
        {
            get { return M_View.PreserveTrailingSpaces; }
            set { M_View.PreserveTrailingSpaces = value; }
        }

        public string ShortcutTag
        {
            get { return M_View.ShortcutTag.ToString(); }
        }

        public bool IsCurrentTop
        {
            get
            {
                bool isCurrentTop = false;
                Terminal.Gui.View parent = M_View.SuperView;
                int num = parent.Subviews.IndexOf(M_View);
                int count = parent.Subviews.Count;
                if (num == (count - 1))
                {
                    isCurrentTop = true;
                }
                return isCurrentTop;
            }
        }

        public bool TabStop
        {
            get { return M_View.TabStop; }
            set { M_View.TabStop = value; }
        }

        public IValue Focused
        {
            get
            {
                if (M_View.Focused != null)
                {
                    return Utils.RevertEqualsObj(M_View.Focused).dll_obj;
                }
                return null;
            }
        }

        public Border Border
        {
            get { return Utils.RevertEqualsObj(M_View.Border); }
            set { M_View.Border = value.M_Border; }
        }

        public Rect Bounds
        {
            get { return new Rect(M_View.Frame.X, M_View.Frame.Y, M_View.Bounds.Width, M_View.Bounds.Height); }
        }

        public object Data
        {
            get { return M_View.Data; }
            set { M_View.Data = value; }
        }

        public Rect Frame
        {
            get { return new Rect(M_View.Frame.X, M_View.Frame.Y, M_View.Frame.Width, M_View.Frame.Height); }
            set { M_View.Frame = value.M_Rect; }
        }

        public TextFormatter TextFormatter
        {
            get { return Utils.RevertEqualsObj(M_View.TextFormatter); }
            set { M_View.TextFormatter = value.M_TextFormatter; }
        }

        public View SuperView
        {
            get { return Utils.RevertEqualsObj(M_View.SuperView); }
        }

        public ColorScheme ColorScheme
        {
            get { return Utils.RevertEqualsObj(M_View.ColorScheme); }
            set { M_View.ColorScheme = value.M_ColorScheme; }
        }

        public Attribute GetFocusColor()
        {
            return new Attribute(M_View.GetFocusColor());
        }

        public Attribute GetNormalColor()
        {
            return new Attribute(M_View.GetNormalColor());
        }

        public void SetFocus()
        {
            M_View.SetFocus();
        }

        public void RemoveAll()
        {
            M_View.RemoveAll();
        }

        public void Remove(View p1)
        {
            M_View.Remove(p1.M_View);
        }

        public Point ScreenToView(int p1, int p2)
        {
            return new Point(M_View.ScreenToView(p1, p2));
        }

        public void LayoutSubviews()
        {
            M_View.LayoutSubviews();
        }

        public Size GetAutoSize()
        {
            return new Size(M_View.GetAutoSize());
        }

        public void SetChildNeedsDisplay()
        {
            M_View.SetChildNeedsDisplay();
        }

        public void Redraw(Rect p1)
        {
            M_View.Redraw(p1.M_Rect);
        }

        public void Clear()
        {
            M_View.Clear();
        }

        public void SendSubviewBackwards(View p1)
        {
            M_View.SendSubviewBackwards(p1.M_View);
        }

        public void SendSubviewToBack(View p1)
        {
            M_View.SendSubviewToBack(p1.M_View);
        }

        public void SetNeedsDisplay(Rect p1 = null)
        {
            M_View.SetNeedsDisplay(p1.M_Rect);
        }

        public void BringSubviewToFront(View p1)
        {
            M_View.BringSubviewToFront(p1.M_View);
        }

        public void Add(View p1)
        {
            M_View.Add(p1.M_View);
        }

        public void BringSubviewForward(View p1)
        {
            M_View.BringSubviewForward(p1.M_View);
        }

        public void CorrectionZet()
        {
            // Необходимая коррекция z-порядка элементов при запуске приложения.
            // Найдено экспериментальным путем.
            Terminal.Gui.View[] array1 = new Terminal.Gui.View[M_View.Subviews.Count];
            for (int i = 0; i < M_View.Subviews.Count; i++)
            {
                Terminal.Gui.View view1 = M_View.Subviews[i];
                array1[i] = view1;
            }
            M_View.RemoveAll();
            //M_View.Add(array1[3]);
            //M_View.Add(array1[0]);
            //M_View.Add(array1[1]);
            //M_View.Add(array1[2]);
            M_View.Add(array1[array1.Length - 1]);
            for (int i = 0; i < array1.Length - 1; i++)
            {
                M_View.Add(array1[i]);
            }
        }

        public View GetTopSuperView()
        {
            return Utils.RevertEqualsObj(M_View.GetTopSuperView());
        }

        public System.Action ShortcutAction
        {
            get { return M_View.ShortcutAction; }
            set { M_View.ShortcutAction = value; }
        }

        public ostgui.Attribute GetHotNormalColor()
        {
            return new Attribute(M_View.GetHotNormalColor());
        }

        public void PlaceTop(View p1, int p2)
        {
            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
            M_View.Y = Terminal.Gui.Pos.Top(p1.M_View) - M_View.Frame.Height - p2 - 1;
        }

        public void PlaceLeft(View p1, int p2)
        {
            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
            M_View.X = Terminal.Gui.Pos.Left(p1.M_View) - M_View.Frame.Width - p2 - 1;
        }

        public void PlaceBottom(View p1, int p2)
        {
            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
            M_View.Y = Terminal.Gui.Pos.Bottom(p1.M_View) + p2 + 1;
        }

        public void PlaceRight(View p1, int p2)
        {
            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
            M_View.X = Terminal.Gui.Pos.Right(p1.M_View) + p2 + 1;
        }

        public void PlaceTopLeft(View p1, int p2, int p3)
        {
            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
            M_View.Y = Terminal.Gui.Pos.Top(p1.M_View) - M_View.Frame.Height - p2 - 1;

            M_View.X = Terminal.Gui.Pos.Left(p1.M_View) - M_View.Frame.Width - p3 - 1;
        }

        public void PlaceTopRight(View p1, int p2, int p3)
        {
            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
            M_View.Y = Terminal.Gui.Pos.Top(p1.M_View) - M_View.Frame.Height - p2 - 1;

            M_View.X = Terminal.Gui.Pos.Right(p1.M_View) + p3 + 1;
        }

        public void PlaceBottomLeft(View p1, int p2, int p3)
        {
            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
            M_View.Y = Terminal.Gui.Pos.Bottom(p1.M_View) + p2 + 1;

            M_View.X = Terminal.Gui.Pos.Left(p1.M_View) - M_View.Frame.Width - p3 - 1;
        }

        public void PlaceBottomRight(View p1, int p2, int p3)
        {
            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
            M_View.Y = Terminal.Gui.Pos.Bottom(p1.M_View) + p2 + 1;

            M_View.X = Terminal.Gui.Pos.Right(p1.M_View) + p3 + 1;
        }

        public void PlaceLeftTop(View p1, int p2, int p3)
        {
            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
            M_View.X = Terminal.Gui.Pos.Left(p1.M_View) - M_View.Frame.Width - p2 - 1;

            M_View.Y = Terminal.Gui.Pos.Top(p1.M_View) - M_View.Frame.Height - p3 - 1;
        }

        public void PlaceLeftBottom(View p1, int p2, int p3)
        {
            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
            M_View.X = Terminal.Gui.Pos.Left(p1.M_View) - M_View.Frame.Width - p2 - 1;

            M_View.Y = Terminal.Gui.Pos.Bottom(p1.M_View) + p3 + 1;
        }

        public void PlaceRightTop(View p1, int p2, int p3)
        {
            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
            M_View.X = Terminal.Gui.Pos.Right(p1.M_View) + p2 + 1;

            M_View.Y = Terminal.Gui.Pos.Top(p1.M_View) - M_View.Frame.Height - p3 - 1;
        }

        public void PlaceRightBottom(View p1, int p2, int p3)
        {
            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
            M_View.X = Terminal.Gui.Pos.Right(p1.M_View) + p2 + 1;

            M_View.Y = Terminal.Gui.Pos.Bottom(p1.M_View) + p3 + 1;
        }

        public new string ToString()
        {
            return M_View.ToString();
        }

        public SubviewCollection Subviews
        {
            get { return new SubviewCollection(M_View.Subviews); }
        }

        public void Center(int p1 = 0, int p2 = 0)
        {
            if (p1 != 0)
            {
                M_View.X = Terminal.Gui.Pos.Center() + p1 - (M_View.Frame.Width / 2) - 1;
            }
            else
            {
                M_View.X = Terminal.Gui.Pos.Center();
            }
            if (p2 != 0)
            {
                M_View.Y = Terminal.Gui.Pos.Center() + p2 - (M_View.Frame.Height / 2) - 1;
            }
            else
            {
                M_View.Y = Terminal.Gui.Pos.Center();
            }
        }

        public void Fill(int p1 = 0, int p2 = 0)
        {
            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
            M_View.Width = Terminal.Gui.Dim.Fill(p1);
            M_View.Height = Terminal.Gui.Dim.Fill(p2);
        }
    }

    [ContextClass("ТфЭлемент", "TfView")]
    public class TfView : AutoContext<TfView>
    {

        public TfView()
        {
            View View1 = new View();
            View1.dll_obj = this;
            Base_obj = View1;
        }

        public TfView(TfRect p1)
        {
            View View1 = new View(p1.Base_obj.M_Rect);
            View1.dll_obj = this;
            Base_obj = View1;
        }

        public TfView(int p1, int p2, string p3)
        {
            View View1 = new View(p1, p2, p3);
            View1.dll_obj = this;
            Base_obj = View1;
        }

        public TfView(TfRect p1, string p2, TfBorder p3)
        {
            View View1 = new View(p1.Base_obj.M_Rect, p2, p3.Base_obj.M_Border);
            View1.dll_obj = this;
            Base_obj = View1;
        }

        public TfView(View p1)
        {
            View View1 = p1;
            View1.dll_obj = this;
            Base_obj = View1;
        }

        public TfView(string p1, int p2, TfBorder p3)
        {
            View View1 = new View(p1, p2, p3.Base_obj.M_Border);
            View1.dll_obj = this;
            Base_obj = View1;
        }

        public void CorrectionZet()
        {
            Base_obj.CorrectionZet();
        }

        public View Base_obj;

        [ContextProperty("АвтоРазмер", "AutoSize")]
        public bool AutoSize
        {
            get { return Base_obj.AutoSize; }
            set { Base_obj.AutoSize = value; }
        }

        [ContextProperty("ВертикальноеВыравниваниеТекста", "VerticalTextAlignment")]
        public int VerticalTextAlignment
        {
            get { return Base_obj.VerticalTextAlignment; }
            set { Base_obj.VerticalTextAlignment = value; }
        }

        [ContextProperty("Верх", "Top")]
        public TfPos Top
        {
            get { return new TfPos(Base_obj.Top); }
        }

        [ContextProperty("ВФокусе", "Focused")]
        public IValue Focused
        {
            get
            {
                if (Base_obj.M_View.Focused.GetType().ToString().Contains("+ContentView"))
                {
                    return Utils.RevertEqualsObj(Base_obj.M_View.Focused.Focused).dll_obj;
                }
                return Utils.RevertEqualsObj(Base_obj.M_View.Focused).dll_obj;
            }
        }

        [ContextProperty("ВыравниваниеТекста", "TextAlignment")]
        public int TextAlignment
        {
            get { return Base_obj.TextAlignment; }
            set { Base_obj.TextAlignment = value; }
        }

        [ContextProperty("Высота", "Height")]
        public IValue Height
        {
            get { return new TfDim().Height(this); }
            set
            {
                if (Utils.IsType<TfDim>(value))
                {
                    Base_obj.M_View.Height = ((TfDim)value).Base_obj.M_Dim;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_View.Height = Terminal.Gui.Dim.Sized(Utils.ToInt32(value));
                }
            }
        }

        [ContextProperty("Граница", "Border")]
        public TfBorder Border
        {
            get { return Base_obj.Border.dll_obj; }
            set
            {
                TfBorder border = new TfBorder();
                Terminal.Gui.Border _border = value.Base_obj.M_Border;
                border.Base_obj.M_Border.Background = _border.Background;
                border.Base_obj.M_Border.BorderBrush = _border.BorderBrush;
                border.Base_obj.M_Border.BorderStyle = _border.BorderStyle;
                border.Base_obj.M_Border.BorderThickness = _border.BorderThickness;
                border.Base_obj.M_Border.Effect3D = _border.Effect3D;
                border.Base_obj.M_Border.Effect3DBrush = _border.Effect3DBrush;
                border.Base_obj.M_Border.Effect3DOffset = _border.Effect3DOffset;
                border.Base_obj.M_Border.Padding = _border.Padding;
                border.Base_obj.M_Border.Title = _border.Title;
                Base_obj.Border = border.Base_obj;
            }
        }

        [ContextProperty("Границы", "Bounds")]
        public TfRect Bounds
        {
            get
            {
                Terminal.Gui.Rect bounds = Base_obj.Bounds.M_Rect;
                return new TfRect(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            }
        }

        [ContextProperty("Данные", "Data")]
        public IValue Data
        {
            get { return Utils.RevertObj(Base_obj.Data); }
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
        public IValue Y
        {
            get { return new TfPos(Base_obj.Y); }
            set
            {
                if (Utils.IsType<TfPos>(value))
                {
                    Base_obj.M_View.Y = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_View.Y = Terminal.Gui.Pos.At(Utils.ToInt32(value));
                }
            }
        }

        [ContextProperty("Идентификатор", "Id")]
        public string Id
        {
            get { return Base_obj.Id; }
            set { Base_obj.Id = value; }
        }

        [ContextProperty("Икс", "X")]
        public IValue X
        {
            get { return new TfPos(Base_obj.X); }
            set
            {
                if (Utils.IsType<TfPos>(value))
                {
                    Base_obj.M_View.X = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_View.X = Terminal.Gui.Pos.At(Utils.ToInt32(value));
                }
            }
        }

        [ContextProperty("Кадр", "Frame")]
        public TfRect Frame
        {
            get
            {
                Terminal.Gui.Rect frame = Base_obj.Frame.M_Rect;
                return new TfRect(frame.X, frame.Y, frame.Width, frame.Height);
            }
            set { Base_obj.Frame = value.Base_obj; }
        }

        [ContextProperty("КлавишаВызова", "HotKey")]
        public int HotKey
        {
            get { return Base_obj.HotKey; }
        }

        [ContextProperty("Лево", "Left")]
        public TfPos Left
        {
            get { return new TfPos(Base_obj.Left); }
        }

        [ContextProperty("Метка", "Tag")]
        public IValue Tag
        {
            get { return Base_obj.Tag; }
            set { Base_obj.Tag = value; }
        }

        [ContextProperty("НаправлениеТекста", "TextDirection")]
        public int TextDirection
        {
            get { return Base_obj.TextDirection; }
            set { Base_obj.TextDirection = value; }
        }

        [ContextProperty("Низ", "Bottom")]
        public TfPos Bottom
        {
            get { return new TfPos(Base_obj.Bottom); }
        }

        private bool disableHotKey = false;
        [ContextProperty("ОтключитьКлавишуВызова", "DisableHotKey")]
        public bool DisableHotKey
        {
            get { return disableHotKey; }
            set
            {
                if (value)
                {
                    Base_obj.HotKeySpecifier = (Rune)0xFFFF;
                    disableHotKey = true;
                }
                else
                {
                    Base_obj.HotKeySpecifier = new Rune("_".ToCharArray()[0]);
                    disableHotKey = false;
                }
            }
        }

        [ContextProperty("Отображать", "Visible")]
        public bool Visible
        {
            get { return Base_obj.Visible; }
            set { Base_obj.Visible = value; }
        }

        [ContextProperty("ОформительТекста", "TextFormatter")]
        public TfTextFormatter TextFormatter
        {
            get { return Base_obj.TextFormatter.dll_obj; }
            set { Base_obj.TextFormatter = value.Base_obj; }
        }

        [ContextProperty("Подэлементы", "Subviews")]
        public TfSubviewCollection Subviews
        {
            get { return new TfSubviewCollection(Base_obj.M_View.Subviews); }
        }

        [ContextProperty("ПорядокОбхода", "TabIndex")]
        public int TabIndex
        {
            get { return Base_obj.TabIndex; }
            set { Base_obj.TabIndex = value; }
        }

        [ContextProperty("Право", "Right")]
        public TfPos Right
        {
            get { return new TfPos(Base_obj.Right); }
        }

        [ContextProperty("Родитель", "SuperView")]
        public IValue SuperView
        {
            get
            {
                if (Base_obj.M_View.SuperView.GetType().ToString().Contains("+ContentView"))
                {
                    return Utils.RevertEqualsObj(Base_obj.M_View.SuperView.SuperView).dll_obj;
                }
                return Utils.RevertEqualsObj(Base_obj.M_View.SuperView).dll_obj;
            }
        }

        [ContextProperty("СтильКомпоновки", "LayoutStyle")]
        public int LayoutStyle
        {
            get { return Base_obj.LayoutStyle; }
            set { Base_obj.LayoutStyle = value; }
        }

        [ContextProperty("Сфокусирован", "HasFocus")]
        public bool HasFocus
        {
            get { return Base_obj.HasFocus; }
        }

        [ContextProperty("ТабФокус", "TabStop")]
        public bool TabStop
        {
            get { return Base_obj.TabStop; }
            set { Base_obj.TabStop = value; }
        }

        [ContextProperty("Текст", "Text")]
        public string Text
        {
            get { return Base_obj.Text; }
            set { Base_obj.Text = value; }
        }

        [ContextProperty("ТекущийСверху", "IsCurrentTop")]
        public bool IsCurrentTop
        {
            get { return Base_obj.IsCurrentTop; }
        }

        [ContextProperty("Фокусируемый", "CanFocus")]
        public bool CanFocus
        {
            get { return Base_obj.CanFocus; }
            set { Base_obj.CanFocus = value; }
        }

        private TfColorScheme colorScheme;
        [ContextProperty("ЦветоваяСхема", "ColorScheme")]
        public TfColorScheme ColorScheme
        {
            get { return colorScheme; }
            set
            {
                colorScheme = new TfColorScheme();
                Terminal.Gui.ColorScheme _colorScheme = value.Base_obj.M_ColorScheme;
                colorScheme.Base_obj.M_ColorScheme.Disabled = _colorScheme.Disabled;
                colorScheme.Base_obj.M_ColorScheme.Focus = _colorScheme.Focus;
                colorScheme.Base_obj.M_ColorScheme.HotFocus = _colorScheme.HotFocus;
                colorScheme.Base_obj.M_ColorScheme.HotNormal = _colorScheme.HotNormal;
                colorScheme.Base_obj.M_ColorScheme.Normal = _colorScheme.Normal;
                Base_obj.ColorScheme = colorScheme.Base_obj;
            }
        }

        [ContextProperty("Ширина", "Width")]
        public IValue Width
        {
            get { return new TfDim().Width(this); }
            set
            {
                if (Utils.IsType<TfDim>(value))
                {
                    Base_obj.M_View.Width = ((TfDim)value).Base_obj.M_Dim;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_View.Width = Terminal.Gui.Dim.Sized(Utils.ToInt32(value));
                }
            }
        }

        [ContextProperty("КлавишаНажата", "KeyPress")]
        public TfAction KeyPress { get; set; }

        [ContextProperty("МышьНадЭлементом", "MouseEnter")]
        public TfAction MouseEnter { get; set; }

        [ContextProperty("МышьПокинулаЭлемент", "MouseLeave")]
        public TfAction MouseLeave { get; set; }

        [ContextProperty("ПриВходе", "Enter")]
        public TfAction Enter { get; set; }

        [ContextProperty("ПриНажатииМыши", "MouseClick")]
        public TfAction MouseClick { get; set; }

        [ContextProperty("ПриУходе", "Leave")]
        public TfAction Leave { get; set; }

        [ContextProperty("СочетаниеКлавишДействие", "ShortcutAction")]
        public TfAction ShortcutAction { get; set; }

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

        [ContextMethod("Выше", "PlaceTop")]
        public void PlaceTop(IValue p1, int p2)
        {
            Base_obj.PlaceTop(((dynamic)p1.AsObject()).Base_obj, p2);
        }

        [ContextMethod("ВышеЛевее", "PlaceTopLeft")]
        public void PlaceTopLeft(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceTopLeft(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("ВышеПравее", "PlaceTopRight")]
        public void PlaceTopRight(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceTopRight(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("Добавить", "Add")]
        public IValue Add(IValue p1)
        {
            Base_obj.Add(((dynamic)p1).Base_obj);
            return p1;
        }

        [ContextMethod("ДобавитьСочетаниеКлавиш", "AddShortcut")]
        public void AddShortcut(decimal p1)
        {
            Utils.AddToShortcutDictionary(p1, this);
        }

        [ContextMethod("Заполнить", "Fill")]
        public void Fill(int p1 = 0, int p2 = 0)
        {
            Base_obj.Fill(p1, p2);
        }

        [ContextMethod("Левее", "PlaceLeft")]
        public void PlaceLeft(IValue p1, int p2)
        {
            Base_obj.PlaceLeft(((dynamic)p1.AsObject()).Base_obj, p2);
        }

        [ContextMethod("ЛевееВыше", "PlaceLeftTop")]
        public void PlaceLeftTop(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceLeftTop(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("ЛевееНиже", "PlaceLeftBottom")]
        public void PlaceLeftBottom(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceLeftBottom(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("НаЗаднийПлан", "SendSubviewToBack")]
        public void SendSubviewToBack(IValue p1)
        {
            Base_obj.SendSubviewToBack(((dynamic)p1).Base_obj);
        }

        [ContextMethod("НаПереднийПлан", "BringSubviewToFront")]
        public void BringSubviewToFront(IValue p1)
        {
            Base_obj.BringSubviewToFront(((dynamic)p1).Base_obj);
        }

        [ContextMethod("НаШагВперед", "BringSubviewForward")]
        public void BringSubviewForward(IValue p1)
        {
            Base_obj.BringSubviewForward(((dynamic)p1).Base_obj);
        }

        [ContextMethod("НаШагНазад", "SendSubviewBackwards")]
        public void SendSubviewBackwards(IValue p1)
        {
            Base_obj.SendSubviewBackwards(((dynamic)p1).Base_obj);
        }

        [ContextMethod("Ниже", "PlaceBottom")]
        public void PlaceBottom(IValue p1, int p2)
        {
            Base_obj.PlaceBottom(((dynamic)p1.AsObject()).Base_obj, p2);
        }

        [ContextMethod("НижеЛевее", "PlaceBottomLeft")]
        public void PlaceBottomLeft(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceBottomLeft(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("НижеПравее", "PlaceBottomRight")]
        public void PlaceBottomRight(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceBottomRight(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("ПолучитьАвтоРазмер", "GetAutoSize")]
        public TfSize GetAutoSize()
        {
            int offsetWidth = 0;
            int offsetHeight = 0;
            try
            {
                offsetWidth = Border.BorderThickness.Left + Border.BorderThickness.Right;
                offsetHeight = Border.BorderThickness.Top + Border.BorderThickness.Bottom;
            }
            catch { }
            int MaxWidthLine = Terminal.Gui.TextFormatter.MaxWidthLine(Text);
            int MaxLines = Terminal.Gui.TextFormatter.MaxLines(Text, MaxWidthLine);
            try
            {
                return new TfSize(MaxWidthLine + 2 + offsetWidth, MaxLines + 2 + offsetHeight);
            }
            catch
            {
                return null;
            }
        }

        [ContextMethod("ПолучитьСочетаниеКлавиш", "GetShortcut")]
        public ValueListImpl GetShortcut()
        {
            ValueListImpl ValueListImpl1 = new ValueListImpl();
            ArrayList ArrayList1 = Utils.GetFromShortcutDictionary(this);
            for (int i = 0; i < ArrayList1.Count; i++)
            {
                decimal shortcut = (decimal)ArrayList1[i];
                ValueListImpl1.Add(ValueFactory.Create(shortcut), OneScriptTerminalGui.instance.Keys.NameEn(shortcut));
            }
            if (ValueListImpl1.Count() > 0)
            {
                return ValueListImpl1;
            }
            return null;
        }

        [ContextMethod("Правее", "PlaceRight")]
        public void PlaceRight(IValue p1, int p2)
        {
            Base_obj.PlaceRight(((dynamic)p1.AsObject()).Base_obj, p2);
        }

        [ContextMethod("ПравееВыше", "PlaceRightTop")]
        public void PlaceRightTop(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceRightTop(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("ПравееНиже", "PlaceRightBottom")]
        public void PlaceRightBottom(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceRightBottom(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("ТочкаНаЭлементе", "ScreenToView")]
        public TfPoint ScreenToView(int p1, int p2)
        {
            return new TfPoint(Base_obj.ScreenToView(p1, p2));
        }

        [ContextMethod("Удалить", "Remove")]
        public void Remove(IValue p1)
        {
            Base_obj.Remove(((dynamic)p1).Base_obj);
        }

        [ContextMethod("УдалитьВсе", "RemoveAll")]
        public void RemoveAll()
        {
            Base_obj.RemoveAll();
        }

        [ContextMethod("УдалитьСочетаниеКлавиш", "RemoveShortcut")]
        public void RemoveShortcut(decimal p1)
        {
            Utils.RemoveFromShortcutDictionary(p1, this);
        }

        [ContextMethod("УстановитьАвтоРазмер", "SetAutoSize")]
        public void SetAutoSize()
        {
            TfSize TfSize1 = GetAutoSize();
            Width = new TfDim().Sized(TfSize1.Width);
            Height = new TfDim().Sized(TfSize1.Height);
        }

        [ContextMethod("УстановитьФокус", "SetFocus")]
        public void SetFocus()
        {
            Base_obj.SetFocus();
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

        [ContextMethod("Центр", "Center")]
        public void Center(int p1 = 0, int p2 = 0)
        {
            Base_obj.Center(p1, p2);
        }

    }
}
