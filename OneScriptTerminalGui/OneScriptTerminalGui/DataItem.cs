using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace ostgui
{
    public class DataItem
    {
        public TfDataItem dll_obj;
        public object Index;
        public System.Data.DataRow M_DataRow;

        public DataItem()
        {
        }

        public DataItem(ostgui.DataItem p1)
        {
            M_DataRow = p1.M_DataRow;
            Index = p1.Index;
        }

        public DataItem(System.Data.DataRow p1, object p2)
        {
            M_DataRow = p1;
            Index = p2;
        }

        public ostgui.DataRow DataRow
        {
            get { return new DataRow(M_DataRow); }
            set { M_DataRow = value.M_DataRow; }
        }

        public object Value
        {
            get
            {
                if (Index != null)
                {
                    if (Index.GetType() == typeof(int))
                    {
                        return M_DataRow[Utils.ToInt32(Index)];
                    }
                    if (Index.GetType() == typeof(string))
                    {
                        return M_DataRow[Utils.ToString(Index)];
                    }
                }
                return null;
            }
            set
            {
                if (Index is string)
                {
                    M_DataRow[(string)Index] = value;
                }
                else
                {
                    M_DataRow[(int)Index] = value;
                }
            }
        }
    }

    [ContextClass("ТфЭлементДанных", "TfDataItem")]
    public class TfDataItem : AutoContext<TfDataItem>
    {

        public TfDataItem()
        {
            DataItem DataItem1 = new DataItem();
            DataItem1.dll_obj = this;
            Base_obj = DataItem1;
        }

        public TfDataItem(DataItem p1)
        {
            DataItem DataItem1 = p1;
            DataItem1.dll_obj = this;
            Base_obj = DataItem1;
        }

        public DataItem Base_obj;

        [ContextProperty("Значение", "Value")]
        public IValue Value
        {
            get { return Utils.RevertObj(Base_obj.Value); }
            set
            {
                Base_obj.Value = Utils.DefineTypeIValue(value);
            }
        }

        [ContextProperty("СтрокаДанных", "DataRow")]
        public TfDataRow DataRow
        {
            get { return ((DataRow)Utils.RevertEqualsObj(Base_obj.M_DataRow)).dll_obj; }
            set { Base_obj.DataRow = value.Base_obj; }
        }

    }
}
