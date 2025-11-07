using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library.ValueList;
using System.Collections;

namespace ostgui
{
    public class ListView : View
    {
        public new TfListView dll_obj;
        public Terminal.Gui.ListView M_ListView;

        public ListView()
        {
            M_ListView = new Terminal.Gui.ListView();
            base.M_View = M_ListView;
            Utils.AddToHashtable(M_ListView, this);
            SetActions(M_ListView);
        }

        private void SetActions(Terminal.Gui.ListView listView)
        {
            listView.Enter += ListView_Enter;
            listView.Leave += ListView_Leave;
            listView.SelectedItemChanged += ListView_SelectedItemChanged;
            listView.OpenSelectedItem += ListView_OpenSelectedItem;

        }

        private void ListView_OpenSelectedItem(Terminal.Gui.ListViewItemEventArgs obj)
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

        private void ListView_SelectedItemChanged(Terminal.Gui.ListViewItemEventArgs obj)
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

        private void ListView_Leave(Terminal.Gui.View.FocusEventArgs obj)
        {
            TfAction action;
            try
            {
                action = ((dynamic)dll_obj).Leave;
            }
            catch
            {
                return;
            }
            if (action != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(action);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(action);
            }
        }

        private void ListView_Enter(Terminal.Gui.View.FocusEventArgs obj)
        {
            TfAction action;
            try
            {
                action = ((dynamic)dll_obj).Enter;
            }
            catch
            {
                return;
            }
            if (action != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(action);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(action);
            }
        }

        public new string ToString()
        {
            return M_ListView.ToString();
        }

        public new Toplevel GetTopSuperView()
        {
            return Utils.RevertEqualsObj(M_ListView.GetTopSuperView());
        }

        public int TopItemIndex
        {
            get { return M_ListView.TopItem; }
            set { M_ListView.TopItem = value; }
        }

        public int SelectedItemIndex
        {
            get { return M_ListView.SelectedItem; }
            set { M_ListView.SelectedItem = value; }
        }

        public int LeftItemIndex
        {
            get { return M_ListView.LeftItem; }
            set { M_ListView.LeftItem = value; }
        }

        public int Maxlength
        {
            get { return M_ListView.Maxlength; }
        }

        public bool AllowsMultipleSelection
        {
            get { return M_ListView.AllowsMultipleSelection; }
            set { M_ListView.AllowsMultipleSelection = value; }
        }

        public bool AllowsMarking
        {
            get { return M_ListView.AllowsMarking; }
            set { M_ListView.AllowsMarking = value; }
        }

        public void EnsureSelectedItemVisible()
        {
            M_ListView.EnsureSelectedItemVisible();
        }

        public bool MoveUp()
        {
            return M_ListView.MoveUp();
        }

        public bool MovePageUp()
        {
            return M_ListView.MovePageUp();
        }

        public bool MoveEnd()
        {
            return M_ListView.MoveEnd();
        }

        public bool MoveHome()
        {
            return M_ListView.MoveHome();
        }

        public bool MoveDown()
        {
            return M_ListView.MoveDown();
        }

        public bool MovePageDown()
        {
            return M_ListView.MovePageDown();
        }

        public void MarkUnmarkRow(int p1, bool p2)
        {
            M_ListView.Source.SetMark(p1, p2);
        }

        public bool ScrollUp(int p1)
        {
            return M_ListView.ScrollUp(p1);
        }

        public bool ScrollLeft(int p1)
        {
            return M_ListView.ScrollLeft(p1);
        }

        public bool ScrollDown(int p1)
        {
            return M_ListView.ScrollDown(p1);
        }

        public bool ScrollRight(int p1)
        {
            return M_ListView.ScrollRight(p1);
        }

        public void AllowsAll()
        {
            int num = M_ListView.SelectedItem;
            for (int i = 0; i < M_ListView.Source.Count; i++)
            {
                M_ListView.SelectedItem = i;
                if (num != i)
                {
                    if (M_ListView.Source.IsMarked(M_ListView.SelectedItem))
                    {
                        M_ListView.MarkUnmarkRow();
                    }
                }
            }
            M_ListView.SelectedItem = num;
            //bool _bool = M_ListView.AllowsAll(); // Это не работает, поэтому обработал по своему.
        }

        public bool Checked(int p1)
        {
            return M_ListView.Source.IsMarked(p1);
        }
    }

    [ContextClass("ТфСписокЭлементов", "TfListView")]
    public class TfListView : AutoContext<TfListView>
    {

        private TfValueList source;

