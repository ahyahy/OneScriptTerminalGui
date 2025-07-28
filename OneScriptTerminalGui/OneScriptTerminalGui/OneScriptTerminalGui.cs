using System;
using System.IO;
using System.Collections;
using System.Text;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library;
using System.Collections.Generic;
using Terminal.Gui;
using System.Reflection;

namespace ostgui
{
    [ContextClass("ТерминалФормыДляОдноСкрипта", "OneScriptTerminalGui")]
    public class OneScriptTerminalGui : AutoContext<OneScriptTerminalGui>
    {
        public static TfToplevel top;
        public static System.Collections.Hashtable hashtable = new Hashtable();
        public static OneScriptTerminalGui instance;
        private static object syncRoot = new Object();
        public static TfEventArgs Event = null;
        public static bool handleEvents = true;
        public static Dictionary<decimal, ArrayList> shortcutDictionary = new Dictionary<decimal, ArrayList>();
        public static int lastMeX = -1;
        public static int lastMeY = -1;
        public static long lastEventTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;

        static byte[] StreamToBytes(Stream input)
        {
            var capacity = input.CanSeek ? (int)input.Length : 0;
            using (var output = new MemoryStream(capacity))
            {
                int readLength;
                var buffer = new byte[4096];
                do
                {
                    readLength = input.Read(buffer, 0, buffer.Length);
                    output.Write(buffer, 0, readLength);
                }
                while (readLength != 0);
                return output.ToArray();
            }
        }

