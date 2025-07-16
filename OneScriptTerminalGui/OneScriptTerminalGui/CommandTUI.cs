using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;

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

        public TfCommandTUI()
        {
            _list = new List<IValue>();
            _list.Add(ValueFactory.Create(Redo));
            _list.Add(ValueFactory.Create(TopHome));
            _list.Add(ValueFactory.Create(TopHomeExtend));
            _list.Add(ValueFactory.Create(LineUpToFirstBranch));
            _list.Add(ValueFactory.Create(EnableOverwrite));
            _list.Add(ValueFactory.Create(Paste));
            _list.Add(ValueFactory.Create(SelectAll));
            _list.Add(ValueFactory.Create(Cut));
            _list.Add(ValueFactory.Create(CutToEndLine));
            _list.Add(ValueFactory.Create(CutToStartLine));
            _list.Add(ValueFactory.Create(EndOfPage));
            _list.Add(ValueFactory.Create(EndOfLine));
            _list.Add(ValueFactory.Create(EndOfLineExtend));
            _list.Add(ValueFactory.Create(Copy));
            _list.Add(ValueFactory.Create(Left));
            _list.Add(ValueFactory.Create(LeftHome));
            _list.Add(ValueFactory.Create(LeftHomeExtend));
            _list.Add(ValueFactory.Create(LeftExtend));
            _list.Add(ValueFactory.Create(StartOfPage));
            _list.Add(ValueFactory.Create(StartOfLine));
            _list.Add(ValueFactory.Create(StartOfLineExtend));
            _list.Add(ValueFactory.Create(LineDownToLastBranch));
            _list.Add(ValueFactory.Create(BottomEnd));
            _list.Add(ValueFactory.Create(BottomEndExtend));
            _list.Add(ValueFactory.Create(NewLine));
            _list.Add(ValueFactory.Create(Refresh));
            _list.Add(ValueFactory.Create(DisableOverwrite));
            _list.Add(ValueFactory.Create(OpenSelectedItem));
            _list.Add(ValueFactory.Create(Cancel));
            _list.Add(ValueFactory.Create(Undo));
            _list.Add(ValueFactory.Create(ToggleChecked));
            _list.Add(ValueFactory.Create(ToggleOverwrite));
            _list.Add(ValueFactory.Create(ToggleExtend));
            _list.Add(ValueFactory.Create(ToggleExpandCollapse));
            _list.Add(ValueFactory.Create(QuitToplevel));
            _list.Add(ValueFactory.Create(Right));
            _list.Add(ValueFactory.Create(RightEnd));
            _list.Add(ValueFactory.Create(RightEndExtend));
            _list.Add(ValueFactory.Create(RightExtend));
            _list.Add(ValueFactory.Create(PreviousView));
            _list.Add(ValueFactory.Create(PreviousViewOrTop));
            _list.Add(ValueFactory.Create(Accept));
            _list.Add(ValueFactory.Create(Suspend));
            _list.Add(ValueFactory.Create(ScrollUp));
            _list.Add(ValueFactory.Create(ScrollLeft));
            _list.Add(ValueFactory.Create(ScrollDown));
            _list.Add(ValueFactory.Create(ScrollRight));
            _list.Add(ValueFactory.Create(Expand));
            _list.Add(ValueFactory.Create(ExpandAll));
            _list.Add(ValueFactory.Create(Collapse));
            _list.Add(ValueFactory.Create(CollapseAll));
            _list.Add(ValueFactory.Create(NextView));
            _list.Add(ValueFactory.Create(Tab));
            _list.Add(ValueFactory.Create(NextViewOrTop));
            _list.Add(ValueFactory.Create(WordLeft));
            _list.Add(ValueFactory.Create(WordLeftExtend));
            _list.Add(ValueFactory.Create(WordRight));
            _list.Add(ValueFactory.Create(WordRightExtend));
            _list.Add(ValueFactory.Create(PageUp));
            _list.Add(ValueFactory.Create(PageUpExtend));
            _list.Add(ValueFactory.Create(PageLeft));
            _list.Add(ValueFactory.Create(PageDown));
            _list.Add(ValueFactory.Create(PageDownExtend));
            _list.Add(ValueFactory.Create(PageRight));
            _list.Add(ValueFactory.Create(LineUp));
            _list.Add(ValueFactory.Create(LineUpExtend));
            _list.Add(ValueFactory.Create(LineDown));
            _list.Add(ValueFactory.Create(LineDownExtend));
            _list.Add(ValueFactory.Create(BackTab));
            _list.Add(ValueFactory.Create(DeleteAll));
            _list.Add(ValueFactory.Create(DeleteCharLeft));
            _list.Add(ValueFactory.Create(DeleteCharRight));
            _list.Add(ValueFactory.Create(KillWordForwards));
            _list.Add(ValueFactory.Create(KillWordBackwards));
            _list.Add(ValueFactory.Create(UnixEmulation));
        }

        [ContextProperty("Вернуть", "Redo")]
        public int Redo
        {
            get { return 60; }
        }

        [ContextProperty("ВерхДомой", "TopHome")]
        public int TopHome
        {
            get { return 29; }
        }

        [ContextProperty("ВерхДомойРасширить", "TopHomeExtend")]
        public int TopHomeExtend
        {
            get { return 30; }
        }

        [ContextProperty("ВерхнийДочернийВетки", "LineUpToFirstBranch")]
        public int LineUpToFirstBranch
        {
            get { return 6; }
        }

        [ContextProperty("ВключитьПерезапись", "EnableOverwrite")]
        public int EnableOverwrite
        {
            get { return 23; }
        }

        [ContextProperty("Вставить", "Paste")]
        public int Paste
        {
            get { return 63; }
        }

        [ContextProperty("ВыбратьВсе", "SelectAll")]
        public int SelectAll
        {
            get { return 45; }
        }

        [ContextProperty("Вырезать", "Cut")]
        public int Cut
        {
            get { return 62; }
        }

        [ContextProperty("ВырезатьДоКонцаСтроки", "CutToEndLine")]
        public int CutToEndLine
        {
            get { return 18; }
        }

        [ContextProperty("ВырезатьДоНачалаСтроки", "CutToStartLine")]
        public int CutToStartLine
        {
            get { return 19; }
        }

        [ContextProperty("КонецСтраницы", "EndOfPage")]
        public int EndOfPage
        {
            get { return 52; }
        }

        [ContextProperty("КонецСтроки", "EndOfLine")]
        public int EndOfLine
        {
            get { return 49; }
        }

        [ContextProperty("КонецСтрокиРасширить", "EndOfLineExtend")]
        public int EndOfLineExtend
        {
            get { return 50; }
        }

        [ContextProperty("Копировать", "Copy")]
        public int Copy
        {
            get { return 61; }
        }

        [ContextProperty("Лево", "Left")]
        public int Left
        {
            get { return 8; }
        }

        [ContextProperty("ЛевоНачало", "LeftHome")]
        public int LeftHome
        {
            get { return 55; }
        }

        [ContextProperty("ЛевоНачалоРасширить", "LeftHomeExtend")]
        public int LeftHomeExtend
        {
            get { return 56; }
        }

        [ContextProperty("ЛевоРасширить", "LeftExtend")]
        public int LeftExtend
        {
            get { return 10; }
        }

        [ContextProperty("НачалоСтраницы", "StartOfPage")]
        public int StartOfPage
        {
            get { return 51; }
        }

        [ContextProperty("НачалоСтроки", "StartOfLine")]
        public int StartOfLine
        {
            get { return 47; }
        }

        [ContextProperty("НачалоСтрокиРасширить", "StartOfLineExtend")]
        public int StartOfLineExtend
        {
            get { return 48; }
        }

        [ContextProperty("НижнийДочернийВетки", "LineDownToLastBranch")]
        public int LineDownToLastBranch
        {
            get { return 2; }
        }

        [ContextProperty("НизКонец", "BottomEnd")]
        public int BottomEnd
        {
            get { return 31; }
        }

        [ContextProperty("НизКонецРасширить", "BottomEndExtend")]
        public int BottomEndExtend
        {
            get { return 32; }
        }

        [ContextProperty("НоваяСтрока", "NewLine")]
        public int NewLine
        {
            get { return 72; }
        }

        [ContextProperty("Обновить", "Refresh")]
        public int Refresh
        {
            get { return 70; }
        }

        [ContextProperty("ОтключитьПерезапись", "DisableOverwrite")]
        public int DisableOverwrite
        {
            get { return 24; }
        }

        [ContextProperty("ОткрытьВыбранный", "OpenSelectedItem")]
        public int OpenSelectedItem
        {
            get { return 33; }
        }

        [ContextProperty("Отмена", "Cancel")]
        public int Cancel
        {
            get { return 41; }
        }

        [ContextProperty("Отменить", "Undo")]
        public int Undo
        {
            get { return 59; }
        }

        [ContextProperty("ПереключитьОтметку", "ToggleChecked")]
        public int ToggleChecked
        {
            get { return 34; }
        }

        [ContextProperty("ПереключитьПерезапись", "ToggleOverwrite")]
        public int ToggleOverwrite
        {
            get { return 22; }
        }

        [ContextProperty("ПереключитьРасширение", "ToggleExtend")]
        public int ToggleExtend
        {
            get { return 71; }
        }

        [ContextProperty("ПереключитьСвертывание", "ToggleExpandCollapse")]
        public int ToggleExpandCollapse
        {
            get { return 36; }
        }

        [ContextProperty("ПокинутьВерхний", "QuitToplevel")]
        public int QuitToplevel
        {
            get { return 64; }
        }

        [ContextProperty("Право", "Right")]
        public int Right
        {
            get { return 11; }
        }

        [ContextProperty("ПравоКонец", "RightEnd")]
        public int RightEnd
        {
            get { return 57; }
        }

        [ContextProperty("ПравоКонецРасширить", "RightEndExtend")]
        public int RightEndExtend
        {
            get { return 58; }
        }

        [ContextProperty("ПравоРасширить", "RightExtend")]
        public int RightExtend
        {
            get { return 13; }
        }

        [ContextProperty("Предыдущий", "PreviousView")]
        public int PreviousView
        {
            get { return 67; }
        }

        [ContextProperty("ПредыдущийИлиВерхний", "PreviousViewOrTop")]
        public int PreviousViewOrTop
        {
            get { return 69; }
        }

        [ContextProperty("Принять", "Accept")]
        public int Accept
        {
            get { return 35; }
        }

        [ContextProperty("Приостановить", "Suspend")]
        public int Suspend
        {
            get { return 65; }
        }

        [ContextProperty("ПрокрутитьВверх", "ScrollUp")]
        public int ScrollUp
        {
            get { return 7; }
        }

        [ContextProperty("ПрокрутитьВлево", "ScrollLeft")]
        public int ScrollLeft
        {
            get { return 9; }
        }

        [ContextProperty("ПрокрутитьВниз", "ScrollDown")]
        public int ScrollDown
        {
            get { return 3; }
        }

        [ContextProperty("ПрокрутитьВправо", "ScrollRight")]
        public int ScrollRight
        {
            get { return 12; }
        }

        [ContextProperty("Развернуть", "Expand")]
        public int Expand
        {
            get { return 37; }
        }

        [ContextProperty("РазвернутьВсе", "ExpandAll")]
        public int ExpandAll
        {
            get { return 38; }
        }

        [ContextProperty("Свернуть", "Collapse")]
        public int Collapse
        {
            get { return 39; }
        }

        [ContextProperty("СвернутьВсе", "CollapseAll")]
        public int CollapseAll
        {
            get { return 40; }
        }

        [ContextProperty("Следующий", "NextView")]
        public int NextView
        {
            get { return 66; }
        }

        [ContextProperty("Следующий", "Tab")]
        public int Tab
        {
            get { return 73; }
        }

        [ContextProperty("СледующийИлиВерхний", "NextViewOrTop")]
        public int NextViewOrTop
        {
            get { return 68; }
        }

        [ContextProperty("СловоВлево", "WordLeft")]
        public int WordLeft
        {
            get { return 14; }
        }

        [ContextProperty("СловоВлевоРасширить", "WordLeftExtend")]
        public int WordLeftExtend
        {
            get { return 15; }
        }

        [ContextProperty("СловоВправо", "WordRight")]
        public int WordRight
        {
            get { return 16; }
        }

        [ContextProperty("СловоВправоРасширить", "WordRightExtend")]
        public int WordRightExtend
        {
            get { return 17; }
        }

        [ContextProperty("СтраницаВерх", "PageUp")]
        public int PageUp
        {
            get { return 27; }
        }

        [ContextProperty("СтраницаВерхРасширить", "PageUpExtend")]
        public int PageUpExtend
        {
            get { return 28; }
        }

        [ContextProperty("СтраницаВлево", "PageLeft")]
        public int PageLeft
        {
            get { return 53; }
        }

        [ContextProperty("СтраницаВниз", "PageDown")]
        public int PageDown
        {
            get { return 25; }
        }

        [ContextProperty("СтраницаВнизРасширить", "PageDownExtend")]
        public int PageDownExtend
        {
            get { return 26; }
        }

        [ContextProperty("СтраницаПраво", "PageRight")]
        public int PageRight
        {
            get { return 54; }
        }

        [ContextProperty("СтрокаВерх", "LineUp")]
        public int LineUp
        {
            get { return 4; }
        }

        [ContextProperty("СтрокаВерхРасширить", "LineUpExtend")]
        public int LineUpExtend
        {
            get { return 5; }
        }

        [ContextProperty("СтрокаВниз", "LineDown")]
        public int LineDown
        {
            get { return 0; }
        }

        [ContextProperty("СтрокаВнизРасширить", "LineDownExtend")]
        public int LineDownExtend
        {
            get { return 1; }
        }

        [ContextProperty("ТабНазад", "BackTab")]
        public int BackTab
        {
            get { return 74; }
        }

        [ContextProperty("УдалитьВсе", "DeleteAll")]
        public int DeleteAll
        {
            get { return 46; }
        }

        [ContextProperty("УдалитьСимволСлева", "DeleteCharLeft")]
        public int DeleteCharLeft
        {
            get { return 44; }
        }

        [ContextProperty("УдалитьСимволСправа", "DeleteCharRight")]
        public int DeleteCharRight
        {
            get { return 43; }
        }

        [ContextProperty("УдалитьСловоВперед", "KillWordForwards")]
        public int KillWordForwards
        {
            get { return 20; }
        }

        [ContextProperty("УдалитьСловоНазад", "KillWordBackwards")]
        public int KillWordBackwards
        {
            get { return 21; }
        }

        [ContextProperty("ЮниксЭмуляция", "UnixEmulation")]
        public int UnixEmulation
        {
            get { return 42; }
        }

        [ContextMethod("ВСтроку", "ВСтроку")]
        public string ToStringRu(decimal p1)
        {
            string str = p1.ToString();
            switch (p1)
            {
                case 60:
                    str = "Вернуть";
                    break;
                case 29:
                    str = "ВерхДомой";
                    break;
                case 30:
                    str = "ВерхДомойРасширить";
                    break;
                case 6:
                    str = "ВерхнийДочернийВетки";
                    break;
                case 23:
                    str = "ВключитьПерезапись";
                    break;
                case 63:
                    str = "Вставить";
                    break;
                case 45:
                    str = "ВыбратьВсе";
                    break;
                case 62:
                    str = "Вырезать";
                    break;
                case 18:
                    str = "ВырезатьДоКонцаСтроки";
                    break;
                case 19:
                    str = "ВырезатьДоНачалаСтроки";
                    break;
                case 52:
                    str = "КонецСтраницы";
                    break;
                case 49:
                    str = "КонецСтроки";
                    break;
                case 50:
                    str = "КонецСтрокиРасширить";
                    break;
                case 61:
                    str = "Копировать";
                    break;
                case 8:
                    str = "Лево";
                    break;
                case 55:
                    str = "ЛевоНачало";
                    break;
                case 56:
                    str = "ЛевоНачалоРасширить";
                    break;
                case 10:
                    str = "ЛевоРасширить";
                    break;
                case 51:
                    str = "НачалоСтраницы";
                    break;
                case 47:
                    str = "НачалоСтроки";
                    break;
                case 48:
                    str = "НачалоСтрокиРасширить";
                    break;
                case 2:
                    str = "НижнийДочернийВетки";
                    break;
                case 31:
                    str = "НизКонец";
                    break;
                case 32:
                    str = "НизКонецРасширить";
                    break;
                case 72:
                    str = "НоваяСтрока";
                    break;
                case 70:
                    str = "Обновить";
                    break;
                case 24:
                    str = "ОтключитьПерезапись";
                    break;
                case 33:
                    str = "ОткрытьВыбранный";
                    break;
                case 41:
                    str = "Отмена";
                    break;
                case 59:
                    str = "Отменить";
                    break;
                case 34:
                    str = "ПереключитьОтметку";
                    break;
                case 22:
                    str = "ПереключитьПерезапись";
                    break;
                case 71:
                    str = "ПереключитьРасширение";
                    break;
                case 36:
                    str = "ПереключитьСвертывание";
                    break;
                case 64:
                    str = "ПокинутьВерхний";
                    break;
                case 11:
                    str = "Право";
                    break;
                case 57:
                    str = "ПравоКонец";
                    break;
                case 58:
                    str = "ПравоКонецРасширить";
                    break;
                case 13:
                    str = "ПравоРасширить";
                    break;
                case 67:
                    str = "Предыдущий";
                    break;
                case 69:
                    str = "ПредыдущийИлиВерхний";
                    break;
                case 35:
                    str = "Принять";
                    break;
                case 65:
                    str = "Приостановить";
                    break;
                case 7:
                    str = "ПрокрутитьВверх";
                    break;
                case 9:
                    str = "ПрокрутитьВлево";
                    break;
                case 3:
                    str = "ПрокрутитьВниз";
                    break;
                case 12:
                    str = "ПрокрутитьВправо";
                    break;
                case 37:
                    str = "Развернуть";
                    break;
                case 38:
                    str = "РазвернутьВсе";
                    break;
                case 39:
                    str = "Свернуть";
                    break;
                case 40:
                    str = "СвернутьВсе";
                    break;
                case 66:
                    str = "Следующий";
                    break;
                case 73:
                    str = "Следующий";
                    break;
                case 68:
                    str = "СледующийИлиВерхний";
                    break;
                case 14:
                    str = "СловоВлево";
                    break;
                case 15:
                    str = "СловоВлевоРасширить";
                    break;
                case 16:
                    str = "СловоВправо";
                    break;
                case 17:
                    str = "СловоВправоРасширить";
                    break;
                case 27:
                    str = "СтраницаВерх";
                    break;
                case 28:
                    str = "СтраницаВерхРасширить";
                    break;
                case 53:
                    str = "СтраницаВлево";
                    break;
                case 25:
                    str = "СтраницаВниз";
                    break;
                case 26:
                    str = "СтраницаВнизРасширить";
                    break;
                case 54:
                    str = "СтраницаПраво";
                    break;
                case 4:
                    str = "СтрокаВерх";
                    break;
                case 5:
                    str = "СтрокаВерхРасширить";
                    break;
                case 0:
                    str = "СтрокаВниз";
                    break;
                case 1:
                    str = "СтрокаВнизРасширить";
                    break;
                case 74:
                    str = "ТабНазад";
                    break;
                case 46:
                    str = "УдалитьВсе";
                    break;
                case 44:
                    str = "УдалитьСимволСлева";
                    break;
                case 43:
                    str = "УдалитьСимволСправа";
                    break;
                case 20:
                    str = "УдалитьСловоВперед";
                    break;
                case 21:
                    str = "УдалитьСловоНазад";
                    break;
                case 42:
                    str = "ЮниксЭмуляция";
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
                case 60:
                    str = "Redo";
                    break;
                case 29:
                    str = "TopHome";
                    break;
                case 30:
                    str = "TopHomeExtend";
                    break;
                case 6:
                    str = "LineUpToFirstBranch";
                    break;
                case 23:
                    str = "EnableOverwrite";
                    break;
                case 63:
                    str = "Paste";
                    break;
                case 45:
                    str = "SelectAll";
                    break;
                case 62:
                    str = "Cut";
                    break;
                case 18:
                    str = "CutToEndLine";
                    break;
                case 19:
                    str = "CutToStartLine";
                    break;
                case 52:
                    str = "EndOfPage";
                    break;
                case 49:
                    str = "EndOfLine";
                    break;
                case 50:
                    str = "EndOfLineExtend";
                    break;
                case 61:
                    str = "Copy";
                    break;
                case 8:
                    str = "Left";
                    break;
                case 55:
                    str = "LeftHome";
                    break;
                case 56:
                    str = "LeftHomeExtend";
                    break;
                case 10:
                    str = "LeftExtend";
                    break;
                case 51:
                    str = "StartOfPage";
                    break;
                case 47:
                    str = "StartOfLine";
                    break;
                case 48:
                    str = "StartOfLineExtend";
                    break;
                case 2:
                    str = "LineDownToLastBranch";
                    break;
                case 31:
                    str = "BottomEnd";
                    break;
                case 32:
                    str = "BottomEndExtend";
                    break;
                case 72:
                    str = "NewLine";
                    break;
                case 70:
                    str = "Refresh";
                    break;
                case 24:
                    str = "DisableOverwrite";
                    break;
                case 33:
                    str = "OpenSelectedItem";
                    break;
                case 41:
                    str = "Cancel";
                    break;
                case 59:
                    str = "Undo";
                    break;
                case 34:
                    str = "ToggleChecked";
                    break;
                case 22:
                    str = "ToggleOverwrite";
                    break;
                case 71:
                    str = "ToggleExtend";
                    break;
                case 36:
                    str = "ToggleExpandCollapse";
                    break;
                case 64:
                    str = "QuitToplevel";
                    break;
                case 11:
                    str = "Right";
                    break;
                case 57:
                    str = "RightEnd";
                    break;
                case 58:
                    str = "RightEndExtend";
                    break;
                case 13:
                    str = "RightExtend";
                    break;
                case 67:
                    str = "PreviousView";
                    break;
                case 69:
                    str = "PreviousViewOrTop";
                    break;
                case 35:
                    str = "Accept";
                    break;
                case 65:
                    str = "Suspend";
                    break;
                case 7:
                    str = "ScrollUp";
                    break;
                case 9:
                    str = "ScrollLeft";
                    break;
                case 3:
                    str = "ScrollDown";
                    break;
                case 12:
                    str = "ScrollRight";
                    break;
                case 37:
                    str = "Expand";
                    break;
                case 38:
                    str = "ExpandAll";
                    break;
                case 39:
                    str = "Collapse";
                    break;
                case 40:
                    str = "CollapseAll";
                    break;
                case 66:
                    str = "NextView";
                    break;
                case 73:
                    str = "Tab";
                    break;
                case 68:
                    str = "NextViewOrTop";
                    break;
                case 14:
                    str = "WordLeft";
                    break;
                case 15:
                    str = "WordLeftExtend";
                    break;
                case 16:
                    str = "WordRight";
                    break;
                case 17:
                    str = "WordRightExtend";
                    break;
                case 27:
                    str = "PageUp";
                    break;
                case 28:
                    str = "PageUpExtend";
                    break;
                case 53:
                    str = "PageLeft";
                    break;
                case 25:
                    str = "PageDown";
                    break;
                case 26:
                    str = "PageDownExtend";
                    break;
                case 54:
                    str = "PageRight";
                    break;
                case 4:
                    str = "LineUp";
                    break;
                case 5:
                    str = "LineUpExtend";
                    break;
                case 0:
                    str = "LineDown";
                    break;
                case 1:
                    str = "LineDownExtend";
                    break;
                case 74:
                    str = "BackTab";
                    break;
                case 46:
                    str = "DeleteAll";
                    break;
                case 44:
                    str = "DeleteCharLeft";
                    break;
                case 43:
                    str = "DeleteCharRight";
                    break;
                case 20:
                    str = "KillWordForwards";
                    break;
                case 21:
                    str = "KillWordBackwards";
                    break;
                case 42:
                    str = "UnixEmulation";
                    break;
            }
            return str;
        }
    }
}