        public TfListView()
        {
            ListView ListView1 = new ListView();
            ListView1.dll_obj = this;
            Base_obj = ListView1;

            source = new TfValueList();
            source.M_Owner = Base_obj.M_ListView;
        }

        public ListView Base_obj;

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
                    Base_obj.M_ListView.Y = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_ListView.Y = Terminal.Gui.Pos.At(Utils.ToInt32(value));
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
                    Base_obj.M_ListView.X = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_ListView.X = Terminal.Gui.Pos.At(Utils.ToInt32(value));
                }
            }
        }

        [ContextProperty("ИндексВерхнего", "TopItemIndex")]
        public int TopItemIndex
        {
            get { return Base_obj.TopItemIndex; }
            set { Base_obj.TopItemIndex = value; }
        }

        [ContextProperty("ИндексВыбранного", "SelectedItemIndex")]
        public int SelectedItemIndex
        {
            get { return Base_obj.SelectedItemIndex; }
            set { Base_obj.SelectedItemIndex = value; }
        }

        [ContextProperty("ИндексЛевого", "LeftItemIndex")]
        public int LeftItemIndex
        {
            get { return Base_obj.LeftItemIndex; }
            set { Base_obj.LeftItemIndex = value; }
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

        [ContextProperty("МножественныйВыбор", "AllowsMultipleSelection")]
        public bool AllowsMultipleSelection
        {
            get { return Base_obj.AllowsMultipleSelection; }
            set { Base_obj.AllowsMultipleSelection = value; }
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

        [ContextProperty("Пометки", "AllowsMarking")]
        public bool AllowsMarking
        {
            get { return Base_obj.AllowsMarking; }
            set { Base_obj.AllowsMarking = value; }
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
                if (Base_obj.M_ListView.SuperView.GetType().ToString().Contains("+ContentView"))
                {
                    return Utils.RevertEqualsObj(Base_obj.M_ListView.SuperView.SuperView).dll_obj;
                }
                return Utils.RevertEqualsObj(Base_obj.M_ListView.SuperView).dll_obj;
            }
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

        [ContextProperty("ТекущийСверху", "IsCurrentTop")]
        public bool IsCurrentTop
        {
            get { return Base_obj.IsCurrentTop; }
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

        [ContextProperty("ПриВходе", "Enter")]
        public TfAction Enter { get; set; }

        [ContextProperty("ПриОткрытииВыбранного", "OpenSelectedItem")]
        public TfAction OpenSelectedItem { get; set; }

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

        [ContextMethod("ПерейтиВверх", "MoveUp")]
        public bool MoveUp()
        {
            return Base_obj.MoveUp();
        }

        [ContextMethod("ПерейтиВверхСтраницы", "MovePageUp")]
        public bool MovePageUp()
        {
            return Base_obj.MovePageUp();
        }

        [ContextMethod("ПерейтиВКонец", "MoveEnd")]
        public bool MoveEnd()
        {
            return Base_obj.MoveEnd();
        }

        [ContextMethod("ПерейтиВНачало", "MoveHome")]
        public bool MoveHome()
        {
            return Base_obj.MoveHome();
        }

        [ContextMethod("ПерейтиВниз", "MoveDown")]
        public bool MoveDown()
        {
            return Base_obj.MoveDown();
        }

        [ContextMethod("ПерейтиВнизСтраницы", "MovePageDown")]
        public bool MovePageDown()
        {
            return Base_obj.MovePageDown();
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

        [ContextMethod("Пометить", "MarkUnmarkRow")]
        public void MarkUnmarkRow(int p1, bool p2 = true)
        {
            Base_obj.MarkUnmarkRow(p1, p2);
        }

        [ContextMethod("Помечен", "Checked")]
        public bool Checked(int p1)
        {
            return Base_obj.Checked(p1);
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

        [ContextMethod("ПрокрутитьВверх", "ScrollUp")]
        public bool ScrollUp(int p1)
        {
            return Base_obj.ScrollUp(p1);
        }

        [ContextMethod("ПрокрутитьВлево", "ScrollLeft")]
        public bool ScrollLeft(int p1)
        {
            return Base_obj.ScrollLeft(p1);
        }

        [ContextMethod("ПрокрутитьВниз", "ScrollDown")]
        public bool ScrollDown(int p1)
        {
            return Base_obj.ScrollDown(p1);
        }

        [ContextMethod("ПрокрутитьВправо", "ScrollRight")]
        public bool ScrollRight(int p1)
        {
            return Base_obj.ScrollRight(p1);
        }

        [ContextMethod("СнятьДляВсех", "AllowsAll")]
        public void AllowsAll()
        {
            Base_obj.AllowsAll();
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
