using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library.ValueList;
using System.Collections;

namespace ostgui
{
    public class TextField : View
    {
        public new TfTextField dll_obj;
        public Terminal.Gui.TextField m_TextField;

        public Terminal.Gui.TextField M_TextField
        {
            get { return m_TextField; }
            set
            {
                m_TextField = value;
                base.M_View = m_TextField;
            }
        }

        public TextField()
        {
            M_TextField = new Terminal.Gui.TextField();
            base.M_View = M_TextField;
            Utils.AddToHashtable(M_TextField, this);

            M_TextField.TextChanged += M_TextField_TextChanged;
            M_TextField.TextChanging += M_TextField_TextChanging;
        }

        private void M_TextField_TextChanging(Terminal.Gui.TextChangingEventArgs obj)
        {
            if (dll_obj.TextChanging != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.TextChanging);
                TfEventArgs1.cancel = ValueFactory.Create(obj.Cancel);
                TfEventArgs1.newText = ValueFactory.Create(obj.NewText.ToString());
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(dll_obj.TextChanging);
                obj.Cancel = TfEventArgs1.Cancel;
            }
        }

        private void M_TextField_TextChanged(NStack.ustring obj)
        {
            if (dll_obj.TextChanged != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.TextChanged);
                TfEventArgs1.oldText = ValueFactory.Create(obj.ToString());
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(dll_obj.TextChanged);
            }
        }

        public new string ToString()
        {
            return M_TextField.ToString();
        }

        public new Toplevel GetTopSuperView()
        {
            return Utils.RevertEqualsObj(M_TextField.GetTopSuperView());
        }

        public string SelectedText
        {
            get
            {
                try
                {
                    return M_TextField.SelectedText.ToString();
                }
                catch { }
                return "";
            }
        }

        public int SelectedLength
        {
            get { return M_TextField.SelectedLength; }
        }

        public bool Used
        {
            get { return M_TextField.Used; }
            set { M_TextField.Used = value; }
        }

        public bool IsDirty
        {
            get { return M_TextField.IsDirty; }
        }

        public bool HasHistoryChanges
        {
            get { return M_TextField.HasHistoryChanges; }
        }

        public decimal DesiredCursorVisibility
        {
            get { return (decimal)M_TextField.DesiredCursorVisibility; }
            set { M_TextField.DesiredCursorVisibility = (Terminal.Gui.CursorVisibility)value; }
        }

        public int SelectedStart
        {
            get { return M_TextField.SelectedStart; }
            set { M_TextField.SelectedStart = value; }
        }

        public bool Secret
        {
            get { return M_TextField.Secret; }
            set { M_TextField.Secret = value; }
        }

        public int ScrollOffset
        {
            get { return M_TextField.ScrollOffset; }
        }

        public bool ReadOnly
        {
            get { return M_TextField.ReadOnly; }
            set { M_TextField.ReadOnly = value; }
        }

        public void InsertText(string p1, bool p2 = true)
        {
            M_TextField.InsertText(p1, p2);
        }

        public void Paste()
        {
            M_TextField.Paste();
        }

        public void SelectAll()
        {
            M_TextField.SelectAll();
        }

        public void Cut()
        {
            M_TextField.Cut();
        }

        public void Copy()
        {
            M_TextField.Copy();
        }

        public void ClearAllSelection()
        {
            M_TextField.ClearAllSelection();
        }

        public void ClearHistoryChanges()
        {
            M_TextField.ClearHistoryChanges();
        }

        public void RemoveAllText()
        {
            M_TextField.DeleteAll();
        }

        public void KillWordBackwards()
        {
            M_TextField.KillWordBackwards();
        }

        public void DeleteCharLeft()
        {
            M_TextField.DeleteCharLeft(false);
        }

        public void DeleteCharRight()
        {
            M_TextField.DeleteCharRight();
        }

        public void KillWordForwards()
        {
            M_TextField.KillWordForwards();
        }

        public new string Text
        {
            get { return M_TextField.Text.ToString(); }
            set { M_TextField.Text = value; }
        }

        public int CursorPosition
        {
            get { return M_TextField.CursorPosition; }
            set { M_TextField.CursorPosition = value; }
        }
    }

    [ContextClass("ТфПолеВвода", "TfTextField")]
    public class TfTextField : AutoContext<TfTextField>
    {

        public TfTextField()
        {
            TextField TextField1 = new TextField();
            TextField1.dll_obj = this;
            Base_obj = TextField1;
        }

        public TextField Base_obj;

        [ContextProperty("Верх", "Top")]
        public TfPos Top
        {
            get { return new TfPos(Base_obj.Top); }
        }

        [ContextProperty("ВыделенныйТекст", "SelectedText")]
        public string SelectedText
        {
            get { return Base_obj.SelectedText; }
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
                    Base_obj.M_TextField.Y = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_TextField.Y = Terminal.Gui.Pos.At(Utils.ToInt32(value));
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
                    Base_obj.M_TextField.X = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_TextField.X = Terminal.Gui.Pos.At(Utils.ToInt32(value));
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

        [ContextProperty("КонтекстноеМеню", "ContextMenu")]
        public TfContextMenu ContextMenu
        {
            get { return new TfContextMenu(Base_obj.M_TextField.ContextMenu); }
        }

        [ContextProperty("Курсор", "DesiredCursorVisibility")]
        public decimal DesiredCursorVisibility
        {
            get { return Base_obj.DesiredCursorVisibility; }
            set { Base_obj.DesiredCursorVisibility = value; }
        }

        [ContextProperty("Лево", "Left")]
        public TfPos Left
        {
            get { return new TfPos(Base_obj.Left); }
        }

        [ContextProperty("Метка", "Tag")]
        public IValue Tag
        {
            get { return Base_obj.Tag; }
            set { Base_obj.Tag = value; }
        }

        [ContextProperty("НачалоВыделения", "SelectedStart")]
        public int SelectedStart
        {
            get { return Base_obj.SelectedStart; }
            set { Base_obj.SelectedStart = value; }
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

        [ContextProperty("ПозицияКурсора", "CursorPosition")]
        public int CursorPosition
        {
            get { return Base_obj.CursorPosition; }
            set { Base_obj.CursorPosition = value; }
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

        [ContextProperty("Родитель", "SuperView")]
        public IValue SuperView
        {
            get
            {
                if (Base_obj.M_TextField.SuperView.GetType().ToString().Contains("+ContentView"))
                {
                    return Utils.RevertEqualsObj(Base_obj.M_TextField.SuperView.SuperView).dll_obj;
                }
                return Utils.RevertEqualsObj(Base_obj.M_TextField.SuperView).dll_obj;
            }
        }

        [ContextProperty("Секрет", "Secret")]
        public bool Secret
        {
            get { return Base_obj.Secret; }
            set { Base_obj.Secret = value; }
        }

        [ContextProperty("СтильКомпоновки", "LayoutStyle")]
        public int LayoutStyle
        {
            get { return Base_obj.LayoutStyle; }
            set { Base_obj.LayoutStyle = value; }
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

        [ContextProperty("КлавишаНажата", "KeyPress")]
        public TfAction KeyPress { get; set; }

        [ContextProperty("МышьНадЭлементом", "MouseEnter")]
        public TfAction MouseEnter { get; set; }

        [ContextProperty("МышьПокинулаЭлемент", "MouseLeave")]
        public TfAction MouseLeave { get; set; }

        [ContextProperty("ПриВходе", "Enter")]
        public TfAction Enter { get; set; }

        [ContextProperty("ПриИзмененииТекста", "TextChanging")]
        public TfAction TextChanging { get; set; }

        [ContextProperty("ПриНажатииМыши", "MouseClick")]
        public TfAction MouseClick { get; set; }

        [ContextProperty("ПриУходе", "Leave")]
        public TfAction Leave { get; set; }

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
        public void InsertText(string p1, bool p2 = true)
        {
            Base_obj.InsertText(p1, p2);
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

        [ContextMethod("СнятьВыделение", "ClearAllSelection")]
        public void ClearAllSelection()
        {
            Base_obj.ClearAllSelection();
        }

        [ContextMethod("ТочкаНаЭлементе", "ScreenToView")]
        public TfPoint ScreenToView(int p1, int p2)
        {
            return new TfPoint(Base_obj.ScreenToView(p1, p2));
        }

        [ContextMethod("УдалитьПредыдущееСлово", "KillWordBackwards")]
        public void KillWordBackwards()
        {
            Base_obj.KillWordBackwards();
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

        [ContextMethod("УдалитьСледующееСлово", "KillWordForwards")]
        public void KillWordForwards()
        {
            Base_obj.KillWordForwards();
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
