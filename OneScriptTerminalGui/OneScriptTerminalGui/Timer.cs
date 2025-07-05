using System;
using ScriptEngine.Machine.Contexts;
using Terminal.Gui;

namespace ostgui
{

    [ContextClass("ТфТаймер", "TfTimer")]
    public class TfTimer : AutoContext<TfTimer>
    {

        private object token;

        public TfTimer()
        {
        }

        private int interval = 0;
        [ContextProperty("Интервал", "Interval")]
        public int Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        [ContextProperty("ПриСрабатыванииТаймера", "Tick")]
        public TfAction Tick { get; set; }

        [ContextMethod("Начать", "Start")]
        public void Start()
        {
            token = Application.MainLoop.AddTimeout(TimeSpan.FromMilliseconds(Interval), (m) =>
            {
                if (Tick != null)
                {
                    TfEventArgs TfEventArgs1 = new TfEventArgs();
                    TfEventArgs1.sender = this;
                    TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Tick);
                    OneScriptTerminalGui.Event = TfEventArgs1;
                    OneScriptTerminalGui.ExecuteEvent(Tick);
                }
                return true;
            });
        }

        [ContextMethod("Остановить", "Stop")]
        public void Stop()
        {
            Application.MainLoop.RemoveTimeout(token);
        }

    }
}
