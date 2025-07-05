using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;

namespace ostgui
{
    [ContextClass("ТфКлавиши", "TfKeys")]
    public class TfKeys : AutoContext<TfKeys>, ICollectionContext, IEnumerable<IValue>
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

        public TfKeys()
        {
            _list = new List<IValue>();
            _list.Add(ValueFactory.Create(a_low));
            _list.Add(ValueFactory.Create(A_up));
            _list.Add(ValueFactory.Create(AltMask));
            _list.Add(ValueFactory.Create(b_low));
            _list.Add(ValueFactory.Create(B_up));
            _list.Add(ValueFactory.Create(Backspace));
            _list.Add(ValueFactory.Create(BackTab));
            _list.Add(ValueFactory.Create(c_low));
            _list.Add(ValueFactory.Create(C_up));
            _list.Add(ValueFactory.Create(CharMask));
            _list.Add(ValueFactory.Create(Clear));
            _list.Add(ValueFactory.Create(CtrlMask));
            _list.Add(ValueFactory.Create(CursorDown));
            _list.Add(ValueFactory.Create(CursorLeft));
            _list.Add(ValueFactory.Create(CursorRight));
            _list.Add(ValueFactory.Create(CursorUp));
            _list.Add(ValueFactory.Create(d_low));
            _list.Add(ValueFactory.Create(D_up));
            _list.Add(ValueFactory.Create(D0));
            _list.Add(ValueFactory.Create(D1));
            _list.Add(ValueFactory.Create(D2));
            _list.Add(ValueFactory.Create(D3));
            _list.Add(ValueFactory.Create(D4));
            _list.Add(ValueFactory.Create(D5));
            _list.Add(ValueFactory.Create(D6));
            _list.Add(ValueFactory.Create(D7));
            _list.Add(ValueFactory.Create(D8));
            _list.Add(ValueFactory.Create(D9));
            _list.Add(ValueFactory.Create(Delete));
            _list.Add(ValueFactory.Create(DeleteChar));
            _list.Add(ValueFactory.Create(e_low));
            _list.Add(ValueFactory.Create(E_up));
            _list.Add(ValueFactory.Create(End));
            _list.Add(ValueFactory.Create(Enter));
            _list.Add(ValueFactory.Create(Esc));
            _list.Add(ValueFactory.Create(f_low));
            _list.Add(ValueFactory.Create(F_up));
            _list.Add(ValueFactory.Create(F1));
            _list.Add(ValueFactory.Create(F10));
            _list.Add(ValueFactory.Create(F11));
            _list.Add(ValueFactory.Create(F12));
            _list.Add(ValueFactory.Create(F2));
            _list.Add(ValueFactory.Create(F3));
            _list.Add(ValueFactory.Create(F4));
            _list.Add(ValueFactory.Create(F5));
            _list.Add(ValueFactory.Create(F6));
            _list.Add(ValueFactory.Create(F7));
            _list.Add(ValueFactory.Create(F8));
            _list.Add(ValueFactory.Create(F9));
            _list.Add(ValueFactory.Create(g_low));
            _list.Add(ValueFactory.Create(G_up));
            _list.Add(ValueFactory.Create(h_low));
            _list.Add(ValueFactory.Create(H_up));
            _list.Add(ValueFactory.Create(Home));
            _list.Add(ValueFactory.Create(i_low));
            _list.Add(ValueFactory.Create(I_up));
            _list.Add(ValueFactory.Create(InsertChar));
            _list.Add(ValueFactory.Create(j_low));
            _list.Add(ValueFactory.Create(J_up));
            _list.Add(ValueFactory.Create(k_low));
            _list.Add(ValueFactory.Create(K_up));
            _list.Add(ValueFactory.Create(l_low));
            _list.Add(ValueFactory.Create(L_up));
            _list.Add(ValueFactory.Create(m_low));
            _list.Add(ValueFactory.Create(M_up));
            _list.Add(ValueFactory.Create(n_low));
            _list.Add(ValueFactory.Create(N_up));
            _list.Add(ValueFactory.Create(Null));
            _list.Add(ValueFactory.Create(o_low));
            _list.Add(ValueFactory.Create(O_up));
            _list.Add(ValueFactory.Create(p_low));
            _list.Add(ValueFactory.Create(P_up));
            _list.Add(ValueFactory.Create(PageDown));
            _list.Add(ValueFactory.Create(PageUp));
            _list.Add(ValueFactory.Create(PrintScreen));
            _list.Add(ValueFactory.Create(q_low));
            _list.Add(ValueFactory.Create(Q_up));
            _list.Add(ValueFactory.Create(r_low));
            _list.Add(ValueFactory.Create(R_up));
            _list.Add(ValueFactory.Create(s_low));
            _list.Add(ValueFactory.Create(S_up));
            _list.Add(ValueFactory.Create(ShiftMask));
            _list.Add(ValueFactory.Create(Space));
            _list.Add(ValueFactory.Create(SpecialMask));
            _list.Add(ValueFactory.Create(t_low));
            _list.Add(ValueFactory.Create(T_up));
            _list.Add(ValueFactory.Create(Tab));
            _list.Add(ValueFactory.Create(u_low));
            _list.Add(ValueFactory.Create(U_up));
            _list.Add(ValueFactory.Create(Unknown));
            _list.Add(ValueFactory.Create(v_low));
            _list.Add(ValueFactory.Create(V_up));
            _list.Add(ValueFactory.Create(w_low));
            _list.Add(ValueFactory.Create(W_up));
            _list.Add(ValueFactory.Create(x_low));
            _list.Add(ValueFactory.Create(X_up));
            _list.Add(ValueFactory.Create(y_low));
            _list.Add(ValueFactory.Create(Y_up));
            _list.Add(ValueFactory.Create(z_low));
            _list.Add(ValueFactory.Create(Z_up));
        }

