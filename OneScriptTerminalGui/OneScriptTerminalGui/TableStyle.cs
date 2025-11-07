using ScriptEngine.Machine.Contexts;
using ScriptEngine.HostedScript.Library;

namespace ostgui
{
    public class TableStyle
    {
        public TfTableStyle dll_obj;
        public Terminal.Gui.TableView.TableStyle M_TableStyle;

        public TableStyle()
        {
            M_TableStyle = new Terminal.Gui.TableView.TableStyle();
            Utils.AddToHashtable(M_TableStyle, this);
        }

        public new string ToString()
        {
            return M_TableStyle.ToString();
        }

        public bool ShowHorizontalScrollIndicators
        {
            get { return M_TableStyle.ShowHorizontalScrollIndicators; }
            set { M_TableStyle.ShowHorizontalScrollIndicators = value; }
        }

        public bool InvertSelectedCellFirstCharacter
        {
            get { return M_TableStyle.InvertSelectedCellFirstCharacter; }
            set { M_TableStyle.InvertSelectedCellFirstCharacter = value; }
        }

        public bool ShowVerticalHeaderLines
        {
            get { return M_TableStyle.ShowVerticalHeaderLines; }
            set { M_TableStyle.ShowVerticalHeaderLines = value; }
        }

        public bool ShowVerticalCellLines
        {
            get { return M_TableStyle.ShowVerticalCellLines; }
            set { M_TableStyle.ShowVerticalCellLines = value; }
        }

        public bool ShowHorizontalHeaderOverline
        {
            get { return M_TableStyle.ShowHorizontalHeaderOverline; }
            set { M_TableStyle.ShowHorizontalHeaderOverline = value; }
        }

        public bool ShowHorizontalHeaderUnderline
        {
            get { return M_TableStyle.ShowHorizontalHeaderUnderline; }
            set { M_TableStyle.ShowHorizontalHeaderUnderline = value; }
        }

        public bool SmoothHorizontalScrolling
        {
            get { return M_TableStyle.SmoothHorizontalScrolling; }
            set { M_TableStyle.SmoothHorizontalScrolling = value; }
        }

        public bool ExpandLastColumn
        {
            get { return M_TableStyle.ExpandLastColumn; }
            set { M_TableStyle.ExpandLastColumn = value; }
        }

        public bool AlwaysShowHeaders
        {
            get { return M_TableStyle.AlwaysShowHeaders; }
            set { M_TableStyle.AlwaysShowHeaders = value; }
        }

        public ColumnStyles ColumnStyles
        {
            get { return Utils.RevertEqualsObj(M_TableStyle.ColumnStyles); }
            set { M_TableStyle.ColumnStyles = value.M_ColumnStyles; }
        }
    }

    [ContextClass("ТфСтильТаблицы", "TfTableStyle")]
    public class TfTableStyle : AutoContext<TfTableStyle>
    {

        public TfTableStyle()
        {
            TableStyle TableStyle1 = new TableStyle();
            TableStyle1.dll_obj = this;
            Base_obj = TableStyle1;
        }

        public TableStyle Base_obj;

        [ContextProperty("ГоризонтальныеСтрелки", "ShowHorizontalScrollIndicators")]
        public bool ShowHorizontalScrollIndicators
        {
            get { return Base_obj.ShowHorizontalScrollIndicators; }
            set { Base_obj.ShowHorizontalScrollIndicators = value; }
        }

        [ContextProperty("ИнвертироватьПервыйСимвол", "InvertSelectedCellFirstCharacter")]
        public bool InvertSelectedCellFirstCharacter
        {
            get { return Base_obj.InvertSelectedCellFirstCharacter; }
            set { Base_obj.InvertSelectedCellFirstCharacter = value; }
        }

        [ContextProperty("ЛинияМеждуЗаголовками", "ShowVerticalHeaderLines")]
        public bool ShowVerticalHeaderLines
        {
            get { return Base_obj.ShowVerticalHeaderLines; }
            set { Base_obj.ShowVerticalHeaderLines = value; }
        }

        [ContextProperty("ЛинияМеждуЯчейками", "ShowVerticalCellLines")]
        public bool ShowVerticalCellLines
        {
            get { return Base_obj.ShowVerticalCellLines; }
            set { Base_obj.ShowVerticalCellLines = value; }
        }

        [ContextProperty("ЛинияНадЗаголовками", "ShowHorizontalHeaderOverline")]
        public bool ShowHorizontalHeaderOverline
        {
            get { return Base_obj.ShowHorizontalHeaderOverline; }
            set { Base_obj.ShowHorizontalHeaderOverline = value; }
        }

        [ContextProperty("ЛинияПодЗаголовками", "ShowHorizontalHeaderUnderline")]
        public bool ShowHorizontalHeaderUnderline
        {
            get { return Base_obj.ShowHorizontalHeaderUnderline; }
            set { Base_obj.ShowHorizontalHeaderUnderline = value; }
        }

        [ContextProperty("ПлавнаяГоризонтальнаяПрокрутка", "SmoothHorizontalScrolling")]
        public bool SmoothHorizontalScrolling
        {
            get { return Base_obj.SmoothHorizontalScrolling; }
            set { Base_obj.SmoothHorizontalScrolling = value; }
        }

