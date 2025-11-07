using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ostgui
{
    [ContextClass("ТфСтильГраницы", "TfBorderStyle")]
    public class TfBorderStyle : AutoContext<TfBorderStyle>, ICollectionContext, IEnumerable<IValue>
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

        public TfBorderStyle()
        {
            _list = new List<decimal>
            {
                Double,
                Rounded,
                Single,
                None,
            }.Select(ValueFactory.Create).ToList();
        }

        private static readonly Dictionary<decimal, string> namesRu = new Dictionary<decimal, string>
        {
            {2, "Двойная"},
            {3, "Закругленная"},
            {1, "Одинарная"},
            {0, "Отсутствие"},
        };

        private static readonly Dictionary<decimal, string> namesEn = new Dictionary<decimal, string>
        {
            {2, "Double"},
            {3, "Rounded"},
            {1, "Single"},
            {0, "None"},
        };

        [ContextProperty("Двойная", "Double")]
        public decimal Double => 2;

        [ContextProperty("Закругленная", "Rounded")]
        public decimal Rounded => 3;

        [ContextProperty("Одинарная", "Single")]
        public decimal Single => 1;

        [ContextProperty("Отсутствие", "None")]
        public decimal None => 0;
    }
}
