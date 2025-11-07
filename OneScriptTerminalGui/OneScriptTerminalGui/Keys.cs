using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

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

        [ContextProperty("Количество", "Count")]
        public int CountProp
        {
            get { return _list.Count; }
        }

        [ContextMethod("Получить", "Get")]
        public IValue Get(int index)
        {
            return _list[index];
        }

        [ContextMethod("Имя")]
        public string NameRu(decimal p1)
        {
            return namesRu.TryGetValue(p1, out string name) ? name : p1.ToString();
        }

        [ContextMethod("Name")]
        public string NameEn(decimal p1)
        {
            return namesEn.TryGetValue(p1, out string name) ? name : p1.ToString();
        }

        public TfKeys()
        {
            _list = new List<decimal>
            {
                a_low,
                A_up,
                AltMask,
                b_low,
                B_up,
                Backspace,
                BackTab,
                c_low,
                C_up,
                CharMask,
                Clear,
                CtrlMask,
                CursorDown,
                CursorLeft,
                CursorRight,
                CursorUp,
                d_low,
                D_up,
                D0,
                D1,
                D2,
                D3,
                D4,
                D5,
                D6,
                D7,
                D8,
                D9,
                Delete,
                DeleteChar,
                e_low,
                E_up,
                End,
                Enter,
                Esc,
                f_low,
                F_up,
                F1,
                F10,
                F11,
                F12,
                F2,
                F3,
                F4,
                F5,
                F6,
                F7,
                F8,
                F9,
                g_low,
                G_up,
                h_low,
                H_up,
                Home,
                i_low,
                I_up,
                InsertChar,
                j_low,
                J_up,
                k_low,
                K_up,
                l_low,
                L_up,
                m_low,
                M_up,
                n_low,
                N_up,
                Null,
                o_low,
                O_up,
                p_low,
                P_up,
                PageDown,
                PageUp,
                PrintScreen,
                q_low,
                Q_up,
                r_low,
                R_up,
                s_low,
                S_up,
                ShiftMask,
                Space,
                SpecialMask,
                t_low,
                T_up,
                Tab,
                u_low,
                U_up,
                Unknown,
                v_low,
                V_up,
                w_low,
                W_up,
                x_low,
                X_up,
                y_low,
                Y_up,
                z_low,
                Z_up,
            }.Select(ValueFactory.Create).ToList();
        }

        private static readonly Dictionary<decimal, string> namesRu = new Dictionary<decimal, string>
        {
            {97, "a_low"},
            {65, "A_up"},
            {2147483648, "AltMask"},
            {98, "b_low"},
            {66, "B_up"},
            {8, "Backspace"},
            {1048586, "BackTab"},
            {99, "c_low"},
            {67, "C_up"},
            {1048575, "CharMask"},
            {12, "Clear"},
            {1073741824, "CtrlMask"},
            {1048577, "CursorDown"},
            {1048578, "CursorLeft"},
            {1048579, "CursorRight"},
            {1048576, "CursorUp"},
            {100, "d_low"},
            {68, "D_up"},
            {48, "D0"},
            {49, "D1"},
            {50, "D2"},
            {51, "D3"},
            {52, "D4"},
            {53, "D5"},
            {54, "D6"},
            {55, "D7"},
            {56, "D8"},
            {57, "D9"},
            {127, "Delete"},
            {1048585, "DeleteChar"},
            {101, "e_low"},
            {69, "E_up"},
            {1048583, "End"},
            {10, "Enter"},
            {27, "Esc"},
            {102, "f_low"},
            {70, "F_up"},
            {1048588, "F1"},
            {1048597, "F10"},
            {1048598, "F11"},
            {1048599, "F12"},
            {1048589, "F2"},
            {1048590, "F3"},
            {1048591, "F4"},
            {1048592, "F5"},
            {1048593, "F6"},
            {1048594, "F7"},
            {1048595, "F8"},
            {1048596, "F9"},
            {103, "g_low"},
            {71, "G_up"},
            {104, "h_low"},
            {72, "H_up"},
            {1048582, "Home"},
            {105, "i_low"},
            {73, "I_up"},
            {45, "InsertChar"},
            {106, "j_low"},
            {74, "J_up"},
            {107, "k_low"},
            {75, "K_up"},
            {108, "l_low"},
            {76, "L_up"},
            {109, "m_low"},
            {77, "M_up"},
            {110, "n_low"},
            {78, "N_up"},
            {0, "Null"},
            {111, "o_low"},
            {79, "O_up"},
            {112, "p_low"},
            {80, "P_up"},
            {1048581, "PageDown"},
            {1048580, "PageUp"},
            {1048587, "PrintScreen"},
            {113, "q_low"},
            {81, "Q_up"},
            {114, "r_low"},
            {82, "R_up"},
            {115, "s_low"},
            {83, "S_up"},
            {268435456, "ShiftMask"},
            {32, "Space"},
            {4293918720, "SpecialMask"},
            {116, "t_low"},
            {84, "T_up"},
            {9, "Tab"},
            {117, "u_low"},
            {85, "U_up"},
            {1048612, "Unknown"},
            {118, "v_low"},
            {86, "V_up"},
            {119, "w_low"},
            {87, "W_up"},
            {120, "x_low"},
            {88, "X_up"},
            {121, "y_low"},
            {89, "Y_up"},
            {122, "z_low"},
            {90, "Z_up"},
        };

        private static readonly Dictionary<decimal, string> namesEn = new Dictionary<decimal, string>
        {
            {97, "a_low"},
            {65, "A_up"},
            {2147483648, "AltMask"},
            {98, "b_low"},
            {66, "B_up"},
            {8, "Backspace"},
            {1048586, "BackTab"},
            {99, "c_low"},
            {67, "C_up"},
            {1048575, "CharMask"},
            {12, "Clear"},
            {1073741824, "CtrlMask"},
            {1048577, "CursorDown"},
            {1048578, "CursorLeft"},
            {1048579, "CursorRight"},
            {1048576, "CursorUp"},
            {100, "d_low"},
            {68, "D_up"},
            {48, "D0"},
            {49, "D1"},
            {50, "D2"},
            {51, "D3"},
            {52, "D4"},
            {53, "D5"},
            {54, "D6"},
            {55, "D7"},
            {56, "D8"},
            {57, "D9"},
            {127, "Delete"},
            {1048585, "DeleteChar"},
            {101, "e_low"},
            {69, "E_up"},
            {1048583, "End"},
            {10, "Enter"},
            {27, "Esc"},
            {102, "f_low"},
            {70, "F_up"},
            {1048588, "F1"},
            {1048597, "F10"},
            {1048598, "F11"},
            {1048599, "F12"},
            {1048589, "F2"},
            {1048590, "F3"},
            {1048591, "F4"},
            {1048592, "F5"},
            {1048593, "F6"},
            {1048594, "F7"},
            {1048595, "F8"},
            {1048596, "F9"},
            {103, "g_low"},
            {71, "G_up"},
            {104, "h_low"},
            {72, "H_up"},
            {1048582, "Home"},
            {105, "i_low"},
            {73, "I_up"},
            {45, "InsertChar"},
            {106, "j_low"},
            {74, "J_up"},
            {107, "k_low"},
            {75, "K_up"},
            {108, "l_low"},
            {76, "L_up"},
            {109, "m_low"},
            {77, "M_up"},
            {110, "n_low"},
            {78, "N_up"},
            {0, "Null"},
            {111, "o_low"},
            {79, "O_up"},
            {112, "p_low"},
            {80, "P_up"},
            {1048581, "PageDown"},
            {1048580, "PageUp"},
            {1048587, "PrintScreen"},
            {113, "q_low"},
            {81, "Q_up"},
            {114, "r_low"},
            {82, "R_up"},
            {115, "s_low"},
            {83, "S_up"},
            {268435456, "ShiftMask"},
            {32, "Space"},
            {4293918720, "SpecialMask"},
            {116, "t_low"},
            {84, "T_up"},
            {9, "Tab"},
            {117, "u_low"},
            {85, "U_up"},
            {1048612, "Unknown"},
            {118, "v_low"},
            {86, "V_up"},
            {119, "w_low"},
            {87, "W_up"},
            {120, "x_low"},
            {88, "X_up"},
            {121, "y_low"},
            {89, "Y_up"},
            {122, "z_low"},
            {90, "Z_up"},
        };

        [ContextProperty("a_low", "a_low")]
        public decimal a_low => 97;

        [ContextProperty("A_up", "A_up")]
        public decimal A_up => 65;

        [ContextProperty("AltMask", "AltMask")]
        public decimal AltMask => 2147483648;

        [ContextProperty("b_low", "b_low")]
        public decimal b_low => 98;

        [ContextProperty("B_up", "B_up")]
        public decimal B_up => 66;

        [ContextProperty("Backspace", "Backspace")]
        public decimal Backspace => 8;

        [ContextProperty("BackTab", "BackTab")]
        public decimal BackTab => 1048586;

        [ContextProperty("c_low", "c_low")]
        public decimal c_low => 99;

        [ContextProperty("C_up", "C_up")]
        public decimal C_up => 67;

        [ContextProperty("CharMask", "CharMask")]
        public decimal CharMask => 1048575;

        [ContextProperty("Clear", "Clear")]
        public decimal Clear => 12;

        [ContextProperty("CtrlMask", "CtrlMask")]
        public decimal CtrlMask => 1073741824;

        [ContextProperty("CursorDown", "CursorDown")]
        public decimal CursorDown => 1048577;

        [ContextProperty("CursorLeft", "CursorLeft")]
        public decimal CursorLeft => 1048578;

        [ContextProperty("CursorRight", "CursorRight")]
        public decimal CursorRight => 1048579;

        [ContextProperty("CursorUp", "CursorUp")]
        public decimal CursorUp => 1048576;

        [ContextProperty("d_low", "d_low")]
        public decimal d_low => 100;

        [ContextProperty("D_up", "D_up")]
        public decimal D_up => 68;

        [ContextProperty("D0", "D0")]
        public decimal D0 => 48;

        [ContextProperty("D1", "D1")]
        public decimal D1 => 49;

        [ContextProperty("D2", "D2")]
        public decimal D2 => 50;

        [ContextProperty("D3", "D3")]
        public decimal D3 => 51;

        [ContextProperty("D4", "D4")]
        public decimal D4 => 52;

        [ContextProperty("D5", "D5")]
        public decimal D5 => 53;

        [ContextProperty("D6", "D6")]
        public decimal D6 => 54;

        [ContextProperty("D7", "D7")]
        public decimal D7 => 55;

        [ContextProperty("D8", "D8")]
        public decimal D8 => 56;

        [ContextProperty("D9", "D9")]
        public decimal D9 => 57;

        [ContextProperty("Delete", "Delete")]
        public decimal Delete => 127;

        [ContextProperty("DeleteChar", "DeleteChar")]
        public decimal DeleteChar => 1048585;

        [ContextProperty("e_low", "e_low")]
        public decimal e_low => 101;

        [ContextProperty("E_up", "E_up")]
        public decimal E_up => 69;

        [ContextProperty("End", "End")]
        public decimal End => 1048583;

        [ContextProperty("Enter", "Enter")]
        public decimal Enter => 10;

        [ContextProperty("Esc", "Esc")]
        public decimal Esc => 27;

        [ContextProperty("f_low", "f_low")]
        public decimal f_low => 102;

        [ContextProperty("F_up", "F_up")]
        public decimal F_up => 70;

        [ContextProperty("F1", "F1")]
        public decimal F1 => 1048588;

        [ContextProperty("F10", "F10")]
        public decimal F10 => 1048597;

        [ContextProperty("F11", "F11")]
        public decimal F11 => 1048598;

        [ContextProperty("F12", "F12")]
        public decimal F12 => 1048599;

        [ContextProperty("F2", "F2")]
        public decimal F2 => 1048589;

        [ContextProperty("F3", "F3")]
        public decimal F3 => 1048590;

        [ContextProperty("F4", "F4")]
        public decimal F4 => 1048591;

        [ContextProperty("F5", "F5")]
        public decimal F5 => 1048592;

        [ContextProperty("F6", "F6")]
        public decimal F6 => 1048593;

        [ContextProperty("F7", "F7")]
        public decimal F7 => 1048594;

        [ContextProperty("F8", "F8")]
        public decimal F8 => 1048595;

        [ContextProperty("F9", "F9")]
        public decimal F9 => 1048596;

        [ContextProperty("g_low", "g_low")]
        public decimal g_low => 103;

        [ContextProperty("G_up", "G_up")]
        public decimal G_up => 71;

        [ContextProperty("h_low", "h_low")]
        public decimal h_low => 104;

        [ContextProperty("H_up", "H_up")]
        public decimal H_up => 72;

        [ContextProperty("Home", "Home")]
        public decimal Home => 1048582;

        [ContextProperty("i_low", "i_low")]
        public decimal i_low => 105;

        [ContextProperty("I_up", "I_up")]
        public decimal I_up => 73;

        [ContextProperty("InsertChar", "InsertChar")]
        public decimal InsertChar => 45;

        [ContextProperty("j_low", "j_low")]
        public decimal j_low => 106;

        [ContextProperty("J_up", "J_up")]
        public decimal J_up => 74;

        [ContextProperty("k_low", "k_low")]
        public decimal k_low => 107;

        [ContextProperty("K_up", "K_up")]
        public decimal K_up => 75;

        [ContextProperty("l_low", "l_low")]
        public decimal l_low => 108;

        [ContextProperty("L_up", "L_up")]
        public decimal L_up => 76;

        [ContextProperty("m_low", "m_low")]
        public decimal m_low => 109;

        [ContextProperty("M_up", "M_up")]
        public decimal M_up => 77;

        [ContextProperty("n_low", "n_low")]
        public decimal n_low => 110;

        [ContextProperty("N_up", "N_up")]
        public decimal N_up => 78;

        [ContextProperty("Null", "Null")]
        public decimal Null => 0;

        [ContextProperty("o_low", "o_low")]
        public decimal o_low => 111;

        [ContextProperty("O_up", "O_up")]
        public decimal O_up => 79;

        [ContextProperty("p_low", "p_low")]
        public decimal p_low => 112;

        [ContextProperty("P_up", "P_up")]
        public decimal P_up => 80;

        [ContextProperty("PageDown", "PageDown")]
        public decimal PageDown => 1048581;

        [ContextProperty("PageUp", "PageUp")]
        public decimal PageUp => 1048580;

        [ContextProperty("PrintScreen", "PrintScreen")]
        public decimal PrintScreen => 1048587;

        [ContextProperty("q_low", "q_low")]
        public decimal q_low => 113;

        [ContextProperty("Q_up", "Q_up")]
        public decimal Q_up => 81;

        [ContextProperty("r_low", "r_low")]
        public decimal r_low => 114;

        [ContextProperty("R_up", "R_up")]
        public decimal R_up => 82;

        [ContextProperty("s_low", "s_low")]
        public decimal s_low => 115;

        [ContextProperty("S_up", "S_up")]
        public decimal S_up => 83;

        [ContextProperty("ShiftMask", "ShiftMask")]
        public decimal ShiftMask => 268435456;

        [ContextProperty("Space", "Space")]
        public decimal Space => 32;

        [ContextProperty("SpecialMask", "SpecialMask")]
        public decimal SpecialMask => 4293918720;

        [ContextProperty("t_low", "t_low")]
        public decimal t_low => 116;

        [ContextProperty("T_up", "T_up")]
        public decimal T_up => 84;

        [ContextProperty("Tab", "Tab")]
        public decimal Tab => 9;

        [ContextProperty("u_low", "u_low")]
        public decimal u_low => 117;

        [ContextProperty("U_up", "U_up")]
        public decimal U_up => 85;

        [ContextProperty("Unknown", "Unknown")]
        public decimal Unknown => 1048612;

        [ContextProperty("v_low", "v_low")]
        public decimal v_low => 118;

        [ContextProperty("V_up", "V_up")]
        public decimal V_up => 86;

        [ContextProperty("w_low", "w_low")]
        public decimal w_low => 119;

        [ContextProperty("W_up", "W_up")]
        public decimal W_up => 87;

        [ContextProperty("x_low", "x_low")]
        public decimal x_low => 120;

        [ContextProperty("X_up", "X_up")]
        public decimal X_up => 88;

        [ContextProperty("y_low", "y_low")]
        public decimal y_low => 121;

        [ContextProperty("Y_up", "Y_up")]
        public decimal Y_up => 89;

        [ContextProperty("z_low", "z_low")]
        public decimal z_low => 122;

        [ContextProperty("Z_up", "Z_up")]
        public decimal Z_up => 90;
    }
}
