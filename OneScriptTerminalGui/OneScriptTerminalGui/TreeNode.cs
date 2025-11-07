using System.Collections.Generic;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.HostedScript.Library;
using ScriptEngine.Machine;

namespace ostgui
{
    public class TreeNode
    {
        public TfTreeNode dll_obj;
        public Terminal.Gui.Trees.TreeNode M_TreeNode;

        public TreeNode()
        {
            M_TreeNode = new Terminal.Gui.Trees.TreeNode();
            Utils.AddToHashtable(M_TreeNode, this);
        }

        public new string ToString()
        {
            return M_TreeNode.ToString();
        }

        public IValue Tag
        {
            get { return M_TreeNode.TagProp; }
            set { M_TreeNode.TagProp = value; }
        }

        public string Text
        {
            get { return M_TreeNode.Text; }
            set { M_TreeNode.Text = value; }
        }

        public IList<Terminal.Gui.Trees.ITreeNode> Children
        {
            get { return M_TreeNode.Children; }
            set { M_TreeNode.Children = value; }
        }

        public void Add(TreeNode p1)
        {
            M_TreeNode.Children.Add(p1.M_TreeNode);
        }
    }

    [ContextClass("ТфУзелДерева", "TfTreeNode")]
    public class TfTreeNode : AutoContext<TfTreeNode>
    {

        public TfTreeNode()
        {
            TreeNode TreeNode1 = new TreeNode();
            TreeNode1.dll_obj = this;
            Base_obj = TreeNode1;
        }

        public TreeNode Base_obj;

        [ContextProperty("Метка", "Tag")]
        public IValue Tag
        {
            get { return Base_obj.Tag; }
            set { Base_obj.Tag = value; }
        }

        [ContextProperty("Текст", "Text")]
        public string Text
        {
            get { return Base_obj.Text; }
            set { Base_obj.Text = value; }
        }

        [ContextProperty("Узлы", "Children")]
        public ArrayImpl Children
        {
            get { return Utils.TreeNodeToArray(Base_obj.M_TreeNode.Children); }
        }

        [ContextMethod("Добавить", "Add")]
        public TfTreeNode Add(TfTreeNode p1)
        {
            Base_obj.Add(p1.Base_obj);
            return p1;
        }

    }
}
