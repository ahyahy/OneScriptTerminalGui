using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library.ValueList;
using System.Collections;

namespace ostgui
{
    public class ScrollBarView : View
    {
        public new TfScrollBarView dll_obj;
        public Terminal.Gui.ScrollBarView M_ScrollBarView;

        public ScrollBarView()
        {
            M_ScrollBarView = new Terminal.Gui.ScrollBarView();
            base.M_View = M_ScrollBarView;
            Utils.AddToHashtable(M_ScrollBarView, this);
        }

        public ScrollBarView(View host, bool isVertical, bool showBothScrollIndicator = true)
        {
            M_ScrollBarView = new Terminal.Gui.ScrollBarView(host.M_View, isVertical, showBothScrollIndicator);
            base.M_View = M_ScrollBarView;
            Utils.AddToHashtable(M_ScrollBarView, this);
            SetActions(M_ScrollBarView, host);
        }

        private void SetActions(Terminal.Gui.ScrollBarView scrollBarView, View host)
        {
            if (host.GetType() == typeof(ListView))
            {
                Terminal.Gui.ListView listView = (Terminal.Gui.ListView)host.M_View;

                scrollBarView.ChangedPosition += () =>
                {
                    listView.TopItem = M_ScrollBarView.Position;
                    if (listView.TopItem != M_ScrollBarView.Position)
                    {
                        M_ScrollBarView.Position = listView.TopItem;
                    }
                    listView.SetNeedsDisplay();
                };

                scrollBarView.OtherScrollBarView.ChangedPosition += () =>
                {
                    listView.LeftItem = M_ScrollBarView.OtherScrollBarView.Position;
                    if (listView.LeftItem != M_ScrollBarView.OtherScrollBarView.Position)
                    {
                        M_ScrollBarView.OtherScrollBarView.Position = listView.LeftItem;
                    }
                    listView.SetNeedsDisplay();
                };

                listView.DrawContent += (e) =>
                {
                    M_ScrollBarView.Size = listView.Source.Count;
                    M_ScrollBarView.Position = listView.TopItem;
                    M_ScrollBarView.OtherScrollBarView.Size = listView.Maxlength + 10;
                    M_ScrollBarView.OtherScrollBarView.Position = listView.LeftItem;
                    M_ScrollBarView.OtherScrollBarView.ColorScheme = M_ScrollBarView.ColorScheme;
                    M_ScrollBarView.Refresh();
                };
            }
        }

        public new string ToString()
        {
            return M_ScrollBarView.ToString();
        }

        public new Toplevel GetTopSuperView()
        {
            return Utils.RevertEqualsObj(M_ScrollBarView.GetTopSuperView());
        }

        public bool AutoHideScrollBars
        {
            get { return M_ScrollBarView.AutoHideScrollBars; }
            set { M_ScrollBarView.AutoHideScrollBars = value; }
        }

        public bool IsVertical
        {
            get { return M_ScrollBarView.IsVertical; }
            set { M_ScrollBarView.IsVertical = value; }
        }

        public IValue Host
        {
            get { return Utils.RevertEqualsObj(M_ScrollBarView.Host).dll_obj; }
        }

        public int Position
        {
            get { return M_ScrollBarView.Position; }
            set { M_ScrollBarView.Position = value; }
        }

        public int Size
        {
            get { return M_ScrollBarView.Size; }
        }

        public bool KeepContentAlwaysInViewport
        {
            get { return M_ScrollBarView.KeepContentAlwaysInViewport; }
            set { M_ScrollBarView.KeepContentAlwaysInViewport = value; }
        }
    }

    [ContextClass("ТфПолосаПрокрутки", "TfScrollBarView")]
    public class TfScrollBarView : AutoContext<TfScrollBarView>
    {

        public TfScrollBarView()
        {
            ScrollBarView ScrollBarView1 = new ScrollBarView();
            ScrollBarView1.dll_obj = this;
            Base_obj = ScrollBarView1;
        }

        public TfScrollBarView(IValue p1, bool p2, bool p3 = true)
        {
            ScrollBarView ScrollBarView1 = new ScrollBarView(((dynamic)p1).Base_obj, p2, p3);
            ScrollBarView1.dll_obj = this;
            Base_obj = ScrollBarView1;
        }

        public ScrollBarView Base_obj;

        [ContextProperty("Вертикально", "IsVertical")]
        public bool IsVertical
        {
            get { return Base_obj.IsVertical; }
            set { Base_obj.IsVertical = value; }
        }

        [ContextProperty("Верх", "Top")]
        public TfPos Top
        {
            get { return new TfPos(Base_obj.Top); }
        }

        [ContextProperty("Владелец", "Host")]
        public IValue Host
        {
            get { return Base_obj.Host; }
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
                    Base_obj.M_ScrollBarView.Y = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_ScrollBarView.Y = Terminal.Gui.Pos.At(Utils.ToInt32(value));
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
                    Base_obj.M_ScrollBarView.X = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_ScrollBarView.X = Terminal.Gui.Pos.At(Utils.ToInt32(value));
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

        [ContextProperty("Размер", "Size")]
        public int Size
        {
            get { return Base_obj.Size; }
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
