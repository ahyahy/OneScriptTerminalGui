using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library;
using Terminal.Gui;

namespace ostgui
{

    [ContextClass("ТфУведомление", "TfBalloons")]
    public class TfBalloons : AutoContext<TfBalloons>
    {

        public TfBalloons()
        {
        }

        private bool autoSize = true;
        [ContextProperty("АвтоРазмер", "AutoSize")]
        public bool AutoSize
        {
            get { return autoSize; }
            set { autoSize = value; }
        }

        private int height = 4;
        [ContextProperty("Высота", "Height")]
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        private string title = "Уведомление";
        [ContextProperty("Заголовок", "Title")]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private TfPos y = new TfPos(new Pos(Terminal.Gui.Pos.Center()));
        [ContextProperty("Игрек", "Y")]
        public TfPos Y
        {
            get { return y; }
            set { y = value; }
        }

        private TfPos x = new TfPos(new Pos(Terminal.Gui.Pos.Center()));
        [ContextProperty("Икс", "X")]
        public TfPos X
        {
            get { return x; }
            set { x = value; }
        }

        private int interval = 3000;
        [ContextProperty("Интервал", "Interval")]
        public int Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        private string message = DateTime.Now.ToString();
        [ContextProperty("Сообщение", "Message")]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        private int width = 50;
        [ContextProperty("Ширина", "Width")]
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        [ContextMethod("Показать", "Show")]
        public void Show(string p1 = null, int p2 = 3000, string p3 = "Уведомление", TfPos p4 = null, TfPos p5 = null)
        {
            int _interval = this.Interval;
            string _text = this.Message;
            string _title = this.Title;
            TfPos _x = this.X;
            TfPos _y = this.Y;
            if (p1 != null)
            {
                _text = p1;
            }
            if (p2 != 3000)
            {
                _interval = p2;
            }
            if (p3 != "Уведомление")
            {
                _title = p3;
            }
            if (p4 != null)
            {
                _x = p4;
            }
            if (p5 != null)
            {
                _y = p5;
            }

            ArrayImpl buttons = new ArrayImpl();
            buttons.Add(ValueFactory.Create("OK"));

            int maxLengthButtons = 0;
            NStack.ustring[] _buttons = new NStack.ustring[buttons.Count()];
            for (int i = 0; i < buttons.Count(); i++)
            {
                string str = buttons.Get(i).AsString();
                maxLengthButtons = maxLengthButtons + str.Length + 8;
                _buttons[i] = str;
            }

            TfTextFormatter TfTextFormatter1 = new TfTextFormatter();
            TfTextFormatter1.Text = _text;
            TfSize TfSize1 = TfTextFormatter1.GetAutoSize();
            int _height = TfSize1.Height + 1;
            int _widthMessage = TfSize1.Width;
            int _widthTitle = Title.Length + 5;
            int _widthTerminal = Application.Driver.Cols - 8;
            int _width = Math.Max(_widthMessage, Math.Max(_widthTitle, maxLengthButtons)) + 3;
            if (_width >= _widthTerminal)
            {
                _width = _widthTerminal;
            }
            int _heightTerminal = Application.Driver.Rows - 8;
            if (_height >= _heightTerminal)
            {
                _height = _heightTerminal;
            }
            if (!AutoSize)
            {
                _width = Width;
                _height = Height;
            }

            var dialog = new Terminal.Gui.Dialog()
            {
                X = _x.Base_obj.M_Pos,
                Y = _y.Base_obj.M_Pos,
                Width = _width,
                Height = _height + 3,
                Title = _title,
            };

            Terminal.Gui.Rect rect = new Terminal.Gui.Rect(0, 0, dialog.Frame.Width - 2, dialog.Frame.Height - 3);
            Terminal.Gui.ScrollView scrollView = new Terminal.Gui.ScrollView(rect);
            scrollView.ColorScheme = dialog.ColorScheme;
            scrollView.ContentSize = new Terminal.Gui.Size(scrollView.Frame.Width - 1, dialog.Frame.Height);
            scrollView.ContentOffset = new Terminal.Gui.Point(0, 0);
            scrollView.ShowVerticalScrollIndicator = true;
            scrollView.ShowHorizontalScrollIndicator = false;

            // нужно рассчитать myHeight наверное через TextFormatter
            int myHeight = scrollView.Frame.Height + 5;

            Terminal.Gui.Rect rect2 = new Terminal.Gui.Rect(0, 0, scrollView.Frame.Width, myHeight);
            Terminal.Gui.TextView textView = new Terminal.Gui.TextView(rect2);
            textView.Text = _text;
            textView.WordWrap = true;
            textView.ColorScheme = dialog.ColorScheme;
            textView.ReadOnly = true;
            scrollView.Add(textView);

            dialog.Add(scrollView);

            Application.Top.Add(dialog);
            var defaultButton = new Terminal.Gui.Button("OK")
            {
                IsDefault = true,
            };
            defaultButton.Clicked += () => Application.Top.Remove(dialog);
            dialog.AddButton(defaultButton);

            defaultButton.SetFocus();

            TimeSpan timeSpan = TimeSpan.FromMilliseconds(p2);
            if (p2 == -1)
            {
                // Установим 20 лет.
                timeSpan = TimeSpan.FromDays(7300);
            }
            object token = Application.MainLoop.AddTimeout(timeSpan, (m) =>
            {
                Application.Top.Remove(dialog);
                return false;
            });
        }

    }
}