        [ContextProperty("a_low", "a_low")]
        public int a_low
        {
            get { return 97; }
        }

        [ContextProperty("A_up", "A_up")]
        public int A_up
        {
            get { return 65; }
        }

        [ContextProperty("AltMask", "AltMask")]
        public decimal AltMask
        {
            get { return 2147483648; }
        }

        [ContextProperty("b_low", "b_low")]
        public int b_low
        {
            get { return 98; }
        }

        [ContextProperty("B_up", "B_up")]
        public int B_up
        {
            get { return 66; }
        }

        [ContextProperty("Backspace", "Backspace")]
        public int Backspace
        {
            get { return 8; }
        }

        [ContextProperty("BackTab", "BackTab")]
        public int BackTab
        {
            get { return 1048586; }
        }

        [ContextProperty("c_low", "c_low")]
        public int c_low
        {
            get { return 99; }
        }

        [ContextProperty("C_up", "C_up")]
        public int C_up
        {
            get { return 67; }
        }

        [ContextProperty("CharMask", "CharMask")]
        public int CharMask
        {
            get { return 1048575; }
        }

        [ContextProperty("Clear", "Clear")]
        public int Clear
        {
            get { return 12; }
        }

        [ContextProperty("CtrlMask", "CtrlMask")]
        public int CtrlMask
        {
            get { return 1073741824; }
        }

        [ContextProperty("CursorDown", "CursorDown")]
        public int CursorDown
        {
            get { return 1048577; }
        }

        [ContextProperty("CursorLeft", "CursorLeft")]
        public int CursorLeft
        {
            get { return 1048578; }
        }

        [ContextProperty("CursorRight", "CursorRight")]
        public int CursorRight
        {
            get { return 1048579; }
        }

        [ContextProperty("CursorUp", "CursorUp")]
        public int CursorUp
        {
            get { return 1048576; }
        }

        [ContextProperty("d_low", "d_low")]
        public int d_low
        {
            get { return 100; }
        }

        [ContextProperty("D_up", "D_up")]
        public int D_up
        {
            get { return 68; }
        }

