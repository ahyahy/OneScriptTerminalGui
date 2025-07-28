using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library.ValueList;
using System.Collections;

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

        public IValue Tag
        {
            get { return M_StatusItem.Tag; }
            set { M_StatusItem.Tag = value; }
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

        public new string ToString()
        {
            return M_StatusItem.ToString();
        }
    }

    [ContextClass("ТфЭлементСтрокиСостояния", "TfStatusItem")]
    public class TfStatusItem : AutoContext<TfStatusItem>
    {

        public Terminal.Gui.StatusBar M_StatusBar { get; set; }

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

        [ContextProperty("Метка", "Tag")]
        public IValue Tag
        {
            get { return Base_obj.Tag; }
            set { Base_obj.Tag = value; }
        }

        [ContextProperty("Нажатие", "Clicked")]
        public TfAction Clicked { get; set; }

        [ContextProperty("СочетаниеКлавишДействие", "ShortcutAction")]
        public TfAction ShortcutAction { get; set; }

        [ContextMethod("ДобавитьСочетаниеКлавиш", "AddShortcut")]
        public void AddShortcut(decimal p1)
        {
            OneScriptTerminalGui.AddToShortcutDictionary(p1, this);
        }

        [ContextMethod("ПолучитьСочетаниеКлавиш", "GetShortcut")]
        public ValueListImpl GetShortcut()
        {
            ValueListImpl ValueListImpl1 = new ValueListImpl();
            ArrayList ArrayList1 = OneScriptTerminalGui.GetFromShortcutDictionary(this);
            for (int i = 0; i < ArrayList1.Count; i++)
            {
                decimal shortcut = (decimal)ArrayList1[i];
                ValueListImpl1.Add(ValueFactory.Create(shortcut), OneScriptTerminalGui.instance.Keys.ToStringRu(shortcut));
            }
            if (ValueListImpl1.Count() > 0)
            {
                return ValueListImpl1;
            }
            return null;
        }

        [ContextMethod("УдалитьСочетаниеКлавиш", "RemoveShortcut")]
        public void RemoveShortcut(decimal p1)
        {
            OneScriptTerminalGui.RemoveFromShortcutDictionary(p1, this);
        }

    }
}
