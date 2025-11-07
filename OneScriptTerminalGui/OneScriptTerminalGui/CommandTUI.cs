using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ostgui
{
    [ContextClass("ТфКомандаTUI", "TfCommandTUI")]
    public class TfCommandTUI : AutoContext<TfCommandTUI>, ICollectionContext, IEnumerable<IValue>
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

        public TfCommandTUI()
        {
            _list = new List<decimal>
            {
                Redo,
                TopHome,
                TopHomeExtend,
                LineUpToFirstBranch,
                EnableOverwrite,
                Paste,
                SelectAll,
                Cut,
                CutToEndLine,
                CutToStartLine,
                EndOfPage,
                EndOfLine,
                EndOfLineExtend,
                Copy,
                Left,
                LeftHome,
                LeftHomeExtend,
                LeftExtend,
                StartOfPage,
                StartOfLine,
                StartOfLineExtend,
                LineDownToLastBranch,
                BottomEnd,
                BottomEndExtend,
                NewLine,
                Refresh,
                DisableOverwrite,
                OpenSelectedItem,
                Cancel,
                Undo,
                ToggleChecked,
                ToggleOverwrite,
                ToggleExtend,
                ToggleExpandCollapse,
                QuitToplevel,
                Right,
                RightEnd,
                RightEndExtend,
                RightExtend,
                PreviousView,
                PreviousViewOrTop,
                Accept,
                Suspend,
                ScrollUp,
                ScrollLeft,
                ScrollDown,
                ScrollRight,
                Expand,
                ExpandAll,
                Collapse,
                CollapseAll,
                NextView,
                Tab,
                NextViewOrTop,
                WordLeft,
                WordLeftExtend,
                WordRight,
                WordRightExtend,
                PageUp,
                PageUpExtend,
                PageLeft,
                PageDown,
                PageDownExtend,
                PageRight,
                LineUp,
                LineUpExtend,
                LineDown,
                LineDownExtend,
                BackTab,
                DeleteAll,
                DeleteCharLeft,
                DeleteCharRight,
                KillWordForwards,
                KillWordBackwards,
                UnixEmulation,
            }.Select(ValueFactory.Create).ToList();
        }

        private static readonly Dictionary<decimal, string> namesRu = new Dictionary<decimal, string>
        {
            {60, "Вернуть"},
            {29, "ВерхДомой"},
            {30, "ВерхДомойРасширить"},
            {6, "ВерхнийДочернийВетки"},
            {23, "ВключитьПерезапись"},
            {63, "Вставить"},
            {45, "ВыбратьВсе"},
            {62, "Вырезать"},
            {18, "ВырезатьДоКонцаСтроки"},
            {19, "ВырезатьДоНачалаСтроки"},
            {52, "КонецСтраницы"},
            {49, "КонецСтроки"},
            {50, "КонецСтрокиРасширить"},
            {61, "Копировать"},
            {8, "Лево"},
            {55, "ЛевоНачало"},
            {56, "ЛевоНачалоРасширить"},
            {10, "ЛевоРасширить"},
            {51, "НачалоСтраницы"},
            {47, "НачалоСтроки"},
            {48, "НачалоСтрокиРасширить"},
            {2, "НижнийДочернийВетки"},
            {31, "НизКонец"},
            {32, "НизКонецРасширить"},
            {72, "НоваяСтрока"},
            {70, "Обновить"},
            {24, "ОтключитьПерезапись"},
            {33, "ОткрытьВыбранный"},
            {41, "Отмена"},
            {59, "Отменить"},
            {34, "ПереключитьОтметку"},
            {22, "ПереключитьПерезапись"},
            {71, "ПереключитьРасширение"},
            {36, "ПереключитьСвертывание"},
            {64, "ПокинутьВерхний"},
            {11, "Право"},
            {57, "ПравоКонец"},
            {58, "ПравоКонецРасширить"},
            {13, "ПравоРасширить"},
            {67, "Предыдущий"},
            {69, "ПредыдущийИлиВерхний"},
            {35, "Принять"},
            {65, "Приостановить"},
            {7, "ПрокрутитьВверх"},
            {9, "ПрокрутитьВлево"},
            {3, "ПрокрутитьВниз"},
            {12, "ПрокрутитьВправо"},
            {37, "Развернуть"},
            {38, "РазвернутьВсе"},
            {39, "Свернуть"},
            {40, "СвернутьВсе"},
            {66, "Следующий"},
            {73, "Следующий"},
            {68, "СледующийИлиВерхний"},
            {14, "СловоВлево"},
            {15, "СловоВлевоРасширить"},
            {16, "СловоВправо"},
            {17, "СловоВправоРасширить"},
            {27, "СтраницаВверх"},
            {28, "СтраницаВверхРасширить"},
            {53, "СтраницаВлево"},
            {25, "СтраницаВниз"},
            {26, "СтраницаВнизРасширить"},
            {54, "СтраницаПраво"},
            {4, "СтрокаВверх"},
            {5, "СтрокаВверхРасширить"},
            {0, "СтрокаВниз"},
            {1, "СтрокаВнизРасширить"},
            {74, "ТабНазад"},
            {46, "УдалитьВсе"},
            {44, "УдалитьСимволСлева"},
            {43, "УдалитьСимволСправа"},
            {20, "УдалитьСловоВперед"},
            {21, "УдалитьСловоНазад"},
            {42, "ЮниксЭмуляция"},
        };

        private static readonly Dictionary<decimal, string> namesEn = new Dictionary<decimal, string>
        {
            {60, "Redo"},
            {29, "TopHome"},
            {30, "TopHomeExtend"},
            {6, "LineUpToFirstBranch"},
            {23, "EnableOverwrite"},
            {63, "Paste"},
            {45, "SelectAll"},
            {62, "Cut"},
            {18, "CutToEndLine"},
            {19, "CutToStartLine"},
            {52, "EndOfPage"},
            {49, "EndOfLine"},
            {50, "EndOfLineExtend"},
            {61, "Copy"},
            {8, "Left"},
            {55, "LeftHome"},
            {56, "LeftHomeExtend"},
            {10, "LeftExtend"},
            {51, "StartOfPage"},
            {47, "StartOfLine"},
            {48, "StartOfLineExtend"},
            {2, "LineDownToLastBranch"},
            {31, "BottomEnd"},
            {32, "BottomEndExtend"},
            {72, "NewLine"},
            {70, "Refresh"},
            {24, "DisableOverwrite"},
            {33, "OpenSelectedItem"},
            {41, "Cancel"},
            {59, "Undo"},
            {34, "ToggleChecked"},
            {22, "ToggleOverwrite"},
            {71, "ToggleExtend"},
            {36, "ToggleExpandCollapse"},
            {64, "QuitToplevel"},
            {11, "Right"},
            {57, "RightEnd"},
            {58, "RightEndExtend"},
            {13, "RightExtend"},
            {67, "PreviousView"},
            {69, "PreviousViewOrTop"},
            {35, "Accept"},
            {65, "Suspend"},
            {7, "ScrollUp"},
            {9, "ScrollLeft"},
            {3, "ScrollDown"},
            {12, "ScrollRight"},
            {37, "Expand"},
            {38, "ExpandAll"},
            {39, "Collapse"},
            {40, "CollapseAll"},
            {66, "NextView"},
            {73, "Tab"},
            {68, "NextViewOrTop"},
            {14, "WordLeft"},
            {15, "WordLeftExtend"},
            {16, "WordRight"},
            {17, "WordRightExtend"},
            {27, "PageUp"},
            {28, "PageUpExtend"},
            {53, "PageLeft"},
            {25, "PageDown"},
            {26, "PageDownExtend"},
            {54, "PageRight"},
            {4, "LineUp"},
            {5, "LineUpExtend"},
            {0, "LineDown"},
            {1, "LineDownExtend"},
            {74, "BackTab"},
            {46, "DeleteAll"},
            {44, "DeleteCharLeft"},
            {43, "DeleteCharRight"},
            {20, "KillWordForwards"},
            {21, "KillWordBackwards"},
            {42, "UnixEmulation"},
        };

        [ContextProperty("Вернуть", "Redo")]
        public decimal Redo => 60;

        [ContextProperty("ВерхДомой", "TopHome")]
        public decimal TopHome => 29;

        [ContextProperty("ВерхДомойРасширить", "TopHomeExtend")]
        public decimal TopHomeExtend => 30;

        [ContextProperty("ВерхнийДочернийВетки", "LineUpToFirstBranch")]
        public decimal LineUpToFirstBranch => 6;

        [ContextProperty("ВключитьПерезапись", "EnableOverwrite")]
        public decimal EnableOverwrite => 23;

        [ContextProperty("Вставить", "Paste")]
        public decimal Paste => 63;

        [ContextProperty("ВыбратьВсе", "SelectAll")]
        public decimal SelectAll => 45;

        [ContextProperty("Вырезать", "Cut")]
        public decimal Cut => 62;

        [ContextProperty("ВырезатьДоКонцаСтроки", "CutToEndLine")]
        public decimal CutToEndLine => 18;

        [ContextProperty("ВырезатьДоНачалаСтроки", "CutToStartLine")]
        public decimal CutToStartLine => 19;

        [ContextProperty("КонецСтраницы", "EndOfPage")]
        public decimal EndOfPage => 52;

        [ContextProperty("КонецСтроки", "EndOfLine")]
        public decimal EndOfLine => 49;

        [ContextProperty("КонецСтрокиРасширить", "EndOfLineExtend")]
        public decimal EndOfLineExtend => 50;

        [ContextProperty("Копировать", "Copy")]
        public decimal Copy => 61;

        [ContextProperty("Лево", "Left")]
        public decimal Left => 8;

        [ContextProperty("ЛевоНачало", "LeftHome")]
        public decimal LeftHome => 55;

        [ContextProperty("ЛевоНачалоРасширить", "LeftHomeExtend")]
        public decimal LeftHomeExtend => 56;

        [ContextProperty("ЛевоРасширить", "LeftExtend")]
        public decimal LeftExtend => 10;

        [ContextProperty("НачалоСтраницы", "StartOfPage")]
        public decimal StartOfPage => 51;

        [ContextProperty("НачалоСтроки", "StartOfLine")]
        public decimal StartOfLine => 47;

        [ContextProperty("НачалоСтрокиРасширить", "StartOfLineExtend")]
        public decimal StartOfLineExtend => 48;

        [ContextProperty("НижнийДочернийВетки", "LineDownToLastBranch")]
        public decimal LineDownToLastBranch => 2;

        [ContextProperty("НизКонец", "BottomEnd")]
        public decimal BottomEnd => 31;

        [ContextProperty("НизКонецРасширить", "BottomEndExtend")]
        public decimal BottomEndExtend => 32;

        [ContextProperty("НоваяСтрока", "NewLine")]
        public decimal NewLine => 72;

        [ContextProperty("Обновить", "Refresh")]
        public decimal Refresh => 70;

        [ContextProperty("ОтключитьПерезапись", "DisableOverwrite")]
        public decimal DisableOverwrite => 24;

        [ContextProperty("ОткрытьВыбранный", "OpenSelectedItem")]
        public decimal OpenSelectedItem => 33;

        [ContextProperty("Отмена", "Cancel")]
        public decimal Cancel => 41;

        [ContextProperty("Отменить", "Undo")]
        public decimal Undo => 59;

        [ContextProperty("ПереключитьОтметку", "ToggleChecked")]
        public decimal ToggleChecked => 34;

        [ContextProperty("ПереключитьПерезапись", "ToggleOverwrite")]
        public decimal ToggleOverwrite => 22;

        [ContextProperty("ПереключитьРасширение", "ToggleExtend")]
        public decimal ToggleExtend => 71;

        [ContextProperty("ПереключитьСвертывание", "ToggleExpandCollapse")]
        public decimal ToggleExpandCollapse => 36;

        [ContextProperty("ПокинутьВерхний", "QuitToplevel")]
        public decimal QuitToplevel => 64;

        [ContextProperty("Право", "Right")]
        public decimal Right => 11;

        [ContextProperty("ПравоКонец", "RightEnd")]
        public decimal RightEnd => 57;

        [ContextProperty("ПравоКонецРасширить", "RightEndExtend")]
        public decimal RightEndExtend => 58;

        [ContextProperty("ПравоРасширить", "RightExtend")]
        public decimal RightExtend => 13;

        [ContextProperty("Предыдущий", "PreviousView")]
        public decimal PreviousView => 67;

        [ContextProperty("ПредыдущийИлиВерхний", "PreviousViewOrTop")]
        public decimal PreviousViewOrTop => 69;

        [ContextProperty("Принять", "Accept")]
        public decimal Accept => 35;

        [ContextProperty("Приостановить", "Suspend")]
        public decimal Suspend => 65;

        [ContextProperty("ПрокрутитьВверх", "ScrollUp")]
        public decimal ScrollUp => 7;

        [ContextProperty("ПрокрутитьВлево", "ScrollLeft")]
        public decimal ScrollLeft => 9;

        [ContextProperty("ПрокрутитьВниз", "ScrollDown")]
        public decimal ScrollDown => 3;

        [ContextProperty("ПрокрутитьВправо", "ScrollRight")]
        public decimal ScrollRight => 12;

        [ContextProperty("Развернуть", "Expand")]
        public decimal Expand => 37;

        [ContextProperty("РазвернутьВсе", "ExpandAll")]
        public decimal ExpandAll => 38;

        [ContextProperty("Свернуть", "Collapse")]
        public decimal Collapse => 39;

        [ContextProperty("СвернутьВсе", "CollapseAll")]
        public decimal CollapseAll => 40;

        [ContextProperty("Следующий", "NextView")]
        public decimal NextView => 66;

        [ContextProperty("Следующий", "Tab")]
        public decimal Tab => 73;

        [ContextProperty("СледующийИлиВерхний", "NextViewOrTop")]
        public decimal NextViewOrTop => 68;

        [ContextProperty("СловоВлево", "WordLeft")]
        public decimal WordLeft => 14;

        [ContextProperty("СловоВлевоРасширить", "WordLeftExtend")]
        public decimal WordLeftExtend => 15;

        [ContextProperty("СловоВправо", "WordRight")]
        public decimal WordRight => 16;

        [ContextProperty("СловоВправоРасширить", "WordRightExtend")]
        public decimal WordRightExtend => 17;

        [ContextProperty("СтраницаВверх", "PageUp")]
        public decimal PageUp => 27;

        [ContextProperty("СтраницаВверхРасширить", "PageUpExtend")]
        public decimal PageUpExtend => 28;

        [ContextProperty("СтраницаВлево", "PageLeft")]
        public decimal PageLeft => 53;

        [ContextProperty("СтраницаВниз", "PageDown")]
        public decimal PageDown => 25;

        [ContextProperty("СтраницаВнизРасширить", "PageDownExtend")]
        public decimal PageDownExtend => 26;

        [ContextProperty("СтраницаПраво", "PageRight")]
        public decimal PageRight => 54;

        [ContextProperty("СтрокаВверх", "LineUp")]
        public decimal LineUp => 4;

        [ContextProperty("СтрокаВверхРасширить", "LineUpExtend")]
        public decimal LineUpExtend => 5;

        [ContextProperty("СтрокаВниз", "LineDown")]
        public decimal LineDown => 0;

        [ContextProperty("СтрокаВнизРасширить", "LineDownExtend")]
        public decimal LineDownExtend => 1;

        [ContextProperty("ТабНазад", "BackTab")]
        public decimal BackTab => 74;

        [ContextProperty("УдалитьВсе", "DeleteAll")]
        public decimal DeleteAll => 46;

        [ContextProperty("УдалитьСимволСлева", "DeleteCharLeft")]
        public decimal DeleteCharLeft => 44;

        [ContextProperty("УдалитьСимволСправа", "DeleteCharRight")]
        public decimal DeleteCharRight => 43;

        [ContextProperty("УдалитьСловоВперед", "KillWordForwards")]
        public decimal KillWordForwards => 20;

        [ContextProperty("УдалитьСловоНазад", "KillWordBackwards")]
        public decimal KillWordBackwards => 21;

        [ContextProperty("ЮниксЭмуляция", "UnixEmulation")]
        public decimal UnixEmulation => 42;
    }
}
