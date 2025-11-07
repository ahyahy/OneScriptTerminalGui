using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library.ValueList;
using System.Collections;

namespace ostgui
{
    public class ScrollView : View
    {
        public new TfScrollView dll_obj;
        public Terminal.Gui.ScrollView M_ScrollView;

        public ScrollView()
        {
            M_ScrollView = new Terminal.Gui.ScrollView();
            base.M_View = M_ScrollView;
            Utils.AddToHashtable(M_ScrollView, this);
        }

        public new string ToString()
        {
            return M_ScrollView.ToString();
        }

        public new Toplevel GetTopSuperView()
        {
            return Utils.RevertEqualsObj(M_ScrollView.GetTopSuperView());
        }

        public bool AutoHideScrollBars
        {
            get { return M_ScrollView.AutoHideScrollBars; }
            set { M_ScrollView.AutoHideScrollBars = value; }
        }

        public bool ShowVerticalScrollIndicator
        {
            get { return M_ScrollView.ShowVerticalScrollIndicator; }
            set { M_ScrollView.ShowVerticalScrollIndicator = value; }
        }

        public bool ShowHorizontalScrollIndicator
        {
            get { return M_ScrollView.ShowHorizontalScrollIndicator; }
            set { M_ScrollView.ShowHorizontalScrollIndicator = value; }
        }

        public ostgui.Size ContentSize
        {
            get { return new Size(M_ScrollView.ContentSize); }
            set { M_ScrollView.ContentSize = value.M_Size; }
        }

        public ostgui.Point ContentOffset
        {
            get { return new Point(M_ScrollView.ContentOffset); }
            set
            {
                M_ScrollView.ContentOffset = value.M_Point;
                M_ScrollView.SetNeedsDisplay();
            }
        }

        public bool KeepContentAlwaysInViewport
        {
            get { return M_ScrollView.KeepContentAlwaysInViewport; }
            set { M_ScrollView.KeepContentAlwaysInViewport = value; }
        }

        public bool ScrollUp(int p1)
        {
            return M_ScrollView.ScrollUp(p1);
        }

        public bool ScrollLeft(int p1)
        {
            return M_ScrollView.ScrollLeft(p1);
        }

        public bool ScrollDown(int p1)
        {
            return M_ScrollView.ScrollDown(p1);
        }

        public bool ScrollRight(int p1)
        {
            return M_ScrollView.ScrollRight(p1);
        }
    }

    [ContextClass("ТфПрокручиваемый", "TfScrollView")]
    public class TfScrollView : AutoContext<TfScrollView>
    {

        public TfScrollView()
        {
            ScrollView ScrollView1 = new ScrollView();
            ScrollView1.dll_obj = this;
            Base_obj = ScrollView1;
        }

        public ScrollView Base_obj;

        [ContextProperty("АвтоскрытиеПрокрутки", "AutoHideScrollBars")]
        public bool AutoHideScrollBars
        {
            get { return Base_obj.AutoHideScrollBars; }
            set { Base_obj.AutoHideScrollBars = value; }
        }

        [ContextProperty("Верх", "Top")]
        public TfPos Top
        {
            get { return new TfPos(Base_obj.Top); }
        }

        [ContextProperty("ВФокусе", "Focused")]
        public IValue Focused
        {
            get
            {
                if (Base_obj.M_ScrollView.Focused.GetType().ToString().Contains("+ContentView"))
                {
                    return Utils.RevertEqualsObj(Base_obj.M_ScrollView.Focused.Focused).dll_obj;
                }
                return Utils.RevertEqualsObj(Base_obj.M_ScrollView.Focused).dll_obj;
            }
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
                    Base_obj.M_ScrollView.Y = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_ScrollView.Y = Terminal.Gui.Pos.At(Utils.ToInt32(value));
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
                    Base_obj.M_ScrollView.X = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_ScrollView.X = Terminal.Gui.Pos.At(Utils.ToInt32(value));
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

        [ContextProperty("Подэлементы", "Subviews")]
        public TfSubviewCollection Subviews
        {
            get { return new TfSubviewCollection(Base_obj.M_ScrollView.Subviews[0].Subviews); }
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

        [ContextProperty("РазмерСодержимого", "ContentSize")]
        public TfSize ContentSize
        {
            get { return new TfSize(Base_obj.ContentSize); }
            set { Base_obj.ContentSize = value.Base_obj; }
        }

        [ContextProperty("Родитель", "SuperView")]
        public IValue SuperView
        {
            get
            {
                if (Base_obj.M_ScrollView.SuperView.GetType().ToString().Contains("+ContentView"))
                {
                    return Utils.RevertEqualsObj(Base_obj.M_ScrollView.SuperView.SuperView).dll_obj;
                }
                return Utils.RevertEqualsObj(Base_obj.M_ScrollView.SuperView).dll_obj;
            }
        }

        [ContextProperty("СмещениеСодержимого", "ContentOffset")]
        public TfPoint ContentOffset
        {
            get { return new TfPoint(Base_obj.ContentOffset); }
            set { Base_obj.ContentOffset = value.Base_obj; }
        }

        [ContextProperty("СохранятьПросмотр", "KeepContentAlwaysInViewport")]
        public bool KeepContentAlwaysInViewport
        {
            get { return Base_obj.KeepContentAlwaysInViewport; }
            set { Base_obj.KeepContentAlwaysInViewport = value; }
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

        [ContextProperty("КлавишаНажата", "KeyPress")]
        public TfAction KeyPress { get; set; }

        [ContextProperty("ПриВходе", "Enter")]
        public TfAction Enter { get; set; }

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

        [ContextMethod("Добавить", "Add")]
        public IValue Add(IValue p1)
        {
            Base_obj.Add(((dynamic)p1).Base_obj);
            return p1;
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

        [ContextMethod("ТочкаНаЭлементе", "ScreenToView")]
        public TfPoint ScreenToView(int p1, int p2)
        {
            return new TfPoint(Base_obj.ScreenToView(p1, p2));
        }

        [ContextMethod("Удалить", "Remove")]
        public void Remove(IValue p1)
        {
            Base_obj.Remove(((dynamic)p1).Base_obj);
        }

        [ContextMethod("УдалитьВсе", "RemoveAll")]
        public void RemoveAll()
        {
            Base_obj.RemoveAll();
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