        [ContextProperty("D0", "D0")]
        public int D0
        {
            get { return 48; }
        }

        [ContextProperty("D1", "D1")]
        public int D1
        {
            get { return 49; }
        }

        [ContextProperty("D2", "D2")]
        public int D2
        {
            get { return 50; }
        }

        [ContextProperty("D3", "D3")]
        public int D3
        {
            get { return 51; }
        }

        [ContextProperty("D4", "D4")]
        public int D4
        {
            get { return 52; }
        }

        [ContextProperty("D5", "D5")]
        public int D5
        {
            get { return 53; }
        }

        [ContextProperty("D6", "D6")]
        public int D6
        {
            get { return 54; }
        }

        [ContextProperty("D7", "D7")]
        public int D7
        {
            get { return 55; }
        }

        [ContextProperty("D8", "D8")]
        public int D8
        {
            get { return 56; }
        }

        [ContextProperty("D9", "D9")]
        public int D9
        {
            get { return 57; }
        }

        [ContextProperty("Delete", "Delete")]
        public int Delete
        {
            get { return 127; }
        }

        [ContextProperty("DeleteChar", "DeleteChar")]
        public int DeleteChar
        {
            get { return 1048585; }
        }

        [ContextProperty("e_low", "e_low")]
        public int e_low
        {
            get { return 101; }
        }

        [ContextProperty("E_up", "E_up")]
        public int E_up
        {
            get { return 69; }
        }

        [ContextProperty("End", "End")]
        public int End
        {
            get { return 1048583; }
        }

        [ContextProperty("Enter", "Enter")]
        public int Enter
        {
            get { return 10; }
        }

        [ContextProperty("Esc", "Esc")]
        public int Esc
        {
            get { return 27; }
        }

        [ContextProperty("f_low", "f_low")]
        public int f_low
        {
            get { return 102; }
        }

        [ContextProperty("F_up", "F_up")]
        public int F_up
        {
            get { return 70; }
        }

        [ContextProperty("F1", "F1")]
        public int F1
        {
            get { return 1048588; }
        }

        [ContextProperty("F10", "F10")]
        public int F10
        {
            get { return 1048597; }
        }

        [ContextProperty("F11", "F11")]
        public int F11
        {
            get { return 1048598; }
        }

        [ContextProperty("F12", "F12")]
        public int F12
        {
            get { return 1048599; }
        }

        [ContextProperty("F2", "F2")]
        public int F2
        {
            get { return 1048589; }
        }

        [ContextProperty("F3", "F3")]
        public int F3
        {
            get { return 1048590; }
        }

        [ContextProperty("F4", "F4")]
        public int F4
        {
            get { return 1048591; }
        }

        [ContextProperty("F5", "F5")]
        public int F5
        {
            get { return 1048592; }
        }

        [ContextProperty("F6", "F6")]
        public int F6
        {
            get { return 1048593; }
        }

        [ContextProperty("F7", "F7")]
        public int F7
        {
            get { return 1048594; }
        }

        [ContextProperty("F8", "F8")]
        public int F8
        {
            get { return 1048595; }
        }

        [ContextProperty("F9", "F9")]
        public int F9
        {
            get { return 1048596; }
        }

        [ContextProperty("g_low", "g_low")]
        public int g_low
        {
            get { return 103; }
        }

        [ContextProperty("G_up", "G_up")]
        public int G_up
        {
            get { return 71; }
        }

        [ContextProperty("h_low", "h_low")]
        public int h_low
        {
            get { return 104; }
        }

        [ContextProperty("H_up", "H_up")]
        public int H_up
        {
            get { return 72; }
        }

        [ContextProperty("Home", "Home")]
        public int Home
        {
            get { return 1048582; }
        }

        [ContextProperty("i_low", "i_low")]
        public int i_low
        {
            get { return 105; }
        }

        [ContextProperty("I_up", "I_up")]
        public int I_up
        {
            get { return 73; }
        }

