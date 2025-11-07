using System;
using ScriptEngine.Machine.Contexts;

namespace ostgui
{
    public class TreeStyle
    {
        public TfTreeStyle dll_obj;
        public Terminal.Gui.Trees.TreeStyle M_TreeStyle;

        public TreeStyle()
        {
            M_TreeStyle = new Terminal.Gui.Trees.TreeStyle();
            Utils.AddToHashtable(M_TreeStyle, this);
        }

        public bool HighlightModelTextOnly
        {
            get { return M_TreeStyle.HighlightModelTextOnly; }
            set { M_TreeStyle.HighlightModelTextOnly = value; }
        }

        public bool InvertExpandSymbolColors
        {
            get { return M_TreeStyle.InvertExpandSymbolColors; }
            set { M_TreeStyle.InvertExpandSymbolColors = value; }
        }

        public bool ShowBranchLines
        {
            get { return M_TreeStyle.ShowBranchLines; }
            set { M_TreeStyle.ShowBranchLines = value; }
        }

        public bool LeaveLastRow
        {
            get { return M_TreeStyle.LeaveLastRow; }
            set { M_TreeStyle.LeaveLastRow = value; }
        }

        public string ExpandableSymbol
        {
            get { return M_TreeStyle.ExpandableSymbol.ToString(); }
            set { M_TreeStyle.ExpandableSymbol = new Rune(value.ToCharArray()[0]); }
        }

        public string CollapseableSymbol
        {
            get { return M_TreeStyle.CollapseableSymbol.ToString(); }
            set { M_TreeStyle.CollapseableSymbol = new Rune(value.ToCharArray()[0]); }
        }

        public bool ColorExpandSymbol
        {
            get { return M_TreeStyle.ColorExpandSymbol; }
            set { M_TreeStyle.ColorExpandSymbol = value; }
        }
    }

    [ContextClass("ТфСтильДерева", "TfTreeStyle")]
    public class TfTreeStyle : AutoContext<TfTreeStyle>
    {

        public TfTreeStyle()
        {
            TreeStyle TreeStyle1 = new TreeStyle();
            TreeStyle1.dll_obj = this;
            Base_obj = TreeStyle1;
        }

        public TreeStyle Base_obj;

        [ContextProperty("ВыделятьТолькоТекст", "HighlightModelTextOnly")]
        public bool HighlightModelTextOnly
        {
            get { return Base_obj.HighlightModelTextOnly; }
            set { Base_obj.HighlightModelTextOnly = value; }
        }

        [ContextProperty("ИнверсияСимволов", "InvertExpandSymbolColors")]
        public bool InvertExpandSymbolColors
        {
            get { return Base_obj.InvertExpandSymbolColors; }
            set { Base_obj.InvertExpandSymbolColors = value; }
        }

        [ContextProperty("ПоказатьВертикальные", "ShowBranchLines")]
        public bool ShowBranchLines
        {
            get { return Base_obj.ShowBranchLines; }
            set { Base_obj.ShowBranchLines = value; }
        }

        [ContextProperty("ПоследняяСтрока", "LeaveLastRow")]
        public bool LeaveLastRow
        {
            get { return Base_obj.LeaveLastRow; }
            set { Base_obj.LeaveLastRow = value; }
        }

        [ContextProperty("СимволРазворачивания", "ExpandableSymbol")]
        public string ExpandableSymbol
        {
            get { return Base_obj.ExpandableSymbol; }
            set { Base_obj.ExpandableSymbol = value; }
        }

        [ContextProperty("СимволСворачивания", "CollapseableSymbol")]
        public string CollapseableSymbol
        {
            get { return Base_obj.CollapseableSymbol; }
            set { Base_obj.CollapseableSymbol = value; }
        }

        [ContextProperty("ЦветныеСимволы", "ColorExpandSymbol")]
        public bool ColorExpandSymbol
        {
            get { return Base_obj.ColorExpandSymbol; }
            set { Base_obj.ColorExpandSymbol = value; }
        }

    }
}
