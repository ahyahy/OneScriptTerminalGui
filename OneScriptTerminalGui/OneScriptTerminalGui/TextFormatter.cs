using System;
using System.Collections.Generic;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library;

namespace ostgui
{
    public class TextFormatter
    {
        public TfTextFormatter dll_obj;
        public Terminal.Gui.TextFormatter M_TextFormatter;

        public TextFormatter()
        {
            M_TextFormatter = new Terminal.Gui.TextFormatter();
            OneScriptTerminalGui.AddToHashtable(M_TextFormatter, this);
        }

        public bool AutoSize
        {
            get { return M_TextFormatter.AutoSize; }
            set { M_TextFormatter.AutoSize = value; }
        }

        public int VerticalAlignment
        {
            get { return (int)M_TextFormatter.VerticalAlignment; }
            set { M_TextFormatter.VerticalAlignment = (Terminal.Gui.VerticalTextAlignment)value; }
        }

        public int Alignment
        {
            get { return (int)M_TextFormatter.Alignment; }
            set { M_TextFormatter.Alignment = (Terminal.Gui.TextAlignment)value; }
        }

        public int HotKey
        {
            get { return (int)M_TextFormatter.HotKey; }
        }

        public ArrayImpl Lines
        {
            get
            {
                ArrayImpl ArrayImpl1 = new ArrayImpl();
                List<NStack.ustring> ustring1 = M_TextFormatter.Lines;
                for (int i = 0; i < ustring1.Count; i++)
                {
                    ArrayImpl1.Add(ValueFactory.Create(ustring1[i].ToString()));
                }
                return ArrayImpl1;
            }
        }

        public int Direction
        {
            get { return (int)M_TextFormatter.Direction; }
            set { M_TextFormatter.Direction = (Terminal.Gui.TextDirection)value; }
        }

        public int HotKeyPos
        {
            get { return M_TextFormatter.HotKeyPos; }
            set { M_TextFormatter.HotKeyPos = value; }
        }

        public int CursorPosition
        {
            get { return M_TextFormatter.CursorPosition; }
            set { M_TextFormatter.CursorPosition = value; }
        }

        public Size Size
        {
            get { return new Size(M_TextFormatter.Size); }
            set { M_TextFormatter.Size = value.M_Size; }
        }

        public Rune HotKeySpecifier
        {
            get { return M_TextFormatter.HotKeySpecifier; }
            set { M_TextFormatter.HotKeySpecifier = value; }
        }

        public bool PreserveTrailingSpaces
        {
            get { return M_TextFormatter.PreserveTrailingSpaces; }
            set { M_TextFormatter.PreserveTrailingSpaces = value; }
        }

        public string Text
        {
            get { return M_TextFormatter.Text.ToString(); }
            set { M_TextFormatter.Text = value; }
        }

        public bool NeedsFormat
        {
            get { return M_TextFormatter.NeedsFormat; }
            set { M_TextFormatter.NeedsFormat = value; }
        }

        public string Justify(string p1, int p2, string p3 = " ", Terminal.Gui.TextDirection p4 = Terminal.Gui.TextDirection.LeftRight_TopBottom)
        {
            return Terminal.Gui.TextFormatter.Justify(p1, p2, Convert.ToChar(p3), p4).ToString();
        }

        public Rect CalcRect(int p1, int p2, string p3, Terminal.Gui.TextDirection p4 = Terminal.Gui.TextDirection.LeftRight_TopBottom)
        {
            return new Rect(Terminal.Gui.TextFormatter.CalcRect(p1, p2, p3, p4));
        }

        public string ReplaceHotKeyWithTag(string p1, int p2)
        {
            return M_TextFormatter.ReplaceHotKeyWithTag(p1, p2).ToString();
        }

        public int MaxWidth(string p1, int p2)
        {
            return Terminal.Gui.TextFormatter.MaxWidth(p1, p2);
        }

        public int GetMaxColsForWidth(string p1, int p2)
        {
            List<NStack.ustring> ustring1 = new List<NStack.ustring>();
            string[] result = p1.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < result.Length; i++)
            {
                ustring1.Add(result[i]);
            }
            return Terminal.Gui.TextFormatter.GetMaxColsForWidth(ustring1, p2);
        }

