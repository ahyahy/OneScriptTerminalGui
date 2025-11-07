using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ostgui
{
    [ContextClass("ТфСтильФлажкаЭлементаМеню", "TfMenuItemCheckStyle")]
    public class TfMenuItemCheckStyle : AutoContext<TfMenuItemCheckStyle>, ICollectionContext, IEnumerable<IValue>
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

        public TfMenuItemCheckStyle()
        {
            _list = new List<decimal>
            {
                NoCheck,
                Checked,
                Radio,
            }.Select(ValueFactory.Create).ToList();
        }

        private static readonly Dictionary<decimal, string> namesRu = new Dictionary<decimal, string>
        {
            {0, "БезОтметки"},
            {1, "Отметка"},
            {2, "Переключатель"},
        };

        private static readonly Dictionary<decimal, string> namesEn = new Dictionary<decimal, string>
        {
            {0, "NoCheck"},
            {1, "Checked"},
            {2, "Radio"},
        };

        [ContextProperty("БезОтметки", "NoCheck")]
        public decimal NoCheck => 0;

        [ContextProperty("Отметка", "Checked")]
        public decimal Checked => 1;

        [ContextProperty("Переключатель", "Radio")]
        public decimal Radio => 2;
    }
}
