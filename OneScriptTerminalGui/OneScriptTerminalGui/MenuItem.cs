﻿using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library.ValueList;
using System.Collections;

namespace ostgui
{
    public class MenuItem
    {
        public TfMenuItem dll_obj;
        public Terminal.Gui.MenuItem m_MenuItem;
        public System.Action Clicked;

        public MenuItem()
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

            M_MenuItem = new Terminal.Gui.MenuItem();
            M_MenuItem.Action = Clicked;
            OneScriptTerminalGui.AddToHashtable(M_MenuItem, this);
        }

        public Terminal.Gui.MenuItem M_MenuItem
        {
            get { return m_MenuItem; }
            set { m_MenuItem = value; }
        }

        public IValue Tag
        {
            get { return M_MenuItem.Tag; }
            set { M_MenuItem.Tag = value; }
        }

        public object Data
        {
            get { return M_MenuItem.Data; }
            set { M_MenuItem.Data = value; }
        }

        public string Title
        {
            get { return M_MenuItem.Title.ToString(); }
            set { M_MenuItem.Title = value; }
        }

        public string HotKey
        {
            get { return M_MenuItem.HotKey.ToString(); }
            set { M_MenuItem.HotKey = value.ToCharArray()[0]; }
        }

        public string Help
        {
            get { return M_MenuItem.Help.ToString(); }
            set { M_MenuItem.Help = value; }
        }

        public bool Checked
        {
            get { return M_MenuItem.Checked; }
            set { M_MenuItem.Checked = value; }
        }

        public int Shortcut
        {
            get { return (int)M_MenuItem.Shortcut; }
            set { M_MenuItem.Shortcut = (Terminal.Gui.Key)value; }
        }

        public IValue Parent
        {
            get { return OneScriptTerminalGui.RevertEqualsObj(M_MenuItem.Parent).dll_obj; }
        }

        public string ShortcutTag
        {
            get { return M_MenuItem.ShortcutTag.ToString(); }
        }

        public int CheckType
        {
            get { return (int)M_MenuItem.CheckType; }
            set { M_MenuItem.CheckType = (Terminal.Gui.MenuItemCheckStyle)value; }
        }

        public new string ToString()
        {
            return M_MenuItem.ToString();
        }
    }

    [ContextClass("ТфЭлементМеню", "TfMenuItem")]
    public class TfMenuItem : AutoContext<TfMenuItem>
    {

        public TfMenuItem()
        {
            MenuItem MenuItem1 = new MenuItem();
            MenuItem1.dll_obj = this;
            Base_obj = MenuItem1;
        }

        public MenuItem Base_obj;

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

        [ContextProperty("КлавишаВызова", "HotKey")]
        public string HotKey
        {
            get { return Base_obj.HotKey; }
        }

        [ContextProperty("Метка", "Tag")]
        public IValue Tag
        {
            get { return Base_obj.Tag; }
            set { Base_obj.Tag = value; }
        }

        [ContextProperty("Подсказка", "Help")]
        public string Help
        {
            get { return Base_obj.Help; }
            set { Base_obj.Help = value; }
        }

        [ContextProperty("Помечен", "Checked")]
        public bool Checked
        {
            get { return Base_obj.Checked; }
            set { Base_obj.Checked = value; }
        }

        [ContextProperty("СтильФлажка", "CheckType")]
        public int CheckType
        {
            get { return Base_obj.CheckType; }
            set { Base_obj.CheckType = value; }
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
