using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library;

namespace ostgui
{

    [ContextClass("ТфАргументыСобытия", "TfEventArgs")]
    public class TfEventArgs : AutoContext<TfEventArgs>
    {

        public TfEventArgs()
        {
        }

        public IValue oldYear = null;
        [ContextProperty("БылоГод", "OldYear")]
        public decimal OldYear
        {
            get { return Utils.ToDecimal(oldYear); }
        }

        public IValue oldDay = null;
        [ContextProperty("БылоДень", "OldDay")]
        public decimal OldDay
        {
            get { return Utils.ToDecimal(oldDay); }
        }

        public IValue oldMonth = null;
        [ContextProperty("БылоМесяц", "OldMonth")]
        public decimal OldMonth
        {
            get { return Utils.ToDecimal(oldMonth); }
        }

        public IValue oldMinutes = null;
        [ContextProperty("БылоМинут", "OldMinutes")]
        public decimal OldMinutes
        {
            get { return Utils.ToDecimal(oldMinutes); }
        }

        public IValue oldSeconds = null;
        [ContextProperty("БылоСекунд", "OldSeconds")]
        public decimal OldSeconds
        {
            get { return Utils.ToDecimal(oldSeconds); }
        }

        public IValue oldTicks = null;
        [ContextProperty("БылоТактов", "OldTicks")]
        public decimal OldTicks
        {
            get { return Utils.ToDecimal(oldTicks); }
        }

        public IValue oldHours = null;
        [ContextProperty("БылоЧасов", "OldHours")]
        public decimal OldHours
        {
            get { return Utils.ToDecimal(oldHours); }
        }

        public IValue tab = null;
        [ContextProperty("Вкладка", "Tab")]
        public TfTabPage Tab
        {
            get { return (TfTabPage)tab; }
        }

        public TfTreeView treeView = null;
        [ContextProperty("Дерево", "TreeView")]
        public TfTreeView TreeView
        {
            get { return treeView; }
        }

        public IValue valueProp = null;
        [ContextProperty("Значение", "Value")]
        public IValue ValueProp
        {
            get { return valueProp; }
        }

        public IValue y = null;
        [ContextProperty("Игрек", "Y")]
        public int Y
        {
            get { return Utils.ToInt32(y); }
        }

        public IValue x = null;
        [ContextProperty("Икс", "X")]
        public int X
        {
            get { return Utils.ToInt32(x); }
        }

        public IValue selectedItem = null;
        [ContextProperty("ИндексВыбранного", "SelectedItem")]
        public int SelectedItem
        {
            get { return Utils.ToInt32(selectedItem); }
        }

        public IValue columnIndex = null;
        [ContextProperty("ИндексКолонки", "ColumnIndex")]
        public int ColumnIndex
        {
            get { return Utils.ToInt32(columnIndex); }
        }

        public IValue previousSelectedItem = null;
        [ContextProperty("ИндексПредыдущегоВыбранного", "PreviousSelectedItem")]
        public int PreviousSelectedItem
        {
            get { return Utils.ToInt32(previousSelectedItem); }
        }

        public IValue rowIndex = null;
        [ContextProperty("ИндексСтроки", "RowIndex")]
        public int RowIndex
        {
            get { return Utils.ToInt32(rowIndex); }
        }

        public IValue keyValue = null;
        [ContextProperty("Клавиша", "KeyValue")]
        public int KeyValue
        {
            get { return Utils.ToInt32(keyValue); }
        }

        public IValue keyToString = null;
        [ContextProperty("КлавишаСтрокой", "KeyToString")]
        public string KeyToString
        {
            get { return keyToString.AsString(); }
        }

        public IValue newTab = null;
        [ContextProperty("НоваяВкладка", "NewTab")]
        public TfTabPage NewTab
        {
            get { return (TfTabPage)newTab; }
        }

        public IValue newCol = null;
        [ContextProperty("НоваяКолонка", "NewCol")]
        public int NewCol
        {
            get { return Utils.ToInt32(newCol); }
        }

        public IValue newRow = null;
        [ContextProperty("НоваяСтрока", "NewRow")]
        public int NewRow
        {
            get { return Utils.ToInt32(newRow); }
        }

        public IValue newMenuBarItem = null;
        [ContextProperty("НовыйПунктМеню", "NewMenuBarItem")]
        public IValue NewMenuBarItem
        {
            get { return newMenuBarItem; }
            set { newMenuBarItem = value; }
        }

        public IValue newText = null;
        [ContextProperty("НовыйТекст", "NewText")]
        public string NewText
        {
            get { return Utils.ToString(newText); }
        }

        public TfTreeNode newNode = null;
        [ContextProperty("НовыйУзел", "NewNode")]
        public TfTreeNode NewNode
        {
            get { return newNode; }
        }

        public IValue cancel = null;
        [ContextProperty("Отмена", "Cancel")]
        public bool Cancel
        {
            get { return cancel.AsBoolean(); }
            set { cancel = ValueFactory.Create(value); }
        }

        public IValue sender = null;
        [ContextProperty("Отправитель", "Sender")]
        public IValue Sender
        {
            get { return sender; }
        }

        public IValue parameter = null;
        [ContextProperty("Параметр", "Parameter")]
        public IValue Parameter
        {
            get { return parameter; }
        }

        public IValue directoryPath = null;
        [ContextProperty("ПутьКаталога", "DirectoryPath")]
        public string DirectoryPath
        {
            get { return directoryPath.AsString(); }
        }

        public IValue filePath = null;
        [ContextProperty("ПутьФайла", "FilePath")]
        public string FilePath
        {
            get { return filePath.AsString(); }
        }

