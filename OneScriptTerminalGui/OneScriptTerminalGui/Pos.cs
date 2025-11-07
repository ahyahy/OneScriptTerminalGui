using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace ostgui
{
    public class Pos
    {
        public TfPos dll_obj;
        public Terminal.Gui.Pos M_Pos;

        public Pos()
        {
            M_Pos = new Terminal.Gui.Pos();
            Utils.AddToHashtable(M_Pos, this);
        }

        public Pos(Terminal.Gui.Pos p1)
        {
            M_Pos = p1;
            Utils.AddToHashtable(M_Pos, this);
        }

        public Pos At(int p1)
        {
            return new Pos(Terminal.Gui.Pos.At(p1));
        }

        public Pos Top(ostgui.View p1)
        {
            return new Pos(Terminal.Gui.Pos.Top(p1.M_View));
        }

        public Pos Y(ostgui.View p1)
        {
            return new Pos(Terminal.Gui.Pos.Y(p1.M_View));
        }

        public Pos X(ostgui.View p1)
        {
            return new Pos(Terminal.Gui.Pos.X(p1.M_View));
        }

        public Pos Left(ostgui.View p1)
        {
            return new Pos(Terminal.Gui.Pos.Left(p1.M_View));
        }

        public Pos Bottom(ostgui.View p1)
        {
            return new Pos(Terminal.Gui.Pos.Bottom(p1.M_View));
        }

        public Pos Right(ostgui.View p1)
        {
            return new Pos(Terminal.Gui.Pos.Right(p1.M_View));
        }

        public Pos Percent(float p1)
        {
            return new Pos(Terminal.Gui.Pos.Percent(p1));
        }

        public Pos Center()
        {
            return new Pos(Terminal.Gui.Pos.Center());
        }

        public Pos AnchorEnd(int p1 = 0)
        {
            return new Pos(Terminal.Gui.Pos.AnchorEnd(p1));
        }

        public Pos Summation(Pos p1, Pos p2)
        {
            return new Pos(p1.M_Pos + p2.M_Pos);
        }

        public Pos Subtract(Pos p1, Pos p2)
        {
            return new Pos(p1.M_Pos - p2.M_Pos);
        }
    }

    [ContextClass("ТфПозиция", "TfPos")]
    public class TfPos : AutoContext<TfPos>
    {

        public TfPos()
        {
            Pos Pos1 = new Pos();
            Pos1.dll_obj = this;
            Base_obj = Pos1;
        }

        public TfPos(Pos p1)
        {
            Pos Pos1 = p1;
            Pos1.dll_obj = this;
            Base_obj = Pos1;
        }

        public Pos Base_obj;

        [ContextMethod("Абсолютно", "At")]
        public TfPos At(int p1)
        {
            return new TfPos(Base_obj.At(p1));
        }

        [ContextMethod("Верх", "Top")]
        public TfPos Top(IValue p1)
        {
            return new TfPos(Base_obj.Top(((dynamic)p1).Base_obj));
        }

        [ContextMethod("Вычесть", "Subtract")]
        public TfPos Subtract(TfPos p1, TfPos p2)
        {
            return new TfPos(Base_obj.Subtract(p1.Base_obj, p2.Base_obj));
        }

        [ContextMethod("Игрек", "Y")]
        public TfPos Y(IValue p1)
        {
            return new TfPos(Base_obj.Y(((dynamic)p1).Base_obj));
        }

        [ContextMethod("Икс", "X")]
        public TfPos X(IValue p1)
        {
            return new TfPos(Base_obj.X(((dynamic)p1).Base_obj));
        }

        [ContextMethod("Лево", "Left")]
        public TfPos Left(IValue p1)
        {
            return new TfPos(Base_obj.Left(((dynamic)p1).Base_obj));
        }

        [ContextMethod("Низ", "Bottom")]
        public TfPos Bottom(IValue p1)
        {
            return new TfPos(Base_obj.Bottom(((dynamic)p1).Base_obj));
        }

        [ContextMethod("Право", "Right")]
        public TfPos Right(IValue p1)
        {
            return new TfPos(Base_obj.Right(((dynamic)p1).Base_obj));
        }

        [ContextMethod("Процент", "Percent")]
        public TfPos Percent(decimal p1)
        {
            return new TfPos(Base_obj.Percent(Utils.ToFloat(p1)));
        }

        [ContextMethod("Сложить", "Summation")]
        public TfPos Summation(TfPos p1, TfPos p2)
        {
            return new TfPos(Base_obj.Summation(p1.Base_obj, p2.Base_obj));
        }

        [ContextMethod("Центр", "Center")]
        public TfPos Center()
        {
            return new TfPos(Base_obj.Center());
        }

        [ContextMethod("ЯкорьКонец", "AnchorEnd")]
        public TfPos AnchorEnd(int p1 = 0)
        {
            return new TfPos(Base_obj.AnchorEnd(p1));
        }

    }
}
