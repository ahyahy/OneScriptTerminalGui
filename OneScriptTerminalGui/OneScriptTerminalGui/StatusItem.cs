using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace ostgui
{
    public class StatusItem
    {
        public TfStatusItem dll_obj;
        public Terminal.Gui.StatusItem M_StatusItem;
        Action Clicked;

        public StatusItem(Terminal.Gui.Key p1, string p2)
        {
            Clicked = delegate ()
            {
                if (dll_obj.Clicked != null)
                {
                    TfEventArgs TfEventArgs1 = new TfEventArgs();
                    TfEventArgs1.sender = dll_obj;
                    TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.Clicked);
                    OneScriptTerminalGui.Event = TfEventArgs1;
                    OneScriptTerminalGui.ExecuteEvent(dll_obj.Clicked);
                }
            };

            M_StatusItem = new Terminal.Gui.StatusItem(p1, p2, Clicked);
            OneScriptTerminalGui.AddToHashtable(M_StatusItem, this);
        }

        public string HotTextSpecifier
        {
            get { return M_StatusItem.HotTextSpecifier.ToString(); }
            set { M_StatusItem.HotTextSpecifier = value.ToCharArray()[0]; }
        }

        public string Title
        {
            get { return M_StatusItem.Title.ToString(); }
            set { M_StatusItem.Title = value; }
        }

        public object Data
        {
            get { return M_StatusItem.Data; }
            set { M_StatusItem.Data = value; }
        }

        public int Shortcut
        {
            get { return (int)M_StatusItem.Shortcut; }
        }

        public new string ToString()
        {
            return M_StatusItem.ToString();
        }
    }

    [ContextClass("ТфЭлементСтрокиСостояния", "TfStatusItem")]
    public class TfStatusItem : AutoContext<TfStatusItem>
    {

        public TfStatusItem(int p1, string p2)
        {
            StatusItem StatusItem1 = new StatusItem((Terminal.Gui.Key)p1, p2);
            StatusItem1.dll_obj = this;
            Base_obj = StatusItem1;
        }

        public StatusItem Base_obj;

        [ContextProperty("Данные", "Data")]
        public IValue Data
        {
            get { return OneScriptTerminalGui.RevertObj(Base_obj.Data); }
            set { Base_obj.Data = value; }
        }

        [ContextProperty("Заголовок", "Title")]
        public string Title
        {
            get { return Base_obj.Title; }
            set { Base_obj.Title = value; }
        }

        [ContextProperty("Нажатие", "Clicked")]
        public TfAction Clicked { get; set; }

    }
}
