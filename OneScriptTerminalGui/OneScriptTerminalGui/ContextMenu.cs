using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library.ValueList;
using System.Collections;

namespace ostgui
{
    public class ContextMenu
    {
        public TfContextMenu dll_obj;
        public Terminal.Gui.ContextMenu M_ContextMenu;

        public ContextMenu()
        {
            M_ContextMenu = new Terminal.Gui.ContextMenu();
            Utils.AddToHashtable(M_ContextMenu, this);
        }

        public ContextMenu(Terminal.Gui.ContextMenu p1)
        {
            M_ContextMenu = p1;
            Utils.AddToHashtable(M_ContextMenu, this);
        }

        public new string ToString()
        {
            return M_ContextMenu.ToString();
        }

        public int Key
        {
            get { return (int)M_ContextMenu.Key; }
            set { M_ContextMenu.Key = (Terminal.Gui.Key)value; }
        }

        public int MouseFlags
        {
            get { return (int)M_ContextMenu.MouseFlags; }
        }

        public bool UseSubMenusSingleFrame
        {
            get { return M_ContextMenu.UseSubMenusSingleFrame; }
            set { M_ContextMenu.UseSubMenusSingleFrame = value; }
        }

        public ostgui.MenuBar MenuBar
        {
            get { return Utils.RevertEqualsObj(M_ContextMenu.MenuBar); }
        }

        public ostgui.Point Position
        {
            get { return new Point(M_ContextMenu.Position); }
            set { M_ContextMenu.Position = value.M_Point; }
        }

        public bool ForceMinimumPosToZero
        {
            get { return M_ContextMenu.ForceMinimumPosToZero; }
            set { M_ContextMenu.ForceMinimumPosToZero = value; }
        }

        public void Show()
        {
            if (M_ContextMenu.UseSubMenusSingleFrame)
            {
                if (M_ContextMenu.MenuItems.Children.Length == 1)
                {
                    if (M_ContextMenu.MenuItems.Children[0].GetType() == typeof(Terminal.Gui.MenuBarItem))
                    {
                        Terminal.Gui.MenuBarItem menuBarItem = (Terminal.Gui.MenuBarItem)M_ContextMenu.MenuItems.Children[0];
                        if (menuBarItem.Children.Length > 0)
                        {
                            M_ContextMenu.Show();
                        }
                    }
                }
                else
                {
                    new TfBalloons().Show("Не правильное использование свойства КонтекстноеМеню.ОдинФреймДляПодменю (ContextMenu.UseSubMenusSingleFrame)", -1);
                }
            }
            else
            {
                M_ContextMenu.Show();
            }
        }

        public void Hide()
        {
            M_ContextMenu.Hide();
        }

        public ostgui.MenuBarItem MenuItems
        {
            get { return Utils.RevertEqualsObj(M_ContextMenu.MenuItems); }
            set { M_ContextMenu.MenuItems = value.M_MenuBarItem; }
        }

        public bool IsShow
        {
            get { return Terminal.Gui.ContextMenu.IsShow; }
        }
    }

    [ContextClass("ТфКонтекстноеМеню", "TfContextMenu")]
    public class TfContextMenu : AutoContext<TfContextMenu>
    {

        public TfContextMenu()
        {
            ContextMenu ContextMenu1 = new ContextMenu();
            ContextMenu1.dll_obj = this;
            Base_obj = ContextMenu1;
        }

        public TfContextMenu(Terminal.Gui.ContextMenu p1)
        {
            ContextMenu ContextMenu1 = new ContextMenu(p1);
            ContextMenu1.dll_obj = this;
            Base_obj = ContextMenu1;
        }

        public ContextMenu Base_obj;

        [ContextProperty("КнопкаМышиДляАктивации", "MouseFlags")]
        public int MouseFlags
        {
            get { return Base_obj.MouseFlags; }
        }

        [ContextProperty("ОдинФреймДляПодменю", "UseSubMenusSingleFrame")]
        public bool UseSubMenusSingleFrame
        {
            get { return Base_obj.UseSubMenusSingleFrame; }
            set { Base_obj.UseSubMenusSingleFrame = value; }
        }

        [ContextProperty("ПодМеню", "SubMenu")]
        public TfMenuBarItem SubMenu
        {
            get { return Base_obj.MenuItems.dll_obj; }
            set { Base_obj.MenuItems = value.Base_obj; }
        }

        [ContextProperty("Позиция", "Position")]
        public TfPoint Position
        {
            get { return new TfPoint(Base_obj.Position); }
            set { Base_obj.Position = value.Base_obj; }
        }

        [ContextProperty("Показано", "IsShow")]
        public bool IsShow
        {
            get { return Base_obj.IsShow; }
        }

        [ContextProperty("СочетаниеКлавишДействие", "ShortcutAction")]
        public TfAction ShortcutAction { get; set; }

        [ContextMethod("ДобавитьСочетаниеКлавиш", "AddShortcut")]
        public void AddShortcut(decimal p1)
        {
            Utils.AddToShortcutDictionary(p1, this);
        }

        [ContextMethod("Показать", "Show")]
        public void Show()
        {
            Base_obj.Show();
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

        [ContextMethod("Скрыть", "Hide")]
        public void Hide()
        {
            Base_obj.Hide();
        }

        [ContextMethod("УдалитьСочетаниеКлавиш", "RemoveShortcut")]
        public void RemoveShortcut(decimal p1)
        {
            Utils.RemoveFromShortcutDictionary(p1, this);
        }

    }
}
