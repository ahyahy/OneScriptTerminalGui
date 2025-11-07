using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ostgui
{
    [ContextClass("ТфНаправлениеТекста", "TfTextDirection")]
    public class TfTextDirection : AutoContext<TfTextDirection>, ICollectionContext, IEnumerable<IValue>
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

        public TfTextDirection()
        {
            _list = new List<decimal>
            {
                TopBottom_LeftRight,
                TopBottom_RightLeft,
                LeftRight_TopBottom,
                LeftRight_BottomTop,
                BottomTop_LeftRight,
                BottomTop_RightLeft,
                RightLeft_TopBottom,
                RightLeft_BottomTop,
            }.Select(ValueFactory.Create).ToList();
        }

        private static readonly Dictionary<decimal, string> namesRu = new Dictionary<decimal, string>
        {
            {1, "СверхуВнизСлеваНаправо"},
            {3, "СверхуВнизСправаНалево"},
            {0, "СлеваНаправоСверхуВниз"},
            {4, "СлеваНаправоСнизуВверх"},
            {5, "СнизуВверхСлеваНаправо"},
            {7, "СнизуВверхСправаНалево"},
            {2, "СправаНалевоСверхуВниз"},
            {6, "СправаНалевоСнизуВверх"},
        };

        private static readonly Dictionary<decimal, string> namesEn = new Dictionary<decimal, string>
        {
            {1, "TopBottom_LeftRight"},
            {3, "TopBottom_RightLeft"},
            {0, "LeftRight_TopBottom"},
            {4, "LeftRight_BottomTop"},
            {5, "BottomTop_LeftRight"},
            {7, "BottomTop_RightLeft"},
            {2, "RightLeft_TopBottom"},
            {6, "RightLeft_BottomTop"},
        };

        [ContextProperty("СверхуВнизСлеваНаправо", "TopBottom_LeftRight")]
        public decimal TopBottom_LeftRight => 1;

        [ContextProperty("СверхуВнизСправаНалево", "TopBottom_RightLeft")]
        public decimal TopBottom_RightLeft => 3;

        [ContextProperty("СлеваНаправоСверхуВниз", "LeftRight_TopBottom")]
        public decimal LeftRight_TopBottom => 0;

        [ContextProperty("СлеваНаправоСнизуВверх", "LeftRight_BottomTop")]
        public decimal LeftRight_BottomTop => 4;

        [ContextProperty("СнизуВверхСлеваНаправо", "BottomTop_LeftRight")]
        public decimal BottomTop_LeftRight => 5;

        [ContextProperty("СнизуВверхСправаНалево", "BottomTop_RightLeft")]
        public decimal BottomTop_RightLeft => 7;

        [ContextProperty("СправаНалевоСверхуВниз", "RightLeft_TopBottom")]
        public decimal RightLeft_TopBottom => 2;

        [ContextProperty("СправаНалевоСнизуВверх", "RightLeft_BottomTop")]
        public decimal RightLeft_BottomTop => 6;
    }
}
