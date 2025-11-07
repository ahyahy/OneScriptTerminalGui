using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace ostgui
{
    public class DataRow
    {
        public TfDataRow dll_obj;
        public System.Data.DataRow M_DataRow;

        public DataRow(ostgui.DataRow p1)
        {
            M_DataRow = p1.M_DataRow;
            Utils.AddToHashtable(M_DataRow, this);
        }

        public DataRow(System.Data.DataRow p1)
        {
            M_DataRow = p1;
            Utils.AddToHashtable(M_DataRow, this);
        }

        public object get_Item(object index)
        {
            return (object)new DataItem(M_DataRow, index);
        }

        public int RowState
        {
            get { return (int)M_DataRow.RowState; }
        }

        public ostgui.DataTable Table
        {
            get { return ((ostgui.DataTableEx)M_DataRow.Table).M_Object; }
        }

        public void AcceptChanges()
        {
            M_DataRow.AcceptChanges();
        }

        public void BeginEdit()
        {
            M_DataRow.BeginEdit();
        }

        public void CancelEdit()
        {
            M_DataRow.CancelEdit();
        }

        public void Delete()
        {
            M_DataRow.Delete();
        }

        public void EndEdit()
        {
            M_DataRow.EndEdit();
        }

        public void RejectChanges()
        {
            M_DataRow.RejectChanges();
        }

        public void SetItem(object index, object item)
        {
            if (index is string)
            {
                M_DataRow[(string)index] = item;
            }
            else
            {
                M_DataRow[(int)index] = item;
            }
            //System.Windows.Forms.Application.DoEvents();
        }
    }

    [ContextClass("ТфСтрокаДанных", "TfDataRow")]
    public class TfDataRow : AutoContext<TfDataRow>
    {

        public TfDataRow(DataRow p1)
        {
            DataRow DataRow1 = p1;
            DataRow1.dll_obj = this;
            Base_obj = DataRow1;
        }

        public DataRow Base_obj;

        [ContextProperty("Таблица", "Table")]
        public TfDataTable Table
        {
            get { return (TfDataTable)Utils.RevertObj(Base_obj.Table); }
        }

        [ContextMethod("Получить", "Get")]
        public TfDataItem Get(IValue p1)
        {
            if (p1.SystemType.Name == "Строка")
            {
                return new TfDataItem((DataItem)Base_obj.get_Item(Utils.ToString(p1)));
            }
            return new TfDataItem((DataItem)Base_obj.get_Item(Utils.ToInt32(p1)));
        }

        [ContextMethod("Удалить", "Delete")]
        public void Delete()
        {
            Base_obj.Delete();
        }

        [ContextMethod("УстановитьЭлемент", "SetItem")]
        public void SetItem(IValue p1, IValue p2)
        {
            dynamic p3 = p1;
            if (Utils.IsString(p1))
            {
                p3 = p1.AsString();
            }
            else if (Utils.IsNumber(p1))
            {
                p3 = Utils.ToInt32(p1);
            }

            if (p2.GetType().ToString().Contains("ostgui."))
            {
                Base_obj.SetItem(p3, Utils.RevertObj(p2));
            }
            else if (Utils.IsString(p2))
            {
                Base_obj.SetItem(p3, p2.AsString());
            }
            else if (Utils.IsBoolean(p2))
            {
                Base_obj.SetItem(p3, p2.AsBoolean());
            }
            else if (Utils.IsDateTime(p2))
            {
                Base_obj.SetItem(p3, new System.DateTime(
                    p2.AsDate().Year,
                    p2.AsDate().Month,
                    p2.AsDate().Day,
                    p2.AsDate().Hour,
                    p2.AsDate().Minute,
                    p2.AsDate().Second
                    ));
            }
            else if (Utils.IsNumber(p2))
            {
                Base_obj.SetItem(p3, p2.AsNumber());
            }
        }

    }
}
