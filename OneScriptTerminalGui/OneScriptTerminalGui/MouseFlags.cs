using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;

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

        public TfMouseFlags()
        {
            _list = new List<IValue>();
            _list.Add(ValueFactory.Create(AllEvents));
            _list.Add(ValueFactory.Create(Button1DoubleClicked));
            _list.Add(ValueFactory.Create(Button1Clicked));
            _list.Add(ValueFactory.Create(Button1Pressed));
            _list.Add(ValueFactory.Create(Button1Released));
            _list.Add(ValueFactory.Create(Button1TripleClicked));
            _list.Add(ValueFactory.Create(Button2DoubleClicked));
            _list.Add(ValueFactory.Create(Button2Clicked));
            _list.Add(ValueFactory.Create(Button2Pressed));
            _list.Add(ValueFactory.Create(Button2Released));
            _list.Add(ValueFactory.Create(Button2TripleClicked));
            _list.Add(ValueFactory.Create(Button3DoubleClicked));
            _list.Add(ValueFactory.Create(Button3Clicked));
            _list.Add(ValueFactory.Create(Button3Pressed));
            _list.Add(ValueFactory.Create(Button3Released));
            _list.Add(ValueFactory.Create(Button3TripleClicked));
            _list.Add(ValueFactory.Create(Button4DoubleClicked));
            _list.Add(ValueFactory.Create(Button4Clicked));
            _list.Add(ValueFactory.Create(Button4Pressed));
            _list.Add(ValueFactory.Create(Button4Released));
            _list.Add(ValueFactory.Create(Button4TripleClicked));
            _list.Add(ValueFactory.Create(WheeledRight));
            _list.Add(ValueFactory.Create(WheeledLeft));
            _list.Add(ValueFactory.Create(WheeledDown));
            _list.Add(ValueFactory.Create(WheeledUp));
            _list.Add(ValueFactory.Create(None));
            _list.Add(ValueFactory.Create(ReportMousePosition));
            _list.Add(ValueFactory.Create(ButtonAlt));
            _list.Add(ValueFactory.Create(ButtonCtrl));
            _list.Add(ValueFactory.Create(ButtonShift));
        }

        [ContextProperty("ВсеСобытия", "AllEvents")]
        public int AllEvents
        {
            get { return 134217727; }
        }

        [ContextProperty("Кнопка1ДвойнойКлик", "Button1DoubleClicked")]
        public int Button1DoubleClicked
        {
            get { return 8; }
        }

        [ContextProperty("Кнопка1Кликнута", "Button1Clicked")]
        public int Button1Clicked
        {
            get { return 4; }
        }

        [ContextProperty("Кнопка1Нажата", "Button1Pressed")]
        public int Button1Pressed
        {
            get { return 2; }
        }

        [ContextProperty("Кнопка1Отпущена", "Button1Released")]
        public int Button1Released
        {
            get { return 1; }
        }

        [ContextProperty("Кнопка1ТройнойКлик", "Button1TripleClicked")]
        public int Button1TripleClicked
        {
            get { return 16; }
        }

        [ContextProperty("Кнопка2ДвойнойКлик", "Button2DoubleClicked")]
        public int Button2DoubleClicked
        {
            get { return 512; }
        }

        [ContextProperty("Кнопка2Кликнута", "Button2Clicked")]
        public int Button2Clicked
        {
            get { return 256; }
        }

        [ContextProperty("Кнопка2Нажата", "Button2Pressed")]
        public int Button2Pressed
        {
            get { return 128; }
        }

        [ContextProperty("Кнопка2Отпущена", "Button2Released")]
        public int Button2Released
        {
            get { return 64; }
        }

        [ContextProperty("Кнопка2ТройнойКлик", "Button2TripleClicked")]
        public int Button2TripleClicked
        {
            get { return 1024; }
        }

        [ContextProperty("Кнопка3ДвойнойКлик", "Button3DoubleClicked")]
        public int Button3DoubleClicked
        {
            get { return 32768; }
        }

        [ContextProperty("Кнопка3Кликнута", "Button3Clicked")]
        public int Button3Clicked
        {
            get { return 16384; }
        }

        [ContextProperty("Кнопка3Нажата", "Button3Pressed")]
        public int Button3Pressed
        {
            get { return 8192; }
        }

        [ContextProperty("Кнопка3Отпущена", "Button3Released")]
        public int Button3Released
        {
            get { return 4096; }
        }

        [ContextProperty("Кнопка3ТройнойКлик", "Button3TripleClicked")]
        public int Button3TripleClicked
        {
            get { return 65536; }
        }

        [ContextProperty("Кнопка4ДвойнойКлик", "Button4DoubleClicked")]
        public int Button4DoubleClicked
        {
            get { return 2097152; }
        }

        [ContextProperty("Кнопка4Кликнута", "Button4Clicked")]
        public int Button4Clicked
        {
            get { return 1048576; }
        }

        [ContextProperty("Кнопка4Нажата", "Button4Pressed")]
        public int Button4Pressed
        {
            get { return 524288; }
        }

        [ContextProperty("Кнопка4Отпущена", "Button4Released")]
        public int Button4Released
        {
            get { return 262144; }
        }

        [ContextProperty("Кнопка4ТройнойКлик", "Button4TripleClicked")]
        public int Button4TripleClicked
        {
            get { return 4194304; }
        }

        [ContextProperty("КолесоCTRLНажато", "WheeledRight")]
        public int WheeledRight
        {
            get { return 553648128; }
        }

        [ContextProperty("КолесоCTRLОтпущено", "WheeledLeft")]
        public int WheeledLeft
        {
            get { return 285212672; }
        }

        [ContextProperty("КолесоНажато", "WheeledDown")]
        public int WheeledDown
        {
            get { return 536870912; }
        }

        [ContextProperty("КолесоОтпущено", "WheeledUp")]
        public int WheeledUp
        {
            get { return 268435456; }
        }

        [ContextProperty("Отсутствие", "None")]
        public int None
        {
            get { return 0; }
        }

        [ContextProperty("ПозицияМыши", "ReportMousePosition")]
        public int ReportMousePosition
        {
            get { return 134217728; }
        }

        [ContextProperty("СовместноALT", "ButtonAlt")]
        public int ButtonAlt
        {
            get { return 67108864; }
        }

        [ContextProperty("СовместноCTRL", "ButtonCtrl")]
        public int ButtonCtrl
        {
            get { return 16777216; }
        }

        [ContextProperty("СовместноSHIFT", "ButtonShift")]
        public int ButtonShift
        {
            get { return 33554432; }
        }

        [ContextMethod("ВСтроку", "ВСтроку")]
        public string ToStringRu(decimal p1)
        {
            string str = p1.ToString();
            switch (p1)
            {
                case 134217727:
                    str = "ВсеСобытия";
                    break;
                case 8:
                    str = "Кнопка1ДвойнойКлик";
                    break;
                case 4:
                    str = "Кнопка1Кликнута";
                    break;
                case 2:
                    str = "Кнопка1Нажата";
                    break;
                case 1:
                    str = "Кнопка1Отпущена";
                    break;
                case 16:
                    str = "Кнопка1ТройнойКлик";
                    break;
                case 512:
                    str = "Кнопка2ДвойнойКлик";
                    break;
                case 256:
                    str = "Кнопка2Кликнута";
                    break;
                case 128:
                    str = "Кнопка2Нажата";
                    break;
                case 64:
                    str = "Кнопка2Отпущена";
                    break;
                case 1024:
                    str = "Кнопка2ТройнойКлик";
                    break;
                case 32768:
                    str = "Кнопка3ДвойнойКлик";
                    break;
                case 16384:
                    str = "Кнопка3Кликнута";
                    break;
                case 8192:
                    str = "Кнопка3Нажата";
                    break;
                case 4096:
                    str = "Кнопка3Отпущена";
                    break;
                case 65536:
                    str = "Кнопка3ТройнойКлик";
                    break;
                case 2097152:
                    str = "Кнопка4ДвойнойКлик";
                    break;
                case 1048576:
                    str = "Кнопка4Кликнута";
                    break;
                case 524288:
                    str = "Кнопка4Нажата";
                    break;
                case 262144:
                    str = "Кнопка4Отпущена";
                    break;
                case 4194304:
                    str = "Кнопка4ТройнойКлик";
                    break;
                case 553648128:
                    str = "КолесоCTRLНажато";
                    break;
                case 285212672:
                    str = "КолесоCTRLОтпущено";
                    break;
                case 536870912:
                    str = "КолесоНажато";
                    break;
                case 268435456:
                    str = "КолесоОтпущено";
                    break;
                case 0:
                    str = "Отсутствие";
                    break;
                case 134217728:
                    str = "ПозицияМыши";
                    break;
                case 67108864:
                    str = "СовместноALT";
                    break;
                case 16777216:
                    str = "СовместноCTRL";
                    break;
                case 33554432:
                    str = "СовместноSHIFT";
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
                case 134217727:
                    str = "AllEvents";
                    break;
                case 8:
                    str = "Button1DoubleClicked";
                    break;
                case 4:
                    str = "Button1Clicked";
                    break;
                case 2:
                    str = "Button1Pressed";
                    break;
                case 1:
                    str = "Button1Released";
                    break;
                case 16:
                    str = "Button1TripleClicked";
                    break;
                case 512:
                    str = "Button2DoubleClicked";
                    break;
                case 256:
                    str = "Button2Clicked";
                    break;
                case 128:
                    str = "Button2Pressed";
                    break;
                case 64:
                    str = "Button2Released";
                    break;
                case 1024:
                    str = "Button2TripleClicked";
                    break;
                case 32768:
                    str = "Button3DoubleClicked";
                    break;
                case 16384:
                    str = "Button3Clicked";
                    break;
                case 8192:
                    str = "Button3Pressed";
                    break;
                case 4096:
                    str = "Button3Released";
                    break;
                case 65536:
                    str = "Button3TripleClicked";
                    break;
                case 2097152:
                    str = "Button4DoubleClicked";
                    break;
                case 1048576:
                    str = "Button4Clicked";
                    break;
                case 524288:
                    str = "Button4Pressed";
                    break;
                case 262144:
                    str = "Button4Released";
                    break;
                case 4194304:
                    str = "Button4TripleClicked";
                    break;
                case 553648128:
                    str = "WheeledRight";
                    break;
                case 285212672:
                    str = "WheeledLeft";
                    break;
                case 536870912:
                    str = "WheeledDown";
                    break;
                case 268435456:
                    str = "WheeledUp";
                    break;
                case 0:
                    str = "None";
                    break;
                case 134217728:
                    str = "ReportMousePosition";
                    break;
                case 67108864:
                    str = "ButtonAlt";
                    break;
                case 16777216:
                    str = "ButtonCtrl";
                    break;
                case 33554432:
                    str = "ButtonShift";
                    break;
            }
            return str;
        }
    }
}
