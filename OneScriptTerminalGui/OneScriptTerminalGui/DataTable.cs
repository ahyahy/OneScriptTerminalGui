using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library;

namespace ostgui
{
    public class DataTableEx : System.Data.DataTable
    {
        public ostgui.DataTable M_Object;
    }

    public class DataTable
    {
        public TfDataTable dll_obj;
        public DataTableEx M_DataTable;

        public DataTable()
        {
            M_DataTable = new DataTableEx();
            M_DataTable.M_Object = this;
            Utils.AddToHashtable(M_DataTable, this);
        }

        public DataTable(ostgui.DataTable p1)
        {
            M_DataTable = p1.M_DataTable;
            M_DataTable.M_Object = this;
            Utils.AddToHashtable(M_DataTable, this);
        }

        public DataTable(string p1)
        {
            M_DataTable = new DataTableEx();
            M_DataTable.M_Object = this;
            M_DataTable.TableName = p1;
            Utils.AddToHashtable(M_DataTable, this);
        }

        public DataTable(System.Data.DataTable p1)
        {
            M_DataTable = (DataTableEx)p1;
            M_DataTable.M_Object = this;
            Utils.AddToHashtable(M_DataTable, this);
        }

        public ostgui.DataColumnCollection Columns
        {
            get { return new DataColumnCollection(M_DataTable.Columns); }
        }

        public ostgui.DataColumn get_Column(object p1)
        {
            if (p1 is int)
            {
                return ((DataColumnEx)(M_DataTable.Columns[Utils.ToInt32(p1)])).M_Object;
            }
            else
            {
                return ((DataColumnEx)(M_DataTable.Columns[Utils.ToString(p1)])).M_Object;
            }
        }

        public ostgui.DataRowCollection Rows
        {
            get { return new DataRowCollection(M_DataTable.Rows); }
        }

        public string TableName
        {
            get { return M_DataTable.TableName; }
            set { M_DataTable.TableName = value; }
        }

        public void AcceptChanges()
        {
            M_DataTable.AcceptChanges();
        }

        public ostgui.DataTable Clone()
        {
            return new DataTable(M_DataTable.Clone());
        }

        public ostgui.DataTable Copy()
        {
            return new DataTable(M_DataTable.Copy());
        }

        public ostgui.DataRow NewRow()
        {
            return new DataRow(M_DataTable.NewRow());
        }

        public void RejectChanges()
        {
            M_DataTable.RejectChanges();
        }

        public object[] Select(string filter)
        {
            System.Data.DataRow[] dataRowArray = M_DataTable.Select(filter);
            int num1 = dataRowArray.Length;
            object[] objArray = new object[num1];
            for (int i = 0; i < dataRowArray.Length; i++)
            {
                objArray[i] = (object)new DataRow(dataRowArray[i]);
            }
            return objArray;
        }

        public void Sort(string expression)
        {
            if (M_DataTable.Rows.Count > 0)
            {
                System.Data.DataTable DataTable1 = M_DataTable.Copy();
                M_DataTable.Clear();
                System.Data.DataRow[] DataRowArray1 = DataTable1.Select((string)null, expression);
                for (int i = 0; i < DataRowArray1.Length; i++)
                {
                    M_DataTable.ImportRow(DataRowArray1[i]);
                }
            }
        }
    }

    [ContextClass("ТфТаблицаДанных", "TfDataTable")]
    public class TfDataTable : AutoContext<TfDataTable>
    {

        private TfDataColumnCollection columns;
        private TfDataRowCollection rows;

        public TfDataTable()
        {
            DataTable DataTable1 = new DataTable();
            DataTable1.dll_obj = this;
            Base_obj = DataTable1;
            columns = new TfDataColumnCollection(Base_obj.Columns);
            rows = new TfDataRowCollection(Base_obj.Rows);
        }

        public TfDataTable(string p1)
        {
            DataTable DataTable1 = new DataTable(p1);
            DataTable1.dll_obj = this;
            Base_obj = DataTable1;
            columns = new TfDataColumnCollection(Base_obj.Columns);
            rows = new TfDataRowCollection(Base_obj.Rows);
        }

        public TfDataTable(DataTable p1)
        {
            DataTable DataTable1 = p1;
            DataTable1.dll_obj = this;
            Base_obj = DataTable1;
            columns = new TfDataColumnCollection(Base_obj.Columns);
            rows = new TfDataRowCollection(Base_obj.Rows);
        }

