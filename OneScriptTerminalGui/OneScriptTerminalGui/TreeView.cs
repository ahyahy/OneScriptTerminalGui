using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library.ValueList;
using System.Collections;
using ScriptEngine.HostedScript.Library;
using System.Linq;

namespace ostgui
{
    public class TreeView : View
    {
        public new TfTreeView dll_obj;
        public Terminal.Gui.TreeView M_TreeView;

        public TreeView()
        {
            M_TreeView = new Terminal.Gui.TreeView();
            base.M_View = M_TreeView;
            Utils.AddToHashtable(M_TreeView, this);

            M_TreeView.ObjectActivated += M_TreeView_ObjectActivated;
            M_TreeView.SelectionChanged += M_TreeView_SelectionChanged;
        }

        private void M_TreeView_SelectionChanged(object sender, Terminal.Gui.Trees.SelectionChangedEventArgs<Terminal.Gui.Trees.ITreeNode> e)
        {
            if (dll_obj.SelectionChanged != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.SelectionChanged);

                Terminal.Gui.Trees.TreeNode nodeOld = null;
                try
                {
                    nodeOld = (Terminal.Gui.Trees.TreeNode)e.OldValue;
                }
                catch { }
                if (nodeOld != null)
                {
                    TfEventArgs1.oldNode = Utils.RevertEqualsObj(nodeOld).dll_obj;
                }

                TfEventArgs1.newNode = Utils.RevertEqualsObj(e.NewValue).dll_obj;
                TfEventArgs1.treeView = Utils.RevertEqualsObj(e.Tree).dll_obj;
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(dll_obj.SelectionChanged);
            }
        }

