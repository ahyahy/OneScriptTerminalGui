using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ostgui
{
    [ContextClass("ТфВидКурсора", "TfCursorVisibility")]
    public class TfCursorVisibility : AutoContext<TfCursorVisibility>, ICollectionContext, IEnumerable<IValue>
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

        public TfCursorVisibility()
        {
            _list = new List<decimal>
            {
                BoxFix,
                VerticalFix,
                Box,
                Vertical,
                Underline,
                Invisible,
                UnderlineFix,
                Default,
            }.Select(ValueFactory.Create).ToList();
        }

        private static readonly Dictionary<decimal, string> namesRu = new Dictionary<decimal, string>
        {
            {33685860, "Блок"},
            {100729113, "Вертикальный"},
            {16908644, "МигающийБлок"},
            {83951897, "МигающийВертикальный"},
            {50397465, "МигающийПодстрочный"},
            {50331673, "Невидимый"},
            {67174681, "Подстрочный"},
            {65817, "ПоУмолчанию"},
        };

        private static readonly Dictionary<decimal, string> namesEn = new Dictionary<decimal, string>
        {
            {33685860, "BoxFix"},
            {100729113, "VerticalFix"},
            {16908644, "Box"},
            {83951897, "Vertical"},
            {50397465, "Underline"},
            {50331673, "Invisible"},
            {67174681, "UnderlineFix"},
            {65817, "Default"},
        };

        [ContextProperty("Блок", "BoxFix")]
        public decimal BoxFix => 33685860;

        [ContextProperty("Вертикальный", "VerticalFix")]
        public decimal VerticalFix => 100729113;

        [ContextProperty("МигающийБлок", "Box")]
        public decimal Box => 16908644;

        [ContextProperty("МигающийВертикальный", "Vertical")]
        public decimal Vertical => 83951897;

        [ContextProperty("МигающийПодстрочный", "Underline")]
        public decimal Underline => 50397465;

        [ContextProperty("Невидимый", "Invisible")]
        public decimal Invisible => 50331673;

        [ContextProperty("Подстрочный", "UnderlineFix")]
        public decimal UnderlineFix => 67174681;

        [ContextProperty("ПоУмолчанию", "Default")]
        public decimal Default => 65817;
    }
}
