using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library.ValueList;
using System.Collections;

namespace ostgui
{
    public class ComboBox : View
    {
        public new TfComboBox dll_obj;
        public Terminal.Gui.ComboBox M_ComboBox;

        public ComboBox()
        {
            M_ComboBox = new Terminal.Gui.ComboBox();
            base.M_View = M_ComboBox;
            Utils.AddToHashtable(M_ComboBox, this);
            Width = new Dim().Sized(10);

            M_ComboBox.Collapsed += M_ComboBox_Collapsed;
            M_ComboBox.Expanded += M_ComboBox_Expanded;
            M_ComboBox.SelectedItemChanged += M_ComboBox_SelectedItemChanged;
            M_ComboBox.OpenSelectedItem += M_ComboBox_OpenSelectedItem;
        }

        private void M_ComboBox_OpenSelectedItem(Terminal.Gui.ListViewItemEventArgs obj)
        {
            if (dll_obj.OpenSelectedItem != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.OpenSelectedItem);
                TfEventArgs1.selectedItem = ValueFactory.Create(obj.Item);
                TfValueList _source = dll_obj.Source;
                ValueListItem _sourceItem = _source.Base_obj.M_ValueList.GetValue(ValueFactory.Create(obj.Item));
                TfEventArgs1.valueProp = _sourceItem;
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(dll_obj.OpenSelectedItem);
            }
        }

