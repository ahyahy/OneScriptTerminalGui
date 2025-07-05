using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;

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

        public TfMenuItemCheckStyle()
        {
            _list = new List<IValue>();
            _list.Add(ValueFactory.Create(NoCheck));
            _list.Add(ValueFactory.Create(Checked));
            _list.Add(ValueFactory.Create(Radio));
        }

        [ContextProperty("БезОтметки", "NoCheck")]
        public int NoCheck
        {
            get { return 0; }
        }

        [ContextProperty("Отметка", "Checked")]
        public int Checked
        {
            get { return 1; }
        }

        [ContextProperty("Переключатель", "Radio")]
        public int Radio
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
                    str = "БезОтметки";
                    break;
                case 1:
                    str = "Отметка";
                    break;
                case 2:
                    str = "Переключатель";
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
                    str = "NoCheck";
                    break;
                case 1:
                    str = "Checked";
                    break;
                case 2:
                    str = "Radio";
                    break;
            }
            return str;
        }
    }
}
