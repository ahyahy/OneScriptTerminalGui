using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;

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

        public TfVerticalTextAlignment()
        {
            _list = new List<IValue>();
            _list.Add(ValueFactory.Create(Top));
            _list.Add(ValueFactory.Create(Bottom));
            _list.Add(ValueFactory.Create(Justified));
            _list.Add(ValueFactory.Create(Middle));
        }

        [ContextProperty("Верх", "Top")]
        public int Top
        {
            get { return 0; }
        }

        [ContextProperty("Низ", "Bottom")]
        public int Bottom
        {
            get { return 1; }
        }

        [ContextProperty("Подобранный", "Justified")]
        public int Justified
        {
            get { return 3; }
        }

        [ContextProperty("Середина", "Middle")]
        public int Middle
        {
            get { return 2; }
        }

        [ContextMethod("ВСтроку", "ВСтроку")]
        public string ToStringRu(decimal p1)
        {
            string str = p1.ToString();
            switch (p1)
            {
                case 0:
                    str = "Верх";
                    break;
                case 1:
                    str = "Низ";
                    break;
                case 3:
                    str = "Подобранный";
                    break;
                case 2:
                    str = "Середина";
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
                case 0:
                    str = "Top";
                    break;
                case 1:
                    str = "Bottom";
                    break;
                case 3:
                    str = "Justified";
                    break;
                case 2:
                    str = "Middle";
                    break;
            }
            return str;
        }
    }
}