        private void M_ComboBox_SelectedItemChanged(Terminal.Gui.ListViewItemEventArgs obj)
        {
            if (dll_obj.SelectedItemChanged != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.SelectedItemChanged);
                TfEventArgs1.selectedItem = ValueFactory.Create(obj.Item);
                TfValueList _source = dll_obj.Source;
                ValueListItem _sourceItem = _source.Base_obj.M_ValueList.GetValue(ValueFactory.Create(obj.Item));
                TfEventArgs1.valueProp = _sourceItem;
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(dll_obj.SelectedItemChanged);
            }
        }

        private void M_ComboBox_Expanded()
        {
            if (dll_obj.Expanded != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.Expanded);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(dll_obj.Expanded);
            }
        }

        private void M_ComboBox_Collapsed()
        {
            if (dll_obj.Collapsed != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.Collapsed);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(dll_obj.Collapsed);
            }
        }

        public new string ToString()
        {
            return M_ComboBox.ToString();
        }

        public new Toplevel GetTopSuperView()
        {
            return Utils.RevertEqualsObj(M_ComboBox.GetTopSuperView());
        }

        public int SelectedItemIndex
        {
            get { return M_ComboBox.SelectedItem; }
            set { M_ComboBox.SelectedItem = value; }
        }

        public bool IsShow
        {
            get { return M_ComboBox.IsShow; }
        }

        public bool HideDropdownListOnClick
        {
            get { return M_ComboBox.HideDropdownListOnClick; }
            set { M_ComboBox.HideDropdownListOnClick = value; }
        }

        public string SelectedText
        {
            get { return M_ComboBox.Text.ToString(); }
            set { M_ComboBox.Text = value; }
        }

        public string SearchText
        {
            get { return M_ComboBox.SearchText.ToString(); }
            set { M_ComboBox.SearchText = value; }
        }

        public bool ReadOnly
        {
            get { return M_ComboBox.ReadOnly; }
            set { M_ComboBox.ReadOnly = value; }
        }

        public void Expand()
        {
            M_ComboBox.Expand();
        }

        public void Collapse()
        {
            M_ComboBox.Collapse();
        }
    }

    [ContextClass("ТфПолеВыбора", "TfComboBox")]
    public class TfComboBox : AutoContext<TfComboBox>
    {

        private TfValueList source;

        public TfComboBox()
        {
            ComboBox ComboBox1 = new ComboBox();
            ComboBox1.dll_obj = this;
            Base_obj = ComboBox1;

            source = new TfValueList();
            source.M_Owner = Base_obj.M_ComboBox;
        }

        public ComboBox Base_obj;

        [ContextProperty("Верх", "Top")]
        public TfPos Top
        {
            get { return new TfPos(Base_obj.Top); }
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

        [ContextProperty("Добавлено", "IsAdded")]
        public bool IsAdded
        {
            get { return Base_obj.IsAdded; }
        }

        [ContextProperty("Доступность", "Enabled")]
        public bool Enabled
        {
            get { return Base_obj.Enabled; }
            set { Base_obj.Enabled = value; }
        }

        [ContextProperty("Игрек", "Y")]
        public IValue Y
        {
            get { return new TfPos(Base_obj.Y); }
            set
            {
                if (Utils.IsType<TfPos>(value))
                {
                    Base_obj.M_ComboBox.Y = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_ComboBox.Y = Terminal.Gui.Pos.At(Utils.ToInt32(value));
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
                    Base_obj.M_ComboBox.X = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_ComboBox.X = Terminal.Gui.Pos.At(Utils.ToInt32(value));
                }
            }
        }

        [ContextProperty("ИндексВыбранного", "SelectedItemIndex")]
        public int SelectedItemIndex
        {
            get { return Base_obj.SelectedItemIndex; }
            set { Base_obj.SelectedItemIndex = value; }
        }

        [ContextProperty("Источник", "Source")]
        public TfValueList Source
        {
            get { return source; }
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

        [ContextProperty("Раскрыт", "IsShow")]
        public bool IsShow
        {
            get { return Base_obj.IsShow; }
        }

        [ContextProperty("Родитель", "SuperView")]
        public IValue SuperView
        {
            get
            {
                if (Base_obj.M_ComboBox.SuperView.GetType().ToString().Contains("+ContentView"))
                {
                    return Utils.RevertEqualsObj(Base_obj.M_ComboBox.SuperView.SuperView).dll_obj;
                }
                return Utils.RevertEqualsObj(Base_obj.M_ComboBox.SuperView).dll_obj;
            }
        }

        [ContextProperty("СкрытьСписокПриНажатии", "HideDropdownListOnClick")]
        public bool HideDropdownListOnClick
        {
            get { return Base_obj.HideDropdownListOnClick; }
            set { Base_obj.HideDropdownListOnClick = value; }
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

        [ContextProperty("ТекстВыбранного", "SelectedText")]
        public string SelectedText
        {
            get { return Base_obj.SelectedText; }
            set { Base_obj.SelectedText = value; }
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

        [ContextProperty("ВыбранныйИзменен", "SelectedItemChanged")]
        public TfAction SelectedItemChanged { get; set; }

        [ContextProperty("КлавишаНажата", "KeyPress")]
        public TfAction KeyPress { get; set; }

        [ContextProperty("ПриВходе", "Enter")]
        public TfAction Enter { get; set; }

        [ContextProperty("ПриОткрытииВыбранного", "OpenSelectedItem")]
        public TfAction OpenSelectedItem { get; set; }

        [ContextProperty("ПриРаскрытии", "Expanded")]
        public TfAction Expanded { get; set; }

        [ContextProperty("ПриСворачивании", "Collapsed")]
        public TfAction Collapsed { get; set; }

        [ContextProperty("ПриУходе", "Leave")]
        public TfAction Leave { get; set; }

        [ContextProperty("СочетаниеКлавишДействие", "ShortcutAction")]
        public TfAction ShortcutAction { get; set; }

        [ContextMethod("ВерхнийРодитель", "GetTopSuperView")]
        public IValue GetTopSuperView()
        {
            return Base_obj.GetTopSuperView().dll_obj;
        }

        [ContextMethod("ВСтроку", "ToString")]
        public new string ToString()
        {
            return Base_obj.ToString();
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

        [ContextMethod("Развернуть", "Expand")]
        public void Expand()
        {
            Base_obj.Expand();
        }

        [ContextMethod("Свернуть", "Collapse")]
        public void Collapse()
        {
            Base_obj.Collapse();
        }

        [ContextMethod("ТочкаНаЭлементе", "ScreenToView")]
        public TfPoint ScreenToView(int p1, int p2)
        {
            return new TfPoint(Base_obj.ScreenToView(p1, p2));
        }

        [ContextMethod("УдалитьСочетаниеКлавиш", "RemoveShortcut")]
        public void RemoveShortcut(decimal p1)
        {
            Utils.RemoveFromShortcutDictionary(p1, this);
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