        [ContextProperty("РасширитьПоследнююКолонку", "ExpandLastColumn")]
        public bool ExpandLastColumn
        {
            get { return Base_obj.ExpandLastColumn; }
            set { Base_obj.ExpandLastColumn = value; }
        }

        [ContextProperty("СтилиКолонки", "ColumnStyles")]
        public TfColumnStyles ColumnStyles
        {
            get { return Base_obj.ColumnStyles.dll_obj; }
            set { Base_obj.ColumnStyles = value.Base_obj; }
        }

        [ContextProperty("ФиксироватьЗаголовки", "AlwaysShowHeaders")]
        public bool AlwaysShowHeaders
        {
            get { return Base_obj.AlwaysShowHeaders; }
            set { Base_obj.AlwaysShowHeaders = value; }
        }

        [ContextMethod("ПолучитьСтильКолонки", "GetColumnStyleIfAny")]
        public TfColumnStyle GetColumnStyleIfAny(TfDataColumn p1)
        {
            try
            {
                return Utils.RevertEqualsObj(this.ColumnStyles.Base_obj[p1.Base_obj.M_DataColumn]).dll_obj;
            }
            catch
            {
                return null;
            }
        }

        [ContextMethod("ЦветДесяткаСтрок", "RowColorByDecimal")]
        public void RowColorByDecimal(
            TfColorScheme p0 = null,
            TfColorScheme p1 = null,
            TfColorScheme p2 = null,
            TfColorScheme p3 = null,
            TfColorScheme p4 = null,
            TfColorScheme p5 = null,
            TfColorScheme p6 = null,
            TfColorScheme p7 = null,
            TfColorScheme p8 = null,
            TfColorScheme p9 = null)
        {
            Base_obj.M_TableStyle.RowColorGetter = (a) =>
            {
                Terminal.Gui.ColorScheme colorScheme = null;
                if (a.RowIndex % 10 == 0)
                {
                    colorScheme = p0 != null ? p0.Base_obj.M_ColorScheme : null;
                }
                else
                {
                    if (a.RowIndex % 10 == 1)
                    {
                        colorScheme = p1 != null ? p1.Base_obj.M_ColorScheme : null;
                    }
                    else
                    {
                        if (a.RowIndex % 10 == 2)
                        {
                            colorScheme = p2 != null ? p2.Base_obj.M_ColorScheme : null;
                        }
                        else
                        {
                            if (a.RowIndex % 10 == 3)
                            {
                                colorScheme = p3 != null ? p3.Base_obj.M_ColorScheme : null;
                            }
                            else
                            {
                                if (a.RowIndex % 10 == 4)
                                {
                                    colorScheme = p4 != null ? p4.Base_obj.M_ColorScheme : null;
                                }
                                else
                                {
                                    if (a.RowIndex % 10 == 5)
                                    {
                                        colorScheme = p5 != null ? p5.Base_obj.M_ColorScheme : null;
                                    }
                                    else
                                    {
                                        if (a.RowIndex % 10 == 6)
                                        {
                                            colorScheme = p6 != null ? p6.Base_obj.M_ColorScheme : null;
                                        }
                                        else
                                        {
                                            if (a.RowIndex % 10 == 7)
                                            {
                                                colorScheme = p7 != null ? p7.Base_obj.M_ColorScheme : null;
                                            }
                                            else
                                            {
                                                if (a.RowIndex % 10 == 8)
                                                {
                                                    colorScheme = p8 != null ? p8.Base_obj.M_ColorScheme : null;
                                                }
                                                else
                                                {
                                                    if (a.RowIndex % 10 == 9)
                                                    {
                                                        colorScheme = p9 != null ? p9.Base_obj.M_ColorScheme : null;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return colorScheme;
            };
        }

        public void SetRowColor(int rowIndex, int index, object[,] array, ref Terminal.Gui.ColorScheme colorScheme)
        {
            if (rowIndex == (int)array[index, 0])
            {
                colorScheme = (TfColorScheme)array[index, 1] != null ? ((TfColorScheme)array[index, 1]).Base_obj.M_ColorScheme : null;
            }
            else
            {
                index++;
                if (index <= ((array.Length / 2) - 1))
                {
                    SetRowColor(rowIndex, index, array, ref colorScheme);
                }
            }
        }

        [ContextMethod("ЦветСтрок", "RowsColor")]
        public void RowsColor(MapImpl p1)
        {
            object[,] array1 = new object[p1.Count(), 2];
            int num = 0;
            foreach (var item in p1)
            {
                array1[num, 0] = Utils.ToInt32(item.Key);
                array1[num, 1] = (TfColorScheme)item.Value;
                num++;
            }
            Base_obj.M_TableStyle.RowColorGetter = (a) =>
            {
                Terminal.Gui.ColorScheme colorScheme = null;
                SetRowColor(a.RowIndex, 0, array1, ref colorScheme);
                return colorScheme;
            };
        }

        [ContextMethod("ЧерезСтрочныйЦветСтрок", "RowColorThroughLine")]
        public void RowColorGetter(int p1, TfColorScheme p2)
        {
            if (p1 > 0)
            {
                Base_obj.M_TableStyle.RowColorGetter = (a) => { return a.RowIndex % p1 == 0 ? p2.Base_obj.M_ColorScheme : null; };
            }
            else
            {
                new TfBalloons().Show("Ошибка в методе 'ЧерезСтрочныйЦветСтрок': Первый параметр должен быть больше ноля.", -1);
            }
        }

    }
}
