using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library;
using Terminal.Gui;

namespace ostgui
{

    [ContextClass("ТфОкноСообщений", "TfMessageBox")]
    public class TfMessageBox : AutoContext<TfMessageBox>
    {

        public TfMessageBox()
        {
            buttons = new ArrayImpl();
            buttons.Add(ValueFactory.Create("Yes"));
            buttons.Add(ValueFactory.Create("No"));
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

        private string title = "ОкноСообщений";
        [ContextProperty("Заголовок", "Title")]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private int defaultButtonIndex = 0;
        [ContextProperty("ИндексКнопкиПоУмолчанию", "DefaultButtonIndex")]
        public int DefaultButtonIndex
        {
            get { return defaultButtonIndex; }
            set { defaultButtonIndex = value; }
        }

        private ArrayImpl buttons;
        [ContextProperty("Кнопки", "Buttons")]
        public ArrayImpl Buttons
        {
            get { return buttons; }
            set { buttons = value; }
        }

        private string message = "";
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

        [ContextMethod("Запрос", "Query")]
        public int Query()
        {
            int maxLengthButtons = 0;
            NStack.ustring[] _buttons = new NStack.ustring[Buttons.Count()];
            for (int i = 0; i < Buttons.Count(); i++)
            {
                string str = Buttons.Get(i).AsString();
                maxLengthButtons = maxLengthButtons + str.Length + 8;
                _buttons[i] = str;
            }

            TfTextFormatter TfTextFormatter1 = new TfTextFormatter();
            TfTextFormatter1.Text = Message;
            TfSize TfSize1 = TfTextFormatter1.GetAutoSize();
            int _height = TfSize1.Height + 1;
            int _widthMessage = TfSize1.Width;
            int _widthTitle = Title.Length + 5;
            int _widthTerminal = Application.Driver.Cols - 8;
            int _width = Math.Max(_widthMessage, Math.Max(_widthTitle, maxLengthButtons));
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
            return Terminal.Gui.MessageBox.Query(_width, _height, Title, Message, _buttons);
        }

    }
}
