using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ostgui
{
    [ContextClass("ТфВертикальноеВыравниваниеТекста", "TfVerticalTextAlignment")]
    public class TfVerticalTextAlignment : AutoContext<TfVerticalTextAlignment>, ICollectionContext, IEnumerable<IValue>
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

        public TfVerticalTextAlignment()
        {
            _list = new List<decimal>
            {
                Top,
                Bottom,
                Justified,
                Middle,
            }.Select(ValueFactory.Create).ToList();
        }

        private static readonly Dictionary<decimal, string> namesRu = new Dictionary<decimal, string>
        {
            {0, "Верх"},
            {1, "Низ"},
            {3, "Подобранный"},
            {2, "Середина"},
        };

        private static readonly Dictionary<decimal, string> namesEn = new Dictionary<decimal, string>
        {
            {0, "Top"},
            {1, "Bottom"},
            {3, "Justified"},
            {2, "Middle"},
        };

        [ContextProperty("Верх", "Top")]
        public decimal Top => 0;

        [ContextProperty("Низ", "Bottom")]
        public decimal Bottom => 1;

        [ContextProperty("Подобранный", "Justified")]
        public decimal Justified => 3;

        [ContextProperty("Середина", "Middle")]
        public decimal Middle => 2;
    }
}
