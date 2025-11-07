using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ostgui
{
    [ContextClass("ТфФлагиМыши", "TfMouseFlags")]
    public class TfMouseFlags : AutoContext<TfMouseFlags>, ICollectionContext, IEnumerable<IValue>
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

        public TfMouseFlags()
        {
            _list = new List<decimal>
            {
                AllEvents,
                Button1DoubleClicked,
                Button1Clicked,
                Button1Pressed,
                Button1Released,
                Button1TripleClicked,
                Button2DoubleClicked,
                Button2Clicked,
                Button2Pressed,
                Button2Released,
                Button2TripleClicked,
                Button3DoubleClicked,
                Button3Clicked,
                Button3Pressed,
                Button3Released,
                Button3TripleClicked,
                Button4DoubleClicked,
                Button4Clicked,
                Button4Pressed,
                Button4Released,
                Button4TripleClicked,
                WheeledRight,
                WheeledLeft,
                WheeledDown,
                WheeledUp,
                None,
                ReportMousePosition,
                ButtonAlt,
                ButtonCtrl,
                ButtonShift,
            }.Select(ValueFactory.Create).ToList();
        }

        private static readonly Dictionary<decimal, string> namesRu = new Dictionary<decimal, string>
        {
            {134217727, "ВсеСобытия"},
            {8, "Кнопка1ДвойнойКлик"},
            {4, "Кнопка1Кликнута"},
            {2, "Кнопка1Нажата"},
            {1, "Кнопка1Отпущена"},
            {16, "Кнопка1ТройнойКлик"},
            {512, "Кнопка2ДвойнойКлик"},
            {256, "Кнопка2Кликнута"},
            {128, "Кнопка2Нажата"},
            {64, "Кнопка2Отпущена"},
            {1024, "Кнопка2ТройнойКлик"},
            {32768, "Кнопка3ДвойнойКлик"},
            {16384, "Кнопка3Кликнута"},
            {8192, "Кнопка3Нажата"},
            {4096, "Кнопка3Отпущена"},
            {65536, "Кнопка3ТройнойКлик"},
            {2097152, "Кнопка4ДвойнойКлик"},
            {1048576, "Кнопка4Кликнута"},
            {524288, "Кнопка4Нажата"},
            {262144, "Кнопка4Отпущена"},
            {4194304, "Кнопка4ТройнойКлик"},
            {553648128, "КолесоCTRLНажато"},
            {285212672, "КолесоCTRLОтпущено"},
            {536870912, "КолесоНажато"},
            {268435456, "КолесоОтпущено"},
            {0, "Отсутствие"},
            {134217728, "ПозицияМыши"},
            {67108864, "СовместноALT"},
            {16777216, "СовместноCTRL"},
            {33554432, "СовместноSHIFT"},
        };

        private static readonly Dictionary<decimal, string> namesEn = new Dictionary<decimal, string>
        {
            {134217727, "AllEvents"},
            {8, "Button1DoubleClicked"},
            {4, "Button1Clicked"},
            {2, "Button1Pressed"},
            {1, "Button1Released"},
            {16, "Button1TripleClicked"},
            {512, "Button2DoubleClicked"},
            {256, "Button2Clicked"},
            {128, "Button2Pressed"},
            {64, "Button2Released"},
            {1024, "Button2TripleClicked"},
            {32768, "Button3DoubleClicked"},
            {16384, "Button3Clicked"},
            {8192, "Button3Pressed"},
            {4096, "Button3Released"},
            {65536, "Button3TripleClicked"},
            {2097152, "Button4DoubleClicked"},
            {1048576, "Button4Clicked"},
            {524288, "Button4Pressed"},
            {262144, "Button4Released"},
            {4194304, "Button4TripleClicked"},
            {553648128, "WheeledRight"},
            {285212672, "WheeledLeft"},
            {536870912, "WheeledDown"},
            {268435456, "WheeledUp"},
            {0, "None"},
            {134217728, "ReportMousePosition"},
            {67108864, "ButtonAlt"},
            {16777216, "ButtonCtrl"},
            {33554432, "ButtonShift"},
        };

        [ContextProperty("ВсеСобытия", "AllEvents")]
        public decimal AllEvents => 134217727;

        [ContextProperty("Кнопка1ДвойнойКлик", "Button1DoubleClicked")]
        public decimal Button1DoubleClicked => 8;

        [ContextProperty("Кнопка1Кликнута", "Button1Clicked")]
        public decimal Button1Clicked => 4;

        [ContextProperty("Кнопка1Нажата", "Button1Pressed")]
        public decimal Button1Pressed => 2;

        [ContextProperty("Кнопка1Отпущена", "Button1Released")]
        public decimal Button1Released => 1;

        [ContextProperty("Кнопка1ТройнойКлик", "Button1TripleClicked")]
        public decimal Button1TripleClicked => 16;

        [ContextProperty("Кнопка2ДвойнойКлик", "Button2DoubleClicked")]
        public decimal Button2DoubleClicked => 512;

        [ContextProperty("Кнопка2Кликнута", "Button2Clicked")]
        public decimal Button2Clicked => 256;

        [ContextProperty("Кнопка2Нажата", "Button2Pressed")]
        public decimal Button2Pressed => 128;

        [ContextProperty("Кнопка2Отпущена", "Button2Released")]
        public decimal Button2Released => 64;

        [ContextProperty("Кнопка2ТройнойКлик", "Button2TripleClicked")]
        public decimal Button2TripleClicked => 1024;

        [ContextProperty("Кнопка3ДвойнойКлик", "Button3DoubleClicked")]
        public decimal Button3DoubleClicked => 32768;

        [ContextProperty("Кнопка3Кликнута", "Button3Clicked")]
        public decimal Button3Clicked => 16384;

        [ContextProperty("Кнопка3Нажата", "Button3Pressed")]
        public decimal Button3Pressed => 8192;

        [ContextProperty("Кнопка3Отпущена", "Button3Released")]
        public decimal Button3Released => 4096;

        [ContextProperty("Кнопка3ТройнойКлик", "Button3TripleClicked")]
        public decimal Button3TripleClicked => 65536;

        [ContextProperty("Кнопка4ДвойнойКлик", "Button4DoubleClicked")]
        public decimal Button4DoubleClicked => 2097152;

        [ContextProperty("Кнопка4Кликнута", "Button4Clicked")]
        public decimal Button4Clicked => 1048576;

        [ContextProperty("Кнопка4Нажата", "Button4Pressed")]
        public decimal Button4Pressed => 524288;

        [ContextProperty("Кнопка4Отпущена", "Button4Released")]
        public decimal Button4Released => 262144;

        [ContextProperty("Кнопка4ТройнойКлик", "Button4TripleClicked")]
        public decimal Button4TripleClicked => 4194304;

        [ContextProperty("КолесоCTRLНажато", "WheeledRight")]
        public decimal WheeledRight => 553648128;

        [ContextProperty("КолесоCTRLОтпущено", "WheeledLeft")]
        public decimal WheeledLeft => 285212672;

        [ContextProperty("КолесоНажато", "WheeledDown")]
        public decimal WheeledDown => 536870912;

        [ContextProperty("КолесоОтпущено", "WheeledUp")]
        public decimal WheeledUp => 268435456;

        [ContextProperty("Отсутствие", "None")]
        public decimal None => 0;

        [ContextProperty("ПозицияМыши", "ReportMousePosition")]
        public decimal ReportMousePosition => 134217728;

        [ContextProperty("СовместноALT", "ButtonAlt")]
        public decimal ButtonAlt => 67108864;

        [ContextProperty("СовместноCTRL", "ButtonCtrl")]
        public decimal ButtonCtrl => 16777216;

        [ContextProperty("СовместноSHIFT", "ButtonShift")]
        public decimal ButtonShift => 33554432;
    }
}
