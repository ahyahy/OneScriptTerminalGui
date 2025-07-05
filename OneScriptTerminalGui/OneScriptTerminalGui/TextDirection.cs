using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;

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

        public TfTextDirection()
        {
            _list = new List<IValue>();
            _list.Add(ValueFactory.Create(TopBottom_LeftRight));
            _list.Add(ValueFactory.Create(TopBottom_RightLeft));
            _list.Add(ValueFactory.Create(LeftRight_TopBottom));
            _list.Add(ValueFactory.Create(LeftRight_BottomTop));
            _list.Add(ValueFactory.Create(BottomTop_LeftRight));
            _list.Add(ValueFactory.Create(BottomTop_RightLeft));
            _list.Add(ValueFactory.Create(RightLeft_TopBottom));
            _list.Add(ValueFactory.Create(RightLeft_BottomTop));
        }

        [ContextProperty("СверхуВнизСлеваНаправо", "TopBottom_LeftRight")]
        public int TopBottom_LeftRight
        {
            get { return 1; }
        }

        [ContextProperty("СверхуВнизСправаНалево", "TopBottom_RightLeft")]
        public int TopBottom_RightLeft
        {
            get { return 3; }
        }

        [ContextProperty("СлеваНаправоСверхуВниз", "LeftRight_TopBottom")]
        public int LeftRight_TopBottom
        {
            get { return 0; }
        }

        [ContextProperty("СлеваНаправоСнизуВверх", "LeftRight_BottomTop")]
        public int LeftRight_BottomTop
        {
            get { return 4; }
        }

        [ContextProperty("СнизуВверхСлеваНаправо", "BottomTop_LeftRight")]
        public int BottomTop_LeftRight
        {
            get { return 5; }
        }

        [ContextProperty("СнизуВверхСправаНалево", "BottomTop_RightLeft")]
        public int BottomTop_RightLeft
        {
            get { return 7; }
        }

        [ContextProperty("СправаНалевоСверхуВниз", "RightLeft_TopBottom")]
        public int RightLeft_TopBottom
        {
            get { return 2; }
        }

        [ContextProperty("СправаНалевоСнизуВверх", "RightLeft_BottomTop")]
        public int RightLeft_BottomTop
        {
            get { return 6; }
        }

        [ContextMethod("ВСтроку", "ВСтроку")]
        public string ToStringRu(decimal p1)
        {
            string str = p1.ToString();
            switch (p1)
            {
                case 1:
                    str = "СверхуВнизСлеваНаправо";
                    break;
                case 3:
                    str = "СверхуВнизСправаНалево";
                    break;
                case 0:
                    str = "СлеваНаправоСверхуВниз";
                    break;
                case 4:
                    str = "СлеваНаправоСнизуВверх";
                    break;
                case 5:
                    str = "СнизуВверхСлеваНаправо";
                    break;
                case 7:
                    str = "СнизуВверхСправаНалево";
                    break;
                case 2:
                    str = "СправаНалевоСверхуВниз";
                    break;
                case 6:
                    str = "СправаНалевоСнизуВверх";
                    break;
            }
            return str;
        }

        [ContextMethod("ToString", "ToString")]
        public string ToStringEn(decimal p1)
        {
            string str = p1.ToString();
            switch (p1)
            {
                case 1:
                    str = "TopBottom_LeftRight";
                    break;
                case 3:
                    str = "TopBottom_RightLeft";
                    break;
                case 0:
                    str = "LeftRight_TopBottom";
                    break;
                case 4:
                    str = "LeftRight_BottomTop";
                    break;
                case 5:
                    str = "BottomTop_LeftRight";
                    break;
                case 7:
                    str = "BottomTop_RightLeft";
                    break;
                case 2:
                    str = "RightLeft_TopBottom";
                    break;
                case 6:
                    str = "RightLeft_BottomTop";
                    break;
            }
            return str;
        }
    }
}