        private void M_TreeView_ObjectActivated(Terminal.Gui.Trees.ObjectActivatedEventArgs<Terminal.Gui.Trees.ITreeNode> obj)
        {
            if (dll_obj.ObjectActivated != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.ObjectActivated);
                TfEventArgs1.treeNode = Utils.RevertEqualsObj(obj.ActivatedObject).dll_obj;
                TfEventArgs1.treeView = Utils.RevertEqualsObj(obj.Tree).dll_obj;
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(dll_obj.ObjectActivated);
            }
        }

        public new string ToString()
        {
            return M_TreeView.ToString();
        }

        public new Toplevel GetTopSuperView()
        {
            return Utils.RevertEqualsObj(M_TreeView.GetTopSuperView());
        }

        public int ContentHeight
        {
            get { return M_TreeView.ContentHeight; }
        }

        public int MaxDepth
        {
            get { return M_TreeView.MaxDepth; }
            set { M_TreeView.MaxDepth = value; }
        }

        public int ObjectActivationKey
        {
            get { return (int)M_TreeView.ObjectActivationKey; }
            set { M_TreeView.ObjectActivationKey = (Terminal.Gui.Key)value; }
        }

        public int ObjectActivationButton
        {
            get { return (int)M_TreeView.ObjectActivationButton; }
            set { M_TreeView.ObjectActivationButton = (Terminal.Gui.MouseFlags)value; }
        }

        public decimal DesiredCursorVisibility
        {
            get { return (decimal)M_TreeView.DesiredCursorVisibility; }
            set { M_TreeView.DesiredCursorVisibility = (Terminal.Gui.CursorVisibility)value; }
        }

        public bool MultiSelect
        {
            get { return M_TreeView.MultiSelect; }
            set { M_TreeView.MultiSelect = value; }
        }

        public bool AllowLetterBasedNavigation
        {
            get { return M_TreeView.AllowLetterBasedNavigation; }
            set { M_TreeView.AllowLetterBasedNavigation = value; }
        }

        public int ScrollOffsetVertical
        {
            get { return M_TreeView.ScrollOffsetVertical; }
            set { M_TreeView.ScrollOffsetVertical = value; }
        }

        public int ScrollOffsetHorizontal
        {
            get { return M_TreeView.ScrollOffsetHorizontal; }
            set { M_TreeView.ScrollOffsetHorizontal = value; }
        }

        public void SelectAll()
        {
            M_TreeView.SelectAll();
        }

        public void AdjustSelectionToBranchStart()
        {
            try
            {
                Terminal.Gui.Trees.ITreeNode parent = M_TreeView.GetParent(M_TreeView.SelectedObject);
                if (parent.Children.Count > 0)
                {
                    M_TreeView.SelectedObject = parent.Children[0];
                }
            }
            catch
            {
                Terminal.Gui.Trees.ITreeNode[] treeNodeArray = M_TreeView.Objects.ToArray();
                if (treeNodeArray.Contains(M_TreeView.SelectedObject))
                {
                    M_TreeView.SelectedObject = treeNodeArray[0];
                }
            }
        }

        public void AdjustSelectionToBranchEnd()
        {
            try
            {
                Terminal.Gui.Trees.ITreeNode parent = M_TreeView.GetParent(M_TreeView.SelectedObject);
                if (parent.Children.Count > 0)
                {
                    M_TreeView.SelectedObject = parent.Children[parent.Children.Count - 1];
                }
            }
            catch
            {
                Terminal.Gui.Trees.ITreeNode[] treeNodeArray = M_TreeView.Objects.ToArray();
                if (treeNodeArray.Contains(M_TreeView.SelectedObject))
                {
                    M_TreeView.SelectedObject = treeNodeArray[treeNodeArray.Length - 1];
                }
            }
        }

        public void GoToEnd()
        {
            M_TreeView.GoToEnd();
        }

        public void GoToFirst()
        {
            M_TreeView.GoToFirst();
        }

        public void AdjustSelection(int p1, bool p2 = false)
        {
            M_TreeView.AdjustSelection(p1, p2);
        }

        public int GetContentWidth(bool p1)
        {
            return M_TreeView.GetContentWidth(p1);
        }

        public void ScrollUp()
        {
            M_TreeView.ScrollUp();
        }

        public void ScrollDown()
        {
            M_TreeView.ScrollDown();
        }

        public void Expand()
        {
            M_TreeView.Expand();
        }

        public void ExpandAll()
        {
            M_TreeView.ExpandAll();
        }

        public void Collapse()
        {
            M_TreeView.Collapse();
        }

        public void CollapseAll()
        {
            M_TreeView.CollapseAll();
        }

        public void ClearObjects()
        {
            M_TreeView.ClearObjects();
        }

        public void AddObject(TreeNode p1)
        {
            M_TreeView.AddObject(p1.M_TreeNode);
        }

        public bool IsSelected(TreeNode p1)
        {
            return M_TreeView.IsSelected(p1.M_TreeNode);
        }

        public void AddObjects(ArrayImpl p1)
        {
            M_TreeView.AddObjects(Utils.ArrayToTreeNode(p1));
        }

        public bool CanExpand(TreeNode p1)
        {
            return M_TreeView.CanExpand(p1.M_TreeNode);
        }

        public void EnsureVisible(TreeNode p1)
        {
            M_TreeView.EnsureVisible(p1.M_TreeNode);
        }

        public void GoTo(TreeNode p1)
        {
            M_TreeView.GoTo(p1.M_TreeNode);
        }

        public IValue GetObjectRow(TreeNode p1)
        {
            var res = M_TreeView.GetObjectRow(p1.M_TreeNode);
            if (res.GetType() == typeof(int))
            {
                return ValueFactory.Create(Convert.ToInt32(res));
            }
            return ValueFactory.Create(Convert.ToBoolean(res));
        }

        public TreeNode GetObjectOnRow(int p1)
        {
            return Utils.RevertEqualsObj(M_TreeView.GetObjectOnRow(p1));
        }

        //public ййййй GetExpandChildren()
        //{
        //    return Base_obj.GetExpandChildren();
        //}

        public TreeNode GetParent(TreeNode p1)
        {
            return Utils.RevertEqualsObj(M_TreeView.GetParent(p1.M_TreeNode));
        }

        public bool IsExpanded(TreeNode p1)
        {
            return M_TreeView.IsExpanded(p1.M_TreeNode);
        }

        public void MovePageUp(bool p1 = false)
        {
            M_TreeView.MovePageUp(p1);
        }

        public void MovePageDown(bool p1 = false)
        {
            M_TreeView.MovePageDown(p1);
        }

        public void Remove(TreeNode p1)
        {
            M_TreeView.Remove(p1.M_TreeNode);
        }

        public TreeStyle Style
        {
            get { return Utils.RevertEqualsObj(M_TreeView.Style); }
            set { M_TreeView.Style = value.M_TreeStyle; }
        }

        public TreeNode SelectedObject
        {
            get { return Utils.RevertEqualsObj(M_TreeView.SelectedObject); }
            set { M_TreeView.SelectedObject = value.M_TreeNode; }
        }
    }

    [ContextClass("ТфДерево", "TfTreeView")]
    public class TfTreeView : AutoContext<TfTreeView>
    {

        public TfTreeView()
        {
            TreeView TreeView1 = new TreeView();
            TreeView1.dll_obj = this;
            Base_obj = TreeView1;
        }

        public TreeView Base_obj;

        [ContextProperty("Верх", "Top")]
        public TfPos Top
        {
            get { return new TfPos(Base_obj.Top); }
        }

        [ContextProperty("Выбранный", "SelectedObject")]
        public TfTreeNode SelectedObject
        {
            get { return Base_obj.SelectedObject.dll_obj; }
            set { Base_obj.SelectedObject = value.Base_obj; }
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

        [ContextProperty("ВысотаСодержимого", "ContentHeight")]
        public int ContentHeight
        {
            get { return Base_obj.ContentHeight; }
        }

        [ContextProperty("Глубина", "MaxDepth")]
        public int MaxDepth
        {
            get { return Base_obj.MaxDepth; }
            set { Base_obj.MaxDepth = value; }
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
                    Base_obj.M_TreeView.Y = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_TreeView.Y = Terminal.Gui.Pos.At(Utils.ToInt32(value));
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
                    Base_obj.M_TreeView.X = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_TreeView.X = Terminal.Gui.Pos.At(Utils.ToInt32(value));
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

        [ContextProperty("КлавишаАктивации", "ObjectActivationKey")]
        public int ObjectActivationKey
        {
            get { return Base_obj.ObjectActivationKey; }
            set { Base_obj.ObjectActivationKey = value; }
        }

        [ContextProperty("КнопкаМышиДляАктивации", "ObjectActivationButton")]
        public int ObjectActivationButton
        {
            get { return Base_obj.ObjectActivationButton; }
            set { Base_obj.ObjectActivationButton = value; }
        }

        [ContextProperty("Корневые", "Objects")]
        public ArrayImpl Objects
        {
            get { return Utils.TreeViewObjectsToArray(Base_obj.M_TreeView.Objects); }
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

        [ContextProperty("МножественныйВыбор", "MultiSelect")]
        public bool MultiSelect
        {
            get { return Base_obj.MultiSelect; }
            set { Base_obj.MultiSelect = value; }
        }

        [ContextProperty("НавигацияБукв", "AllowLetterBasedNavigation")]
        public bool AllowLetterBasedNavigation
        {
            get { return Base_obj.AllowLetterBasedNavigation; }
            set { Base_obj.AllowLetterBasedNavigation = value; }
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

        [ContextProperty("Родитель", "SuperView")]
        public IValue SuperView
        {
            get
            {
                if (Base_obj.M_TreeView.SuperView.GetType().ToString().Contains("+ContentView"))
                {
                    return Utils.RevertEqualsObj(Base_obj.M_TreeView.SuperView.SuperView).dll_obj;
                }
                return Utils.RevertEqualsObj(Base_obj.M_TreeView.SuperView).dll_obj;
            }
        }

        [ContextProperty("СмещениеВертикальное", "ScrollOffsetVertical")]
        public int ScrollOffsetVertical
        {
            get { return Base_obj.ScrollOffsetVertical; }
            set { Base_obj.ScrollOffsetVertical = value; }
        }

        [ContextProperty("СмещениеГоризонтальное", "ScrollOffsetHorizontal")]
        public int ScrollOffsetHorizontal
        {
            get { return Base_obj.ScrollOffsetHorizontal; }
            set { Base_obj.ScrollOffsetHorizontal = value; }
        }

        [ContextProperty("Стиль", "Style")]
        public TfTreeStyle Style
        {
            get { return Base_obj.Style.dll_obj; }
            set { Base_obj.Style = value.Base_obj; }
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

        [ContextProperty("Активирован", "ObjectActivated")]
        public TfAction ObjectActivated { get; set; }

        [ContextProperty("ВыборИзменен", "SelectionChanged")]
        public TfAction SelectionChanged { get; set; }

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

        [ContextMethod("Выбран", "IsSelected")]
        public bool IsSelected(TfTreeNode p1)
        {
            return Base_obj.IsSelected(p1.Base_obj);
        }

        [ContextMethod("ВыбратьВсе", "SelectAll")]
        public void SelectAll()
        {
            Base_obj.SelectAll();
        }

        [ContextMethod("ВыбратьПервый", "AdjustSelectionToBranchStart")]
        public void AdjustSelectionToBranchStart()
        {
            Base_obj.AdjustSelectionToBranchStart();
        }

        [ContextMethod("ВыбратьПоследний", "AdjustSelectionToBranchEnd")]
        public void AdjustSelectionToBranchEnd()
        {
            Base_obj.AdjustSelectionToBranchEnd();
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

        [ContextMethod("ДобавитьУзел", "AddObject")]
        public TfTreeNode AddObject(TfTreeNode p1)
        {
            Base_obj.AddObject(p1.Base_obj);
            return p1;
        }

        [ContextMethod("ДобавитьУзлы", "AddObjects")]
        public void AddObjects(ArrayImpl p1)
        {
            Base_obj.AddObjects(p1);
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

        [ContextMethod("МожноРазвернуть", "CanExpand")]
        public bool CanExpand(TfTreeNode p1)
        {
            return Base_obj.CanExpand(p1.Base_obj);
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

        [ContextMethod("ОбеспечитьОтображение", "EnsureVisible")]
        public void EnsureVisible(TfTreeNode p1)
        {
            Base_obj.EnsureVisible(p1.Base_obj);
        }

        [ContextMethod("Перейти", "GoTo")]
        public void GoTo(TfTreeNode p1)
        {
            Base_obj.GoTo(p1.Base_obj);
        }

        [ContextMethod("ПерейтиВКонец", "GoToEnd")]
        public void GoToEnd()
        {
            Base_obj.GoToEnd();
        }

        [ContextMethod("ПерейтиВНачало", "GoToFirst")]
        public void GoToFirst()
        {
            Base_obj.GoToFirst();
        }

        [ContextMethod("ПереместитьВыбор", "AdjustSelection")]
        public void AdjustSelection(int p1, bool p2 = false)
        {
            Base_obj.AdjustSelection(p1, p2);
        }

        [ContextMethod("ПолучитьВсеВыбранные", "GetAllSelectedObjects")]
        public ArrayImpl GetAllSelectedObjects()
        {
            return Utils.TreeViewObjectsToArray(Base_obj.M_TreeView.GetAllSelectedObjects());
        }

        [ContextMethod("ПолучитьИндексУзла", "GetObjectRow")]
        public IValue GetObjectRow(TfTreeNode p1)
        {
            return Base_obj.GetObjectRow(p1.Base_obj);
        }

        [ContextMethod("ПолучитьПоИндексу", "GetObjectOnRow")]
        public TfTreeNode GetObjectOnRow(int p1)
        {
            try
            {
                return Base_obj.GetObjectOnRow(p1).dll_obj;
            }
            catch
            {
                return null;
            }
        }

        [ContextMethod("ПолучитьРазвернутые", "GetExpandChildren")]
        public ArrayImpl GetExpandChildren(TfTreeNode p1)
        {
            return Utils.TreeViewObjectsToArray(Base_obj.M_TreeView.GetChildren(p1.Base_obj.M_TreeNode));
        }

        [ContextMethod("ПолучитьРодителя", "GetParent")]
        public TfTreeNode GetParent(TfTreeNode p1)
        {
            try
            {
                return Base_obj.GetParent(p1.Base_obj).dll_obj;
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

        [ContextMethod("ПолучитьШиринуСодержимого", "GetContentWidth")]
        public int GetContentWidth(bool p1)
        {
            return Base_obj.GetContentWidth(p1);
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
        public void ScrollUp()
        {
            Base_obj.ScrollUp();
        }

        [ContextMethod("ПрокрутитьВниз", "ScrollDown")]
        public void ScrollDown()
        {
            Base_obj.ScrollDown();
        }

        [ContextMethod("Развернут", "IsExpanded")]
        public bool IsExpanded(TfTreeNode p1)
        {
            return Base_obj.IsExpanded(p1.Base_obj);
        }

        [ContextMethod("Развернуть", "Expand")]
        public void Expand()
        {
            Base_obj.Expand();
        }

        [ContextMethod("РазвернутьВсе", "ExpandAll")]
        public void ExpandAll()
        {
            Base_obj.ExpandAll();
        }

        [ContextMethod("Свернуть", "Collapse")]
        public void Collapse()
        {
            Base_obj.Collapse();
        }

        [ContextMethod("СвернутьВсе", "CollapseAll")]
        public void CollapseAll()
        {
            Base_obj.CollapseAll();
        }

        [ContextMethod("СтраницаВверх", "MovePageUp")]
        public void MovePageUp(bool p1 = false)
        {
            Base_obj.MovePageUp(p1);
        }

        [ContextMethod("СтраницаВниз", "MovePageDown")]
        public void MovePageDown(bool p1 = false)
        {
            Base_obj.MovePageDown(p1);
        }

        [ContextMethod("ТочкаНаЭлементе", "ScreenToView")]
        public TfPoint ScreenToView(int p1, int p2)
        {
            return new TfPoint(Base_obj.ScreenToView(p1, p2));
        }

        [ContextMethod("Удалить", "Remove")]
        public void Remove(TfTreeNode p1)
        {
            Base_obj.Remove(p1.Base_obj);
        }

        [ContextMethod("УдалитьВсеУзлы", "ClearObjects")]
        public void ClearObjects()
        {
            Base_obj.ClearObjects();
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
