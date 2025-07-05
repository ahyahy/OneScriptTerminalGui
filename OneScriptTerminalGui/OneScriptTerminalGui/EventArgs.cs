using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace ostgui
{

    [ContextClass("ТфАргументыСобытия", "TfEventArgs")]
    public class TfEventArgs : AutoContext<TfEventArgs>
    {

        public TfEventArgs()
        {
        }

        public TfBorder border = null;
        [ContextProperty("Граница", "Border")]
        public TfBorder Border
        {
            get { return border; }
        }

        public IValue y = null;
        [ContextProperty("Игрек", "Y")]
        public int Y
        {
            get { return Convert.ToInt32(y.AsNumber()); }
        }

        public IValue x = null;
        [ContextProperty("Икс", "X")]
        public int X
        {
            get { return Convert.ToInt32(x.AsNumber()); }
        }

        public IValue keyValue = null;
        [ContextProperty("Клавиша", "KeyValue")]
        public int KeyValue
        {
            get { return Convert.ToInt32(keyValue.AsNumber()); }
        }

        public IValue keyToString = null;
        [ContextProperty("КлавишаСтрокой", "KeyToString")]
        public string KeyToString
        {
            get { return keyToString.AsString(); }
        }

        public IValue cancel = null;
        [ContextProperty("Отмена", "Cancel")]
        public bool Cancel
        {
            get { return cancel.AsBoolean(); }
            set { cancel = ValueFactory.Create(value); }
        }

        public IValue sender = null;
        [ContextProperty("Отправитель", "Sender")]
        public IValue Sender
        {
            get { return sender; }
        }

        public IValue parameter = null;
        [ContextProperty("Параметр", "Parameter")]
        public IValue Parameter
        {
            get { return parameter; }
        }

        public TfRect rect = null;
        [ContextProperty("Прямоугольник", "Rect")]
        public TfRect Rect
        {
            get { return rect; }
        }

        public TfSize size = null;
        [ContextProperty("Размер", "Size")]
        public TfSize Size
        {
            get { return size; }
        }

        public IValue ofY = null;
        [ContextProperty("СмещениеИгрек", "OfY")]
        public int OfY
        {
            get { return Convert.ToInt32(ofY.AsNumber()); }
        }

        public IValue ofX = null;
        [ContextProperty("СмещениеИкс", "OfX")]
        public int OfX
        {
            get { return Convert.ToInt32(ofX.AsNumber()); }
        }

        public TfRect oldBounds = null;
        [ContextProperty("СтарыеГраницы", "OldBounds")]
        public TfRect OldBounds
        {
            get { return oldBounds; }
        }

        public IValue flags = null;
        [ContextProperty("ФлагиМыши", "Flags")]
        public int Flags
        {
            get { return Convert.ToInt32(flags.AsNumber()); }
        }

        public IValue view = null;
        [ContextProperty("Элемент", "View")]
        public IValue View
        {
            get { return view; }
        }

        public IValue isAlt = null;
        [ContextProperty("ЭтоAlt", "IsAlt")]
        public bool IsAlt
        {
            get { return isAlt.AsBoolean(); }
        }

        public IValue isCapslock = null;
        [ContextProperty("ЭтоCapslock", "IsCapslock")]
        public bool IsCapslock
        {
            get { return isCapslock.AsBoolean(); }
        }

        public IValue isCtrl = null;
        [ContextProperty("ЭтоCtrl", "IsCtrl")]
        public bool IsCtrl
        {
            get { return isCtrl.AsBoolean(); }
        }

        public IValue isNumlock = null;
        [ContextProperty("ЭтоNumlock", "IsNumlock")]
        public bool IsNumlock
        {
            get { return isNumlock.AsBoolean(); }
        }

        public IValue isScrolllock = null;
        [ContextProperty("ЭтоScrolllock", "IsScrolllock")]
        public bool IsScrolllock
        {
            get { return isScrolllock.AsBoolean(); }
        }

        public IValue isShift = null;
        [ContextProperty("ЭтоShift", "IsShift")]
        public bool IsShift
        {
            get { return isShift.AsBoolean(); }
        }

    }
}
