using System;
using System.IO;
using System.Text;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library;
using Terminal.Gui;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace ostgui
{
    [ContextClass("ТерминалФормыДляОдноСкрипта", "OneScriptTerminalGui")]
    public class OneScriptTerminalGui : AutoContext<OneScriptTerminalGui>
    {
        public TfToplevel top;
        public static OneScriptTerminalGui instance;
        public static TfEventArgs Event = null;
        public static bool handleEvents = true;
        private static Action OnOpen;
        public static bool isWin = System.Environment.OSVersion.VersionString.Contains("Microsoft");

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
                        byte[] assemblyData = Utils.StreamToBytes(stream);
                        stream.Dispose();
                        return Assembly.Load(assemblyData);
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

            instance = new OneScriptTerminalGui();
            instance.Init();

            if (isWin)
            {
                IntPtr hwnd = GetConsoleWindow();
                if (hwnd != IntPtr.Zero)
                {
                    GetWindowRect(hwnd, out RECT originalRect);
                    originalLeft = originalRect.left;
                    currentLeft = originalLeft;

                    // Запускаем мониторинг левой границы в отдельном потоке
                    Thread monitorThread = new Thread(() => MonitorWindowPosition(hwnd));
                    monitorThread.IsBackground = true;
                    monitorThread.Start();
                    isRunning = true;
                }
            }

            return instance;
        }

        [DllImport("user32.dll")]
        private static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left, top, right, bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public POINT ptMinPosition;
            public POINT ptMaxPosition;
            public RECT rcNormalPosition;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x, y;
        }

        private const uint SWP_NOZORDER = 0x0004;
        private static bool isRunning = true;
        private static int originalLeft;
        private static int currentLeft;
        private const int SW_MAXIMIZE = 3;
        private static bool applicationIsStop = false;

        private static void MonitorWindowPosition(IntPtr hwnd)
        {
            int currentWidth;
            int currentHeight;
            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
            bool isMaximizedByState = false;
            while (isRunning && !applicationIsStop)
            {
                // Проверка состояния окна
                placement.length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));
                if (GetWindowRect(hwnd, out RECT currentRect))
                {
                    currentWidth = currentRect.right - currentRect.left;
                    currentHeight = currentRect.bottom - currentRect.top;
                    if (GetWindowPlacement(hwnd, ref placement))
                    {
                        isMaximizedByState = (placement.showCmd == SW_MAXIMIZE);
                        if (isMaximizedByState)
                        {
                            SetWindowPos(hwnd, IntPtr.Zero,
                                currentRect.left,
                                currentRect.top,
                                currentWidth,
                                currentHeight,
                                SWP_NOZORDER);
                            currentLeft = currentRect.left;
                        }
                        else
                        {
                            // Если левая граница сдвинулась
                            if (currentRect.left != currentLeft && currentLeft != 0)
                            {
                                SetWindowPos(hwnd, IntPtr.Zero,
                                    currentRect.left,
                                    currentRect.top,
                                    Utils.minCols,
                                    currentHeight,
                                    SWP_NOZORDER);
                                currentLeft = currentRect.left;
                            }
                        }
                    }
                }
                Thread.Sleep(50); // Проверяем каждые 50мс
            }
        }

        // Методы и свойства объекта OneScriptTerminalGui.

        [ContextProperty("ПлатформаWin", "WinPlatform")]
        public bool WinPlatform
        {
            get { return isWin; }
        }

        [ContextMethod("ТаймерНачатьИОстановить", "TimerStartAndStop")]
        public void TimerStartAndStop(IRuntimeContextInstance p1, string p2, int p3 = 1000, int p4 = 1)
        {
            TfTimer timer = new TfTimer();
            timer.Interval = p3;
            timer.Iterations = p4;
            TfAction action = Action(p1, p2);
            timer.TimerStartAndStop(action);
        }

        [ContextProperty("ЛогФайл", "PathLog")]
        public string PathLog
        {
            get { return Utils.PathLog; }
            set { Utils.PathLog = value; }
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

        private int labelLanguage = 0;
        public int LabelLanguage
        {
            get { return labelLanguage; }
            set { labelLanguage = value; }
        }

        [ContextProperty("Отправитель", "Sender")]
        public IValue Sender
        {
            get { return Utils.RevertEqualsObj(Event.Sender); }
        }

        [ContextProperty("АргументыСобытия", "EventArgs")]
        public TfEventArgs EventArgs
        {
            get { return Event; }
        }

        public void Init()
        {
            Application.Init();
            try
            {
                Utils.minCols = Cols;
                Utils.minRows = Rows;

                Application.NotifyStopRunState += Application_NotifyStopRunState;
                Application.NotifyNewRunState += Application_NotifyNewRunState;
                top = new TfToplevel(Application.Top);
                applicationIsStop = false;
            }
            catch (Exception ex)
            {
                Utils.GlobalContext().Echo($"Ошибка инициализации: {ex.Message}");
            }
        }

        private void Application_NotifyStopRunState(Terminal.Gui.Toplevel obj)
        {
            applicationIsStop = true;
        }

        [ContextProperty("РазмерИзменен", "Resized")]
        public TfAction Resized { get; set; }

        private decimal quitKey;
        [ContextProperty("КлавишаВыхода", "QuitKey")]
        public decimal QuitKey
        {
            get { return quitKey; }
            set { quitKey = value; }
        }

        [ContextMethod("Эмодзи", "Emoji")]
        public string Emoji(IValue p1)
        {
            var sb = new StringBuilder();
            if (Utils.IsNumber(p1))
            {
                try
                {
                    sb.Append(Char.ConvertFromUtf32(Utils.ToInt32(p1))).ToString();
                }
                catch { }
            }
            else if (Utils.IsString(p1))
            {
                string p2 = Utils.ToString(p1);
                p2 = p2.Replace("0x", "").Replace("0х", "").Replace("\\u", "");
                try
                {
                    try
                    {
                        int num = Convert.ToInt32(p2);
                        string str = Char.ConvertFromUtf32(num);
                        sb.Append(str);
                    }
                    catch
                    {
                        int num = Convert.ToInt32(p2, 16);
                        string str = Char.ConvertFromUtf32(num);
                        sb.Append(str);
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

        [ContextMethod("ОтправитьКлавиши", "SendKeys")]
        public void SendKeys(string p1, bool p3, bool p4, bool p5)
        {
            System.Char char1 = Convert.ToChar(p1.Substring(0, 1));
            Application.Driver.SendKeys(char1, (System.ConsoleKey)0, p3, p4, p5);
        }

        [ContextMethod("ОтправитьКлавишуКонсоли", "SendConsoleKey")]
        public void SendConsoleKey(int p1, bool p2 = false, bool p3 = false, bool p4 = false)
        {
            Application.Driver.SendKeys(System.Char.MinValue, (System.ConsoleKey)p1, p2, p3, p4);
        }

        [ContextProperty("ТекстБуфераОбмена", "ClipboardText")]
        public string ClipboardText
        {
            get { return Terminal.Gui.Clipboard.Contents.ToString(); }
            set { Terminal.Gui.Clipboard.Contents = value; }
        }

        [ContextMethod("Выполнить", "Execute")]
        public IValue Execute(TfAction p1)
        {
            TfEventArgs eventArgs = new TfEventArgs();
            eventArgs.sender = this;
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
                Utils.GlobalContext().Echo("Ошибка2: " + ex.Message);
            }
            return res;
        }

        [ContextProperty("ПриОткрытии", "NotifyNewRunState")]
        public TfAction NotifyNewRunState { get; set; }

        [ContextMethod("ДобавитьВесьТекст", "AppendAllText")]
        public void AppendAllText(string p1, string p2)
        {
            if (!File.Exists(p1))
            {
                File.Create(p1).Close();
            }
            File.AppendAllText(p1, p2, Encoding.UTF8);
        }

        [ContextMethod("Обновить", "Refresh")]
        public void Refresh()
        {
            Application.Refresh();
        }

        private bool errorRecord = false;
        [ContextProperty("ЗаписьОшибок", "ErrorRecord")]
        public bool ErrorRecord
        {
            get { return errorRecord; }
            set { errorRecord = value; }
        }

        [ContextMethod("Завершить", "Shutdown")]
        public void Shutdown()
        {
            //Application.RequestStop(Top.Base_obj.M_Toplevel);
            Application.Shutdown();
            applicationIsStop = true;
        }

        [ContextMethod("ЗапуститьИЗавершить", "RunAndShutdown")]
        public void RunAndShutdown()
        {
            try
            {
                //Top.CorrectionZet(); // Конфликтует с созданием меню.
                Application.Begin(top.Base_obj.M_Toplevel);
            }
            catch (Exception ex)
            {
                string _ex = "" + ex.StackTrace;
                if (ErrorRecord)
                {
                    Utils.WriteToFile("Error RunAndShutdown = " + _ex);
                }
            }
        }

        [ContextMethod("Запуск", "Run")]
        public void Run()
        {
            try
            {
                //Top.CorrectionZet(); // Конфликтует с созданием меню.
                Application.Run();
            }
            catch (Exception ex)
            {
                string _ex = "" + ex.StackTrace;
                if (ErrorRecord)
                {
                    Utils.WriteToFile("Error Run = " + _ex);
                }
            }
        }

        [ContextProperty("РазрешитьСобытия", "AllowEvents")]
        public bool HandleEvents
        {
            get { return handleEvents; }
            set { handleEvents = value; }
        }

        // Методы создания перечислений.

        private TfSortOrder tf_SortOrder;
        [ContextProperty("ПорядокСортировки", "SortOrder")]
        public TfSortOrder SortOrder
        {
            get
            {
                if (tf_SortOrder == null)
                {
                    tf_SortOrder = new TfSortOrder();
                }
                return tf_SortOrder;
            }
        }

        private TfConsoleKey tf_ConsoleKey;
        [ContextProperty("КлавишиКонсоли", "ConsoleKey")]
        public TfConsoleKey ConsoleKey
        {
            get
            {
                if (tf_ConsoleKey == null)
                {
                    tf_ConsoleKey = new TfConsoleKey();
                }
                return tf_ConsoleKey;
            }
        }

        private static TfCommandTUI tf_CommandTUI;
        [ContextProperty("КомандаTUI", "CommandTUI")]
        public TfCommandTUI CommandTUI
        {
            get
            {
                if (tf_CommandTUI == null)
                {
                    tf_CommandTUI = new TfCommandTUI();
                }
                return tf_CommandTUI;
            }
        }

        private static TfDialogResult tf_DialogResult;
        [ContextProperty("РезультатДиалога", "DialogResult")]
        public TfDialogResult DialogResult
        {
            get
            {
                if (tf_DialogResult == null)
                {
                    tf_DialogResult = new TfDialogResult();
                }
                return tf_DialogResult;
            }
        }

        private static TfLanguage tf_Language;
        [ContextProperty("Язык", "Language")]
        public TfLanguage Language
        {
            get
            {
                if (tf_Language == null)
                {
                    tf_Language = new TfLanguage();
                }
                return tf_Language;
            }
        }

        private static TfVerticalTextAlignment tf_VerticalTextAlignment;
        [ContextProperty("ВертикальноеВыравниваниеТекста", "VerticalTextAlignment")]
        public TfVerticalTextAlignment VerticalTextAlignment
        {
            get
            {
                if (tf_VerticalTextAlignment == null)
                {
                    tf_VerticalTextAlignment = new TfVerticalTextAlignment();
                }
                return tf_VerticalTextAlignment;
            }
        }

        private static TfCursorVisibility tf_CursorVisibility;
        [ContextProperty("ВидКурсора", "CursorVisibility")]
        public TfCursorVisibility CursorVisibility
        {
            get
            {
                if (tf_CursorVisibility == null)
                {
                    tf_CursorVisibility = new TfCursorVisibility();
                }
                return tf_CursorVisibility;
            }
        }

        private static TfTextAlignment tf_TextAlignment;
        [ContextProperty("ВыравниваниеТекста", "TextAlignment")]
        public TfTextAlignment TextAlignment
        {
            get
            {
                if (tf_TextAlignment == null)
                {
                    tf_TextAlignment = new TfTextAlignment();
                }
                return tf_TextAlignment;
            }
        }

        private static TfKeys tf_Keys;
        [ContextProperty("Клавиши", "Keys")]
        public TfKeys Keys
        {
            get
            {
                if (tf_Keys == null)
                {
                    tf_Keys = new TfKeys();
                }
                return tf_Keys;
            }
        }

        private static TfTextDirection tf_TextDirection;
        [ContextProperty("НаправлениеТекста", "TextDirection")]
        public TfTextDirection TextDirection
        {
            get
            {
                if (tf_TextDirection == null)
                {
                    tf_TextDirection = new TfTextDirection();
                }
                return tf_TextDirection;
            }
        }

        private static TfLayoutStyle tf_LayoutStyle;
        [ContextProperty("СтильКомпоновки", "LayoutStyle")]
        public TfLayoutStyle LayoutStyle
        {
            get
            {
                if (tf_LayoutStyle == null)
                {
                    tf_LayoutStyle = new TfLayoutStyle();
                }
                return tf_LayoutStyle;
            }
        }

        private static TfProgressBarStyle tf_ProgressBarStyle;
        [ContextProperty("СтильИндикатора", "ProgressBarStyle")]
        public TfProgressBarStyle ProgressBarStyle
        {
            get
            {
                if (tf_ProgressBarStyle == null)
                {
                    tf_ProgressBarStyle = new TfProgressBarStyle();
                }
                return tf_ProgressBarStyle;
            }
        }

        private static TfButtonAlignments tf_ButtonAlignments;
        [ContextProperty("ВыравниваниеКнопок", "ButtonAlignments")]
        public TfButtonAlignments ButtonAlignments
        {
            get
            {
                if (tf_ButtonAlignments == null)
                {
                    tf_ButtonAlignments = new TfButtonAlignments();
                }
                return tf_ButtonAlignments;
            }
        }

        private static TfDisplayModeLayout tf_DisplayModeLayout;
        [ContextProperty("МакетПереключателя", "DisplayModeLayout")]
        public TfDisplayModeLayout DisplayModeLayout
        {
            get
            {
                if (tf_DisplayModeLayout == null)
                {
                    tf_DisplayModeLayout = new TfDisplayModeLayout();
                }
                return tf_DisplayModeLayout;
            }
        }

        private static TfProgressBarFormat tf_ProgressBarFormat;
        [ContextProperty("ФорматИндикатора", "ProgressBarFormat")]
        public TfProgressBarFormat ProgressBarFormat
        {
            get
            {
                if (tf_ProgressBarFormat == null)
                {
                    tf_ProgressBarFormat = new TfProgressBarFormat();
                }
                return tf_ProgressBarFormat;
            }
        }

        private static TfMenuItemCheckStyle tf_MenuItemCheckStyle;
        [ContextProperty("СтильФлажкаЭлементаМеню", "MenuItemCheckStyle")]
        public TfMenuItemCheckStyle MenuItemCheckStyle
        {
            get
            {
                if (tf_MenuItemCheckStyle == null)
                {
                    tf_MenuItemCheckStyle = new TfMenuItemCheckStyle();
                }
                return tf_MenuItemCheckStyle;
            }
        }

        private static TfMouseFlags tf_MouseFlags;
        [ContextProperty("ФлагиМыши", "MouseFlags")]
        public TfMouseFlags MouseFlags
        {
            get
            {
                if (tf_MouseFlags == null)
                {
                    tf_MouseFlags = new TfMouseFlags();
                }
                return tf_MouseFlags;
            }
        }

        private static TfColor tf_Color;
        [ContextProperty("Цвет", "Color")]
        public TfColor Color
        {
            get
            {
                if (tf_Color == null)
                {
                    tf_Color = new TfColor();
                }
                return tf_Color;
            }
        }

        private static TfBorderStyle tf_BorderStyle;
        [ContextProperty("СтильГраницы", "BorderStyle")]
        public TfBorderStyle BorderStyle
        {
            get
            {
                if (tf_BorderStyle == null)
                {
                    tf_BorderStyle = new TfBorderStyle();
                }
                return tf_BorderStyle;
            }
        }

        // Методы создания объектов.

        [ContextMethod("Математика", "Math")]
        public TfMath Math()
        {
            return new TfMath();
        }

        [ContextMethod("СтильВкладки", "TabStyle")]
        public TfTabStyle TabStyle()
        {
            return new TfTabStyle();
        }

        [ContextMethod("Уведомление", "Balloons")]
        public TfBalloons Balloons()
        {
            return new TfBalloons();
        }

        [ContextMethod("СтильКолонки", "ColumnStyle")]
        public TfColumnStyle ColumnStyle()
        {
            return new TfColumnStyle();
        }

        [ContextMethod("СтилиКолонки", "ColumnStyles")]
        public TfColumnStyles ColumnStyles()
        {
            return new TfColumnStyles();
        }

        [ContextMethod("Переключатель", "RadioGroup")]
        public TfRadioGroup RadioGroup(int p1 = 1, int p2 = 1, int p3 = 10, int p4 = 5)
        {
            TfRadioGroup radioGroup = new TfRadioGroup();
            radioGroup.X = ValueFactory.Create(p1);
            radioGroup.Y = ValueFactory.Create(p2);
            radioGroup.Width = ValueFactory.Create(p3);
            radioGroup.Height = ValueFactory.Create(p4);
            return radioGroup;
        }

        [ContextMethod("КолонкаДанных", "DataColumn")]
        public TfDataColumn DataColumn(string p1 = "Column", int p2 = 0)
        {
            int type1 = p2;
            System.Type DataType1 = typeof(System.String);
            if (type1 == 0)
            {
                DataType1 = typeof(System.String);
            }
            else if (type1 == 1)
            {
                DataType1 = typeof(System.Decimal);
            }
            else if (type1 == 2)
            {
                DataType1 = typeof(System.Boolean);
            }
            else if (type1 == 3)
            {
                DataType1 = typeof(System.DateTime);
            }
            else if (type1 == 4)
            {
                DataType1 = typeof(System.Object);
            }
            return new TfDataColumn(p1, DataType1);
        }

        [ContextMethod("ТаблицаДанных", "DataTable")]
        public TfDataTable DataTable()
        {
            return new TfDataTable();
        }

        [ContextProperty("ТипДанных", "DataType")]
        public TfDataType DataType1
        {
            get { return new TfDataType(); }
        }

        [ContextMethod("Вкладка", "TabPage")]
        public TfTabPage TabPage(string p1 = "Вкладка", IValue p2 = null)
        {
            IValue view;
            if (p2 != null)
            {
                view = p2;
            }
            else
            {
                view = new TfToplevel();
            }
            TfTabPage tab = new TfTabPage();
            tab.Text = p1;
            tab.View = view;
            return tab;
        }

        [ContextMethod("ПанельВкладок", "TabView")]
        public TfTabView TabView(int p1 = 1, int p2 = 1, int p3 = 20, int p4 = 5)
        {
            TfTabView tabView = new TfTabView();
            tabView.Frame = new TfRect(p1, p2, p3, p4);
            return tabView;
        }

        [ContextMethod("СтильДерева", "TreeStyle")]
        public TfTreeStyle TreeStyle()
        {
            return new TfTreeStyle();
        }

        [ContextMethod("УзелДерева", "TreeNode")]
        public TfTreeNode TreeNode(string p1 = "Узел")
        {
            TfTreeNode treeNode = new TfTreeNode();
            treeNode.Text = p1;
            return treeNode;
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
        public TfAttribute Attribute(IValue p1 = null, IValue p2 = null)
        {
            if (Utils.AllNull(p1, p2))
            {
                return new TfAttribute();
            }
            else if (Utils.AllNotNull(p1) && Utils.AllNull(p2))
            {
                return new TfAttribute(Utils.ToInt32(p1));
            }
            else if (Utils.AllNotNull(p1, p2))
            {
                return new TfAttribute(Utils.ToInt32(p1), Utils.ToInt32(p2));
            }
            return null;
        }

        [ContextMethod("ЭлементМеню", "MenuItem")]
        public TfMenuItem MenuItem(string p1 = "Элемент меню")
        {
            TfMenuItem menuItem = new TfMenuItem();
            menuItem.Title = p1;
            return menuItem;
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
                return new TfThickness(Utils.ToInt32(p1), Utils.ToInt32(p2), Utils.ToInt32(p3), Utils.ToInt32(p4));
            }
            return new TfThickness(Utils.ToInt32(p1));
        }

        [ContextMethod("ЭлементСтрокиСостояния", "StatusItem")]
        public TfStatusItem StatusItem(int p1, string p2)
        {
            return new TfStatusItem(p1, p2);
        }

        [ContextMethod("ПунктМеню", "MenuBarItem")]
        public TfMenuBarItem MenuBarItem(string p1 = "Пункт меню")
        {
            TfMenuBarItem menuBarItem = new TfMenuBarItem();
            menuBarItem.Title = p1;
            return menuBarItem;
        }

        [ContextMethod("ПанельМеню", "MenuBar")]
        public TfMenuBar MenuBar()
        {
            return new TfMenuBar();
        }

        [ContextMethod("СтрокаСостояния", "StatusBar")]
        public TfStatusBar StatusBar()
        {
            return new TfStatusBar();
        }

        [ContextMethod("Таймер", "Timer")]
        public TfTimer Timer(int p1 = 1000)
        {
            TfTimer timer = new TfTimer();
            timer.Interval = p1;
            return timer;
        }

        [ContextMethod("ОкноСообщений", "MessageBox")]
        public TfMessageBox MessageBox()
        {
            return new TfMessageBox();
        }

        [ContextMethod("СтильТаблицы", "TableStyle")]
        public TfTableStyle TableStyle()
        {
            return new TfTableStyle();
        }

        [ContextMethod("КонтекстноеМеню", "ContextMenu")]
        public TfContextMenu ContextMenu()
        {
            return new TfContextMenu();
        }

        [ContextMethod("Дерево", "TreeView")]
        public TfTreeView TreeView(int p1 = 1, int p2 = 1, int p3 = 10, int p4 = 5)
        {
            TfTreeView treeView = new TfTreeView();
            treeView.Frame = new TfRect(p1, p2, p3, p4);
            return treeView;
        }

        [ContextMethod("Текстовый", "TextView")]
        public TfTextView TextView(int p1 = 1, int p2 = 1, int p3 = 30, int p4 = 5)
        {
            TfTextView textView = new TfTextView();
            textView.X = ValueFactory.Create(p1);
            textView.Y = ValueFactory.Create(p2);
            textView.Width = ValueFactory.Create(p3);
            textView.Height = ValueFactory.Create(p4);
            return textView;
        }

        [ContextMethod("ПолеДаты", "DateField")]
        public TfDateField DateField(IValue p1 = null, int p2 = 1, int p3 = 1)
        {
            TfDateField dateField = new TfDateField();
            DateTime dateTime = DateTime.Now;
            if (p1 != null)
            {
                dateTime = p1.AsDate();
            }
            dateField.Base_obj.M_DateField.Date = dateTime;
            dateField.X = ValueFactory.Create(p2);
            dateField.Y = ValueFactory.Create(p3);
            return dateField;
        }

        [ContextMethod("ПолеВремени", "TimeField")]
        public TfTimeField TimeField(IValue p1 = null, int p2 = 1, int p3 = 1)
        {
            TfTimeField timeField = new TfTimeField();
            DateTime dateTime = DateTime.Now;
            if (p1 != null)
            {
                dateTime = p1.AsDate();
            }
            timeField.Base_obj.M_TimeField.Time = dateTime - DateTime.MinValue;
            timeField.X = ValueFactory.Create(p2);
            timeField.Y = ValueFactory.Create(p3);
            return timeField;
        }

        [ContextMethod("ПолеВвода", "TextField")]
        public TfTextField TextField(string p1 = "Поле ввода", int p2 = 1, int p3 = 1, int p4 = 14)
        {
            TfTextField textField = new TfTextField();
            textField.Text = p1;
            textField.X = ValueFactory.Create(p2);
            textField.Y = ValueFactory.Create(p3);
            textField.Width = ValueFactory.Create(p4);
            return textField;
        }

        [ContextMethod("Таблица", "TableView")]
        public TfTableView TableView(int p1 = 1, int p2 = 1, int p3 = 60, int p4 = 8)
        {
            TfTableView tableView = new TfTableView();
            tableView.X = ValueFactory.Create(p1);
            tableView.Y = ValueFactory.Create(p2);
            tableView.Width = ValueFactory.Create(p3);
            tableView.Height = ValueFactory.Create(p4);
            return tableView;
        }

        [ContextMethod("ПолосаПрокрутки", "ScrollBarView")]
        public TfScrollBarView ScrollBarView(IValue p1)
        {
            return new TfScrollBarView(p1, true, true);
        }

        [ContextMethod("Прокручиваемый", "ScrollView")]
        public TfScrollView ScrollView(int p1 = 1, int p2 = 1, int p3 = 20, int p4 = 5)
        {
            TfScrollView scrollView = new TfScrollView();
            scrollView.X = ValueFactory.Create(p1);
            scrollView.Y = ValueFactory.Create(p2);
            scrollView.Width = ValueFactory.Create(p3);
            scrollView.Height = ValueFactory.Create(p4);
            return scrollView;
        }

        [ContextMethod("РамкаГруппы", "FrameView")]
        public TfFrameView FrameView(string p1 = "Рамка группы", int p2 = 1, int p3 = 1, int p4 = 17, int p5 = 5)
        {
            TfFrameView frameView = new TfFrameView();
            frameView.Title = p1;
            frameView.X = ValueFactory.Create(p2);
            frameView.Y = ValueFactory.Create(p3);
            frameView.Width = ValueFactory.Create(p4);
            frameView.Height = ValueFactory.Create(p5);
            return frameView;
        }

        [ContextMethod("ВыборЦвета", "ColorPicker")]
        public TfColorPicker ColorPicker(string p1 = "Выбор цвета", int p2 = 1, int p3 = 1)
        {
            TfColorPicker сolorPicker = new TfColorPicker();
            сolorPicker.Text = p1;
            сolorPicker.X = ValueFactory.Create(p2);
            сolorPicker.Y = ValueFactory.Create(p3);
            return сolorPicker;
        }

        [ContextMethod("СписокЭлементов", "ListView")]
        public TfListView ListView(int p1 = 1, int p2 = 1, int p3 = 30, int p4 = 5)
        {
            TfListView listView = new TfListView();
            listView.X = ValueFactory.Create(p1);
            listView.Y = ValueFactory.Create(p2);
            listView.Width = ValueFactory.Create(p3);
            listView.Height = ValueFactory.Create(p4);
            return listView;
        }

        [ContextMethod("ПолеВыбора", "ComboBox")]
        public TfComboBox ComboBox(int p1 = 1, int p2 = 1, int p3 = 30, int p4 = 5)
        {
            TfComboBox comboBox = new TfComboBox();
            comboBox.X = ValueFactory.Create(p1);
            comboBox.Y = ValueFactory.Create(p2);
            comboBox.Width = ValueFactory.Create(p3);
            comboBox.Height = ValueFactory.Create(p4);
            return comboBox;
        }

        [ContextMethod("Диалог", "Dialog")]
        public TfDialog Dialog(string p1 = "Диалог", int p2 = 1, int p3 = 1, int p4 = 30, int p5 = 10)
        {
            TfDialog dialog = new TfDialog();
            dialog.Title = p1;
            dialog.X = ValueFactory.Create(p2);
            dialog.Y = ValueFactory.Create(p3);
            dialog.Width = ValueFactory.Create(p4);
            dialog.Height = ValueFactory.Create(p5);
            return dialog;
        }

        [ContextMethod("ДиалогОткрытия", "OpenDialog")]
        public TfOpenDialog OpenDialog(string p1 = "Диалог открытия", string p2 = "Сообщение", int p3 = 1, int p4 = 1, int p5 = 80, int p6 = 20, int p7 = 0)
        {
            LabelLanguage = p7;
            TfOpenDialog openDialog = new TfOpenDialog();
            openDialog.Title = p1;
            openDialog.Message = p2;
            openDialog.X = ValueFactory.Create(p3);
            openDialog.Y = ValueFactory.Create(p4);
            openDialog.Width = ValueFactory.Create(p5);
            openDialog.Height = ValueFactory.Create(p6);
            openDialog.Base_obj.LabelLanguage = LabelLanguage;
            return openDialog;
        }

        [ContextMethod("ДиалогСохранения", "SaveDialog")]
        public TfSaveDialog SaveDialog(string p1 = "Диалог сохранения", string p2 = "Сообщение", int p3 = 1, int p4 = 1, int p5 = 80, int p6 = 20, int p7 = 0)
        {
            LabelLanguage = p7;
            TfSaveDialog saveDialog = new TfSaveDialog();
            saveDialog.Title = p1;
            saveDialog.Message = p2;
            saveDialog.X = ValueFactory.Create(p3);
            saveDialog.Y = ValueFactory.Create(p4);
            saveDialog.Width = ValueFactory.Create(p5);
            saveDialog.Height = ValueFactory.Create(p6);
            saveDialog.Base_obj.LabelLanguage = LabelLanguage;
            return saveDialog;
        }

        [ContextMethod("Индикатор", "ProgressBar")]
        public TfProgressBar ProgОкноressBar(int p1 = 1, int p2 = 1, int p3 = 40, int p4 = 1)
        {
            TfProgressBar progressBar = new TfProgressBar();
            progressBar.X = ValueFactory.Create(p1);
            progressBar.Y = ValueFactory.Create(p2);
            progressBar.Width = ValueFactory.Create(p3);
            progressBar.Height = ValueFactory.Create(p4);
            return progressBar;
        }

        [ContextMethod("Надпись", "Label")]
        public TfLabel Label(string p1 = "Надпись", int p2 = 1, int p3 = 1, int p4 = 10, int p5 = 1)
        {
            TfLabel label = new TfLabel();
            label.Text = p1;
            label.X = ValueFactory.Create(p2);
            label.Y = ValueFactory.Create(p3);
            label.Width = ValueFactory.Create(p4);
            label.Height = ValueFactory.Create(p5);
            return label;
        }

        [ContextMethod("Флажок", "CheckBox")]
        public TfCheckBox CheckBox(string p1 = "Флажок", int p2 = 1, int p3 = 1)
        {
            TfCheckBox checkBox = new TfCheckBox();
            checkBox.Text = p1;
            checkBox.X = ValueFactory.Create(p2);
            checkBox.Y = ValueFactory.Create(p3);
            return checkBox;
        }

        [ContextMethod("Граница", "Border")]
        public TfBorder Border(IValue p1 = null)
        {
            TfBorder border = new TfBorder();
            if (Utils.AllNotNull(p1))
            {
                border.BorderStyle = Utils.ToInt32(p1);
            }
            return border;
        }

        [ContextMethod("Окно", "Window")]
        public TfWindow Window(string p1 = "Окно", int p2 = 1, int p3 = 1, int p4 = 10, int p5 = 5)
        {
            TfWindow window = new TfWindow();
            window.Title = p1;
            window.X = ValueFactory.Create(p2);
            window.Y = ValueFactory.Create(p3);
            window.Width = ValueFactory.Create(p4);
            window.Height = ValueFactory.Create(p5);
            return window;
        }

        [ContextMethod("Действие", "Action")]
        public TfAction Action(IRuntimeContextInstance script = null, string methodName = null, IValue param = null)
        {
            return new TfAction(script, methodName, param);
        }

        [ContextProperty("Верхний", "Top")]
        public TfToplevel Top
        {
            get { return top; }
        }

        [ContextMethod("Кнопка", "Button")]
        public TfButton Button(string p1 = "Кнопка", int p2 = 1, int p3 = 1, int p4 = 10, int p5 = 1)
        {
            TfButton button = new TfButton();
            button.Text = p1;
            button.Frame = new TfRect(p2, p3, p4, p5);
            return button;
        }

        public TfView View(IValue p1 = null, IValue p2 = null, IValue p3 = null)
        {
            if (Utils.AllNull(p1, p2, p3))
            {
                return new TfView();
            }
            else if (Utils.AllNotNull(p1) && Utils.AllNull(p2, p3))
            {
                if (Utils.IsType<TfRect>(p1))
                {
                    return new TfView((TfRect)p1);
                }
            }
            else if (Utils.AllNotNull(p1, p2, p3))
            {
                if (Utils.IsNumber(p1) && Utils.IsNumber(p2) && Utils.IsString(p3))
                {
                    return new TfView(Utils.ToInt32(p1), Utils.ToInt32(p2), Utils.ToString(p3));
                }
                else if (Utils.IsType<TfRect>(p1) && Utils.IsString(p2) && Utils.IsType<TfBorder>(p3))
                {
                    return new TfView((TfRect)p1, Utils.ToString(p2), (TfBorder)p3);
                }
                else if (Utils.IsString(p1) && Utils.IsNumber(p2) && Utils.IsType<TfBorder>(p3))
                {
                    return new TfView(Utils.ToString(p1), Utils.ToInt32(p2), (TfBorder)p3);
                }
            }
            return null;
        }

        [ContextMethod("Размер", "Size")]
        public TfSize Size(int p1 = 10, int p2 = 5)
        {
            TfSize size = new TfSize();
            size.Width = p1;
            size.Height = p2;
            return size;
        }

        [ContextMethod("Прямоугольник", "Rect")]
        public TfRect Rect(int p1 = 1, int p2 = 1, int p3 = 10, int p4 = 5)
        {
            TfRect rect = new TfRect();
            rect.X = p1;
            rect.Y = p2;
            rect.Width = p3;
            rect.Height = p4;
            return rect;
        }

        [ContextMethod("Точка", "Point")]
        public TfPoint Rect(IValue p1 = null, IValue p2 = null)
        {
            if (Utils.AllNull(p1, p2))
            {
                return new TfPoint();
            }
            else if (Utils.AllNotNull(p1, p2))
            {
                return new TfPoint(Utils.ToInt32(p1), Utils.ToInt32(p2));
            }
            return null;
        }

        [ContextMethod("Верхний", "Toplevel")]
        public TfToplevel Toplevel(int p1 = 1, int p2 = 1, int p3 = 10, int p4 = 5)
        {
            TfToplevel toplevel = new TfToplevel();
            toplevel.X = ValueFactory.Create(p1);
            toplevel.Y = ValueFactory.Create(p2);
            toplevel.Width = ValueFactory.Create(p3);
            toplevel.Height = ValueFactory.Create(p4);
            return toplevel;
        }

        // Вспомогательные методы и объекты.

        private void Application_NotifyNewRunState(Application.RunState obj)
        {
            OnOpen.Invoke();
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
                Utils.GlobalContext().Echo("Обработчик не выполнен: " + action.MethodName + Environment.NewLine + ex.StackTrace);
            }
            Event = null;
            try
            {
                Application.Refresh();
            }
            catch { }
        }

        [ContextMethod("РазобратьСтроку", "SplitString")]
        public ArrayImpl SplitString(string p1, string p2)
        {
            return Utils.SplitString(p1, p2);
        }

        [ContextProperty("НоваяСтрока", "NewLine")]
        public string NewLine
        {
            get { return Utils.NewLine; }
        }
    }
}
