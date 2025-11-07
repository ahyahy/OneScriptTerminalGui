using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library.ValueList;
using System.Collections;
using System.Collections.Generic;

namespace ostgui
{
    public class TextView : View
    {
        public new TfTextView dll_obj;
        public Terminal.Gui.TextView M_TextView;

        public TextView()
        {
            M_TextView = new Terminal.Gui.TextView();
            base.M_View = M_TextView;
            Utils.AddToHashtable(M_TextView, this);

            M_TextView.ContentsChanged += M_TextView_ContentsChanged;
            M_TextView.TextChanged += M_TextView_TextChanged;
        }

        private void M_TextView_TextChanged()
        {
            if (dll_obj.TextChanged != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.TextChanged);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(dll_obj.TextChanged);
            }
        }

        private void M_TextView_ContentsChanged(Terminal.Gui.TextView.ContentsChangedEventArgs obj)
        {
            if (dll_obj.ContentsChanged != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.ContentsChanged);
                TfEventArgs1.columnIndex = ValueFactory.Create(obj.Col);
                TfEventArgs1.rowIndex = ValueFactory.Create(obj.Row);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(dll_obj.ContentsChanged);
            }
        }

        public new string ToString()
        {
            return M_TextView.ToString();
        }

        public new Toplevel GetTopSuperView()
        {
            return Utils.RevertEqualsObj(M_TextView.GetTopSuperView());
        }

        public int TopRow
        {
            get { return M_TextView.TopRow; }
            set { M_TextView.TopRow = value; }
        }

        public bool Selecting
        {
            get { return M_TextView.Selecting; }
            set { M_TextView.Selecting = value; }
        }

        public string SelectedText
        {
            get { return M_TextView.SelectedText.ToString(); }
        }

        public int SelectedLength
        {
            get { return M_TextView.SelectedLength; }
        }

        public bool Used
        {
            get { return M_TextView.Used; }
            set { M_TextView.Used = value; }
        }

        public bool IsDirty
        {
            get { return M_TextView.IsDirty; }
        }

        public bool HasHistoryChanges
        {
            get { return M_TextView.HasHistoryChanges; }
        }

        public int Lines
        {
            get { return M_TextView.Lines; }
        }

        public int SelectionStartColumn
        {
            get { return M_TextView.SelectionStartColumn; }
            set { M_TextView.SelectionStartColumn = value; }
        }

        public decimal DesiredCursorVisibility
        {
            get { return (decimal)M_TextView.DesiredCursorVisibility; }
            set { M_TextView.DesiredCursorVisibility = (Terminal.Gui.CursorVisibility)value; }
        }

        public int LeftColumn
        {
            get { return M_TextView.LeftColumn; }
            set { M_TextView.LeftColumn = value; }
        }

        public int Maxlength
        {
            get { return M_TextView.Maxlength; }
        }

        public bool Multiline
        {
            get { return M_TextView.Multiline; }
            set { M_TextView.Multiline = value; }
        }

        public bool WordWrap
        {
            get { return M_TextView.WordWrap; }
            set { M_TextView.WordWrap = value; }
        }

        public bool AllowsReturn
        {
            get { return M_TextView.AllowsReturn; }
            set { M_TextView.AllowsReturn = value; }
        }

        public bool AllowsTab
        {
            get { return M_TextView.AllowsTab; }
            set { M_TextView.AllowsTab = value; }
        }

        public int BottomOffset
        {
            get { return M_TextView.BottomOffset; }
            set { M_TextView.BottomOffset = value; }
        }

        public int SelectionStartRow
        {
            get { return M_TextView.SelectionStartRow; }
            set { M_TextView.SelectionStartRow = value; }
        }

        public int CurrentColumn
        {
            get { return M_TextView.CurrentColumn; }
        }

        public int CurrentRow
        {
            get { return M_TextView.CurrentRow; }
        }

        public bool ReadOnly
        {
            get { return M_TextView.ReadOnly; }
            set { M_TextView.ReadOnly = value; }
        }

        public int TabWidth
        {
            get { return M_TextView.TabWidth; }
            set { M_TextView.TabWidth = value; }
        }

        public void Paste()
        {
            M_TextView.Paste();
        }

        public void InsertText(string p1)
        {
            M_TextView.InsertText(p1);
        }

        public void SelectAll()
        {
            M_TextView.SelectAll();
        }

        public void Cut()
        {
            M_TextView.Cut();
        }

        public void LoadStream(System.IO.Stream p1)
        {
            M_TextView.LoadStream(p1);
        }

        public bool CloseFile()
        {
            return M_TextView.CloseFile();
        }

        public void Copy()
        {
            M_TextView.Copy();
        }

        public void ClearHistoryChanges()
        {
            M_TextView.ClearHistoryChanges();
        }

        public void MoveEnd()
        {
            M_TextView.MoveEnd();
        }

        public void MoveHome()
        {
            M_TextView.MoveHome();
        }

        public void ScrollTo(int p1, bool p2 = true)
        {
            M_TextView.ScrollTo(p1, p2);
        }

        public string GetCurrentLine()
        {
            List<Rune> list = M_TextView.GetCurrentLine();
            string str = "";
            for (int i = 0; i < list.Count; i++)
            {
                str += list[i].ToString();
            }
            return str;
        }

        public void DeleteCharLeft()
        {
            M_TextView.DeleteCharLeft();
        }

        public void DeleteCharRight()
        {
            M_TextView.DeleteCharRight();
        }

        public void RemoveAllText()
        {
            M_TextView.DeleteAll();
        }

        public Point CursorPosition
        {
            get { return new Point(M_TextView.CursorPosition); }
            set { M_TextView.CursorPosition = new Terminal.Gui.Point(value.X, value.Y); }
        }
    }

    [ContextClass("ТфТекстовый", "TfTextView")]
    public class TfTextView : AutoContext<TfTextView>
    {

        public TfTextView()
        {
            TextView TextView1 = new TextView();
            TextView1.dll_obj = this;
            Base_obj = TextView1;
        }

        public TfBorder Border
        {
            get { return Base_obj.Border.dll_obj; }
            set { Base_obj.Border = value.Base_obj; }
        }

        public TextView Base_obj;

        [ContextProperty("ВертикальноеВыравниваниеТекста", "VerticalTextAlignment")]
        public int VerticalTextAlignment
        {
            get { return Base_obj.VerticalTextAlignment; }
            set { Base_obj.VerticalTextAlignment = value; }
        }

        [ContextProperty("Верх", "Top")]
        public TfPos Top
        {
            get { return new TfPos(Base_obj.Top); }
        }

        [ContextProperty("ВерхняяСтрока", "TopRow")]
        public int TopRow
        {
            get { return Base_obj.TopRow; }
            set { Base_obj.TopRow = value; }
        }

        [ContextProperty("Выборка", "Selecting")]
        public bool Selecting
        {
            get { return Base_obj.Selecting; }
            set { Base_obj.Selecting = value; }
        }

        [ContextProperty("ВыделенныйТекст", "SelectedText")]
        public string SelectedText
        {
            get { return Base_obj.SelectedText; }
        }

        [ContextProperty("ВыравниваниеТекста", "TextAlignment")]
        public int TextAlignment
        {
            get { return Base_obj.TextAlignment; }
            set { Base_obj.TextAlignment = value; }
        }

        [ContextProperty("Высота", "Height")]
        public IValue Height
        {
            get { return new TfDim().Height(this); }
            set
            {
                if (Utils.IsType<TfDim>(value))
                {
                    Base_obj.M_View.Height = ((TfDim)value).Base_obj.M_Dim;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_View.Height = Terminal.Gui.Dim.Sized(Utils.ToInt32(value));
                }
            }
        }

        [ContextProperty("Границы", "Bounds")]
        public TfRect Bounds
        {
            get
            {
                Terminal.Gui.Rect bounds = Base_obj.Bounds.M_Rect;
                return new TfRect(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            }
        }

        [ContextProperty("Данные", "Data")]
        public IValue Data
        {
            get { return Utils.RevertObj(Base_obj.Data); }
            set { Base_obj.Data = value; }
        }

        [ContextProperty("ДлинаВыделенного", "SelectedLength")]
        public int SelectedLength
        {
            get { return Base_obj.SelectedLength; }
        }

        [ContextProperty("Добавлено", "IsAdded")]
        public bool IsAdded
        {
            get { return Base_obj.IsAdded; }
        }

        [ContextProperty("Добавлять", "Used")]
        public bool Used
        {
            get { return Base_obj.Used; }
            set { Base_obj.Used = value; }
        }

        [ContextProperty("Доступность", "Enabled")]
        public bool Enabled
        {
            get { return Base_obj.Enabled; }
            set { Base_obj.Enabled = value; }
        }

        [ContextProperty("ЕстьИзменения", "IsDirty")]
        public bool IsDirty
        {
            get { return Base_obj.IsDirty; }
        }

        [ContextProperty("ЕстьИстория", "HasHistoryChanges")]
        public bool HasHistoryChanges
        {
            get { return Base_obj.HasHistoryChanges; }
        }

        [ContextProperty("Игрек", "Y")]
        public IValue Y
        {
            get { return new TfPos(Base_obj.Y); }
            set
            {
                if (Utils.IsType<TfPos>(value))
                {
                    Base_obj.M_TextView.Y = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_TextView.Y = Terminal.Gui.Pos.At(Utils.ToInt32(value));
                }
            }
        }

        [ContextProperty("Идентификатор", "Id")]
        public string Id
        {
            get { return Base_obj.Id; }
            set { Base_obj.Id = value; }
        }

        [ContextProperty("Икс", "X")]
        public IValue X
        {
            get { return new TfPos(Base_obj.X); }
            set
            {
                if (Utils.IsType<TfPos>(value))
                {
                    Base_obj.M_TextView.X = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_TextView.X = Terminal.Gui.Pos.At(Utils.ToInt32(value));
                }
            }
        }

        [ContextProperty("Кадр", "Frame")]
        public TfRect Frame
        {
            get
            {
                Terminal.Gui.Rect frame = Base_obj.Frame.M_Rect;
                return new TfRect(frame.X, frame.Y, frame.Width, frame.Height);
            }
            set { Base_obj.Frame = value.Base_obj; }
        }

        [ContextProperty("КоличествоСтрок", "Lines")]
        public int Lines
        {
            get { return Base_obj.Lines; }
        }

        [ContextProperty("КолонкаНачалаВыделения", "SelectionStartColumn")]
        public int SelectionStartColumn
        {
            get { return Base_obj.SelectionStartColumn; }
            set { Base_obj.SelectionStartColumn = value; }
        }

        [ContextProperty("КонтекстноеМеню", "ContextMenu")]
        public TfContextMenu ContextMenu
        {
            get { return new TfContextMenu(Base_obj.M_TextView.ContextMenu); }
        }

        [ContextProperty("Курсор", "DesiredCursorVisibility")]
        public decimal DesiredCursorVisibility
        {
            get { return Base_obj.DesiredCursorVisibility; }
            set { Base_obj.DesiredCursorVisibility = value; }
        }

        [ContextProperty("ЛеваяКолонка", "LeftColumn")]
        public int LeftColumn
        {
            get { return Base_obj.LeftColumn; }
            set { Base_obj.LeftColumn = value; }
        }

        [ContextProperty("Лево", "Left")]
        public TfPos Left
        {
            get { return new TfPos(Base_obj.Left); }
        }

        [ContextProperty("МаксимальнаяДлина", "Maxlength")]
        public int Maxlength
        {
            get { return Base_obj.Maxlength; }
        }

        [ContextProperty("Метка", "Tag")]
        public IValue Tag
        {
            get { return Base_obj.Tag; }
            set { Base_obj.Tag = value; }
        }

        [ContextProperty("Многострочный", "Multiline")]
        public bool Multiline
        {
            get { return Base_obj.Multiline; }
            set { Base_obj.Multiline = value; }
        }

        [ContextProperty("Низ", "Bottom")]
        public TfPos Bottom
        {
            get { return new TfPos(Base_obj.Bottom); }
        }

        [ContextProperty("Отображать", "Visible")]
        public bool Visible
        {
            get { return Base_obj.Visible; }
            set { Base_obj.Visible = value; }
        }

        [ContextProperty("Перенос", "WordWrap")]
        public bool WordWrap
        {
            get { return Base_obj.WordWrap; }
            set { Base_obj.WordWrap = value; }
        }

        [ContextProperty("ПозицияКурсора", "CursorPosition")]
        public TfPoint CursorPosition
        {
            get { return new TfPoint(Base_obj.CursorPosition); }
            set { Base_obj.CursorPosition = value.Base_obj; }
        }

        [ContextProperty("ПорядокОбхода", "TabIndex")]
        public int TabIndex
        {
            get { return Base_obj.TabIndex; }
            set { Base_obj.TabIndex = value; }
        }

        [ContextProperty("Право", "Right")]
        public TfPos Right
        {
            get { return new TfPos(Base_obj.Right); }
        }

        [ContextProperty("ПринятиеВозврат", "AllowsReturn")]
        public bool AllowsReturn
        {
            get { return Base_obj.AllowsReturn; }
            set { Base_obj.AllowsReturn = value; }
        }

        [ContextProperty("ПринятиеТаб", "AllowsTab")]
        public bool AllowsTab
        {
            get { return Base_obj.AllowsTab; }
            set { Base_obj.AllowsTab = value; }
        }

        [ContextProperty("Родитель", "SuperView")]
        public IValue SuperView
        {
            get
            {
                if (Base_obj.M_TextView.SuperView.GetType().ToString().Contains("+ContentView"))
                {
                    return Utils.RevertEqualsObj(Base_obj.M_TextView.SuperView.SuperView).dll_obj;
                }
                return Utils.RevertEqualsObj(Base_obj.M_TextView.SuperView).dll_obj;
            }
        }

        [ContextProperty("СмещениеВниз", "BottomOffset")]
        public int BottomOffset
        {
            get { return Base_obj.BottomOffset; }
            set { Base_obj.BottomOffset = value; }
        }

        [ContextProperty("СтильКомпоновки", "LayoutStyle")]
        public int LayoutStyle
        {
            get { return Base_obj.LayoutStyle; }
            set { Base_obj.LayoutStyle = value; }
        }

        [ContextProperty("СтрокаНачалаВыделения", "SelectionStartRow")]
        public int SelectionStartRow
        {
            get { return Base_obj.SelectionStartRow; }
            set { Base_obj.SelectionStartRow = value; }
        }

        [ContextProperty("Сфокусирован", "HasFocus")]
        public bool HasFocus
        {
            get { return Base_obj.HasFocus; }
        }

        [ContextProperty("ТабФокус", "TabStop")]
        public bool TabStop
        {
            get { return Base_obj.TabStop; }
            set { Base_obj.TabStop = value; }
        }

        [ContextProperty("Текст", "Text")]
        public string Text
        {
            get { return Base_obj.Text; }
            set { Base_obj.Text = value; }
        }

        [ContextProperty("ТекущаяКолонка", "CurrentColumn")]
        public int CurrentColumn
        {
            get { return Base_obj.CurrentColumn; }
        }

        [ContextProperty("ТекущаяСтрока", "CurrentRow")]
        public int CurrentRow
        {
            get { return Base_obj.CurrentRow; }
        }

        [ContextProperty("ТекущийСверху", "IsCurrentTop")]
        public bool IsCurrentTop
        {
            get { return Base_obj.IsCurrentTop; }
        }

        [ContextProperty("ТолькоЧтение", "ReadOnly")]
        public bool ReadOnly
        {
            get { return Base_obj.ReadOnly; }
            set { Base_obj.ReadOnly = value; }
        }

        [ContextProperty("Фокусируемый", "CanFocus")]
        public bool CanFocus
        {
            get { return Base_obj.CanFocus; }
            set { Base_obj.CanFocus = value; }
        }

        private TfColorScheme colorScheme;
        [ContextProperty("ЦветоваяСхема", "ColorScheme")]
        public TfColorScheme ColorScheme
        {
            get { return colorScheme; }
            set
            {
                colorScheme = new TfColorScheme();
                Terminal.Gui.ColorScheme _colorScheme = value.Base_obj.M_ColorScheme;
                colorScheme.Base_obj.M_ColorScheme.Disabled = _colorScheme.Disabled;
                colorScheme.Base_obj.M_ColorScheme.Focus = _colorScheme.Focus;
                colorScheme.Base_obj.M_ColorScheme.HotFocus = _colorScheme.HotFocus;
                colorScheme.Base_obj.M_ColorScheme.HotNormal = _colorScheme.HotNormal;
                colorScheme.Base_obj.M_ColorScheme.Normal = _colorScheme.Normal;
                Base_obj.ColorScheme = colorScheme.Base_obj;
            }
        }

        [ContextProperty("Ширина", "Width")]
        public IValue Width
        {
            get { return new TfDim().Width(this); }
            set
            {
                if (Utils.IsType<TfDim>(value))
                {
                    Base_obj.M_View.Width = ((TfDim)value).Base_obj.M_Dim;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_View.Width = Terminal.Gui.Dim.Sized(Utils.ToInt32(value));
                }
            }
        }

        [ContextProperty("ШиринаТаб", "TabWidth")]
        public int TabWidth
        {
            get { return Base_obj.TabWidth; }
            set { Base_obj.TabWidth = value; }
        }

        [ContextProperty("КлавишаНажата", "KeyPress")]
        public TfAction KeyPress { get; set; }

        [ContextProperty("МышьНадЭлементом", "MouseEnter")]
        public TfAction MouseEnter { get; set; }

        [ContextProperty("МышьПокинулаЭлемент", "MouseLeave")]
        public TfAction MouseLeave { get; set; }

        [ContextProperty("ПриВходе", "Enter")]
        public TfAction Enter { get; set; }

        [ContextProperty("ПриНажатииМыши", "MouseClick")]
        public TfAction MouseClick { get; set; }

        [ContextProperty("ПриУходе", "Leave")]
        public TfAction Leave { get; set; }

        [ContextProperty("СодержимоеИзменено", "ContentsChanged")]
        public TfAction ContentsChanged { get; set; }

        [ContextProperty("СочетаниеКлавишДействие", "ShortcutAction")]
        public TfAction ShortcutAction { get; set; }

        [ContextProperty("ТекстИзменен", "TextChanged")]
        public TfAction TextChanged { get; set; }

        [ContextMethod("ВерхнийРодитель", "GetTopSuperView")]
        public IValue GetTopSuperView()
        {
            return Base_obj.GetTopSuperView().dll_obj;
        }

        [ContextMethod("Вставить", "Paste")]
        public void Paste()
        {
            Base_obj.Paste();
        }

        [ContextMethod("ВставитьТекст", "InsertText")]
        public void InsertText(string p1)
        {
            Base_obj.InsertText(p1);
        }

        [ContextMethod("ВСтроку", "ToString")]
        public new string ToString()
        {
            return Base_obj.ToString();
        }

        [ContextMethod("ВыбратьВсе", "SelectAll")]
        public void SelectAll()
        {
            Base_obj.SelectAll();
        }

        [ContextMethod("Вырезать", "Cut")]
        public void Cut()
        {
            Base_obj.Cut();
        }

        [ContextMethod("Выше", "PlaceTop")]
        public void PlaceTop(IValue p1, int p2)
        {
            Base_obj.PlaceTop(((dynamic)p1.AsObject()).Base_obj, p2);
        }

        [ContextMethod("ВышеЛевее", "PlaceTopLeft")]
        public void PlaceTopLeft(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceTopLeft(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("ВышеПравее", "PlaceTopRight")]
        public void PlaceTopRight(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceTopRight(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("ДобавитьСочетаниеКлавиш", "AddShortcut")]
        public void AddShortcut(decimal p1)
        {
            Utils.AddToShortcutDictionary(p1, this);
        }

        [ContextMethod("Заполнить", "Fill")]
        public void Fill(int p1 = 0, int p2 = 0)
        {
            Base_obj.Fill(p1, p2);
        }

        [ContextMethod("Копировать", "Copy")]
        public void Copy()
        {
            Base_obj.Copy();
        }

        [ContextMethod("Левее", "PlaceLeft")]
        public void PlaceLeft(IValue p1, int p2)
        {
            Base_obj.PlaceLeft(((dynamic)p1.AsObject()).Base_obj, p2);
        }

        [ContextMethod("ЛевееВыше", "PlaceLeftTop")]
        public void PlaceLeftTop(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceLeftTop(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("ЛевееНиже", "PlaceLeftBottom")]
        public void PlaceLeftBottom(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceLeftBottom(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("Ниже", "PlaceBottom")]
        public void PlaceBottom(IValue p1, int p2)
        {
            Base_obj.PlaceBottom(((dynamic)p1.AsObject()).Base_obj, p2);
        }

        [ContextMethod("НижеЛевее", "PlaceBottomLeft")]
        public void PlaceBottomLeft(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceBottomLeft(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("НижеПравее", "PlaceBottomRight")]
        public void PlaceBottomRight(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceBottomRight(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("ОчиститьИсторию", "ClearHistoryChanges")]
        public void ClearHistoryChanges()
        {
            Base_obj.ClearHistoryChanges();
        }

        [ContextMethod("ПереместитьВКонец", "MoveEnd")]
        public void MoveEnd()
        {
            Base_obj.MoveEnd();
        }

        [ContextMethod("ПереместитьВНачало", "MoveHome")]
        public void MoveHome()
        {
            Base_obj.MoveHome();
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

        [ContextMethod("Правее", "PlaceRight")]
        public void PlaceRight(IValue p1, int p2)
        {
            Base_obj.PlaceRight(((dynamic)p1.AsObject()).Base_obj, p2);
        }

        [ContextMethod("ПравееВыше", "PlaceRightTop")]
        public void PlaceRightTop(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceRightTop(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("ПравееНиже", "PlaceRightBottom")]
        public void PlaceRightBottom(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceRightBottom(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("ПрокрутитьДо", "ScrollTo")]
        public void ScrollTo(int p1, bool p2 = true)
        {
            Base_obj.ScrollTo(p1, p2);
        }

        [ContextMethod("ТекстТекущейСтроки", "GetCurrentLine")]
        public string GetCurrentLine()
        {
            return Base_obj.GetCurrentLine();
        }

        [ContextMethod("ТочкаНаЭлементе", "ScreenToView")]
        public TfPoint ScreenToView(int p1, int p2)
        {
            return new TfPoint(Base_obj.ScreenToView(p1, p2));
        }

        [ContextMethod("УдалитьСимволСлева", "DeleteCharLeft")]
        public void DeleteCharLeft()
        {
            Base_obj.DeleteCharLeft();
        }

        [ContextMethod("УдалитьСимволСправа", "DeleteCharRight")]
        public void DeleteCharRight()
        {
            Base_obj.DeleteCharRight();
        }

        [ContextMethod("УдалитьСочетаниеКлавиш", "RemoveShortcut")]
        public void RemoveShortcut(decimal p1)
        {
            Utils.RemoveFromShortcutDictionary(p1, this);
        }

        [ContextMethod("УдалитьТекст", "RemoveAllText")]
        public void RemoveAllText()
        {
            Base_obj.RemoveAllText();
        }

        [ContextMethod("УстановитьФокус", "SetFocus")]
        public void SetFocus()
        {
            Base_obj.SetFocus();
        }

        [ContextMethod("ЦветВыделенного", "GetHotNormalColor")]
        public TfAttribute GetHotNormalColor()
        {
            return new TfAttribute(Base_obj.GetHotNormalColor());
        }

        [ContextMethod("ЦветОбычного", "GetNormalColor")]
        public TfAttribute GetNormalColor()
        {
            return new TfAttribute(Base_obj.GetNormalColor());
        }

        [ContextMethod("ЦветФокуса", "GetFocusColor")]
        public TfAttribute GetFocusColor()
        {
            return new TfAttribute(Base_obj.GetFocusColor());
        }

        [ContextMethod("Центр", "Center")]
        public void Center(int p1 = 0, int p2 = 0)
        {
            Base_obj.Center(p1, p2);
        }

    }
}
