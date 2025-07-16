﻿using System;
using ScriptEngine.Machine.Contexts;
using Terminal.Gui;

namespace ostgui
{

    [ContextClass("ТфТаймер", "TfTimer")]
    public class TfTimer : AutoContext<TfTimer>
    {

        private object token;
        private bool stop = false;

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
            stop = false;
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
                if (stop)
                {
                    return false;
                }
                return true;
            });
        }

        [ContextMethod("Остановить", "Stop")]
        public void Stop()
        {
            stop = true;
            // token для остановки не срабатывает, поэтому добавлено поле stop.
            Application.MainLoop.RemoveTimeout(token);
        }

    }
}
