using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace ostgui
{
    public class Window : Toplevel
    {
        public new TfWindow dll_obj;
        public Terminal.Gui.Window M_Window;

        public Window()
        {
            M_Window = new Terminal.Gui.Window();
            base.M_Toplevel = M_Window;
            OneScriptTerminalGui.AddToHashtable(M_Window, this);
        }

        public Window(string p1)
        {
            M_Window = new Terminal.Gui.Window(p1);
            base.M_Toplevel = M_Window;
            OneScriptTerminalGui.AddToHashtable(M_Window, this);
        }

        public Window(Rect p1, string p2)
        {
            M_Window = new Terminal.Gui.Window(p1.M_Rect, p2);
            base.M_Toplevel = M_Window;
            OneScriptTerminalGui.AddToHashtable(M_Window, this);
        }

        public Window(string p1, int p2, Border p3)
        {
            M_Window = new Terminal.Gui.Window(p1, p2, p3.M_Border);
            base.M_Toplevel = M_Window;
            OneScriptTerminalGui.AddToHashtable(M_Window, this);
        }

        public Window(Rect p1, string p2, int p3, Border p4)
        {
            M_Window = new Terminal.Gui.Window(p1.M_Rect, p2, p3, p4.M_Border);
            base.M_Toplevel = M_Window;
            OneScriptTerminalGui.AddToHashtable(M_Window, this);
        }

        public string Title
        {
            get { return M_Window.Title.ToString(); }
            set { M_Window.Title = value; }
        }

        public new string ToString()
        {
            return M_Window.ToString();
        }

        public new Window GetTopSuperView()
        {
            return OneScriptTerminalGui.RevertEqualsObj(M_Window.GetTopSuperView());
        }
    }

    [ContextClass("ТфОкно", "TfWindow")]
    public class TfWindow : AutoContext<TfWindow>
    {

        public TfWindow()
        {
            Window Window1 = new Window();
            Window1.dll_obj = this;
            Base_obj = Window1;
        }

        public TfWindow(string p1)
        {
            Window Window1 = new Window(p1);
            Window1.dll_obj = this;
            Base_obj = Window1;
        }

        public TfWindow(TfRect p1, string p2)
        {
            Window Window1 = new Window(p1.Base_obj, p2);
            Window1.dll_obj = this;
            Base_obj = Window1;
        }

        public TfWindow(string p1, int p2, TfBorder p3)
        {
            Window Window1 = new Window(p1, p2, p3.Base_obj);
            Window1.dll_obj = this;
            Base_obj = Window1;
        }

        public TfWindow(TfRect p1, string p2, int p3, TfBorder p4)
        {
            Window Window1 = new Window(p1.Base_obj, p2, p3, p4.Base_obj);
            Window1.dll_obj = this;
            Base_obj = Window1;
        }

        public Window Base_obj;

        public TfAction LayoutComplete { get; set; }
        public TfAction LayoutStarted { get; set; }
        public TfAction DrawContentComplete { get; set; }
        public TfAction DrawContent { get; set; }

        [ContextProperty("ВертикальноеВыравниваниеТекста", "VerticalTextAlignment")]
        public int VerticalTextAlignment
        {
            get { return (int)Base_obj.M_Window.Subviews[0].VerticalTextAlignment; }
            set { Base_obj.M_Window.Subviews[0].VerticalTextAlignment = (Terminal.Gui.VerticalTextAlignment)value; }
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
                if (Base_obj.M_Window.Subviews[0].Focused != null)
                {
                    return OneScriptTerminalGui.RevertEqualsObj(Base_obj.M_Window.Subviews[0].Focused).dll_obj;
                }
                return null;
            }
        }

        [ContextProperty("ВыравниваниеТекста", "TextAlignment")]
        public int TextAlignment
        {
            get { return (int)Base_obj.M_Window.Subviews[0].TextAlignment; }
            set { Base_obj.M_Window.Subviews[0].TextAlignment = (Terminal.Gui.TextAlignment)value; }
        }

        [ContextProperty("Высота", "Height")]
        public TfDim Height
        {
            get { return Base_obj.Height.dll_obj; }
            set { Base_obj.Height = value.Base_obj; }
        }

        [ContextProperty("Граница", "Border")]
        public TfBorder Border
        {
            get { return Base_obj.Border.dll_obj; }
            set { Base_obj.Border = value.Base_obj; }
        }

        [ContextProperty("Границы", "Bounds")]
        public TfRect Bounds
        {
            get { return new TfRect(Base_obj.Frame.M_Rect.X, Base_obj.Frame.M_Rect.Y, Base_obj.Bounds.M_Rect.Width, Base_obj.Bounds.M_Rect.Height); }
        }

        [ContextProperty("Данные", "Data")]
        public IValue Data
        {
            get { return OneScriptTerminalGui.RevertObj(Base_obj.Data); }
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

        [ContextProperty("Заголовок", "Title")]
        public string Title
        {
            get { return Base_obj.Title; }
            set { Base_obj.Title = value; }
        }

        [ContextProperty("Игрек", "Y")]
        public TfPos Y
        {
            get { return new TfPos(Base_obj.Y); }
            set { Base_obj.Y = value.Base_obj; }
        }

        [ContextProperty("Идентификатор", "Id")]
        public string Id
        {
            get { return Base_obj.Id; }
            set { Base_obj.Id = value; }
        }

        [ContextProperty("Икс", "X")]
        public TfPos X
        {
            get { return new TfPos(Base_obj.X); }
            set { Base_obj.X = value.Base_obj; }
        }

        [ContextProperty("Кадр", "Frame")]
        public TfRect Frame
        {
            get { return new TfRect(Base_obj.Frame.M_Rect.X, Base_obj.Frame.M_Rect.Y, Base_obj.Frame.M_Rect.Width, Base_obj.Frame.M_Rect.Height); }
            set { Base_obj.Frame = value.Base_obj; }
        }

        [ContextProperty("Лево", "Left")]
        public TfPos Left
        {
            get { return new TfPos(Base_obj.Left); }
        }

        [ContextProperty("Модально", "Modal")]
        public bool Modal
        {
            get { return Base_obj.Modal; }
            set { Base_obj.Modal = value; }
        }

        [ContextProperty("НаправлениеТекста", "TextDirection")]
        public int TextDirection
        {
            get { return (int)Base_obj.M_Window.Subviews[0].TextDirection; }
            set { Base_obj.M_Window.Subviews[0].TextDirection = (Terminal.Gui.TextDirection)value; }
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

        [ContextProperty("ОформительТекста", "TextFormatter")]
        public TfTextFormatter TextFormatter
        {
            get { return OneScriptTerminalGui.RevertEqualsObj(Base_obj.M_Window.Subviews[0].TextFormatter).dll_obj; }
            set { Base_obj.M_Window.Subviews[0].TextFormatter = value.Base_obj.M_TextFormatter; }
        }

        [ContextProperty("ПанельМеню", "MenuBar")]
        public TfMenuBar MenuBar
        {
            get { return Base_obj.MenuBar.dll_obj; }
            set { Base_obj.MenuBar = value.Base_obj; }
        }

        [ContextProperty("Подэлементы", "Subviews")]
        public TfSubviewCollection Subviews
        {
            get { return new TfSubviewCollection((dynamic)Base_obj.M_Window.Subviews[0].Subviews); }
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
                try
                {
                    return OneScriptTerminalGui.RevertEqualsObj(Base_obj.M_Window.Subviews[0].SuperView.SuperView.SuperView).dll_obj;
                }
                catch (Exception)
                {
                    return OneScriptTerminalGui.RevertEqualsObj(Base_obj.M_Window.Subviews[0].SuperView.SuperView).dll_obj;
                }
            }
        }

        [ContextProperty("СтильКомпоновки", "LayoutStyle")]
        public int LayoutStyle
        {
            get { return Base_obj.LayoutStyle; }
            set { Base_obj.LayoutStyle = value; }
        }

        [ContextProperty("СтрокаСостояния", "StatusBar")]
        public TfStatusBar StatusBar
        {
            get { return Base_obj.StatusBar.dll_obj; }
            set { Base_obj.M_View.Add(value.Base_obj.M_View); }
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
        }

        [ContextProperty("ЦветоваяСхема", "ColorScheme")]
        public TfColorScheme ColorScheme
        {
            get { return Base_obj.ColorScheme.dll_obj; }
            set { Base_obj.ColorScheme = value.Base_obj; }
        }

        [ContextProperty("Ширина", "Width")]
        public TfDim Width
        {
            get { return Base_obj.Width.dll_obj; }
            set { Base_obj.Width = value.Base_obj; }
        }

        [ContextProperty("Активирован", "Activate")]
        public TfAction Activate { get; set; }

        [ContextProperty("ВидимостьИзменена", "VisibleChanged")]
        public TfAction VisibleChanged { get; set; }

        [ContextProperty("ВсеДочерниеЗакрыты", "AllChildClosed")]
        public TfAction AllChildClosed { get; set; }

        [ContextProperty("Выгружен", "Unloaded")]
        public TfAction Unloaded { get; set; }

        [ContextProperty("Готов", "Ready")]
        public TfAction Ready { get; set; }

        [ContextProperty("Деактивирован", "Deactivate")]
        public TfAction Deactivate { get; set; }

        [ContextProperty("ДобавленЭлемент", "Added")]
        public TfAction Added { get; set; }

        [ContextProperty("ДоступностьИзменена", "EnabledChanged")]
        public TfAction EnabledChanged { get; set; }

        [ContextProperty("ДочернийВыгружен", "ChildUnloaded")]
        public TfAction ChildUnloaded { get; set; }

        [ContextProperty("ДочернийЗагружен", "ChildLoaded")]
        public TfAction ChildLoaded { get; set; }

        [ContextProperty("ДочернийЗакрыт", "ChildClosed")]
        public TfAction ChildClosed { get; set; }

        [ContextProperty("ЗаголовокИзменен", "TitleChanged")]
        public TfAction TitleChanged { get; set; }

        [ContextProperty("ЗаголовокИзменяется", "TitleChanging")]
        public TfAction TitleChanging { get; set; }

        [ContextProperty("Закрыт", "Closed")]
        public TfAction Closed { get; set; }

        [ContextProperty("КлавишаВыходаИзменена", "QuitKeyChanged")]
        public TfAction QuitKeyChanged { get; set; }

        [ContextProperty("КлавишаНажата", "KeyPress")]
        public TfAction KeyPress { get; set; }

        [ContextProperty("МышьНадЭлементом", "MouseEnter")]
        public TfAction MouseEnter { get; set; }

        [ContextProperty("МышьПокинулаЭлемент", "MouseLeave")]
        public TfAction MouseLeave { get; set; }

        [ContextProperty("ПриВходе", "Enter")]
        public TfAction Enter { get; set; }

        [ContextProperty("ПриЗагруке", "Loaded")]
        public TfAction Loaded { get; set; }

        [ContextProperty("ПриЗакрытии", "Closing")]
        public TfAction Closing { get; set; }

        [ContextProperty("ПриНажатииМыши", "MouseClick")]
        public TfAction MouseClick { get; set; }

        [ContextProperty("РазмерИзменен", "Resized")]
        public TfAction Resized { get; set; }

        [ContextProperty("ФокусируемостьИзменена", "CanFocusChanged")]
        public TfAction CanFocusChanged { get; set; }

        [ContextProperty("ЭлементАктивирован", "InitializedItem")]
        public TfAction InitializedItem { get; set; }

        [ContextProperty("ЭлементПокинут", "Leave")]
        public TfAction Leave { get; set; }

        [ContextProperty("ЭлементУдален", "Removed")]
        public TfAction Removed { get; set; }

        [ContextMethod("ВерхнийРодитель", "GetTopSuperView")]
        public IValue GetTopSuperView()
        {
            try
            {
                return OneScriptTerminalGui.RevertEqualsObj(Base_obj.M_Window.Subviews[0].SuperView.SuperView.SuperView.GetTopSuperView()).dll_obj;
            }
            catch (Exception)
            {
                return OneScriptTerminalGui.RevertEqualsObj(Base_obj.M_Window.Subviews[0].SuperView.SuperView.GetTopSuperView()).dll_obj;
            }
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
        public void Add(IValue p1)
        {
            Base_obj.Add(((dynamic)p1).Base_obj);
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
            Base_obj.M_Window.Subviews[0].SendSubviewToBack(((dynamic)p1).Base_obj.M_View);
        }

        [ContextMethod("НаПереднийПлан", "BringSubviewToFront")]
        public void BringSubviewToFront(IValue p1)
        {
            Base_obj.M_Window.Subviews[0].BringSubviewToFront(((dynamic)p1).Base_obj.M_View);
        }

        [ContextMethod("НаШагВперед", "BringSubviewForward")]
        public void BringSubviewForward(IValue p1)
        {
            Base_obj.M_Window.Subviews[0].BringSubviewForward(((dynamic)p1).Base_obj.M_View);
        }

        [ContextMethod("НаШагНазад", "SendSubviewBackwards")]
        public void SendSubviewBackwards(IValue p1)
        {
            Base_obj.M_Window.Subviews[0].SendSubviewBackwards(((dynamic)p1).Base_obj.M_View);
        }

        [ContextMethod("Ниже", "PlaceBottom")]
        public void PlaceBottom(IValue p1, int p2)
        {
            Base_obj.PlaceBottom(((dynamic)p1.AsObject()).Base_obj, p2 - 1);
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
                return new TfSize(MaxWidthLine + 2 + offsetWidth, MaxLines + 2 + offsetHeight);
            }
            catch
            {
                return null;
            }
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