        public TfSize size = null;
        [ContextProperty("Размер", "Size")]
        public TfSize Size
        {
            get { return size; }
        }

        public IValue dialogResult = null;
        [ContextProperty("РезультатДиалога", "DialogResult")]
        public decimal DialogResult
        {
            get { return Utils.ToDecimal(dialogResult); }
        }

        public IValue newYear = null;
        [ContextProperty("СталоГод", "NewYear")]
        public decimal NewYear
        {
            get { return Utils.ToDecimal(newYear); }
        }

        public IValue newDay = null;
        [ContextProperty("СталоДень", "NewDay")]
        public decimal NewDay
        {
            get { return Utils.ToDecimal(newDay); }
        }

        public IValue newMonth = null;
        [ContextProperty("СталоМесяц", "NewMonth")]
        public decimal NewMonth
        {
            get { return Utils.ToDecimal(newMonth); }
        }

        public IValue newMinutes = null;
        [ContextProperty("СталоМинут", "NewMinutes")]
        public decimal NewMinutes
        {
            get { return Utils.ToDecimal(newMinutes); }
        }

        public IValue newSeconds = null;
        [ContextProperty("СталоСекунд", "NewSeconds")]
        public decimal NewSeconds
        {
            get { return Utils.ToDecimal(newSeconds); }
        }

        public IValue newTicks = null;
        [ContextProperty("СталоТактов", "NewTicks")]
        public decimal NewTicks
        {
            get { return Utils.ToDecimal(newTicks); }
        }

        public IValue newHours = null;
        [ContextProperty("СталоЧасов", "NewHours")]
        public decimal NewHours
        {
            get { return Utils.ToDecimal(newHours); }
        }

        public IValue oldTab = null;
        [ContextProperty("СтараяВкладка", "OldTab")]
        public TfTabPage OldTab
        {
            get { return (TfTabPage)oldTab; }
        }

        public IValue oldCol = null;
        [ContextProperty("СтараяКолонка", "OldCol")]
        public int OldCol
        {
            get { return Utils.ToInt32(oldCol); }
        }

        public IValue oldRow = null;
        [ContextProperty("СтараяСтрока", "OldRow")]
        public int OldRow
        {
            get { return Utils.ToInt32(oldRow); }
        }

        public IValue oldToggled = null;
        [ContextProperty("СтарыйСтатус", "OldToggled")]
        public bool OldToggled
        {
            get { return oldToggled.AsBoolean(); }
            set { oldToggled = ValueFactory.Create(value); }
        }

        public IValue oldText = null;
        [ContextProperty("СтарыйТекст", "OldText")]
        public string OldText
        {
            get { return Utils.ToString(oldText); }
        }

        public TfTreeNode oldNode = null;
        [ContextProperty("СтарыйУзел", "OldNode")]
        public TfTreeNode OldNode
        {
            get { return oldNode; }
        }

        public TfDataTable dataTable = null;
        [ContextProperty("ТаблицаДанных", "DataTable")]
        public TfDataTable DataTable
        {
            get { return dataTable; }
        }

        public IValue currentMenu = null;
        [ContextProperty("ТекущийПунктМеню", "CurrentMenu")]
        public IValue CurrentMenu
        {
            get { return currentMenu; }
        }

        public TfTreeNode treeNode = null;
        [ContextProperty("УзелДерева", "TreeNode")]
        public TfTreeNode TreeNode
        {
            get { return treeNode; }
        }

        public ArrayImpl filePaths = null;
        [ContextProperty("Файлы", "FilePaths")]
        public ArrayImpl FilePaths
        {
            get { return filePaths; }
        }

        public IValue flags = null;
        [ContextProperty("ФлагиМыши", "Flags")]
        public int Flags
        {
            get { return Utils.ToInt32(flags); }
        }

        public IValue timeFormat = null;
        [ContextProperty("ФорматВремени", "TimeFormat")]
        public string TimeFormat
        {
            get { return Utils.ToString(timeFormat); }
        }

        public IValue dateFormat = null;
        [ContextProperty("ФорматДаты", "DateFormat")]
        public string DateFormat
        {
            get { return Utils.ToString(dateFormat); }
        }

        public IValue view = null;
        [ContextProperty("Элемент", "View")]
        public IValue View
        {
            get { return view; }
        }

        public IValue menuItem = null;
        [ContextProperty("ЭлементМеню", "MenuItem")]
        public IValue MenuItem
        {
            get { return menuItem; }
        }

        public IValue isAlt = null;
        [ContextProperty("ЭтоAlt", "IsAlt")]
        public bool IsAlt
        {
            get { return isAlt.AsBoolean(); }
        }

        public IValue isCapslock = null;
        [ContextProperty("ЭтоCapslock", "IsCapslock")]
        public bool IsCapslock
        {
            get { return isCapslock.AsBoolean(); }
        }

        public IValue isCtrl = null;
        [ContextProperty("ЭтоCtrl", "IsCtrl")]
        public bool IsCtrl
        {
            get { return isCtrl.AsBoolean(); }
        }

        public IValue isNumlock = null;
        [ContextProperty("ЭтоNumlock", "IsNumlock")]
        public bool IsNumlock
        {
            get { return isNumlock.AsBoolean(); }
        }

        public IValue isScrolllock = null;
        [ContextProperty("ЭтоScrolllock", "IsScrolllock")]
        public bool IsScrolllock
        {
            get { return isScrolllock.AsBoolean(); }
        }

        public IValue isShift = null;
        [ContextProperty("ЭтоShift", "IsShift")]
        public bool IsShift
        {
            get { return isShift.AsBoolean(); }
        }

    }
}
