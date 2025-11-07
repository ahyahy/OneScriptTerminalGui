using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ostgui
{
    [ContextClass("ТфТипДанных", "TfDataType")]
    public class TfDataType : AutoContext<TfDataType>, ICollectionContext, IEnumerable<IValue>
    {
        private List<IValue> _list;

        public int Count()
        {
            return _list.Count;
        }

        public CollectionEnumerator GetManagedIterator()
        {
            return new CollectionEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<IValue>)_list).GetEnumerator();
        }

        IEnumerator<IValue> IEnumerable<IValue>.GetEnumerator()
        {
            foreach (var item in _list)
            {
                yield return (item as IValue);
            }
        }

        [ContextProperty("Количество", "Count")]
        public int CountProp
        {
            get { return _list.Count; }
        }

        [ContextMethod("Получить", "Get")]
        public IValue Get(int index)
        {
            return _list[index];
        }

        [ContextMethod("Имя")]
        public string NameRu(decimal p1)
        {
            return namesRu.TryGetValue(p1, out string name) ? name : p1.ToString();
        }

        [ContextMethod("Name")]
        public string NameEn(decimal p1)
        {
            return namesEn.TryGetValue(p1, out string name) ? name : p1.ToString();
        }

        public TfDataType()
        {
            _list = new List<decimal>
            {
                Boolean,
                Date,
                Object,
                String,
                Number,
            }.Select(ValueFactory.Create).ToList();
        }

        private static readonly Dictionary<decimal, string> namesRu = new Dictionary<decimal, string>
        {
            {2, "Булево"},
            {3, "Дата"},
            {4, "Объект"},
            {0, "Строка"},
            {1, "Число"},
        };

        private static readonly Dictionary<decimal, string> namesEn = new Dictionary<decimal, string>
        {
            {2, "Boolean"},
            {3, "Date"},
            {4, "Object"},
            {0, "String"},
            {1, "Number"},
        };

        [ContextProperty("Булево", "Boolean")]
        public decimal Boolean => 2;

        [ContextProperty("Дата", "Date")]
        public decimal Date => 3;

        [ContextProperty("Объект", "Object")]
        public decimal Object => 4;

        [ContextProperty("Строка", "String")]
        public decimal String => 0;

        [ContextProperty("Число", "Number")]
        public decimal Number => 1;
    }
}
