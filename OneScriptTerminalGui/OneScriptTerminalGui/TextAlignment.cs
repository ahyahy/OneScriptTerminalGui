using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;

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

        public TfTextAlignment()
        {
            _list = new List<IValue>();
            _list.Add(ValueFactory.Create(Left));
            _list.Add(ValueFactory.Create(Justified));
            _list.Add(ValueFactory.Create(Right));
            _list.Add(ValueFactory.Create(Centered));
        }

        [ContextProperty("Лево", "Left")]
        public int Left
        {
            get { return 0; }
        }

        [ContextProperty("Подобранный", "Justified")]
        public int Justified
        {
            get { return 3; }
        }

        [ContextProperty("Право", "Right")]
        public int Right
        {
            get { return 1; }
        }

        [ContextProperty("Центр", "Centered")]
        public int Centered
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
                    str = "Лево";
                    break;
                case 3:
                    str = "Подобранный";
                    break;
                case 1:
                    str = "Право";
                    break;
                case 2:
                    str = "Центр";
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
                    str = "Left";
                    break;
                case 3:
                    str = "Justified";
                    break;
                case 1:
                    str = "Right";
                    break;
                case 2:
                    str = "Centered";
                    break;
            }
            return str;
        }
    }
}
