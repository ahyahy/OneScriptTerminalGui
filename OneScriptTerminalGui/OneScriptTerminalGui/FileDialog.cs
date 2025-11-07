using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library;
using ScriptEngine.HostedScript.Library.ValueList;
using System.Collections;
using Terminal.Gui;

namespace ostgui
{
    public class FileDialog : Dialog
    {
        public new TfFileDialog dll_obj;
        public Terminal.Gui.FileDialog m_FileDialog;

        public Terminal.Gui.FileDialog M_FileDialog
        {
            get { return m_FileDialog; }
            set
            {
                m_FileDialog = value;
                base.M_Dialog = m_FileDialog;
                m_FileDialog.DialogClosed += this.M_FileDialog_DialogClosed;
            }
        }

        private void M_FileDialog_DialogClosed(object sender, DialogEventArgs e)
        {
            dynamic Sender = Utils.RevertEqualsObj(M_FileDialog).dll_obj;
            if (Sender.DialogClosed != null)
            {
                TfEventArgs TfEventArgs1 = new TfEventArgs();
                TfEventArgs1.sender = Sender;
                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.DialogClosed);
                TfEventArgs1.view = Utils.RevertEqualsObj(e.Dialog).dll_obj;
                TfEventArgs1.dialogResult = ValueFactory.Create(e.DialogResult);
                TfEventArgs1.directoryPath = ValueFactory.Create(e.DirectoryPath);
                TfEventArgs1.filePath = ValueFactory.Create(e.FilePath);
                ArrayImpl filePaths = null;
                try
                {
                    filePaths = e.FilePaths;
                }
                catch { }
                TfEventArgs1.filePaths = filePaths;
                OneScriptTerminalGui.Event = TfEventArgs1;
                OneScriptTerminalGui.ExecuteEvent(Sender.DialogClosed);
            }
        }

        public FileDialog()
        {
            M_FileDialog = new Terminal.Gui.FileDialog();
            base.M_Dialog = M_FileDialog;
            Utils.AddToHashtable(M_FileDialog, this);
        }

        public new string ToString()
        {
            return M_FileDialog.ToString();
        }

        public new Toplevel GetTopSuperView()
        {
            return Utils.RevertEqualsObj(M_Dialog.GetTopSuperView());
        }

        public bool AllowsOtherFileTypes
        {
            get { return M_FileDialog.AllowsOtherFileTypes; }
            set { M_FileDialog.AllowsOtherFileTypes = value; }
        }

        private ArrayImpl allowedFileTypes = null;
        public ArrayImpl AllowedFileTypes
        {
            get { return allowedFileTypes; }
            set
            {
                allowedFileTypes = value;
                if (value == null)
                {
                    M_FileDialog.AllowedFileTypes = null;
                }
                else
                {
                    ArrayImpl value1;
                    if (M_FileDialog.GetType() == typeof(Terminal.Gui.SaveDialog))
                    {
                        value1 = new ArrayImpl();
                        for (int i = 0; i < value.Count(); i++)
                        {
                            string item = value.Get(i).AsString();
                            if (!item.Contains(";"))
                            {
                                if (!item.Contains("*"))
                                {
                                    value1.Add(value.Get(i));
                                }
                            }
                        }
                    }
                    else
                    {
                        value1 = value;
                    }

                    string[] arr = new string[value1.Count()];
                    for (int i = 0; i < value1.Count(); i++)
                    {
                        arr[i] = value1.Get(i).ToString();
                    }
                    M_FileDialog.AllowedFileTypes = arr;
                }
            }
        }

        public bool Canceled
        {
            get { return M_FileDialog.Canceled; }
        }

        public string Prompt
        {
            get { return M_FileDialog.Prompt.ToString(); }
            set { M_FileDialog.Prompt = value; }
        }

        public string DirectoryPath
        {
            get { return M_FileDialog.DirectoryPath.ToString(); }
            set { M_FileDialog.DirectoryPath = value; }
        }

        public string FilePath
        {
            get { return M_FileDialog.FilePath.ToString(); }
            set { M_FileDialog.FilePath = value; }
        }

        public bool IsExtensionHidden
        {
            get { return M_FileDialog.IsExtensionHidden; }
            set { M_FileDialog.IsExtensionHidden = value; }
        }

        public bool CanCreateDirectories
        {
            get { return M_FileDialog.CanCreateDirectories; }
            set { M_FileDialog.CanCreateDirectories = value; }
        }

