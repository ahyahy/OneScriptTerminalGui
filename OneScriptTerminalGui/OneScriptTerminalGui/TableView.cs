using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library.ValueList;
using ScriptEngine.HostedScript.Library;
using System.Collections;
using Terminal.Gui;
using System.Collections.Generic;
using System.Linq;

namespace ostgui
{
    public class TableView : View
    {
        public new TfTableView dll_obj;
        public Terminal.Gui.TableView M_TableView;

        public TableView()
        {
            M_TableView = new Terminal.Gui.TableView();
            base.M_View = M_TableView;
            Utils.AddToHashtable(M_TableView, this);

            M_TableView.CellActivated += M_TableView_CellActivated;
            M_TableView.SelectedCellChanged += M_TableView_SelectedCellChanged;
        }

        private void M_TableView_SelectedCellChanged(Terminal.Gui.TableView.SelectedCellChangedEventArgs obj)
        {
            if (dll_obj.SelectedCellChanged != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = dll_obj;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.SelectedCellChanged);
                TfEventArgs1.newCol = ValueFactory.Create(obj.NewCol);
                TfEventArgs1.newRow = ValueFactory.Create(obj.NewRow);
                TfEventArgs1.oldCol = ValueFactory.Create(obj.OldCol);
                TfEventArgs1.oldRow = ValueFactory.Create(obj.OldRow);
                TfEventArgs1.dataTable = ((DataTable)((DataTableEx)M_TableView.Table).M_Object).dll_obj;
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(dll_obj.SelectedCellChanged);
            }
        }