        [ContextProperty("InsertChar", "InsertChar")]
        public int InsertChar
        {
            get { return 45; }
        }

        [ContextProperty("j_low", "j_low")]
        public int j_low
        {
            get { return 106; }
        }

        [ContextProperty("J_up", "J_up")]
        public int J_up
        {
            get { return 74; }
        }

        [ContextProperty("k_low", "k_low")]
        public int k_low
        {
            get { return 107; }
        }

        [ContextProperty("K_up", "K_up")]
        public int K_up
        {
            get { return 75; }
        }

        [ContextProperty("l_low", "l_low")]
        public int l_low
        {
            get { return 108; }
        }

        [ContextProperty("L_up", "L_up")]
        public int L_up
        {
            get { return 76; }
        }

        [ContextProperty("m_low", "m_low")]
        public int m_low
        {
            get { return 109; }
        }

        [ContextProperty("M_up", "M_up")]
        public int M_up
        {
            get { return 77; }
        }

        [ContextProperty("n_low", "n_low")]
        public int n_low
        {
            get { return 110; }
        }

        [ContextProperty("N_up", "N_up")]
        public int N_up
        {
            get { return 78; }
        }

        [ContextProperty("Null", "Null")]
        public int Null
        {
            get { return 0; }
        }

        [ContextProperty("o_low", "o_low")]
        public int o_low
        {
            get { return 111; }
        }

        [ContextProperty("O_up", "O_up")]
        public int O_up
        {
            get { return 79; }
        }

        [ContextProperty("p_low", "p_low")]
        public int p_low
        {
            get { return 112; }
        }

        [ContextProperty("P_up", "P_up")]
        public int P_up
        {
            get { return 80; }
        }

        [ContextProperty("PageDown", "PageDown")]
        public int PageDown
        {
            get { return 1048581; }
        }

        [ContextProperty("PageUp", "PageUp")]
        public int PageUp
        {
            get { return 1048580; }
        }

        [ContextProperty("PrintScreen", "PrintScreen")]
        public int PrintScreen
        {
            get { return 1048587; }
        }

        [ContextProperty("q_low", "q_low")]
        public int q_low
        {
            get { return 113; }
        }

        [ContextProperty("Q_up", "Q_up")]
        public int Q_up
        {
            get { return 81; }
        }

        [ContextProperty("r_low", "r_low")]
        public int r_low
        {
            get { return 114; }
        }

        [ContextProperty("R_up", "R_up")]
        public int R_up
        {
            get { return 82; }
        }

        [ContextProperty("s_low", "s_low")]
        public int s_low
        {
            get { return 115; }
        }

        [ContextProperty("S_up", "S_up")]
        public int S_up
        {
            get { return 83; }
        }

        [ContextProperty("ShiftMask", "ShiftMask")]
        public int ShiftMask
        {
            get { return 268435456; }
        }

        [ContextProperty("Space", "Space")]
        public int Space
        {
            get { return 32; }
        }

        [ContextProperty("SpecialMask", "SpecialMask")]
        public decimal SpecialMask
        {
            get { return 4293918720; }
        }

        [ContextProperty("t_low", "t_low")]
        public int t_low
        {
            get { return 116; }
        }

        [ContextProperty("T_up", "T_up")]
        public int T_up
        {
            get { return 84; }
        }

        [ContextProperty("Tab", "Tab")]
        public int Tab
        {
            get { return 9; }
        }

        [ContextProperty("u_low", "u_low")]
        public int u_low
        {
            get { return 117; }
        }

        [ContextProperty("U_up", "U_up")]
        public int U_up
        {
            get { return 85; }
        }

        [ContextProperty("Unknown", "Unknown")]
        public int Unknown
        {
            get { return 1048612; }
        }

        [ContextProperty("v_low", "v_low")]
        public int v_low
        {
            get { return 118; }
        }

        [ContextProperty("V_up", "V_up")]
        public int V_up
        {
            get { return 86; }
        }

        [ContextProperty("w_low", "w_low")]
        public int w_low
        {
            get { return 119; }
        }

