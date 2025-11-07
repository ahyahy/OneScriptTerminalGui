using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

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

        public TfColor()
        {
            _list = new List<decimal>
            {
                White,
                Cyan,
                Green,
                Brown,
                Red,
                Magenta,
                Gray,
                Blue,
                DarkGray,
                Black,
                BrightCyan,
                BrightYellow,
                BrightGreen,
                BrightRed,
                BrightMagenta,
                BrightBlue,
            }.Select(ValueFactory.Create).ToList();
        }

        private static readonly Dictionary<decimal, string> namesRu = new Dictionary<decimal, string>
        {
            {15, "Белый"},
            {3, "Голубой"},
            {2, "Зеленый"},
            {6, "Коричневый"},
            {4, "Красный"},
            {5, "Пурпурный"},
            {7, "Серый"},
            {1, "Синий"},
            {8, "ТемноСерый"},
            {0, "Черный"},
            {11, "ЯркоГолубой"},
            {14, "ЯркоЖелтый"},
            {10, "ЯркоЗеленый"},
            {12, "ЯркоКрасный"},
            {13, "ЯркоПурпурный"},
            {9, "ЯркоСиний"},
        };

        private static readonly Dictionary<decimal, string> namesEn = new Dictionary<decimal, string>
        {
            {15, "White"},
            {3, "Cyan"},
            {2, "Green"},
            {6, "Brown"},
            {4, "Red"},
            {5, "Magenta"},
            {7, "Gray"},
            {1, "Blue"},
            {8, "DarkGray"},
            {0, "Black"},
            {11, "BrightCyan"},
            {14, "BrightYellow"},
            {10, "BrightGreen"},
            {12, "BrightRed"},
            {13, "BrightMagenta"},
            {9, "BrightBlue"},
        };

        [ContextProperty("Белый", "White")]
        public decimal White => 15;

        [ContextProperty("Голубой", "Cyan")]
        public decimal Cyan => 3;

        [ContextProperty("Зеленый", "Green")]
        public decimal Green => 2;

        [ContextProperty("Коричневый", "Brown")]
        public decimal Brown => 6;

        [ContextProperty("Красный", "Red")]
        public decimal Red => 4;

        [ContextProperty("Пурпурный", "Magenta")]
        public decimal Magenta => 5;

        [ContextProperty("Серый", "Gray")]
        public decimal Gray => 7;

        [ContextProperty("Синий", "Blue")]
        public decimal Blue => 1;

        [ContextProperty("ТемноСерый", "DarkGray")]
        public decimal DarkGray => 8;

        [ContextProperty("Черный", "Black")]
        public decimal Black => 0;

        [ContextProperty("ЯркоГолубой", "BrightCyan")]
        public decimal BrightCyan => 11;

        [ContextProperty("ЯркоЖелтый", "BrightYellow")]
        public decimal BrightYellow => 14;

        [ContextProperty("ЯркоЗеленый", "BrightGreen")]
        public decimal BrightGreen => 10;

        [ContextProperty("ЯркоКрасный", "BrightRed")]
        public decimal BrightRed => 12;

        [ContextProperty("ЯркоПурпурный", "BrightMagenta")]
        public decimal BrightMagenta => 13;

        [ContextProperty("ЯркоСиний", "BrightBlue")]
        public decimal BrightBlue => 9;
    }
}
