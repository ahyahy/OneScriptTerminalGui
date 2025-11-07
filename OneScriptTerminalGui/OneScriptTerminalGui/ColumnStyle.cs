using ScriptEngine.Machine.Contexts;
using ScriptEngine.HostedScript.Library;

namespace ostgui
{
    public class ColumnStyle
    {
        public TfColumnStyle dll_obj;
        public Terminal.Gui.TableView.ColumnStyle M_ColumnStyle;

        public ColumnStyle()
        {
            M_ColumnStyle = new Terminal.Gui.TableView.ColumnStyle();
            Utils.AddToHashtable(M_ColumnStyle, this);
        }

        public new string ToString()
        {
            return M_ColumnStyle.ToString();
        }

        public int TextAlignment
        {
            get { return (int)M_ColumnStyle.Alignment; }
            set { M_ColumnStyle.Alignment = (Terminal.Gui.TextAlignment)value; }
        }

        public int MaxWidth
        {
            get { return M_ColumnStyle.MaxWidth; }
            set { M_ColumnStyle.MaxWidth = value; }
        }

        public int MinWidth
        {
            get { return M_ColumnStyle.MinWidth; }
            set { M_ColumnStyle.MinWidth = value; }
        }

        public int MinAcceptableWidth
        {
            get { return M_ColumnStyle.MinAcceptableWidth; }
            set { M_ColumnStyle.MinAcceptableWidth = value; }
        }

        public bool Visible
        {
            get { return M_ColumnStyle.Visible; }
            set { M_ColumnStyle.Visible = value; }
        }

        public string Format
        {
            get { return M_ColumnStyle.Format; }
            set { M_ColumnStyle.Format = value; }
        }
    }

    [ContextClass("ТфСтильКолонки", "TfColumnStyle")]
    public class TfColumnStyle : AutoContext<TfColumnStyle>
    {

        public TfColumnStyle()
        {
            ColumnStyle ColumnStyle1 = new ColumnStyle();
            ColumnStyle1.dll_obj = this;
            Base_obj = ColumnStyle1;
        }

        public ColumnStyle Base_obj;

        [ContextProperty("ВыравниваниеТекста", "TextAlignment")]
        public int TextAlignment
        {
            get { return Base_obj.TextAlignment; }
            set { Base_obj.TextAlignment = value; }
        }

        [ContextProperty("МаксимальнаяШирина", "MaxWidth")]
        public int MaxWidth
        {
            get { return Base_obj.MaxWidth; }
            set { Base_obj.MaxWidth = value; }
        }

        [ContextProperty("МинимальнаяШирина", "MinWidth")]
        public int MinWidth
        {
            get { return Base_obj.MinWidth; }
            set { Base_obj.MinWidth = value; }
        }

        [ContextProperty("МинимальноДопустимаяШирина", "MinAcceptableWidth")]
        public int MinAcceptableWidth
        {
            get { return Base_obj.MinAcceptableWidth; }
            set { Base_obj.MinAcceptableWidth = value; }
        }

        [ContextProperty("Отображать", "Visible")]
        public bool Visible
        {
            get { return Base_obj.Visible; }
            set { Base_obj.Visible = value; }
        }

        public void SetCellColor(int rowIndex, int index, object[,] array, ref Terminal.Gui.ColorScheme colorScheme)
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
                    SetCellColor(rowIndex, index, array, ref colorScheme);
                }
            }
        }

        [ContextMethod("ЦветЯчеек", "CellsColor")]
        public void CellsColor(MapImpl p1)
        {
            object[,] array1 = new object[p1.Count(), 2];
            int num = 0;
            foreach (var item in p1)
            {
                array1[num, 0] = Utils.ToInt32(item.Key);
                array1[num, 1] = (TfColorScheme)item.Value;
                num++;
            }
            Base_obj.M_ColumnStyle.ColorGetter = (a) =>
            {
                Terminal.Gui.ColorScheme colorScheme = null;
                SetCellColor(a.RowIndex, 0, array1, ref colorScheme);
                return colorScheme;
            };
        }

    }
}
