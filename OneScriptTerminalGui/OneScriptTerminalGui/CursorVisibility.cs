using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;

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

        public TfCursorVisibility()
        {
            _list = new List<IValue>();
            _list.Add(ValueFactory.Create(BoxFix));
            _list.Add(ValueFactory.Create(VerticalFix));
            _list.Add(ValueFactory.Create(Box));
            _list.Add(ValueFactory.Create(Vertical));
            _list.Add(ValueFactory.Create(Underline));
            _list.Add(ValueFactory.Create(Invisible));
            _list.Add(ValueFactory.Create(UnderlineFix));
            _list.Add(ValueFactory.Create(Default));
        }

        [ContextProperty("Блок", "BoxFix")]
        public int BoxFix
        {
            get { return 33685860; }
        }

        [ContextProperty("Вертикальный", "VerticalFix")]
        public int VerticalFix
        {
            get { return 100729113; }
        }

        [ContextProperty("МигающийБлок", "Box")]
        public int Box
        {
            get { return 16908644; }
        }

        [ContextProperty("МигающийВертикальный", "Vertical")]
        public int Vertical
        {
            get { return 83951897; }
        }

        [ContextProperty("МигающийПодстрочный", "Underline")]
        public int Underline
        {
            get { return 50397465; }
        }

        [ContextProperty("Невидимый", "Invisible")]
        public int Invisible
        {
            get { return 50331673; }
        }

        [ContextProperty("Подстрочный", "UnderlineFix")]
        public int UnderlineFix
        {
            get { return 67174681; }
        }

        [ContextProperty("ПоУмолчанию", "Default")]
        public int Default
        {
            get { return 65817; }
        }

        [ContextMethod("ВСтроку", "ВСтроку")]
        public string ToStringRu(decimal p1)
        {
            string str = p1.ToString();
            switch (p1)
            {
                case 33685860:
                    str = "Блок";
                    break;
                case 100729113:
                    str = "Вертикальный";
                    break;
                case 16908644:
                    str = "МигающийБлок";
                    break;
                case 83951897:
                    str = "МигающийВертикальный";
                    break;
                case 50397465:
                    str = "МигающийПодстрочный";
                    break;
                case 50331673:
                    str = "Невидимый";
                    break;
                case 67174681:
                    str = "Подстрочный";
                    break;
                case 65817:
                    str = "ПоУмолчанию";
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
                case 33685860:
                    str = "BoxFix";
                    break;
                case 100729113:
                    str = "VerticalFix";
                    break;
                case 16908644:
                    str = "Box";
                    break;
                case 83951897:
                    str = "Vertical";
                    break;
                case 50397465:
                    str = "Underline";
                    break;
                case 50331673:
                    str = "Invisible";
                    break;
                case 67174681:
                    str = "UnderlineFix";
                    break;
                case 65817:
                    str = "Default";
                    break;
            }
            return str;
        }
    }
}
