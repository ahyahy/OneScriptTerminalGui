using ScriptEngine.Machine.Contexts;

namespace ostgui
{
    public class Attribute
    {
        public TfAttribute dll_obj;
        public Terminal.Gui.Attribute M_Attribute;

        public Attribute()
        {
            M_Attribute = new Terminal.Gui.Attribute();
            OneScriptTerminalGui.AddToHashtable(M_Attribute, this);
        }

        public Attribute(int p1)
        {
            M_Attribute = new Terminal.Gui.Attribute((Terminal.Gui.Color)p1);
            OneScriptTerminalGui.AddToHashtable(M_Attribute, this);
        }

        public Attribute(int p1, int p2)
        {
            M_Attribute = new Terminal.Gui.Attribute((Terminal.Gui.Color)p1, (Terminal.Gui.Color)p2);
            OneScriptTerminalGui.AddToHashtable(M_Attribute, this);
        }

        public Attribute(int p1, int p2, int p3)
        {
            M_Attribute = new Terminal.Gui.Attribute(p1, (Terminal.Gui.Color)p2, (Terminal.Gui.Color)p3);
            OneScriptTerminalGui.AddToHashtable(M_Attribute, this);
        }

        public Attribute(Terminal.Gui.Attribute p1)
        {
            M_Attribute = p1;
            OneScriptTerminalGui.AddToHashtable(M_Attribute, this);
        }

        public bool Initialized
        {
            get { return M_Attribute.Initialized; }
        }

        public bool HasValidColors
        {
            get { return M_Attribute.HasValidColors; }
        }

        public int Value
        {
            get { return M_Attribute.Value; }
        }

        public int Foreground
        {
            get { return (int)M_Attribute.Foreground; }
        }

        public int Background
        {
            get { return (int)M_Attribute.Background; }
        }
    }

    [ContextClass("ТфАтрибут", "TfAttribute")]
    public class TfAttribute : AutoContext<TfAttribute>
    {

        public TfAttribute()
        {
            Attribute Attribute1 = new Attribute();
            Attribute1.dll_obj = this;
            Base_obj = Attribute1;
        }

        public TfAttribute(int p1)
        {
            Attribute Attribute1 = new Attribute(p1);
            Attribute1.dll_obj = this;
            Base_obj = Attribute1;
        }

        public TfAttribute(int p1, int p2)
        {
            Attribute Attribute1 = new Attribute(p1, p2);
            Attribute1.dll_obj = this;
            Base_obj = Attribute1;
        }

        public TfAttribute(int p1, int p2, int p3)
        {
            Attribute Attribute1 = new Attribute(p1, p2, p3);
            Attribute1.dll_obj = this;
            Base_obj = Attribute1;
        }

        public TfAttribute(ostgui.Attribute p1)
        {
            Attribute Attribute1 = p1;
            Attribute1.dll_obj = this;
            Base_obj = Attribute1;
        }

        public Attribute Base_obj;

        [ContextProperty("Значение", "Value")]
        public int Value
        {
            get { return Base_obj.Value; }
        }

        [ContextProperty("ОсновнойЦвет", "Foreground")]
        public int Foreground
        {
            get { return Base_obj.Foreground; }
        }

        [ContextProperty("ЦветФона", "Background")]
        public int Background
        {
            get { return Base_obj.Background; }
        }

    }
}
