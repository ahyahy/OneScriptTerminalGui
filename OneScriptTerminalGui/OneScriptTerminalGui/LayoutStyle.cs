using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;

namespace ostgui
{
    [ContextClass("ТфСтильКомпоновки", "TfLayoutStyle")]
    public class TfLayoutStyle : AutoContext<TfLayoutStyle>, ICollectionContext, IEnumerable<IValue>
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

        public TfLayoutStyle()
        {
            _list = new List<IValue>();
            _list.Add(ValueFactory.Create(Absolute));
            _list.Add(ValueFactory.Create(Computed));
        }

        [ContextProperty("Абсолютно", "Absolute")]
        public int Absolute
        {
            get { return 0; }
        }

        [ContextProperty("Вычислено", "Computed")]
        public int Computed
        {
            get { return 1; }
        }

        [ContextMethod("ВСтроку", "ВСтроку")]
        public string ToStringRu(decimal p1)
        {
            string str = p1.ToString();
            switch (p1)
            {
                case 0:
                    str = "Абсолютно";
                    break;
                case 1:
                    str = "Вычислено";
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
                    str = "Absolute";
                    break;
                case 1:
                    str = "Computed";
                    break;
            }
            return str;
        }
    }
}
