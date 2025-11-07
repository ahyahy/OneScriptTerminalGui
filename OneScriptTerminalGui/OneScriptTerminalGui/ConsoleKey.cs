using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ostgui
{
    [ContextClass("ТфКлавишиКонсоли", "TfConsoleKey")]
    public class TfConsoleKey : AutoContext<TfConsoleKey>, ICollectionContext, IEnumerable<IValue>
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

        public TfConsoleKey()
        {
            _list = new List<decimal>
            {
                A,
                Add,
                Applications,
                Attention,
                B,
                Backspace,
                BrowserBack,
                BrowserFavorites,
                BrowserForward,
                BrowserHome,
                BrowserRefresh,
                BrowserSearch,
                BrowserStop,
                C,
                Clear,
                CrSel,
                D,
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
                Decimal,
                Delete,
                Divide,
                DownArrow,
                E,
                End,
                Enter,
                EraseEndOfFile,
                Escape,
                Execute,
                ExSel,
                F,
                F1,
                F10,
                F11,
                F12,
                F13,
                F14,
                F15,
                F16,
                F17,
                F18,
                F19,
                F2,
                F20,
                F21,
                F22,
                F23,
                F24,
                F3,
                F4,
                F5,
                F6,
                F7,
                F8,
                F9,
                G,
                H,
                Help,
                Home,
                I,
                Insert,
                J,
                K,
                L,
                LaunchApp1,
                LaunchApp2,
                LaunchMail,
                LaunchMediaSelect,
                LeftArrow,
                LeftWindows,
                M,
                MediaNext,
                MediaPlay,
                MediaPrevious,
                MediaStop,
                Multiply,
                N,
                NoName,
                NumPad0,
                NumPad1,
                NumPad2,
                NumPad3,
                NumPad4,
                NumPad5,
                NumPad6,
                NumPad7,
                NumPad8,
                NumPad9,
                O,
                Oem1,
                Oem102,
                Oem2,
                Oem3,
                Oem4,
                Oem5,
                Oem6,
                Oem7,
                Oem8,
                OemClear,
                OemComma,
                OemMinus,
                OemPeriod,
                OemPlus,
                P,
                Pa1,
                Packet,
                PageDown,
                PageUp,
                Pause,
                Play,
                Print,
                PrintScreen,
                Process,
                Q,
                R,
                RightArrow,
                RightWindows,
                S,
                Select,
                Separator,
                Sleep,
                Spacebar,
                Subtract,
                T,
                Tab,
                U,
                UpArrow,
                V,
                VolumeDown,
                VolumeMute,
                VolumeUp,
                W,
                X,
                Y,
                Z,
                Zoom,
            }.Select(ValueFactory.Create).ToList();
        }

        private static readonly Dictionary<decimal, string> namesRu = new Dictionary<decimal, string>
        {
            {65, "A"},
            {107, "Add"},
            {93, "Applications"},
            {246, "Attention"},
            {66, "B"},
            {8, "Backspace"},
            {166, "BrowserBack"},
            {171, "BrowserFavorites"},
            {167, "BrowserForward"},
            {172, "BrowserHome"},
            {168, "BrowserRefresh"},
            {170, "BrowserSearch"},
            {169, "BrowserStop"},
            {67, "C"},
            {12, "Clear"},
            {247, "CrSel"},
            {68, "D"},
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
            {110, "Decimal"},
            {46, "Delete"},
            {111, "Divide"},
            {40, "DownArrow"},
            {69, "E"},
            {35, "End"},
            {13, "Enter"},
            {249, "EraseEndOfFile"},
            {27, "Escape"},
            {43, "Execute"},
            {248, "ExSel"},
            {70, "F"},
            {112, "F1"},
            {121, "F10"},
            {122, "F11"},
            {123, "F12"},
            {124, "F13"},
            {125, "F14"},
            {126, "F15"},
            {127, "F16"},
            {128, "F17"},
            {129, "F18"},
            {130, "F19"},
            {113, "F2"},
            {131, "F20"},
            {132, "F21"},
            {133, "F22"},
            {134, "F23"},
            {135, "F24"},
            {114, "F3"},
            {115, "F4"},
            {116, "F5"},
            {117, "F6"},
            {118, "F7"},
            {119, "F8"},
            {120, "F9"},
            {71, "G"},
            {72, "H"},
            {47, "Help"},
            {36, "Home"},
            {73, "I"},
            {45, "Insert"},
            {74, "J"},
            {75, "K"},
            {76, "L"},
            {182, "LaunchApp1"},
            {183, "LaunchApp2"},
            {180, "LaunchMail"},
            {181, "LaunchMediaSelect"},
            {37, "LeftArrow"},
            {91, "LeftWindows"},
            {77, "M"},
            {176, "MediaNext"},
            {179, "MediaPlay"},
            {177, "MediaPrevious"},
            {178, "MediaStop"},
            {106, "Multiply"},
            {78, "N"},
            {252, "NoName"},
            {96, "NumPad0"},
            {97, "NumPad1"},
            {98, "NumPad2"},
            {99, "NumPad3"},
            {100, "NumPad4"},
            {101, "NumPad5"},
            {102, "NumPad6"},
            {103, "NumPad7"},
            {104, "NumPad8"},
            {105, "NumPad9"},
            {79, "O"},
            {186, "Oem1"},
            {226, "Oem102"},
            {191, "Oem2"},
            {192, "Oem3"},
            {219, "Oem4"},
            {220, "Oem5"},
            {221, "Oem6"},
            {222, "Oem7"},
            {223, "Oem8"},
            {254, "OemClear"},
            {188, "OemComma"},
            {189, "OemMinus"},
            {190, "OemPeriod"},
            {187, "OemPlus"},
            {80, "P"},
            {253, "Pa1"},
            {231, "Packet"},
            {34, "PageDown"},
            {33, "PageUp"},
            {19, "Pause"},
            {250, "Play"},
            {42, "Print"},
            {44, "PrintScreen"},
            {229, "Process"},
            {81, "Q"},
            {82, "R"},
            {39, "RightArrow"},
            {92, "RightWindows"},
            {83, "S"},
            {41, "Select"},
            {108, "Separator"},
            {95, "Sleep"},
            {32, "Spacebar"},
            {109, "Subtract"},
            {84, "T"},
            {9, "Tab"},
            {85, "U"},
            {38, "UpArrow"},
            {86, "V"},
            {174, "VolumeDown"},
            {173, "VolumeMute"},
            {175, "VolumeUp"},
            {87, "W"},
            {88, "X"},
            {89, "Y"},
            {90, "Z"},
            {251, "Zoom"},
        };

        private static readonly Dictionary<decimal, string> namesEn = new Dictionary<decimal, string>
        {
            {65, "A"},
            {107, "Add"},
            {93, "Applications"},
            {246, "Attention"},
            {66, "B"},
            {8, "Backspace"},
            {166, "BrowserBack"},
            {171, "BrowserFavorites"},
            {167, "BrowserForward"},
            {172, "BrowserHome"},
            {168, "BrowserRefresh"},
            {170, "BrowserSearch"},
            {169, "BrowserStop"},
            {67, "C"},
            {12, "Clear"},
            {247, "CrSel"},
            {68, "D"},
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
            {110, "Decimal"},
            {46, "Delete"},
            {111, "Divide"},
            {40, "DownArrow"},
            {69, "E"},
            {35, "End"},
            {13, "Enter"},
            {249, "EraseEndOfFile"},
            {27, "Escape"},
            {43, "Execute"},
            {248, "ExSel"},
            {70, "F"},
            {112, "F1"},
            {121, "F10"},
            {122, "F11"},
            {123, "F12"},
            {124, "F13"},
            {125, "F14"},
            {126, "F15"},
            {127, "F16"},
            {128, "F17"},
            {129, "F18"},
            {130, "F19"},
            {113, "F2"},
            {131, "F20"},
            {132, "F21"},
            {133, "F22"},
            {134, "F23"},
            {135, "F24"},
            {114, "F3"},
            {115, "F4"},
            {116, "F5"},
            {117, "F6"},
            {118, "F7"},
            {119, "F8"},
            {120, "F9"},
            {71, "G"},
            {72, "H"},
            {47, "Help"},
            {36, "Home"},
            {73, "I"},
            {45, "Insert"},
            {74, "J"},
            {75, "K"},
            {76, "L"},
            {182, "LaunchApp1"},
            {183, "LaunchApp2"},
            {180, "LaunchMail"},
            {181, "LaunchMediaSelect"},
            {37, "LeftArrow"},
            {91, "LeftWindows"},
            {77, "M"},
            {176, "MediaNext"},
            {179, "MediaPlay"},
            {177, "MediaPrevious"},
            {178, "MediaStop"},
            {106, "Multiply"},
            {78, "N"},
            {252, "NoName"},
            {96, "NumPad0"},
            {97, "NumPad1"},
            {98, "NumPad2"},
            {99, "NumPad3"},
            {100, "NumPad4"},
            {101, "NumPad5"},
            {102, "NumPad6"},
            {103, "NumPad7"},
            {104, "NumPad8"},
            {105, "NumPad9"},
            {79, "O"},
            {186, "Oem1"},
            {226, "Oem102"},
            {191, "Oem2"},
            {192, "Oem3"},
            {219, "Oem4"},
            {220, "Oem5"},
            {221, "Oem6"},
            {222, "Oem7"},
            {223, "Oem8"},
            {254, "OemClear"},
            {188, "OemComma"},
            {189, "OemMinus"},
            {190, "OemPeriod"},
            {187, "OemPlus"},
            {80, "P"},
            {253, "Pa1"},
            {231, "Packet"},
            {34, "PageDown"},
            {33, "PageUp"},
            {19, "Pause"},
            {250, "Play"},
            {42, "Print"},
            {44, "PrintScreen"},
            {229, "Process"},
            {81, "Q"},
            {82, "R"},
            {39, "RightArrow"},
            {92, "RightWindows"},
            {83, "S"},
            {41, "Select"},
            {108, "Separator"},
            {95, "Sleep"},
            {32, "Spacebar"},
            {109, "Subtract"},
            {84, "T"},
            {9, "Tab"},
            {85, "U"},
            {38, "UpArrow"},
            {86, "V"},
            {174, "VolumeDown"},
            {173, "VolumeMute"},
            {175, "VolumeUp"},
            {87, "W"},
            {88, "X"},
            {89, "Y"},
            {90, "Z"},
            {251, "Zoom"},
        };

        [ContextProperty("A", "A")]
        public decimal A => 65;

        [ContextProperty("Add", "Add")]
        public decimal Add => 107;

        [ContextProperty("Applications", "Applications")]
        public decimal Applications => 93;

        [ContextProperty("Attention", "Attention")]
        public decimal Attention => 246;

        [ContextProperty("B", "B")]
        public decimal B => 66;

        [ContextProperty("Backspace", "Backspace")]
        public decimal Backspace => 8;

        [ContextProperty("BrowserBack", "BrowserBack")]
        public decimal BrowserBack => 166;

        [ContextProperty("BrowserFavorites", "BrowserFavorites")]
        public decimal BrowserFavorites => 171;

        [ContextProperty("BrowserForward", "BrowserForward")]
        public decimal BrowserForward => 167;

        [ContextProperty("BrowserHome", "BrowserHome")]
        public decimal BrowserHome => 172;

        [ContextProperty("BrowserRefresh", "BrowserRefresh")]
        public decimal BrowserRefresh => 168;

        [ContextProperty("BrowserSearch", "BrowserSearch")]
        public decimal BrowserSearch => 170;

        [ContextProperty("BrowserStop", "BrowserStop")]
        public decimal BrowserStop => 169;

        [ContextProperty("C", "C")]
        public decimal C => 67;

        [ContextProperty("Clear", "Clear")]
        public decimal Clear => 12;

        [ContextProperty("CrSel", "CrSel")]
        public decimal CrSel => 247;

        [ContextProperty("D", "D")]
        public decimal D => 68;

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

        [ContextProperty("Decimal", "Decimal")]
        public decimal Decimal => 110;

        [ContextProperty("Delete", "Delete")]
        public decimal Delete => 46;

        [ContextProperty("Divide", "Divide")]
        public decimal Divide => 111;

        [ContextProperty("DownArrow", "DownArrow")]
        public decimal DownArrow => 40;

        [ContextProperty("E", "E")]
        public decimal E => 69;

        [ContextProperty("End", "End")]
        public decimal End => 35;

        [ContextProperty("Enter", "Enter")]
        public decimal Enter => 13;

        [ContextProperty("EraseEndOfFile", "EraseEndOfFile")]
        public decimal EraseEndOfFile => 249;

        [ContextProperty("Escape", "Escape")]
        public decimal Escape => 27;

        [ContextProperty("Execute", "Execute")]
        public decimal Execute => 43;

        [ContextProperty("ExSel", "ExSel")]
        public decimal ExSel => 248;

        [ContextProperty("F", "F")]
        public decimal F => 70;

        [ContextProperty("F1", "F1")]
        public decimal F1 => 112;

        [ContextProperty("F10", "F10")]
        public decimal F10 => 121;

        [ContextProperty("F11", "F11")]
        public decimal F11 => 122;

        [ContextProperty("F12", "F12")]
        public decimal F12 => 123;

        [ContextProperty("F13", "F13")]
        public decimal F13 => 124;

        [ContextProperty("F14", "F14")]
        public decimal F14 => 125;

        [ContextProperty("F15", "F15")]
        public decimal F15 => 126;

        [ContextProperty("F16", "F16")]
        public decimal F16 => 127;

        [ContextProperty("F17", "F17")]
        public decimal F17 => 128;

        [ContextProperty("F18", "F18")]
        public decimal F18 => 129;

        [ContextProperty("F19", "F19")]
        public decimal F19 => 130;

        [ContextProperty("F2", "F2")]
        public decimal F2 => 113;

        [ContextProperty("F20", "F20")]
        public decimal F20 => 131;

        [ContextProperty("F21", "F21")]
        public decimal F21 => 132;

        [ContextProperty("F22", "F22")]
        public decimal F22 => 133;

        [ContextProperty("F23", "F23")]
        public decimal F23 => 134;

        [ContextProperty("F24", "F24")]
        public decimal F24 => 135;

        [ContextProperty("F3", "F3")]
        public decimal F3 => 114;

        [ContextProperty("F4", "F4")]
        public decimal F4 => 115;

        [ContextProperty("F5", "F5")]
        public decimal F5 => 116;

        [ContextProperty("F6", "F6")]
        public decimal F6 => 117;

        [ContextProperty("F7", "F7")]
        public decimal F7 => 118;

        [ContextProperty("F8", "F8")]
        public decimal F8 => 119;

        [ContextProperty("F9", "F9")]
        public decimal F9 => 120;

        [ContextProperty("G", "G")]
        public decimal G => 71;

        [ContextProperty("H", "H")]
        public decimal H => 72;

        [ContextProperty("Help", "Help")]
        public decimal Help => 47;

        [ContextProperty("Home", "Home")]
        public decimal Home => 36;

        [ContextProperty("I", "I")]
        public decimal I => 73;

        [ContextProperty("Insert", "Insert")]
        public decimal Insert => 45;

        [ContextProperty("J", "J")]
        public decimal J => 74;

        [ContextProperty("K", "K")]
        public decimal K => 75;

        [ContextProperty("L", "L")]
        public decimal L => 76;

        [ContextProperty("LaunchApp1", "LaunchApp1")]
        public decimal LaunchApp1 => 182;

        [ContextProperty("LaunchApp2", "LaunchApp2")]
        public decimal LaunchApp2 => 183;

        [ContextProperty("LaunchMail", "LaunchMail")]
        public decimal LaunchMail => 180;

        [ContextProperty("LaunchMediaSelect", "LaunchMediaSelect")]
        public decimal LaunchMediaSelect => 181;

        [ContextProperty("LeftArrow", "LeftArrow")]
        public decimal LeftArrow => 37;

        [ContextProperty("LeftWindows", "LeftWindows")]
        public decimal LeftWindows => 91;

        [ContextProperty("M", "M")]
        public decimal M => 77;

        [ContextProperty("MediaNext", "MediaNext")]
        public decimal MediaNext => 176;

        [ContextProperty("MediaPlay", "MediaPlay")]
        public decimal MediaPlay => 179;

        [ContextProperty("MediaPrevious", "MediaPrevious")]
        public decimal MediaPrevious => 177;

        [ContextProperty("MediaStop", "MediaStop")]
        public decimal MediaStop => 178;

        [ContextProperty("Multiply", "Multiply")]
        public decimal Multiply => 106;

        [ContextProperty("N", "N")]
        public decimal N => 78;

        [ContextProperty("NoName", "NoName")]
        public decimal NoName => 252;

        [ContextProperty("NumPad0", "NumPad0")]
        public decimal NumPad0 => 96;

        [ContextProperty("NumPad1", "NumPad1")]
        public decimal NumPad1 => 97;

        [ContextProperty("NumPad2", "NumPad2")]
        public decimal NumPad2 => 98;

        [ContextProperty("NumPad3", "NumPad3")]
        public decimal NumPad3 => 99;

        [ContextProperty("NumPad4", "NumPad4")]
        public decimal NumPad4 => 100;

        [ContextProperty("NumPad5", "NumPad5")]
        public decimal NumPad5 => 101;

        [ContextProperty("NumPad6", "NumPad6")]
        public decimal NumPad6 => 102;

        [ContextProperty("NumPad7", "NumPad7")]
        public decimal NumPad7 => 103;

        [ContextProperty("NumPad8", "NumPad8")]
        public decimal NumPad8 => 104;

        [ContextProperty("NumPad9", "NumPad9")]
        public decimal NumPad9 => 105;

        [ContextProperty("O", "O")]
        public decimal O => 79;

        [ContextProperty("Oem1", "Oem1")]
        public decimal Oem1 => 186;

        [ContextProperty("Oem102", "Oem102")]
        public decimal Oem102 => 226;

        [ContextProperty("Oem2", "Oem2")]
        public decimal Oem2 => 191;

        [ContextProperty("Oem3", "Oem3")]
        public decimal Oem3 => 192;

        [ContextProperty("Oem4", "Oem4")]
        public decimal Oem4 => 219;

        [ContextProperty("Oem5", "Oem5")]
        public decimal Oem5 => 220;

        [ContextProperty("Oem6", "Oem6")]
        public decimal Oem6 => 221;

        [ContextProperty("Oem7", "Oem7")]
        public decimal Oem7 => 222;

        [ContextProperty("Oem8", "Oem8")]
        public decimal Oem8 => 223;

        [ContextProperty("OemClear", "OemClear")]
        public decimal OemClear => 254;

        [ContextProperty("OemComma", "OemComma")]
        public decimal OemComma => 188;

        [ContextProperty("OemMinus", "OemMinus")]
        public decimal OemMinus => 189;

        [ContextProperty("OemPeriod", "OemPeriod")]
        public decimal OemPeriod => 190;

        [ContextProperty("OemPlus", "OemPlus")]
        public decimal OemPlus => 187;

        [ContextProperty("P", "P")]
        public decimal P => 80;

        [ContextProperty("Pa1", "Pa1")]
        public decimal Pa1 => 253;

        [ContextProperty("Packet", "Packet")]
        public decimal Packet => 231;

        [ContextProperty("PageDown", "PageDown")]
        public decimal PageDown => 34;

        [ContextProperty("PageUp", "PageUp")]
        public decimal PageUp => 33;

        [ContextProperty("Pause", "Pause")]
        public decimal Pause => 19;

        [ContextProperty("Play", "Play")]
        public decimal Play => 250;

        [ContextProperty("Print", "Print")]
        public decimal Print => 42;

        [ContextProperty("PrintScreen", "PrintScreen")]
        public decimal PrintScreen => 44;

        [ContextProperty("Process", "Process")]
        public decimal Process => 229;

        [ContextProperty("Q", "Q")]
        public decimal Q => 81;

        [ContextProperty("R", "R")]
        public decimal R => 82;

        [ContextProperty("RightArrow", "RightArrow")]
        public decimal RightArrow => 39;

        [ContextProperty("RightWindows", "RightWindows")]
        public decimal RightWindows => 92;

        [ContextProperty("S", "S")]
        public decimal S => 83;

        [ContextProperty("Select", "Select")]
        public decimal Select => 41;

        [ContextProperty("Separator", "Separator")]
        public decimal Separator => 108;

        [ContextProperty("Sleep", "Sleep")]
        public decimal Sleep => 95;

        [ContextProperty("Spacebar", "Spacebar")]
        public decimal Spacebar => 32;

        [ContextProperty("Subtract", "Subtract")]
        public decimal Subtract => 109;

        [ContextProperty("T", "T")]
        public decimal T => 84;

        [ContextProperty("Tab", "Tab")]
        public decimal Tab => 9;

        [ContextProperty("U", "U")]
        public decimal U => 85;

        [ContextProperty("UpArrow", "UpArrow")]
        public decimal UpArrow => 38;

        [ContextProperty("V", "V")]
        public decimal V => 86;

        [ContextProperty("VolumeDown", "VolumeDown")]
        public decimal VolumeDown => 174;

        [ContextProperty("VolumeMute", "VolumeMute")]
        public decimal VolumeMute => 173;

        [ContextProperty("VolumeUp", "VolumeUp")]
        public decimal VolumeUp => 175;

        [ContextProperty("W", "W")]
        public decimal W => 87;

        [ContextProperty("X", "X")]
        public decimal X => 88;

        [ContextProperty("Y", "Y")]
        public decimal Y => 89;

        [ContextProperty("Z", "Z")]
        public decimal Z => 90;

        [ContextProperty("Zoom", "Zoom")]
        public decimal Zoom => 251;
    }
}
