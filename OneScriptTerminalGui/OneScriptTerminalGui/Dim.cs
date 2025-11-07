using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace ostgui
{
    public class Dim
    {
        public TfDim dll_obj;
        public Terminal.Gui.Dim M_Dim;

        public Dim()
        {
            M_Dim = new Terminal.Gui.Dim();
            Utils.AddToHashtable(M_Dim, this);
        }

        public Dim(Terminal.Gui.Dim p1)
        {
            M_Dim = p1;
            Utils.AddToHashtable(M_Dim, this);
        }

        public Dim Percent(float p1, bool p2 = false)
        {
            return new Dim(Terminal.Gui.Dim.Percent(p1, p2));
        }

        public Dim Height(View p1)
        {
            return new Dim(Terminal.Gui.Dim.Height(p1.M_View));
        }

        public Dim Fill(int p1 = 0)
        {
            return new Dim(Terminal.Gui.Dim.Fill(p1));
        }

        public Dim Sized(int p1)
        {
            return new Dim(Terminal.Gui.Dim.Sized(p1));
        }

        public Dim Width(View p1)
        {
            return new Dim(Terminal.Gui.Dim.Width(p1.M_View));
        }

        public Dim Summation(Dim p1, Dim p2)
        {
            return new Dim(p1.M_Dim + p2.M_Dim);
        }

        public Dim Subtract(Dim p1, Dim p2)
        {
            return new Dim(p1.M_Dim - p2.M_Dim);
        }
    }

    [ContextClass("ТфВеличина", "TfDim")]
    public class TfDim : AutoContext<TfDim>
    {

        public TfDim()
        {
            Dim Dim1 = new Dim();
            Dim1.dll_obj = this;
            Base_obj = Dim1;
        }

        public TfDim(Dim p1)
        {
            Dim Dim1 = p1;
            Dim1.dll_obj = this;
            Base_obj = Dim1;
        }

        public Dim Base_obj;

        [ContextMethod("Абсолютно", "Sized")]
        public TfDim Sized(int p1)
        {
            return new TfDim(Base_obj.Sized(p1));
        }

        [ContextMethod("Высота", "Height")]
        public TfDim Height(IValue p1)
        {
            return new TfDim(Base_obj.Height(((dynamic)p1).Base_obj));
        }

        [ContextMethod("Вычесть", "Subtract")]
        public TfDim Subtract(TfDim p1, TfDim p2)
        {
            return new TfDim(Base_obj.Subtract(p1.Base_obj, p2.Base_obj));
        }

        [ContextMethod("Заполнить", "Fill")]
        public TfDim Fill(int p1 = 0)
        {
            return new TfDim(Base_obj.Fill(p1));
        }

        [ContextMethod("Процент", "Percent")]
        public TfDim Percent(IValue p1, bool p2 = false)
        {
            return new TfDim(Base_obj.Percent(Utils.ToFloat(p1), p2));
        }

        [ContextMethod("Сложить", "Summation")]
        public TfDim Summation(TfDim p1, TfDim p2)
        {
            return new TfDim(Base_obj.Summation(p1.Base_obj, p2.Base_obj));
        }

        [ContextMethod("Ширина", "Width")]
        public TfDim Width(IValue p1)
        {
            return new TfDim(Base_obj.Width(((dynamic)p1).Base_obj));
        }

    }
}
