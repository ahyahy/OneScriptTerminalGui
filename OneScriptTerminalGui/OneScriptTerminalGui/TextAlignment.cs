using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ostgui
{
    [ContextClass("ТфВыравниваниеТекста", "TfTextAlignment")]
    public class TfTextAlignment : AutoContext<TfTextAlignment>, ICollectionContext, IEnumerable<IValue>
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

        public TfTextAlignment()
        {
            _list = new List<decimal>
            {
                Left,
                Justified,
                Right,
                Centered,
            }.Select(ValueFactory.Create).ToList();
        }

        private static readonly Dictionary<decimal, string> namesRu = new Dictionary<decimal, string>
        {
            {0, "Лево"},
            {3, "Подобранный"},
            {1, "Право"},
            {2, "Центр"},
        };

        private static readonly Dictionary<decimal, string> namesEn = new Dictionary<decimal, string>
        {
            {0, "Left"},
            {3, "Justified"},
            {1, "Right"},
            {2, "Centered"},
        };

        [ContextProperty("Лево", "Left")]
        public decimal Left => 0;

        [ContextProperty("Подобранный", "Justified")]
        public decimal Justified => 3;

        [ContextProperty("Право", "Right")]
        public decimal Right => 1;

        [ContextProperty("Центр", "Centered")]
        public decimal Centered => 2;
    }
}