        public DataTable Base_obj;

        [ContextProperty("Колонки", "Columns")]
        public TfDataColumnCollection Columns
        {
            get { return columns; }
        }

        [ContextProperty("Строки", "Rows")]
        public TfDataRowCollection Rows
        {
            get { return rows; }
        }

        [ContextMethod("Выбрать", "Select")]
        public ArrayImpl Select(string p1)
        {
            ArrayImpl ArrayImpl1 = new ArrayImpl();
            try
            {
                object[] objects = Base_obj.Select(p1);
                for (int i = 0; i < objects.Length; i++)
                {
                    ArrayImpl1.Add(new TfDataRow((ostgui.DataRow)objects[i]));
                }
            }
            catch
            {
            }
            return ArrayImpl1;
        }

        [ContextMethod("ВыгрузитьКолонку", "UnloadColumn")]
        public ArrayImpl UnloadColumn(IValue p1)
        {
            ArrayImpl ArrayImpl1 = new ArrayImpl();
            if (Utils.IsNumber(p1))
            {
                for (int i = 0; i < Base_obj.Rows.Count; i++)
                {
                    dynamic p2 = Base_obj.Rows[i].get_Item(Utils.ToInt32(p1));
                    ArrayImpl1.Add(p2.Value);
                }
                return ArrayImpl1;
            }
            else if (Utils.IsString(p1))
            {
                for (int i = 0; i < Base_obj.Rows.Count; i++)
                {
                    dynamic p2 = Base_obj.Rows[i].get_Item(p1.AsString());
                    ArrayImpl1.Add(p2.Value);
                }
                return ArrayImpl1;
            }
            else if (Utils.IsType<TfDataColumn>(p1))
            {
                for (int i = 0; i < Base_obj.Rows.Count; i++)
                {
                    dynamic p2 = Base_obj.Rows[i].get_Item(((TfDataColumn)p1.AsObject()).Base_obj.ColumnName);
                    ArrayImpl1.Add(p2.Value);
                }
                return ArrayImpl1;
            }
            return null;
        }

        [ContextMethod("ЗагрузитьКолонку", "LoadColumn")]
        public void LoadColumn(ArrayImpl p1, IValue p2)
        {
            dynamic p3 = null;
            if (Utils.IsNumber(p2))
            {
                p3 = Utils.ToInt32(p2);
            }
            else if (Utils.IsString(p2))
            {
                p3 = p2.AsString();
            }
            else if (Utils.IsType<TfDataColumn>(p2))
            {
                p3 = ((TfDataColumn)p2.AsObject()).Base_obj.ColumnName;
            }

            for (int i = 0; i < p1.Count(); i++)
            {
                Base_obj.Rows[i].SetItem(p3, Utils.DefineTypeIValue(p1.Get(i)));
            }
        }

        [ContextMethod("Клонировать", "Clone")]
        public TfDataTable Clone()
        {
            return new TfDataTable(Base_obj.Clone());
        }

        [ContextMethod("Колонка", "Column")]
        public TfDataColumn Column(IValue p1)
        {
            if (Utils.IsNumber(p1))
            {
                return new TfDataColumn(Base_obj.get_Column(Utils.ToInt32(p1)));
            }
            else if (Utils.IsString(p1))
            {
                return new TfDataColumn(Base_obj.get_Column(Utils.ToString(p1)));
            }
            return null;
        }

        [ContextMethod("Колонки", "Columns")]
        public TfDataColumn Columns2(IValue p1)
        {
            if (Utils.IsNumber(p1))
            {
                return ((DataColumnEx)Base_obj.M_DataTable.Columns[Utils.ToInt32(p1)]).M_Object.dll_obj;
            }
            if (Utils.IsString(p1))
            {
                return ((DataColumnEx)Base_obj.M_DataTable.Columns[p1.AsString()]).M_Object.dll_obj;
            }
            return null;
        }

        [ContextMethod("Копировать", "Copy")]
        public TfDataTable Copy()
        {
            return new TfDataTable(Base_obj.Copy());
        }

        [ContextMethod("НоваяСтрока", "NewRow")]
        public TfDataRow NewRow()
        {
            return new TfDataRow(Base_obj.NewRow());
        }

        [ContextMethod("Строки", "Rows")]
        public TfDataRow Rows2(int p1)
        {
            return new TfDataRow(Base_obj.Rows[p1]);
        }

    }
}