        public static OneScriptTerminalGui getInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new OneScriptTerminalGui();
                    }
                }
            }
            return instance;
        }

        [ScriptConstructor]
        public static IRuntimeContextInstance Constructor()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string resourcepath = "ostgui." + new AssemblyName(args.Name).Name + ".dll";
                if (Assembly.GetExecutingAssembly().GetName().Name == "OneScriptTerminalGui" &&
                    resourcepath != "ostgui.Terminal.Gui.dll")
                {
                    var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcepath);
                    if (stream != null)
                    {
                        using (stream)
                        {
                            return Assembly.Load(StreamToBytes(stream));
                        }
                    }
                }
                return null;
            };

            OnOpen = delegate ()
            {
                if (instance.NotifyNewRunState != null)
                {
                    TfEventArgs TfEventArgs1 = new TfEventArgs();
                    TfEventArgs1.sender = instance;
                    TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(instance.NotifyNewRunState);
                    OneScriptTerminalGui.Event = TfEventArgs1;
                    OneScriptTerminalGui.ExecuteEvent(instance.NotifyNewRunState);
                }
            };

            OneScriptTerminalGui inst = getInstance();
            return inst;
        }

        static Action OnOpen;
        private static void Application_NotifyNewRunState(Application.RunState obj)
        {
            OnOpen.Invoke();
        }

        [ContextProperty("РазмерИзменен", "Resized")]
        public TfAction Resized { get; set; }

        public static SystemGlobalContext GlobalContext()
        {
            return GlobalsManager.GetGlobalContext<SystemGlobalContext>();
        }

        private decimal quitKey;
        [ContextProperty("КлавишаВыхода", "QuitKey")]
        public decimal QuitKey
        {
            get { return quitKey; }
            set { quitKey = value; }
        }

        private static TfConsoleKey tf_ConsoleKey = new TfConsoleKey();
        [ContextProperty("КлавишиКонсоли", "ConsoleKey")]
        public TfConsoleKey ConsoleKey
        {
            get { return tf_ConsoleKey; }
        }

        private static TfCommandTUI tf_CommandTUI = new TfCommandTUI();
        [ContextProperty("КомандаTUI", "CommandTUI")]
        public TfCommandTUI CommandTUI
        {
            get { return tf_CommandTUI; }
        }

        [ContextMethod("Эмодзи", "Emoji")]
        public string Emoji(IValue p1)
        {
            var sb = new StringBuilder();
            if (p1.SystemType.Name == "Число")
            {
                try
                {
                    sb.Append(Char.ConvertFromUtf32(Convert.ToInt32(p1.AsNumber()))).ToString();
                }
                catch { }
            }
            else if (p1.SystemType.Name == "Строка")
            {
                string p2 = p1.AsString();
                p2 = p2.Replace("0x", "").Replace("0х", "").Replace("\\u", "");
                try
                {
                    try
                    {
                        int num = Convert.ToInt32(p2);
                        string str = Char.ConvertFromUtf32(num);
                        sb.Append(str).ToString();
                    }
                    catch
                    {
                        int num = Convert.ToInt32(p2, 16);
                        string str = Char.ConvertFromUtf32(num);
                        sb.Append(str).ToString();
                    }
                }
                catch { }
            }
            return sb.ToString();
        }

        [ContextProperty("Высота", "Rows")]
        public int Rows
        {
            get { return Application.Driver.Rows; }
        }

        [ContextProperty("Ширина", "Cols")]
        public int Cols
        {
            get { return Application.Driver.Cols; }
        }

        [ContextMethod("КлавишаВвод", "ButtonEnter")]
        public void ButtonEnter()
        {
            Application.Driver.SendKeys(System.Char.MinValue, System.ConsoleKey.Enter, false, false, false);
        }

        [ContextMethod("ПраваяКвадратная", "RightBracket")]
        public string RightBracket()
        {
            return Application.Driver.RightBracket.ToString();
        }

        [ContextMethod("ЛеваяКвадратная", "LeftBracket")]
        public string LeftBracket()
        {
            return Application.Driver.LeftBracket.ToString();
        }

        [ContextMethod("МалыйБлок", "BlocksMeterSegment")]
        public string BlocksMeterSegment()
        {
            return Application.Driver.BlocksMeterSegment.ToString();
        }

        [ContextMethod("БольшойБлок", "ContinuousMeterSegment")]
        public string ContinuousMeterSegment()
        {
            return Application.Driver.ContinuousMeterSegment.ToString();
        }

        [ContextMethod("ЛевыйИндикатор", "LeftDefaultIndicator")]
        public string LeftDefaultIndicator()
        {
            return Application.Driver.LeftDefaultIndicator.ToString();
        }

        [ContextMethod("ПравыйИндикатор", "RightDefaultIndicator")]
        public string RightDefaultIndicator()
        {
            return Application.Driver.RightDefaultIndicator.ToString();
        }

        [ContextMethod("ВерхняяСтрелка", "ArrowUp")]
        public string ArrowUp()
        {
            return Application.Driver.UpArrow.ToString();
        }

        [ContextMethod("ЛеваяСтрелка", "ArrowLeft")]
        public string ArrowLeft()
        {
            return Application.Driver.LeftArrow.ToString();
        }

       [ContextMethod("НижняяСтрелка", "ArrowDown")]
        public string ArrowDown()
        {
            return Application.Driver.DownArrow.ToString();
        }

       [ContextMethod("ПраваяСтрелка", "ArrowRight")]
        public string ArrowRight()
        {
            return Application.Driver.RightArrow.ToString();
        }

       [ContextMethod("ВерхнийТройник", "TopTee")]
        public string TopTee()
        {
            return Application.Driver.TopTee.ToString();
        }

       [ContextMethod("ЛевыйТройник", "LeftTee")]
        public string LeftTee()
        {
            return Application.Driver.LeftTee.ToString();
        }

       [ContextMethod("НижнийТройник", "BottomTee")]
        public string BottomTee()
        {
            return Application.Driver.BottomTee.ToString();
        }

       [ContextMethod("ПравыйТройник", "RightTee")]
        public string RightTee()
        {
            return Application.Driver.RightTee.ToString();
        }

        [ContextMethod("Пометка", "Checked")]
        public string Checked()
        {
            return Application.Driver.Checked.ToString();
        }

        [ContextMethod("Алмаз", "Diamond")]
        public string Diamond()
        {
            return Application.Driver.Diamond.ToString();
        }

        [ContextMethod("ДвойнаяГоризонтальная", "HDLine")]
        public string HDLine()
        {
            return Application.Driver.HDLine.ToString();
        }

        [ContextMethod("Горизонтальная", "HLine")]
        public string HLine()
        {
            return Application.Driver.HLine.ToString();
        }

        [ContextMethod("ГоризонтальнаяСЗакругленнымиУглами", "HRLine")]
        public string HRLine()
        {
            return Application.Driver.HRLine.ToString();
        }

        [ContextMethod("НижнийЛевыйУгол", "LLCorner")]
        public string LLCorner()
        {
            return Application.Driver.LLCorner.ToString();
        }

        [ContextMethod("НижнийЛевыйДвойнойУгол", "LLDCorner")]
        public string LLDCorner()
        {
            return Application.Driver.LLDCorner.ToString();
        }

        [ContextMethod("НижнийЛевыйЗакругленныйУгол", "LLRCorner")]
        public string LLRCorner()
        {
            return Application.Driver.LLRCorner.ToString();
        }

        [ContextMethod("НижнийПравыйУгол", "LRCorner")]
        public string LRCorner()
        {
            return Application.Driver.LRCorner.ToString();
        }

        [ContextMethod("НижнийПравыйДвойнойУгол", "LRDCorner")]
        public string LRDCorner()
        {
            return Application.Driver.LRDCorner.ToString();
        }

        [ContextMethod("НижнийПравыйЗакругленныйУгол", "LRRCorner")]
        public string LRRCorner()
        {
            return Application.Driver.LRRCorner.ToString();
        }

        [ContextMethod("Выделенный", "Selected")]
        public string Selected()
        {
            return Application.Driver.Selected.ToString();
        }

        [ContextMethod("Точечный", "Stipple")]
        public string Stipple()
        {
            return Application.Driver.Stipple.ToString();
        }

        [ContextMethod("ВерхнийЛевыйУгол", "ULCorner")]
        public string ULCorner()
        {
            return Application.Driver.ULCorner.ToString();
        }

        [ContextMethod("ВерхнийЛевыйДвойнойУгол", "ULDCorner")]
        public string ULDCorner()
        {
            return Application.Driver.ULDCorner.ToString();
        }

        [ContextMethod("ВерхнийЛевыйЗакругленныйУгол", "ULRCorner")]
        public string ULRCorner()
        {
            return Application.Driver.ULRCorner.ToString();
        }

        [ContextMethod("БезПометки", "UnChecked")]
        public string UnChecked()
        {
            return Application.Driver.UnChecked.ToString();
        }

        [ContextMethod("БезВыделения", "UnSelected")]
        public string UnSelected()
        {
            return Application.Driver.UnSelected.ToString();
        }

        [ContextMethod("ВерхнийПравыйУгол", "URCorner")]
        public string URCorner()
        {
            return Application.Driver.URCorner.ToString();
        }

        [ContextMethod("ВерхнийПравыйДвойнойУгол", "URDCorner")]
        public string URDCorner()
        {
            return Application.Driver.URDCorner.ToString();
        }

        [ContextMethod("ВерхнийПравыйЗакругленныйУгол", "URRCorner")]
        public string URRCorner()
        {
            return Application.Driver.URRCorner.ToString();
        }

        [ContextMethod("ВертикальнаяДвойная", "VDLine")]
        public string VDLine()
        {
            return Application.Driver.VDLine.ToString();
        }

        [ContextMethod("Вертикальная", "VLine")]
        public string VLine()
        {
            return Application.Driver.VLine.ToString();
        }

        [ContextMethod("ВертикальнаяСЗакругленнымиУглами", "VRLine")]
        public string VRLine()
        {
            return Application.Driver.VRLine.ToString();
        }

        [ContextMethod("Таймер", "Timer")]
        public TfTimer Timer()
        {
            return new TfTimer();
        }

        [ContextMethod("ОкноСообщений", "MessageBox")]
        public TfMessageBox MessageBox()
        {
            return new TfMessageBox();
        }

        [ContextMethod("ОтправитьКлавиши", "SendKeys")]
        public void SendKeys(string p1, bool p3, bool p4, bool p5)
        {
            System.Char char1 = Convert.ToChar(p1.Substring(0, 1));
            Application.Driver.SendKeys(char1, (System.ConsoleKey)0, p3, p4, p5);
        }

        [ContextProperty("ТекстБуфераОбмена", "ClipboardText")]
        public string ClipboardText
        {
            get { return Terminal.Gui.Clipboard.Contents.ToString(); }
            set { Terminal.Gui.Clipboard.Contents = value; }
        }

        [ContextMethod("СтрокаСостояния", "StatusBar")]
        public TfStatusBar StatusBar()
        {
            return new TfStatusBar();
        }

        [ContextMethod("Выполнить", "Execute")]
        public IValue Execute(TfAction p1)
        {
            TfEventArgs eventArgs = new TfEventArgs();
            eventArgs.sender = instance;
            eventArgs.parameter = OneScriptTerminalGui.GetEventParameter(p1);
            Event = eventArgs;

            TfAction Action1 = p1;
            IRuntimeContextInstance script = Action1.Script;
            string method = Action1.MethodName;
            ReflectorContext reflector = new ReflectorContext();
            IValue res = ValueFactory.Create();
            try
            {
                res = reflector.CallMethod(script, method, null);
            }
            catch (Exception ex)
            {
                GlobalContext().Echo("Ошибка2: " + ex.Message);
            }
            return res;
        }

        [ContextProperty("ПриОткрытии", "NotifyNewRunState")]
        public TfAction NotifyNewRunState { get; set; }

        [ContextMethod("ЭлементМеню", "MenuItem")]
        public TfMenuItem MenuItem()
        {
            return new TfMenuItem();
        }

        [ContextProperty("Цвета", "Colors")]
        public TfColors Colors
        {
            get { return new TfColors(); }
        }

        [ContextMethod("Толщина", "Thickness")]
        public TfThickness Thickness(IValue p1, IValue p2 = null, IValue p3 = null, IValue p4 = null)
        {
            if (p2 != null && p3 != null && p4 != null)
            {
                return new TfThickness(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()), Convert.ToInt32(p3.AsNumber()), Convert.ToInt32(p4.AsNumber()));
            }
            return new TfThickness(Convert.ToInt32(p1.AsNumber()));
        }

        [ContextMethod("ЭлементСтрокиСостояния", "StatusItem")]
        public TfStatusItem StatusItem(int p1, string p2)
        {
            return new TfStatusItem(p1, p2);
        }

        [ContextMethod("ПунктМеню", "MenuBarItem")]
        public TfMenuBarItem MenuBarItem()
        {
            return new TfMenuBarItem();
        }

        [ContextMethod("ПанельМеню", "MenuBar")]
        public TfMenuBar MenuBar()
        {
            return new TfMenuBar();
        }

        [ContextMethod("ДобавитьВесьТекст", "AppendAllText")]
        public void AppendAllText(string p1, string p2)
        {
            File.AppendAllText(p1, p2, Encoding.UTF8);
        }

        [ContextProperty("Величина", "Dim")]
        public TfDim Dim
        {
            get { return new TfDim(); }
        }

        [ContextProperty("Позиция", "Pos")]
        public TfPos Pos
        {
            get { return new TfPos(); }
        }

        [ContextMethod("Обновить", "Refresh")]
        public void Refresh()
        {
            Application.Refresh();
        }

        [ContextMethod("Завершить", "Shutdown")]
        public void Shutdown()
        {
            //Application.Shutdown();
            Application.RequestStop(Top.Base_obj.M_Toplevel);
        }

        [ContextMethod("ОформительТекста", "TextFormatter")]
        public TfTextFormatter TextFormatter()
        {
            return new TfTextFormatter();
        }

        [ContextMethod("ЦветоваяСхема", "ColorScheme")]
        public TfColorScheme ColorScheme()
        {
            return new TfColorScheme();
        }

        [ContextMethod("Атрибут", "Attribute")]
        public TfAttribute Attribute(IValue p1 = null, IValue p2 = null, IValue p3 = null)
        {
            if (p1 == null && p2 == null && p3 == null)
            {
                return new TfAttribute();
            }
            else if (p1 != null && p2 == null && p3 == null)
            {
                if (p1.SystemType.Name == "Число")
                {
                    return new TfAttribute(Convert.ToInt32(p1.AsNumber()));
                }
                else
                {
                    return null;
                }
            }
            else if (p1 != null && p2 != null && p3 == null)
            {
                if (p1.SystemType.Name == "Число")
                {
                    return new TfAttribute(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()));
                }
                else
                {
                    return null;
                }
            }
            else if (p1 != null && p2 != null && p3 != null)
            {
                if (p1.SystemType.Name == "Число")
                {
                    return new TfAttribute(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()), Convert.ToInt32(p3.AsNumber()));
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        [ContextMethod("ЗапуститьИЗавершить", "RunAndShutdown")]
        public void RunAndShutdown()
        {
            //Top.CorrectionZet(); // Конфликтует с созданием меню.
            Application.Begin(top.Base_obj.M_Toplevel);
        }

        [ContextMethod("Запуск", "Run")]
        public void Run()
        {
            //Top.CorrectionZet(); // Конфликтует с созданием меню.
            Application.Run();
        }

        [ContextProperty("РазрешитьСобытия", "AllowEvents")]
        public bool HandleEvents
        {
            get { return handleEvents; }
            set { handleEvents = value; }
        }

        public static dynamic GetEventParameter(dynamic dll_objEvent)
        {
            if (dll_objEvent != null)
            {
                dynamic eventType = dll_objEvent.GetType();
                if (eventType == typeof(DelegateAction))
                {
                    return null;
                }
                else if (eventType == typeof(TfAction))
                {
                    return ((TfAction)dll_objEvent).Parameter;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static void ExecuteEvent(TfAction action)
        {
            if (!handleEvents)
            {
                return;
            }
            if (action == null)
            {
                return;
            }
            ReflectorContext reflector = new ReflectorContext();
            try
            {
                reflector.CallMethod(action.Script, action.MethodName, null);
            }
            catch (Exception ex)
            {
                GlobalContext().Echo("Обработчик не выполнен: " + action.MethodName + Environment.NewLine + ex.StackTrace);
            }
            Event = null;
            Application.Refresh();
        }

        [ContextProperty("Отправитель", "Sender")]
        public IValue Sender
        {
            get { return RevertEqualsObj(Event.Sender); }
        }

        [ContextProperty("АргументыСобытия", "EventArgs")]
        public TfEventArgs EventArgs
        {
            get { return Event; }
        }

        private static TfVerticalTextAlignment tf_VerticalTextAlignment = new TfVerticalTextAlignment();
        [ContextProperty("ВертикальноеВыравниваниеТекста", "VerticalTextAlignment")]
        public TfVerticalTextAlignment VerticalTextAlignment
        {
            get { return tf_VerticalTextAlignment; }
        }

        private static TfCursorVisibility tf_CursorVisibility = new TfCursorVisibility();
        [ContextProperty("ВидКурсора", "CursorVisibility")]
        public TfCursorVisibility CursorVisibility
        {
            get { return tf_CursorVisibility; }
        }

        private static TfTextAlignment tf_TextAlignment = new TfTextAlignment();
        [ContextProperty("ВыравниваниеТекста", "TextAlignment")]
        public TfTextAlignment TextAlignment
        {
            get { return tf_TextAlignment; }
        }

        private static TfKeys tf_Keys = new TfKeys();
        [ContextProperty("Клавиши", "Keys")]
        public TfKeys Keys
        {
            get { return tf_Keys; }
        }

        private static TfTextDirection tf_TextDirection = new TfTextDirection();
        [ContextProperty("НаправлениеТекста", "TextDirection")]
        public TfTextDirection TextDirection
        {
            get { return tf_TextDirection; }
        }

        private static TfLayoutStyle tf_LayoutStyle = new TfLayoutStyle();
        [ContextProperty("СтильКомпоновки", "LayoutStyle")]
        public TfLayoutStyle LayoutStyle
        {
            get { return tf_LayoutStyle; }
        }

        private static TfMenuItemCheckStyle tf_MenuItemCheckStyle = new TfMenuItemCheckStyle();
        [ContextProperty("СтильФлажкаЭлементаМеню", "MenuItemCheckStyle")]
        public TfMenuItemCheckStyle MenuItemCheckStyle
        {
            get { return tf_MenuItemCheckStyle; }
        }

        private static TfMouseFlags tf_MouseFlags = new TfMouseFlags();
        [ContextProperty("ФлагиМыши", "MouseFlags")]
        public TfMouseFlags MouseFlags
        {
            get { return tf_MouseFlags; }
        }

        private static TfColor tf_Color = new TfColor();
        [ContextProperty("Цвет", "Color")]
        public TfColor Color
        {
            get { return tf_Color; }
        }

        private static TfBorderStyle tf_BorderStyle = new TfBorderStyle();
        [ContextProperty("СтильГраницы", "BorderStyle")]
        public TfBorderStyle BorderStyle
        {
            get { return tf_BorderStyle; }
        }

        [ContextMethod("Граница", "Border")]
        public TfBorder Border()
        {
            return new TfBorder();
        }

        [ContextMethod("Окно", "Window")]
        public TfWindow Window(IValue p1 = null, IValue p2 = null, IValue p3 = null, IValue p4 = null)
        {
            if (p1 == null && p2 == null && p3 == null && p4 == null)
            {
                return new TfWindow();
            }
            else if (p1 != null && p2 == null && p3 == null && p4 == null)
            {
                if (p1.SystemType.Name == "Строка")
                {
                    return new TfWindow(p1.AsString());
                }
                else
                {
                    return null;
                }
            }
            else if (p1 != null && p2 != null && p3 == null && p4 == null)
            {
                if (p1.GetType() == typeof(TfRect) && p2.SystemType.Name == "Строка")
                {
                    return new TfWindow((TfRect)p1, p2.AsString());
                }
                else
                {
                    return null;
                }
            }
            else if (p1 != null && p2 != null && p3 != null && p4 == null)
            {
                if (p1.SystemType.Name == "Строка" && p2.SystemType.Name == "Число" && p3.GetType() == typeof(TfBorder))
                {
                    return new TfWindow(p1.AsString(), Convert.ToInt32(p2.AsNumber()), (TfBorder)p3);
                }
                else
                {
                    return null;
                }
            }
            else if (p1 != null && p2 != null && p3 != null && p4 != null)
            {
                if (p1.GetType() == typeof(TfRect) && p2.SystemType.Name == "Строка" && p3.SystemType.Name == "Число" && p4.GetType() == typeof(TfBorder))
                {
                    return new TfWindow((TfRect)p1, p2.AsString(), Convert.ToInt32(p3.AsNumber()), (TfBorder)p4);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        [ContextMethod("Действие", "Action")]
        public TfAction Action(IRuntimeContextInstance script = null, string methodName = null, IValue param = null)
        {
            return new TfAction(script, methodName, param);
        }

        [ContextMethod("Активировать", "Init")]
        public void Init()
        {
            Application.Init();
            try
            {
                Application.NotifyNewRunState += Application_NotifyNewRunState;
                top = new TfToplevel(Application.Top);
            }
            catch { }
        }

        [ContextProperty("Верхний", "Top")]
        public TfToplevel Top
        {
            get { return top; }
        }

        [ContextMethod("Кнопка", "Button")]
        public TfButton Button(IValue p1 = null, IValue p2 = null, IValue p3 = null, IValue p4 = null)
        {
            if (p1 == null && p2 == null && p3 == null && p4 == null)
            {
                return new TfButton();
            }
            else if (p1 != null && p2 != null && p3 != null && p4 != null)
            {
                if (p1.SystemType.Name == "Число" && p2.SystemType.Name == "Число" && p3.SystemType.Name == "Строка" && p4.SystemType.Name == "Булево")
                {
                    return new TfButton(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()), p3.AsString(), p4.AsBoolean());
                }
                else if (p1.SystemType.Name == "Число" && p2.SystemType.Name == "Число" && p3.SystemType.Name == "Число" && p4.SystemType.Name == "Число")
                {
                    TfButton TfButton1 = new TfButton();
                    TfButton1.Base_obj.M_Button.X = Terminal.Gui.Pos.At(Convert.ToInt32(p1.AsNumber()));
                    TfButton1.Base_obj.M_Button.Y = Terminal.Gui.Pos.At(Convert.ToInt32(p2.AsNumber()));
                    TfButton1.Base_obj.M_Button.Width = Terminal.Gui.Dim.Sized(Convert.ToInt32(p3.AsNumber()));
                    TfButton1.Base_obj.M_Button.Height = Terminal.Gui.Dim.Sized(Convert.ToInt32(p4.AsNumber()));
                    return TfButton1;
                }
                else
                {
                    return null;
                }
            }
            else if (p1 != null && p2 != null && p3 != null && p4 == null)
            {
                if (p1.SystemType.Name == "Число" && p2.SystemType.Name == "Число" && p3.SystemType.Name == "Строка")
                {
                    return new TfButton(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()), p3.AsString());
                }
                else
                {
                    return null;
                }
            }
            else if (p1 != null && p2 != null && p3 == null && p4 == null)
            {
                if (p1.SystemType.Name == "Строка" && p2.SystemType.Name == "Булево")
                {
                    return new TfButton(p1.AsString(), p2.AsBoolean());
                }
                else
                {
                    return null;
                }
            }
            else if (p1 != null && p2 == null && p3 == null && p4 == null)
            {
                if (p1.SystemType.Name == "Строка")
                {
                    return new TfButton(p1.AsString());
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public TfView View(IValue p1 = null, IValue p2 = null, IValue p3 = null)
        {
            if (p1 == null && p2 == null && p3 == null)
            {
                return new TfView();
            }
            else if (p1 != null && p2 == null && p3 == null)
            {
                if (p1.GetType() == typeof(TfRect))
                {
                    return new TfView((TfRect)p1);
                }
                else
                {
                    return null;
                }
            }
            else if (p1 != null && p2 != null && p3 != null)
            {
                if (p1.SystemType.Name == "Число" && p2.SystemType.Name == "Число" && p3.SystemType.Name == "Строка")
                {
                    return new TfView(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()), p3.AsString());
                }
                else if (p1.GetType() == typeof(TfRect) && p2.SystemType.Name == "Строка" && p3.GetType() == typeof(TfBorder))
                {
                    return new TfView((TfRect)p1, p2.AsString(), (TfBorder)p3);
                }
                else if (p1.SystemType.Name == "Строка" && p2.SystemType.Name == "Число" && p3.GetType() == typeof(TfBorder))
                {
                    return new TfView(p1.AsString(), Convert.ToInt32(p2.AsNumber()), (TfBorder)p3);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        [ContextMethod("Размер", "Size")]
        public TfSize Size(IValue p1 = null, IValue p2 = null)
        {
            if (p1 == null && p2 == null)
            {
                return new TfSize();
            }
            else if (p1 != null && p2 != null)
            {
                if (p1.SystemType.Name == "Число" && p2.SystemType.Name == "Число")
                {
                    return new TfSize(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()));
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        [ContextMethod("Прямоугольник", "Rect")]
        public TfRect Rect(IValue p1 = null, IValue p2 = null, IValue p3 = null, IValue p4 = null)
        {
            if (p1 == null && p2 == null && p3 == null && p4 == null)
            {
                return new TfRect();
            }
            else if (p1 != null && p2 != null && p3 == null && p4 == null)
            {
                if (p1.GetType() == typeof(TfPoint) && p2.GetType() == typeof(TfSize))
                {
                    return new TfRect((TfPoint)p1, (TfSize)p2);
                }
                else
                {
                    return null;
                }
            }
            else if (p1 != null && p2 != null && p3 != null && p4 != null)
            {
                if (p1.SystemType.Name == "Число" && p2.SystemType.Name == "Число" && p3.SystemType.Name == "Число" && p4.SystemType.Name == "Число")
                {
                    return new TfRect(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()), Convert.ToInt32(p3.AsNumber()), Convert.ToInt32(p4.AsNumber()));
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        [ContextMethod("Точка", "Point")]
        public TfPoint Rect(IValue p1 = null, IValue p2 = null)
        {
            if (p1 == null && p2 == null)
            {
                return new TfPoint();
            }
            else if (p1 != null && p2 != null)
            {
                if (p1.SystemType.Name == "Число" && p2.SystemType.Name == "Число")
                {
                    return new TfPoint(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()));
                }
                else
                {
                    return null;
                }
            }
            else if (p1 != null && p2 == null)
            {
                if (p1.GetType() == typeof(TfSize))
                {
                    return new TfPoint((TfSize)p1);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        [ContextMethod("Верхний", "Toplevel")]
        public TfToplevel Toplevel(IValue p1 = null, IValue p2 = null, IValue p3 = null, IValue p4 = null)
        {
            if (p1 != null)
            {
                if (p1.GetType() == typeof(TfRect))
                {
                    return new TfToplevel((TfRect)p1);
                }
                else if (p1.SystemType.Name == "Число")
                {
                    TfRect TfRect1 = new TfRect(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()), Convert.ToInt32(p3.AsNumber()), Convert.ToInt32(p4.AsNumber()));
                    return new TfToplevel(TfRect1);
                }
            }
            return new TfToplevel();
        }

        public static System.Collections.ArrayList StrFindBetween(string p1, string p2 = null, string p3 = null, bool p4 = true, bool p5 = true)
        {
            //p1 - исходная строка
            //p2 - подстрока поиска от которой ведем поиск
            //p3 - подстрока поиска до которой ведем поиск
            //p4 - не включать p2 и p3 в результат
            //p5 - в результат не будут включены участки, содержащие другие найденные участки, удовлетворяющие переданным параметрам
            //функция возвращает массив строк
            string str1 = p1;
            int Position1;
            System.Collections.ArrayList ArrayList1 = new System.Collections.ArrayList();
            if (p2 != null && p3 == null)
            {
                Position1 = str1.IndexOf(p2);
                while (Position1 >= 0)
                {
                    ArrayList1.Add(ValueFactory.Create("" + ((p4) ? str1.Substring(Position1 + p2.Length) : str1.Substring(Position1))));
                    str1 = str1.Substring(Position1 + 1);
                    Position1 = str1.IndexOf(p2);
                }
            }
            else if (p2 == null && p3 != null)
            {
                Position1 = str1.IndexOf(p3) + 1;
                int SumPosition1 = Position1;
                while (Position1 > 0)
                {
                    ArrayList1.Add(ValueFactory.Create("" + ((p4) ? str1.Substring(0, SumPosition1 - 1) : str1.Substring(0, SumPosition1 - 1 + p3.Length))));
                    try
                    {
                        Position1 = str1.Substring(SumPosition1 + 1).IndexOf(p3) + 1;
                        SumPosition1 = SumPosition1 + Position1 + 1;
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            else if (p2 != null && p3 != null)
            {
                Position1 = str1.IndexOf(p2);
                while (Position1 >= 0)
                {
                    string Стр2;
                    Стр2 = (p4) ? str1.Substring(Position1 + p2.Length) : str1.Substring(Position1);
                    int Position2 = Стр2.IndexOf(p3) + 1;
                    int SumPosition2 = Position2;
                    while (Position2 > 0)
                    {
                        if (p5)
                        {
                            if (Стр2.Substring(0, SumPosition2 - 1).IndexOf(p3) <= -1)
                            {
                                ArrayList1.Add(ValueFactory.Create("" + ((p4) ? Стр2.Substring(0, SumPosition2 - 1) : Стр2.Substring(0, SumPosition2 - 1 + p3.Length))));
                            }
                        }
                        else
                        {
                            ArrayList1.Add(ValueFactory.Create("" + ((p4) ? Стр2.Substring(0, SumPosition2 - 1) : Стр2.Substring(0, SumPosition2 - 1 + p3.Length))));
                        }
                        try
                        {
                            Position2 = Стр2.Substring(SumPosition2 + 1).IndexOf(p3) + 1;
                            SumPosition2 = SumPosition2 + Position2 + 1;
                        }
                        catch
                        {
                            break;

                        }
                    }
                    str1 = str1.Substring(Position1 + 1);
                    Position1 = str1.IndexOf(p2);
                }
            }
            return ArrayList1;
        }

        public static void AddToHashtable(dynamic p1, dynamic p2)
        {
            if (!hashtable.ContainsKey(p1))
            {
                hashtable.Add(p1, p2);
            }
            else
            {
                if (!((object)hashtable[p1]).Equals(p2))
                {
                    hashtable[p1] = p2;
                }
            }
        }

        public static void AddToShortcutDictionary(decimal p1, IValue p2)
        {
            if (!shortcutDictionary.ContainsKey(p1))
            {
                ArrayList ArrayList1 = new ArrayList();
                ArrayList1.Add(p2);
                shortcutDictionary.Add(p1, ArrayList1);
            }
            else
            {
                ArrayList ArrayList1 = shortcutDictionary[p1];
                if (!ArrayList1.Contains(p2))
                {
                    ArrayList1.Add(p2);
                }
            }
        }

        public static void RemoveFromShortcutDictionary(decimal p1, IValue p2)
        {
            if (shortcutDictionary.ContainsKey(p1))
            {
                try
                {
                    shortcutDictionary[p1].Remove(p2);
                }
                catch { }
            }
        }

        public static ArrayList GetFromShortcutDictionary(IValue p1)
        {
            ArrayList ArrayList1 = new ArrayList();
            foreach (var item in shortcutDictionary)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    if (item.Value[i] == p1)
                    {
                        ArrayList1.Add(item.Key);
                    }
                }
            }
            return ArrayList1;
        }

        public static dynamic RevertShortcut(dynamic shortcut)
        {
            try
            {
                return shortcutDictionary[shortcut];
            }
            catch
            {
                return null;
            }
        }

        public static dynamic RevertEqualsObj(dynamic initialObject)
        {
            try
            {
                return hashtable[initialObject];
            }
            catch
            {
                return null;
            }
        }

        public static IValue RevertObj(dynamic initialObject)
        {
            //ScriptEngine.Machine.Values.NullValue NullValue1;
            //ScriptEngine.Machine.Values.BooleanValue BooleanValue1;
            //ScriptEngine.Machine.Values.DateValue DateValue1;
            //ScriptEngine.Machine.Values.NumberValue NumberValue1;
            //ScriptEngine.Machine.Values.StringValue StringValue1;

            //ScriptEngine.Machine.Values.GenericValue GenericValue1;
            //ScriptEngine.Machine.Values.TypeTypeValue TypeTypeValue1;
            //ScriptEngine.Machine.Values.UndefinedValue UndefinedValue1;

            // Если initialObject равен null.
            try
            {
                if (initialObject == null)
                {
                    return (IValue)null;
                }
            }
            catch { }
            // Если initialObject равен null.
            try
            {
                string str_initialObject = initialObject.GetType().ToString();
            }
            catch
            {
                return (IValue)null;
            }
            // initialObject не равен null
            dynamic Obj1 = null;
            string str1 = initialObject.GetType().ToString();
            // Если initialObject второго уровня и у него есть ссылка на третий уровень.
            try
            {
                Obj1 = initialObject.dll_obj;
            }
            catch { }
            if (Obj1 != null)
            {
                return (IValue)Obj1;
            }
            // если initialObject не из пространства имен onescriptgui, то есть Уровень1 и у него есть аналог в
            // пространстве имен ostgui с конструктором принимающим параметром initialObject
            try
            {
                if (!str1.Contains("ostgui."))
                {
                    string str2 = "ostgui.Tf" + str1.Substring(str1.LastIndexOf(".") + 1);
                    System.Type TestType = System.Type.GetType(str2, false, true);
                    object[] args = { initialObject };
                    Obj1 = Activator.CreateInstance(TestType, args);
                }
            }
            catch { }
            if (Obj1 != null)
            {
                return (IValue)Obj1;
            }
            // если initialObject из пространства имен onescriptgui, то есть Уровень2 и у него есть аналог в
            // пространстве имен ostgui с конструктором принимающим параметром initialObject
            try
            {
                if (str1.Contains("ostgui."))
                {
                    string str3 = str1.Replace("ostgui.", "ostgui.Tf");
                    System.Type TestType = System.Type.GetType(str3, false, true);
                    object[] args = { initialObject };
                    Obj1 = Activator.CreateInstance(TestType, args);
                }
            }
            catch { }
            if (Obj1 != null)
            {
                return (IValue)Obj1;
            }
            // Если initialObject с возможными другими типами.
            string str4 = null;
            try
            {
                str4 = initialObject.SystemType.Name;
            }
            catch
            {
                if ((str1 == "System.String") ||
                (str1 == "System.Decimal") ||
                (str1 == "System.Int32") ||
                (str1 == "System.Boolean") ||
                (str1 == "System.DateTime"))
                {
                    return (IValue)ValueFactory.Create(initialObject);
                }
                else if (str1 == "System.Byte")
                {
                    int vOut = Convert.ToInt32(initialObject);
                    return (IValue)ValueFactory.Create(vOut);
                }
                else if (str1 == "System.DBNull")
                {
                    string vOut = Convert.ToString(initialObject);
                    return (IValue)ValueFactory.Create(vOut);
                }
            }
            // Если тип initialObject определяется односкриптом.
            if (str4 == "Неопределено")
            {
                return (IValue)null;
            }
            if (str4 == "Булево")
            {
                return (IValue)initialObject;
            }
            if (str4 == "Дата")
            {
                return (IValue)initialObject;
            }
            if (str4 == "Число")
            {
                return (IValue)initialObject;
            }
            if (str4 == "Строка")
            {
                return (IValue)initialObject;
            }
            // Если ничего не подходит.
            return (IValue)initialObject;
        }

        public static void WriteToFile(string str)
        {
            // добавление в файл
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("C:\\444\\Ошибки.txt", true, Encoding.UTF8))
            {
                writer.WriteLineAsync(str);
            }
        }
    }
}
