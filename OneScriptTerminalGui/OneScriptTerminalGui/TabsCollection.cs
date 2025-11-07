using ScriptEngine.Machine.Contexts;

namespace ostgui
{

    [ContextClass("ТфКоллекцияВкладок", "TfTabsCollection")]
    public class TfTabsCollection : AutoContext<TfTabsCollection>
    {

        public Terminal.Gui.TabView M_TabView;
        public Terminal.Gui.TabView.Tab[] M_Object;

        [ContextProperty("Количество", "Count")]
        public int Count
        {
            get { return Utils.IReadOnlyCollectionToArray(M_TabView.Tabs).Length; }
        }

        [ContextMethod("Добавить", "Add")]
        public void Add(TfTabPage p1, bool p2)
        {
            M_TabView.AddTab(p1.Base_obj.M_TabPage, p2);
        }

        [ContextMethod("Очистить", "Clear")]
        public void Clear()
        {
            M_Object = Utils.IReadOnlyCollectionToArray(M_TabView.Tabs);
            for (int i = 0; i < M_Object.Length; i++)
            {
                M_TabView.RemoveTab(M_Object[i]);
            }
        }

        [ContextMethod("Получить", "Get")]
        public TfTabPage Get(int p1)
        {
            M_Object = Utils.IReadOnlyCollectionToArray(M_TabView.Tabs);
            return Utils.RevertEqualsObj(M_Object[p1]).dll_obj;
        }

        [ContextMethod("Удалить", "Remove")]
        public void Remove(TfTabPage p1)
        {
            M_TabView.RemoveTab(p1.Base_obj.M_TabPage);
        }

    }
}
