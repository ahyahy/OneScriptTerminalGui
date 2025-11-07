using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ostgui
{
    [ContextClass("ТфВыравниваниеКнопок", "TfButtonAlignments")]
    public class TfButtonAlignments : AutoContext<TfButtonAlignments>, ICollectionContext, IEnumerable<IValue>
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

        public TfButtonAlignments()
        {
            _list = new List<decimal>
            {
                Left,
                Justify,
                Right,
                Center,
            }.Select(ValueFactory.Create).ToList();
        }

        private static readonly Dictionary<decimal, string> namesRu = new Dictionary<decimal, string>
        {
            {2, "Лево"},
            {1, "Подобранный"},
            {3, "Право"},
            {0, "Центр"},
        };

        private static readonly Dictionary<decimal, string> namesEn = new Dictionary<decimal, string>
        {
            {2, "Left"},
            {1, "Justify"},
            {3, "Right"},
            {0, "Center"},
        };

        [ContextProperty("Лево", "Left")]
        public decimal Left => 2;

        [ContextProperty("Подобранный", "Justify")]
        public decimal Justify => 1;

        [ContextProperty("Право", "Right")]
        public decimal Right => 3;

        [ContextProperty("Центр", "Center")]
        public decimal Center => 0;
    }
}
