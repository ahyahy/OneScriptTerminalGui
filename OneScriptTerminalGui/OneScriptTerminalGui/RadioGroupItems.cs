using ScriptEngine.Machine.Contexts;
using NStack;

namespace ostgui
{

    [ContextClass("ТфМеткиПереключателя", "TfRadioGroupItems")]
    public class TfRadioGroupItems : AutoContext<TfRadioGroupItems>
    {

        public Terminal.Gui.RadioGroup M_RadioGroup;

        public ustring[] M_Object
        {
            get { return M_RadioGroup.RadioLabels; }
            set { M_RadioGroup.RadioLabels = value; }
        }

        [ContextProperty("Количество", "Count")]
        public int Count
        {
            get { return M_Object.Length; }
        }

        [ContextMethod("Добавить", "Add")]
        public void Add(string p1)
        {
            ustring[] ustring2 = new ustring[M_Object.Length + 1];
            M_Object.CopyTo(ustring2, 0);
            ustring2[M_Object.Length] = p1;
            M_Object = ustring2;
        }

        [ContextMethod("Очистить", "Clear")]
        public void Clear()
        {
            M_Object = new ustring[0];
        }

        [ContextMethod("Получить", "Get")]
        public string Get(int p1)
        {
            return M_Object[p1].ToString();
        }

        [ContextMethod("Удалить", "Remove")]
        public void Remove(string p1)
        {
            ustring[] ustring2 = new ustring[M_Object.Length - 1];
            int index = 0;
            for (int i = 0; i < M_Object.Length; i++)
            {
                ustring ustring1 = M_Object[i];
                if (ustring1 != p1)
                {
                    ustring2[index] = ustring1;
                    index++;
                }
            }
            M_Object = ustring2;
        }

    }
}
