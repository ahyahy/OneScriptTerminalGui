using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;

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

        public TfConsoleKey()
        {
            _list = new List<IValue>();
            _list.Add(ValueFactory.Create(A));
            _list.Add(ValueFactory.Create(Add));
            _list.Add(ValueFactory.Create(Applications));
            _list.Add(ValueFactory.Create(Attention));
            _list.Add(ValueFactory.Create(B));
            _list.Add(ValueFactory.Create(Backspace));
            _list.Add(ValueFactory.Create(BrowserBack));
            _list.Add(ValueFactory.Create(BrowserFavorites));
            _list.Add(ValueFactory.Create(BrowserForward));
            _list.Add(ValueFactory.Create(BrowserHome));
            _list.Add(ValueFactory.Create(BrowserRefresh));
            _list.Add(ValueFactory.Create(BrowserSearch));
            _list.Add(ValueFactory.Create(BrowserStop));
            _list.Add(ValueFactory.Create(C));
            _list.Add(ValueFactory.Create(Clear));
            _list.Add(ValueFactory.Create(CrSel));
            _list.Add(ValueFactory.Create(D));
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
            _list.Add(ValueFactory.Create(Decimal));
            _list.Add(ValueFactory.Create(Delete));
            _list.Add(ValueFactory.Create(Divide));
            _list.Add(ValueFactory.Create(DownArrow));
            _list.Add(ValueFactory.Create(E));
            _list.Add(ValueFactory.Create(End));
            _list.Add(ValueFactory.Create(Enter));
            _list.Add(ValueFactory.Create(EraseEndOfFile));
            _list.Add(ValueFactory.Create(Escape));
            _list.Add(ValueFactory.Create(Execute));
            _list.Add(ValueFactory.Create(ExSel));
            _list.Add(ValueFactory.Create(F));
            _list.Add(ValueFactory.Create(F1));
            _list.Add(ValueFactory.Create(F10));
            _list.Add(ValueFactory.Create(F11));
            _list.Add(ValueFactory.Create(F12));
            _list.Add(ValueFactory.Create(F13));
            _list.Add(ValueFactory.Create(F14));
            _list.Add(ValueFactory.Create(F15));
            _list.Add(ValueFactory.Create(F16));
            _list.Add(ValueFactory.Create(F17));
            _list.Add(ValueFactory.Create(F18));
            _list.Add(ValueFactory.Create(F19));
            _list.Add(ValueFactory.Create(F2));
            _list.Add(ValueFactory.Create(F20));
            _list.Add(ValueFactory.Create(F21));
            _list.Add(ValueFactory.Create(F22));
            _list.Add(ValueFactory.Create(F23));
            _list.Add(ValueFactory.Create(F24));
            _list.Add(ValueFactory.Create(F3));
            _list.Add(ValueFactory.Create(F4));
            _list.Add(ValueFactory.Create(F5));
            _list.Add(ValueFactory.Create(F6));
            _list.Add(ValueFactory.Create(F7));
            _list.Add(ValueFactory.Create(F8));
            _list.Add(ValueFactory.Create(F9));
            _list.Add(ValueFactory.Create(G));
            _list.Add(ValueFactory.Create(H));
            _list.Add(ValueFactory.Create(Help));
            _list.Add(ValueFactory.Create(Home));
            _list.Add(ValueFactory.Create(I));
            _list.Add(ValueFactory.Create(Insert));
            _list.Add(ValueFactory.Create(J));
            _list.Add(ValueFactory.Create(K));
            _list.Add(ValueFactory.Create(L));
            _list.Add(ValueFactory.Create(LaunchApp1));
            _list.Add(ValueFactory.Create(LaunchApp2));
            _list.Add(ValueFactory.Create(LaunchMail));
            _list.Add(ValueFactory.Create(LaunchMediaSelect));
            _list.Add(ValueFactory.Create(LeftArrow));
            _list.Add(ValueFactory.Create(LeftWindows));
            _list.Add(ValueFactory.Create(M));
            _list.Add(ValueFactory.Create(MediaNext));
            _list.Add(ValueFactory.Create(MediaPlay));
            _list.Add(ValueFactory.Create(MediaPrevious));
            _list.Add(ValueFactory.Create(MediaStop));
            _list.Add(ValueFactory.Create(Multiply));
            _list.Add(ValueFactory.Create(N));
            _list.Add(ValueFactory.Create(NoName));
            _list.Add(ValueFactory.Create(NumPad0));
            _list.Add(ValueFactory.Create(NumPad1));
            _list.Add(ValueFactory.Create(NumPad2));
            _list.Add(ValueFactory.Create(NumPad3));
            _list.Add(ValueFactory.Create(NumPad4));
            _list.Add(ValueFactory.Create(NumPad5));
            _list.Add(ValueFactory.Create(NumPad6));
            _list.Add(ValueFactory.Create(NumPad7));
            _list.Add(ValueFactory.Create(NumPad8));
            _list.Add(ValueFactory.Create(NumPad9));
            _list.Add(ValueFactory.Create(O));
            _list.Add(ValueFactory.Create(Oem1));
            _list.Add(ValueFactory.Create(Oem102));
            _list.Add(ValueFactory.Create(Oem2));
            _list.Add(ValueFactory.Create(Oem3));
            _list.Add(ValueFactory.Create(Oem4));
            _list.Add(ValueFactory.Create(Oem5));
            _list.Add(ValueFactory.Create(Oem6));
            _list.Add(ValueFactory.Create(Oem7));
            _list.Add(ValueFactory.Create(Oem8));
            _list.Add(ValueFactory.Create(OemClear));
            _list.Add(ValueFactory.Create(OemComma));
            _list.Add(ValueFactory.Create(OemMinus));
            _list.Add(ValueFactory.Create(OemPeriod));
            _list.Add(ValueFactory.Create(OemPlus));
            _list.Add(ValueFactory.Create(P));
            _list.Add(ValueFactory.Create(Pa1));
            _list.Add(ValueFactory.Create(Packet));
            _list.Add(ValueFactory.Create(PageDown));
            _list.Add(ValueFactory.Create(PageUp));
            _list.Add(ValueFactory.Create(Pause));
            _list.Add(ValueFactory.Create(Play));
            _list.Add(ValueFactory.Create(Print));
            _list.Add(ValueFactory.Create(PrintScreen));
            _list.Add(ValueFactory.Create(Process));
            _list.Add(ValueFactory.Create(Q));
            _list.Add(ValueFactory.Create(R));
            _list.Add(ValueFactory.Create(RightArrow));
            _list.Add(ValueFactory.Create(RightWindows));
            _list.Add(ValueFactory.Create(S));
            _list.Add(ValueFactory.Create(Select));
            _list.Add(ValueFactory.Create(Separator));
            _list.Add(ValueFactory.Create(Sleep));
            _list.Add(ValueFactory.Create(Spacebar));
            _list.Add(ValueFactory.Create(Subtract));
            _list.Add(ValueFactory.Create(T));
            _list.Add(ValueFactory.Create(Tab));
            _list.Add(ValueFactory.Create(U));
            _list.Add(ValueFactory.Create(UpArrow));
            _list.Add(ValueFactory.Create(V));
            _list.Add(ValueFactory.Create(VolumeDown));
            _list.Add(ValueFactory.Create(VolumeMute));
            _list.Add(ValueFactory.Create(VolumeUp));
            _list.Add(ValueFactory.Create(W));
            _list.Add(ValueFactory.Create(X));
            _list.Add(ValueFactory.Create(Y));
            _list.Add(ValueFactory.Create(Z));
            _list.Add(ValueFactory.Create(Zoom));
        }

        [ContextProperty("A", "A")]
        public int A
        {
            get { return 65; }
        }

        [ContextProperty("Add", "Add")]
        public int Add
        {
            get { return 107; }
        }

        [ContextProperty("Applications", "Applications")]
        public int Applications
        {
            get { return 93; }
        }

        [ContextProperty("Attention", "Attention")]
        public int Attention
        {
            get { return 246; }
        }

        [ContextProperty("B", "B")]
        public int B
        {
            get { return 66; }
        }

        [ContextProperty("Backspace", "Backspace")]
        public int Backspace
        {
            get { return 8; }
        }

        [ContextProperty("BrowserBack", "BrowserBack")]
        public int BrowserBack
        {
            get { return 166; }
        }

        [ContextProperty("BrowserFavorites", "BrowserFavorites")]
        public int BrowserFavorites
        {
            get { return 171; }
        }

        [ContextProperty("BrowserForward", "BrowserForward")]
        public int BrowserForward
        {
            get { return 167; }
        }

        [ContextProperty("BrowserHome", "BrowserHome")]
        public int BrowserHome
        {
            get { return 172; }
        }

        [ContextProperty("BrowserRefresh", "BrowserRefresh")]
        public int BrowserRefresh
        {
            get { return 168; }
        }

        [ContextProperty("BrowserSearch", "BrowserSearch")]
        public int BrowserSearch
        {
            get { return 170; }
        }

        [ContextProperty("BrowserStop", "BrowserStop")]
        public int BrowserStop
        {
            get { return 169; }
        }

        [ContextProperty("C", "C")]
        public int C
        {
            get { return 67; }
        }

        [ContextProperty("Clear", "Clear")]
        public int Clear
        {
            get { return 12; }
        }

        [ContextProperty("CrSel", "CrSel")]
        public int CrSel
        {
            get { return 247; }
        }

        [ContextProperty("D", "D")]
        public int D
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

        [ContextProperty("Decimal", "Decimal")]
        public int Decimal
        {
            get { return 110; }
        }

        [ContextProperty("Delete", "Delete")]
        public int Delete
        {
            get { return 46; }
        }

        [ContextProperty("Divide", "Divide")]
        public int Divide
        {
            get { return 111; }
        }

        [ContextProperty("DownArrow", "DownArrow")]
        public int DownArrow
        {
            get { return 40; }
        }

        [ContextProperty("E", "E")]
        public int E
        {
            get { return 69; }
        }

        [ContextProperty("End", "End")]
        public int End
        {
            get { return 35; }
        }

        [ContextProperty("Enter", "Enter")]
        public int Enter
        {
            get { return 13; }
        }

        [ContextProperty("EraseEndOfFile", "EraseEndOfFile")]
        public int EraseEndOfFile
        {
            get { return 249; }
        }

        [ContextProperty("Escape", "Escape")]
        public int Escape
        {
            get { return 27; }
        }

        [ContextProperty("Execute", "Execute")]
        public int Execute
        {
            get { return 43; }
        }

        [ContextProperty("ExSel", "ExSel")]
        public int ExSel
        {
            get { return 248; }
        }

        [ContextProperty("F", "F")]
        public int F
        {
            get { return 70; }
        }

        [ContextProperty("F1", "F1")]
        public int F1
        {
            get { return 112; }
        }

        [ContextProperty("F10", "F10")]
        public int F10
        {
            get { return 121; }
        }

        [ContextProperty("F11", "F11")]
        public int F11
        {
            get { return 122; }
        }

        [ContextProperty("F12", "F12")]
        public int F12
        {
            get { return 123; }
        }

        [ContextProperty("F13", "F13")]
        public int F13
        {
            get { return 124; }
        }

        [ContextProperty("F14", "F14")]
        public int F14
        {
            get { return 125; }
        }

        [ContextProperty("F15", "F15")]
        public int F15
        {
            get { return 126; }
        }

        [ContextProperty("F16", "F16")]
        public int F16
        {
            get { return 127; }
        }

        [ContextProperty("F17", "F17")]
        public int F17
        {
            get { return 128; }
        }

        [ContextProperty("F18", "F18")]
        public int F18
        {
            get { return 129; }
        }

        [ContextProperty("F19", "F19")]
        public int F19
        {
            get { return 130; }
        }

        [ContextProperty("F2", "F2")]
        public int F2
        {
            get { return 113; }
        }

        [ContextProperty("F20", "F20")]
        public int F20
        {
            get { return 131; }
        }

        [ContextProperty("F21", "F21")]
        public int F21
        {
            get { return 132; }
        }

        [ContextProperty("F22", "F22")]
        public int F22
        {
            get { return 133; }
        }

        [ContextProperty("F23", "F23")]
        public int F23
        {
            get { return 134; }
        }

        [ContextProperty("F24", "F24")]
        public int F24
        {
            get { return 135; }
        }

        [ContextProperty("F3", "F3")]
        public int F3
        {
            get { return 114; }
        }

        [ContextProperty("F4", "F4")]
        public int F4
        {
            get { return 115; }
        }

        [ContextProperty("F5", "F5")]
        public int F5
        {
            get { return 116; }
        }

        [ContextProperty("F6", "F6")]
        public int F6
        {
            get { return 117; }
        }

        [ContextProperty("F7", "F7")]
        public int F7
        {
            get { return 118; }
        }

        [ContextProperty("F8", "F8")]
        public int F8
        {
            get { return 119; }
        }

        [ContextProperty("F9", "F9")]
        public int F9
        {
            get { return 120; }
        }

        [ContextProperty("G", "G")]
        public int G
        {
            get { return 71; }
        }

        [ContextProperty("H", "H")]
        public int H
        {
            get { return 72; }
        }

        [ContextProperty("Help", "Help")]
        public int Help
        {
            get { return 47; }
        }

        [ContextProperty("Home", "Home")]
        public int Home
        {
            get { return 36; }
        }

        [ContextProperty("I", "I")]
        public int I
        {
            get { return 73; }
        }

        [ContextProperty("Insert", "Insert")]
        public int Insert
        {
            get { return 45; }
        }

        [ContextProperty("J", "J")]
        public int J
        {
            get { return 74; }
        }

        [ContextProperty("K", "K")]
        public int K
        {
            get { return 75; }
        }

        [ContextProperty("L", "L")]
        public int L
        {
            get { return 76; }
        }

        [ContextProperty("LaunchApp1", "LaunchApp1")]
        public int LaunchApp1
        {
            get { return 182; }
        }

        [ContextProperty("LaunchApp2", "LaunchApp2")]
        public int LaunchApp2
        {
            get { return 183; }
        }

        [ContextProperty("LaunchMail", "LaunchMail")]
        public int LaunchMail
        {
            get { return 180; }
        }

        [ContextProperty("LaunchMediaSelect", "LaunchMediaSelect")]
        public int LaunchMediaSelect
        {
            get { return 181; }
        }

        [ContextProperty("LeftArrow", "LeftArrow")]
        public int LeftArrow
        {
            get { return 37; }
        }

        [ContextProperty("LeftWindows", "LeftWindows")]
        public int LeftWindows
        {
            get { return 91; }
        }

        [ContextProperty("M", "M")]
        public int M
        {
            get { return 77; }
        }

        [ContextProperty("MediaNext", "MediaNext")]
        public int MediaNext
        {
            get { return 176; }
        }

        [ContextProperty("MediaPlay", "MediaPlay")]
        public int MediaPlay
        {
            get { return 179; }
        }

        [ContextProperty("MediaPrevious", "MediaPrevious")]
        public int MediaPrevious
        {
            get { return 177; }
        }

        [ContextProperty("MediaStop", "MediaStop")]
        public int MediaStop
        {
            get { return 178; }
        }

        [ContextProperty("Multiply", "Multiply")]
        public int Multiply
        {
            get { return 106; }
        }

        [ContextProperty("N", "N")]
        public int N
        {
            get { return 78; }
        }

        [ContextProperty("NoName", "NoName")]
        public int NoName
        {
            get { return 252; }
        }

        [ContextProperty("NumPad0", "NumPad0")]
        public int NumPad0
        {
            get { return 96; }
        }

        [ContextProperty("NumPad1", "NumPad1")]
        public int NumPad1
        {
            get { return 97; }
        }

        [ContextProperty("NumPad2", "NumPad2")]
        public int NumPad2
        {
            get { return 98; }
        }

        [ContextProperty("NumPad3", "NumPad3")]
        public int NumPad3
        {
            get { return 99; }
        }

        [ContextProperty("NumPad4", "NumPad4")]
        public int NumPad4
        {
            get { return 100; }
        }

        [ContextProperty("NumPad5", "NumPad5")]
        public int NumPad5
        {
            get { return 101; }
        }

        [ContextProperty("NumPad6", "NumPad6")]
        public int NumPad6
        {
            get { return 102; }
        }

        [ContextProperty("NumPad7", "NumPad7")]
        public int NumPad7
        {
            get { return 103; }
        }

        [ContextProperty("NumPad8", "NumPad8")]
        public int NumPad8
        {
            get { return 104; }
        }

        [ContextProperty("NumPad9", "NumPad9")]
        public int NumPad9
        {
            get { return 105; }
        }

        [ContextProperty("O", "O")]
        public int O
        {
            get { return 79; }
        }

        [ContextProperty("Oem1", "Oem1")]
        public int Oem1
        {
            get { return 186; }
        }

        [ContextProperty("Oem102", "Oem102")]
        public int Oem102
        {
            get { return 226; }
        }

        [ContextProperty("Oem2", "Oem2")]
        public int Oem2
        {
            get { return 191; }
        }

        [ContextProperty("Oem3", "Oem3")]
        public int Oem3
        {
            get { return 192; }
        }

        [ContextProperty("Oem4", "Oem4")]
        public int Oem4
        {
            get { return 219; }
        }

        [ContextProperty("Oem5", "Oem5")]
        public int Oem5
        {
            get { return 220; }
        }

        [ContextProperty("Oem6", "Oem6")]
        public int Oem6
        {
            get { return 221; }
        }

        [ContextProperty("Oem7", "Oem7")]
        public int Oem7
        {
            get { return 222; }
        }

        [ContextProperty("Oem8", "Oem8")]
        public int Oem8
        {
            get { return 223; }
        }

        [ContextProperty("OemClear", "OemClear")]
        public int OemClear
        {
            get { return 254; }
        }

        [ContextProperty("OemComma", "OemComma")]
        public int OemComma
        {
            get { return 188; }
        }

        [ContextProperty("OemMinus", "OemMinus")]
        public int OemMinus
        {
            get { return 189; }
        }

        [ContextProperty("OemPeriod", "OemPeriod")]
        public int OemPeriod
        {
            get { return 190; }
        }

        [ContextProperty("OemPlus", "OemPlus")]
        public int OemPlus
        {
            get { return 187; }
        }

        [ContextProperty("P", "P")]
        public int P
        {
            get { return 80; }
        }

        [ContextProperty("Pa1", "Pa1")]
        public int Pa1
        {
            get { return 253; }
        }

        [ContextProperty("Packet", "Packet")]
        public int Packet
        {
            get { return 231; }
        }

        [ContextProperty("PageDown", "PageDown")]
        public int PageDown
        {
            get { return 34; }
        }

        [ContextProperty("PageUp", "PageUp")]
        public int PageUp
        {
            get { return 33; }
        }

        [ContextProperty("Pause", "Pause")]
        public int Pause
        {
            get { return 19; }
        }

        [ContextProperty("Play", "Play")]
        public int Play
        {
            get { return 250; }
        }

        [ContextProperty("Print", "Print")]
        public int Print
        {
            get { return 42; }
        }

        [ContextProperty("PrintScreen", "PrintScreen")]
        public int PrintScreen
        {
            get { return 44; }
        }

        [ContextProperty("Process", "Process")]
        public int Process
        {
            get { return 229; }
        }

        [ContextProperty("Q", "Q")]
        public int Q
        {
            get { return 81; }
        }

        [ContextProperty("R", "R")]
        public int R
        {
            get { return 82; }
        }

        [ContextProperty("RightArrow", "RightArrow")]
        public int RightArrow
        {
            get { return 39; }
        }

        [ContextProperty("RightWindows", "RightWindows")]
        public int RightWindows
        {
            get { return 92; }
        }

        [ContextProperty("S", "S")]
        public int S
        {
            get { return 83; }
        }

        [ContextProperty("Select", "Select")]
        public int Select
        {
            get { return 41; }
        }

        [ContextProperty("Separator", "Separator")]
        public int Separator
        {
            get { return 108; }
        }

        [ContextProperty("Sleep", "Sleep")]
        public int Sleep
        {
            get { return 95; }
        }

        [ContextProperty("Spacebar", "Spacebar")]
        public int Spacebar
        {
            get { return 32; }
        }

        [ContextProperty("Subtract", "Subtract")]
        public int Subtract
        {
            get { return 109; }
        }

        [ContextProperty("T", "T")]
        public int T
        {
            get { return 84; }
        }

        [ContextProperty("Tab", "Tab")]
        public int Tab
        {
            get { return 9; }
        }

        [ContextProperty("U", "U")]
        public int U
        {
            get { return 85; }
        }

        [ContextProperty("UpArrow", "UpArrow")]
        public int UpArrow
        {
            get { return 38; }
        }

        [ContextProperty("V", "V")]
        public int V
        {
            get { return 86; }
        }

        [ContextProperty("VolumeDown", "VolumeDown")]
        public int VolumeDown
        {
            get { return 174; }
        }

        [ContextProperty("VolumeMute", "VolumeMute")]
        public int VolumeMute
        {
            get { return 173; }
        }

        [ContextProperty("VolumeUp", "VolumeUp")]
        public int VolumeUp
        {
            get { return 175; }
        }

        [ContextProperty("W", "W")]
        public int W
        {
            get { return 87; }
        }

        [ContextProperty("X", "X")]
        public int X
        {
            get { return 88; }
        }

        [ContextProperty("Y", "Y")]
        public int Y
        {
            get { return 89; }
        }

        [ContextProperty("Z", "Z")]
        public int Z
        {
            get { return 90; }
        }

        [ContextProperty("Zoom", "Zoom")]
        public int Zoom
        {
            get { return 251; }
        }

        [ContextMethod("ВСтроку", "ВСтроку")]
        public string ToStringRu(decimal p1)
        {
            string str = p1.ToString();
            switch (p1)
            {
                case 65:
                    str = "A";
                    break;
                case 107:
                    str = "Add";
                    break;
                case 93:
                    str = "Applications";
                    break;
                case 246:
                    str = "Attention";
                    break;
                case 66:
                    str = "B";
                    break;
                case 8:
                    str = "Backspace";
                    break;
                case 166:
                    str = "BrowserBack";
                    break;
                case 171:
                    str = "BrowserFavorites";
                    break;
                case 167:
                    str = "BrowserForward";
                    break;
                case 172:
                    str = "BrowserHome";
                    break;
                case 168:
                    str = "BrowserRefresh";
                    break;
                case 170:
                    str = "BrowserSearch";
                    break;
                case 169:
                    str = "BrowserStop";
                    break;
                case 67:
                    str = "C";
                    break;
                case 12:
                    str = "Clear";
                    break;
                case 247:
                    str = "CrSel";
                    break;
                case 68:
                    str = "D";
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
                case 110:
                    str = "Decimal";
                    break;
                case 46:
                    str = "Delete";
                    break;
                case 111:
                    str = "Divide";
                    break;
                case 40:
                    str = "DownArrow";
                    break;
                case 69:
                    str = "E";
                    break;
                case 35:
                    str = "End";
                    break;
                case 13:
                    str = "Enter";
                    break;
                case 249:
                    str = "EraseEndOfFile";
                    break;
                case 27:
                    str = "Escape";
                    break;
                case 43:
                    str = "Execute";
                    break;
                case 248:
                    str = "ExSel";
                    break;
                case 70:
                    str = "F";
                    break;
                case 112:
                    str = "F1";
                    break;
                case 121:
                    str = "F10";
                    break;
                case 122:
                    str = "F11";
                    break;
                case 123:
                    str = "F12";
                    break;
                case 124:
                    str = "F13";
                    break;
                case 125:
                    str = "F14";
                    break;
                case 126:
                    str = "F15";
                    break;
                case 127:
                    str = "F16";
                    break;
                case 128:
                    str = "F17";
                    break;
                case 129:
                    str = "F18";
                    break;
                case 130:
                    str = "F19";
                    break;
                case 113:
                    str = "F2";
                    break;
                case 131:
                    str = "F20";
                    break;
                case 132:
                    str = "F21";
                    break;
                case 133:
                    str = "F22";
                    break;
                case 134:
                    str = "F23";
                    break;
                case 135:
                    str = "F24";
                    break;
                case 114:
                    str = "F3";
                    break;
                case 115:
                    str = "F4";
                    break;
                case 116:
                    str = "F5";
                    break;
                case 117:
                    str = "F6";
                    break;
                case 118:
                    str = "F7";
                    break;
                case 119:
                    str = "F8";
                    break;
                case 120:
                    str = "F9";
                    break;
                case 71:
                    str = "G";
                    break;
                case 72:
                    str = "H";
                    break;
                case 47:
                    str = "Help";
                    break;
                case 36:
                    str = "Home";
                    break;
                case 73:
                    str = "I";
                    break;
                case 45:
                    str = "Insert";
                    break;
                case 74:
                    str = "J";
                    break;
                case 75:
                    str = "K";
                    break;
                case 76:
                    str = "L";
                    break;
                case 182:
                    str = "LaunchApp1";
                    break;
                case 183:
                    str = "LaunchApp2";
                    break;
                case 180:
                    str = "LaunchMail";
                    break;
                case 181:
                    str = "LaunchMediaSelect";
                    break;
                case 37:
                    str = "LeftArrow";
                    break;
                case 91:
                    str = "LeftWindows";
                    break;
                case 77:
                    str = "M";
                    break;
                case 176:
                    str = "MediaNext";
                    break;
                case 179:
                    str = "MediaPlay";
                    break;
                case 177:
                    str = "MediaPrevious";
                    break;
                case 178:
                    str = "MediaStop";
                    break;
                case 106:
                    str = "Multiply";
                    break;
                case 78:
                    str = "N";
                    break;
                case 252:
                    str = "NoName";
                    break;
                case 96:
                    str = "NumPad0";
                    break;
                case 97:
                    str = "NumPad1";
                    break;
                case 98:
                    str = "NumPad2";
                    break;
                case 99:
                    str = "NumPad3";
                    break;
                case 100:
                    str = "NumPad4";
                    break;
                case 101:
                    str = "NumPad5";
                    break;
                case 102:
                    str = "NumPad6";
                    break;
                case 103:
                    str = "NumPad7";
                    break;
                case 104:
                    str = "NumPad8";
                    break;
                case 105:
                    str = "NumPad9";
                    break;
                case 79:
                    str = "O";
                    break;
                case 186:
                    str = "Oem1";
                    break;
                case 226:
                    str = "Oem102";
                    break;
                case 191:
                    str = "Oem2";
                    break;
                case 192:
                    str = "Oem3";
                    break;
                case 219:
                    str = "Oem4";
                    break;
                case 220:
                    str = "Oem5";
                    break;
                case 221:
                    str = "Oem6";
                    break;
                case 222:
                    str = "Oem7";
                    break;
                case 223:
                    str = "Oem8";
                    break;
                case 254:
                    str = "OemClear";
                    break;
                case 188:
                    str = "OemComma";
                    break;
                case 189:
                    str = "OemMinus";
                    break;
                case 190:
                    str = "OemPeriod";
                    break;
                case 187:
                    str = "OemPlus";
                    break;
                case 80:
                    str = "P";
                    break;
                case 253:
                    str = "Pa1";
                    break;
                case 231:
                    str = "Packet";
                    break;
                case 34:
                    str = "PageDown";
                    break;
                case 33:
                    str = "PageUp";
                    break;
                case 19:
                    str = "Pause";
                    break;
                case 250:
                    str = "Play";
                    break;
                case 42:
                    str = "Print";
                    break;
                case 44:
                    str = "PrintScreen";
                    break;
                case 229:
                    str = "Process";
                    break;
                case 81:
                    str = "Q";
                    break;
                case 82:
                    str = "R";
                    break;
                case 39:
                    str = "RightArrow";
                    break;
                case 92:
                    str = "RightWindows";
                    break;
                case 83:
                    str = "S";
                    break;
                case 41:
                    str = "Select";
                    break;
                case 108:
                    str = "Separator";
                    break;
                case 95:
                    str = "Sleep";
                    break;
                case 32:
                    str = "Spacebar";
                    break;
                case 109:
                    str = "Subtract";
                    break;
                case 84:
                    str = "T";
                    break;
                case 9:
                    str = "Tab";
                    break;
                case 85:
                    str = "U";
                    break;
                case 38:
                    str = "UpArrow";
                    break;
                case 86:
                    str = "V";
                    break;
                case 174:
                    str = "VolumeDown";
                    break;
                case 173:
                    str = "VolumeMute";
                    break;
                case 175:
                    str = "VolumeUp";
                    break;
                case 87:
                    str = "W";
                    break;
                case 88:
                    str = "X";
                    break;
                case 89:
                    str = "Y";
                    break;
                case 90:
                    str = "Z";
                    break;
                case 251:
                    str = "Zoom";
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
                case 65:
                    str = "A";
                    break;
                case 107:
                    str = "Add";
                    break;
                case 93:
                    str = "Applications";
                    break;
                case 246:
                    str = "Attention";
                    break;
                case 66:
                    str = "B";
                    break;
                case 8:
                    str = "Backspace";
                    break;
                case 166:
                    str = "BrowserBack";
                    break;
                case 171:
                    str = "BrowserFavorites";
                    break;
                case 167:
                    str = "BrowserForward";
                    break;
                case 172:
                    str = "BrowserHome";
                    break;
                case 168:
                    str = "BrowserRefresh";
                    break;
                case 170:
                    str = "BrowserSearch";
                    break;
                case 169:
                    str = "BrowserStop";
                    break;
                case 67:
                    str = "C";
                    break;
                case 12:
                    str = "Clear";
                    break;
                case 247:
                    str = "CrSel";
                    break;
                case 68:
                    str = "D";
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
                case 110:
                    str = "Decimal";
                    break;
                case 46:
                    str = "Delete";
                    break;
                case 111:
                    str = "Divide";
                    break;
                case 40:
                    str = "DownArrow";
                    break;
                case 69:
                    str = "E";
                    break;
                case 35:
                    str = "End";
                    break;
                case 13:
                    str = "Enter";
                    break;
                case 249:
                    str = "EraseEndOfFile";
                    break;
                case 27:
                    str = "Escape";
                    break;
                case 43:
                    str = "Execute";
                    break;
                case 248:
                    str = "ExSel";
                    break;
                case 70:
                    str = "F";
                    break;
                case 112:
                    str = "F1";
                    break;
                case 121:
                    str = "F10";
                    break;
                case 122:
                    str = "F11";
                    break;
                case 123:
                    str = "F12";
                    break;
                case 124:
                    str = "F13";
                    break;
                case 125:
                    str = "F14";
                    break;
                case 126:
                    str = "F15";
                    break;
                case 127:
                    str = "F16";
                    break;
                case 128:
                    str = "F17";
                    break;
                case 129:
                    str = "F18";
                    break;
                case 130:
                    str = "F19";
                    break;
                case 113:
                    str = "F2";
                    break;
                case 131:
                    str = "F20";
                    break;
                case 132:
                    str = "F21";
                    break;
                case 133:
                    str = "F22";
                    break;
                case 134:
                    str = "F23";
                    break;
                case 135:
                    str = "F24";
                    break;
                case 114:
                    str = "F3";
                    break;
                case 115:
                    str = "F4";
                    break;
                case 116:
                    str = "F5";
                    break;
                case 117:
                    str = "F6";
                    break;
                case 118:
                    str = "F7";
                    break;
                case 119:
                    str = "F8";
                    break;
                case 120:
                    str = "F9";
                    break;
                case 71:
                    str = "G";
                    break;
                case 72:
                    str = "H";
                    break;
                case 47:
                    str = "Help";
                    break;
                case 36:
                    str = "Home";
                    break;
                case 73:
                    str = "I";
                    break;
                case 45:
                    str = "Insert";
                    break;
                case 74:
                    str = "J";
                    break;
                case 75:
                    str = "K";
                    break;
                case 76:
                    str = "L";
                    break;
                case 182:
                    str = "LaunchApp1";
                    break;
                case 183:
                    str = "LaunchApp2";
                    break;
                case 180:
                    str = "LaunchMail";
                    break;
                case 181:
                    str = "LaunchMediaSelect";
                    break;
                case 37:
                    str = "LeftArrow";
                    break;
                case 91:
                    str = "LeftWindows";
                    break;
                case 77:
                    str = "M";
                    break;
                case 176:
                    str = "MediaNext";
                    break;
                case 179:
                    str = "MediaPlay";
                    break;
                case 177:
                    str = "MediaPrevious";
                    break;
                case 178:
                    str = "MediaStop";
                    break;
                case 106:
                    str = "Multiply";
                    break;
                case 78:
                    str = "N";
                    break;
                case 252:
                    str = "NoName";
                    break;
                case 96:
                    str = "NumPad0";
                    break;
                case 97:
                    str = "NumPad1";
                    break;
                case 98:
                    str = "NumPad2";
                    break;
                case 99:
                    str = "NumPad3";
                    break;
                case 100:
                    str = "NumPad4";
                    break;
                case 101:
                    str = "NumPad5";
                    break;
                case 102:
                    str = "NumPad6";
                    break;
                case 103:
                    str = "NumPad7";
                    break;
                case 104:
                    str = "NumPad8";
                    break;
                case 105:
                    str = "NumPad9";
                    break;
                case 79:
                    str = "O";
                    break;
                case 186:
                    str = "Oem1";
                    break;
                case 226:
                    str = "Oem102";
                    break;
                case 191:
                    str = "Oem2";
                    break;
                case 192:
                    str = "Oem3";
                    break;
                case 219:
                    str = "Oem4";
                    break;
                case 220:
                    str = "Oem5";
                    break;
                case 221:
                    str = "Oem6";
                    break;
                case 222:
                    str = "Oem7";
                    break;
                case 223:
                    str = "Oem8";
                    break;
                case 254:
                    str = "OemClear";
                    break;
                case 188:
                    str = "OemComma";
                    break;
                case 189:
                    str = "OemMinus";
                    break;
                case 190:
                    str = "OemPeriod";
                    break;
                case 187:
                    str = "OemPlus";
                    break;
                case 80:
                    str = "P";
                    break;
                case 253:
                    str = "Pa1";
                    break;
                case 231:
                    str = "Packet";
                    break;
                case 34:
                    str = "PageDown";
                    break;
                case 33:
                    str = "PageUp";
                    break;
                case 19:
                    str = "Pause";
                    break;
                case 250:
                    str = "Play";
                    break;
                case 42:
                    str = "Print";
                    break;
                case 44:
                    str = "PrintScreen";
                    break;
                case 229:
                    str = "Process";
                    break;
                case 81:
                    str = "Q";
                    break;
                case 82:
                    str = "R";
                    break;
                case 39:
                    str = "RightArrow";
                    break;
                case 92:
                    str = "RightWindows";
                    break;
                case 83:
                    str = "S";
                    break;
                case 41:
                    str = "Select";
                    break;
                case 108:
                    str = "Separator";
                    break;
                case 95:
                    str = "Sleep";
                    break;
                case 32:
                    str = "Spacebar";
                    break;
                case 109:
                    str = "Subtract";
                    break;
                case 84:
                    str = "T";
                    break;
                case 9:
                    str = "Tab";
                    break;
                case 85:
                    str = "U";
                    break;
                case 38:
                    str = "UpArrow";
                    break;
                case 86:
                    str = "V";
                    break;
                case 174:
                    str = "VolumeDown";
                    break;
                case 173:
                    str = "VolumeMute";
                    break;
                case 175:
                    str = "VolumeUp";
                    break;
                case 87:
                    str = "W";
                    break;
                case 88:
                    str = "X";
                    break;
                case 89:
                    str = "Y";
                    break;
                case 90:
                    str = "Z";
                    break;
                case 251:
                    str = "Zoom";
                    break;
            }
            return str;
        }
    }
}
