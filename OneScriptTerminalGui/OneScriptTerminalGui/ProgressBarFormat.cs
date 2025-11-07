using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ostgui
{
    [ContextClass("ТфФорматИндикатора", "TfProgressBarFormat")]
    public class TfProgressBarFormat : AutoContext<TfProgressBarFormat>, ICollectionContext, IEnumerable<IValue>
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

        public TfProgressBarFormat()
        {
            _list = new List<decimal>
            {
                Framed,
                FramedPlusPercentage,
                FramedProgressPadded,
                Simple,
                SimplePlusPercentage,
            }.Select(ValueFactory.Create).ToList();
        }

        private static readonly Dictionary<decimal, string> namesRu = new Dictionary<decimal, string>
        {
            {2, "Кадр"},
            {3, "КадрПроценты"},
            {4, "КадрПроцентыОтступы"},
            {0, "Простой"},
            {1, "ПростойПроценты"},
        };

        private static readonly Dictionary<decimal, string> namesEn = new Dictionary<decimal, string>
        {
            {2, "Framed"},
            {3, "FramedPlusPercentage"},
            {4, "FramedProgressPadded"},
            {0, "Simple"},
            {1, "SimplePlusPercentage"},
        };

        [ContextProperty("Кадр", "Framed")]
        public decimal Framed => 2;

        [ContextProperty("КадрПроценты", "FramedPlusPercentage")]
        public decimal FramedPlusPercentage => 3;

        [ContextProperty("КадрПроцентыОтступы", "FramedProgressPadded")]
        public decimal FramedProgressPadded => 4;

        [ContextProperty("Простой", "Simple")]
        public decimal Simple => 0;

        [ContextProperty("ПростойПроценты", "SimplePlusPercentage")]
        public decimal SimplePlusPercentage => 1;
    }
}
