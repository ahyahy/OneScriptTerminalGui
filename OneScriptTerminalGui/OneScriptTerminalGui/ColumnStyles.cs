using ScriptEngine.Machine.Contexts;
using System.Collections.Generic;

namespace ostgui
{
    public class ColumnStyles : Dictionary<System.Data.DataColumn, Terminal.Gui.TableView.ColumnStyle>
    {
        public TfColumnStyles dll_obj;
        public Dictionary<System.Data.DataColumn, Terminal.Gui.TableView.ColumnStyle> M_ColumnStyles;

        public ColumnStyles()
        {
            M_ColumnStyles = new Dictionary<System.Data.DataColumn, Terminal.Gui.TableView.ColumnStyle>();
            Utils.AddToHashtable(M_ColumnStyles, this);
        }

        public ColumnStyles(Dictionary<System.Data.DataColumn, Terminal.Gui.TableView.ColumnStyle> p1)
        {
            M_ColumnStyles = p1;
        }

        public ColumnStyles(ostgui.ColumnStyles p1)
        {
            M_ColumnStyles = p1.M_ColumnStyles;
        }

        public new int Count
        {
            get
            {
                int count = 0;
                foreach (KeyValuePair<System.Data.DataColumn, Terminal.Gui.TableView.ColumnStyle> DictionaryEntry in M_ColumnStyles)
                {
                    count = count + 1;
                }
                return count;
            }
        }

        public new object this[System.Data.DataColumn index]
        {
            get { return M_ColumnStyles[index]; }
        }

        public new System.Collections.IEnumerator GetEnumerator()
        {
            return M_ColumnStyles.GetEnumerator();
        }

        public new void Add(System.Data.DataColumn key, Terminal.Gui.TableView.ColumnStyle item)
        {
            M_ColumnStyles.Add(key, item);
        }

        public new void Remove(System.Data.DataColumn index)
        {
            M_ColumnStyles.Remove(index);
        }
    }

    [ContextClass("ТфСтилиКолонки", "TfColumnStyles")]
    public class TfColumnStyles : AutoContext<TfColumnStyles>
    {

        public TfColumnStyles()
        {
            ColumnStyles ColumnStyles1 = new ColumnStyles();
            ColumnStyles1.dll_obj = this;
            Base_obj = ColumnStyles1;
        }

        public ColumnStyles Base_obj;

        [ContextProperty("Количество", "Count")]
        public int Count
        {
            get { return Base_obj.Count; }
        }

        [ContextMethod("Добавить", "Add")]
        public void Add(TfDataColumn key, TfColumnStyle item)
        {
            Base_obj.Add(key.Base_obj.M_DataColumn, item.Base_obj.M_ColumnStyle);
        }

        [ContextMethod("Удалить", "Remove")]
        public void Remove(TfDataColumn p1)
        {
            Base_obj.Remove(p1.Base_obj.M_DataColumn);
        }

    }
}
