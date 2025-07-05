using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace ostgui
{
    [ContextClass("ТфДействие", "TfAction")]
    public class TfAction : AutoContext<TfAction>
    {
        public TfAction(IRuntimeContextInstance script = null, string methodName = null, IValue param = null)
        {
            Script = script;
            MethodName = methodName;
            Parameter = param;
        }

        [ContextProperty("ИмяМетода", "MethodName")]
        public string MethodName { get; set; }

        [ContextProperty("Параметр", "Parameter")]
        public IValue Parameter { get; set; }

        [ContextProperty("Сценарий", "Script")]
        public IRuntimeContextInstance Script { get; set; }
    }
}
