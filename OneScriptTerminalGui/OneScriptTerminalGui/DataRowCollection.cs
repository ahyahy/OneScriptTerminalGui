using System;
using System.Collections;
using ScriptEngine.Machine.Contexts;

namespace ostgui
{
    public class DataRowCollection : ICollection, IEnumerable, IEnumerator
    {
        public TfDataRowCollection dll_obj;
        private System.Collections.IEnumerator Enumerator;
        public System.Data.DataRowCollection M_DataRowCollection;

        public DataRowCollection(System.Data.DataRowCollection p1)
        {
            M_DataRowCollection = p1;
        }

        public int Count
        {
            get { return M_DataRowCollection.Count; }
        }

        public object Current
        {
            get { return new DataRow((System.Data.DataRow)Enumerator.Current); }
        }

        public bool IsSynchronized
        {
            get { return M_DataRowCollection.IsSynchronized; }
        }

        public void Reset()
        {
            Enumerator.Reset();
        }

        public object SyncRoot
        {
            get { return M_DataRowCollection.SyncRoot; }
        }

        public ostgui.DataRow this[int index]
        {
            get { return new DataRow(M_DataRowCollection[index]); }
            set
            {
            }
        }

        public ostgui.DataRow Add(DataRow p1)
        {
            M_DataRowCollection.Add(p1.M_DataRow);
            return p1;
        }

        public void Clear()
        {
            M_DataRowCollection.Clear();
        }

        public void CopyTo(Array array, int index)
        {
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            Enumerator = M_DataRowCollection.GetEnumerator();
            return (IEnumerator)this;
        }

        public ostgui.DataRow InsertAt(ostgui.DataRow p1, int index)
        {
            M_DataRowCollection.InsertAt(p1.M_DataRow, index);
            return p1;
        }

        public bool MoveNext()
        {
            return Enumerator.MoveNext();
        }

        public void Remove(DataRow p1)
        {
            M_DataRowCollection.Remove(p1.M_DataRow);
        }

        public void RemoveAt(int p1)
        {
            M_DataRowCollection.RemoveAt(p1);
        }
    }

    [ContextClass("ТфСтрокиДанных", "TfDataRowCollection")]
    public class TfDataRowCollection : AutoContext<TfDataRowCollection>
    {

        public TfDataRowCollection(DataRowCollection p1)
        {
            DataRowCollection DataRowCollection1 = p1;
            DataRowCollection1.dll_obj = this;
            Base_obj = DataRowCollection1;
        }

        public DataRowCollection Base_obj;

        [ContextProperty("Количество", "Count")]
        public int Count
        {
            get { return Base_obj.Count; }
        }

        [ContextMethod("Вставить", "InsertAt")]
        public TfDataRow InsertAt(TfDataRow p1, int p2)
        {
            return new TfDataRow(Base_obj.InsertAt(p1.Base_obj, p2));
        }

        [ContextMethod("Добавить", "Add")]
        public TfDataRow Add(TfDataRow p1)
        {
            return new TfDataRow(Base_obj.Add(p1.Base_obj));
        }

        [ContextMethod("Очистить", "Clear")]
        public void Clear()
        {
            Base_obj.Clear();
        }

        [ContextMethod("Получить", "Get")]
        public TfDataRow Get(int p1)
        {
            return new TfDataRow(Base_obj[p1]);
        }

        [ContextMethod("Удалить", "Remove")]
        public void Remove(TfDataRow p1)
        {
            Base_obj.Remove(p1.Base_obj);
        }

        [ContextMethod("УдалитьПоИндексу", "RemoveAt")]
        public void RemoveAt(int p1)
        {
            Base_obj.RemoveAt(p1);
        }

        [ContextMethod("Элемент", "Item")]
        public TfDataRow Item(int p1, TfDataRow p2 = null)
        {
            if (p2 != null)
            {
                Base_obj.RemoveAt(p1);
                Base_obj.InsertAt(p2.Base_obj, p1);
                return new TfDataRow(Base_obj[p1]);
            }
            else
            {
                return new TfDataRow(Base_obj[p1]);
            }
        }

    }
}
