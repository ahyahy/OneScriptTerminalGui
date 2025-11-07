using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library.ValueList;
using System.Collections;

namespace ostgui
{
    public class Button : View
    {
        public new TfButton dll_obj;
        public Terminal.Gui.Button M_Button;

        public Button()
        {
            M_Button = new Terminal.Gui.Button();
            base.M_View = M_Button;
            Utils.AddToHashtable(M_Button, this);
            SetActions(M_Button);
        }

        public Button(Terminal.Gui.Button p1)
        {
            M_Button = p1;
            base.M_View = M_Button;
            Utils.AddToHashtable(M_Button, this);
            SetActions(M_Button);
        }

        private void SetActions(Terminal.Gui.Button button)
        {
            button.Clicked += Button_Clicked;
        }

        private void Button_Clicked()
        {
            if (dll_obj.Clicked != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.Clicked);
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(dll_obj.Clicked);
            }
        }

        public bool IsDefault
        {
            get { return M_Button.IsDefault; }
            set { M_Button.IsDefault = value; }
        }

        public new string ToString()
        {
            return M_Button.ToString();
        }

        public new Toplevel GetTopSuperView()
        {
            return Utils.RevertEqualsObj(M_Button.GetTopSuperView());
        }
    }

    [ContextClass("ТфКнопка", "TfButton")]
    public class TfButton : AutoContext<TfButton>
    {

        public TfButton()
        {
            Button Button1 = new Button();
            Button1.dll_obj = this;
            Base_obj = Button1;
        }

        public TfButton(Terminal.Gui.Button p1)
        {
            Button Button1 = new Button(p1);
            Button1.dll_obj = this;
            Base_obj = Button1;
        }

        public Button Base_obj;

        [ContextProperty("АвтоРазмер", "AutoSize")]
        public bool AutoSize
        {
            get { return Base_obj.AutoSize; }
            set { Base_obj.AutoSize = value; }
        }

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

        [ContextProperty("ВФокусе", "Focused")]
        public IValue Focused
        {
            get
            {
                if (Base_obj.M_Button.Focused.GetType().ToString().Contains("+ContentView"))
                {
                    return Utils.RevertEqualsObj(Base_obj.M_Button.Focused.Focused).dll_obj;
                }
                return Utils.RevertEqualsObj(Base_obj.M_Button.Focused).dll_obj;
            }
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

        [ContextProperty("Граница", "Border")]
        public TfBorder Border
        {
            get { return Base_obj.Border.dll_obj; }
            set
            {
                TfBorder border = new TfBorder();
                Terminal.Gui.Border _border = value.Base_obj.M_Border;
                border.Base_obj.M_Border.Background = _border.Background;
                border.Base_obj.M_Border.BorderBrush = _border.BorderBrush;
                border.Base_obj.M_Border.BorderStyle = _border.BorderStyle;
                border.Base_obj.M_Border.BorderThickness = _border.BorderThickness;
                border.Base_obj.M_Border.Effect3D = _border.Effect3D;
                border.Base_obj.M_Border.Effect3DBrush = _border.Effect3DBrush;
                border.Base_obj.M_Border.Effect3DOffset = _border.Effect3DOffset;
                border.Base_obj.M_Border.Padding = _border.Padding;
                border.Base_obj.M_Border.Title = _border.Title;
                Base_obj.Border = border.Base_obj;
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
                    Base_obj.M_Button.Y = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_Button.Y = Terminal.Gui.Pos.At(Utils.ToInt32(value));
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
                    Base_obj.M_Button.X = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_Button.X = Terminal.Gui.Pos.At(Utils.ToInt32(value));
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

        [ContextProperty("КлавишаВызова", "HotKey")]
        public int HotKey
        {
            get { return Base_obj.HotKey; }
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

        [ContextProperty("НаправлениеТекста", "TextDirection")]
        public int TextDirection
        {
            get { return Base_obj.TextDirection; }
            set { Base_obj.TextDirection = value; }
        }

        [ContextProperty("Низ", "Bottom")]
        public TfPos Bottom
        {
            get { return new TfPos(Base_obj.Bottom); }
        }

        private bool disableHotKey = false;
        [ContextProperty("ОтключитьКлавишуВызова", "DisableHotKey")]
        public bool DisableHotKey
        {
            get { return disableHotKey; }
            set
            {
                if (value)
                {
                    Base_obj.HotKeySpecifier = (Rune)0xFFFF;
                    disableHotKey = true;
                }
                else
                {
                    Base_obj.HotKeySpecifier = new Rune("_".ToCharArray()[0]);
                    disableHotKey = false;
                }
            }
        }

        [ContextProperty("Отображать", "Visible")]
        public bool Visible
        {
            get { return Base_obj.Visible; }
            set { Base_obj.Visible = value; }
        }

        [ContextProperty("ОформительТекста", "TextFormatter")]
        public TfTextFormatter TextFormatter
        {
            get { return Base_obj.TextFormatter.dll_obj; }
            set { Base_obj.TextFormatter = value.Base_obj; }
        }

        [ContextProperty("Подэлементы", "Subviews")]
        public TfSubviewCollection Subviews
        {
            get { return new TfSubviewCollection(Base_obj.M_Button.Subviews); }
        }

        [ContextProperty("ПорядокОбхода", "TabIndex")]
        public int TabIndex
        {
            get { return Base_obj.TabIndex; }
            set { Base_obj.TabIndex = value; }
        }

        [ContextProperty("ПоУмолчанию", "IsDefault")]
        public bool IsDefault
        {
            get { return Base_obj.IsDefault; }
            set { Base_obj.IsDefault = value; }
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
                if (Base_obj.M_Button.SuperView.GetType().ToString().Contains("+ContentView"))
                {
                    return Utils.RevertEqualsObj(Base_obj.M_Button.SuperView.SuperView).dll_obj;
                }
                return Utils.RevertEqualsObj(Base_obj.M_Button.SuperView).dll_obj;
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

        [ContextProperty("Нажатие", "Clicked")]
        public TfAction Clicked { get; set; }

        [ContextProperty("ПриВходе", "Enter")]
        public TfAction Enter { get; set; }

        [ContextProperty("ПриНажатииМыши", "MouseClick")]
        public TfAction MouseClick { get; set; }

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

        [ContextMethod("НаЗаднийПлан", "SendSubviewToBack")]
        public void SendSubviewToBack(IValue p1)
        {
            Base_obj.SendSubviewToBack(((dynamic)p1).Base_obj);
        }

        [ContextMethod("НаПереднийПлан", "BringSubviewToFront")]
        public void BringSubviewToFront(IValue p1)
        {
            Base_obj.BringSubviewToFront(((dynamic)p1).Base_obj);
        }

        [ContextMethod("НаШагВперед", "BringSubviewForward")]
        public void BringSubviewForward(IValue p1)
        {
            Base_obj.BringSubviewForward(((dynamic)p1).Base_obj);
        }

        [ContextMethod("НаШагНазад", "SendSubviewBackwards")]
        public void SendSubviewBackwards(IValue p1)
        {
            Base_obj.SendSubviewBackwards(((dynamic)p1).Base_obj);
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

        [ContextMethod("ПолучитьАвтоРазмер", "GetAutoSize")]
        public TfSize GetAutoSize()
        {
            int offsetWidth = 0;
            int offsetHeight = 0;
            try
            {
                offsetWidth = Border.BorderThickness.Left + Border.BorderThickness.Right;
                offsetHeight = Border.BorderThickness.Top + Border.BorderThickness.Bottom;
            }
            catch { }
            int MaxWidthLine = Terminal.Gui.TextFormatter.MaxWidthLine(Text);
            int MaxLines = Terminal.Gui.TextFormatter.MaxLines(Text, MaxWidthLine);
            try
            {
                return new TfSize(MaxWidthLine + offsetWidth, MaxLines + offsetHeight);
            }
            catch
            {
                return null;
            }
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

        [ContextMethod("УстановитьАвтоРазмер", "SetAutoSize")]
        public void SetAutoSize()
        {
            TfSize TfSize1 = GetAutoSize();
            Width = new TfDim().Sized(TfSize1.Width);
            Height = new TfDim().Sized(TfSize1.Height);
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