        public string Message
        {
            get { return M_FileDialog.Message.ToString(); }
            set { M_FileDialog.Message = value; }
        }
    }

    [ContextClass("ТфФайловыйДиалог", "TfFileDialog")]
    public class TfFileDialog : AutoContext<TfFileDialog>
    {

        public TfFileDialog()
        {
            FileDialog FileDialog1 = new FileDialog();
            FileDialog1.dll_obj = this;
            Base_obj = FileDialog1;
        }

        public FileDialog Base_obj;

        [ContextProperty("Верх", "Top")]
        public TfPos Top
        {
            get { return new TfPos(Base_obj.Top); }
        }

        [ContextProperty("ВыравниваниеКнопок", "ButtonAlignment")]
        public int ButtonAlignment
        {
            get { return Base_obj.ButtonAlignment; }
            set { Base_obj.ButtonAlignment = value; }
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

        [ContextProperty("ДопустимыДругиеРасширения", "AllowsOtherFileTypes")]
        public bool AllowsOtherFileTypes
        {
            get { return Base_obj.AllowsOtherFileTypes; }
            set { Base_obj.AllowsOtherFileTypes = value; }
        }

        [ContextProperty("ДопустимыеФайлы", "AllowedFileTypes")]
        public ArrayImpl AllowedFileTypes
        {
            get { return Base_obj.AllowedFileTypes; }
            set { Base_obj.AllowedFileTypes = value; }
        }

        [ContextProperty("Доступность", "Enabled")]
        public bool Enabled
        {
            get { return Base_obj.Enabled; }
            set { Base_obj.Enabled = value; }
        }

        [ContextProperty("Заголовок", "Title")]
        public string Title
        {
            get { return Base_obj.Title; }
            set { Base_obj.Title = value; }
        }

        [ContextProperty("Игрек", "Y")]
        public IValue Y
        {
            get { return new TfPos(Base_obj.Y); }
            set
            {
                if (Utils.IsType<TfPos>(value))
                {
                    Base_obj.M_FileDialog.Y = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_FileDialog.Y = Terminal.Gui.Pos.At(Utils.ToInt32(value));
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
                    Base_obj.M_FileDialog.X = ((TfPos)value).Base_obj.M_Pos;
                }
                else if (Utils.IsNumber(value))
                {
                    Base_obj.M_FileDialog.X = Terminal.Gui.Pos.At(Utils.ToInt32(value));
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

        [ContextProperty("Лево", "Left")]
        public TfPos Left
        {
            get { return new TfPos(Base_obj.Left); }
        }

        [ContextProperty("Метка", "Tag")]
        public IValue Tag
        {
            get { return Base_obj.Tag; }
            set { Base_obj.Tag = value; }
        }

        [ContextProperty("Низ", "Bottom")]
        public TfPos Bottom
        {
            get { return new TfPos(Base_obj.Bottom); }
        }

        [ContextProperty("Отмена", "Canceled")]
        public bool Canceled
        {
            get { return Base_obj.Canceled; }
        }

        [ContextProperty("Отображать", "Visible")]
        public bool Visible
        {
            get { return Base_obj.Visible; }
            set { Base_obj.Visible = value; }
        }

        [ContextProperty("Подсказка", "Prompt")]
        public string Prompt
        {
            get { return Base_obj.Prompt; }
            set { Base_obj.Prompt = value; }
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

        [ContextProperty("ПутьКаталога", "DirectoryPath")]
        public string DirectoryPath
        {
            get { return Base_obj.DirectoryPath; }
            set { Base_obj.DirectoryPath = value; }
        }

        [ContextProperty("ПутьФайла", "FilePath")]
        public string FilePath
        {
            get { return Base_obj.FilePath; }
            set { Base_obj.FilePath = value; }
        }

        [ContextProperty("Родитель", "SuperView")]
        public IValue SuperView
        {
            get
            {
                if (Base_obj.M_FileDialog.SuperView.GetType().ToString().Contains("+ContentView"))
                {
                    return Utils.RevertEqualsObj(Base_obj.M_FileDialog.SuperView.SuperView).dll_obj;
                }
                return Utils.RevertEqualsObj(Base_obj.M_FileDialog.SuperView).dll_obj;
            }
        }

        [ContextProperty("СкрытьРасширение", "IsExtensionHidden")]
        public bool IsExtensionHidden
        {
            get { return Base_obj.IsExtensionHidden; }
            set { Base_obj.IsExtensionHidden = value; }
        }

        [ContextProperty("СозданиеКаталога", "CanCreateDirectories")]
        public bool CanCreateDirectories
        {
            get { return Base_obj.CanCreateDirectories; }
            set { Base_obj.CanCreateDirectories = value; }
        }

        [ContextProperty("Сообщение", "Message")]
        public string Message
        {
            get { return Base_obj.Message; }
            set { Base_obj.Message = value; }
        }

        [ContextProperty("СтильКомпоновки", "LayoutStyle")]
        public int LayoutStyle
        {
            get { return Base_obj.LayoutStyle; }
            set { Base_obj.LayoutStyle = value; }
        }

        [ContextProperty("Сфокусирован", "HasFocus")]
        public bool HasFocus
        {
            get { return Base_obj.HasFocus; }
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
            get { return Base_obj.M_FileDialog.CanFocus; }
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

        [ContextProperty("ДиалогЗакрыт", "DialogClosed")]
        public TfAction DialogClosed { get; set; }

        [ContextProperty("КлавишаНажата", "KeyPress")]
        public TfAction KeyPress { get; set; }

        [ContextProperty("МышьНадЭлементом", "MouseEnter")]
        public TfAction MouseEnter { get; set; }

        [ContextProperty("МышьПокинулаЭлемент", "MouseLeave")]
        public TfAction MouseLeave { get; set; }

        [ContextProperty("ПриВходе", "Enter")]
        public TfAction Enter { get; set; }

        [ContextProperty("ПриНажатииМыши", "MouseClick")]
        public TfAction MouseClick { get; set; }

        [ContextProperty("ПриУходе", "Leave")]
        public TfAction Leave { get; set; }

        [ContextProperty("СочетаниеКлавишДействие", "ShortcutAction")]
        public TfAction ShortcutAction { get; set; }

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

        [ContextMethod("ДобавитьКнопку", "AddButton")]
        public void AddButton(TfButton p1)
        {
            Base_obj.AddButton(p1.Base_obj);
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

        [ContextMethod("ТочкаНаЭлементе", "ScreenToView")]
        public TfPoint ScreenToView(int p1, int p2)
        {
            return new TfPoint(Base_obj.ScreenToView(p1, p2));
        }

        [ContextMethod("УдалитьСочетаниеКлавиш", "RemoveShortcut")]
        public void RemoveShortcut(decimal p1)
        {
            Utils.RemoveFromShortcutDictionary(p1, this);
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

    }
}
