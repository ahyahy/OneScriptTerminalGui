using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;

namespace ostgui
{
    public class SubviewCollection : System.Collections.IEnumerator, System.Collections.IEnumerable
    {
        public TfSubviewCollection dll_obj;
        public IList<Terminal.Gui.View> M_SubviewCollection;
        public System.Collections.IEnumerator Enumerator;
        public object current;

        public SubviewCollection(IList<Terminal.Gui.View> p1)
        {
            M_SubviewCollection = p1;
        }

        public IList<Terminal.Gui.View> List
        {
            get { return M_SubviewCollection; }
        }

        public IEnumerator<Terminal.Gui.View> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        }

        public virtual object Current
        {
            get { return current; }
        }

        public virtual void Reset()
        {
            Enumerator.Reset();
        }

        public virtual bool MoveNext()
        {
            return Enumerator.MoveNext();
        }
    }

    [ContextClass("ТфКоллекцияПодэлементов", "TfSubviewCollection")]
    public class TfSubviewCollection : AutoContext<TfSubviewCollection>, ICollectionContext, IEnumerable<IValue>
    {

        public TfSubviewCollection(IList<Terminal.Gui.View> p1)
        {
            SubviewCollection SubviewCollection1 = new SubviewCollection(p1);
            SubviewCollection1.dll_obj = this;
            Base_obj = SubviewCollection1;
        }

        public int Count()
        {
            return CountControl;
        }

        public CollectionEnumerator GetManagedIterator()
        {
            return new CollectionEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<IValue>)this).GetEnumerator();
        }

        IEnumerator<IValue> IEnumerable<IValue>.GetEnumerator()
        {
            foreach (var item in Base_obj.M_SubviewCollection)
            {
                yield return (OneScriptTerminalGui.RevertEqualsObj(item).dll_obj as IValue);
            }
        }

        public SubviewCollection Base_obj;

        [ContextProperty("Количество", "Count")]
        public int CountControl
        {
            get { return Base_obj.M_SubviewCollection.Count; }
        }

        [ContextMethod("Индекс", "IndexOf")]
        public int IndexOf(IValue p1)
        {
            int index1 = -1;
            for (int i = 0; i < Base_obj.M_SubviewCollection.Count; i++)
            {
                if (Base_obj.M_SubviewCollection[i] == ((dynamic)p1).Base_obj.M_View)
                {
                    index1 = i;
                    break;
                }
            }
            return index1;
        }

        [ContextMethod("Содержит", "Contains")]
        public bool Contains(IValue p1)
        {
            return Base_obj.M_SubviewCollection.Contains((Terminal.Gui.View)((dynamic)p1).Base_obj.M_View);
        }

        [ContextMethod("Элемент", "Item")]
        public IValue Item(int p1)
        {
            return OneScriptTerminalGui.RevertEqualsObj(Base_obj.M_SubviewCollection[p1]).dll_obj;
        }

    }
}