        public int GetSumMaxCharWidth(string p1, int p2 = -1, int p3 = -1)
        {
            List<NStack.ustring> ustring1 = new List<NStack.ustring>();
            string[] result = p1.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < result.Length; i++)
            {
                ustring1.Add(result[i]);
            }
            return Terminal.Gui.TextFormatter.GetSumMaxCharWidth(ustring1, p2, p3);
        }

        public int GetMaxLengthForWidth(string p1, int p2)
        {
            return Terminal.Gui.TextFormatter.GetMaxLengthForWidth(p1, p2);
        }

        public int MaxLines(string p1, int p2)
        {
            return Terminal.Gui.TextFormatter.MaxLines(p1, p2);
        }

        public bool FindHotKey(string p1, string p2, bool p3, out int p4, out Terminal.Gui.Key p5)
        {
            Rune Rune1 = p2.ToCharArray()[0];
            return Terminal.Gui.TextFormatter.FindHotKey(p1, Rune1, p3, out p4, out p5);
        }

        public string ClipAndJustify(string p1, int p2, int p3, Terminal.Gui.TextDirection p4 = Terminal.Gui.TextDirection.LeftRight_TopBottom)
        {
            return Terminal.Gui.TextFormatter.ClipAndJustify(p1, p2, (Terminal.Gui.TextAlignment)p3, p4).ToString();
        }

        public string ClipOrPad(string p1, int p2)
        {
            return Terminal.Gui.TextFormatter.ClipOrPad(p1, p2);
        }

        public string WordWrap(string p1, int p2, bool p3 = false, int p4 = 0, Terminal.Gui.TextDirection p5 = Terminal.Gui.TextDirection.LeftRight_TopBottom)
        {
            string str = "";
            List<NStack.ustring> list1 = Terminal.Gui.TextFormatter.WordWrap(p1, p2, p3, p4, (Terminal.Gui.TextDirection)p5);
            for (int i = 0; i < list1.Count; i++)
            {
                if (i == 0)
                {
                    str = list1[i].ToString() + Environment.NewLine;
                }
                else if (i == (list1.Count - 1))
                {
                    str += list1[i].ToString();
                }
                else
                {
                    str += list1[i].ToString() + Environment.NewLine;
                }
            }
            return str;
        }

        public string SplitNewLine(string p1)
        {
            List<NStack.ustring> ustring1 = Terminal.Gui.TextFormatter.SplitNewLine(p1);
            string str = "";
            for (int i = 0; i < ustring1.Count; i++)
            {
                str += ustring1[i].ToString() + Environment.NewLine;
            }
            return str;
        }

        public void Draw(Rect p1, Attribute p2, Attribute p3, Rect p4 = default, bool p5 = true)
        {
            M_TextFormatter.Draw(p1.M_Rect, p2.M_Attribute, p3.M_Attribute, p4.M_Rect, p5);
        }

        public int MaxWidthLine(string p1)
        {
            return Terminal.Gui.TextFormatter.MaxWidthLine(p1);
        }

        public string RemoveHotKeySpecifier(string p1, int p2, string p3)
        {
            return Terminal.Gui.TextFormatter.RemoveHotKeySpecifier(p1, p2, p3.ToCharArray()[0]).ToString();
        }

        public string Format(string p1, int p2, int p3, bool p4, bool p5 = false, int p6 = 0, int p7 = 0)
        {
            List<NStack.ustring> ustring1 = Terminal.Gui.TextFormatter.Format(p1, p2, (Terminal.Gui.TextAlignment)p3, p4, p5, p6, (Terminal.Gui.TextDirection)p7);
            string str = "";
            for (int i = 0; i < ustring1.Count; i++)
            {
                str += ustring1[i].ToString() + Environment.NewLine;
            }
            return str;
        }

        public int GetTextWidth(string p1)
        {
            return Terminal.Gui.TextFormatter.GetTextWidth(p1.ToString());
        }

        public new string ToString()
        {
            return M_TextFormatter.ToString();
        }
    }

    [ContextClass("ТфОформительТекста", "TfTextFormatter")]
    public class TfTextFormatter : AutoContext<TfTextFormatter>
    {

        public TfTextFormatter()
        {
            TextFormatter TextFormatter1 = new TextFormatter();
            TextFormatter1.dll_obj = this;
            Base_obj = TextFormatter1;
        }

        public TfSize Size
        {
            get { return new TfSize(Base_obj.Size); }
            set { Base_obj.Size = value.Base_obj; }
        }

        public TextFormatter Base_obj;

        public TfAction HotKeyChanged { get; set; }

        [ContextProperty("ВертикальноеВыравнивание", "VerticalAlignment")]
        public int VerticalAlignment
        {
            get { return Base_obj.VerticalAlignment; }
            set { Base_obj.VerticalAlignment = value; }
        }

        [ContextProperty("Выравнивание", "Alignment")]
        public int Alignment
        {
            get { return Base_obj.Alignment; }
            set { Base_obj.Alignment = value; }
        }

        [ContextProperty("Линии", "Lines")]
        public ArrayImpl Lines
        {
            get { return Base_obj.Lines; }
        }

        [ContextProperty("НаправлениеТекста", "Direction")]
        public int Direction
        {
            get { return Base_obj.Direction; }
            set { Base_obj.Direction = value; }
        }

        [ContextProperty("Текст", "Text")]
        public string Text
        {
            get { return Base_obj.Text; }
            set { Base_obj.Text = value; }
        }

        [ContextMethod("МаксимальнаяШиринаСтроки", "MaxWidthLine")]
        public int MaxWidthLine(string p1)
        {
            return Base_obj.MaxWidthLine(p1);
        }

        [ContextMethod("МаксимальноСтрок", "MaxLines")]
        public int MaxLines(string p1, int p2)
        {
            return Base_obj.MaxLines(p1, p2);
        }

        [ContextMethod("ПереносСлов", "WordWrap")]
        public string WordWrap(string p1, int p2, bool p3 = false, int p4 = 0, int p5 = 0)
        {
            return Base_obj.WordWrap(p1, p2, p3, p4, (Terminal.Gui.TextDirection)p5);
        }

        [ContextMethod("ПолучитьАвтоРазмер", "GetAutoSize")]
        public TfSize GetAutoSize()
        {
            try
            {
                return new TfSize(MaxWidthLine(Text) + 2, MaxLines(Text, MaxWidthLine(Text)) + 2);
            }
            catch
            {
                return null;
            }
        }

        [ContextMethod("ШиринаТекста", "GetTextWidth")]
        public int GetTextWidth(string p1)
        {
            return Base_obj.GetTextWidth(p1);
        }

    }
}
