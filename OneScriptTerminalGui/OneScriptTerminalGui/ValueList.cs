using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library;
using ScriptEngine.HostedScript.Library.ValueList;
using System.Collections.Generic;

namespace ostgui
{
    // Этот модуль нужен для динамического изменения источника для элементов 
    // ПолеВыбора (ComboBox) и СписокЭлементов (ListView). Как только источник 
    // изменится ПолеВыбора и СписокЭлементов актуализируются.
    // По сути это обертка (wrapper-класс) над классом СписокЗначений из односкрипта. 
    // Вы можете использовать все методы-свойства, как если бы обращались к односкриптовому СписокЗначений.
    // Можно было бы обойтись без создания класса ValueList, создав только 
    // класс TfValueList и прописав всё нужное в нём. Но сделаю подробный вариант 
    // для себя, чтобы был пример кодирования события. Слабонервных прошу пропускать 
    // этот код.

    public class ValueList
    {
        public TfValueList dll_obj;
        public ValueListImpl M_ValueList;

        public ValueList()
        {
            M_ValueList = new ValueListImpl();
            Utils.AddToHashtable(M_ValueList, this);

            this.CollectionChanged += ValueList_CollectionChanged;
        }

        private void ValueList_CollectionChanged(object sender, AddEventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < M_ValueList.Count(); i++)
            {
                string item = M_ValueList.GetValue(ValueFactory.Create(i)).Presentation;
                if (item.Length != 0)
                {
                    list.Add(item);
                }
                else
                {
                    list.Add(M_ValueList.GetValue(ValueFactory.Create(i)).Value.ToString());
                }
            }
            ((dynamic)dll_obj.M_Owner).SetSource(list);
        }

        private event EventHandler<AddEventArgs> CollectionChanged;
        public void OnCollectionChanged(ValueList obj)
        {
            var handler = CollectionChanged;
            if (handler != null)
            {
                handler(this, new AddEventArgs(obj));
            }
        }

        public ValueListItem Add(IValue value, string presentation = null, bool check = false, IValue picture = null)
        {
            ValueListItem ValueListItem1 = M_ValueList.Add(value, presentation, check, picture);
            OnCollectionChanged(this);
            return ValueListItem1;
        }

        public ValueListItem GetValue(IValue index)
        {
            return M_ValueList.GetValue(index);
        }

        public ValueListItem Insert(int index, IValue value, string presentation = null, bool check = false, IValue picture = null)
        {
            ValueListItem ValueListItem1 = M_ValueList.Insert(index, value, presentation, check, picture);
            OnCollectionChanged(this);
            return ValueListItem1;
        }

        public void Clear()
        {
            M_ValueList.Clear();
            OnCollectionChanged(this);
        }

        public void FillChecks(bool check)
        {
            M_ValueList.FillChecks(check);
        }

        public int IndexOf(ValueListItem item)
        {
            return M_ValueList.IndexOf(item);
        }

        public IValue FindByValue(IValue val)
        {
            return M_ValueList.FindByValue(val);
        }

        public void Move(IValue item, int offset)
        {
            M_ValueList.Move(item, offset);
            OnCollectionChanged(this);
        }

        public void SortByValue(SortDirectionEnum? direction = null)
        {
            M_ValueList.SortByValue(direction);
            OnCollectionChanged(this);
        }

        public void SortByPresentation(SortDirectionEnum? direction = null)
        {
            M_ValueList.SortByPresentation(direction);
            OnCollectionChanged(this);
        }

        public void Delete(IValue item)
        {
            M_ValueList.Delete(item);
            OnCollectionChanged(this);
        }

        public int Count()
        {
            return M_ValueList.Count();
        }

        private class AddEventArgs : System.EventArgs
        {
            private ValueList valueList;

            public AddEventArgs(ValueList obj)
            {
                valueList = obj;
            }
            public ValueList Obj
            {
                get { return valueList; }
                set { valueList = value; }
            }
        }
    }

    [ContextClass("ТфСписокЗначений", "TfValueList")]
    public class TfValueList : AutoContext<TfValueList>
    {
        public Terminal.Gui.View M_Owner;

        public TfValueList()
        {
            ValueList ValueList1 = new ValueList();
            ValueList1.dll_obj = this;
            Base_obj = ValueList1;
        }

        public ValueList Base_obj;

        [ContextMethod("Добавить", "Add")]
        public ValueListItem Add(IValue value, string presentation = null, bool check = false, IValue picture = null)
        {
            ValueListItem ValueListItem1 = Base_obj.Add(value, presentation, check, picture);
            return ValueListItem1;
        }

        [ContextMethod("Количество", "Count")]
        public int Count()
        {
            return Base_obj.Count();
        }

        [ContextMethod("Получить", "Get")]
        public ValueListItem GetValue(IValue index)
        {
            return Base_obj.GetValue(index);
        }

        [ContextMethod("Вставить", "Insert")]
        public ValueListItem Insert(int index, IValue value, string presentation = null, bool check = false, IValue picture = null)
        {
            return Base_obj.Insert(index, value, presentation, check, picture);
        }

        [ContextMethod("Очистить", "Clear")]
        public void Clear()
        {
            Base_obj.Clear();
        }

        [ContextMethod("ЗаполнитьПометки", "FillChecks")]
        public void FillChecks(bool check)
        {
            Base_obj.FillChecks(check);
        }

        [ContextMethod("Индекс", "IndexOf")]
        public int IndexOf(ValueListItem item)
        {
            return Base_obj.IndexOf(item);
        }

        [ContextMethod("НайтиПоЗначению", "FindByValue")]
        public IValue FindByValue(IValue val)
        {
            return Base_obj.FindByValue(val);
        }

        [ContextMethod("Сдвинуть", "Move")]
        public void Move(IValue item, int offset)
        {
            Base_obj.Move(item, offset);
        }

        [ContextMethod("СортироватьПоЗначению", "SortByValue")]
        public void SortByValue(int p1 = 1)
        {
            if (p1 == 1)
            {
                Base_obj.SortByValue(SortDirectionEnum.Asc);
            }
            else if (p1 == 2)
            {
                Base_obj.SortByValue(SortDirectionEnum.Desc);
            }
        }

        [ContextMethod("СортироватьПоПредставлению", "SortByPresentation")]
        public void SortByPresentation(int p1 = 1)
        {
            if (p1 == 1)
            {
                Base_obj.SortByPresentation(SortDirectionEnum.Asc);
            }
            else if (p1 == 2)
            {
                Base_obj.SortByPresentation(SortDirectionEnum.Desc);
            }
        }

        [ContextMethod("Удалить", "Delete")]
        public void Delete(IValue item)
        {
            Base_obj.Delete(item);
        }
    }
}
