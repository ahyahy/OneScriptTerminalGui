using ScriptEngine.Machine.Contexts;

namespace ostgui
{

    [ContextClass("ТфЭлементыСтрокиСостояния", "TfStatusBarItems")]
    public class TfStatusBarItems : AutoContext<TfStatusBarItems>
    {

        public Terminal.Gui.StatusBar M_StatusBar;

        public Terminal.Gui.StatusItem[] M_Object
        {
            get { return M_StatusBar.Items; }
            set { M_StatusBar.Items = value; }
        }

        [ContextProperty("Количество", "Count")]
        public int Count
        {
            get { return M_Object.Length; }
        }

        [ContextMethod("Добавить", "Add")]
        public TfStatusItem Add(TfStatusItem p1)
        {
            Terminal.Gui.StatusItem[] StatusItem2 = new Terminal.Gui.StatusItem[M_Object.Length + 1];
            M_Object.CopyTo(StatusItem2, 0);
            StatusItem2[M_Object.Length] = p1.Base_obj.M_StatusItem;
            M_Object = StatusItem2;
            p1.M_StatusBar = M_StatusBar;
            return p1;
        }

        [ContextMethod("Очистить", "Clear")]
        public void Clear()
        {
            M_Object = new Terminal.Gui.StatusItem[0];
        }

        [ContextMethod("Получить", "Get")]
        public TfStatusItem Get(int p1)
        {
            return Utils.RevertEqualsObj(M_Object[p1]).dll_obj;
        }

        [ContextMethod("Удалить", "Remove")]
        public void Remove(TfStatusItem p1)
        {
            Terminal.Gui.StatusItem[] StatusItem2 = new Terminal.Gui.StatusItem[M_Object.Length - 1];
            int index = 0;
            for (int i = 0; i < M_Object.Length; i++)
            {
                Terminal.Gui.StatusItem StatusItem1 = M_Object[i];
                if (StatusItem1 != p1.Base_obj.M_StatusItem)
                {
                    StatusItem2[index] = StatusItem1;
                    index++;
                }
            }
            M_Object = StatusItem2;
        }

    }
}
