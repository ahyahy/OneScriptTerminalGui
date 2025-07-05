using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;

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

        public TfBorderStyle()
        {
            _list = new List<IValue>();
            _list.Add(ValueFactory.Create(Double));
            _list.Add(ValueFactory.Create(Rounded));
            _list.Add(ValueFactory.Create(Single));
            _list.Add(ValueFactory.Create(None));
        }

        [ContextProperty("Двойная", "Double")]
        public int Double
        {
            get { return 2; }
        }

        [ContextProperty("Закругленная", "Rounded")]
        public int Rounded
        {
            get { return 3; }
        }

        [ContextProperty("Одинарная", "Single")]
        public int Single
        {
            get { return 1; }
        }

        [ContextProperty("Отсутствие", "None")]
        public int None
        {
            get { return 0; }
        }

        [ContextMethod("ВСтроку", "ВСтроку")]
        public string ToStringRu(decimal p1)
        {
            string str = p1.ToString();
            switch (p1)
            {
                case 2:
                    str = "Двойная";
                    break;
                case 3:
                    str = "Закругленная";
                    break;
                case 1:
                    str = "Одинарная";
                    break;
                case 0:
                    str = "Отсутствие";
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
                case 2:
                    str = "Double";
                    break;
                case 3:
                    str = "Rounded";
                    break;
                case 1:
                    str = "Single";
                    break;
                case 0:
                    str = "None";
                    break;
            }
            return str;
        }
    }
}