        [ContextProperty("W_up", "W_up")]
        public int W_up
        {
            get { return 87; }
        }

        [ContextProperty("x_low", "x_low")]
        public int x_low
        {
            get { return 120; }
        }

        [ContextProperty("X_up", "X_up")]
        public int X_up
        {
            get { return 88; }
        }

        [ContextProperty("y_low", "y_low")]
        public int y_low
        {
            get { return 121; }
        }

        [ContextProperty("Y_up", "Y_up")]
        public int Y_up
        {
            get { return 89; }
        }

        [ContextProperty("z_low", "z_low")]
        public int z_low
        {
            get { return 122; }
        }

        [ContextProperty("Z_up", "Z_up")]
        public int Z_up
        {
            get { return 90; }
        }

        [ContextMethod("ВСтроку", "ВСтроку")]
        public string ToStringRu(decimal p1)
        {
            string str = p1.ToString();
            switch (p1)
            {
                case 97:
                    str = "a_low";
                    break;
                case 65:
                    str = "A_up";
                    break;
                case 2147483648:
                    str = "AltMask";
                    break;
                case 98:
                    str = "b_low";
                    break;
                case 66:
                    str = "B_up";
                    break;
                case 8:
                    str = "Backspace";
                    break;
                case 1048586:
                    str = "BackTab";
                    break;
                case 99:
                    str = "c_low";
                    break;
                case 67:
                    str = "C_up";
                    break;
                case 1048575:
                    str = "CharMask";
                    break;
                case 12:
                    str = "Clear";
                    break;
                case 1073741824:
                    str = "CtrlMask";
                    break;
                case 1048577:
                    str = "CursorDown";
                    break;
                case 1048578:
                    str = "CursorLeft";
                    break;
                case 1048579:
                    str = "CursorRight";
                    break;
                case 1048576:
                    str = "CursorUp";
                    break;
                case 100:
                    str = "d_low";
                    break;
                case 68:
                    str = "D_up";
                    break;
                case 48:
                    str = "D0";
                    break;
                case 49:
                    str = "D1";
                    break;
                case 50:
                    str = "D2";
                    break;
                case 51:
                    str = "D3";
                    break;
                case 52:
                    str = "D4";
                    break;
                case 53:
                    str = "D5";
                    break;
                case 54:
                    str = "D6";
                    break;
                case 55:
                    str = "D7";
                    break;
                case 56:
                    str = "D8";
                    break;
                case 57:
                    str = "D9";
                    break;
                case 127:
                    str = "Delete";
                    break;
                case 1048585:
                    str = "DeleteChar";
                    break;
                case 101:
                    str = "e_low";
                    break;
                case 69:
                    str = "E_up";
                    break;
                case 1048583:
                    str = "End";
                    break;
                case 10:
                    str = "Enter";
                    break;
                case 27:
                    str = "Esc";
                    break;
                case 102:
                    str = "f_low";
                    break;
                case 70:
                    str = "F_up";
                    break;
                case 1048588:
                    str = "F1";
                    break;
                case 1048597:
                    str = "F10";
                    break;
                case 1048598:
                    str = "F11";
                    break;
                case 1048599:
                    str = "F12";
                    break;
                case 1048589:
                    str = "F2";
                    break;
                case 1048590:
                    str = "F3";
                    break;
                case 1048591:
                    str = "F4";
                    break;
                case 1048592:
                    str = "F5";
                    break;
                case 1048593:
                    str = "F6";
                    break;
                case 1048594:
                    str = "F7";
                    break;
                case 1048595:
                    str = "F8";
                    break;
                case 1048596:
                    str = "F9";
                    break;
                case 103:
                    str = "g_low";
                    break;
                case 71:
                    str = "G_up";
                    break;
                case 104:
                    str = "h_low";
                    break;
                case 72:
                    str = "H_up";
                    break;
                case 1048582:
                    str = "Home";
                    break;
                case 105:
                    str = "i_low";
                    break;
                case 73:
                    str = "I_up";
                    break;
                case 45:
                    str = "InsertChar";
                    break;
                case 106:
                    str = "j_low";
                    break;
                case 74:
                    str = "J_up";
                    break;
                case 107:
                    str = "k_low";
                    break;
                case 75:
                    str = "K_up";
                    break;
                case 108:
                    str = "l_low";
                    break;
                case 76:
                    str = "L_up";
                    break;
                case 109:
                    str = "m_low";
                    break;
                case 77:
                    str = "M_up";
                    break;
                case 110:
                    str = "n_low";
                    break;
                case 78:
                    str = "N_up";
                    break;
                case 0:
                    str = "Null";
                    break;
                case 111:
                    str = "o_low";
                    break;
                case 79:
                    str = "O_up";
                    break;
                case 112:
                    str = "p_low";
                    break;
                case 80:
                    str = "P_up";
                    break;
                case 1048581:
                    str = "PageDown";
                    break;
                case 1048580:
                    str = "PageUp";
                    break;
                case 1048587:
                    str = "PrintScreen";
                    break;
                case 113:
                    str = "q_low";
                    break;
                case 81:
                    str = "Q_up";
                    break;
                case 114:
                    str = "r_low";
                    break;
                case 82:
                    str = "R_up";
                    break;
                case 115:
                    str = "s_low";
                    break;
                case 83:
                    str = "S_up";
                    break;
                case 268435456:
                    str = "ShiftMask";
                    break;
                case 32:
                    str = "Space";
                    break;
                case 4293918720:
                    str = "SpecialMask";
                    break;
                case 116:
                    str = "t_low";
                    break;
                case 84:
                    str = "T_up";
                    break;
                case 9:
                    str = "Tab";
                    break;
                case 117:
                    str = "u_low";
                    break;
                case 85:
                    str = "U_up";
                    break;
                case 1048612:
                    str = "Unknown";
                    break;
                case 118:
                    str = "v_low";
                    break;
                case 86:
                    str = "V_up";
                    break;
                case 119:
                    str = "w_low";
                    break;
                case 87:
                    str = "W_up";
                    break;
                case 120:
                    str = "x_low";
                    break;
                case 88:
                    str = "X_up";
                    break;
                case 121:
                    str = "y_low";
                    break;
                case 89:
                    str = "Y_up";
                    break;
                case 122:
                    str = "z_low";
                    break;
                case 90:
                    str = "Z_up";
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
                case 97:
                    str = "a_low";
                    break;
                case 65:
                    str = "A_up";
                    break;
                case 2147483648:
                    str = "AltMask";
                    break;
                case 98:
                    str = "b_low";
                    break;
                case 66:
                    str = "B_up";
                    break;
                case 8:
                    str = "Backspace";
                    break;
                case 1048586:
                    str = "BackTab";
                    break;
                case 99:
                    str = "c_low";
                    break;
                case 67:
                    str = "C_up";
                    break;
                case 1048575:
                    str = "CharMask";
                    break;
                case 12:
                    str = "Clear";
                    break;
                case 1073741824:
                    str = "CtrlMask";
                    break;
                case 1048577:
                    str = "CursorDown";
                    break;
                case 1048578:
                    str = "CursorLeft";
                    break;
                case 1048579:
                    str = "CursorRight";
                    break;
                case 1048576:
                    str = "CursorUp";
                    break;
                case 100:
                    str = "d_low";
                    break;
                case 68:
                    str = "D_up";
                    break;
                case 48:
                    str = "D0";
                    break;
                case 49:
                    str = "D1";
                    break;
                case 50:
                    str = "D2";
                    break;
                case 51:
                    str = "D3";
                    break;
                case 52:
                    str = "D4";
                    break;
                case 53:
                    str = "D5";
                    break;
                case 54:
                    str = "D6";
                    break;
                case 55:
                    str = "D7";
                    break;
                case 56:
                    str = "D8";
                    break;
                case 57:
                    str = "D9";
                    break;
                case 127:
                    str = "Delete";
                    break;
                case 1048585:
                    str = "DeleteChar";
                    break;
                case 101:
                    str = "e_low";
                    break;
                case 69:
                    str = "E_up";
                    break;
                case 1048583:
                    str = "End";
                    break;
                case 10:
                    str = "Enter";
                    break;
                case 27:
                    str = "Esc";
                    break;
                case 102:
                    str = "f_low";
                    break;
                case 70:
                    str = "F_up";
                    break;
                case 1048588:
                    str = "F1";
                    break;
                case 1048597:
                    str = "F10";
                    break;
                case 1048598:
                    str = "F11";
                    break;
                case 1048599:
                    str = "F12";
                    break;
                case 1048589:
                    str = "F2";
                    break;
                case 1048590:
                    str = "F3";
                    break;
                case 1048591:
                    str = "F4";
                    break;
                case 1048592:
                    str = "F5";
                    break;
                case 1048593:
                    str = "F6";
                    break;
                case 1048594:
                    str = "F7";
                    break;
                case 1048595:
                    str = "F8";
                    break;
                case 1048596:
                    str = "F9";
                    break;
                case 103:
                    str = "g_low";
                    break;
                case 71:
                    str = "G_up";
                    break;
                case 104:
                    str = "h_low";
                    break;
                case 72:
                    str = "H_up";
                    break;
                case 1048582:
                    str = "Home";
                    break;
                case 105:
                    str = "i_low";
                    break;
                case 73:
                    str = "I_up";
                    break;
                case 45:
                    str = "InsertChar";
                    break;
                case 106:
                    str = "j_low";
                    break;
                case 74:
                    str = "J_up";
                    break;
                case 107:
                    str = "k_low";
                    break;
                case 75:
                    str = "K_up";
                    break;
                case 108:
                    str = "l_low";
                    break;
                case 76:
                    str = "L_up";
                    break;
                case 109:
                    str = "m_low";
                    break;
                case 77:
                    str = "M_up";
                    break;
                case 110:
                    str = "n_low";
                    break;
                case 78:
                    str = "N_up";
                    break;
                case 0:
                    str = "Null";
                    break;
                case 111:
                    str = "o_low";
                    break;
                case 79:
                    str = "O_up";
                    break;
                case 112:
                    str = "p_low";
                    break;
                case 80:
                    str = "P_up";
                    break;
                case 1048581:
                    str = "PageDown";
                    break;
                case 1048580:
                    str = "PageUp";
                    break;
                case 1048587:
                    str = "PrintScreen";
                    break;
                case 113:
                    str = "q_low";
                    break;
                case 81:
                    str = "Q_up";
                    break;
                case 114:
                    str = "r_low";
                    break;
                case 82:
                    str = "R_up";
                    break;
                case 115:
                    str = "s_low";
                    break;
                case 83:
                    str = "S_up";
                    break;
                case 268435456:
                    str = "ShiftMask";
                    break;
                case 32:
                    str = "Space";
                    break;
                case 4293918720:
                    str = "SpecialMask";
                    break;
                case 116:
                    str = "t_low";
                    break;
                case 84:
                    str = "T_up";
                    break;
                case 9:
                    str = "Tab";
                    break;
                case 117:
                    str = "u_low";
                    break;
                case 85:
                    str = "U_up";
                    break;
                case 1048612:
                    str = "Unknown";
                    break;
                case 118:
                    str = "v_low";
                    break;
                case 86:
                    str = "V_up";
                    break;
                case 119:
                    str = "w_low";
                    break;
                case 87:
                    str = "W_up";
                    break;
                case 120:
                    str = "x_low";
                    break;
                case 88:
                    str = "X_up";
                    break;
                case 121:
                    str = "y_low";
                    break;
                case 89:
                    str = "Y_up";
                    break;
                case 122:
                    str = "z_low";
                    break;
                case 90:
                    str = "Z_up";
                    break;
            }
            return str;
        }
    }
}
