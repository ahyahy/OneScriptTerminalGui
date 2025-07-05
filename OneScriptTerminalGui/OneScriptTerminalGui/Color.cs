using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;

namespace ostgui
{
    [ContextClass("ТфЦвет", "TfColor")]
    public class TfColor : AutoContext<TfColor>, ICollectionContext, IEnumerable<IValue>
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

        public TfColor()
        {
            _list = new List<IValue>();
            _list.Add(ValueFactory.Create(White));
            _list.Add(ValueFactory.Create(Cyan));
            _list.Add(ValueFactory.Create(Green));
            _list.Add(ValueFactory.Create(Brown));
            _list.Add(ValueFactory.Create(Red));
            _list.Add(ValueFactory.Create(Magenta));
            _list.Add(ValueFactory.Create(Gray));
            _list.Add(ValueFactory.Create(Blue));
            _list.Add(ValueFactory.Create(DarkGray));
            _list.Add(ValueFactory.Create(Black));
            _list.Add(ValueFactory.Create(BrightCyan));
            _list.Add(ValueFactory.Create(BrightYellow));
            _list.Add(ValueFactory.Create(BrightGreen));
            _list.Add(ValueFactory.Create(BrightRed));
            _list.Add(ValueFactory.Create(BrightMagenta));
            _list.Add(ValueFactory.Create(BrightBlue));
        }

        [ContextProperty("Белый", "White")]
        public int White
        {
            get { return 15; }
        }

        [ContextProperty("Голубой", "Cyan")]
        public int Cyan
        {
            get { return 3; }
        }

        [ContextProperty("Зеленый", "Green")]
        public int Green
        {
            get { return 2; }
        }

        [ContextProperty("Коричневый", "Brown")]
        public int Brown
        {
            get { return 6; }
        }

        [ContextProperty("Красный", "Red")]
        public int Red
        {
            get { return 4; }
        }

        [ContextProperty("Пурпурный", "Magenta")]
        public int Magenta
        {
            get { return 5; }
        }

        [ContextProperty("Серый", "Gray")]
        public int Gray
        {
            get { return 7; }
        }

        [ContextProperty("Синий", "Blue")]
        public int Blue
        {
            get { return 1; }
        }

        [ContextProperty("ТемноСерый", "DarkGray")]
        public int DarkGray
        {
            get { return 8; }
        }

        [ContextProperty("Черный", "Black")]
        public int Black
        {
            get { return 0; }
        }

        [ContextProperty("ЯркоГолубой", "BrightCyan")]
        public int BrightCyan
        {
            get { return 11; }
        }

        [ContextProperty("ЯркоЖелтый", "BrightYellow")]
        public int BrightYellow
        {
            get { return 14; }
        }

        [ContextProperty("ЯркоЗеленый", "BrightGreen")]
        public int BrightGreen
        {
            get { return 10; }
        }

        [ContextProperty("ЯркоКрасный", "BrightRed")]
        public int BrightRed
        {
            get { return 12; }
        }

        [ContextProperty("ЯркоПурпурный", "BrightMagenta")]
        public int BrightMagenta
        {
            get { return 13; }
        }

        [ContextProperty("ЯркоСиний", "BrightBlue")]
        public int BrightBlue
        {
            get { return 9; }
        }

        [ContextMethod("ВСтроку", "ВСтроку")]
        public string ToStringRu(decimal p1)
        {
            string str = p1.ToString();
            switch (p1)
            {
                case 15:
                    str = "Белый";
                    break;
                case 3:
                    str = "Голубой";
                    break;
                case 2:
                    str = "Зеленый";
                    break;
                case 6:
                    str = "Коричневый";
                    break;
                case 4:
                    str = "Красный";
                    break;
                case 5:
                    str = "Пурпурный";
                    break;
                case 7:
                    str = "Серый";
                    break;
                case 1:
                    str = "Синий";
                    break;
                case 8:
                    str = "ТемноСерый";
                    break;
                case 0:
                    str = "Черный";
                    break;
                case 11:
                    str = "ЯркоГолубой";
                    break;
                case 14:
                    str = "ЯркоЖелтый";
                    break;
                case 10:
                    str = "ЯркоЗеленый";
                    break;
                case 12:
                    str = "ЯркоКрасный";
                    break;
                case 13:
                    str = "ЯркоПурпурный";
                    break;
                case 9:
                    str = "ЯркоСиний";
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
                case 15:
                    str = "White";
                    break;
                case 3:
                    str = "Cyan";
                    break;
                case 2:
                    str = "Green";
                    break;
                case 6:
                    str = "Brown";
                    break;
                case 4:
                    str = "Red";
                    break;
                case 5:
                    str = "Magenta";
                    break;
                case 7:
                    str = "Gray";
                    break;
                case 1:
                    str = "Blue";
                    break;
                case 8:
                    str = "DarkGray";
                    break;
                case 0:
                    str = "Black";
                    break;
                case 11:
                    str = "BrightCyan";
                    break;
                case 14:
                    str = "BrightYellow";
                    break;
                case 10:
                    str = "BrightGreen";
                    break;
                case 12:
                    str = "BrightRed";
                    break;
                case 13:
                    str = "BrightMagenta";
                    break;
                case 9:
                    str = "BrightBlue";
                    break;
            }
            return str;
        }
    }
}