        private void M_TableView_CellActivated(Terminal.Gui.TableView.CellActivatedEventArgs obj)
        {
            if (dll_obj.NativeCellActivated)
            {
                EditCurrentCell(obj);
            }
            else
            {
                if (dll_obj.CellActivated != null)
                {
                    TfEventArgs TfEventArgs1 = new TfEventArgs();
                    TfEventArgs1.sender = dll_obj;
                    TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.CellActivated);
                    TfEventArgs1.columnIndex = ValueFactory.Create(obj.Col);
                    TfEventArgs1.rowIndex = ValueFactory.Create(obj.Row);
                    TfEventArgs1.dataTable = ((DataTable)((DataTableEx)M_TableView.Table).M_Object).dll_obj;
                    OneScriptTerminalGui.Event = TfEventArgs1;
                    OneScriptTerminalGui.ExecuteEvent(dll_obj.CellActivated);
                }
            }
        }

        private void EditCurrentCell(Terminal.Gui.TableView.CellActivatedEventArgs e)
        {
            if (e.Table == null)
                return;
            var o = e.Table.Rows[e.Row][e.Col];

            var title = o is uint u ? GetUnicodeCategory(u) + $"(0x{o:X4})" : "Введите новое значение";

            var oldValue = e.Table.Rows[e.Row][e.Col].ToString();
            bool okPressed = false;

            var ok = new Terminal.Gui.Button("ОК", is_default: true);
            ok.Clicked += () => { okPressed = true; Application.RequestStop(); };
            var cancel = new Terminal.Gui.Button("Отмена");
            cancel.Clicked += () => { Application.RequestStop(); };
            var d = new Terminal.Gui.Dialog(title, 60, 20, ok, cancel);

            var lbl = new Terminal.Gui.Label()
            {
                X = 0,
                Y = 1,
                Text = e.Table.Columns[e.Col].ColumnName
            };

            var tf = new Terminal.Gui.TextField()
            {
                Text = oldValue,
                X = 0,
                Y = 2,
                Width = Terminal.Gui.Dim.Fill()
            };

            d.Add(lbl, tf);
            tf.SetFocus();

            Application.Run(d);

            if (okPressed)
            {
                try
                {
                    e.Table.Rows[e.Row][e.Col] = string.IsNullOrWhiteSpace(tf.Text.ToString()) ? DBNull.Value : (object)tf.Text;
                }
                catch (Exception ex)
                {
                    Terminal.Gui.MessageBox.ErrorQuery(60, 20, "Не удалось установить текст", ex.Message, "ОК");
                }
                M_TableView.Update();
            }
        }

        private string GetUnicodeCategory(uint u)
        {
            return Ranges.FirstOrDefault(r => u >= r.Start && u <= r.End)?.Category ?? "Неизвестный";
        }

        class UnicodeRange
        {
            public uint Start;
            public uint End;
            public string Category;
            public UnicodeRange(uint start, uint end, string category)
            {
                this.Start = start;
                this.End = end;
                this.Category = category;
            }
        }

        public static uint MaxCodePointVal => 0x10FFFF;

        List<UnicodeRange> Ranges = new List<UnicodeRange> {
            new UnicodeRange (0x0000, 0x001F, "ASCII Control Characters"),
            new UnicodeRange (0x0080, 0x009F, "C0 Control Characters"),
            new UnicodeRange(0x1100, 0x11ff,"Hangul Jamo"), // Здесь обычно начинаются широкие символы
            new UnicodeRange(0x20A0, 0x20CF,"Currency Symbols"),
            new UnicodeRange(0x2100, 0x214F,"Letterlike Symbols"),
            new UnicodeRange(0x2190, 0x21ff,"Arrows" ),
            new UnicodeRange(0x2200, 0x22ff,"Mathematical symbols"),
            new UnicodeRange(0x2300, 0x23ff,"Miscellaneous Technical"),
            new UnicodeRange(0x2500, 0x25ff,"Box Drawing & Geometric Shapes"),
            new UnicodeRange(0x2600, 0x26ff,"Miscellaneous Symbols"),
            new UnicodeRange(0x2700, 0x27ff,"Dingbats"),
            new UnicodeRange(0x2800, 0x28ff,"Braille"),
            new UnicodeRange(0x2b00, 0x2bff,"Miscellaneous Symbols and Arrows"),
            new UnicodeRange(0xFB00, 0xFb4f,"Alphabetic Presentation Forms"),
            new UnicodeRange(0x12400, 0x1240f,"Cuneiform Numbers and Punctuation"),
            new UnicodeRange(0x1FA00, 0x1FA0f,"Chess Symbols"),
            new UnicodeRange((uint)(MaxCodePointVal - 16), (uint)MaxCodePointVal,"End"),
            new UnicodeRange (0x0020 ,0x007F ,"Basic Latin"),
            new UnicodeRange (0x00A0 ,0x00FF ,"Latin-1 Supplement"),
            new UnicodeRange (0x0100 ,0x017F ,"Latin Extended-A"),
            new UnicodeRange (0x0180 ,0x024F ,"Latin Extended-B"),
            new UnicodeRange (0x0250 ,0x02AF ,"IPA Extensions"),
            new UnicodeRange (0x02B0 ,0x02FF ,"Spacing Modifier Letters"),
            new UnicodeRange (0x0300 ,0x036F ,"Combining Diacritical Marks"),
            new UnicodeRange (0x0370 ,0x03FF ,"Greek and Coptic"),
            new UnicodeRange (0x0400 ,0x04FF ,"Cyrillic"),
            new UnicodeRange (0x0500 ,0x052F ,"Cyrillic Supplementary"),
            new UnicodeRange (0x0530 ,0x058F ,"Armenian"),
            new UnicodeRange (0x0590 ,0x05FF ,"Hebrew"),
            new UnicodeRange (0x0600 ,0x06FF ,"Arabic"),
            new UnicodeRange (0x0700 ,0x074F ,"Syriac"),
            new UnicodeRange (0x0780 ,0x07BF ,"Thaana"),
            new UnicodeRange (0x0900 ,0x097F ,"Devanagari"),
            new UnicodeRange (0x0980 ,0x09FF ,"Bengali"),
            new UnicodeRange (0x0A00 ,0x0A7F ,"Gurmukhi"),
            new UnicodeRange (0x0A80 ,0x0AFF ,"Gujarati"),
            new UnicodeRange (0x0B00 ,0x0B7F ,"Oriya"),
            new UnicodeRange (0x0B80 ,0x0BFF ,"Tamil"),
            new UnicodeRange (0x0C00 ,0x0C7F ,"Telugu"),
            new UnicodeRange (0x0C80 ,0x0CFF ,"Kannada"),
            new UnicodeRange (0x0D00 ,0x0D7F ,"Malayalam"),
            new UnicodeRange (0x0D80 ,0x0DFF ,"Sinhala"),
            new UnicodeRange (0x0E00 ,0x0E7F ,"Thai"),
            new UnicodeRange (0x0E80 ,0x0EFF ,"Lao"),
            new UnicodeRange (0x0F00 ,0x0FFF ,"Tibetan"),
            new UnicodeRange (0x1000 ,0x109F ,"Myanmar"),
            new UnicodeRange (0x10A0 ,0x10FF ,"Georgian"),
            new UnicodeRange (0x1100 ,0x11FF ,"Hangul Jamo"),
            new UnicodeRange (0x1200 ,0x137F ,"Ethiopic"),
            new UnicodeRange (0x13A0 ,0x13FF ,"Cherokee"),
            new UnicodeRange (0x1400 ,0x167F ,"Unified Canadian Aboriginal Syllabics"),
            new UnicodeRange (0x1680 ,0x169F ,"Ogham"),
            new UnicodeRange (0x16A0 ,0x16FF ,"Runic"),
            new UnicodeRange (0x1700 ,0x171F ,"Tagalog"),
            new UnicodeRange (0x1720 ,0x173F ,"Hanunoo"),
            new UnicodeRange (0x1740 ,0x175F ,"Buhid"),
            new UnicodeRange (0x1760 ,0x177F ,"Tagbanwa"),
            new UnicodeRange (0x1780 ,0x17FF ,"Khmer"),
            new UnicodeRange (0x1800 ,0x18AF ,"Mongolian"),
            new UnicodeRange (0x1900 ,0x194F ,"Limbu"),
            new UnicodeRange (0x1950 ,0x197F ,"Tai Le"),
            new UnicodeRange (0x19E0 ,0x19FF ,"Khmer Symbols"),
            new UnicodeRange (0x1D00 ,0x1D7F ,"Phonetic Extensions"),
            new UnicodeRange (0x1E00 ,0x1EFF ,"Latin Extended Additional"),
            new UnicodeRange (0x1F00 ,0x1FFF ,"Greek Extended"),
            new UnicodeRange (0x2000 ,0x206F ,"General Punctuation"),
            new UnicodeRange (0x2070 ,0x209F ,"Superscripts and Subscripts"),
            new UnicodeRange (0x20A0 ,0x20CF ,"Currency Symbols"),
            new UnicodeRange (0x20D0 ,0x20FF ,"Combining Diacritical Marks for Symbols"),
            new UnicodeRange (0x2100 ,0x214F ,"Letterlike Symbols"),
            new UnicodeRange (0x2150 ,0x218F ,"Number Forms"),
            new UnicodeRange (0x2190 ,0x21FF ,"Arrows"),
            new UnicodeRange (0x2200 ,0x22FF ,"Mathematical Operators"),
            new UnicodeRange (0x2300 ,0x23FF ,"Miscellaneous Technical"),
            new UnicodeRange (0x2400 ,0x243F ,"Control Pictures"),
            new UnicodeRange (0x2440 ,0x245F ,"Optical Character Recognition"),
            new UnicodeRange (0x2460 ,0x24FF ,"Enclosed Alphanumerics"),
            new UnicodeRange (0x2500 ,0x257F ,"Box Drawing"),
            new UnicodeRange (0x2580 ,0x259F ,"Block Elements"),
            new UnicodeRange (0x25A0 ,0x25FF ,"Geometric Shapes"),
            new UnicodeRange (0x2600 ,0x26FF ,"Miscellaneous Symbols"),
            new UnicodeRange (0x2700 ,0x27BF ,"Dingbats"),
            new UnicodeRange (0x27C0 ,0x27EF ,"Miscellaneous Mathematical Symbols-A"),
            new UnicodeRange (0x27F0 ,0x27FF ,"Supplemental Arrows-A"),
            new UnicodeRange (0x2800 ,0x28FF ,"Braille Patterns"),
            new UnicodeRange (0x2900 ,0x297F ,"Supplemental Arrows-B"),
            new UnicodeRange (0x2980 ,0x29FF ,"Miscellaneous Mathematical Symbols-B"),
            new UnicodeRange (0x2A00 ,0x2AFF ,"Supplemental Mathematical Operators"),
            new UnicodeRange (0x2B00 ,0x2BFF ,"Miscellaneous Symbols and Arrows"),
            new UnicodeRange (0x2E80 ,0x2EFF ,"CJK Radicals Supplement"),
            new UnicodeRange (0x2F00 ,0x2FDF ,"Kangxi Radicals"),
            new UnicodeRange (0x2FF0 ,0x2FFF ,"Ideographic Description Characters"),
            new UnicodeRange (0x3000 ,0x303F ,"CJK Symbols and Punctuation"),
            new UnicodeRange (0x3040 ,0x309F ,"Hiragana"),
            new UnicodeRange (0x30A0 ,0x30FF ,"Katakana"),
            new UnicodeRange (0x3100 ,0x312F ,"Bopomofo"),
            new UnicodeRange (0x3130 ,0x318F ,"Hangul Compatibility Jamo"),
            new UnicodeRange (0x3190 ,0x319F ,"Kanbun"),
            new UnicodeRange (0x31A0 ,0x31BF ,"Bopomofo Extended"),
            new UnicodeRange (0x31F0 ,0x31FF ,"Katakana Phonetic Extensions"),
            new UnicodeRange (0x3200 ,0x32FF ,"Enclosed CJK Letters and Months"),
            new UnicodeRange (0x3300 ,0x33FF ,"CJK Compatibility"),
            new UnicodeRange (0x3400 ,0x4DBF ,"CJK Unified Ideographs Extension A"),
            new UnicodeRange (0x4DC0 ,0x4DFF ,"Yijing Hexagram Symbols"),
            new UnicodeRange (0x4E00 ,0x9FFF ,"CJK Unified Ideographs"),
            new UnicodeRange (0xA000 ,0xA48F ,"Yi Syllables"),
            new UnicodeRange (0xA490 ,0xA4CF ,"Yi Radicals"),
            new UnicodeRange (0xAC00 ,0xD7AF ,"Hangul Syllables"),
            new UnicodeRange (0xD800 ,0xDB7F ,"High Surrogates"),
            new UnicodeRange (0xDB80 ,0xDBFF ,"High Private Use Surrogates"),
            new UnicodeRange (0xDC00 ,0xDFFF ,"Low Surrogates"),
            new UnicodeRange (0xE000 ,0xF8FF ,"Private Use Area"),
            new UnicodeRange (0xF900 ,0xFAFF ,"CJK Compatibility Ideographs"),
            new UnicodeRange (0xFB00 ,0xFB4F ,"Alphabetic Presentation Forms"),
            new UnicodeRange (0xFB50 ,0xFDFF ,"Arabic Presentation Forms-A"),
            new UnicodeRange (0xFE00 ,0xFE0F ,"Variation Selectors"),
            new UnicodeRange (0xFE20 ,0xFE2F ,"Combining Half Marks"),
            new UnicodeRange (0xFE30 ,0xFE4F ,"CJK Compatibility Forms"),
            new UnicodeRange (0xFE50 ,0xFE6F ,"Small Form Variants"),
            new UnicodeRange (0xFE70 ,0xFEFF ,"Arabic Presentation Forms-B"),
            new UnicodeRange (0xFF00 ,0xFFEF ,"Halfwidth and Fullwidth Forms"),
            new UnicodeRange (0xFFF0 ,0xFFFF ,"Specials"),
            new UnicodeRange (0x10000, 0x1007F ,"Linear B Syllabary"),
            new UnicodeRange (0x10080, 0x100FF ,"Linear B Ideograms"),
            new UnicodeRange (0x10100, 0x1013F ,"Aegean Numbers"),
            new UnicodeRange (0x10300, 0x1032F ,"Old Italic"),
            new UnicodeRange (0x10330, 0x1034F ,"Gothic"),
            new UnicodeRange (0x10380, 0x1039F ,"Ugaritic"),
            new UnicodeRange (0x10400, 0x1044F ,"Deseret"),
            new UnicodeRange (0x10450, 0x1047F ,"Shavian"),
            new UnicodeRange (0x10480, 0x104AF ,"Osmanya"),
            new UnicodeRange (0x10800, 0x1083F ,"Cypriot Syllabary"),
            new UnicodeRange (0x1D000, 0x1D0FF ,"Byzantine Musical Symbols"),
            new UnicodeRange (0x1D100, 0x1D1FF ,"Musical Symbols"),
            new UnicodeRange (0x1D300, 0x1D35F ,"Tai Xuan Jing Symbols"),
            new UnicodeRange (0x1D400, 0x1D7FF ,"Mathematical Alphanumeric Symbols"),
            new UnicodeRange (0x1F600, 0x1F532 ,"Emojis Symbols"),
            new UnicodeRange (0x20000, 0x2A6DF ,"CJK Unified Ideographs Extension B"),
            new UnicodeRange (0x2F800, 0x2FA1F ,"CJK Compatibility Ideographs Supplement"),
            new UnicodeRange (0xE0000, 0xE007F ,"Tags"),
        };

        public new string ToString()
        {
            return M_TableView.ToString();
        }

        public new Toplevel GetTopSuperView()
        {
            return Utils.RevertEqualsObj(M_TableView.GetTopSuperView());
        }

        public int SelectedColumn
        {
            get { return M_TableView.SelectedColumn; }
            set { M_TableView.SelectedColumn = value; }
        }

        public int SelectedRow
        {
            get { return M_TableView.SelectedRow; }
            set { M_TableView.SelectedRow = value; }
        }

        public bool FullRowSelect
        {
            get { return M_TableView.FullRowSelect; }
            set { M_TableView.FullRowSelect = value; }
        }

        public int CellActivationKey
        {
            get { return (int)M_TableView.CellActivationKey; }
            set { M_TableView.CellActivationKey = (Terminal.Gui.Key)value; }
        }

        public int MaxCellWidth
        {
            get { return M_TableView.MaxCellWidth; }
            set { M_TableView.MaxCellWidth = value; }
        }

        public bool MultiSelect
        {
            get { return M_TableView.MultiSelect; }
            set { M_TableView.MultiSelect = value; }
        }

        public string SeparatorSymbol
        {
            get { return M_TableView.SeparatorSymbol.ToString(); }
            set { M_TableView.SeparatorSymbol = value.ToCharArray()[0]; }
        }

        public string NullSymbol
        {
            get { return M_TableView.NullSymbol; }
            set { M_TableView.NullSymbol = value; }
        }

        public int ColumnOffset
        {
            get { return M_TableView.ColumnOffset; }
            set { M_TableView.ColumnOffset = value; }
        }

        public int RowOffset
        {
            get { return M_TableView.RowOffset; }
            set { M_TableView.RowOffset = value; }
        }

        public void SelectAll()
        {
            M_TableView.SelectAll();
        }

        public void ChangeSelectionByOffset(int p1, int p2, bool p3)
        {
            M_TableView.ChangeSelectionByOffset(p1, p2, p3);
        }

        public void ChangeSelectionToEndOfRow(bool p1)
        {
            M_TableView.ChangeSelectionToEndOfRow(p1);
        }

        public void ChangeSelectionToEndOfTable(bool p1)
        {
            M_TableView.ChangeSelectionToEndOfTable(p1);
        }

        public void ChangeSelectionToStartOfRow(bool p1)
        {
            M_TableView.ChangeSelectionToStartOfRow(p1);
        }

        public void ChangeSelectionToStartOfTable(bool p1)
        {
            M_TableView.ChangeSelectionToStartOfTable(p1);
        }

        public void EnsureValidSelection()
        {
            M_TableView.EnsureValidSelection();
        }

        [ContextMethod("ОбеспечитьДопустимыеСмещенияПрокрутки", "EnsureValidScrollOffsets")]
        public void EnsureValidScrollOffsets()
        {
            M_TableView.EnsureValidScrollOffsets();
        }

        [ContextMethod("ОбеспечитьОтображениеВыбранной", "EnsureSelectedCellIsVisible")]
        public void EnsureSelectedCellIsVisible()
        {
            M_TableView.EnsureSelectedCellIsVisible();
        }

        public ArrayImpl GetAllSelectedCells()
        {
            return Utils.GetAllSelectedCells(M_TableView.GetAllSelectedCells());
        }

        public void PageUp(bool p1)
        {
            M_TableView.PageUp(p1);
        }

        public void PageDown(bool p1)
        {
            M_TableView.PageDown(p1);
        }

        public ostgui.Point CellToScreen(int p1, int p2)
        {
            try
            {
                return new Point((Terminal.Gui.Point)M_TableView.CellToScreen(p1, p2));
            }
            catch
            {
                return null;
            }
        }

        public void SetSelection(int p1, int p2, bool p3)
        {
            M_TableView.SetSelection(p1, p2, p3);
        }

        public TableStyle Style
        {
            get { return Utils.RevertEqualsObj(M_TableView.Style); }
            set { M_TableView.Style = value.M_TableStyle; }
        }

        public DataTable Table
        {
            get { return Utils.RevertEqualsObj(M_TableView.Table); }
            set { M_TableView.Table = value.M_DataTable; }
        }

        public Point ScreenToCell(int p1, int p2)
        {
            try
            {
                dynamic point = M_TableView.ScreenToCell(p1, p2);
                return new Point(point.X, point.Y);
            }
            catch
            {
                return null;
            }
        }

        public bool IsSelected(int p1, int p2)
        {
            return M_TableView.IsSelected(p1, p2);
        }

        public ArrayImpl MultiSelectedRegions
        {
            get { return Utils.MultiSelectedRegions(M_TableView.MultiSelectedRegions); }
        }
    }

    [ContextClass("ТфТаблица", "TfTableView")]
    public class TfTableView : AutoContext<TfTableView>
    {

        public TfTableView()
        {
            TableView TableView1 = new TableView();
            TableView1.dll_obj = this;
            Base_obj = TableView1;
        }

        public TableView Base_obj;

        [ContextProperty("Верх", "Top")]
        public TfPos Top
        {
            get { return new TfPos(Base_obj.Top); }
        }

        [ContextProperty("ВыбраннаяКолонка", "SelectedColumn")]
        public int SelectedColumn
        {
            get { return Base_obj.SelectedColumn; }
            set { Base_obj.SelectedColumn = value; }
        }

        [ContextProperty("ВыбраннаяСтрока", "SelectedRow")]
        public int SelectedRow
        {
            get { return Base_obj.SelectedRow; }
            set { Base_obj.SelectedRow = value; }
        }

        [ContextProperty("ВыбранныеРегионы", "MultiSelectedRegions")]
        public IValue MultiSelectedRegions
        {
            get { return Base_obj.MultiSelectedRegions; }
        }

        [ContextProperty("ВыделениеСтроки", "FullRowSelect")]
        public bool FullRowSelect
        {
            get { return Base_obj.FullRowSelect; }
            set { Base_obj.FullRowSelect = value; }
        }

        [ContextProperty("Высота", "Height")]
        public IValue Height
        {
            get { return new TfDim().Height(this); }
            set
            {
                if (Utils.IsType<TfDim>(value))
                {
                    Base_obj.M_View.Height = ((TfDim)value).Base_obj.M_Dim;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_View.Height = Terminal.Gui.Dim.Sized(Utils.ToInt32(value));
                }
            }
        }

        [ContextProperty("Границы", "Bounds")]
        public TfRect Bounds
        {
            get
            {
                Terminal.Gui.Rect bounds = Base_obj.Bounds.M_Rect;
                return new TfRect(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            }
        }

        [ContextProperty("Данные", "Data")]
        public IValue Data
        {
            get { return Utils.RevertObj(Base_obj.Data); }
            set { Base_obj.Data = value; }
        }

        [ContextProperty("Добавлено", "IsAdded")]
        public bool IsAdded
        {
            get { return Base_obj.IsAdded; }
        }

        [ContextProperty("Доступность", "Enabled")]
        public bool Enabled
        {
            get { return Base_obj.Enabled; }
            set { Base_obj.Enabled = value; }
        }

        [ContextProperty("Игрек", "Y")]
        public IValue Y
        {
            get { return new TfPos(Base_obj.Y); }
            set
            {
                if (Utils.IsType<TfPos>(value))
                {
                    Base_obj.M_TableView.Y = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_TableView.Y = Terminal.Gui.Pos.At(Utils.ToInt32(value));
                }
            }
        }

        [ContextProperty("Идентификатор", "Id")]
        public string Id
        {
            get { return Base_obj.Id; }
            set { Base_obj.Id = value; }
        }

        [ContextProperty("Икс", "X")]
        public IValue X
        {
            get { return new TfPos(Base_obj.X); }
            set
            {
                if (Utils.IsType<TfPos>(value))
                {
                    Base_obj.M_TableView.X = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_TableView.X = Terminal.Gui.Pos.At(Utils.ToInt32(value));
                }
            }
        }

        [ContextProperty("Кадр", "Frame")]
        public TfRect Frame
        {
            get
            {
                Terminal.Gui.Rect frame = Base_obj.Frame.M_Rect;
                return new TfRect(frame.X, frame.Y, frame.Width, frame.Height);
            }
            set { Base_obj.Frame = value.Base_obj; }
        }

        [ContextProperty("КлавишаАктивацииЯчейки", "CellActivationKey")]
        public int CellActivationKey
        {
            get { return Base_obj.CellActivationKey; }
            set { Base_obj.CellActivationKey = value; }
        }

        [ContextProperty("Лево", "Left")]
        public TfPos Left
        {
            get { return new TfPos(Base_obj.Left); }
        }

        [ContextProperty("МаксимальнаяШиринаЯчейки", "MaxCellWidth")]
        public int MaxCellWidth
        {
            get { return Base_obj.MaxCellWidth; }
            set { Base_obj.MaxCellWidth = value; }
        }

        [ContextProperty("Метка", "Tag")]
        public IValue Tag
        {
            get { return Base_obj.Tag; }
            set { Base_obj.Tag = value; }
        }

        [ContextProperty("МножественныйВыбор", "MultiSelect")]
        public bool MultiSelect
        {
            get { return Base_obj.MultiSelect; }
            set { Base_obj.MultiSelect = value; }
        }

        [ContextProperty("Низ", "Bottom")]
        public TfPos Bottom
        {
            get { return new TfPos(Base_obj.Bottom); }
        }

        [ContextProperty("Отображать", "Visible")]
        public bool Visible
        {
            get { return Base_obj.Visible; }
            set { Base_obj.Visible = value; }
        }

        [ContextProperty("ПорядокОбхода", "TabIndex")]
        public int TabIndex
        {
            get { return Base_obj.TabIndex; }
            set { Base_obj.TabIndex = value; }
        }

        [ContextProperty("Право", "Right")]
        public TfPos Right
        {
            get { return new TfPos(Base_obj.Right); }
        }

        [ContextProperty("Разделитель", "SeparatorSymbol")]
        public string SeparatorSymbol
        {
            get { return Base_obj.SeparatorSymbol; }
            set { Base_obj.SeparatorSymbol = value; }
        }

        [ContextProperty("Родитель", "SuperView")]
        public IValue SuperView
        {
            get
            {
                if (Base_obj.M_TableView.SuperView.GetType().ToString().Contains("+ContentView"))
                {
                    return Utils.RevertEqualsObj(Base_obj.M_TableView.SuperView.SuperView).dll_obj;
                }
                return Utils.RevertEqualsObj(Base_obj.M_TableView.SuperView).dll_obj;
            }
        }

        [ContextProperty("СимволНеопределено", "NullSymbol")]
        public string NullSymbol
        {
            get { return Base_obj.NullSymbol; }
            set { Base_obj.NullSymbol = value; }
        }

        [ContextProperty("СмещениеКолонки", "ColumnOffset")]
        public int ColumnOffset
        {
            get { return Base_obj.ColumnOffset; }
            set { Base_obj.ColumnOffset = value; }
        }

        [ContextProperty("СмещениеСтроки", "RowOffset")]
        public int RowOffset
        {
            get { return Base_obj.RowOffset; }
            set { Base_obj.RowOffset = value; }
        }

        private bool nativeCellActivated = true;
        [ContextProperty("СтандартнаяАктивацияЯчейки", "NativeCellActivated")]
        public bool NativeCellActivated
        {
            get { return nativeCellActivated; }
            set { nativeCellActivated = value; }
        }

        [ContextProperty("СтильКомпоновки", "LayoutStyle")]
        public int LayoutStyle
        {
            get { return Base_obj.LayoutStyle; }
            set { Base_obj.LayoutStyle = value; }
        }

        [ContextProperty("СтильТаблицы", "Style")]
        public TfTableStyle Style
        {
            get { return Base_obj.Style.dll_obj; }
            set { Base_obj.Style = value.Base_obj; }
        }

        [ContextProperty("Сфокусирован", "HasFocus")]
        public bool HasFocus
        {
            get { return Base_obj.HasFocus; }
        }

        [ContextProperty("ТаблицаДанных", "Table")]
        public TfDataTable Table
        {
            get { return Base_obj.Table.dll_obj; }
            set { Base_obj.Table = value.Base_obj; }
        }

        [ContextProperty("ТабФокус", "TabStop")]
        public bool TabStop
        {
            get { return Base_obj.TabStop; }
            set { Base_obj.TabStop = value; }
        }

        [ContextProperty("ТекущийСверху", "IsCurrentTop")]
        public bool IsCurrentTop
        {
            get { return Base_obj.IsCurrentTop; }
        }

        [ContextProperty("Фокусируемый", "CanFocus")]
        public bool CanFocus
        {
            get { return Base_obj.CanFocus; }
            set { Base_obj.CanFocus = value; }
        }

        private TfColorScheme colorScheme;
        [ContextProperty("ЦветоваяСхема", "ColorScheme")]
        public TfColorScheme ColorScheme
        {
            get { return colorScheme; }
            set
            {
                colorScheme = new TfColorScheme();
                Terminal.Gui.ColorScheme _colorScheme = value.Base_obj.M_ColorScheme;
                colorScheme.Base_obj.M_ColorScheme.Disabled = _colorScheme.Disabled;
                colorScheme.Base_obj.M_ColorScheme.Focus = _colorScheme.Focus;
                colorScheme.Base_obj.M_ColorScheme.HotFocus = _colorScheme.HotFocus;
                colorScheme.Base_obj.M_ColorScheme.HotNormal = _colorScheme.HotNormal;
                colorScheme.Base_obj.M_ColorScheme.Normal = _colorScheme.Normal;
                Base_obj.ColorScheme = colorScheme.Base_obj;
            }
        }

        [ContextProperty("Ширина", "Width")]
        public IValue Width
        {
            get { return new TfDim().Width(this); }
            set
            {
                if (Utils.IsType<TfDim>(value))
                {
                    Base_obj.M_View.Width = ((TfDim)value).Base_obj.M_Dim;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_View.Width = Terminal.Gui.Dim.Sized(Utils.ToInt32(value));
                }
            }
        }

        [ContextProperty("КлавишаНажата", "KeyPress")]
        public TfAction KeyPress { get; set; }

        [ContextProperty("МышьНадЭлементом", "MouseEnter")]
        public TfAction MouseEnter { get; set; }

        [ContextProperty("МышьПокинулаЭлемент", "MouseLeave")]
        public TfAction MouseLeave { get; set; }

        [ContextProperty("ПриАктивацииЯчейки", "CellActivated")]
        public TfAction CellActivated { get; set; }

        [ContextProperty("ПриВходе", "Enter")]
        public TfAction Enter { get; set; }

        [ContextProperty("ПриНажатииМыши", "MouseClick")]
        public TfAction MouseClick { get; set; }

        [ContextProperty("ПриУходе", "Leave")]
        public TfAction Leave { get; set; }

        [ContextProperty("СочетаниеКлавишДействие", "ShortcutAction")]
        public TfAction ShortcutAction { get; set; }

        [ContextProperty("ЯчейкаИзменена", "SelectedCellChanged")]
        public TfAction SelectedCellChanged { get; set; }

        [ContextMethod("ВерхнийРодитель", "GetTopSuperView")]
        public IValue GetTopSuperView()
        {
            return Base_obj.GetTopSuperView().dll_obj;
        }

        [ContextMethod("ВСтроку", "ToString")]
        public new string ToString()
        {
            return Base_obj.ToString();
        }

        [ContextMethod("Выделена", "IsSelected")]
        public bool IsSelected(int p1, int p2)
        {
            return Base_obj.IsSelected(p1, p2);
        }

        [ContextMethod("ВыделитьВсе", "SelectAll")]
        public void SelectAll()
        {
            Base_obj.SelectAll();
        }

        [ContextMethod("Выше", "PlaceTop")]
        public void PlaceTop(IValue p1, int p2)
        {
            Base_obj.PlaceTop(((dynamic)p1.AsObject()).Base_obj, p2);
        }

        [ContextMethod("ВышеЛевее", "PlaceTopLeft")]
        public void PlaceTopLeft(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceTopLeft(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("ВышеПравее", "PlaceTopRight")]
        public void PlaceTopRight(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceTopRight(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("ДобавитьСочетаниеКлавиш", "AddShortcut")]
        public void AddShortcut(decimal p1)
        {
            Utils.AddToShortcutDictionary(p1, this);
        }

        [ContextMethod("Заполнить", "Fill")]
        public void Fill(int p1 = 0, int p2 = 0)
        {
            Base_obj.Fill(p1, p2);
        }

        [ContextMethod("ИзменитьВыделенное", "ChangeSelectionByOffset")]
        public void ChangeSelectionByOffset(int p1, int p2, bool p3)
        {
            Base_obj.ChangeSelectionByOffset(p1, p2, p3);
        }

        [ContextMethod("ИзменитьВыделенноеДоКонцаСтроки", "ChangeSelectionToEndOfRow")]
        public void ChangeSelectionToEndOfRow(bool p1)
        {
            Base_obj.ChangeSelectionToEndOfRow(p1);
        }

        [ContextMethod("ИзменитьВыделенноеДоКонцаТаблицы", "ChangeSelectionToEndOfTable")]
        public void ChangeSelectionToEndOfTable(bool p1)
        {
            Base_obj.ChangeSelectionToEndOfTable(p1);
        }

        [ContextMethod("ИзменитьВыделенноеДоНачалаСтроки", "ChangeSelectionToStartOfRow")]
        public void ChangeSelectionToStartOfRow(bool p1)
        {
            Base_obj.ChangeSelectionToStartOfRow(p1);
        }

        [ContextMethod("ИзменитьВыделенноеДоНачалаТаблицы", "ChangeSelectionToStartOfTable")]
        public void ChangeSelectionToStartOfTable(bool p1)
        {
            Base_obj.ChangeSelectionToStartOfTable(p1);
        }

        [ContextMethod("Левее", "PlaceLeft")]
        public void PlaceLeft(IValue p1, int p2)
        {
            Base_obj.PlaceLeft(((dynamic)p1.AsObject()).Base_obj, p2);
        }

        [ContextMethod("ЛевееВыше", "PlaceLeftTop")]
        public void PlaceLeftTop(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceLeftTop(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("ЛевееНиже", "PlaceLeftBottom")]
        public void PlaceLeftBottom(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceLeftBottom(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("Ниже", "PlaceBottom")]
        public void PlaceBottom(IValue p1, int p2)
        {
            Base_obj.PlaceBottom(((dynamic)p1.AsObject()).Base_obj, p2);
        }

        [ContextMethod("НижеЛевее", "PlaceBottomLeft")]
        public void PlaceBottomLeft(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceBottomLeft(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("НижеПравее", "PlaceBottomRight")]
        public void PlaceBottomRight(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceBottomRight(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("ОбеспечитьОтображениеВыбранной", "EnsureSelectedCellIsVisible")]
        public void EnsureSelectedCellIsVisible()
        {
            Base_obj.EnsureSelectedCellIsVisible();
        }

        [ContextMethod("ПолучитьВсеВыбранныеЯчейки", "GetAllSelectedCells")]
        public ArrayImpl GetAllSelectedCells()
        {
            return Base_obj.GetAllSelectedCells();
        }

        [ContextMethod("ПолучитьСочетаниеКлавиш", "GetShortcut")]
        public ValueListImpl GetShortcut()
        {
            ValueListImpl ValueListImpl1 = new ValueListImpl();
            ArrayList ArrayList1 = Utils.GetFromShortcutDictionary(this);
            for (int i = 0; i < ArrayList1.Count; i++)
            {
                decimal shortcut = (decimal)ArrayList1[i];
                ValueListImpl1.Add(ValueFactory.Create(shortcut), OneScriptTerminalGui.instance.Keys.NameEn(shortcut));
            }
            if (ValueListImpl1.Count() > 0)
            {
                return ValueListImpl1;
            }
            return null;
        }

        [ContextMethod("Правее", "PlaceRight")]
        public void PlaceRight(IValue p1, int p2)
        {
            Base_obj.PlaceRight(((dynamic)p1.AsObject()).Base_obj, p2);
        }

        [ContextMethod("ПравееВыше", "PlaceRightTop")]
        public void PlaceRightTop(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceRightTop(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("ПравееНиже", "PlaceRightBottom")]
        public void PlaceRightBottom(IValue p1, int p2, int p3)
        {
            Base_obj.PlaceRightBottom(((dynamic)p1.AsObject()).Base_obj, p2, p3);
        }

        [ContextMethod("СтраницаВверх", "PageUp")]
        public void PageUp(bool p1)
        {
            Base_obj.PageUp(p1);
        }

        [ContextMethod("СтраницаВниз", "PageDown")]
        public void PageDown(bool p1)
        {
            Base_obj.PageDown(p1);
        }

        [ContextMethod("ТочкаНаЭлементе", "ScreenToView")]
        public TfPoint ScreenToView(int p1, int p2)
        {
            return new TfPoint(Base_obj.ScreenToView(p1, p2));
        }

        [ContextMethod("ТочкаНаЯчейке", "CellToScreen")]
        public TfPoint CellToScreen(int p1, int p2)
        {
            try
            {
                return new TfPoint(Base_obj.CellToScreen(p1, p2));
            }
            catch
            {
                return null;
            }
        }

        [ContextMethod("УдалитьСочетаниеКлавиш", "RemoveShortcut")]
        public void RemoveShortcut(decimal p1)
        {
            Utils.RemoveFromShortcutDictionary(p1, this);
        }

        [ContextMethod("УстановитьВыделение", "SetSelection")]
        public void SetSelection(int p1, int p2, bool p3)
        {
            Base_obj.SetSelection(p1, p2, p3);
        }

        [ContextMethod("УстановитьФокус", "SetFocus")]
        public void SetFocus()
        {
            Base_obj.SetFocus();
        }

        [ContextMethod("ЦветВыделенного", "GetHotNormalColor")]
        public TfAttribute GetHotNormalColor()
        {
            return new TfAttribute(Base_obj.GetHotNormalColor());
        }

        [ContextMethod("ЦветОбычного", "GetNormalColor")]
        public TfAttribute GetNormalColor()
        {
            return new TfAttribute(Base_obj.GetNormalColor());
        }

        [ContextMethod("ЦветФокуса", "GetFocusColor")]
        public TfAttribute GetFocusColor()
        {
            return new TfAttribute(Base_obj.GetFocusColor());
        }

        [ContextMethod("Центр", "Center")]
        public void Center(int p1 = 0, int p2 = 0)
        {
            Base_obj.Center(p1, p2);
        }

        [ContextMethod("ЯчейкаНаТочке", "ScreenToCell")]
        public TfPoint ScreenToCell(int p1, int p2)
        {
            try
            {
                return new TfPoint(Base_obj.ScreenToCell(p1, p2));
            }
            catch
            {
                return null;
            }
        }

    }
}
