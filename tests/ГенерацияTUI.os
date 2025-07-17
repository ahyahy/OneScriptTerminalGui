// Скрипт читает файлы справки в КаталогСправки и создает *.cs файлы в КаталогВыгрузки
// Из каталога C:\444\ВыгрузкаДекларФорм\ файлы *.cs можно скопировать в каталог проекта.

Перем СтрРазделОбъявленияПеременных;
Перем КаталогСправки, КаталогВыгрузки, СписокНеизменныхКлассов;

Функция ОтобратьФайлы(Фильтр)
	// Фильтр = Класс Конструктор Члены Свойства Свойство Методы Метод Перечисление
	М_Фильтр = Новый Массив;
	ВыбранныеФайлы = НайтиФайлы(КаталогСправки, "*.html", Истина);
	Найдено1 = 0;
	Для А = 0 По ВыбранныеФайлы.ВГраница() Цикл
		ТекстДок = Новый ТекстовыйДокумент;
		ТекстДок.Прочитать(ВыбранныеФайлы[А].ПолноеИмя);
		Стр = ТекстДок.ПолучитьТекст();
		М = СтрНайтиМежду(Стр, "<H1 class=dtH1", "/H1>", , );
		Если М.Количество() > 0 Тогда
			СтрЗаголовка= М[0];
			Если (СтрНайти(СтрЗаголовка, Фильтр + "<") > 0) или (СтрНайти(СтрЗаголовка, Фильтр + " <") > 0) Тогда
				Найдено1 = Найдено1 + 1;
				// // // Сообщить("================================================================================================");
				// // // Сообщить("" + ВыбранныеФайлы[А].ПолноеИмя + "=" + СтрЗаголовка);
				// Сообщить("" + СтрЗаголовка);22
				М_Фильтр.Добавить(ВыбранныеФайлы[А].ПолноеИмя);
			КонецЕсли;
		КонецЕсли;
	КонецЦикла;
	
	Сообщить("Найдено1 (" + Фильтр + ") = " + Найдено1);
	Возврат М_Фильтр;
КонецФункции

Функция РазобратьСтроку(Строка, Разделитель)
	// Сообщить("==" + Строка);
	Стр = СтрЗаменить(Строка, Разделитель, Символы.ПС);
	М = Новый Массив;
	Если ПустаяСтрока(Стр) Тогда
		Возврат М;
	КонецЕсли;
	Для Ч = 1 По СтрЧислоСтрок(Стр) Цикл
		М.Добавить(СтрПолучитьСтроку(Стр, Ч));
	КонецЦикла;
	Возврат М;
КонецФункции

Функция СтрНайтиМежду(СтрПараметр, Фрагмент1 = Неопределено, Фрагмент2 = Неопределено, ИсключитьФрагменты = Истина, БезНаложения = Истина)
	//Стр - исходная строка
	//Фрагмент1 - подстрока поиска от которой ведем поиск
	//Фрагмент2 - подстрока поиска до которой ведем поиск
	//ИсключитьФрагменты - не включать Фрагмент1 и Фрагмент2 в результат
	//БезНаложения - в результат не будут включены участки, содержащие другие найденные участки, удовлетворяющие переданным параметрам
	//функция возвращает массив строк
	Стр = СтрПараметр;
	М = Новый Массив;
	Если (Фрагмент1 <> Неопределено) и (Фрагмент2 = Неопределено) Тогда
		Позиция = Найти(Стр, Фрагмент1);
		Пока Позиция > 0 Цикл
			М.Добавить(?(ИсключитьФрагменты, Сред(Стр, Позиция + СтрДлина(Фрагмент1)), Сред(Стр, Позиция)));
			Стр = Сред(Стр, Позиция + 1);
			Позиция = Найти(Стр, Фрагмент1);
		КонецЦикла;
	ИначеЕсли (Фрагмент1 = Неопределено) и (Фрагмент2 <> Неопределено) Тогда
		Позиция = Найти(Стр, Фрагмент2);
		СуммаПозиций = Позиция;
		Пока Позиция > 0 Цикл
			М.Добавить(?(ИсключитьФрагменты, Сред(Стр, 1, СуммаПозиций - 1), Сред(Стр, 1, СуммаПозиций - 1 + СтрДлина(Фрагмент2))));
			Позиция = Найти(Сред(Стр, СуммаПозиций + 1), Фрагмент2);
			СуммаПозиций = СуммаПозиций + Позиция;
		КонецЦикла;
	ИначеЕсли (Фрагмент1 <> Неопределено) и (Фрагмент2 <> Неопределено) Тогда
		Позиция = Найти(Стр, Фрагмент1);
		Пока Позиция > 0 Цикл
			Стр2 = ?(ИсключитьФрагменты, Сред(Стр, Позиция + СтрДлина(Фрагмент1)), Сред(Стр, Позиция));
			Позиция2 = Найти(Стр2, Фрагмент2);
			СуммаПозиций2 = Позиция2;
			Пока Позиция2 > 0 Цикл
				Если БезНаложения Тогда
					Если Найти(Сред(Стр2, 1, СуммаПозиций2 - 1), Фрагмент2) = 0 Тогда
						М.Добавить("" + ?(ИсключитьФрагменты, Сред(Стр2, 1, СуммаПозиций2 - 1), Сред(Стр2, 1, СуммаПозиций2 - 1 + СтрДлина(Фрагмент2))));
					КонецЕсли;
				Иначе
					М.Добавить("" + ?(ИсключитьФрагменты, Сред(Стр2, 1, СуммаПозиций2 - 1), Сред(Стр2, 1, СуммаПозиций2 - 1 + СтрДлина(Фрагмент2))));
				КонецЕсли;
				Позиция2 = Найти(Сред(Стр2, СуммаПозиций2 + 1), Фрагмент2);
				СуммаПозиций2 = СуммаПозиций2 + Позиция2;
			КонецЦикла;
			Стр = Сред(Стр, Позиция + 1);
			Позиция = Найти(Стр, Фрагмент1);
		КонецЦикла;
	КонецЕсли;
	
	Возврат М;
КонецФункции//СтрНайтиМежду

Функция Директивы(КлассАнгл)
	Если КлассАнгл = "qqqqqqqqqq" Тогда
		Стр = 
		"using System;
		|using System.Collections.Generic;
		|using System.Text;
		|using ScriptEngine.Machine.Contexts;
		|using ScriptEngine.Machine;
		|using System.Reflection;
		|using ScriptEngine.HostedScript.Library;
		|using Terminal.Gui;
		|";
		Возврат Стр;
	ИначеЕсли КлассАнгл = "VerticalTextAlignment"
		или КлассАнгл = "CursorVisibility"
		или КлассАнгл = "TextAlignment"
		или КлассАнгл = "Keys"
		или КлассАнгл = "ConsoleKey"
		или КлассАнгл = "CommandTUI"
		или КлассАнгл = "TextDirection"
		или КлассАнгл = "BorderStyle"
		или КлассАнгл = "LayoutStyle"
		или КлассАнгл = "MenuItemCheckStyle"
		или КлассАнгл = "MouseFlags"
		или КлассАнгл = "Color"
		или КлассАнгл = "SubviewCollection"
		Тогда
		Стр = 
		"using ScriptEngine.Machine.Contexts;
		|using ScriptEngine.Machine;
		|using System.Collections.Generic;
		|using System.Collections;
		|";
		Возврат Стр;
	ИначеЕсли КлассАнгл = "Attribute"
		или КлассАнгл = "MenusCollection"
		или КлассАнгл = "StatusBarItems"
		Тогда
		Стр = 
		"using ScriptEngine.Machine.Contexts;
		|";
		Возврат Стр;
	ИначеЕсли КлассАнгл = "Border"
		или КлассАнгл = "MenuBar"
		или КлассАнгл = "MenuBarItem"
		или КлассАнгл = "MenuItem"
		или КлассАнгл = "StatusBar"
		или КлассАнгл = "Toplevel"
		или КлассАнгл = "MenuBarItemChildren"
		Тогда
		Стр = 
		"using ScriptEngine.Machine.Contexts;
		|using ScriptEngine.Machine;
		|";
		Возврат Стр;
	ИначеЕсли КлассАнгл = "Button"
		или КлассАнгл = "Dim"
		или КлассАнгл = "EventArgs"
		или КлассАнгл = "Pos"
		или КлассАнгл = "Rect"
		или КлассАнгл = "StatusItem"
		Тогда
		Стр = 
		"using System;
		|using ScriptEngine.Machine.Contexts;
		|using ScriptEngine.Machine;
		|";
		Возврат Стр;
	ИначеЕсли КлассАнгл = "Window"
		Тогда
		Стр = 
		"using System;
		|using ScriptEngine.Machine.Contexts;
		|using ScriptEngine.Machine;
		|using Terminal.Gui;
		|";
		Возврат Стр;
	ИначеЕсли КлассАнгл = "Colors"
		или КлассАнгл = "ColorScheme"
		или КлассАнгл = "Point"
		или КлассАнгл = "Size"
		или КлассАнгл = "Thickness"
		Тогда
		Стр = 
		"using ScriptEngine.Machine.Contexts;
		|";
		Возврат Стр;
	ИначеЕсли КлассАнгл = "MessageBox"
		Тогда
		Стр = 
		"using System;
		|using ScriptEngine.Machine.Contexts;
		|using ScriptEngine.Machine;
		|using ScriptEngine.HostedScript.Library;
		|using Terminal.Gui;
		|";
		Возврат Стр;
	ИначеЕсли КлассАнгл = "TextFormatter"
		Тогда
		Стр = 
		"using System;
		|using System.Collections.Generic;
		|using ScriptEngine.Machine.Contexts;
		|using ScriptEngine.Machine;
		|using ScriptEngine.HostedScript.Library;
		|";
		Возврат Стр;
	ИначеЕсли КлассАнгл = "Timer"
		Тогда
		Стр = 
		"using System;
		|using ScriptEngine.Machine.Contexts;
		|using Terminal.Gui;
		|";
		Возврат Стр;
	ИначеЕсли КлассАнгл = "View"
		Тогда
		Стр = 
		"using System;
		|using ScriptEngine.Machine.Contexts;
		|using ScriptEngine.Machine;
		|using Terminal.Gui;
		|";
		Возврат Стр;
		
		

		
		
		
	Иначе
		Стр = 
		"using System;
		|using System.Collections;
		|using System.Collections.Generic;
		|using System.Text;
		|using ScriptEngine.Machine.Contexts;
		|using ScriptEngine.Machine;
		|using System.Reflection;
		|using ScriptEngine.HostedScript.Library;
		|using Terminal.Gui;
		|";
		Возврат Стр;
		
	КонецЕсли;
	Возврат "";
КонецФункции//Директивы

Функция Шапка(КлассАнгл, КлассРус)
	Если КлассАнгл = "SubviewCollection" Тогда
		Стр = "
		|    [ContextClass(""ТфКоллекцияПодэлементов"", ""TfSubviewCollection"")]
		|    public class TfSubviewCollection : AutoContext<TfSubviewCollection>, ICollectionContext, IEnumerable<IValue>
		|    {";
	ИначеЕсли КлассАнгл = "MenusCollection" Тогда
		Стр = "
		|    [ContextClass(""ТфКоллекцияПодменю"", ""TfMenusCollection"")]
		|    public class TfMenusCollection : AutoContext<TfMenusCollection>
		|    {";
		
	
	Иначе
		Стр = "
		|    [ContextClass(""Тф" + КлассРус + """, ""Tf" + КлассАнгл + """)]
		|    public class Tf" + КлассАнгл + " : AutoContext<Tf" + КлассАнгл + ">
		|    {";
	КонецЕсли;
	Возврат Стр;
КонецФункции //Шапка

Функция РазделОбъявленияПеременных(КлассАнгл, КлассРус)
	Если КлассАнгл = "йййййййййййййййййй" Тогда
		Стр = 
		"        [DllImport(""User32.dll"")] static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, UIntPtr dwExtraInfo);
		|        private static object syncRoot = new Object();
		|
		|        public static bool systemVersionIsMicrosoft = false;
		|        public static bool goOn = true;";
		
	Иначе
		Стр = "";
	КонецЕсли;
	Возврат Стр;
КонецФункции//РазделОбъявленияПеременных

Функция Конструктор(КлассАнгл, КлассРус)
	Если КлассАнгл = "Button" Тогда
		Стр = 
		"        public TfButton()
		|        {
		|            Button Button1 = new Button();
		|            Button1.dll_obj = this;
		|            Base_obj = Button1;
		|        }
		|
		|        public TfButton(string p1, bool p2 = false)
		|        {
		|            Button Button1 = new Button(p1, p2);
		|            Button1.dll_obj = this;
		|            Base_obj = Button1;
		|        }
		|
		|        public TfButton(int p1, int p2, string p3)
		|        {
		|            Button Button1 = new Button(p1, p2, p3);
		|            Button1.dll_obj = this;
		|            Base_obj = Button1;
		|        }
		|
		|        public TfButton(int p1, int p2, string p3, bool p4)
		|        {
		|            Button Button1 = new Button(p1, p2, p3, p4);
		|            Button1.dll_obj = this;
		|            Base_obj = Button1;
		|        }
		|
		|        public TfAction LayoutComplete { get; set; }
		|        public TfAction LayoutStarted { get; set; }
		|        public TfAction DrawContentComplete { get; set; }
		|        public TfAction DrawContent { get; set; }
		|        public TfAction InitializedItem { get; set; }
		|        public TfAction CanFocusChanged { get; set; }
		|        public TfAction Added { get; set; }
		|        public TfAction Removed { get; set; }
		|
		|        public Button Base_obj;
		|";
	ИначеЕсли КлассАнгл = "ColorScheme" Тогда
		Стр = 
		"        public TfColorScheme()
		|        {
		|            ColorScheme ColorScheme1 = new ColorScheme();
		|            ColorScheme1.dll_obj = this;
		|            Base_obj = ColorScheme1;
		|        }
		|
		|        public TfColorScheme(ostgui.ColorScheme p1)
		|        {
		|            ColorScheme ColorScheme1 = p1;
		|            ColorScheme1.dll_obj = this;
		|            Base_obj = ColorScheme1;
		|        }
		|
		|        public ColorScheme Base_obj;
		|";
	ИначеЕсли КлассАнгл = "Timer" Тогда
		Стр = 
		"        private object token;
		|        private bool stop = false;
		|
		|        public TfTimer()
		|        {
		|        }
		|";
	ИначеЕсли КлассАнгл = "MessageBox" Тогда
		Стр = 
		"        public TfMessageBox()
		|        {
		|            buttons = new ArrayImpl();
		|            buttons.Add(ValueFactory.Create(""Yes""));
		|            buttons.Add(ValueFactory.Create(""No""));
		|        }
		|
		|        private int Clicked = -1;
		|        private object token = null;
		|";
	ИначеЕсли КлассАнгл = "Dim" Тогда
		Стр = 
		"        public TfDim()
		|        {
		|            Dim Dim1 = new Dim();
		|            Dim1.dll_obj = this;
		|            Base_obj = Dim1;
		|        }
		|
		|        public TfDim(Dim p1)
		|        {
		|            Dim Dim1 = p1;
		|            Dim1.dll_obj = this;
		|            Base_obj = Dim1;
		|        }
		|
		|        public Dim Base_obj;
		|";
	ИначеЕсли КлассАнгл = "MenuBarItemChildren" Тогда
		Стр = 
		"        public ostgui.MenuBarItem M_MenuBarItem;
		|
		|        public Terminal.Gui.MenuItem[] M_Object
		|        {
		|            get { return M_MenuBarItem.Children; }
		|            set { M_MenuBarItem.Children = value; }
		|        }
		|";
	ИначеЕсли КлассАнгл = "MenuItem" Тогда
		Стр = 
		"        public TfMenuItem()
		|        {
		|            MenuItem MenuItem1 = new MenuItem();
		|            MenuItem1.dll_obj = this;
		|            Base_obj = MenuItem1;
		|        }
		|
		|        public MenuItem Base_obj;
		|";
	ИначеЕсли КлассАнгл = "StatusBar" Тогда
		Стр = 
		"        private TfStatusBarItems statusBarItems;
		|
		|        public TfStatusBar()
		|        {
		|            StatusBar StatusBar1 = new StatusBar();
		|            StatusBar1.dll_obj = this;
		|            Base_obj = StatusBar1;
		|
		|            statusBarItems = new TfStatusBarItems();
		|            statusBarItems.M_StatusBar = Base_obj;
		|        }
		|
		|        public StatusBar Base_obj;
		|
		|        public TfAction HotKeyChanged { get; set; }
		|        public TfAction LayoutComplete { get; set; }
		|        public TfAction LayoutStarted { get; set; }
		|        public TfAction DrawContentComplete { get; set; }
		|        public TfAction DrawContent { get; set; }
		|        public TfAction Added { get; set; }
		|        public TfAction InitializedItem { get; set; }
		|        public TfAction Removed { get; set; }
		|";
	ИначеЕсли КлассАнгл = "StatusBarItems" Тогда
		Стр = 
		"        public ostgui.StatusBar M_StatusBar;
		|
		|        public Terminal.Gui.StatusItem[] M_Object
		|        {
		|            get { return M_StatusBar.Items; }
		|            set { M_StatusBar.Items = value; }
		|        }
		|";
	ИначеЕсли КлассАнгл = "StatusItem" Тогда
		Стр = 
		"        public TfStatusItem(int p1, string p2)
		|        {
		|            StatusItem StatusItem1 = new StatusItem((Terminal.Gui.Key)p1, p2);
		|            StatusItem1.dll_obj = this;
		|            Base_obj = StatusItem1;
		|        }
		|
		|        public StatusItem Base_obj;
		|";
	ИначеЕсли КлассАнгл = "Pos" Тогда
		Стр = 
		"        public TfPos()
		|        {
		|            Pos Pos1 = new Pos();
		|            Pos1.dll_obj = this;
		|            Base_obj = Pos1;
		|        }
		|
		|        public TfPos(Pos p1)
		|        {
		|            Pos Pos1 = p1;
		|            Pos1.dll_obj = this;
		|            Base_obj = Pos1;
		|        }
		|
		|        public Pos Base_obj;
		|";
	ИначеЕсли КлассАнгл = "MenuBar" Тогда
		Стр = 
		"        private TfMenusCollection menusCollection;
		|
		|        public TfMenuBar()
		|        {
		|            MenuBar MenuBar1 = new MenuBar();
		|            MenuBar1.dll_obj = this;
		|            Base_obj = MenuBar1;
		|
		|            menusCollection = new TfMenusCollection();
		|            menusCollection.M_MenuBar = Base_obj;
		|        }
		|
		|        public TfAction LayoutComplete { get; set; }
		|        public TfAction LayoutStarted { get; set; }
		|        public TfAction DrawContentComplete { get; set; }
		|        public TfAction DrawContent { get; set; }
		|        public TfAction ShortcutAction { get; set; }
		|        public TfAction Added { get; set; }
		|        public TfAction InitializedItem { get; set; }
		|        public TfAction MenuClosing { get; set; }
		|        public TfAction KeyPress { get; set; }
		|        public TfAction Removed { get; set; }
		|        public TfAction MouseClick { get; set; }
		|
		|        public MenuBar Base_obj;
		|";
	ИначеЕсли КлассАнгл = "MenuBarItem" Тогда
		Стр = 
		"        private TfMenuBarItemChildren menuBarItemChildren;
		|
		|        public TfMenuBarItem()
		|        {
		|            MenuBarItem MenuBarItem1 = new MenuBarItem();
		|            MenuBarItem1.dll_obj = this;
		|            Base_obj = MenuBarItem1;
		|
		|            menuBarItemChildren = new TfMenuBarItemChildren();
		|            menuBarItemChildren.M_MenuBarItem = Base_obj;
		|        }
		|
		|        public MenuBarItem Base_obj;
		|";
	ИначеЕсли КлассАнгл = "MenusCollection" Тогда
		Стр = 
		"        public ostgui.MenuBar M_MenuBar;
		|
		|        public Terminal.Gui.MenuBarItem[] M_Object
		|        {
		|            get { return M_MenuBar.Menus; }
		|            set { M_MenuBar.Menus = value; }
		|        }
		|";
	ИначеЕсли КлассАнгл = "Thickness" Тогда
		Стр = 
		"        public TfThickness(int p1)
		|        {
		|            Thickness Thickness1 = new Thickness(p1);
		|            Thickness1.dll_obj = this;
		|            Base_obj = Thickness1;
		|        }
		|
		|        public TfThickness(int left, int top, int right, int bottom)
		|        {
		|            Thickness Thickness1 = new Thickness(left, top, right, bottom);
		|            Thickness1.dll_obj = this;
		|            Base_obj = Thickness1;
		|        }
		|
		|        public TfThickness(ostgui.Thickness p1)
		|        {
		|            Thickness Thickness1 = p1;
		|            Thickness1.dll_obj = this;
		|            Base_obj = Thickness1;
		|        }
		|
		|        public Thickness Base_obj;
		|";
	ИначеЕсли КлассАнгл = "EventArgs" Тогда
		Стр = 
		"        public TfEventArgs()
		|        {
		|        }
		|";
	ИначеЕсли КлассАнгл = "TextFormatter" Тогда
		Стр = 
		"        public TfTextFormatter()
		|        {
		|            TextFormatter TextFormatter1 = new TextFormatter();
		|            TextFormatter1.dll_obj = this;
		|            Base_obj = TextFormatter1;
		|        }
		|
		|        public TfSize Size
		|        {
		|            get { return new TfSize(Base_obj.Size); }
		|            set { Base_obj.Size = value.Base_obj; }
		|        }
		|
		|        public TextFormatter Base_obj;
		|
		|        public TfAction HotKeyChanged { get; set; }
		|";
	ИначеЕсли КлассАнгл = "SubviewCollection" Тогда
		Стр = 
		"        public TfSubviewCollection(IList<Terminal.Gui.View> p1)
		|        {
		|            SubviewCollection SubviewCollection1 = new SubviewCollection(p1);
		|            SubviewCollection1.dll_obj = this;
		|            Base_obj = SubviewCollection1;
		|        }
		|
		|        public int Count()
		|        {
		|            return CountControl;
		|        }
		|
		|        public CollectionEnumerator GetManagedIterator()
		|        {
		|            return new CollectionEnumerator(this);
		|        }
		|
		|        IEnumerator IEnumerable.GetEnumerator()
		|        {
		|            return ((IEnumerable<IValue>)this).GetEnumerator();
		|        }
		|
		|        IEnumerator<IValue> IEnumerable<IValue>.GetEnumerator()
		|        {
		|            foreach (var item in Base_obj.M_SubviewCollection)
		|            {
		|                yield return (OneScriptTerminalGui.RevertEqualsObj(item).dll_obj as IValue);
		|            }
		|        }
		|
		|        public SubviewCollection Base_obj;
		|";
	ИначеЕсли КлассАнгл = "Window" Тогда
		Стр = 
		"        public TfWindow()
		|        {
		|            Window Window1 = new Window();
		|            Window1.dll_obj = this;
		|            Base_obj = Window1;
		|        }
		|
		|        public TfWindow(string p1)
		|        {
		|            Window Window1 = new Window(p1);
		|            Window1.dll_obj = this;
		|            Base_obj = Window1;
		|        }
		|
		|        public TfWindow(TfRect p1, string p2)
		|        {
		|            Window Window1 = new Window(p1.Base_obj, p2);
		|            Window1.dll_obj = this;
		|            Base_obj = Window1;
		|        }
		|
		|        public TfWindow(string p1, int p2, TfBorder p3)
		|        {
		|            Window Window1 = new Window(p1, p2, p3.Base_obj);
		|            Window1.dll_obj = this;
		|            Base_obj = Window1;
		|        }
		|
		|        public TfWindow(TfRect p1, string p2, int p3, TfBorder p4)
		|        {
		|            Window Window1 = new Window(p1.Base_obj, p2, p3, p4.Base_obj);
		|            Window1.dll_obj = this;
		|            Base_obj = Window1;
		|        }
		|
		|        public TfAction Activate { get; set; }
		|        public TfAction Deactivate { get; set; }
		|        public TfAction AllChildClosed { get; set; }
		|        public TfAction KeyPress { get; set; }
		|        public TfAction InitializedItem { get; set; }
		|        public TfAction Added { get; set; }
		|        public TfAction Removed { get; set; }
		|        public TfAction LayoutComplete { get; set; }
		|        public TfAction LayoutStarted { get; set; }
		|        public TfAction DrawContentComplete { get; set; }
		|        public TfAction DrawContent { get; set; }
		|        public TfAction TitleChanging { get; set; }
		|        public TfAction Loaded { get; set; }
		|        public TfAction Resized { get; set; }
		|        public TfAction Closing { get; set; }
		|
		|        public Window Base_obj;
		|";
	ИначеЕсли КлассАнгл = "Point" Тогда
		Стр = 
		"        public TfPoint()
		|        {
		|            Point Point1 = new Point();
		|            Point1.dll_obj = this;
		|            Base_obj = Point1;
		|        }
		|
		|        public TfPoint(TfSize p1)
		|        {
		|            Point Point1 = new Point(p1.Base_obj.M_Size);
		|            Point1.dll_obj = this;
		|            Base_obj = Point1;
		|        }
		|
		|        public TfPoint(Point p1)
		|        {
		|            Point Point1 = p1;
		|            Point1.dll_obj = this;
		|            Base_obj = Point1;
		|        }
		|
		|        public TfPoint(int p1, int p2)
		|        {
		|            Point Point1 = new Point(p1, p2);
		|            Point1.dll_obj = this;
		|            Base_obj = Point1;
		|        }
		|
		|        public Point Base_obj;
		|";
	ИначеЕсли КлассАнгл = "Attribute" Тогда
		Стр = 
		"        public TfAttribute()
		|        {
		|            Attribute Attribute1 = new Attribute();
		|            Attribute1.dll_obj = this;
		|            Base_obj = Attribute1;
		|        }
		|
		|        public TfAttribute(int p1)
		|        {
		|            Attribute Attribute1 = new Attribute(p1);
		|            Attribute1.dll_obj = this;
		|            Base_obj = Attribute1;
		|        }
		|
		|        public TfAttribute(int p1, int p2)
		|        {
		|            Attribute Attribute1 = new Attribute(p1, p2);
		|            Attribute1.dll_obj = this;
		|            Base_obj = Attribute1;
		|        }
		|
		|        public TfAttribute(int p1, int p2, int p3)
		|        {
		|            Attribute Attribute1 = new Attribute(p1, p2, p3);
		|            Attribute1.dll_obj = this;
		|            Base_obj = Attribute1;
		|        }
		|
		|        public TfAttribute(ostgui.Attribute p1)
		|        {
		|            Attribute Attribute1 = p1;
		|            Attribute1.dll_obj = this;
		|            Base_obj = Attribute1;
		|        }
		|
		|        public Attribute Base_obj;
		|";
	ИначеЕсли КлассАнгл = "Rect" Тогда
		Стр = 
		"        public TfRect()
		|        {
		|            Rect Rect1 = new Rect();
		|            Rect1.dll_obj = this;
		|            Base_obj = Rect1;
		|        }
		|
		|        public TfRect(TfPoint p1, TfSize p2)
		|        {
		|            Rect Rect1 = new Rect(p1.Base_obj.M_Point, p2.Base_obj.M_Size);
		|            Rect1.dll_obj = this;
		|            Base_obj = Rect1;
		|        }
		|
		|        public TfRect(Rect p1)
		|        {
		|            Rect Rect1 = p1;
		|            Rect1.dll_obj = this;
		|            Base_obj = Rect1;
		|        }
		|
		|        public TfRect(int x, int y, int width, int height)
		|        {
		|            Rect Rect1 = new Rect(x, y, width, height);
		|            Rect1.dll_obj = this;
		|            Base_obj = Rect1;
		|        }
		|
		|        public Rect Base_obj;
		|";
	ИначеЕсли КлассАнгл = "Size" Тогда
		Стр = 
		"        public TfSize()
		|        {
		|            Size Size1 = new Size();
		|            Size1.dll_obj = this;
		|            Base_obj = Size1;
		|        }
		|
		|        public TfSize(Size p1)
		|        {
		|            Size Size1 = p1;
		|            Size1.dll_obj = this;
		|            Base_obj = Size1;
		|        }
		|
		|        public TfSize(int p1, int p2)
		|        {
		|            Size Size1 = new Size(p1, p2);
		|            Size1.dll_obj = this;
		|            Base_obj = Size1;
		|        }
		|
		|        public Size Base_obj;
		|";
	ИначеЕсли КлассАнгл = "View" Тогда
		Стр = 
		"        public TfView()
		|        {
		|            View View1 = new View();
		|            View1.dll_obj = this;
		|            Base_obj = View1;
		|        }
		|
		|        public TfView(TfRect p1)
		|        {
		|            View View1 = new View(p1.Base_obj.M_Rect);
		|            View1.dll_obj = this;
		|            Base_obj = View1;
		|        }
		|
		|        public TfView(int p1, int p2, string p3)
		|        {
		|            View View1 = new View(p1, p2, p3);
		|            View1.dll_obj = this;
		|            Base_obj = View1;
		|        }
		|
		|        public TfView(TfRect p1, string p2, TfBorder p3)
		|        {
		|            View View1 = new View(p1.Base_obj.M_Rect, p2, p3.Base_obj.M_Border);
		|            View1.dll_obj = this;
		|            Base_obj = View1;
		|        }
		|
		|        public TfView(View p1)
		|        {
		|            View View1 = p1;
		|            View1.dll_obj = this;
		|            Base_obj = View1;
		|        }
		|
		|        public TfView(string p1, int p2, TfBorder p3)
		|        {
		|            View View1 = new View(p1, p2, p3.Base_obj.M_Border);
		|            View1.dll_obj = this;
		|            Base_obj = View1;
		|        }
		|
		|        public void CorrectionZet()
		|        {
		|            Base_obj.CorrectionZet();
		|        }
		|
		|        public TfAction LayoutComplete { get; set; }
		|        public TfAction LayoutStarted { get; set; }
		|        public TfAction DrawContentComplete { get; set; }
		|        public TfAction DrawContent { get; set; }
		|        public TfAction Added { get; set; }
		|        public TfAction Removed { get; set; }
		|
		|        public View Base_obj;
		|";
	ИначеЕсли КлассАнгл = "Toplevel" Тогда
		Стр = 
		"        public TfToplevel()
		|        {
		|            Toplevel Toplevel1 = new Toplevel();
		|            Toplevel1.dll_obj = this;
		|            Base_obj = Toplevel1;
		|        }
		|
		|        public TfToplevel(int p1, int p2, int p3, int p4)
		|        {
		|            TfRect Rect1 = new TfRect(p1, p2, p3, p4);
		|            Toplevel Toplevel1 = new Toplevel(Rect1.Base_obj.M_Rect);
		|            Toplevel1.dll_obj = this;
		|            Base_obj = Toplevel1;
		|        }
		|
		|        public TfToplevel(TfRect p1)
		|        {
		|            Toplevel Toplevel1 = new Toplevel(p1.Base_obj.M_Rect);
		|            Toplevel1.dll_obj = this;
		|            Base_obj = Toplevel1;
		|        }
		|
		|        public TfToplevel(ostgui.Toplevel p1)
		|        {
		|            Toplevel Toplevel1 = p1;
		|            Toplevel1.dll_obj = this;
		|            Base_obj = Toplevel1;
		|        }
		|
		|        public TfToplevel(Terminal.Gui.Toplevel p1)
		|        {
		|            Toplevel Toplevel1 = new Toplevel(p1);
		|            Toplevel1.dll_obj = this;
		|            Base_obj = Toplevel1;
		|        }
		|
		|        public void CorrectionZet()
		|        {
		|            Base_obj.CorrectionZet();
		|        }
		|
		|        public TfAction Unloaded { get; set; }
		|        public TfAction Ready { get; set; }
		|        public TfAction ChildUnloaded { get; set; }
		|        public TfAction ChildLoaded { get; set; }
		|        public TfAction ChildClosed { get; set; }
		|        public TfAction QuitKeyChanged { get; set; }
		|        public TfAction Closed { get; set; }
		|        public TfAction CanFocusChanged { get; set; }
		|        public TfAction InitializedItem { get; set; }
		|        public TfAction Activate { get; set; }
		|        public TfAction Deactivate { get; set; }
		|        public TfAction AllChildClosed { get; set; }
		|        public TfAction KeyPress { get; set; }
		|        public TfAction Added { get; set; }
		|        public TfAction Removed { get; set; }
		|        public TfAction LayoutComplete { get; set; }
		|        public TfAction LayoutStarted { get; set; }
		|        public TfAction DrawContentComplete { get; set; }
		|        public TfAction DrawContent { get; set; }
		|        public TfAction Leave { get; set; }
		|        public TfAction Enter { get; set; }
		|        public TfAction Loaded { get; set; }
		|        public TfAction Closing { get; set; }
		|        public TfAction Resized { get; set; }
		|
		|        public Toplevel Base_obj;
		|";
		
		
		
	Иначе
		Стр = 
		"        public Tf" + КлассАнгл + "()
		|        {
		|            " + КлассАнгл + " " + КлассАнгл + "1 = new " + КлассАнгл + "();
		|            " + КлассАнгл + "1.dll_obj = this;
		|            Base_obj = " + КлассАнгл + "1;
		|        }
		|
		|        public " + КлассАнгл + " Base_obj;
		|";
	КонецЕсли;
	Возврат Стр;
КонецФункции//Конструктор

Функция Свойства(ФайлСвойств, КлассАнгл, КлассРус)
	ТекстДокСвойств = Новый ТекстовыйДокумент;
	КаталогНаДиске = Новый Файл(ФайлСвойств);
    Если Не (КаталогНаДиске.Существует()) Тогда
		Возврат "";
	КонецЕсли;
	ТекстДокСвойств.Прочитать(ФайлСвойств);
	СтрТекстДокСвойств = ТекстДокСвойств.ПолучитьТекст();
	М505 = СтрНайтиМежду(СтрТекстДокСвойств, "<TBODY>", "</TABLE>", Ложь, );
	Если Не (М505.Количество() > 1) Тогда
		Возврат "";
	КонецЕсли;
	СтрТаблицаСвойств = М505[1];
	Массив1 = СтрНайтиМежду(СтрТаблицаСвойств, "<TR vAlign=top>", "</TR>", Ложь, );
	// Сообщить("Массив1.Количество()=" + Массив1.Количество());
	Если Массив1.Количество() > 0 Тогда
		Стр = "";
		Для А = 0 По Массив1.ВГраница() Цикл
			//найдем первую ячейку строки таблицы
			М07 = СтрНайтиМежду(Массив1[А], "<TD width=""50%"">", "</TD>", Ложь, );
			СтрХ = М07[0];
			СтрХ = СтрЗаменить(СтрХ, "&nbsp;", " ");
			
			ИмяФайлаСвойства = КаталогСправки + "\" + СтрНайтиМежду(СтрХ, "<A href=""", """>", , )[0];
			
			КаталогНаДиске = Новый Файл(ИмяФайлаСвойства);
			Если Не КаталогНаДиске.Существует() Тогда
				Продолжить;
			КонецЕсли;
			
			ТекстДокСвойства = Новый ТекстовыйДокумент;
			ТекстДокСвойства.Прочитать(ИмяФайлаСвойства);
			СтрТекстДокСвойства = ТекстДокСвойства.ПолучитьТекст();
			СтрРаздела = СтрНайтиМежду(СтрТекстДокСвойства, "<H4 class=dtH4>Использование</H4>", "<H4 class=dtH4>Значение</H4>", , )[0];
			СтрИспользование = СтрНайтиМежду(СтрРаздела, "<P>", "</P>", , )[0];
			СтрИспользование = СтрЗаменить(СтрИспользование, ".", "");

			СвойствоАнгл = СтрНайтиМежду(СтрХ, "(", ")", , )[0];
			СвойствоРус = СтрНайтиМежду(СтрХ, ".html"">", " (", , )[0];
			
			ТипЗнач = "xxxx";
			СтрРаздела = СтрНайтиМежду(СтрТекстДокСвойства, "<H4 class=dtH4>Значение</H4>", "/P>", , )[0];
			ТипЗнач = СтрНайтиМежду(СтрРаздела, "<P>Тип: ", "<", , )[0];
			ТипЗнач = СтрЗаменить(ТипЗнач, ".", "");
			Если ТипЗнач = "Число" Тогда
				ТипЗнач = "int";
			ИначеЕсли ТипЗнач = "Строка" Тогда
				ТипЗнач = "string";
			ИначеЕсли ТипЗнач = "Булево" Тогда
				ТипЗнач = "bool";
			ИначеЕсли ТипЗнач = "Произвольный" Тогда
				ТипЗнач = "IValue";
			ИначеЕсли СвойствоАнгл = "VerticalTextAlignment" 
				или СвойствоАнгл = "TextAlignment" 
				или СвойствоАнгл = "HotKey" 
				или СвойствоАнгл = "TextDirection" 
				или СвойствоАнгл = "Shortcut" 
				или СвойствоАнгл = "LayoutStyle" 
			
				Тогда
				ТипЗнач = "int";
			ИначеЕсли СвойствоАнгл = "TextFormatter" Тогда
				ТипЗнач = "TfTextFormatter";
			ИначеЕсли СвойствоАнгл = "Subviews" Тогда
				ТипЗнач = "ArrayImpl";
			ИначеЕсли СвойствоАнгл = "ShortcutAction" Тогда
				ТипЗнач = "TfAction";
			ИначеЕсли СвойствоАнгл = "ColorScheme" Тогда
				ТипЗнач = "TfColorScheme";
			ИначеЕсли СвойствоАнгл = "Border" Тогда
				ТипЗнач = "TfBorder";
			ИначеЕсли СвойствоАнгл = "Bounds" 
				или СвойствоАнгл = "Frame" 
				Тогда
				ТипЗнач = "TfRect";
			КонецЕсли;

			Если (СвойствоРус = "Данные") Тогда
				Стр = Стр +
				"        [ContextProperty(""Данные"", ""Data"")]
				|        public IValue Data
				|        {
				|            get { return OneScriptTerminalGui.RevertObj(Base_obj.Data); }
				|            set { Base_obj.Data = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Подэлементы") и (КлассАнгл = "Window") Тогда
				Стр = Стр +
				"        [ContextProperty(""Подэлементы"", ""Subviews"")]
				|        public TfSubviewCollection Subviews
				|        {
				|            get { return new TfSubviewCollection((dynamic)Base_obj.M_Window.Subviews[0].Subviews); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Подэлементы") Тогда
				Стр = Стр +
				"        [ContextProperty(""Подэлементы"", ""Subviews"")]
				|        public TfSubviewCollection Subviews
				|        {
				|            get { return new TfSubviewCollection(Base_obj.M_" + КлассАнгл + ".Subviews); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Количество") и (КлассАнгл = "SubviewCollection") Тогда
				Стр = Стр +
				"        [ContextProperty(""Количество"", ""Count"")]
				|        public int CountControl
				|        {
				|            get { return Base_obj.M_SubviewCollection.Count; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ОсновнойЦвет") Тогда
				Стр = Стр +
				"        [ContextProperty(""ОсновнойЦвет"", ""Foreground"")]
				|        public int Foreground
				|        {
				|            get { return Base_obj.Foreground; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ЦветФона") и (КлассАнгл = "Attribute") Тогда
				Стр = Стр +
				"        [ContextProperty(""ЦветФона"", ""Background"")]
				|        public int Background
				|        {
				|            get { return Base_obj.Background; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ЦветФона") Тогда
				Стр = Стр +
				"        [ContextProperty(""ЦветФона"", ""Background"")]
				|        public int Background
				|        {
				|            get { return Base_obj.Background; }
				|            set { Base_obj.Background = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ПанельМеню") и (КлассАнгл = "Toplevel") Тогда
				Стр = Стр +
				"        [ContextProperty(""ПанельМеню"", ""MenuBar"")]
				|        public TfMenuBar MenuBar
				|        {
				|            get { return Base_obj.MenuBar.dll_obj; }
				|            set { Base_obj.MenuBar = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "СтрокаСостояния") и (КлассАнгл = "Toplevel") Тогда
				Стр = Стр +
				"        [ContextProperty(""СтрокаСостояния"", ""StatusBar"")]
				|        public TfStatusBar StatusBar
				|        {
				|            get { return Base_obj.StatusBar.dll_obj; }
				|            set { Base_obj.M_View.Add(value.Base_obj.M_View); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Игрек") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue y = null;
				|        [ContextProperty(""Игрек"", ""Y"")]
				|        public int Y
				|        {
				|            get { return Convert.ToInt32(y.AsNumber()); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Игрек") и (
				(КлассАнгл = "Point") или 
				(КлассАнгл = "Rect")) Тогда
				Стр = Стр +
				"        [ContextProperty(""Игрек"", ""Y"")]
				|        public int Y
				|        {
				|            get { return Base_obj.Y; }
				|            set { Base_obj.Y = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Икс") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue x = null;
				|        [ContextProperty(""Икс"", ""X"")]
				|        public int X
				|        {
				|            get { return Convert.ToInt32(x.AsNumber()); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Икс") и (
				(КлассАнгл = "Point") или 
				(КлассАнгл = "Rect")) Тогда
				Стр = Стр +
				"        [ContextProperty(""Икс"", ""X"")]
				|        public int X
				|        {
				|            get { return Base_obj.X; }
				|            set { Base_obj.X = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Икс") Тогда
				Стр = Стр +
				"        [ContextProperty(""Икс"", ""X"")]
				|        public TfPos X
				|        {
				|            get { return new TfPos(Base_obj.X); }
				|            set { Base_obj.X = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Клавиша") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue keyValue = null;
				|        [ContextProperty(""Клавиша"", ""KeyValue"")]
				|        public int KeyValue
				|        {
				|            get { return Convert.ToInt32(keyValue.AsNumber()); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "КлавишаСтрокой") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue keyToString = null;
				|        [ContextProperty(""КлавишаСтрокой"", ""KeyToString"")]
				|        public string KeyToString
				|        {
				|            get { return keyToString.AsString(); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Отправитель") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue sender = null;
				|        [ContextProperty(""Отправитель"", ""Sender"")]
				|        public IValue Sender
				|        {
				|            get { return sender; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Параметр") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue parameter = null;
				|        [ContextProperty(""Параметр"", ""Parameter"")]
				|        public IValue Parameter
				|        {
				|            get { return parameter; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Прямоугольник") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public TfRect rect = null;
				|        [ContextProperty(""Прямоугольник"", ""Rect"")]
				|        public TfRect Rect
				|        {
				|            get { return rect; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "СмещениеИгрек") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue ofY = null;
				|        [ContextProperty(""СмещениеИгрек"", ""OfY"")]
				|        public int OfY
				|        {
				|            get { return Convert.ToInt32(ofY.AsNumber()); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "СмещениеИкс") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue ofX = null;
				|        [ContextProperty(""СмещениеИкс"", ""OfX"")]
				|        public int OfX
				|        {
				|            get { return Convert.ToInt32(ofX.AsNumber()); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "СтарыеГраницы") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public TfRect oldBounds = null;
				|        [ContextProperty(""СтарыеГраницы"", ""OldBounds"")]
				|        public TfRect OldBounds
				|        {
				|            get { return oldBounds; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ФлагиМыши") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue flags = null;
				|        [ContextProperty(""ФлагиМыши"", ""Flags"")]
				|        public int Flags
				|        {
				|            get { return Convert.ToInt32(flags.AsNumber()); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Элемент") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue view = null;
				|        [ContextProperty(""Элемент"", ""View"")]
				|        public IValue View
				|        {
				|            get { return view; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ЭтоAlt") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue isAlt = null;
				|        [ContextProperty(""ЭтоAlt"", ""IsAlt"")]
				|        public bool IsAlt
				|        {
				|            get { return isAlt.AsBoolean(); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ЭтоCapslock") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue isCapslock = null;
				|        [ContextProperty(""ЭтоCapslock"", ""IsCapslock"")]
				|        public bool IsCapslock
				|        {
				|            get { return isCapslock.AsBoolean(); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ЭтоCtrl") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue isCtrl = null;
				|        [ContextProperty(""ЭтоCtrl"", ""IsCtrl"")]
				|        public bool IsCtrl
				|        {
				|            get { return isCtrl.AsBoolean(); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ЭтоNumlock") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue isNumlock = null;
				|        [ContextProperty(""ЭтоNumlock"", ""IsNumlock"")]
				|        public bool IsNumlock
				|        {
				|            get { return isNumlock.AsBoolean(); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ЭтоScrolllock") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue isScrolllock = null;
				|        [ContextProperty(""ЭтоScrolllock"", ""IsScrolllock"")]
				|        public bool IsScrolllock
				|        {
				|            get { return isScrolllock.AsBoolean(); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ЭтоShift") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue isShift = null;
				|        [ContextProperty(""ЭтоShift"", ""IsShift"")]
				|        public bool IsShift
				|        {
				|            get { return isShift.AsBoolean(); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Размер") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public TfSize size = null;
				|        [ContextProperty(""Размер"", ""Size"")]
				|        public TfSize Size
				|        {
				|            get { return size; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Отмена") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue cancel = null;
				|        [ContextProperty(""Отмена"", ""Cancel"")]
				|        public bool Cancel
				|        {
				|            get { return cancel.AsBoolean(); }
				|            set { cancel = ValueFactory.Create(value); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Заголовок") и (КлассАнгл = "Border") Тогда
				Стр = Стр +
				"        [ContextProperty(""Заголовок"", ""Title"")]
				|        public string Title
				|        {
				|            get { return Base_obj.Title; }
				|            set { Base_obj.Title = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Смещение3D") и (КлассАнгл = "Border") Тогда
				Стр = Стр +
				"        [ContextProperty(""Смещение3D"", ""Effect3DOffset"")]
				|        public TfPoint Effect3DOffset
				|        {
				|            get { return new TfPoint(Base_obj.Effect3DOffset); }
				|            set { Base_obj.Effect3DOffset = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "СтильГраницы") и (КлассАнгл = "Border") Тогда
				Стр = Стр +
				"        [ContextProperty(""СтильГраницы"", ""BorderStyle"")]
				|        public int BorderStyle
				|        {
				|            get { return Base_obj.BorderStyle; }
				|            set { Base_obj.BorderStyle = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ТолщинаГраницы") и (КлассАнгл = "Border") Тогда
				Стр = Стр +
				"        [ContextProperty(""ТолщинаГраницы"", ""BorderThickness"")]
				|        public TfThickness BorderThickness
				|        {
				|            get { return new TfThickness(Base_obj.BorderThickness); }
				|            set { Base_obj.BorderThickness = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Цвет3D") и (КлассАнгл = "Border") Тогда
				Стр = Стр +
				"        [ContextProperty(""Цвет3D"", ""Effect3DBrush"")]
				|        public TfAttribute Effect3DBrush
				|        {
				|            get { return new TfAttribute(Base_obj.Effect3DBrush); }
				|            set { Base_obj.Effect3DBrush = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ЦветГраницы") и (КлассАнгл = "Border") Тогда
				Стр = Стр +
				"        [ContextProperty(""ЦветГраницы"", ""BorderBrush"")]
				|        public int BorderBrush
				|        {
				|            get { return Base_obj.BorderBrush; }
				|            set { Base_obj.BorderBrush = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "СимволКлавиши") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue keySymbol = null;
				|        [ContextProperty(""СимволКлавиши"", ""KeySymbol"")]
				|        public string KeySymbol
				|        {
				|            get { return keySymbol.AsString(); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Граница") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public TfBorder border = null;
				|        [ContextProperty(""Граница"", ""Border"")]
				|        public TfBorder Border
				|        {
				|            get { return border; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Граница") Тогда
				Стр = Стр +
				"        [ContextProperty(""Граница"", ""Border"")]
				|        public TfBorder Border
				|        {
				|            get { return Base_obj.Border.dll_obj; }
				|            set { Base_obj.Border = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Заголовок") и (КлассАнгл = "Window") Тогда
				Стр = Стр +
				"        [ContextProperty(""Заголовок"", ""Title"")]
				|        public string Title
				|        {
				|            get { return Base_obj.Title; }
				|            set { Base_obj.Title = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ПанельМеню") и (КлассАнгл = "Window") Тогда
				Стр = Стр +
				"        [ContextProperty(""ПанельМеню"", ""MenuBar"")]
				|        public TfMenuBar MenuBar
				|        {
				|            get { return Base_obj.MenuBar.dll_obj; }
				|            set { Base_obj.MenuBar = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "СтрокаСостояния") и (КлассАнгл = "Window") Тогда
				Стр = Стр +
				"        [ContextProperty(""СтрокаСостояния"", ""StatusBar"")]
				|        public TfStatusBar StatusBar
				|        {
				|            get { return Base_obj.StatusBar.dll_obj; }
				|            set { Base_obj.M_View.Add(value.Base_obj.M_View); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ВертикальноеВыравнивание") и (КлассАнгл = "TextFormatter") Тогда
				Стр = Стр +
				"        [ContextProperty(""ВертикальноеВыравнивание"", ""VerticalAlignment"")]
				|        public int VerticalAlignment
				|        {
				|            get { return Base_obj.VerticalAlignment; }
				|            set { Base_obj.VerticalAlignment = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Выравнивание") и (КлассАнгл = "TextFormatter") Тогда
				Стр = Стр +
				"        [ContextProperty(""Выравнивание"", ""Alignment"")]
				|        public int Alignment
				|        {
				|            get { return Base_obj.Alignment; }
				|            set { Base_obj.Alignment = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "НаправлениеТекста") и (КлассАнгл = "TextFormatter") Тогда
				Стр = Стр +
				"        [ContextProperty(""НаправлениеТекста"", ""Direction"")]
				|        public int Direction
				|        {
				|            get { return Base_obj.Direction; }
				|            set { Base_obj.Direction = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ГорячийНормальный") и (КлассАнгл = "ColorScheme") Тогда
				Стр = Стр +
				"        [ContextProperty(""ГорячийНормальный"", ""HotNormal"")]
				|        public TfAttribute HotNormal
				|        {
				|            get { return Base_obj.HotNormal.dll_obj; }
				|            set { Base_obj.HotNormal = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ГорячийФокус") и (КлассАнгл = "ColorScheme") Тогда
				Стр = Стр +
				"        [ContextProperty(""ГорячийФокус"", ""HotFocus"")]
				|        public TfAttribute HotFocus
				|        {
				|            get { return Base_obj.HotFocus.dll_obj; }
				|            set { Base_obj.HotFocus = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Нормальный") и (КлассАнгл = "ColorScheme") Тогда
				Стр = Стр +
				"        [ContextProperty(""Нормальный"", ""Normal"")]
				|        public TfAttribute Normal
				|        {
				|            get { return Base_obj.Normal.dll_obj; }
				|            set { Base_obj.Normal = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Отключено") и (КлассАнгл = "ColorScheme") Тогда
				Стр = Стр +
				"        [ContextProperty(""Отключено"", ""Disabled"")]
				|        public TfAttribute Disabled
				|        {
				|            get { return Base_obj.Disabled.dll_obj; }
				|            set { Base_obj.Disabled = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Фокус") и (КлассАнгл = "ColorScheme") Тогда
				Стр = Стр +
				"        [ContextProperty(""Фокус"", ""Focus"")]
				|        public TfAttribute Focus
				|        {
				|            get { return Base_obj.Focus.dll_obj; }
				|            set { Base_obj.Focus = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Количество") и (КлассАнгл = "MenusCollection") Тогда
				Стр = Стр +
				"        [ContextProperty(""Количество"", ""Count"")]
				|        public int Count
				|        {
				|            get { return M_Object.Length; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Клавиша") и (КлассАнгл = "MenuBar") Тогда
				Стр = Стр +
				"        [ContextProperty(""Клавиша"", ""Key"")]
				|        public int Key
				|        {
				|            get { return Base_obj.Key; }
				|            set { Base_obj.Key = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ПодМеню") и (КлассАнгл = "MenuBar") Тогда
				Стр = Стр +
				"        [ContextProperty(""ПодМеню"", ""Menus"")]
				|        public TfMenusCollection Menus
				|        {
				|            get { return menusCollection; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ПоследнийФокус") и (КлассАнгл = "MenuBar") Тогда
				Стр = Стр +
				"        [ContextProperty(""ПоследнийФокус"", ""LastFocused"")]
				|        public IValue LastFocused
				|        {
				|            get { return Base_obj.LastFocused.dll_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Количество") и (КлассАнгл = "StatusBarItems") Тогда
				Стр = Стр +
				"        [ContextProperty(""Количество"", ""Count"")]
				|        public int Count
				|        {
				|            get { return M_Object.Length; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Элементы") и (КлассАнгл = "StatusBar") Тогда
				Стр = Стр +
				"        [ContextProperty(""Элементы"", ""Items"")]
				|        public TfStatusBarItems Items
				|        {
				|            get { return statusBarItems; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "СтильФлажка") и (КлассАнгл = "MenuItem") Тогда
				Стр = Стр +
				"        [ContextProperty(""СтильФлажка"", ""CheckType"")]
				|        public int CheckType
				|        {
				|            get { return Base_obj.CheckType; }
				|            set { Base_obj.CheckType = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Родитель") и (КлассАнгл = "MenuItem") Тогда
				Стр = Стр +
				"        [ContextProperty(""Родитель"", ""Parent"")]
				|        public IValue Parent
				|        {
				|            get { return Base_obj.Parent; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Количество") и (КлассАнгл = "MenuBarItemChildren") Тогда
				Стр = Стр +
				"        [ContextProperty(""Количество"", ""Count"")]
				|        public int Count
				|        {
				|            get { return M_Object.Length; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Родитель") и (КлассАнгл = "MenuBarItem") Тогда
				Стр = Стр +
				"        [ContextProperty(""Родитель"", ""Parent"")]
				|        public IValue Parent
				|        {
				|            get { return Base_obj.Parent; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Родитель") и (КлассАнгл = "Border") Тогда
				Стр = Стр +
				"        [ContextProperty(""Родитель"", ""Parent"")]
				|        public IValue Parent
				|        {
				|            get { return Base_obj.Parent; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Родитель") и (КлассАнгл = "Window") Тогда
				Стр = Стр +
				"        [ContextProperty(""Родитель"", ""SuperView"")]
				|        public IValue SuperView
				|        {
				|            get
				|            {
				|                try
				|                {
				|                    return OneScriptTerminalGui.RevertEqualsObj(Base_obj.M_Window.Subviews[0].SuperView.SuperView.SuperView).dll_obj;
				|                }
				|                catch (Exception)
				|                {
				|                    return OneScriptTerminalGui.RevertEqualsObj(Base_obj.M_Window.Subviews[0].SuperView.SuperView).dll_obj;
				|                }
				|            }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Родитель") Тогда
				Стр = Стр +
				"        [ContextProperty(""Родитель"", ""SuperView"")]
				|        public IValue SuperView
				|        {
				|            get { return OneScriptTerminalGui.RevertEqualsObj(Base_obj.SuperView.M_View).dll_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "СтильФлажка") и (КлассАнгл = "MenuBarItem") Тогда
				Стр = Стр +
				"        [ContextProperty(""СтильФлажка"", ""CheckType"")]
				|        public int CheckType
				|        {
				|            get { return Base_obj.CheckType; }
				|            set { Base_obj.CheckType = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Элементы") и (КлассАнгл = "MenuBarItem") Тогда
				Стр = Стр +
				"        [ContextProperty(""Элементы"", ""Children"")]
				|        public TfMenuBarItemChildren Children
				|        {
				|            get { return menuBarItemChildren; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Верхний") и (КлассАнгл = "Colors") Тогда
				Стр = Стр +
				"        [ContextProperty(""Верхний"", ""TopLevel"")]
				|        public TfColorScheme TopLevel
				|        {
				|            get { return new TfColorScheme(Base_obj.TopLevel); }
				|            set { Base_obj.TopLevel = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Диалог") и (КлассАнгл = "Colors") Тогда
				Стр = Стр +
				"        [ContextProperty(""Диалог"", ""Dialog"")]
				|        public TfColorScheme Dialog
				|        {
				|            get { return new TfColorScheme(Base_obj.Dialog); }
				|            set { Base_obj.Dialog = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Меню") и (КлассАнгл = "Colors") Тогда
				Стр = Стр +
				"        [ContextProperty(""Меню"", ""Menu"")]
				|        public TfColorScheme Menu
				|        {
				|            get { return new TfColorScheme(Base_obj.Menu); }
				|            set { Base_obj.Menu = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Основа") и (КлассАнгл = "Colors") Тогда
				Стр = Стр +
				"        [ContextProperty(""Основа"", ""Base"")]
				|        public TfColorScheme Base
				|        {
				|            get { return new TfColorScheme(Base_obj.Base); }
				|            set { Base_obj.Base = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Ошибка") и (КлассАнгл = "Colors") Тогда
				Стр = Стр +
				"        [ContextProperty(""Ошибка"", ""Error"")]
				|        public TfColorScheme Error
				|        {
				|            get { return new TfColorScheme(Base_obj.Error); }
				|            set { Base_obj.Error = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Высота") и (КлассАнгл = "Size") Тогда
				Стр = Стр +
				"        [ContextProperty(""Высота"", ""Height"")]
				|        public int Height
				|        {
				|            get { return Base_obj.Height; }
				|            set { Base_obj.Height = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Высота") и (КлассАнгл = "Rect") Тогда
				Стр = Стр +
				"        [ContextProperty(""Высота"", ""Height"")]
				|        public int Height
				|        {
				|            get { return Base_obj.Height; }
				|            set { Base_obj.Height = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Ширина") и (КлассАнгл = "Rect") Тогда
				Стр = Стр +
				"        [ContextProperty(""Ширина"", ""Width"")]
				|        public int Width
				|        {
				|            get { return Base_obj.Width; }
				|            set { Base_obj.Width = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Ширина") и (КлассАнгл = "Size") Тогда
				Стр = Стр +
				"        [ContextProperty(""Ширина"", ""Width"")]
				|        public int Width
				|        {
				|            get { return Base_obj.Width; }
				|            set { Base_obj.Width = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Высота") и (КлассАнгл = "MessageBox") Тогда
				Стр = Стр +
				"        private int height = 4;
				|        [ContextProperty(""Высота"", ""Height"")]
				|        public int Height
				|        {
				|            get { return height; }
				|            set { height = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Высота") Тогда
				Стр = Стр +
				"        [ContextProperty(""Высота"", ""Height"")]
				|        public TfDim Height
				|        {
				|            get { return Base_obj.Height.dll_obj; }
				|            set { Base_obj.Height = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Верх") и (КлассАнгл = "Thickness") Тогда
				Стр = Стр +
				"        [ContextProperty(""Верх"", ""Top"")]
				|        public int Top
				|        {
				|            get { return Base_obj.Top; }
				|            set { Base_obj.Top = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Низ") и (КлассАнгл = "Thickness") Тогда
				Стр = Стр +
				"        [ContextProperty(""Низ"", ""Bottom"")]
				|        public int Bottom
				|        {
				|            get { return Base_obj.Bottom; }
				|            set { Base_obj.Bottom = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Лево") и (КлассАнгл = "Thickness") Тогда
				Стр = Стр +
				"        [ContextProperty(""Лево"", ""Left"")]
				|        public int Left
				|        {
				|            get { return Base_obj.Left; }
				|            set { Base_obj.Left = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Право") и (КлассАнгл = "Thickness") Тогда
				Стр = Стр +
				"        [ContextProperty(""Право"", ""Right"")]
				|        public int Right
				|        {
				|            get { return Base_obj.Right; }
				|            set { Base_obj.Right = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Верх") и (КлассАнгл = "Rect") Тогда
				Стр = Стр +
				"        [ContextProperty(""Верх"", ""Top"")]
				|        public int Top
				|        {
				|            get { return Base_obj.Top; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Лево") и (КлассАнгл = "Rect") Тогда
				Стр = Стр +
				"        [ContextProperty(""Лево"", ""Left"")]
				|        public int Left
				|        {
				|            get { return Base_obj.Left; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Низ") и (КлассАнгл = "Rect") Тогда
				Стр = Стр +
				"        [ContextProperty(""Низ"", ""Bottom"")]
				|        public int Bottom
				|        {
				|            get { return Base_obj.Bottom; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Право") и (КлассАнгл = "Rect") Тогда
				Стр = Стр +
				"        [ContextProperty(""Право"", ""Right"")]
				|        public int Right
				|        {
				|            get { return Base_obj.Right; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Верх") Тогда
				Стр = Стр +
				"        [ContextProperty(""Верх"", ""Top"")]
				|        public TfPos Top
				|        {
				|            get { return new TfPos(Base_obj.Top); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Низ") Тогда
				Стр = Стр +
				"        [ContextProperty(""Низ"", ""Bottom"")]
				|        public TfPos Bottom
				|        {
				|            get { return new TfPos(Base_obj.Bottom); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Лево") Тогда
				Стр = Стр +
				"        [ContextProperty(""Лево"", ""Left"")]
				|        public TfPos Left
				|        {
				|            get { return new TfPos(Base_obj.Left); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Право") Тогда
				Стр = Стр +
				"        [ContextProperty(""Право"", ""Right"")]
				|        public TfPos Right
				|        {
				|            get { return new TfPos(Base_obj.Right); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Игрек") Тогда
				Стр = Стр +
				"        [ContextProperty(""Игрек"", ""Y"")]
				|        public TfPos Y
				|        {
				|            get { return new TfPos(Base_obj.Y); }
				|            set { Base_obj.Y = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Ширина") и (КлассАнгл = "MessageBox") Тогда
				Стр = Стр +
				"        private int width = 50;
				|        [ContextProperty(""Ширина"", ""Width"")]
				|        public int Width
				|        {
				|            get { return width; }
				|            set { width = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Ширина") Тогда
				Стр = Стр +
				"        [ContextProperty(""Ширина"", ""Width"")]
				|        public TfDim Width
				|        {
				|            get { return Base_obj.Width.dll_obj; }
				|            set { Base_obj.Width = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Границы") Тогда
				Стр = Стр +
				"        [ContextProperty(""Границы"", ""Bounds"")]
				|        public TfRect Bounds
				|        {
				|            get { return new TfRect(Base_obj.Frame.M_Rect.X, Base_obj.Frame.M_Rect.Y, Base_obj.Bounds.M_Rect.Width, Base_obj.Bounds.M_Rect.Height); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Кадр") Тогда
				Стр = Стр +
				"        [ContextProperty(""Кадр"", ""Frame"")]
				|        public TfRect Frame
				|        {
				|            get { return new TfRect(Base_obj.Frame.M_Rect.X, Base_obj.Frame.M_Rect.Y, Base_obj.Frame.M_Rect.Width, Base_obj.Frame.M_Rect.Height); }
				|            set { Base_obj.Frame = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ЦветоваяСхема") Тогда
				Стр = Стр +
				"        [ContextProperty(""ЦветоваяСхема"", ""ColorScheme"")]
				|        public TfColorScheme ColorScheme
				|        {
				|            get { return Base_obj.ColorScheme.dll_obj; }
				|            set { Base_obj.ColorScheme = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "СимволКлавишиВызова") Тогда
				Стр = Стр +
				"        [ContextProperty(""СимволКлавишиВызова"", ""HotKeySpecifier"")]
				|        public IValue HotKeySpecifier
				|        {
				|            get
				|            {
				|                if (Base_obj.HotKeySpecifier == (Rune)0xFFFF)
				|                {
				|                    return ValueFactory.CreateNullValue();
				|                }
				|                else
				|                {
				|                    return ValueFactory.Create(Base_obj.HotKeySpecifier.ToString());
				|                }
				|            }
				|            set
				|            {
				|                if (value.SystemType.Name == ""Неопределено"")
				|                {
				|                    Base_obj.HotKeySpecifier = (Rune)0xFFFF;
				|                }
				|                else
				|                {
				|                    Base_obj.HotKeySpecifier = value.AsString().ToCharArray()[0];
				|                }
				|            }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ВертикальноеВыравниваниеТекста") и (КлассАнгл = "Window") Тогда
				Стр = Стр +
				"        [ContextProperty(""ВертикальноеВыравниваниеТекста"", ""VerticalTextAlignment"")]
				|        public int VerticalTextAlignment
				|        {
				|            get { return (int)Base_obj.M_Window.Subviews[0].VerticalTextAlignment; }
				|            set { Base_obj.M_Window.Subviews[0].VerticalTextAlignment = (Terminal.Gui.VerticalTextAlignment)value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ВФокусе") и (КлассАнгл = "Window") Тогда
				Стр = Стр +
				"        [ContextProperty(""ВФокусе"", ""Focused"")]
				|        public IValue Focused
				|        {
				|            get
				|            {
				|                if (Base_obj.M_Window.Subviews[0].Focused != null)
				|                {
				|                    return OneScriptTerminalGui.RevertEqualsObj(Base_obj.M_Window.Subviews[0].Focused).dll_obj;
				|                }
				|                return null;
				|            }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ВыравниваниеТекста") и (КлассАнгл = "Window") Тогда
				Стр = Стр +
				"        [ContextProperty(""ВыравниваниеТекста"", ""TextAlignment"")]
				|        public int TextAlignment
				|        {
				|            get { return (int)Base_obj.M_Window.Subviews[0].TextAlignment; }
				|            set { Base_obj.M_Window.Subviews[0].TextAlignment = (Terminal.Gui.TextAlignment)value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ИгнорироватьГраницуПриПерерисовке") и (КлассАнгл = "Window") Тогда
				Стр = Стр +
				"        [ContextProperty(""ИгнорироватьГраницуПриПерерисовке"", ""IgnoreBorderPropertyOnRedraw"")]
				|        public bool IgnoreBorderPropertyOnRedraw
				|        {
				|            get { return Base_obj.M_Window.Subviews[0].IgnoreBorderPropertyOnRedraw; }
				|            set { Base_obj.M_Window.Subviews[0].IgnoreBorderPropertyOnRedraw = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "НаправлениеТекста") и (КлассАнгл = "Window") Тогда
				Стр = Стр +
				"        [ContextProperty(""НаправлениеТекста"", ""TextDirection"")]
				|        public int TextDirection
				|        {
				|            get { return (int)Base_obj.M_Window.Subviews[0].TextDirection; }
				|            set { Base_obj.M_Window.Subviews[0].TextDirection = (Terminal.Gui.TextDirection)value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ОформительТекста") и (КлассАнгл = "Window") Тогда
				Стр = Стр +
				"        [ContextProperty(""ОформительТекста"", ""TextFormatter"")]
				|        public TfTextFormatter TextFormatter
				|        {
				|            get { return OneScriptTerminalGui.RevertEqualsObj(Base_obj.M_Window.Subviews[0].TextFormatter).dll_obj; }
				|            set { Base_obj.M_Window.Subviews[0].TextFormatter = value.Base_obj.M_TextFormatter; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ОформительТекста") Тогда
				Стр = Стр +
				"        [ContextProperty(""ОформительТекста"", ""TextFormatter"")]
				|        public TfTextFormatter TextFormatter
				|        {
				|            get { return Base_obj.TextFormatter.dll_obj; }
				|            set { Base_obj.TextFormatter = value.Base_obj; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Линии") Тогда
				Стр = Стр +
				"        [ContextProperty(""Линии"", ""Lines"")]
				|        public ArrayImpl Lines
				|        {
				|            get { return Base_obj.Lines; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Заголовок") и (КлассАнгл = "MessageBox") Тогда
				Стр = Стр +
				"        private string title = ""ОкноСообщений"";
				|        [ContextProperty(""Заголовок"", ""Title"")]
				|        public string Title
				|        {
				|            get { return title; }
				|            set { title = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ИндексКнопкиПоУмолчанию") и (КлассАнгл = "MessageBox") Тогда
				Стр = Стр +
				"        private int defaultButtonIndex = 0;
				|        [ContextProperty(""ИндексКнопкиПоУмолчанию"", ""DefaultButtonIndex"")]
				|        public int DefaultButtonIndex
				|        {
				|            get { return defaultButtonIndex; }
				|            set { defaultButtonIndex = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Кнопки") и (КлассАнгл = "MessageBox") Тогда
				Стр = Стр +
				"        private ArrayImpl buttons;
				|        [ContextProperty(""Кнопки"", ""Buttons"")]
				|        public ArrayImpl Buttons
				|        {
				|            get { return buttons; }
				|            set { buttons = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Сообщение") и (КлассАнгл = "MessageBox") Тогда
				Стр = Стр +
				"        private string message = """";
				|        [ContextProperty(""Сообщение"", ""Message"")]
				|        public string Message
				|        {
				|            get { return message; }
				|            set { message = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "АвтоРазмер") и (КлассАнгл = "MessageBox") Тогда
				Стр = Стр +
				"        private bool autoSize = true;
				|        [ContextProperty(""АвтоРазмер"", ""AutoSize"")]
				|        public bool AutoSize
				|        {
				|            get { return autoSize; }
				|            set { autoSize = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Интервал") и (КлассАнгл = "Timer") Тогда
				Стр = Стр +
				"        private int interval = 0;
				|        [ContextProperty(""Интервал"", ""Interval"")]
				|        public int Interval
				|        {
				|            get { return interval; }
				|            set { interval = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "Интервал") и (КлассАнгл = "MessageBox") Тогда
				Стр = Стр +
				"        private int interval = 0;
				|        [ContextProperty(""Интервал"", ""Interval"")]
				|        public int Interval
				|        {
				|            get { return interval; }
				|            set { interval = value; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "НовыйЗаголовок") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue newTitle = null;
				|        [ContextProperty(""НовыйЗаголовок"", ""NewTitle"")]
				|        public string NewTitle
				|        {
				|            get { return newTitle.AsString(); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "СтарыйЗаголовок") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue oldTitle = null;
				|        [ContextProperty(""СтарыйЗаголовок"", ""OldTitle"")]
				|        public string OldTitle
				|        {
				|            get { return oldTitle.AsString(); }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ЭлементМеню") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue menuItem = null;
				|        [ContextProperty(""ЭлементМеню"", ""MenuItem"")]
				|        public IValue MenuItem
				|        {
				|            get { return menuItem; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "ТекущийПунктМеню") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue currentMenu = null;
				|        [ContextProperty(""ТекущийПунктМеню"", ""CurrentMenu"")]
				|        public IValue CurrentMenu
				|        {
				|            get { return currentMenu; }
				|        }
				|
				|";
			ИначеЕсли (СвойствоРус = "НовыйПунктМеню") и (КлассАнгл = "EventArgs") Тогда
				Стр = Стр +
				"        public IValue newMenuBarItem = null;
				|        [ContextProperty(""НовыйПунктМеню"", ""NewMenuBarItem"")]
				|        public IValue NewMenuBarItem
				|        {
				|            get { return newMenuBarItem; }
				|            set { newMenuBarItem = value; }
				|        }
				|
				|";
				
				
				
				
				



				
				
				
				
				
				
				
				
				
			ИначеЕсли (СвойствоРус = "йййййййй") и (КлассАнгл = "йййййййй") Тогда
				Стр = Стр +
				"        [ContextProperty(""йййййййй"", ""йййййййй"")]


				|
				|";
				
				
				
				
				
			Иначе
				Если СтрИспользование = "Только чтение" Тогда
					Стр = Стр +
					"        [ContextProperty(""" + СвойствоРус + """, """ + СвойствоАнгл + """)]
					|        public " + ТипЗнач + " " + СвойствоАнгл + "
					|        {
					|            get { return Base_obj." + СвойствоАнгл + "; }
					|        }
					|
					|";
				ИначеЕсли СтрИспользование = "Чтение и запись" Тогда
					Стр = Стр +
					"        [ContextProperty(""" + СвойствоРус + """, """ + СвойствоАнгл + """)]
					|        public " + ТипЗнач + " " + СвойствоАнгл + "
					|        {
					|            get { return Base_obj." + СвойствоАнгл + "; }
					|            set { Base_obj." + СвойствоАнгл + " = value; }
					|        }
					|
					|";
				КонецЕсли;
			КонецЕсли;
		КонецЦикла;
	Иначе
		Стр = "";
	КонецЕсли;
	
	Возврат Стр;
КонецФункции//Свойства

Функция События(ФайлСобытий, КлассАнгл, КлассРус)
	ТекстДокСобытия = Новый ТекстовыйДокумент;
	КаталогНаДиске = Новый Файл(ФайлСобытий);
    Если Не (КаталогНаДиске.Существует()) Тогда
		Возврат "";
	КонецЕсли;
	ТекстДокСобытия.Прочитать(ФайлСобытий);
	СтрТекстДокСобытия = ТекстДокСобытия.ПолучитьТекст();
	М505 = СтрНайтиМежду(СтрТекстДокСобытия, "<TBODY>", "</TABLE>", Ложь, );
	Если Не (М505.Количество() > 1) Тогда
		Возврат "";
	КонецЕсли;
	СтрТаблицаСобытий = М505[1];
	Массив1 = СтрНайтиМежду(СтрТаблицаСобытий, "<TR vAlign=top>", "</TR>", Ложь, );
	// Сообщить("Массив1.Количество()=" + Массив1.Количество());
	Если Массив1.Количество() > 0 Тогда
		Стр = "";
		Для А = 0 По Массив1.ВГраница() Цикл
			//найдем первую ячейку строки таблицы
			М07 = СтрНайтиМежду(Массив1[А], "<TD width=""50%"">", "</TD>", Ложь, );
			СтрХ = М07[0];
			СтрХ = СтрЗаменить(СтрХ, "&nbsp;", " ");
			ИмяФайлаСобытия = КаталогСправки + "\" + СтрНайтиМежду(СтрХ, "<A href=""", """>", , )[0];
			// Сообщить("ИмяФайлаСобытия = " + ИмяФайлаСобытия);
			
			КаталогНаДиске = Новый Файл(ИмяФайлаСобытия);
			Если Не КаталогНаДиске.Существует() Тогда
				Продолжить;
			КонецЕсли;
			
			ТекстДокСобытия = Новый ТекстовыйДокумент;
			ТекстДокСобытия.Прочитать(ИмяФайлаСобытия);
			СтрТекстДокСобытия = ТекстДокСобытия.ПолучитьТекст();
			// <H1 class=dtH1>Кнопка.Нажатие&nbsp;(Button.Clicked)&nbsp;Событие</H1>
			М506 = СтрНайтиМежду(СтрТекстДокСобытия, "<H1 class=dtH1>", "Событие</H1>", , );
			Если М506.Количество() > 0 Тогда
				Стр506 = М506[0];
				Стр506 = СтрЗаменить(Стр506, "&nbsp;", ".");
				Стр506 = СтрЗаменить(Стр506, ".", " ");
				Стр506 = СтрЗаменить(Стр506, "(", "");
				Стр506 = СтрЗаменить(Стр506, ")", "");
				Стр506 = СокрЛП(Стр506);
				// Сообщить("Стр506 = " + Стр506);
			КонецЕсли;
			М507 = СтрРазделить(Стр506, " ");
			КлассРус = М507[0];
			СобытиеРус = М507[1];
			КлассАнгл = М507[2];
			СобытиеАнгл = М507[3];
			// Сообщить("КлассРус = " + КлассРус);
			// Сообщить("СобытиеРус = " + СобытиеРус);
			// Сообщить("КлассАнгл = " + КлассАнгл);
			// Сообщить("СобытиеАнгл = " + СобытиеАнгл);
			// Сообщить("====================================");
			
			Если (СобытиеРус = "йййййййй") и (КлассАнгл = "йййййййй") Тогда
				Стр = Стр +
				"        [ContextProperty(""ЭлементАктивирован"", ""InitializedItem"")]
				|        public TfAction InitializedItem
				|        {
				|            get { return Base_obj.Initialized; }
				|            set { Base_obj.Initialized = value; }
				|        }
				|
				|";
			ИначеЕсли (СобытиеРус = "йййййййй") и (КлассАнгл = "йййййййй") Тогда
				Стр = Стр +
				"        [ContextMethod(""Закрыть"", ""Close"")]
				|        public void Close()
				|        {
				|            string strFunc = ""mapKeyEl.get(\u0022"" + ItemKey + ""\u0022).close();"";
				|            йййййййййййййй.SendStrFunc(strFunc);
				|        }
				|
				|";
				

			
			Иначе	
				Стр = Стр +
				"        [ContextProperty(""" + СобытиеРус + """, """ + СобытиеАнгл + """)]
				|        public TfAction " + СобытиеАнгл + " { get; set; }
				|
				|";
			КонецЕсли;
		КонецЦикла;
	Иначе
		Стр = "" + Символы.ПС;
	КонецЕсли;
	
	Возврат Стр;
	// Возврат "";
КонецФункции//События

Функция Методы(ФайлМетодов, КлассАнгл)
	ТекстДокМетоды = Новый ТекстовыйДокумент;
	КаталогНаДиске = Новый Файл(ФайлМетодов);
    Если Не (КаталогНаДиске.Существует()) Тогда
		Возврат "";
	КонецЕсли;
	ТекстДокМетоды.Прочитать(ФайлМетодов);
	СтрТекстДокМетоды = ТекстДокМетоды.ПолучитьТекст();
	М505 = СтрНайтиМежду(СтрТекстДокМетоды, "<TBODY>", "</TABLE>", Ложь, );
	Если Не (М505.Количество() > 1) Тогда
		Возврат "";
	КонецЕсли;
	СтрТаблицаСвойств = М505[1];
	Массив1 = СтрНайтиМежду(СтрТаблицаСвойств, "<TR vAlign=top>", "</TR>", Ложь, );
	// Сообщить("Массив1.Количество()=" + Массив1.Количество());
	Если Массив1.Количество() > 0 Тогда
		Стр = "";
		Для А = 0 По Массив1.ВГраница() Цикл
			М07 = СтрНайтиМежду(Массив1[А], "<TD width=""50%"">", "</TD>", Ложь, );
			СтрХ = М07[0];
			СтрХ = СтрЗаменить(СтрХ, "&nbsp;", " ");
			
			ИмяФайлаМетода = КаталогСправки + "\" + СтрНайтиМежду(СтрХ, "<A href=""", """>", , )[0];
			
			КаталогНаДиске = Новый Файл(ИмяФайлаМетода);
			Если Не КаталогНаДиске.Существует() Тогда
				Продолжить;
			КонецЕсли;
			
			ТекстДокСвойства = Новый ТекстовыйДокумент;
			ТекстДокСвойства.Прочитать(ИмяФайлаМетода);
			СтрТекстДокМетода = ТекстДокСвойства.ПолучитьТекст();
			// <H1 class=dtH1>Верхний.Добавить (Toplevel.Add)&nbsp;Метод</H1>
			М506 = СтрНайтиМежду(СтрТекстДокМетода, "<H1 class=dtH1>", "Метод</H1>", , );
			Если М506.Количество() > 0 Тогда
				Стр506 = М506[0];
				Стр506 = СтрЗаменить(Стр506, "&nbsp;", "");
				Стр506 = СтрЗаменить(Стр506, ".", " ");
				Стр506 = СтрЗаменить(Стр506, "(", "");
				Стр506 = СтрЗаменить(Стр506, ")", "");
				// Сообщить("Стр506 = " + Стр506);
			КонецЕсли;
			М507 = СтрРазделить(Стр506, " ");
			КлассРус = М507[0];
			МетодРус = М507[1];
			КлассАнгл = М507[2];
			МетодАнгл = М507[3];
			// Сообщить("КлассРус = " + КлассРус);
			// Сообщить("МетодРус = " + МетодРус);
			// Сообщить("КлассАнгл = " + КлассАнгл);
			// Сообщить("МетодАнгл = " + МетодАнгл);
			// Сообщить("====================================");
			
			Если (МетодРус = "йййййййй") и (КлассАнгл = "йййййййй") Тогда
				Стр = Стр +
				"        [ContextMethod(""Закрыть"", ""Close"")]
				|        public void Close()
				|        {
				|            string strFunc = ""mapKeyEl.get(\u0022"" + ItemKey + ""\u0022).close();"";
				|            йййййййййййййй.SendStrFunc(strFunc);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Добавить") и (КлассАнгл = "Point") Тогда
				Стр = Стр +
				"        [ContextMethod(""Добавить"", ""Add"")]
				|        public TfPoint Add(TfPoint p1, TfSize p2)
				|        {
				|            Terminal.Gui.Point point1 = Terminal.Gui.Point.Add(p1.Base_obj.M_Point, p2.Base_obj.M_Size);
				|            return new TfPoint(point1.X, point1.Y);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Добавить") и (КлассАнгл = "Size") Тогда
				Стр = Стр +
				"        [ContextMethod(""Добавить"", ""Add"")]
				|        public TfSize Add(TfSize p1, TfSize p2)
				|        {
				|            Terminal.Gui.Size size1 = Terminal.Gui.Size.Add(p1.Base_obj.M_Size, p2.Base_obj.M_Size);
				|            return new TfSize(size1.Width, size1.Height);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Добавить") и (КлассАнгл = "Toplevel") Тогда
				Стр = Стр +
				"        [ContextMethod(""Добавить"", ""Add"")]
				|        public void Add(IValue p1)
				|        {
				|            Base_obj.Add(((dynamic)p1).Base_obj);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Добавить") и (КлассАнгл = "MenusCollection") Тогда
				Стр = Стр +
				"        [ContextMethod(""Добавить"", ""Add"")]
				|        public void Add(TfMenuBarItem p1)
				|        {
				|            Terminal.Gui.MenuBarItem[] MenuBarItem2 = new Terminal.Gui.MenuBarItem[M_Object.Length + 1];
				|            M_Object.CopyTo(MenuBarItem2, 0);
				|            MenuBarItem2[M_Object.Length] = p1.Base_obj.M_MenuBarItem;
				|            M_Object = MenuBarItem2;
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Добавить") и (КлассАнгл = "StatusBarItems") Тогда
				Стр = Стр +
				"        [ContextMethod(""Добавить"", ""Add"")]
				|        public TfStatusItem Add(TfStatusItem p1)
				|        {
				|            Terminal.Gui.StatusItem[] StatusItem2 = new Terminal.Gui.StatusItem[M_Object.Length + 1];
				|            M_Object.CopyTo(StatusItem2, 0);
				|            StatusItem2[M_Object.Length] = p1.Base_obj.M_StatusItem;
				|            M_Object = StatusItem2;
				|            return p1;
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Добавить") и (КлассАнгл = "MenuBarItemChildren") Тогда
				Стр = Стр +
				"        [ContextMethod(""Добавить"", ""Add"")]
				|        public void Add(IValue p1 = null)
				|        {
				|            if (p1 == null)
				|            {
				|                Terminal.Gui.MenuItem[] MenuItem2 = new Terminal.Gui.MenuItem[M_Object.Length + 1];
				|                M_Object.CopyTo(MenuItem2, 0);
				|                MenuItem2[M_Object.Length] = null;
				|                M_Object = MenuItem2;
				|            }
				|            else
				|            {
				|                Terminal.Gui.MenuItem[] MenuItem2 = new Terminal.Gui.MenuItem[M_Object.Length + 1];
				|                M_Object.CopyTo(MenuItem2, 0);
				|                MenuItem2[M_Object.Length] = (Terminal.Gui.MenuItem)((dynamic)p1).Base_obj.M_MenuItem;
				|                M_Object = MenuItem2;
				|            }
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Добавить") Тогда
				Стр = Стр +
				"        [ContextMethod(""Добавить"", ""Add"")]
				|        public void Add(IValue p1)
				|        {
				|            Base_obj.Add(((dynamic)p1).Base_obj);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ВСтроку") Тогда
				Стр = Стр +
				"        [ContextMethod(""ВСтроку"", ""ToString"")]
				|        public new string ToString()
				|        {
				|            return Base_obj.ToString();
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Содержит") и (КлассАнгл = "SubviewCollection") Тогда
				Стр = Стр +
				"        [ContextMethod(""Содержит"", ""Contains"")]
				|        public bool Contains(IValue p1)
				|        {
				|            return Base_obj.M_SubviewCollection.Contains((Terminal.Gui.View)((dynamic)p1).Base_obj.M_View);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Содержит") и (КлассАнгл = "Rect") Тогда
				Стр = Стр +
				"        [ContextMethod(""Содержит"", ""Contains"")]
				|        public IValue Contains(IValue p1, IValue p2 = null)
				|        {
				|            if (p1.GetType() == typeof(TfRect))
				|            {
				|                return ValueFactory.Create(Base_obj.Contains(((TfRect)p1).Base_obj.M_Rect));
				|            }
				|            else if (p1.GetType() == typeof(TfPoint))
				|            {
				|                return ValueFactory.Create(Base_obj.Contains(((TfPoint)p1).Base_obj.M_Point));
				|            }
				|            else if (p1.SystemType.Name == ""Число"" && p2 != null)
				|            {
				|                return ValueFactory.Create(Base_obj.Contains(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber())));
				|            }
				|            else
				|            {
				|                return ValueFactory.CreateNullValue();
				|            }
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Получить") и (КлассАнгл = "Attribute") Тогда
				Стр = Стр +
				"        [ContextMethod(""Получить"", ""Get"")]
				|        public TfAttribute Get()
				|        {
				|            return new TfAttribute(Base_obj.Get());
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Создать") и (КлассАнгл = "Attribute") Тогда
				Стр = Стр +
				"        [ContextMethod(""Создать"", ""Make"")]
				|        public TfAttribute Make(int p1, int p2)
				|        {
				|            return new TfAttribute(Base_obj.Make(p1, p2));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ВерхнийРодитель") и (КлассАнгл = "Window") Тогда
				Стр = Стр +
				"        [ContextMethod(""ВерхнийРодитель"", ""GetTopSuperView"")]
				|        public IValue GetTopSuperView()
				|        {
				|            try
				|            {
				|                return OneScriptTerminalGui.RevertEqualsObj(Base_obj.M_Window.Subviews[0].SuperView.SuperView.SuperView.GetTopSuperView()).dll_obj;
				|            }
				|            catch (Exception)
				|            {
				|                return OneScriptTerminalGui.RevertEqualsObj(Base_obj.M_Window.Subviews[0].SuperView.SuperView.GetTopSuperView()).dll_obj;
				|            }
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ВерхнийРодитель") Тогда
				Стр = Стр +
				"        [ContextMethod(""ВерхнийРодитель"", ""GetTopSuperView"")]
				|        public IValue GetTopSuperView()
				|        {
				|            return Base_obj.GetTopSuperView().dll_obj;
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "НаШагВперед") и (КлассАнгл = "Window") Тогда
				Стр = Стр +
				"        [ContextMethod(""НаШагВперед"", ""BringSubviewForward"")]
				|        public void BringSubviewForward(IValue p1)
				|        {
				|            Base_obj.M_Window.Subviews[0].BringSubviewForward(((dynamic)p1).Base_obj.M_View);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "НаШагВперед") Тогда
				Стр = Стр +
				"        [ContextMethod(""НаШагВперед"", ""BringSubviewForward"")]
				|        public void BringSubviewForward(IValue p1)
				|        {
				|            Base_obj.BringSubviewForward(((dynamic)p1).Base_obj);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Обновить") Тогда
				Стр = Стр +
				"        [ContextMethod(""Обновить"", ""SetNeedsDisplay"")]
				|        public void SetNeedsDisplay(TfRect p1 = null)
				|        {
				|            Base_obj.SetNeedsDisplay(p1.Base_obj);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "НаЗаднийПлан") и (КлассАнгл = "Window") Тогда
				Стр = Стр +
				"        [ContextMethod(""НаЗаднийПлан"", ""SendSubviewToBack"")]
				|        public void SendSubviewToBack(IValue p1)
				|        {
				|            Base_obj.M_Window.Subviews[0].SendSubviewToBack(((dynamic)p1).Base_obj.M_View);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "НаЗаднийПлан") Тогда
				Стр = Стр +
				"        [ContextMethod(""НаЗаднийПлан"", ""SendSubviewToBack"")]
				|        public void SendSubviewToBack(IValue p1)
				|        {
				|            Base_obj.SendSubviewToBack(((dynamic)p1).Base_obj);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "НаШагНазад") и (КлассАнгл = "Window") Тогда
				Стр = Стр +
				"        [ContextMethod(""НаШагНазад"", ""SendSubviewBackwards"")]
				|        public void SendSubviewBackwards(IValue p1)
				|        {
				|            Base_obj.M_Window.Subviews[0].SendSubviewBackwards(((dynamic)p1).Base_obj.M_View);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "НаШагНазад") Тогда
				Стр = Стр +
				"        [ContextMethod(""НаШагНазад"", ""SendSubviewBackwards"")]
				|        public void SendSubviewBackwards(IValue p1)
				|        {
				|            Base_obj.SendSubviewBackwards(((dynamic)p1).Base_obj);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Очистить") и (КлассАнгл = "MenusCollection") Тогда
				Стр = Стр +
				"        [ContextMethod(""Очистить"", ""Clear"")]
				|        public void Clear()
				|        {
				|            M_Object = new Terminal.Gui.MenuBarItem[0];
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Удалить") и (КлассАнгл = "MenuBarItemChildren") Тогда
				Стр = Стр +
				"        [ContextMethod(""Удалить"", ""Remove"")]
				|        public void Remove(IValue p1)
				|        {
				|            Terminal.Gui.MenuItem[] MenuItem2 = new Terminal.Gui.MenuItem[M_Object.Length - 1];
				|            int index = 0;
				|            for (int i = 0; i < M_Object.Length; i++)
				|            {
				|                Terminal.Gui.MenuItem MenuItem1 = M_Object[i];
				|                if (MenuItem1 != ((dynamic)p1).Base_obj.M_MenuItem)
				|                {
				|                    MenuItem2[index] = MenuItem1;
				|                    index++;
				|                }
				|            }
				|            M_Object = MenuItem2;
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Удалить") и (КлассАнгл = "StatusBar") Тогда
				Стр = Стр +
				"        [ContextMethod(""Удалить"", ""Remove"")]
				|        public void Remove(TfStatusItem p1)
				|        {
				|            Items.Remove(p1);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Удалить") и (КлассАнгл = "MenusCollection") Тогда
				Стр = Стр +
				"        [ContextMethod(""Удалить"", ""Remove"")]
				|        public void Remove(TfMenuBarItem p1)
				|        {
				|            Terminal.Gui.MenuBarItem[] MenuBarItem2 = new Terminal.Gui.MenuBarItem[M_Object.Length - 1];
				|            int index = 0;
				|            for (int i = 0; i < M_Object.Length; i++)
				|            {
				|                Terminal.Gui.MenuBarItem MenuBarItem1 = M_Object[i];
				|                if (MenuBarItem1 != p1.Base_obj.M_MenuBarItem)
				|                {
				|                    MenuBarItem2[index] = MenuBarItem1;
				|                    index++;
				|                }
				|            }
				|            M_Object = MenuBarItem2;
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Очистить") и (КлассАнгл = "StatusBarItems") Тогда
				Стр = Стр +
				"        [ContextMethod(""Очистить"", ""Clear"")]
				|        public void Clear()
				|        {
				|            M_Object = new Terminal.Gui.StatusItem[0];
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Очистить") и (КлассАнгл = "MenuBarItemChildren") Тогда
				Стр = Стр +
				"        [ContextMethod(""Очистить"", ""Clear"")]
				|        public void Clear()
				|        {
				|            M_Object = new Terminal.Gui.MenuItem[0];
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Перерисовать") Тогда
				Стр = Стр +
				"        [ContextMethod(""Перерисовать"", ""Redraw"")]
				|        public void Redraw(TfRect p1)
				|        {
				|            Base_obj.Redraw(p1.Base_obj);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ПерерисоватьДочерние") Тогда
				Стр = Стр +
				"        [ContextMethod(""ПерерисоватьДочерние"", ""SetChildNeedsDisplay"")]
				|        public void SetChildNeedsDisplay()
				|        {
				|            Base_obj.SetChildNeedsDisplay();
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "УстановитьАвтоРазмер") Тогда
				Стр = Стр +
				"        [ContextMethod(""УстановитьАвтоРазмер"", ""SetAutoSize"")]
				|        public void SetAutoSize()
				|        {
				|            TfSize TfSize1 = GetAutoSize();
				|            Width = new TfDim().Sized(TfSize1.Width);
				|            Height = new TfDim().Sized(TfSize1.Height);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ПолучитьАвтоРазмер") и (
				КлассАнгл = "Button" 
				или КлассАнгл = "Toplevel") 
				Тогда
				Стр = Стр +
				"        [ContextMethod(""ПолучитьАвтоРазмер"", ""GetAutoSize"")]
				|        public TfSize GetAutoSize()
				|        {
				|            int offsetWidth = 0;
				|            int offsetHeight = 0;
				|            try
				|            {
				|                offsetWidth = Border.BorderThickness.Left + Border.BorderThickness.Right;
				|                offsetHeight = Border.BorderThickness.Top + Border.BorderThickness.Bottom;
				|            }
				|            catch { }
				|            int MaxWidthLine = Terminal.Gui.TextFormatter.MaxWidthLine(Text);
				|            int MaxLines = Terminal.Gui.TextFormatter.MaxLines(Text, MaxWidthLine);
				|            try
				|            {
				|                return new TfSize(MaxWidthLine + offsetWidth, MaxLines + offsetHeight);
				|            }
				|            catch
				|            {
				|                return null;
				|            }
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ПолучитьАвтоРазмер") и (КлассАнгл = "TextFormatter") Тогда
				Стр = Стр +
				"        [ContextMethod(""ПолучитьАвтоРазмер"", ""GetAutoSize"")]
				|        public TfSize GetAutoSize()
				|        {
				|            try
				|            {
				|                return new TfSize(MaxWidthLine(Text) + 2, MaxLines(Text, MaxWidthLine(Text)) + 2);
				|            }
				|            catch
				|            {
				|                return null;
				|            }
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ПолучитьАвтоРазмер") Тогда
				Стр = Стр +
				"        [ContextMethod(""ПолучитьАвтоРазмер"", ""GetAutoSize"")]
				|        public TfSize GetAutoSize()
				|        {
				|            int offsetWidth = 0;
				|            int offsetHeight = 0;
				|            try
				|            {
				|                offsetWidth = Border.BorderThickness.Left + Border.BorderThickness.Right;
				|                offsetHeight = Border.BorderThickness.Top + Border.BorderThickness.Bottom;
				|            }
				|            catch { }
				|            int MaxWidthLine = Terminal.Gui.TextFormatter.MaxWidthLine(Text);
				|            int MaxLines = Terminal.Gui.TextFormatter.MaxLines(Text, MaxWidthLine);
				|            try
				|            {
				|                return new TfSize(MaxWidthLine + 2 + offsetWidth, MaxLines + 2 + offsetHeight);
				|            }
				|            catch
				|            {
				|                return null;
				|            }
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "РазместитьПодэлементы") Тогда
				Стр = Стр +
				"        [ContextMethod(""РазместитьПодэлементы"", ""LayoutSubviews"")]
				|        public void LayoutSubviews()
				|        {
				|            Base_obj.LayoutSubviews();
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ТочкаНаЭлементе") Тогда
				Стр = Стр +
				"        [ContextMethod(""ТочкаНаЭлементе"", ""ScreenToView"")]
				|        public TfPoint ScreenToView(int p1, int p2)
				|        {
				|            return new TfPoint(Base_obj.ScreenToView(p1, p2));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Удалить") и (КлассАнгл = "StatusBarItems") Тогда
				Стр = Стр +
				"        [ContextMethod(""Удалить"", ""Remove"")]
				|        public void Remove(TfStatusItem p1)
				|        {
				|            Terminal.Gui.StatusItem[] StatusItem2 = new Terminal.Gui.StatusItem[M_Object.Length - 1];
				|            int index = 0;
				|            for (int i = 0; i < M_Object.Length; i++)
				|            {
				|                Terminal.Gui.StatusItem StatusItem1 = M_Object[i];
				|                if (StatusItem1 != p1.Base_obj.M_StatusItem)
				|                {
				|                    StatusItem2[index] = StatusItem1;
				|                    index++;
				|                }
				|            }
				|            M_Object = StatusItem2;
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Удалить") и (КлассАнгл = "MenuBar") Тогда
				Стр = Стр +
				"        [ContextMethod(""Удалить"", ""Remove"")]
				|        public void Remove(TfMenuBarItem p1)
				|        {
				|            Menus.Remove(p1);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Удалить") Тогда
				Стр = Стр +
				"        [ContextMethod(""Удалить"", ""Remove"")]
				|        public void Remove(IValue p1)
				|        {
				|            Base_obj.Remove(((dynamic)p1).Base_obj);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "УдалитьВсе") и (КлассАнгл = "StatusBar") Тогда
				Стр = Стр +
				"        [ContextMethod(""УдалитьВсе"", ""RemoveAll"")]
				|        public void RemoveAll()
				|        {
				|            Items.Clear();
				|        }

				|
				|";
			ИначеЕсли (МетодРус = "УдалитьВсе") и (КлассАнгл = "MenuBar") Тогда
				Стр = Стр +
				"        [ContextMethod(""УдалитьВсе"", ""RemoveAll"")]
				|        public void RemoveAll()
				|        {
				|            Menus.Clear();
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "УдалитьВсе") Тогда
				Стр = Стр +
				"        [ContextMethod(""УдалитьВсе"", ""RemoveAll"")]
				|        public void RemoveAll()
				|        {
				|            Base_obj.RemoveAll();
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "УстановитьФокус") Тогда
				Стр = Стр +
				"        [ContextMethod(""УстановитьФокус"", ""SetFocus"")]
				|        public void SetFocus()
				|        {
				|            Base_obj.SetFocus();
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ЦветВыделенного") Тогда
				Стр = Стр +
				"        [ContextMethod(""ЦветВыделенного"", ""GetHotNormalColor"")]
				|        public TfAttribute GetHotNormalColor()
				|        {
				|            return new TfAttribute(Base_obj.GetHotNormalColor());
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ЦветОбычного") Тогда
				Стр = Стр +
				"        [ContextMethod(""ЦветОбычного"", ""GetNormalColor"")]
				|        public TfAttribute GetNormalColor()
				|        {
				|            return new TfAttribute(Base_obj.GetNormalColor());
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ЦветФокуса") Тогда
				Стр = Стр +
				"        [ContextMethod(""ЦветФокуса"", ""GetFocusColor"")]
				|        public TfAttribute GetFocusColor()
				|        {
				|            return new TfAttribute(Base_obj.GetFocusColor());
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Вычесть") и (КлассАнгл = "Point") Тогда
				Стр = Стр +
				"        [ContextMethod(""Вычесть"", ""Subtract"")]
				|        public TfPoint Subtract(TfPoint p1, TfSize p2)
				|        {
				|            return new TfPoint(Base_obj.Subtract(p1.Base_obj, p2.Base_obj));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Вычесть") и (КлассАнгл = "Pos") Тогда
				Стр = Стр +
				"        [ContextMethod(""Вычесть"", ""Subtract"")]
				|        public TfPos Subtract(TfPos p1, TfPos p2)
				|        {
				|            return new TfPos(Base_obj.Subtract(p1.Base_obj, p2.Base_obj));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Вычесть") и (КлассАнгл = "Dim") Тогда
				Стр = Стр +
				"        [ContextMethod(""Вычесть"", ""Subtract"")]
				|        public TfDim Subtract(TfDim p1, TfDim p2)
				|        {
				|            return new TfDim(Base_obj.Subtract(p1.Base_obj, p2.Base_obj));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Вычесть") Тогда
				Стр = Стр +
				"        [ContextMethod(""Вычесть"", ""Subtract"")]
				|        public TfSize Subtract(TfSize p1, TfSize p2)
				|        {
				|            return new TfSize(Base_obj.Subtract(p1.Base_obj, p2.Base_obj));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ИзГраниц") Тогда
				Стр = Стр +
				"        [ContextMethod(""ИзГраниц"", ""FromLTRB"")]
				|        public TfRect FromLTRB(int p1, int p2, int p3, int p4)
				|        {
				|            return new TfRect(Base_obj.FromLTRB(p1, p2, p3, p4));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Образовать") Тогда
				Стр = Стр +
				"        [ContextMethod(""Образовать"", ""Inflate"")]
				|        public TfRect Inflate(IValue p1, IValue p2 = null, IValue p3 = null)
				|        {
				|            if (p1.GetType() == typeof(TfSize))
				|            {
				|                Base_obj.Inflate(((TfSize)p1).Base_obj.M_Size);
				|                return this;
				|            }
				|            else if (p1.SystemType.Name == ""Число"" && p2.SystemType.Name == ""Число"")
				|            {
				|                Base_obj.Inflate(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()));
				|                return this;
				|            }
				|            else if (p1.GetType() == typeof(TfRect) && p2.SystemType.Name == ""Число"" && p3.SystemType.Name == ""Число"")
				|            {
				|                return new TfRect(Base_obj.Inflate(((TfRect)p1).Base_obj, Convert.ToInt32(p2.AsNumber()), Convert.ToInt32(p3.AsNumber())));
				|            }
				|            else
				|            {
				|                return null;
				|            }
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Сместить") Тогда
				Стр = Стр +
				"        [ContextMethod(""Сместить"", ""Offset"")]
				|        public void Offset(int p1, int p2)
				|        {
				|            Base_obj.Offset(p1, p2);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Элемент") и (КлассАнгл = "SubviewCollection") Тогда
				Стр = Стр +
				"        [ContextMethod(""Элемент"", ""Item"")]
				|        public IValue Item(int p1)
				|        {
				|            return OneScriptTerminalGui.RevertEqualsObj(Base_obj.M_SubviewCollection[p1]).dll_obj;
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ВычислитьТолщину") и (КлассАнгл = "Border") Тогда
				Стр = Стр +
				"        [ContextMethod(""ВычислитьТолщину"", ""GetSumThickness"")]
				|        public TfThickness GetSumThickness()
				|        {
				|            return new TfThickness(Base_obj.GetSumThickness());
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "РисоватьВсеГраницы") и (КлассАнгл = "Border") Тогда
				Стр = Стр +
				"        [ContextMethod(""РисоватьВсеГраницы"", ""DrawFullContent"")]
				|        public void DrawFullContent()
				|        {
				|            Base_obj.DrawFullContent();
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "РисоватьГраницу") и (КлассАнгл = "Border") Тогда
				Стр = Стр +
				"        [ContextMethod(""РисоватьГраницу"", ""DrawContent"")]
				|        public void DrawContent(TfView view = null, bool fill = true)
				|        {
				|            Base_obj.DrawContent(view.Base_obj, fill);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "РисоватьЗаголовок") и (КлассАнгл = "Border") Тогда
				Стр = Стр +
				"        [ContextMethod(""РисоватьЗаголовок"", ""DrawTitle"")]
				|        public void DrawTitle(TfView p1, TfRect p2 = null)
				|        {
				|            if (p2 != null)
				|            {
				|                Base_obj.DrawTitle(p1.Base_obj, p2.Base_obj);
				|            }
				|            else
				|            {
				|                Base_obj.DrawTitle(p1.Base_obj);
				|            }
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Выровнять") и (КлассАнгл = "TextFormatter") Тогда
				Стр = Стр +
				"        [ContextMethod(""Выровнять"", ""Justify"")]
				|        public string Justify(string p1, int p2, string p3 = "" "", int p4 = 0)
				|        {
				|            return Base_obj.Justify(p1, p2, p3, (Terminal.Gui.TextDirection)p4);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "МаксимальнаяШирина") и (КлассАнгл = "TextFormatter") Тогда
				Стр = Стр +
				"        [ContextMethod(""МаксимальнаяШирина"", ""MaxWidth"")]
				|        public int MaxWidth(string p1, int p2)
				|        {
				|            return Base_obj.MaxWidth(p1, p2);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "МаксимальноСтрок") и (КлассАнгл = "TextFormatter") Тогда
				Стр = Стр +
				"        [ContextMethod(""МаксимальноСтрок"", ""MaxLines"")]
				|        public int MaxLines(string p1, int p2)
				|        {
				|            return Base_obj.MaxLines(p1, p2);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ПереносСлов") и (КлассАнгл = "TextFormatter") Тогда
				Стр = Стр +
				"        [ContextMethod(""ПереносСлов"", ""WordWrap"")]
				|        public string WordWrap(string p1, int p2, bool p3 = false, int p4 = 0, int p5 = 0)
				|        {
				|            return Base_obj.WordWrap(p1, p2, p3, p4, (Terminal.Gui.TextDirection)p5);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Рисовать") и (КлассАнгл = "TextFormatter") Тогда
				Стр = Стр +
				"        [ContextMethod(""Рисовать"", ""Draw"")]
				|        public void Draw(TfRect p1, TfAttribute p2, TfAttribute p3, TfRect p4 = default, bool p5 = true)
				|        {
				|            Base_obj.Draw(p1.Base_obj, p2.Base_obj, p3.Base_obj, p4.Base_obj, p5);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "МаксимальнаяШиринаСтроки") и (КлассАнгл = "TextFormatter") Тогда
				Стр = Стр +
				"        [ContextMethod(""МаксимальнаяШиринаСтроки"", ""MaxWidthLine"")]
				|        public int MaxWidthLine(string p1)
				|        {
				|            return Base_obj.MaxWidthLine(p1);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ШиринаТекста") и (КлассАнгл = "TextFormatter") Тогда
				Стр = Стр +
				"        [ContextMethod(""ШиринаТекста"", ""GetTextWidth"")]
				|        public int GetTextWidth(string p1)
				|        {
				|            return Base_obj.GetTextWidth(p1);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ПунктМеню") и (КлассАнгл = "MenusCollection") Тогда
				Стр = Стр +
				"        [ContextMethod(""ПунктМеню"", ""MenuBarItem"")]
				|        public TfMenuBarItem MenuBarItem(int p1)
				|        {
				|            return OneScriptTerminalGui.RevertEqualsObj(M_Object[p1]).dll_obj;
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Закрыть") и (КлассАнгл = "MenuBar") Тогда
				Стр = Стр +
				"        [ContextMethod(""Закрыть"", ""CloseMenu"")]
				|        public bool CloseMenu()
				|        {
				|            return Base_obj.CloseMenu();
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Открыть") и (КлассАнгл = "MenuBar") Тогда
				Стр = Стр +
				"        [ContextMethod(""Открыть"", ""OpenMenu"")]
				|        public void OpenMenu()
				|        {
				|            Base_obj.OpenMenu();
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Абсолютно") и (КлассАнгл = "Pos") Тогда
				Стр = Стр +
				"        [ContextMethod(""Абсолютно"", ""At"")]
				|        public TfPos At(int p1)
				|        {
				|            return new TfPos(Base_obj.At(p1));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Центр") и (КлассАнгл = "Pos") Тогда
				Стр = Стр +
				"        [ContextMethod(""Центр"", ""Center"")]
				|        public TfPos Center()
				|        {
				|            return new TfPos(Base_obj.Center());
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ЯкорьКонец") и (КлассАнгл = "Pos") Тогда
				Стр = Стр +
				"        [ContextMethod(""ЯкорьКонец"", ""AnchorEnd"")]
				|        public TfPos AnchorEnd(int p1 = 0)
				|        {
				|            return new TfPos(Base_obj.AnchorEnd(p1));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ЭлементСтрокиСостояния") и (КлассАнгл = "StatusBarItems") Тогда
				Стр = Стр +
				"        [ContextMethod(""ЭлементСтрокиСостояния"", ""StatusItem"")]
				|        public TfStatusItem StatusItem(int p1)
				|        {
				|            return OneScriptTerminalGui.RevertEqualsObj(M_Object[p1]).dll_obj;
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ВставитьПоИндексу") и (КлассАнгл = "StatusBar") Тогда
				Стр = Стр +
				"        [ContextMethod(""ВставитьПоИндексу"", ""AddItemAt"")]
				|        public void AddItemAt(int p1, TfStatusItem p2)
				|        {
				|            Base_obj.AddItemAt(p1, p2.Base_obj);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ЭлементПунктаМеню") и (КлассАнгл = "MenuBarItemChildren") Тогда
				Стр = Стр +
				"        [ContextMethod(""ЭлементПунктаМеню"", ""ItemMenuBarItem"")]
				|        public TfMenuItem ItemMenuBarItem(int p1)
				|        {
				|            return OneScriptTerminalGui.RevertEqualsObj(M_Object[p1]).dll_obj;
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Высота") и (КлассАнгл = "Dim") Тогда
				Стр = Стр +
				"        [ContextMethod(""Высота"", ""Height"")]
				|        public TfDim Height(IValue p1)
				|        {
				|            return new TfDim(Base_obj.Height(((dynamic)p1).Base_obj));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Заполнить") и (КлассАнгл = "Dim") Тогда
				Стр = Стр +
				"        [ContextMethod(""Заполнить"", ""Fill"")]
				|        public TfDim Fill(int p1 = 0)
				|        {
				|            return new TfDim(Base_obj.Fill(p1));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Выше") Тогда
				Стр = Стр +
				"        [ContextMethod(""Выше"", ""PlaceTop"")]
				|        public void PlaceTop(IValue p1, int p2)
				|        {
				|            Base_obj.PlaceTop(((dynamic)p1.AsObject()).Base_obj, p2);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Левее") Тогда
				Стр = Стр +
				"        [ContextMethod(""Левее"", ""PlaceLeft"")]
				|        public void PlaceLeft(IValue p1, int p2)
				|        {
				|            Base_obj.PlaceLeft(((dynamic)p1.AsObject()).Base_obj, p2);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Ниже") Тогда
				Если КлассАнгл = "Window" Тогда
					Величина1 = "p2 - 1";
				Иначе
					Величина1 = "p2";
				КонецЕсли;
				Стр = Стр +
				"        [ContextMethod(""Ниже"", ""PlaceBottom"")]
				|        public void PlaceBottom(IValue p1, int p2)
				|        {
				|            Base_obj.PlaceBottom(((dynamic)p1.AsObject()).Base_obj, " + Величина1 + ");
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Правее") Тогда
				Стр = Стр +
				"        [ContextMethod(""Правее"", ""PlaceRight"")]
				|        public void PlaceRight(IValue p1, int p2)
				|        {
				|            Base_obj.PlaceRight(((dynamic)p1.AsObject()).Base_obj, p2);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Абсолютно") и (КлассАнгл = "Dim") Тогда
				Стр = Стр +
				"        [ContextMethod(""Абсолютно"", ""Sized"")]
				|        public TfDim Sized(int p1)
				|        {
				|            return new TfDim(Base_obj.Sized(p1));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Ширина") и (КлассАнгл = "Dim") Тогда
				Стр = Стр +
				"        [ContextMethod(""Ширина"", ""Width"")]
				|        public TfDim Width(IValue p1)
				|        {
				|            return new TfDim(Base_obj.Width(((dynamic)p1).Base_obj));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Сложить") и (КлассАнгл = "Pos") Тогда
				Стр = Стр +
				"        [ContextMethod(""Сложить"", ""Summation"")]
				|        public TfPos Summation(TfPos p1, TfPos p2)
				|        {
				|            return new TfPos(Base_obj.Summation(p1.Base_obj, p2.Base_obj));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Верх") и (КлассАнгл = "Pos") Тогда
				Стр = Стр +
				"        [ContextMethod(""Верх"", ""Top"")]
				|        public TfPos Top(IValue p1)
				|        {
				|            return new TfPos(Base_obj.Top(((dynamic)p1).Base_obj));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Игрек") и (КлассАнгл = "Pos") Тогда
				Стр = Стр +
				"        [ContextMethod(""Игрек"", ""Y"")]
				|        public TfPos Y(IValue p1)
				|        {
				|            return new TfPos(Base_obj.Y(((dynamic)p1).Base_obj));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Икс") и (КлассАнгл = "Pos") Тогда
				Стр = Стр +
				"        [ContextMethod(""Икс"", ""X"")]
				|        public TfPos X(IValue p1)
				|        {
				|            return new TfPos(Base_obj.X(((dynamic)p1).Base_obj));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Лево") и (КлассАнгл = "Pos") Тогда
				Стр = Стр +
				"        [ContextMethod(""Лево"", ""Left"")]
				|        public TfPos Left(IValue p1)
				|        {
				|            return new TfPos(Base_obj.Left(((dynamic)p1).Base_obj));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Низ") и (КлассАнгл = "Pos") Тогда
				Стр = Стр +
				"        [ContextMethod(""Низ"", ""Bottom"")]
				|        public TfPos Bottom(IValue p1)
				|        {
				|            return new TfPos(Base_obj.Bottom(((dynamic)p1).Base_obj));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Право") и (КлассАнгл = "Pos") Тогда
				Стр = Стр +
				"        [ContextMethod(""Право"", ""Right"")]
				|        public TfPos Right(IValue p1)
				|        {
				|            return new TfPos(Base_obj.Right(((dynamic)p1).Base_obj));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Процент") и (КлассАнгл = "Pos") Тогда
				Стр = Стр +
				"        [ContextMethod(""Процент"", ""Percent"")]
				|        public TfPos Percent(IValue p1)
				|        {
				|            return new TfPos(Base_obj.Percent(Convert.ToSingle(p1.AsNumber())));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Процент") и (КлассАнгл = "Dim") Тогда
				Стр = Стр +
				"        [ContextMethod(""Процент"", ""Percent"")]
				|        public TfDim Percent(IValue p1, bool p2 = false)
				|        {
				|            return new TfDim(Base_obj.Percent(Convert.ToSingle(p1.AsNumber()), p2));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Сложить") и (КлассАнгл = "Dim") Тогда
				Стр = Стр +
				"        [ContextMethod(""Сложить"", ""Summation"")]
				|        public TfDim Summation(TfDim p1, TfDim p2)
				|        {
				|            return new TfDim(Base_obj.Summation(p1.Base_obj, p2.Base_obj));
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ВышеЛевее") Тогда
				Стр = Стр +
				"        [ContextMethod(""ВышеЛевее"", ""PlaceTopLeft"")]
				|        public void PlaceTopLeft(IValue p1, int p2, int p3)
				|        {
				|            Base_obj.PlaceTopLeft(((dynamic)p1.AsObject()).Base_obj, p2, p3);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ВышеПравее") Тогда
				Стр = Стр +
				"        [ContextMethod(""ВышеПравее"", ""PlaceTopRight"")]
				|        public void PlaceTopRight(IValue p1, int p2, int p3)
				|        {
				|            Base_obj.PlaceTopRight(((dynamic)p1.AsObject()).Base_obj, p2, p3);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ЛевееВыше") Тогда
				Стр = Стр +
				"        [ContextMethod(""ЛевееВыше"", ""PlaceLeftTop"")]
				|        public void PlaceLeftTop(IValue p1, int p2, int p3)
				|        {
				|            Base_obj.PlaceLeftTop(((dynamic)p1.AsObject()).Base_obj, p2, p3);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ЛевееНиже") Тогда
				Стр = Стр +
				"        [ContextMethod(""ЛевееНиже"", ""PlaceLeftBottom"")]
				|        public void PlaceLeftBottom(IValue p1, int p2, int p3)
				|        {
				|            Base_obj.PlaceLeftBottom(((dynamic)p1.AsObject()).Base_obj, p2, p3);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "НижеЛевее") Тогда
				Стр = Стр +
				"        [ContextMethod(""НижеЛевее"", ""PlaceBottomLeft"")]
				|        public void PlaceBottomLeft(IValue p1, int p2, int p3)
				|        {
				|            Base_obj.PlaceBottomLeft(((dynamic)p1.AsObject()).Base_obj, p2, p3);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "НижеПравее") Тогда
				Стр = Стр +
				"        [ContextMethod(""НижеПравее"", ""PlaceBottomRight"")]
				|        public void PlaceBottomRight(IValue p1, int p2, int p3)
				|        {
				|            Base_obj.PlaceBottomRight(((dynamic)p1.AsObject()).Base_obj, p2, p3);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ПравееВыше") Тогда
				Стр = Стр +
				"        [ContextMethod(""ПравееВыше"", ""PlaceRightTop"")]
				|        public void PlaceRightTop(IValue p1, int p2, int p3)
				|        {
				|            Base_obj.PlaceRightTop(((dynamic)p1.AsObject()).Base_obj, p2, p3);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "ПравееНиже") Тогда
				Стр = Стр +
				"        [ContextMethod(""ПравееНиже"", ""PlaceRightBottom"")]
				|        public void PlaceRightBottom(IValue p1, int p2, int p3)
				|        {
				|            Base_obj.PlaceRightBottom(((dynamic)p1.AsObject()).Base_obj, p2, p3);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Индекс") и (КлассАнгл = "SubviewCollection") Тогда
				Стр = Стр +
				"        [ContextMethod(""Индекс"", ""IndexOf"")]
				|        public int IndexOf(IValue p1)
				|        {
				|            int index1 = -1;
				|            for (int i = 0; i < Base_obj.M_SubviewCollection.Count; i++)
				|            {
				|                if (Base_obj.M_SubviewCollection[i] == ((dynamic)p1).Base_obj.M_View)
				|                {
				|                    index1 = i;
				|                    break;
				|                }
				|            }
				|            return index1;
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Центр") Тогда
				Стр = Стр +
				"        [ContextMethod(""Центр"", ""Center"")]
				|        public void Center(int p1 = 0, int p2 = 0)
				|        {
				|            Base_obj.Center(p1, p2);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Заполнить") Тогда
				Стр = Стр +
				"        [ContextMethod(""Заполнить"", ""Fill"")]
				|        public void Fill(int p1 = 0, int p2 = 0)
				|        {
				|            Base_obj.Fill(p1, p2);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "НаПереднийПлан") и (КлассАнгл = "Window") Тогда
				Стр = Стр +
				"        [ContextMethod(""НаПереднийПлан"", ""BringSubviewToFront"")]
				|        public void BringSubviewToFront(IValue p1)
				|        {
				|            Base_obj.M_Window.Subviews[0].BringSubviewToFront(((dynamic)p1).Base_obj.M_View);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "НаПереднийПлан") Тогда
				Стр = Стр +
				"        [ContextMethod(""НаПереднийПлан"", ""BringSubviewToFront"")]
				|        public void BringSubviewToFront(IValue p1)
				|        {
				|            Base_obj.BringSubviewToFront(((dynamic)p1).Base_obj);
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Запрос") и (КлассАнгл = "MessageBox") Тогда
				Стр = Стр +
				"        [ContextMethod(""Запрос"", ""Query"")]
				|        public int Query()
				|        {
				|            // Уберем на время появления модального окна события мыши.
				|            dynamic actRootMouseEvent = Application.RootMouseEvent;
				|            Application.RootMouseEvent = null;
				|
				|            int maxLengthButtons = 0;
				|            NStack.ustring[] _buttons = new NStack.ustring[Buttons.Count()];
				|            for (int i = 0; i < Buttons.Count(); i++)
				|            {
				|                string str = Buttons.Get(i).AsString();
				|                maxLengthButtons = maxLengthButtons + str.Length + 8;
				|                _buttons[i] = str;
				|            }
				|
				|            TfTextFormatter TfTextFormatter1 = new TfTextFormatter();
				|            TfTextFormatter1.Text = Message;
				|            TfSize TfSize1 = TfTextFormatter1.GetAutoSize();
				|            int _height = TfSize1.Height + 1;
				|            int _widthMessage = TfSize1.Width;
				|            int _widthTitle = Title.Length + 5;
				|            int _widthTerminal = Application.Driver.Cols - 8;
				|            int _width = Math.Max(_widthMessage, Math.Max(_widthTitle, maxLengthButtons));
				|            if (_width >= _widthTerminal)
				|            {
				|                _width = _widthTerminal;
				|            }
				|            int _heightTerminal = Application.Driver.Rows - 8;
				|            if (_height >= _heightTerminal)
				|            {
				|                _height = _heightTerminal;
				|            }
				|            if (!AutoSize)
				|            {
				|                _width = Width;
				|                _height = Height;
				|            }
				|
				|            Clicked = Terminal.Gui.MessageBox.Query(_width, _height, Title, Message, DefaultButtonIndex, _buttons);
				|            Application.MainLoop.RemoveTimeout(token);
				|            // Вернем события мыши.
				|            Application.RootMouseEvent = actRootMouseEvent;
				|            return Clicked;
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Начать") и (КлассАнгл = "Timer") Тогда
				Стр = Стр +
				"        [ContextMethod(""Начать"", ""Start"")]
				|        public void Start()
				|        {
				|            stop = false;
				|            token = Application.MainLoop.AddTimeout(TimeSpan.FromMilliseconds(Interval), (m) =>
				|            {
				|                if (Tick != null)
				|                {
				|                    TfEventArgs TfEventArgs1 = new TfEventArgs();
				|                    TfEventArgs1.sender = this;
				|                    TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Tick);
				|                    OneScriptTerminalGui.Event = TfEventArgs1;
				|                    OneScriptTerminalGui.ExecuteEvent(Tick);
				|                }
				|                if (stop)
				|                {
				|                    return false;
				|                }
				|                return true;
				|            });
				|        }
				|
				|";
			ИначеЕсли (МетодРус = "Остановить") и (КлассАнгл = "Timer") Тогда
				Стр = Стр +
				"        [ContextMethod(""Остановить"", ""Stop"")]
				|        public void Stop()
				|        {
				|            stop = true;
				|            // token для остановки не срабатывает, поэтому добавлено поле stop.
				|            Application.MainLoop.RemoveTimeout(token);
				|        }
				|
				|";


				
				
				
				
				
				
				
			ИначеЕсли (МетодРус = "йййййййй") и (КлассАнгл = "йййййййй") Тогда
				Стр = Стр +
				"        [ContextMethod(""Закрыть"", ""Close"")]


				|
				|";
				
				
			
			
			
			Иначе
				Стр = Стр +
				"        [ContextMethod(""" + МетодРус + """, """ + МетодАнгл + """)]
				|        public ййййй " + МетодАнгл + "()
				|        {
				|            return Base_obj." + МетодАнгл + "();
				|        }
				|
				|";
			КонецЕсли;
		КонецЦикла;
	Иначе
		Стр = "" + Символы.ПС;
	КонецЕсли;
	
	Возврат Стр;
КонецФункции//Методы

Функция Подвал()
	Стр = 
	"    }
	|}";
	Возврат Стр;
КонецФункции

Процедура ВыгрузкаTUI()
	Таймер = ТекущаяУниверсальнаяДатаВМиллисекундах();
	
	УдалитьФайлы(КаталогВыгрузки, "*.cs");
	
	// Создадим файлы cs которые не нуждаются в сборке и неизменны. Остальные будем собирать анализируя справку.
	СоздатьФайлТФ("OneScriptTerminalGui");
	СоздатьФайлТФ("Action");
	СоздатьФайлТФ("Responder");
	
	// Соберем и запишем файлы перечислений.
	ЗаписатьПеречисления();

	//===== Обработаем классы ==========================================================================================================================
	ВыбранныеФайлы = ОтобратьФайлы("Класс");
	Для А = 0 По ВыбранныеФайлы.ВГраница() Цикл
		// Пропустим неизменные классы.
		Пропустим = Ложь;
		Для А2 = 0 По СписокНеизменныхКлассов.Количество() - 1 Цикл
			Если ВыбранныеФайлы[А] = КаталогСправки + "\OSTGui." + СписокНеизменныхКлассов.Получить(А2).Значение + ".html" Тогда
				Пропустим = Истина;
				// Сообщить("Пропустим = Истина " + СписокНеизменныхКлассов.Получить(А2).Значение);
			КонецЕсли;
		КонецЦикла;
		
		Если Пропустим Тогда
			Продолжить;
		КонецЕсли;
		
		ТекстДок = Новый ТекстовыйДокумент;
		ТекстДок.Прочитать(ВыбранныеФайлы[А]);
		Стр = ТекстДок.ПолучитьТекст();
		
		// Сообщить("" + Стр);
		// Сообщить("=====================================================================================================");
		
		СтрЗаголовка = СтрНайтиМежду(Стр, "<H1 class=dtH1", "/H1>", , )[0];
		М01 = СтрНайтиМежду(СтрЗаголовка, "(", ")", , );
		Стр33 = СтрЗаголовка;
		Стр33 = СтрЗаменить(Стр33, "&nbsp;", " ");
		Стр33 = СтрЗаменить(Стр33, ">", "");
		М08 = РазобратьСтроку(Стр33, " ");
		ИмяФайлаВыгрузки = КаталогВыгрузки + "\" + М01[0] + ".cs";
		КлассАнгл = М01[0];
		КлассРус = М08[0];
		
		// Сообщить("ИмяФайлаВыгрузки = " + ИмяФайлаВыгрузки);
		// Сообщить("КлассАнгл = " + КлассАнгл);
		// Сообщить("КлассРус = " + КлассРус);
		
		// определим имя файлов событий, свойств, методов.
		ФайлСобытий = КаталогСправки + "\OSTGui." + КлассАнгл + "Events.html";
		ФайлСвойств = КаталогСправки + "\OSTGui." + КлассАнгл + "Properties.html";
		ФайлМетодов = КаталогСправки + "\OSTGui." + КлассАнгл + "Methods.html";
		СтрДирективы = Директивы(КлассАнгл);
		СтрШапка = Шапка(КлассАнгл, КлассРус);
		СтрРазделОбъявленияПеременных = РазделОбъявленияПеременных(КлассАнгл, КлассРус);
		СтрКонструктор = Конструктор(КлассАнгл, КлассРус);
		СтрСвойства = Свойства(ФайлСвойств, КлассАнгл, КлассРус);
		СтрСобытия = События(ФайлСобытий, КлассАнгл, КлассРус);
		СтрМетоды = Методы(ФайлМетодов, КлассАнгл);
		СтрПодвал = Подвал();
		
		// СортироватьСтрРазделОбъявленияПеременных();
		СтрВыгрузки = "";
		СтрВыгрузки = СтрВыгрузки + СтрДирективы + Символы.ПС;
		СтрВыгрузки = СтрВыгрузки + КлассВторогоУровня(КлассАнгл);
		СтрВыгрузки = СтрВыгрузки + СтрШапка + Символы.ПС;
		СтрВыгрузки = СтрВыгрузки + СтрРазделОбъявленияПеременных + Символы.ПС;
		СтрВыгрузки = СтрВыгрузки + СтрКонструктор + Символы.ПС;
		СтрВыгрузки = СтрВыгрузки + СтрСвойства;
		СтрВыгрузки = СтрВыгрузки + СтрСобытия;
		СтрВыгрузки = СтрВыгрузки + СтрМетоды;
		СтрВыгрузки = СтрВыгрузки + СтрПодвал + Символы.ПС;
		
		ЗаписьТекста = Новый ЗаписьТекста();
		ЗаписьТекста.Открыть(ИмяФайлаВыгрузки,,,);
		ЗаписьТекста.Записать(СтрВыгрузки);
		ЗаписьТекста.Закрыть();

		
	КонецЦикла;
	//===== Закончили с классами ==========================================================================================================================
	// ЗавершитьРаботу(0);
	
	Сообщить("Выполнено за: " + ((ТекущаяУниверсальнаяДатаВМиллисекундах()-Таймер)/1000)/60 + " мин." + " " + ТекущаяДата());
КонецПроцедуры//ВыгрузкаTUI

Процедура ЗаписатьПеречисления()
	ВыбранныеФайлы = ОтобратьФайлы("Перечисление");
	Для А = 0 По ВыбранныеФайлы.ВГраница() Цикл
		Если ВыбранныеФайлы[А] = КаталогСправки + "\йййййййййййййй.ColorEnumeration.html" Тогда
			Продолжить;
		КонецЕсли;
		
		ТекстДок = Новый ТекстовыйДокумент;
		ТекстДок.Прочитать(ВыбранныеФайлы[А]);
		Стр = ТекстДок.ПолучитьТекст();
		
		// Сообщить("" + Стр);
		// Сообщить("=====================================================================================================");
		
		СтрЗаголовка= СтрНайтиМежду(Стр, "<H1 class=dtH1", "/H1>", , )[0];
		М01 = СтрНайтиМежду(СтрЗаголовка, "(", ")", , );
		СтрЗаголовка = СтрЗаменить(СтрЗаголовка, "&nbsp;", " ");
		Стр33 = СтрНайтиМежду(СтрЗаголовка, ">", " Перечисление<", , )[0];
		Стр33 = СтрЗаменить(Стр33, "&nbsp;", " ");
		Стр33 = СтрЗаменить(Стр33, ">", "");
		М08 = РазобратьСтроку(Стр33, " ");
		ИмяФайлаВыгрузки = КаталогВыгрузки + "\" + М01[0] + ".cs";
		КлассАнгл = М01[0];
		КлассРус = М08[0];
		// Сообщить("====" + КлассРус);
		// Сообщить("====" + КлассАнгл);
		// Сообщить("=====================================================================================================");
		
		//находим текст таблицы
		СтрТаблица = СтрНайтиМежду(Стр, "<TBODY>", "</TABLE>", Ложь, );
		СтрТаблицыПеречисления = СтрНайтиМежду(СтрТаблица[1], "<TR vAlign=top>", "</TR>", Ложь, );
		СтрРазделОбъявленияПеременныхДляПеречисления = "";
		СтрСвойстваДляПеречисления = "";
		
		СтрРазделОбъявленияПеременныхДляПеречисления = 
        "
		|        private List<IValue> _list;
		|
		|        public int Count()
		|        {
		|            return _list.Count;
		|        }
		|
		|        public CollectionEnumerator GetManagedIterator()
		|        {
		|            return new CollectionEnumerator(this);
		|        }
		|
		|        IEnumerator IEnumerable.GetEnumerator()
		|        {
		|            return ((IEnumerable<IValue>)_list).GetEnumerator();
		|        }
		|
		|        IEnumerator<IValue> IEnumerable<IValue>.GetEnumerator()
		|        {
		|            foreach (var item in _list)
		|            {
		|                yield return (item as IValue);
		|            }
		|        }
		|";
		
		СтрКонструктораДляПеречисления = 
		"        public Tf" + КлассАнгл + "()
		|        {
		|            _list = new List<IValue>();";
		
		СтрМетодаВСтрокуДляПеречисленияНачало = "
		|        [ContextMethod(""ВСтроку"", ""ВСтроку"")]
		|        public string ToStringRu(decimal p1)
		|        {
		|            string str = p1.ToString();
		|            switch (p1)
		|            {";
		
		СтрМетодаВСтрокуДляПеречисленияКонец = 
		"            }
		|            return str;
		|        }";
		
		СтрМетодаToStringДляПеречисленияНачало = "
		|        [ContextMethod(""ToString"", ""ToString"")]
		|        public string ToStringEn(decimal p1)
		|        {
		|            string str = p1.ToString();
		|            switch (p1)
		|            {";
		
		СтрМетодаToStringДляПеречисленияКонец = 
		"            }
		|            return str;
		|        }";
		
		Для А02 = 1 По СтрТаблицыПеречисления.ВГраница() Цикл
			М12 = СтрНайтиМежду(СтрТаблицыПеречисления[А02], "<TD>", "</TD>", , );
			М14 = СтрНайтиМежду(М12[0], "<B>", "</B>", , );
			М13 = РазобратьСтроку(СтрЗаменить(М14[0], "&nbsp;", " "), " ");
			ИмяПеречАнгл = М01[0];
			ИмяПеречРус = М08[0];
			ИмяЧленаАнгл = М13[1];
			// Сообщить("==" + ИмяЧленаАнгл);
			// если здесь ошибка, тогда возможно есть лишний пробел в одном из значений перечисления
			ИмяЧленаАнгл = СтрНайтиМежду(ИмяЧленаАнгл, "(", ")", , )[0];
			ИмяЧленаРус = М13[0];
			ОписаниеЧлена = М12[1];
			Пока СтрЧислоВхождений(ОписаниеЧлена, Символы.ПС) > 0 Цикл
				ОписаниеЧлена = СтрЗаменить(ОписаниеЧлена, Символы.ПС, " ");
			КонецЦикла;
			Пока СтрЧислоВхождений(ОписаниеЧлена, Символы.Таб) > 0 Цикл
				ОписаниеЧлена = СтрЗаменить(ОписаниеЧлена, Символы.Таб, " ");
			КонецЦикла;
			Пока СтрЧислоВхождений(ОписаниеЧлена, "  ") > 0 Цикл
				ОписаниеЧлена = СтрЗаменить(ОписаниеЧлена, "  ", " ");
			КонецЦикла;
			ЗначениеЧлена = М12[2];
			// Сообщить("--------------");
			// Сообщить("ИмяПеречРус = " + ИмяПеречРус);
			// Сообщить("ИмяПеречАнгл = " + ИмяПеречАнгл);
			// Сообщить("ИмяЧленаРус = " + ИмяЧленаРус);
			// Сообщить("ИмяЧленаАнгл = " + ИмяЧленаАнгл);
			// Сообщить("ОписаниеЧлена = " + ОписаниеЧлена);
			// Сообщить("ЗначениеЧлена = " + ЗначениеЧлена);
			
			СтрСвойстваДляПеречисления = СтрСвойстваДляПеречисления + Символы.ПС + 
			"        [ContextProperty(""" + ИмяЧленаРус + """, """ + ИмяЧленаАнгл + """)]
			|        public int " + ИмяЧленаАнгл + "
			|        {
			|            get { return " + ЗначениеЧлена + "; }
			|        }" + ?(А02 = СтрТаблицыПеречисления.ВГраница(), "", Символы.ПС);
			
			СтрКонструктораДляПеречисления = СтрКонструктораДляПеречисления + "
			|            _list.Add(ValueFactory.Create(" + ИмяЧленаАнгл + "));";
			
			СтрМетодаВСтрокуДляПеречисленияНачало = СтрМетодаВСтрокуДляПеречисленияНачало + "
			|                case " + ЗначениеЧлена + ":
			|                    str = """ + ИмяЧленаРус + """;
			|                    break;";
			
			СтрМетодаToStringДляПеречисленияНачало = СтрМетодаToStringДляПеречисленияНачало + "
			|                case " + ЗначениеЧлена + ":
			|                    str = """ + ИмяЧленаАнгл + """;
			|                    break;";
			
		КонецЦикла;
		
		СтрКонструктораДляПеречисления = СтрКонструктораДляПеречисления + "
		|        }";
		
		СтрВыгрузкиПеречисленийШапка = Директивы(КлассАнгл);
		СтрВыгрузкиПеречислений = СтрВыгрузкиПеречисленийШапка + 
		"
		|namespace ostgui
		|{
		|    [ContextClass(""Тф" + КлассРус + """, ""Tf" + КлассАнгл + """)]
		|    public class Tf" + КлассАнгл + " : AutoContext<Tf" + КлассАнгл + ">, ICollectionContext, IEnumerable<IValue>
		|    {";
		СтрВыгрузкиПеречислений = СтрВыгрузкиПеречислений + СтрРазделОбъявленияПеременныхДляПеречисления + Символы.ПС;
		СтрВыгрузкиПеречислений = СтрВыгрузкиПеречислений + СтрКонструктораДляПеречисления + Символы.ПС;
		СтрВыгрузкиПеречислений = СтрВыгрузкиПеречислений + СтрСвойстваДляПеречисления + Символы.ПС;
		СтрВыгрузкиПеречислений = СтрВыгрузкиПеречислений + СтрМетодаВСтрокуДляПеречисленияНачало + Символы.ПС;
		СтрВыгрузкиПеречислений = СтрВыгрузкиПеречислений + СтрМетодаВСтрокуДляПеречисленияКонец + Символы.ПС;
		СтрВыгрузкиПеречислений = СтрВыгрузкиПеречислений + СтрМетодаToStringДляПеречисленияНачало + Символы.ПС;
		СтрВыгрузкиПеречислений = СтрВыгрузкиПеречислений + СтрМетодаToStringДляПеречисленияКонец + Символы.ПС;
		СтрВыгрузкиПеречислений = СтрВыгрузкиПеречислений + 
		"    }" + Символы.ПС + 
		"}";
		
		ПодстрокаПоиска = "public int SpecialMask";
		ПодстрокаЗамены = "public decimal SpecialMask";
		СтрВыгрузкиПеречислений = СтрЗаменить(СтрВыгрузкиПеречислений, ПодстрокаПоиска, ПодстрокаЗамены);
		
		ПодстрокаПоиска = "public int AltMask";
		ПодстрокаЗамены = "public decimal AltMask";
		СтрВыгрузкиПеречислений = СтрЗаменить(СтрВыгрузкиПеречислений, ПодстрокаПоиска, ПодстрокаЗамены);
		
		ТекстДокПеречислений = Новый ТекстовыйДокумент;
		ТекстДокПеречислений.УстановитьТекст(СтрВыгрузкиПеречислений);
		ТекстДокПеречислений.Записать(ИмяФайлаВыгрузки);
	КонецЦикла;
КонецПроцедуры//ЗаписатьПеречисления

Функция КлассВторогоУровня(ИмяФайлаТФ)
	Стр = "";
	Если Ложь Тогда
	// ИначеЕсли ИмяФайлаТФ = "" Тогда
		// Стр = Стр + 
		// "namespace ostgui
		// |{
		

		// |";
		
		
		
		
	ИначеЕсли ИмяФайлаТФ = "Timer" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|";
	ИначеЕсли ИмяФайлаТФ = "MessageBox" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|";
	ИначеЕсли ИмяФайлаТФ = "Dim" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class Dim
		|    {
		|        public TfDim dll_obj;
		|        public Terminal.Gui.Dim M_Dim;
		|
		|        public Dim()
		|        {
		|            M_Dim = new Terminal.Gui.Dim();
		|            OneScriptTerminalGui.AddToHashtable(M_Dim, this);
		|        }
		|
		|        public Dim(Terminal.Gui.Dim p1)
		|        {
		|            M_Dim = p1;
		|            OneScriptTerminalGui.AddToHashtable(M_Dim, this);
		|        }
		|
		|        public Dim Percent(float p1, bool p2 = false)
		|        {
		|            return new Dim(Terminal.Gui.Dim.Percent(p1, p2));
		|        }
		|
		|        public Dim Height(View p1)
		|        {
		|            return new Dim(Terminal.Gui.Dim.Height(p1.M_View));
		|        }
		|
		|        public Dim Fill(int p1 = 0)
		|        {
		|            return new Dim(Terminal.Gui.Dim.Fill(p1));
		|        }
		|
		|        public Dim Sized(int p1)
		|        {
		|            return new Dim(Terminal.Gui.Dim.Sized(p1));
		|        }
		|
		|        public Dim Width(View p1)
		|        {
		|            return new Dim(Terminal.Gui.Dim.Width(p1.M_View));
		|        }
		|
		|        public Dim Summation(Dim p1, Dim p2)
		|        {
		|            return new Dim(p1.M_Dim + p2.M_Dim);
		|        }
		|
		|        public Dim Subtract(Dim p1, Dim p2)
		|        {
		|            return new Dim(p1.M_Dim - p2.M_Dim);
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "Colors" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class Colors
		|    {
		|        public TfColors dll_obj;
		|        public Terminal.Gui.ColorScheme M_Colors;
		|
		|        public Colors()
		|        {
		|            M_Colors = new Terminal.Gui.ColorScheme();
		|        }
		|
		|        public ColorScheme TopLevel
		|        {
		|            get { return new ColorScheme(Terminal.Gui.Colors.TopLevel); }
		|            set { M_Colors = value.M_ColorScheme; }
		|        }
		|
		|        public ColorScheme Dialog
		|        {
		|            get { return new ColorScheme(Terminal.Gui.Colors.Dialog); }
		|            set { M_Colors = value.M_ColorScheme; }
		|        }
		|
		|        public ColorScheme Menu
		|        {
		|            get { return new ColorScheme(Terminal.Gui.Colors.Menu); }
		|            set { M_Colors = value.M_ColorScheme; }
		|        }
		|
		|        public ColorScheme Base
		|        {
		|            get { return new ColorScheme(Terminal.Gui.Colors.Base); }
		|            set { M_Colors = value.M_ColorScheme; }
		|        }
		|
		|        public ColorScheme Error
		|        {
		|            get { return new ColorScheme(Terminal.Gui.Colors.Error); }
		|            set { M_Colors = value.M_ColorScheme; }
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_Colors.ToString();
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "MenuBarItemChildren" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|";
	ИначеЕсли ИмяФайлаТФ = "MenuItem" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class MenuItem
		|    {
		|        public TfMenuItem dll_obj;
		|        public Terminal.Gui.MenuItem m_MenuItem;
		|        public System.Action Clicked;
		|
		|        public MenuItem()
		|        {
		|            Clicked = delegate ()
		|            {
		|                if (dll_obj.Clicked != null)
		|                {
		|                    TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                    TfEventArgs1.sender = dll_obj;
		|                    TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.Clicked);
		|                    OneScriptTerminalGui.Event = TfEventArgs1;
		|                    OneScriptTerminalGui.ExecuteEvent(dll_obj.Clicked);
		|                }
		|            };
		|
		|            M_MenuItem = new Terminal.Gui.MenuItem();
		|            M_MenuItem.Action = Clicked;
		|            OneScriptTerminalGui.AddToHashtable(M_MenuItem, this);
		|        }
		|
		|        public Terminal.Gui.MenuItem M_MenuItem
		|        {
		|            get { return m_MenuItem; }
		|            set { m_MenuItem = value; }
		|        }
		|
		|        public object Data
		|        {
		|            get { return M_MenuItem.Data; }
		|            set { M_MenuItem.Data = value; }
		|        }
		|
		|        public string Title
		|        {
		|            get { return M_MenuItem.Title.ToString(); }
		|            set { M_MenuItem.Title = value; }
		|        }
		|
		|        public string HotKey
		|        {
		|            get { return M_MenuItem.HotKey.ToString(); }
		|            set { M_MenuItem.HotKey = value.ToCharArray()[0]; }
		|        }
		|
		|        public string Help
		|        {
		|            get { return M_MenuItem.Help.ToString(); }
		|            set { M_MenuItem.Help = value; }
		|        }
		|
		|        public bool Checked
		|        {
		|            get { return M_MenuItem.Checked; }
		|            set { M_MenuItem.Checked = value; }
		|        }
		|
		|        public int Shortcut
		|        {
		|            get { return (int)M_MenuItem.Shortcut; }
		|            set { M_MenuItem.Shortcut = (Terminal.Gui.Key)value; }
		|        }
		|
		|        public IValue Parent
		|        {
		|            get { return OneScriptTerminalGui.RevertEqualsObj(M_MenuItem.Parent).dll_obj; }
		|        }
		|
		|        public string ShortcutTag
		|        {
		|            get { return M_MenuItem.ShortcutTag.ToString(); }
		|        }
		|
		|        public int CheckType
		|        {
		|            get { return (int)M_MenuItem.CheckType; }
		|            set { M_MenuItem.CheckType = (Terminal.Gui.MenuItemCheckStyle)value; }
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_MenuItem.ToString();
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "StatusBarItems" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|";
	ИначеЕсли ИмяФайлаТФ = "StatusItem" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class StatusItem
		|    {
		|        public TfStatusItem dll_obj;
		|        public Terminal.Gui.StatusItem M_StatusItem;
		|        Action Clicked;
		|
		|        public StatusItem(Terminal.Gui.Key p1, string p2)
		|        {
		|            Clicked = delegate ()
		|            {
		|                if (dll_obj.Clicked != null)
		|                {
		|                    TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                    TfEventArgs1.sender = dll_obj;
		|                    TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.Clicked);
		|                    OneScriptTerminalGui.Event = TfEventArgs1;
		|                    OneScriptTerminalGui.ExecuteEvent(dll_obj.Clicked);
		|                }
		|            };
		|
		|            M_StatusItem = new Terminal.Gui.StatusItem(p1, p2, Clicked);
		|            OneScriptTerminalGui.AddToHashtable(M_StatusItem, this);
		|        }
		|
		|        public string HotTextSpecifier
		|        {
		|            get { return M_StatusItem.HotTextSpecifier.ToString(); }
		|            set { M_StatusItem.HotTextSpecifier = value.ToCharArray()[0]; }
		|        }
		|
		|        public string Title
		|        {
		|            get { return M_StatusItem.Title.ToString(); }
		|            set { M_StatusItem.Title = value; }
		|        }
		|
		|        public object Data
		|        {
		|            get { return M_StatusItem.Data; }
		|            set { M_StatusItem.Data = value; }
		|        }
		|
		|        public int Shortcut
		|        {
		|            get { return (int)M_StatusItem.Shortcut; }
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_StatusItem.ToString();
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "Pos" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class Pos
		|    {
		|        public TfPos dll_obj;
		|        public Terminal.Gui.Pos M_Pos;
		|
		|        public Pos()
		|        {
		|            M_Pos = new Terminal.Gui.Pos();
		|            OneScriptTerminalGui.AddToHashtable(M_Pos, this);
		|        }
		|
		|        public Pos(Terminal.Gui.Pos p1)
		|        {
		|            M_Pos = p1;
		|            OneScriptTerminalGui.AddToHashtable(M_Pos, this);
		|        }
		|
		|        public Pos At(int p1)
		|        {
		|            return new Pos(Terminal.Gui.Pos.At(p1));
		|        }
		|
		|        public Pos Top(ostgui.View p1)
		|        {
		|            return new Pos(Terminal.Gui.Pos.Top(p1.M_View));
		|        }
		|
		|        public Pos Y(ostgui.View p1)
		|        {
		|            return new Pos(Terminal.Gui.Pos.Y(p1.M_View));
		|        }
		|
		|        public Pos X(ostgui.View p1)
		|        {
		|            return new Pos(Terminal.Gui.Pos.X(p1.M_View));
		|        }
		|
		|        public Pos Left(ostgui.View p1)
		|        {
		|            return new Pos(Terminal.Gui.Pos.Left(p1.M_View));
		|        }
		|
		|        public Pos Bottom(ostgui.View p1)
		|        {
		|            return new Pos(Terminal.Gui.Pos.Bottom(p1.M_View));
		|        }
		|
		|        public Pos Right(ostgui.View p1)
		|        {
		|            return new Pos(Terminal.Gui.Pos.Right(p1.M_View));
		|        }
		|
		|        public Pos Percent(float p1)
		|        {
		|            return new Pos(Terminal.Gui.Pos.Percent(p1));
		|        }
		|
		|        public Pos Center()
		|        {
		|            return new Pos(Terminal.Gui.Pos.Center());
		|        }
		|
		|        public Pos AnchorEnd(int p1 = 0)
		|        {
		|            return new Pos(Terminal.Gui.Pos.AnchorEnd(p1));
		|        }
		|
		|        public Pos Summation(Pos p1, Pos p2)
		|        {
		|            return new Pos(p1.M_Pos + p2.M_Pos);
		|        }
		|
		|        public Pos Subtract(Pos p1, Pos p2)
		|        {
		|            return new Pos(p1.M_Pos - p2.M_Pos);
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "MenuBarItem" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class MenuBarItem : ostgui.MenuItem
		|    {
		|        public new TfMenuBarItem dll_obj;
		|        public Terminal.Gui.MenuBarItem M_MenuBarItem;
		|
		|        public MenuBarItem()
		|        {
		|            Clicked = delegate ()
		|            {
		|                if (dll_obj.Clicked != null)
		|                {
		|                    TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                    TfEventArgs1.sender = dll_obj;
		|                    TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.Clicked);
		|                    OneScriptTerminalGui.Event = TfEventArgs1;
		|                    OneScriptTerminalGui.ExecuteEvent(dll_obj.Clicked);
		|                }
		|            };
		|
		|            M_MenuBarItem = new Terminal.Gui.MenuBarItem();
		|            base.M_MenuItem = M_MenuBarItem;
		|            M_MenuBarItem.Action = Clicked;
		|            OneScriptTerminalGui.AddToHashtable(M_MenuBarItem, this);
		|        }
		|
		|        public Terminal.Gui.MenuItem[] Children
		|        {
		|            get { return M_MenuBarItem.Children; }
		|            set { M_MenuBarItem.Children = value; }
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_MenuBarItem.ToString();
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "MenusCollection" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{";
	ИначеЕсли ИмяФайлаТФ = "Thickness" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class Thickness
		|    {
		|        public TfThickness dll_obj;
		|        public Terminal.Gui.Thickness M_Thickness;
		|
		|        public Thickness(int p1)
		|        {
		|            M_Thickness = new Terminal.Gui.Thickness(p1);
		|        }
		|
		|        public Thickness(int left, int top, int right, int bottom)
		|        {
		|            M_Thickness = new Terminal.Gui.Thickness(left, top, right, bottom);
		|        }
		|
		|        public int Top
		|        {
		|            get { return M_Thickness.Top; }
		|            set { M_Thickness.Top = value; }
		|        }
		|
		|        public int Left
		|        {
		|            get { return M_Thickness.Left; }
		|            set { M_Thickness.Left = value; }
		|        }
		|
		|        public int Bottom
		|        {
		|            get { return M_Thickness.Bottom; }
		|            set { M_Thickness.Bottom = value; }
		|        }
		|
		|        public int Right
		|        {
		|            get { return M_Thickness.Right; }
		|            set { M_Thickness.Right = value; }
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_Thickness.ToString();
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "StatusBar" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class StatusBar : View
		|    {
		|        public new TfStatusBar dll_obj;
		|        public Terminal.Gui.StatusBar M_StatusBar;
		|
		|        public StatusBar()
		|        {
		|            M_StatusBar = new Terminal.Gui.StatusBar();
		|            base.M_View = M_StatusBar;
		|            OneScriptTerminalGui.AddToHashtable(M_StatusBar, this);
		|        }
		|
		|        public string ShortcutDelimiter
		|        {
		|            get { return Terminal.Gui.StatusBar.ShortcutDelimiter.ToString(); }
		|            set { Terminal.Gui.StatusBar.ShortcutDelimiter = value; }
		|        }
		|
		|        public void AddItemAt(int p1, StatusItem p2)
		|        {
		|            M_StatusBar.AddItemAt(p1, p2.M_StatusItem);
		|        }
		|
		|        public Terminal.Gui.StatusItem[] Items
		|        {
		|            get { return M_StatusBar.Items; }
		|            set { M_StatusBar.Items = value; }
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_StatusBar.ToString();
		|        }
		|
		|        public new Toplevel GetTopSuperView()
		|        {
		|            return OneScriptTerminalGui.RevertEqualsObj(M_StatusBar.GetTopSuperView());
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "MenuBar" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class MenuBar : View
		|    {
		|        public new TfMenuBar dll_obj;
		|        public Terminal.Gui.MenuBar M_MenuBar;
		|
		|        public MenuBar()
		|        {
		|            M_MenuBar = new Terminal.Gui.MenuBar();
		|            base.M_View = M_MenuBar;
		|            OneScriptTerminalGui.AddToHashtable(M_MenuBar, this);
		|
		|            M_MenuBar.MenuAllClosed += M_MenuBar_MenuAllClosed;
		|            M_MenuBar.MenuOpened += M_MenuBar_MenuOpened;
		|            M_MenuBar.MenuOpening += M_MenuBar_MenuOpening;
		|            M_MenuBar.MouseLeave += M_MenuBar_MouseLeave;
		|        }
		|
		|        private void M_MenuBar_MouseLeave(Terminal.Gui.View.MouseEventArgs obj)
		|        {
		|            if (dll_obj.MouseLeave != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.MouseLeave);
		|                TfEventArgs1.flags = ValueFactory.Create((int)obj.MouseEvent.Flags);
		|                TfEventArgs1.view = OneScriptTerminalGui.RevertEqualsObj(M_MenuBar).dll_obj;
		|                TfEventArgs1.x = ValueFactory.Create(obj.MouseEvent.X);
		|                TfEventArgs1.y = ValueFactory.Create(obj.MouseEvent.Y);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.MouseLeave);
		|            }
		|        }
		|
		|        private void M_MenuBar_MenuOpening(Terminal.Gui.MenuOpeningEventArgs obj)
		|        {
		|            if (dll_obj.MenuOpening != null)
		|            {
		|                obj.NewMenuBarItem = obj.CurrentMenu;
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.MenuOpening);
		|                TfEventArgs1.cancel = ValueFactory.Create(false);
		|                TfEventArgs1.cancel = ValueFactory.Create(obj.Cancel);
		|                TfEventArgs1.currentMenu = OneScriptTerminalGui.RevertEqualsObj(obj.CurrentMenu).dll_obj;
		|                TfEventArgs1.newMenuBarItem = OneScriptTerminalGui.RevertEqualsObj(obj.NewMenuBarItem).dll_obj;
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.MenuOpening);
		|
		|                if (TfEventArgs1.Cancel)
		|                {
		|                    M_MenuBar.MenuOpening -= M_MenuBar_MenuOpening;
		|                    obj.NewMenuBarItem = ((TfMenuBarItem)TfEventArgs1.NewMenuBarItem).Base_obj.M_MenuBarItem;
		|                    M_MenuBar.MenuOpening += M_MenuBar_MenuOpening;
		|                }
		|            }
		|        }
		|
		|        private void M_MenuBar_MenuOpened(Terminal.Gui.MenuItem obj)
		|        {
		|            if (dll_obj.MenuOpened != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.MenuOpened);
		|                TfEventArgs1.menuItem = OneScriptTerminalGui.RevertEqualsObj(obj).dll_obj;
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.MenuOpened);
		|            }
		|        }
		|
		|        private void M_MenuBar_MenuAllClosed()
		|        {
		|            if (dll_obj.MenuAllClosed != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.MenuAllClosed);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.MenuAllClosed);
		|            }
		|        }
		|
		|        public bool UseSubMenusSingleFrame
		|        {
		|            get { return M_MenuBar.UseSubMenusSingleFrame; }
		|            set { M_MenuBar.UseSubMenusSingleFrame = value; }
		|        }
		|
		|        public bool UseKeysUpDownAsKeysLeftRight
		|        {
		|            get { return M_MenuBar.UseKeysUpDownAsKeysLeftRight; }
		|            set { M_MenuBar.UseKeysUpDownAsKeysLeftRight = value; }
		|        }
		|
		|        public bool IsMenuOpen
		|        {
		|            get { return M_MenuBar.IsMenuOpen; }
		|        }
		|
		|        public new bool Visible
		|        {
		|            get { return M_MenuBar.Visible; }
		|            set { M_MenuBar.Visible = value; }
		|        }
		|
		|        public bool CloseMenu()
		|        {
		|            M_MenuBar.OnMenuAllClosed();
		|            return M_MenuBar.CloseMenu(true);
		|        }
		|
		|        public void OpenMenu()
		|        {
		|            M_MenuBar.OpenMenu();
		|        }
		|
		|        public int Key
		|        {
		|            get { return (int)M_MenuBar.Key; }
		|            set { M_MenuBar.Key = (Terminal.Gui.Key)value; }
		|        }
		|
		|        public Terminal.Gui.MenuBarItem[] Menus
		|        {
		|            get { return M_MenuBar.Menus; }
		|            set { M_MenuBar.Menus = value; }
		|        }
		|
		|        public ostgui.View LastFocused
		|        {
		|            get { return OneScriptTerminalGui.RevertEqualsObj(M_MenuBar.LastFocused); }
		|        }
		|
		|        public string ShortcutDelimiter
		|        {
		|            get { return Terminal.Gui.MenuBar.ShortcutDelimiter.ToString(); }
		|            set { Terminal.Gui.MenuBar.ShortcutDelimiter = value; }
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_MenuBar.ToString();
		|        }
		|
		|        public new Toplevel GetTopSuperView()
		|        {
		|            return OneScriptTerminalGui.RevertEqualsObj(M_MenuBar.GetTopSuperView());
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "EventArgs" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|";
	ИначеЕсли ИмяФайлаТФ = "Attribute" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class Attribute
		|    {
		|        public TfAttribute dll_obj;
		|        public Terminal.Gui.Attribute M_Attribute;
		|
		|        public Attribute()
		|        {
		|            M_Attribute = new Terminal.Gui.Attribute();
		|            OneScriptTerminalGui.AddToHashtable(M_Attribute, this);
		|        }
		|
		|        public Attribute(int p1)
		|        {
		|            M_Attribute = new Terminal.Gui.Attribute((Terminal.Gui.Color)p1);
		|            OneScriptTerminalGui.AddToHashtable(M_Attribute, this);
		|        }
		|
		|        public Attribute(int p1, int p2)
		|        {
		|            M_Attribute = new Terminal.Gui.Attribute((Terminal.Gui.Color)p1, (Terminal.Gui.Color)p2);
		|            OneScriptTerminalGui.AddToHashtable(M_Attribute, this);
		|        }
		|
		|        public Attribute(int p1, int p2, int p3)
		|        {
		|            M_Attribute = new Terminal.Gui.Attribute(p1, (Terminal.Gui.Color)p2, (Terminal.Gui.Color)p3);
		|            OneScriptTerminalGui.AddToHashtable(M_Attribute, this);
		|        }
		|
		|        public Attribute(Terminal.Gui.Attribute p1)
		|        {
		|            M_Attribute = p1;
		|            OneScriptTerminalGui.AddToHashtable(M_Attribute, this);
		|        }
		|
		|        public bool Initialized
		|        {
		|            get { return M_Attribute.Initialized; }
		|        }
		|
		|        public bool HasValidColors
		|        {
		|            get { return M_Attribute.HasValidColors; }
		|        }
		|
		|        public int Value
		|        {
		|            get { return M_Attribute.Value; }
		|        }
		|
		|        public int Foreground
		|        {
		|            get { return (int)M_Attribute.Foreground; }
		|        }
		|
		|        public int Background
		|        {
		|            get { return (int)M_Attribute.Background; }
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "SubviewCollection" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class SubviewCollection : System.Collections.IEnumerator, System.Collections.IEnumerable
		|    {
		|        public TfSubviewCollection dll_obj;
		|        public IList<Terminal.Gui.View> M_SubviewCollection;
		|        public System.Collections.IEnumerator Enumerator;
		|        public object current;
		|
		|        public SubviewCollection(IList<Terminal.Gui.View> p1)
		|        {
		|            M_SubviewCollection = p1;
		|        }
		|
		|        public IList<Terminal.Gui.View> List
		|        {
		|            get { return M_SubviewCollection; }
		|        }
		|
		|        public IEnumerator<Terminal.Gui.View> GetEnumerator()
		|        {
		|            return List.GetEnumerator();
		|        }
		|
		|        IEnumerator IEnumerable.GetEnumerator()
		|        {
		|            return List.GetEnumerator();
		|        }
		|
		|        public virtual object Current
		|        {
		|            get { return current; }
		|        }
		|
		|        public virtual void Reset()
		|        {
		|            Enumerator.Reset();
		|        }
		|
		|        public virtual bool MoveNext()
		|        {
		|            return Enumerator.MoveNext();
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "Window" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class Window : Toplevel
		|    {
		|        public new TfWindow dll_obj;
		|        public Terminal.Gui.Window M_Window;
		|
		|        public Window()
		|        {
		|            M_Window = new Terminal.Gui.Window();
		|            base.M_Toplevel = M_Window;
		|            OneScriptTerminalGui.AddToHashtable(M_Window, this);
		|            SetActions(M_Window);
		|        }
		|
		|        public Window(string p1)
		|        {
		|            M_Window = new Terminal.Gui.Window(p1);
		|            base.M_Toplevel = M_Window;
		|            OneScriptTerminalGui.AddToHashtable(M_Window, this);
		|            SetActions(M_Window);
		|        }
		|
		|        public Window(Rect p1, string p2)
		|        {
		|            M_Window = new Terminal.Gui.Window(p1.M_Rect, p2);
		|            base.M_Toplevel = M_Window;
		|            OneScriptTerminalGui.AddToHashtable(M_Window, this);
		|            SetActions(M_Window);
		|        }
		|
		|        public Window(string p1, int p2, Border p3)
		|        {
		|            M_Window = new Terminal.Gui.Window(p1, p2, p3.M_Border);
		|            base.M_Toplevel = M_Window;
		|            OneScriptTerminalGui.AddToHashtable(M_Window, this);
		|            SetActions(M_Window);
		|        }
		|
		|        public Window(Rect p1, string p2, int p3, Border p4)
		|        {
		|            M_Window = new Terminal.Gui.Window(p1.M_Rect, p2, p3, p4.M_Border);
		|            base.M_Toplevel = M_Window;
		|            OneScriptTerminalGui.AddToHashtable(M_Window, this);
		|            SetActions(M_Window);
		|        }
		|
		|        private void SetActions(Terminal.Gui.Window window)
		|        {
		|            window.TitleChanged += Window_TitleChanged;
		|            M_Window.Subviews[0].MouseEnter += Window_MouseEnter;
		|            M_Window.Subviews[0].MouseLeave += Window_MouseLeave;
		|            M_Window.MouseClick += Window_MouseClick;
		|            M_Window.Subviews[0].Leave += M_Window_Leave;
		|        }
		|
		|        private void M_Window_Leave(Terminal.Gui.View.FocusEventArgs obj)
		|        {
		|            if (dll_obj.Leave != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.Leave);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.Leave);
		|            }
		|        }
		|
		|        private void Window_MouseClick(Terminal.Gui.View.MouseEventArgs obj)
		|        {
		|            if (dll_obj.MouseClick != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.MouseClick);
		|                TfEventArgs1.flags = ValueFactory.Create((int)obj.MouseEvent.Flags);
		|                TfEventArgs1.view = OneScriptTerminalGui.RevertEqualsObj(M_Window).dll_obj;
		|                TfEventArgs1.x = ValueFactory.Create(obj.MouseEvent.X);
		|                TfEventArgs1.y = ValueFactory.Create(obj.MouseEvent.Y);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.MouseClick);
		|            }
		|        }
		|
		|        private void Window_MouseLeave(Terminal.Gui.View.MouseEventArgs obj)
		|        {
		|            if (dll_obj.MouseLeave != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.MouseLeave);
		|                TfEventArgs1.flags = ValueFactory.Create((int)obj.MouseEvent.Flags);
		|                TfEventArgs1.view = OneScriptTerminalGui.RevertEqualsObj(obj.MouseEvent.View).dll_obj;
		|                TfEventArgs1.x = ValueFactory.Create(obj.MouseEvent.X);
		|                TfEventArgs1.y = ValueFactory.Create(obj.MouseEvent.Y);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.MouseLeave);
		|            }
		|        }
		|
		|        private void Window_MouseEnter(Terminal.Gui.View.MouseEventArgs obj)
		|        {
		|            if (dll_obj.MouseEnter != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.MouseEnter);
		|                TfEventArgs1.flags = ValueFactory.Create((int)obj.MouseEvent.Flags);
		|                TfEventArgs1.view = OneScriptTerminalGui.RevertEqualsObj(M_Window).dll_obj;
		|                TfEventArgs1.x = ValueFactory.Create(obj.MouseEvent.X);
		|                TfEventArgs1.y = ValueFactory.Create(obj.MouseEvent.Y);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.MouseEnter);
		|            }
		|        }
		|
		|        private void Window_TitleChanged(Terminal.Gui.Window.TitleEventArgs obj)
		|        {
		|            if (dll_obj.TitleChanged != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.TitleChanged);
		|                TfEventArgs1.cancel = ValueFactory.Create(obj.Cancel);
		|                TfEventArgs1.newTitle = ValueFactory.Create(obj.NewTitle.ToString());
		|                TfEventArgs1.oldTitle = ValueFactory.Create(obj.OldTitle.ToString());
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.TitleChanged);
		|
		|                if (TfEventArgs1.Cancel)
		|                {
		|                    M_Window.TitleChanged -= Window_TitleChanged;
		|                    dll_obj.Title = TfEventArgs1.OldTitle;
		|                    M_Window.TitleChanged += Window_TitleChanged;
		|                }
		|            }
		|        }
		|
		|        public string Title
		|        {
		|            get { return M_Window.Title.ToString(); }
		|            set { M_Window.Title = value; }
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_Window.ToString();
		|        }
		|
		|        public new Window GetTopSuperView()
		|        {
		|            return OneScriptTerminalGui.RevertEqualsObj(M_Window.GetTopSuperView());
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "Border" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class Border
		|    {
		|        public TfBorder dll_obj;
		|        public Terminal.Gui.Border M_Border;
		|
		|        public Border()
		|        {
		|            M_Border = new Terminal.Gui.Border();
		|            OneScriptTerminalGui.AddToHashtable(M_Border, this);
		|            M_Border.BorderChanged += M_Border_BorderChanged;
		|        }
		|
		|        public Border(Terminal.Gui.Border p1)
		|        {
		|            M_Border = p1;
		|            OneScriptTerminalGui.AddToHashtable(M_Border, this);
		|            M_Border.BorderChanged += M_Border_BorderChanged;
		|        }
		|
		|        private void M_Border_BorderChanged(Terminal.Gui.Border obj)
		|        {
		|            if (dll_obj.BorderChanged != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.BorderChanged);
		|                TfEventArgs1.border = OneScriptTerminalGui.RevertEqualsObj(obj).dll_obj;
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.BorderChanged);
		|            }
		|        }
		|
		|        public int ActualHeight
		|        {
		|            get { return M_Border.ActualHeight; }
		|        }
		|
		|        public int ActualWidth
		|        {
		|            get { return M_Border.ActualWidth; }
		|        }
		|
		|        public string Title
		|        {
		|            get { return M_Border.Title.ToString(); }
		|            set { M_Border.Title = value; }
		|        }
		|
		|        public bool DrawMarginFrame
		|        {
		|            get { return M_Border.DrawMarginFrame; }
		|            set { M_Border.DrawMarginFrame = value; }
		|        }
		|
		|        public IValue Parent
		|        {
		|            get { return OneScriptTerminalGui.RevertEqualsObj(M_Border.Parent).dll_obj; }
		|        }
		|
		|        public Point Effect3DOffset
		|        {
		|            get { return new Point(M_Border.Effect3DOffset); }
		|            set { M_Border.Effect3DOffset = value.M_Point; }
		|        }
		|
		|        public int BorderStyle
		|        {
		|            get { return (int)M_Border.BorderStyle; }
		|            set { M_Border.BorderStyle = (Terminal.Gui.BorderStyle)value; }
		|        }
		|
		|        public Thickness BorderThickness
		|        {
		|            get { return new Thickness(M_Border.BorderThickness.Left, M_Border.BorderThickness.Top, M_Border.BorderThickness.Right, M_Border.BorderThickness.Bottom); }
		|            set { M_Border.BorderThickness = value.M_Thickness; }
		|        }
		|
		|        public Attribute Effect3DBrush
		|        {
		|            get { return new Attribute(M_Border.Effect3DBrush.Value); }
		|            set { M_Border.Effect3DBrush = value.M_Attribute; }
		|        }
		|
		|        public int BorderBrush
		|        {
		|            get { return (int)M_Border.BorderBrush; }
		|            set { M_Border.BorderBrush = (Terminal.Gui.Color)value; }
		|        }
		|
		|        public int Background
		|        {
		|            get { return (int)M_Border.Background; }
		|            set { M_Border.Background = (Terminal.Gui.Color)value; }
		|        }
		|
		|        public bool Effect3D
		|        {
		|            get { return M_Border.Effect3D; }
		|            set { M_Border.Effect3D = value; }
		|        }
		|
		|        public Thickness GetSumThickness()
		|        {
		|            Terminal.Gui.Thickness Thickness1 = M_Border.GetSumThickness();
		|            return new Thickness(Thickness1.Left, Thickness1.Top, Thickness1.Right, Thickness1.Bottom);
		|        }
		|
		|        public void DrawFullContent()
		|        {
		|            M_Border.DrawFullContent();
		|        }
		|
		|        public void DrawContent(View view = null, bool fill = true)
		|        {
		|            M_Border.DrawContent(view.M_View, fill);
		|        }
		|
		|        public void DrawTitle(View p1)
		|        {
		|            M_Border.DrawTitle(p1.M_View);
		|        }
		|
		|        public void DrawTitle(View p1, Rect p2)
		|        {
		|            M_Border.DrawTitle(p1.M_View, p2.M_Rect);
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_Border.ToString();
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "View" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class View : Responder
		|    {
		|        public TfView dll_obj;
		|        private Terminal.Gui.View m_View;
		|
		|        public View(Terminal.Gui.View view = null)
		|        {
		|            if (view != null)
		|            {
		|                M_View = view;
		|                OneScriptTerminalGui.AddToHashtable(M_View, this);
		|            }
		|            else
		|            {
		|                M_View = new Terminal.Gui.View();
		|                OneScriptTerminalGui.AddToHashtable(M_View, this);
		|            }
		|        }
		|        public View(Terminal.Gui.Rect p1)
		|        {
		|            M_View = new Terminal.Gui.View(p1);
		|            OneScriptTerminalGui.AddToHashtable(M_View, this);
		|        }
		|        public View(int p1, int p2, string p3)
		|        {
		|            M_View = new Terminal.Gui.View(p1, p2, p3);
		|            OneScriptTerminalGui.AddToHashtable(M_View, this);
		|        }
		|        public View(Terminal.Gui.Rect p1, string p2, Terminal.Gui.Border p3)
		|        {
		|            M_View = new Terminal.Gui.View(p1, p2, p3);
		|            OneScriptTerminalGui.AddToHashtable(M_View, this);
		|        }
		|        public View(string p1, int p2, Terminal.Gui.Border p3)
		|        {
		|            M_View = new Terminal.Gui.View(p1, (Terminal.Gui.TextDirection)p2, p3);
		|            OneScriptTerminalGui.AddToHashtable(M_View, this);
		|        }
		|
		|        public Terminal.Gui.View M_View
		|        {
		|            get { return m_View; }
		|            set
		|            {
		|                m_View = value;
		|                base.M_Responder = m_View;
		|                m_View.Added += M_View_Added;
		|                m_View.CanFocusChanged += M_View_CanFocusChanged;
		|                //m_View.DrawContent += M_View_DrawContent;
		|                //m_View.DrawContentComplete += M_View_DrawContentComplete;
		|                m_View.EnabledChanged += M_View_EnabledChanged;
		|                m_View.Enter += M_View_Enter;
		|                m_View.HotKeyChanged += M_View_HotKeyChanged;
		|                m_View.Initialized += M_View_Initialized;
		|                //m_View.KeyDown += M_View_KeyDown;
		|                m_View.KeyPress += M_View_KeyPress;
		|                //m_View.KeyUp += M_View_KeyUp;
		|                //m_View.LayoutComplete += M_View_LayoutComplete;
		|                //m_View.LayoutStarted += M_View_LayoutStarted;
		|                m_View.Leave += M_View_Leave;
		|                m_View.MouseClick += M_View_MouseClick;
		|                m_View.MouseEnter += M_View_MouseEnter;
		|                m_View.MouseLeave += M_View_MouseLeave;
		|                m_View.Removed += M_View_Removed;
		|                m_View.VisibleChanged += M_View_VisibleChanged;
		|
		|                System.Action OnShortcutAction = delegate ()
		|                {
		|                    if (OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj.ShortcutAction != null)
		|                    {
		|                        TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                        TfEventArgs1.sender = dll_obj;
		|                        TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj.ShortcutAction);
		|                        OneScriptTerminalGui.Event = TfEventArgs1;
		|                        OneScriptTerminalGui.ExecuteEvent(OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj.ShortcutAction);
		|                    }
		|                };
		|                m_View.ShortcutAction = OnShortcutAction;
		|
		|                // Обеспечим данными событие мыши MouseEnter.
		|                Application.RootMouseEvent += delegate (MouseEvent me)
		|                {
		|                    Terminal.Gui.View host1 = me.View;
		|                    MouseEvent myme = new MouseEvent();
		|                    myme.Flags = me.Flags;
		|                    myme.Handled = me.Handled;
		|                    myme.View = me.View;
		|                    int meX = me.X;
		|                    int meY = me.Y;
		|                    Terminal.Gui.Point point = host1.ScreenToView(me.X, me.Y);
		|                    int frameX = host1.Frame.X;
		|                    int frameY = host1.Frame.Y;
		|                    int frameWidth = host1.Frame.Width;
		|                    int frameHeight = host1.Frame.Height;
		|                    int x = point.X;
		|                    int y = point.Y;
		|                    if (me.View.GetType().ToString() == ""Terminal.Gui.Window+ContentView"")
		|                    {
		|                        if (meX >= (frameX + 1))
		|                        {
		|                            if (meY >= (frameY + 1))
		|                            {
		|                                if (meX < (frameX + frameWidth + 1))
		|                                {
		|                                    if (meY > (frameY + frameHeight + 1))
		|                                    {
		|                                        myme.X = x;
		|                                        myme.Y = y;
		|                                        host1.OnMouseEnter(myme);
		|                                    }
		|                                }
		|                            }
		|                        }
		|                    }
		|                    else if (me.View.GetType().ToString() == ""Terminal.Gui.Window"")
		|                    {
		|                        // Ничего не делаем.
		|                    }
		|                    else
		|                    {
		|                        if (meX >= frameX)
		|                        {
		|                            if (meY >= frameY)
		|                            {
		|                                if (meX < (frameX + frameWidth))
		|                                {
		|                                    if (meY < (frameY + frameHeight))
		|                                    {
		|                                        myme.X = x;
		|                                        myme.Y = y;
		|                                        host1.OnMouseEnter(myme);
		|                                    }
		|                                }
		|                            }
		|                        }
		|                    }
		|                };
		|            }
		|        }
		|
		|        private void M_View_VisibleChanged()
		|        {
		|            dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|            if (Sender.VisibleChanged != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = Sender;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.VisibleChanged);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(Sender.VisibleChanged);
		|            }
		|        }
		|
		|        private void M_View_Removed(Terminal.Gui.View obj)
		|        {
		|            dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|            if (Sender.Removed != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = Sender;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.Removed);
		|                TfEventArgs1.view = OneScriptTerminalGui.RevertEqualsObj(obj).dll_obj;
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(Sender.Removed);
		|            }
		|        }
		|
		|        private void M_View_MouseLeave(Terminal.Gui.View.MouseEventArgs obj)
		|        {
		|            dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|            if (Sender.GetType() == typeof(TfWindow))
		|            {
		|                return;
		|            }
		|            if (Sender.GetType() == typeof(TfMenuBar))
		|            {
		|                return;
		|            }
		|            if (Sender.MouseLeave != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = Sender;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.MouseLeave);
		|                TfEventArgs1.flags = ValueFactory.Create((int)obj.MouseEvent.Flags);
		|                TfEventArgs1.view = OneScriptTerminalGui.RevertEqualsObj(obj.MouseEvent.View).dll_obj;
		|                TfEventArgs1.x = ValueFactory.Create(obj.MouseEvent.X);
		|                TfEventArgs1.y = ValueFactory.Create(obj.MouseEvent.Y);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(Sender.MouseLeave);
		|            }
		|        }
		|
		|        private void M_View_MouseEnter(Terminal.Gui.View.MouseEventArgs obj)
		|        {
		|            dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|            if (Sender.GetType() == typeof(TfWindow))
		|            {
		|                return;
		|            }
		|            if (Sender.MouseEnter != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = Sender;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.MouseEnter);
		|                TfEventArgs1.flags = ValueFactory.Create((int)obj.MouseEvent.Flags);
		|                TfEventArgs1.view = OneScriptTerminalGui.RevertEqualsObj(obj.MouseEvent.View).dll_obj;
		|                TfEventArgs1.x = ValueFactory.Create(obj.MouseEvent.X);
		|                TfEventArgs1.y = ValueFactory.Create(obj.MouseEvent.Y);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(Sender.MouseEnter);
		|            }
		|        }
		|
		|        private void M_View_MouseClick(Terminal.Gui.View.MouseEventArgs obj)
		|        {
		|            dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|            if (Sender.GetType() == typeof(TfWindow))
		|            {
		|                return;
		|            }
		|            if (Sender.MouseClick != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = Sender;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.MouseClick);
		|                TfEventArgs1.flags = ValueFactory.Create((int)obj.MouseEvent.Flags);
		|                TfEventArgs1.view = OneScriptTerminalGui.RevertEqualsObj(obj.MouseEvent.View).dll_obj;
		|                TfEventArgs1.x = ValueFactory.Create(obj.MouseEvent.X);
		|                TfEventArgs1.y = ValueFactory.Create(obj.MouseEvent.Y);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(Sender.MouseClick);
		|            }
		|        }
		|
		|        private void M_View_Leave(Terminal.Gui.View.FocusEventArgs obj)
		|        {
		|            dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|            if (Sender.GetType() == typeof(TfWindow))
		|            {
		|                return;
		|            }
		|            if (Sender.Leave != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = Sender;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.Leave);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(Sender.Leave);
		|            }
		|        }
		|
		|        //private void M_View_LayoutStarted(Terminal.Gui.View.LayoutEventArgs obj)
		|        //{
		|        //    dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|        //    if (Sender.LayoutStarted != null)
		|        //    {
		|        //        TfEventArgs TfEventArgs1 = new TfEventArgs();
		|        //        TfEventArgs1.sender = Sender;
		|        //        TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.LayoutStarted);
		|        //        TfEventArgs1.oldBounds = new TfRect(obj.OldBounds.X, obj.OldBounds.Y, obj.OldBounds.Width, obj.OldBounds.Height);
		|        //        OneScriptTerminalGui.Event = TfEventArgs1;
		|        //        OneScriptTerminalGui.ExecuteEvent(Sender.LayoutStarted);
		|        //    }
		|        //}
		|
		|        //private void M_View_LayoutComplete(Terminal.Gui.View.LayoutEventArgs obj)
		|        //{
		|        //    dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|        //    if (Sender.LayoutComplete != null)
		|        //    {
		|        //        TfEventArgs TfEventArgs1 = new TfEventArgs();
		|        //        TfEventArgs1.sender = Sender;
		|        //        TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.LayoutComplete);
		|        //        TfEventArgs1.oldBounds = new TfRect(obj.OldBounds.X, obj.OldBounds.Y, obj.OldBounds.Width, obj.OldBounds.Height);
		|        //        OneScriptTerminalGui.Event = TfEventArgs1;
		|        //        OneScriptTerminalGui.ExecuteEvent(Sender.LayoutComplete);
		|        //    }
		|        //}
		|
		|        private void M_View_KeyUp(Terminal.Gui.View.KeyEventEventArgs obj)
		|        {
		|            dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|            if (Sender.KeyUp != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = Sender;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.KeyUp);
		|                TfEventArgs1.isAlt = ValueFactory.Create(obj.KeyEvent.IsAlt);
		|                TfEventArgs1.isCapslock = ValueFactory.Create(obj.KeyEvent.IsCapslock);
		|                TfEventArgs1.isCtrl = ValueFactory.Create(obj.KeyEvent.IsCtrl);
		|                TfEventArgs1.isNumlock = ValueFactory.Create(obj.KeyEvent.IsNumlock);
		|                TfEventArgs1.isScrolllock = ValueFactory.Create(obj.KeyEvent.IsScrolllock);
		|                TfEventArgs1.isShift = ValueFactory.Create(obj.KeyEvent.IsShift);
		|                TfEventArgs1.keyValue = ValueFactory.Create(obj.KeyEvent.KeyValue);
		|                TfEventArgs1.keyToString = ValueFactory.Create(obj.KeyEvent.Key.ToString());
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(Sender.KeyUp);
		|            }
		|        }
		|
		|        private void M_View_KeyPress(Terminal.Gui.View.KeyEventEventArgs obj)
		|        {
		|            dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|            TfAction shortcutAction = null;
		|            try
		|            {
		|                shortcutAction = Sender.ShortcutAction;
		|            }
		|            catch
		|            {
		|                return;
		|            }
		|            if (shortcutAction != null)
		|            {
		|                if (obj.KeyEvent.Key == (Terminal.Gui.Key)Sender.Shortcut)
		|                {
		|                    M_View.ShortcutAction.Invoke();
		|                }
		|            }
		|            if (Sender.KeyPress != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = Sender;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.KeyPress);
		|                TfEventArgs1.isAlt = ValueFactory.Create(obj.KeyEvent.IsAlt);
		|                TfEventArgs1.isCapslock = ValueFactory.Create(obj.KeyEvent.IsCapslock);
		|                TfEventArgs1.isCtrl = ValueFactory.Create(obj.KeyEvent.IsCtrl);
		|                TfEventArgs1.isNumlock = ValueFactory.Create(obj.KeyEvent.IsNumlock);
		|                TfEventArgs1.isScrolllock = ValueFactory.Create(obj.KeyEvent.IsScrolllock);
		|                TfEventArgs1.isShift = ValueFactory.Create(obj.KeyEvent.IsShift);
		|                TfEventArgs1.keyValue = ValueFactory.Create(obj.KeyEvent.KeyValue);
		|                TfEventArgs1.keyToString = ValueFactory.Create(obj.KeyEvent.Key.ToString());
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(Sender.KeyPress);
		|            }
		|        }
		|
		|        private void M_View_KeyDown(Terminal.Gui.View.KeyEventEventArgs obj)
		|        {
		|            dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|            if (Sender.KeyDown != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = Sender;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.KeyDown);
		|                TfEventArgs1.isAlt = ValueFactory.Create(obj.KeyEvent.IsAlt);
		|                TfEventArgs1.isCapslock = ValueFactory.Create(obj.KeyEvent.IsCapslock);
		|                TfEventArgs1.isCtrl = ValueFactory.Create(obj.KeyEvent.IsCtrl);
		|                TfEventArgs1.isNumlock = ValueFactory.Create(obj.KeyEvent.IsNumlock);
		|                TfEventArgs1.isScrolllock = ValueFactory.Create(obj.KeyEvent.IsScrolllock);
		|                TfEventArgs1.isShift = ValueFactory.Create(obj.KeyEvent.IsShift);
		|                TfEventArgs1.keyValue = ValueFactory.Create(obj.KeyEvent.KeyValue);
		|                TfEventArgs1.keyToString = ValueFactory.Create(obj.KeyEvent.Key.ToString());
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(Sender.KeyDown);
		|            }
		|        }
		|
		|        private void M_View_Initialized(object sender, System.EventArgs e)
		|        {
		|            dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|            if (Sender.InitializedItem != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = Sender;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.InitializedItem);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(Sender.InitializedItem);
		|            }
		|        }
		|
		|        private void M_View_HotKeyChanged(Terminal.Gui.Key obj)
		|        {
		|            dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|            if (Sender.HotKeyChanged != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = Sender;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.HotKeyChanged);
		|                TfEventArgs1.keyValue = ValueFactory.Create((int)obj);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(Sender.HotKeyChanged);
		|            }
		|        }
		|
		|        private void M_View_Enter(Terminal.Gui.View.FocusEventArgs obj)
		|        {
		|            dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|            if (Sender.Enter != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = Sender;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.Enter);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(Sender.Enter);
		|            }
		|        }
		|
		|        private void M_View_EnabledChanged()
		|        {
		|            dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|            if (Sender.EnabledChanged != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = Sender;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.EnabledChanged);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(Sender.EnabledChanged);
		|            }
		|        }
		|
		|        //private void M_View_DrawContentComplete(Terminal.Gui.Rect obj)
		|        //{
		|        //    dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|        //    if (Sender.DrawContentComplete != null)
		|        //    {
		|        //        TfEventArgs TfEventArgs1 = new TfEventArgs();
		|        //        TfEventArgs1.sender = Sender;
		|        //        TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.DrawContentComplete);
		|        //        TfEventArgs1.rect = new TfRect(obj.X, obj.Y, obj.Width, obj.Height);
		|        //        OneScriptTerminalGui.Event = TfEventArgs1;
		|        //        OneScriptTerminalGui.ExecuteEvent(Sender.DrawContentComplete);
		|        //    }
		|        //}
		|
		|        //private void M_View_DrawContent(Terminal.Gui.Rect obj)
		|        //{
		|        //    dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|        //    if (Sender.DrawContent != null)
		|        //    {
		|        //        TfEventArgs TfEventArgs1 = new TfEventArgs();
		|        //        TfEventArgs1.sender = Sender;
		|        //        TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.DrawContent);
		|        //        TfEventArgs1.rect = new TfRect(obj.X, obj.Y, obj.Width, obj.Height);
		|        //        OneScriptTerminalGui.Event = TfEventArgs1;
		|        //        OneScriptTerminalGui.ExecuteEvent(Sender.DrawContent);
		|        //    }
		|        //}
		|
		|        private void M_View_CanFocusChanged()
		|        {
		|            dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|            if (Sender.CanFocusChanged != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = Sender;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.CanFocusChanged);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(Sender.CanFocusChanged);
		|            }
		|        }
		|
		|        private void M_View_Added(Terminal.Gui.View obj)
		|        {
		|            dynamic Sender = OneScriptTerminalGui.RevertEqualsObj(M_View).dll_obj;
		|            if (Sender.Added != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = Sender;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(Sender.Added);
		|                TfEventArgs1.view = OneScriptTerminalGui.RevertEqualsObj(obj).dll_obj;
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(Sender.Added);
		|            }
		|        }
		|
		|        public ostgui.Pos X
		|        {
		|            get { return new Pos(Terminal.Gui.Pos.X(M_View)); }
		|            set
		|            {
		|                if (M_View.GetType() != typeof(Terminal.Gui.Window))
		|                {
		|                    if (value.M_Pos.ToString().Contains(""Pos.Absolute""))
		|                    {
		|                        M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Absolute;
		|                    }
		|                }
		|                M_View.X = value.M_Pos;
		|            }
		|        }
		|
		|        public ostgui.Pos Y
		|        {
		|            get { return new Pos(Terminal.Gui.Pos.Y(M_View)); }
		|            set { M_View.Y = value.M_Pos; }
		|        }
		|
		|        public string Text
		|        {
		|            get { return M_View.Text.ToString(); }
		|            set { M_View.Text = value; }
		|        }
		|
		|        public ostgui.Dim Width
		|        {
		|            get { return OneScriptTerminalGui.RevertEqualsObj(M_View.Width); }
		|            set { M_View.Width = value.M_Dim; }
		|        }
		|
		|        public ostgui.Dim Height
		|        {
		|            get { return OneScriptTerminalGui.RevertEqualsObj(M_View.Height); }
		|            set { M_View.Height = value.M_Dim; }
		|        }
		|
		|        public ostgui.Pos Left
		|        {
		|            get { return new Pos(Terminal.Gui.Pos.Left(M_View)); }
		|        }
		|
		|        public ostgui.Pos Right
		|        {
		|            get { return new Pos(Terminal.Gui.Pos.Right(M_View)); }
		|        }
		|
		|        public ostgui.Pos Top
		|        {
		|            get { return new Pos(Terminal.Gui.Pos.Top(M_View)); }
		|        }
		|
		|        public ostgui.Pos Bottom
		|        {
		|            get { return new Pos(Terminal.Gui.Pos.Bottom(M_View)); }
		|        }
		|
		|        public int LayoutStyle
		|        {
		|            get { return (int)M_View.LayoutStyle; }
		|            set { M_View.LayoutStyle = (LayoutStyle)value; }
		|        }
		|
		|        public bool AutoSize
		|        {
		|            get { return M_View.AutoSize; }
		|            set { M_View.AutoSize = value; }
		|        }
		|
		|        public int VerticalTextAlignment
		|        {
		|            get { return (int)M_View.VerticalTextAlignment; }
		|            set { M_View.VerticalTextAlignment = (Terminal.Gui.VerticalTextAlignment)value; }
		|        }
		|
		|        public int TextAlignment
		|        {
		|            get { return (int)M_View.TextAlignment; }
		|            set { M_View.TextAlignment = (TextAlignment)value; }
		|        }
		|
		|        public int HotKey
		|        {
		|            get { return (int)M_View.HotKey; }
		|            set { M_View.HotKey = (Terminal.Gui.Key)value; }
		|        }
		|
		|        public bool IsAdded
		|        {
		|            get { return M_View.IsAdded; }
		|        }
		|
		|        public bool IgnoreBorderPropertyOnRedraw
		|        {
		|            get { return M_View.IgnoreBorderPropertyOnRedraw; }
		|            set { M_View.IgnoreBorderPropertyOnRedraw = value; }
		|        }
		|
		|        public bool IsInitialized
		|        {
		|            get { return M_View.IsInitialized; }
		|            set { M_View.IsInitialized = value; }
		|        }
		|
		|        public string Id
		|        {
		|            get { return M_View.Id.ToString(); }
		|            set { M_View.Id = value; }
		|        }
		|
		|        public int TextDirection
		|        {
		|            get { return (int)M_View.TextDirection; }
		|            set { M_View.TextDirection = (Terminal.Gui.TextDirection)value; }
		|        }
		|
		|        public bool ClearOnVisibleFalse
		|        {
		|            get { return M_View.ClearOnVisibleFalse; }
		|            set { M_View.ClearOnVisibleFalse = value; }
		|        }
		|
		|        public bool WantContinuousButtonPressed
		|        {
		|            get { return M_View.WantContinuousButtonPressed; }
		|            set { M_View.WantContinuousButtonPressed = value; }
		|        }
		|
		|        public bool WantMousePositionReports
		|        {
		|            get { return M_View.WantMousePositionReports; }
		|            set { M_View.WantMousePositionReports = value; }
		|        }
		|
		|        public int TabIndex
		|        {
		|            get { return M_View.TabIndex; }
		|            set { M_View.TabIndex = value; }
		|        }
		|
		|        public Rune HotKeySpecifier
		|        {
		|            get { return M_View.HotKeySpecifier; }
		|            set { M_View.HotKeySpecifier = value; }
		|        }
		|
		|        public bool PreserveTrailingSpaces
		|        {
		|            get { return M_View.PreserveTrailingSpaces; }
		|            set { M_View.PreserveTrailingSpaces = value; }
		|        }
		|
		|        public Terminal.Gui.Key shortcut;
		|        [ContextProperty(""СочетаниеКлавиш"", ""Shortcut"")]
		|        public int Shortcut
		|        {
		|            get { return (int)shortcut; }
		|            set { shortcut = (Terminal.Gui.Key)value; }
		|        }
		|
		|        public string ShortcutTag
		|        {
		|            get { return M_View.ShortcutTag.ToString(); }
		|        }
		|
		|        public bool IsCurrentTop
		|        {
		|            get
		|            {
		|                bool isCurrentTop = false;
		|                Terminal.Gui.View parent = M_View.SuperView;
		|                int num = parent.Subviews.IndexOf(M_View);
		|                int count = parent.Subviews.Count;
		|                if (num == (count - 1))
		|                {
		|                    isCurrentTop = true;
		|                }
		|                return isCurrentTop;
		|            }
		|        }
		|
		|        public bool TabStop
		|        {
		|            get { return M_View.TabStop; }
		|            set { M_View.TabStop = value; }
		|        }
		|
		|        public IValue Focused
		|        {
		|            get
		|            {
		|                if (M_View.Focused != null)
		|                {
		|                    return OneScriptTerminalGui.RevertEqualsObj(M_View.Focused).dll_obj;
		|                }
		|                return null;
		|            }
		|        }
		|
		|        public Border Border
		|        {
		|            get { return OneScriptTerminalGui.RevertEqualsObj(M_View.Border); }
		|            set { M_View.Border = value.M_Border; }
		|        }
		|
		|        public Rect Bounds
		|        {
		|            get { return new Rect(M_View.Frame.X, M_View.Frame.Y, M_View.Bounds.Width, M_View.Bounds.Height); }
		|        }
		|
		|        public object Data
		|        {
		|            get { return M_View.Data; }
		|            set { M_View.Data = value; }
		|        }
		|
		|        public Rect Frame
		|        {
		|            get { return new Rect(M_View.Frame.X, M_View.Frame.Y, M_View.Frame.Width, M_View.Frame.Height); }
		|            set { M_View.Frame = value.M_Rect; }
		|        }
		|
		|        public TextFormatter TextFormatter
		|        {
		|            get { return OneScriptTerminalGui.RevertEqualsObj(M_View.TextFormatter); }
		|            set { M_View.TextFormatter = value.M_TextFormatter; }
		|        }
		|
		|        public View SuperView
		|        {
		|            get { return OneScriptTerminalGui.RevertEqualsObj(M_View.SuperView); }
		|        }
		|
		|        public ColorScheme ColorScheme
		|        {
		|            get { return OneScriptTerminalGui.RevertEqualsObj(M_View.ColorScheme); }
		|            set { M_View.ColorScheme = value.M_ColorScheme; }
		|        }
		|
		|        public Attribute GetFocusColor()
		|        {
		|            return new Attribute(M_View.GetFocusColor());
		|        }
		|
		|        public Attribute GetNormalColor()
		|        {
		|            return new Attribute(M_View.GetNormalColor());
		|        }
		|
		|        public void SetFocus()
		|        {
		|            M_View.SetFocus();
		|        }
		|
		|        public void RemoveAll()
		|        {
		|            M_View.RemoveAll();
		|        }
		|
		|        public void Remove(View p1)
		|        {
		|            M_View.Remove(p1.M_View);
		|        }
		|
		|        public Point ScreenToView(int p1, int p2)
		|        {
		|            return new Point(M_View.ScreenToView(p1, p2));
		|        }
		|
		|        public void LayoutSubviews()
		|        {
		|            M_View.LayoutSubviews();
		|        }
		|
		|        public Size GetAutoSize()
		|        {
		|            return new Size(M_View.GetAutoSize());
		|        }
		|
		|        public void SetChildNeedsDisplay()
		|        {
		|            M_View.SetChildNeedsDisplay();
		|        }
		|
		|        public void Redraw(Rect p1)
		|        {
		|            M_View.Redraw(p1.M_Rect);
		|        }
		|
		|        public void Clear()
		|        {
		|            M_View.Clear();
		|        }
		|
		|        public void SendSubviewBackwards(View p1)
		|        {
		|            M_View.SendSubviewBackwards(p1.M_View);
		|        }
		|
		|        public void SendSubviewToBack(View p1)
		|        {
		|            M_View.SendSubviewToBack(p1.M_View);
		|        }
		|
		|        public void SetNeedsDisplay(Rect p1 = null)
		|        {
		|            M_View.SetNeedsDisplay(p1.M_Rect);
		|        }
		|
		|        public void BringSubviewToFront(View p1)
		|        {
		|            M_View.BringSubviewToFront(p1.M_View);
		|        }
		|
		|        public void Add(View p1)
		|        {
		|            M_View.Add(p1.M_View);
		|        }
		|
		|        public void BringSubviewForward(View p1)
		|        {
		|            M_View.BringSubviewForward(p1.M_View);
		|        }
		|
		|        public void CorrectionZet()
		|        {
		|            // Необходимая коррекция z-порядка элементов при запуске приложения.
		|            // Найдено экспериментальным путем.
		|            Terminal.Gui.View[] array1 = new Terminal.Gui.View[M_View.Subviews.Count];
		|            for (int i = 0; i < M_View.Subviews.Count; i++)
		|            {
		|                Terminal.Gui.View view1 = M_View.Subviews[i];
		|                array1[i] = view1;
		|            }
		|            M_View.RemoveAll();
		|            //M_View.Add(array1[3]);
		|            //M_View.Add(array1[0]);
		|            //M_View.Add(array1[1]);
		|            //M_View.Add(array1[2]);
		|            M_View.Add(array1[array1.Length - 1]);
		|            for (int i = 0; i < array1.Length - 1; i++)
		|            {
		|                M_View.Add(array1[i]);
		|            }
		|        }
		|
		|        public View GetTopSuperView()
		|        {
		|            return OneScriptTerminalGui.RevertEqualsObj(M_View.GetTopSuperView());
		|        }
		|
		|        public System.Action ShortcutAction
		|        {
		|            get { return M_View.ShortcutAction; }
		|            set { M_View.ShortcutAction = value; }
		|        }
		|
		|        public ostgui.Attribute GetHotNormalColor()
		|        {
		|            return new Attribute(M_View.GetHotNormalColor());
		|        }
		|
		|        public void PlaceTop(View p1, int p2)
		|        {
		|            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
		|            M_View.Y = Terminal.Gui.Pos.Top(p1.M_View) - M_View.Frame.Height - p2 - 1;
		|        }
		|
		|        public void PlaceLeft(View p1, int p2)
		|        {
		|            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
		|            M_View.X = Terminal.Gui.Pos.Left(p1.M_View) - M_View.Frame.Width - p2 - 1;
		|        }
		|
		|        public void PlaceBottom(View p1, int p2)
		|        {
		|            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
		|            M_View.Y = Terminal.Gui.Pos.Bottom(p1.M_View) + p2 + 1;
		|        }
		|
		|        public void PlaceRight(View p1, int p2)
		|        {
		|            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
		|            M_View.X = Terminal.Gui.Pos.Right(p1.M_View) + p2 + 1;
		|        }
		|
		|        public void PlaceTopLeft(View p1, int p2, int p3)
		|        {
		|            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
		|            M_View.Y = Terminal.Gui.Pos.Top(p1.M_View) - M_View.Frame.Height - p2 - 1;
		|
		|            M_View.X = Terminal.Gui.Pos.Left(p1.M_View) - M_View.Frame.Width - p3 - 1;
		|        }
		|
		|        public void PlaceTopRight(View p1, int p2, int p3)
		|        {
		|            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
		|            M_View.Y = Terminal.Gui.Pos.Top(p1.M_View) - M_View.Frame.Height - p2 - 1;
		|
		|            M_View.X = Terminal.Gui.Pos.Right(p1.M_View) + p3 + 1;
		|        }
		|
		|        public void PlaceBottomLeft(View p1, int p2, int p3)
		|        {
		|            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
		|            M_View.Y = Terminal.Gui.Pos.Bottom(p1.M_View) + p2 + 1;
		|
		|            M_View.X = Terminal.Gui.Pos.Left(p1.M_View) - M_View.Frame.Width - p3 - 1;
		|        }
		|
		|        public void PlaceBottomRight(View p1, int p2, int p3)
		|        {
		|            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
		|            M_View.Y = Terminal.Gui.Pos.Bottom(p1.M_View) + p2 + 1;
		|
		|            M_View.X = Terminal.Gui.Pos.Right(p1.M_View) + p3 + 1;
		|        }
		|
		|        public void PlaceLeftTop(View p1, int p2, int p3)
		|        {
		|            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
		|            M_View.X = Terminal.Gui.Pos.Left(p1.M_View) - M_View.Frame.Width - p2 - 1;
		|
		|            M_View.Y = Terminal.Gui.Pos.Top(p1.M_View) - M_View.Frame.Height - p3 - 1;
		|        }
		|
		|        public void PlaceLeftBottom(View p1, int p2, int p3)
		|        {
		|            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
		|            M_View.X = Terminal.Gui.Pos.Left(p1.M_View) - M_View.Frame.Width - p2 - 1;
		|
		|            M_View.Y = Terminal.Gui.Pos.Bottom(p1.M_View) + p3 + 1;
		|        }
		|
		|        public void PlaceRightTop(View p1, int p2, int p3)
		|        {
		|            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
		|            M_View.X = Terminal.Gui.Pos.Right(p1.M_View) + p2 + 1;
		|
		|            M_View.Y = Terminal.Gui.Pos.Top(p1.M_View) - M_View.Frame.Height - p3 - 1;
		|        }
		|
		|        public void PlaceRightBottom(View p1, int p2, int p3)
		|        {
		|            M_View.LayoutStyle = Terminal.Gui.LayoutStyle.Computed;
		|            M_View.X = Terminal.Gui.Pos.Right(p1.M_View) + p2 + 1;
		|
		|            M_View.Y = Terminal.Gui.Pos.Bottom(p1.M_View) + p3 + 1;
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_View.ToString();
		|        }
		|
		|        public SubviewCollection Subviews
		|        {
		|            get { return new SubviewCollection(M_View.Subviews); }
		|        }
		|
		|        public void Center(int p1 = 0, int p2 = 0)
		|        {
		|            if (p1 != 0)
		|            {
		|                M_View.X = Terminal.Gui.Pos.Center() + p1 - (M_View.Frame.Width / 2) - 1;
		|            }
		|            else
		|            {
		|                M_View.X = Terminal.Gui.Pos.Center();
		|            }
		|            if (p2 != 0)
		|            {
		|                M_View.Y = Terminal.Gui.Pos.Center() + p2 - (M_View.Frame.Height / 2) - 1;
		|            }
		|            else
		|            {
		|                M_View.Y = Terminal.Gui.Pos.Center();
		|            }
		|        }
		|
		|        public void Fill(int p1 = 0, int p2 = 0)
		|        {
		|            M_View.Width = Terminal.Gui.Dim.Fill(p1);
		|            M_View.Height = Terminal.Gui.Dim.Fill(p1);
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "Toplevel" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class Toplevel : View
		|    {
		|        public new TfToplevel dll_obj;
		|        public Terminal.Gui.Toplevel m_Toplevel;
		|
		|        public Terminal.Gui.Toplevel M_Toplevel
		|        {
		|            get { return m_Toplevel; }
		|            set
		|            {
		|                m_Toplevel = value;
		|                base.M_View = m_Toplevel;
		|            }
		|        }
		|
		|        public Toplevel()
		|        {
		|            M_Toplevel = new Terminal.Gui.Toplevel();
		|            base.M_View = M_Toplevel;
		|            OneScriptTerminalGui.AddToHashtable(M_Toplevel, this);
		|            SetActions(M_Toplevel);
		|        }
		|
		|        public Toplevel(Terminal.Gui.Rect p1)
		|        {
		|            M_Toplevel = new Terminal.Gui.Toplevel(p1);
		|            base.M_View = M_Toplevel;
		|            OneScriptTerminalGui.AddToHashtable(M_Toplevel, this);
		|            SetActions(M_Toplevel);
		|        }
		|
		|        public Toplevel(Terminal.Gui.Toplevel p1)
		|        {
		|            M_Toplevel = p1;
		|            base.M_View = M_Toplevel;
		|            OneScriptTerminalGui.AddToHashtable(M_Toplevel, this);
		|            SetActions(M_Toplevel);
		|        }
		|
		|        private void SetActions(Terminal.Gui.Toplevel toplevel)
		|        {
		|            toplevel.Activate += M_Toplevel_Activate;
		|            toplevel.AllChildClosed += M_Toplevel_AllChildClosed;
		|            toplevel.ChildClosed += M_Toplevel_ChildClosed;
		|            toplevel.ChildLoaded += M_Toplevel_ChildLoaded;
		|            toplevel.ChildUnloaded += M_Toplevel_ChildUnloaded;
		|            toplevel.Closed += M_Toplevel_Closed;
		|            toplevel.Closing += M_Toplevel_Closing;
		|            toplevel.Deactivate += M_Toplevel_Deactivate;
		|            toplevel.Loaded += M_Toplevel_Loaded;
		|            toplevel.QuitKeyChanged += M_Toplevel_QuitKeyChanged;
		|            toplevel.Ready += M_Toplevel_Ready;
		|            toplevel.Resized += M_Toplevel_Resized;
		|            toplevel.Unloaded += M_Toplevel_Unloaded;
		|        }
		|
		|        private void M_Toplevel_Activate(Terminal.Gui.Toplevel obj)
		|        {
		|            if (dll_obj.Activate != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.Activate);
		|                TfEventArgs1.view = OneScriptTerminalGui.RevertEqualsObj(obj).dll_obj;
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.Activate);
		|            }
		|        }
		|
		|        private void M_Toplevel_AllChildClosed()
		|        {
		|            if (dll_obj.AllChildClosed != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.AllChildClosed);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.AllChildClosed);
		|            }
		|        }
		|
		|        private void M_Toplevel_ChildClosed(Terminal.Gui.Toplevel obj)
		|        {
		|            if (dll_obj.ChildClosed != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.ChildClosed);
		|                TfEventArgs1.view = OneScriptTerminalGui.RevertEqualsObj(obj).dll_obj;
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.ChildClosed);
		|            }
		|        }
		|
		|        private void M_Toplevel_ChildLoaded(Terminal.Gui.Toplevel obj)
		|        {
		|            if (dll_obj.ChildLoaded != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.ChildLoaded);
		|                TfEventArgs1.view = OneScriptTerminalGui.RevertEqualsObj(obj).dll_obj;
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.ChildLoaded);
		|            }
		|        }
		|
		|        private void M_Toplevel_ChildUnloaded(Terminal.Gui.Toplevel obj)
		|        {
		|            if (dll_obj.ChildUnloaded != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.ChildUnloaded);
		|                TfEventArgs1.view = OneScriptTerminalGui.RevertEqualsObj(obj).dll_obj;
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.ChildUnloaded);
		|            }
		|        }
		|
		|        private void M_Toplevel_Closed(Terminal.Gui.Toplevel obj)
		|        {
		|            if (dll_obj.Closed != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.Closed);
		|                TfEventArgs1.view = OneScriptTerminalGui.RevertEqualsObj(obj).dll_obj;
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.Closed);
		|            }
		|        }
		|
		|        private void M_Toplevel_Closing(Terminal.Gui.ToplevelClosingEventArgs obj)
		|        {
		|            if (dll_obj.Closing != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.Closing);
		|                TfEventArgs1.cancel = ValueFactory.Create(obj.Cancel);
		|                TfEventArgs1.view = OneScriptTerminalGui.RevertEqualsObj(obj.RequestingTop).dll_obj;
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.Closing);
		|                obj.Cancel = TfEventArgs1.Cancel;
		|            }
		|        }
		|
		|        private void M_Toplevel_Deactivate(Terminal.Gui.Toplevel obj)
		|        {
		|            if (dll_obj.Deactivate != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.Deactivate);
		|                TfEventArgs1.view = OneScriptTerminalGui.RevertEqualsObj(obj).dll_obj;
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.Deactivate);
		|            }
		|        }
		|
		|        private void M_Toplevel_Loaded()
		|        {
		|            if (dll_obj.Loaded != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.Loaded);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.Loaded);
		|            }
		|        }
		|
		|        private void M_Toplevel_QuitKeyChanged(Terminal.Gui.Key obj)
		|        {
		|            if (dll_obj.QuitKeyChanged != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.QuitKeyChanged);
		|                TfEventArgs1.keyValue = ValueFactory.Create((int)obj);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.QuitKeyChanged);
		|            }
		|        }
		|
		|        private void M_Toplevel_Ready()
		|        {
		|            if (dll_obj.Ready != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.Ready);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.Ready);
		|            }
		|        }
		|
		|        private void M_Toplevel_Resized(Terminal.Gui.Size obj)
		|        {
		|            if (OneScriptTerminalGui.instance.Resized != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(OneScriptTerminalGui.instance.Resized);
		|                TfEventArgs1.size = new TfSize(obj.Width, obj.Height);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(OneScriptTerminalGui.instance.Resized);
		|            }
		|        }
		|
		|        private void M_Toplevel_Unloaded()
		|        {
		|            if (dll_obj.Unloaded != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.Unloaded);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.Unloaded);
		|            }
		|        }
		|
		|        public bool Modal
		|        {
		|            get { return M_Toplevel.Modal; }
		|            set { M_Toplevel.Modal = value; }
		|        }
		|
		|        public ostgui.MenuBar MenuBar
		|        {
		|            get { return OneScriptTerminalGui.RevertEqualsObj(M_Toplevel.MenuBar); }
		|            set { M_Toplevel.Add(value.M_MenuBar); }
		|        }
		|
		|        public StatusBar StatusBar
		|        {
		|            get { return OneScriptTerminalGui.RevertEqualsObj((M_Toplevel.StatusBar)); }
		|            set { M_Toplevel.StatusBar = value.M_StatusBar; }
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_Toplevel.ToString();
		|        }
		|
		|        public new Toplevel GetTopSuperView()
		|        {
		|            return OneScriptTerminalGui.RevertEqualsObj(M_Toplevel.GetTopSuperView());
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "TextFormatter" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class TextFormatter
		|    {
		|        public TfTextFormatter dll_obj;
		|        public Terminal.Gui.TextFormatter M_TextFormatter;
		|
		|        public TextFormatter()
		|        {
		|            M_TextFormatter = new Terminal.Gui.TextFormatter();
		|            OneScriptTerminalGui.AddToHashtable(M_TextFormatter, this);
		|        }
		|
		|        public bool AutoSize
		|        {
		|            get { return M_TextFormatter.AutoSize; }
		|            set { M_TextFormatter.AutoSize = value; }
		|        }
		|
		|        public int VerticalAlignment
		|        {
		|            get { return (int)M_TextFormatter.VerticalAlignment; }
		|            set { M_TextFormatter.VerticalAlignment = (Terminal.Gui.VerticalTextAlignment)value; }
		|        }
		|
		|        public int Alignment
		|        {
		|            get { return (int)M_TextFormatter.Alignment; }
		|            set { M_TextFormatter.Alignment = (Terminal.Gui.TextAlignment)value; }
		|        }
		|
		|        public int HotKey
		|        {
		|            get { return (int)M_TextFormatter.HotKey; }
		|        }
		|
		|        public ArrayImpl Lines
		|        {
		|            get
		|            {
		|                ArrayImpl ArrayImpl1 = new ArrayImpl();
		|                List<NStack.ustring> ustring1 = M_TextFormatter.Lines;
		|                for (int i = 0; i < ustring1.Count; i++)
		|                {
		|                    ArrayImpl1.Add(ValueFactory.Create(ustring1[i].ToString()));
		|                }
		|                return ArrayImpl1;
		|            }
		|        }
		|
		|        public int Direction
		|        {
		|            get { return (int)M_TextFormatter.Direction; }
		|            set { M_TextFormatter.Direction = (Terminal.Gui.TextDirection)value; }
		|        }
		|
		|        public int HotKeyPos
		|        {
		|            get { return M_TextFormatter.HotKeyPos; }
		|            set { M_TextFormatter.HotKeyPos = value; }
		|        }
		|
		|        public int CursorPosition
		|        {
		|            get { return M_TextFormatter.CursorPosition; }
		|            set { M_TextFormatter.CursorPosition = value; }
		|        }
		|
		|        public Size Size
		|        {
		|            get { return new Size(M_TextFormatter.Size); }
		|            set { M_TextFormatter.Size = value.M_Size; }
		|        }
		|
		|        public Rune HotKeySpecifier
		|        {
		|            get { return M_TextFormatter.HotKeySpecifier; }
		|            set { M_TextFormatter.HotKeySpecifier = value; }
		|        }
		|
		|        public bool PreserveTrailingSpaces
		|        {
		|            get { return M_TextFormatter.PreserveTrailingSpaces; }
		|            set { M_TextFormatter.PreserveTrailingSpaces = value; }
		|        }
		|
		|        public string Text
		|        {
		|            get { return M_TextFormatter.Text.ToString(); }
		|            set { M_TextFormatter.Text = value; }
		|        }
		|
		|        public bool NeedsFormat
		|        {
		|            get { return M_TextFormatter.NeedsFormat; }
		|            set { M_TextFormatter.NeedsFormat = value; }
		|        }
		|
		|        public string Justify(string p1, int p2, string p3 = "" "", Terminal.Gui.TextDirection p4 = Terminal.Gui.TextDirection.LeftRight_TopBottom)
		|        {
		|            return Terminal.Gui.TextFormatter.Justify(p1, p2, Convert.ToChar(p3), p4).ToString();
		|        }
		|
		|        public Rect CalcRect(int p1, int p2, string p3, Terminal.Gui.TextDirection p4 = Terminal.Gui.TextDirection.LeftRight_TopBottom)
		|        {
		|            return new Rect(Terminal.Gui.TextFormatter.CalcRect(p1, p2, p3, p4));
		|        }
		|
		|        public string ReplaceHotKeyWithTag(string p1, int p2)
		|        {
		|            return M_TextFormatter.ReplaceHotKeyWithTag(p1, p2).ToString();
		|        }
		|
		|        public int MaxWidth(string p1, int p2)
		|        {
		|            return Terminal.Gui.TextFormatter.MaxWidth(p1, p2);
		|        }
		|
		|        public int GetMaxColsForWidth(string p1, int p2)
		|        {
		|            List<NStack.ustring> ustring1 = new List<NStack.ustring>();
		|            string[] result = p1.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
		|            for (int i = 0; i < result.Length; i++)
		|            {
		|                ustring1.Add(result[i]);
		|            }
		|            return Terminal.Gui.TextFormatter.GetMaxColsForWidth(ustring1, p2);
		|        }
		|
		|        public int GetSumMaxCharWidth(string p1, int p2 = -1, int p3 = -1)
		|        {
		|            List<NStack.ustring> ustring1 = new List<NStack.ustring>();
		|            string[] result = p1.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
		|            for (int i = 0; i < result.Length; i++)
		|            {
		|                ustring1.Add(result[i]);
		|            }
		|            return Terminal.Gui.TextFormatter.GetSumMaxCharWidth(ustring1, p2, p3);
		|        }
		|
		|        public int GetMaxLengthForWidth(string p1, int p2)
		|        {
		|            return Terminal.Gui.TextFormatter.GetMaxLengthForWidth(p1, p2);
		|        }
		|
		|        public int MaxLines(string p1, int p2)
		|        {
		|            return Terminal.Gui.TextFormatter.MaxLines(p1, p2);
		|        }
		|
		|        public bool FindHotKey(string p1, string p2, bool p3, out int p4, out Terminal.Gui.Key p5)
		|        {
		|            Rune Rune1 = p2.ToCharArray()[0];
		|            return Terminal.Gui.TextFormatter.FindHotKey(p1, Rune1, p3, out p4, out p5);
		|        }
		|
		|        public string ClipAndJustify(string p1, int p2, int p3, Terminal.Gui.TextDirection p4 = Terminal.Gui.TextDirection.LeftRight_TopBottom)
		|        {
		|            return Terminal.Gui.TextFormatter.ClipAndJustify(p1, p2, (Terminal.Gui.TextAlignment)p3, p4).ToString();
		|        }
		|
		|        public string ClipOrPad(string p1, int p2)
		|        {
		|            return Terminal.Gui.TextFormatter.ClipOrPad(p1, p2);
		|        }
		|
		|        public string WordWrap(string p1, int p2, bool p3 = false, int p4 = 0, Terminal.Gui.TextDirection p5 = Terminal.Gui.TextDirection.LeftRight_TopBottom)
		|        {
		|            string str = """";
		|            List<NStack.ustring> list1 = Terminal.Gui.TextFormatter.WordWrap(p1, p2, p3, p4, (Terminal.Gui.TextDirection)p5);
		|            for (int i = 0; i < list1.Count; i++)
		|            {
		|                if (i == 0)
		|                {
		|                    str = list1[i].ToString() + Environment.NewLine;
		|                }
		|                else if (i == (list1.Count - 1))
		|                {
		|                    str += list1[i].ToString();
		|                }
		|                else
		|                {
		|                    str += list1[i].ToString() + Environment.NewLine;
		|                }
		|            }
		|            return str;
		|        }
		|
		|        public string SplitNewLine(string p1)
		|        {
		|            List<NStack.ustring> ustring1 = Terminal.Gui.TextFormatter.SplitNewLine(p1);
		|            string str = """";
		|            for (int i = 0; i < ustring1.Count; i++)
		|            {
		|                str += ustring1[i].ToString() + Environment.NewLine;
		|            }
		|            return str;
		|        }
		|
		|        public void Draw(Rect p1, Attribute p2, Attribute p3, Rect p4 = default, bool p5 = true)
		|        {
		|            M_TextFormatter.Draw(p1.M_Rect, p2.M_Attribute, p3.M_Attribute, p4.M_Rect, p5);
		|        }
		|
		|        public int MaxWidthLine(string p1)
		|        {
		|            return Terminal.Gui.TextFormatter.MaxWidthLine(p1);
		|        }
		|
		|        public string RemoveHotKeySpecifier(string p1, int p2, string p3)
		|        {
		|            return Terminal.Gui.TextFormatter.RemoveHotKeySpecifier(p1, p2, p3.ToCharArray()[0]).ToString();
		|        }
		|
		|        public string Format(string p1, int p2, int p3, bool p4, bool p5 = false, int p6 = 0, int p7 = 0)
		|        {
		|            List<NStack.ustring> ustring1 = Terminal.Gui.TextFormatter.Format(p1, p2, (Terminal.Gui.TextAlignment)p3, p4, p5, p6, (Terminal.Gui.TextDirection)p7);
		|            string str = """";
		|            for (int i = 0; i < ustring1.Count; i++)
		|            {
		|                str += ustring1[i].ToString() + Environment.NewLine;
		|            }
		|            return str;
		|        }
		|
		|        public int GetTextWidth(string p1)
		|        {
		|            return Terminal.Gui.TextFormatter.GetTextWidth(p1.ToString());
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_TextFormatter.ToString();
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "Size" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class Size
		|    {
		|        public TfSize dll_obj;
		|        public Terminal.Gui.Size M_Size;
		|
		|        public Size()
		|        {
		|            M_Size = new Terminal.Gui.Size();
		|        }
		|
		|        public Size(Terminal.Gui.Point p1)
		|        {
		|            M_Size = new Terminal.Gui.Size(p1);
		|        }
		|
		|        public Size(Terminal.Gui.Size p1)
		|        {
		|            M_Size = p1;
		|        }
		|
		|        public Size(int width, int height)
		|        {
		|            M_Size = new Terminal.Gui.Size(width, height);
		|        }
		|
		|        public int Width
		|        {
		|            get { return M_Size.Width; }
		|            set { M_Size.Width = value; }
		|        }
		|
		|        public int Height
		|        {
		|            get { return M_Size.Height; }
		|            set { M_Size.Height = value; }
		|        }
		|
		|        public Size Subtract(Size p1, Size p2)
		|        {
		|            return new Size(Terminal.Gui.Size.Subtract(p1.M_Size, p2.M_Size));
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_Size.ToString();
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "Rect" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class Rect
		|    {
		|        public TfRect dll_obj;
		|        public Terminal.Gui.Rect M_Rect;
		|
		|        public Rect()
		|        {
		|            M_Rect = new Terminal.Gui.Rect();
		|        }
		|
		|        public Rect(Terminal.Gui.Point p1, Terminal.Gui.Size p2)
		|        {
		|            M_Rect = new Terminal.Gui.Rect(p1, p2);
		|        }
		|
		|        public Rect(Terminal.Gui.Rect p1)
		|        {
		|            M_Rect = p1;
		|        }
		|
		|        public Rect(int x, int y, int width, int height)
		|        {
		|            M_Rect = new Terminal.Gui.Rect(x, y, width, height);
		|        }
		|
		|        public int X
		|        {
		|            get { return M_Rect.X; }
		|            set { M_Rect.X = value; }
		|        }
		|
		|        public int Y
		|        {
		|            get { return M_Rect.Y; }
		|            set { M_Rect.Y = value; }
		|        }
		|
		|        public int Width
		|        {
		|            get { return M_Rect.Width; }
		|            set { M_Rect.Width = value; }
		|        }
		|
		|        public int Height
		|        {
		|            get { return M_Rect.Height; }
		|            set { M_Rect.Height = value; }
		|        }
		|
		|        public int Left
		|        {
		|            get { return M_Rect.Left; }
		|        }
		|
		|        public int Right
		|        {
		|            get { return M_Rect.Right; }
		|        }
		|
		|        public int Top
		|        {
		|            get { return M_Rect.Top; }
		|        }
		|
		|        public int Bottom
		|        {
		|            get { return M_Rect.Bottom; }
		|        }
		|
		|        public bool Contains(Terminal.Gui.Rect p1)
		|        {
		|            return M_Rect.Contains(p1);
		|        }
		|
		|        public bool Contains(Terminal.Gui.Point p1)
		|        {
		|            return M_Rect.Contains(p1);
		|        }
		|
		|        public bool Contains(int p1, int p2)
		|        {
		|            return M_Rect.Contains(p1, p2);
		|        }
		|
		|        public void Offset(int p1, int p2)
		|        {
		|            M_Rect.Offset(p1, p2);
		|        }
		|
		|        public void Inflate(Terminal.Gui.Size p1)
		|        {
		|            M_Rect.Inflate(p1);
		|        }
		|
		|        public ostgui.Rect Inflate(Rect p1, int p2, int p3)
		|        {
		|            return new Rect(Terminal.Gui.Rect.Inflate(p1.M_Rect, p2, p3));
		|        }
		|
		|        public void Inflate(int p1, int p2)
		|        {
		|            M_Rect.Inflate(p1, p2);
		|        }
		|
		|        public Rect FromLTRB(int p1, int p2, int p3, int p4)
		|        {
		|            return new Rect(Terminal.Gui.Rect.FromLTRB(p1, p2, p3, p4));
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_Rect.ToString();
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "Point" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class Point
		|    {
		|        public TfPoint dll_obj;
		|        public Terminal.Gui.Point M_Point;
		|
		|        public Point()
		|        {
		|            M_Point = new Terminal.Gui.Point();
		|        }
		|
		|        public Point(Terminal.Gui.Size p1)
		|        {
		|            M_Point = new Terminal.Gui.Point(p1);
		|        }
		|
		|        public Point(int x, int y)
		|        {
		|            M_Point = new Terminal.Gui.Point(x, y);
		|        }
		|
		|        public Point(Terminal.Gui.Point p1)
		|        {
		|            M_Point = p1;
		|        }
		|
		|        public int X
		|        {
		|            get { return M_Point.X; }
		|            set { M_Point.X = value; }
		|        }
		|
		|        public int Y
		|        {
		|            get { return M_Point.Y; }
		|            set { M_Point.Y = value; }
		|        }
		|
		|        public void Offset(int p1, int p2)
		|        {
		|            M_Point.Offset(p1, p2);
		|        }
		|
		|        public ostgui.Point Subtract(Point p1, Size p2)
		|        {
		|            return new Point(Terminal.Gui.Point.Subtract(p1.M_Point, p2.M_Size));
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_Point.ToString();
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "ColorScheme" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class ColorScheme
		|    {
		|        public TfColorScheme dll_obj;
		|        public Terminal.Gui.ColorScheme M_ColorScheme;
		|
		|        public ColorScheme()
		|        {
		|            M_ColorScheme = new Terminal.Gui.ColorScheme();
		|            OneScriptTerminalGui.AddToHashtable(M_ColorScheme, this);
		|        }
		|
		|        public ColorScheme(Terminal.Gui.ColorScheme p1)
		|        {
		|            M_ColorScheme = p1;
		|            OneScriptTerminalGui.AddToHashtable(M_ColorScheme, this);
		|        }
		|
		|        public Attribute HotNormal
		|        {
		|            get { return OneScriptTerminalGui.RevertEqualsObj(M_ColorScheme.HotNormal); }
		|            set { M_ColorScheme.HotNormal = value.M_Attribute; }
		|        }
		|
		|        public Attribute HotFocus
		|        {
		|            get { return OneScriptTerminalGui.RevertEqualsObj(M_ColorScheme.HotFocus); }
		|            set { M_ColorScheme.HotFocus = value.M_Attribute; }
		|        }
		|
		|        public Attribute Normal
		|        {
		|            get { return OneScriptTerminalGui.RevertEqualsObj(M_ColorScheme.Normal); }
		|            set { M_ColorScheme.Normal = value.M_Attribute; }
		|        }
		|
		|        public Attribute Disabled
		|        {
		|            get { return OneScriptTerminalGui.RevertEqualsObj(M_ColorScheme.Disabled); }
		|            set { M_ColorScheme.Disabled = value.M_Attribute; }
		|        }
		|
		|        public Attribute Focus
		|        {
		|            get { return OneScriptTerminalGui.RevertEqualsObj(M_ColorScheme.Focus); }
		|            set { M_ColorScheme.Focus = value.M_Attribute; }
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_ColorScheme.ToString();
		|        }
		|    }
		|";
	ИначеЕсли ИмяФайлаТФ = "Button" Тогда
		Стр = Стр + 
		"namespace ostgui
		|{
		|    public class Button : View
		|    {
		|        public new TfButton dll_obj;
		|        public Terminal.Gui.Button M_Button;
		|
		|        public Button()
		|        {
		|            M_Button = new Terminal.Gui.Button();
		|            base.M_View = M_Button;
		|            OneScriptTerminalGui.AddToHashtable(M_Button, this);
		|            SetActions(M_Button);
		|        }
		|
		|        public Button(string p1, bool p2 = false)
		|        {
		|            M_Button = new Terminal.Gui.Button(p1, p2);
		|            base.M_View = M_Button;
		|            OneScriptTerminalGui.AddToHashtable(M_Button, this);
		|            SetActions(M_Button);
		|        }
		|
		|        public Button(int p1, int p2, string p3)
		|        {
		|            M_Button = new Terminal.Gui.Button(p1, p2, p3);
		|            base.M_View = M_Button;
		|            OneScriptTerminalGui.AddToHashtable(M_Button, this);
		|            SetActions(M_Button);
		|        }
		|
		|        public Button(int p1, int p2, string p3, bool p4)
		|        {
		|            M_Button = new Terminal.Gui.Button(p1, p2, p3, p4);
		|            base.M_View = M_Button;
		|            OneScriptTerminalGui.AddToHashtable(M_Button, this);
		|            SetActions(M_Button);
		|        }
		|
		|        private void SetActions(Terminal.Gui.Button button)
		|        {
		|            button.Clicked += Button_Clicked;
		|        }
		|
		|        private void Button_Clicked()
		|        {
		|            if (dll_obj.Clicked != null)
		|            {
		|                TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                TfEventArgs1.sender = dll_obj;
		|                TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(dll_obj.Clicked);
		|                OneScriptTerminalGui.Event = TfEventArgs1;
		|                OneScriptTerminalGui.ExecuteEvent(dll_obj.Clicked);
		|            }
		|        }
		|
		|        public bool IsDefault
		|        {
		|            get { return M_Button.IsDefault; }
		|            set { M_Button.IsDefault = value; }
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_Button.ToString();
		|        }
		|
		|        public new Toplevel GetTopSuperView()
		|        {
		|            return OneScriptTerminalGui.RevertEqualsObj(M_Button.GetTopSuperView());
		|        }
		|    }
		|";
	КонецЕсли;
	Возврат Стр;
КонецФункции//КлассВторогоУровня

Процедура СоздатьФайлТФ(ИмяФайлаТФ)
	СписокНеизменныхКлассов.Добавить(ИмяФайлаТФ);
	СтрВыгрузки = "";
	Если Ложь Тогда
	// ИначеЕсли ИмяФайлаТФ = "" Тогда
		// СтрВыгрузки = СтрВыгрузки + 
		// "namespace ostgui
		// |{
		
		// |    }
		// |}
		// |";
		// ТекстДокХХХ = Новый ТекстовыйДокумент;
		// ТекстДокХХХ.УстановитьТекст(СтрВыгрузки);
		// ТекстДокХХХ.Записать(КаталогВыгрузки + "\" + ИмяФайлаТФ + ".cs");
		
		
		
		
		
	ИначеЕсли ИмяФайлаТФ = "Responder" Тогда
		СтрВыгрузки = СтрВыгрузки + 
		"namespace ostgui
		|{
		|    public class Responder : Terminal.Gui.Responder
		|    {
		|        public Terminal.Gui.Responder M_Responder;
		|
		|        public new bool CanFocus
		|        {
		|            get { return M_Responder.CanFocus; }
		|            set { M_Responder.CanFocus = value; }
		|        }
		|
		|        public new bool Enabled
		|        {
		|            get { return M_Responder.Enabled; }
		|            set { M_Responder.Enabled = value; }
		|        }
		|
		|        public new bool Visible
		|        {
		|            get { return M_Responder.Visible; }
		|            set { M_Responder.Visible = value; }
		|        }
		|
		|        public new bool HasFocus
		|        {
		|            get { return M_Responder.HasFocus; }
		|        }
		|
		|        public new void Dispose()
		|        {
		|            M_Responder.Dispose();
		|        }
		|
		|        public new string ToString()
		|        {
		|            return M_Responder.ToString();
		|        }
		|    }
		|}
		|";
		ТекстДокХХХ = Новый ТекстовыйДокумент;
		ТекстДокХХХ.УстановитьТекст(СтрВыгрузки);
		ТекстДокХХХ.Записать(КаталогВыгрузки + "\" + ИмяФайлаТФ + ".cs");
	ИначеЕсли ИмяФайлаТФ = "Action" Тогда
		СтрВыгрузки = СтрВыгрузки + 
		"using ScriptEngine.Machine.Contexts;
		|using ScriptEngine.Machine;
		|
		|namespace ostgui
		|{
		|    [ContextClass(""ТфДействие"", ""TfAction"")]
		|    public class TfAction : AutoContext<TfAction>
		|    {
		|        public TfAction(IRuntimeContextInstance script = null, string methodName = null, IValue param = null)
		|        {
		|            Script = script;
		|            MethodName = methodName;
		|            Parameter = param;
		|        }
		|
		|        [ContextProperty(""ИмяМетода"", ""MethodName"")]
		|        public string MethodName { get; set; }
		|
		|        [ContextProperty(""Параметр"", ""Parameter"")]
		|        public IValue Parameter { get; set; }
		|
		|        [ContextProperty(""Сценарий"", ""Script"")]
		|        public IRuntimeContextInstance Script { get; set; }
		|    }
		|}
		|";
		ТекстДокХХХ = Новый ТекстовыйДокумент;
		ТекстДокХХХ.УстановитьТекст(СтрВыгрузки);
		ТекстДокХХХ.Записать(КаталогВыгрузки + "\" + ИмяФайлаТФ + ".cs");
	ИначеЕсли ИмяФайлаТФ = "OneScriptTerminalGui" Тогда
		СтрВыгрузки = СтрВыгрузки + 
		"using System;
		|using System.IO;
		|using System.Collections;
		|using System.Text;
		|using ScriptEngine.Machine.Contexts;
		|using ScriptEngine.Machine;
		|using ScriptEngine.HostedScript.Library;
		|using Terminal.Gui;
		|using System.Reflection;
		|
		|namespace ostgui
		|{
		|    [ContextClass(""ТерминалФормыДляОдноСкрипта"", ""OneScriptTerminalGui"")]
		|    public class OneScriptTerminalGui : AutoContext<OneScriptTerminalGui>
		|    {
		|        public static TfToplevel top;
		|        public static System.Collections.Hashtable hashtable = new Hashtable();
		|        public static OneScriptTerminalGui instance;
		|        private static object syncRoot = new Object();
		|        public static TfEventArgs Event = null;
		|        public static bool handleEvents = true;
		|
		|        static byte[] StreamToBytes(Stream input)
		|        {
		|            var capacity = input.CanSeek ? (int)input.Length : 0;
		|            using (var output = new MemoryStream(capacity))
		|            {
		|                int readLength;
		|                var buffer = new byte[4096];
		|                do
		|                {
		|                    readLength = input.Read(buffer, 0, buffer.Length);
		|                    output.Write(buffer, 0, readLength);
		|                }
		|                while (readLength != 0);
		|                return output.ToArray();
		|            }
		|        }
		|
		|        public static OneScriptTerminalGui getInstance()
		|        {
		|            if (instance == null)
		|            {
		|                lock (syncRoot)
		|                {
		|                    if (instance == null)
		|                    {
		|                        instance = new OneScriptTerminalGui();
		|                    }
		|                }
		|            }
		|            return instance;
		|        }
		|
		|        [ScriptConstructor]
		|        public static IRuntimeContextInstance Constructor()
		|        {
		|            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
		|            {
		|                string resourcepath = ""ostgui."" + new AssemblyName(args.Name).Name + "".dll"";
		|                if (Assembly.GetExecutingAssembly().GetName().Name == ""OneScriptTerminalGui"" &&
		|                    resourcepath != ""ostgui.Terminal.Gui.dll"")
		|                {
		|                    var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcepath);
		|                    if (stream != null)
		|                    {
		|                        using (stream)
		|                        {
		|                            return Assembly.Load(StreamToBytes(stream));
		|                        }
		|                    }
		|                }
		|                return null;
		|            };
		|
		|            OnOpen = delegate ()
		|            {
		|                if (instance.NotifyNewRunState != null)
		|                {
		|                    TfEventArgs TfEventArgs1 = new TfEventArgs();
		|                    TfEventArgs1.sender = instance;
		|                    TfEventArgs1.parameter = OneScriptTerminalGui.GetEventParameter(instance.NotifyNewRunState);
		|                    OneScriptTerminalGui.Event = TfEventArgs1;
		|                    OneScriptTerminalGui.ExecuteEvent(instance.NotifyNewRunState);
		|                }
		|            };
		|
		|            OneScriptTerminalGui inst = getInstance();
		|            return inst;
		|        }
		|
		|        static Action OnOpen;
		|        private static void Application_NotifyNewRunState(Application.RunState obj)
		|        {
		|            OnOpen.Invoke();
		|        }
		|
		|        [ContextProperty(""РазмерИзменен"", ""Resized"")]
		|        public TfAction Resized { get; set; }
		|
		|        public static SystemGlobalContext GlobalContext()
		|        {
		|            return GlobalsManager.GetGlobalContext<SystemGlobalContext>();
		|        }
		|
		|        private static TfConsoleKey tf_ConsoleKey = new TfConsoleKey();
		|        [ContextProperty(""КлавишиКонсоли"", ""ConsoleKey"")]
		|        public TfConsoleKey ConsoleKey
		|        {
		|            get { return tf_ConsoleKey; }
		|        }
		|
		|        private static TfCommandTUI tf_CommandTUI = new TfCommandTUI();
		|        [ContextProperty(""КомандаTUI"", ""CommandTUI"")]
		|        public TfCommandTUI CommandTUI
		|        {
		|            get { return tf_CommandTUI; }
		|        }
		|
		|        [ContextMethod(""Эмодзи"", ""Emoji"")]
		|        public string Emoji(IValue p1)
		|        {
		|            var sb = new StringBuilder();
		|            if (p1.SystemType.Name == ""Число"")
		|            {
		|                try
		|                {
		|                    sb.Append(Char.ConvertFromUtf32(Convert.ToInt32(p1.AsNumber()))).ToString();
		|                }
		|                catch { }
		|            }
		|            else if (p1.SystemType.Name == ""Строка"")
		|            {
		|                string p2 = p1.AsString();
		|                p2 = p2.Replace(""0x"", """").Replace(""0х"", """").Replace(""\\u"", """");
		|                try
		|                {
		|                    try
		|                    {
		|                        int num = Convert.ToInt32(p2);
		|                        string str = Char.ConvertFromUtf32(num);
		|                        sb.Append(str).ToString();
		|                    }
		|                    catch
		|                    {
		|                        int num = Convert.ToInt32(p2, 16);
		|                        string str = Char.ConvertFromUtf32(num);
		|                        sb.Append(str).ToString();
		|                    }
		|                }
		|                catch { }
		|            }
		|            return sb.ToString();
		|        }
		|
		|        [ContextProperty(""Высота"", ""Rows"")]
		|        public int Rows
		|        {
		|            get { return Application.Driver.Rows; }
		|        }
		|
		|        [ContextProperty(""Ширина"", ""Cols"")]
		|        public int Cols
		|        {
		|            get { return Application.Driver.Cols; }
		|        }
		|
		|        [ContextMethod(""КлавишаВвод"", ""ButtonEnter"")]
		|        public void ButtonEnter()
		|        {
		|            Application.Driver.SendKeys(System.Char.MinValue, System.ConsoleKey.Enter, false, false, false);
		|        }
		|
		|        [ContextMethod(""СтрелкаВправо"", ""RightArrow"")]
		|        public void RightArrow()
		|        {
		|            string str = Application.Driver.RightArrow.ToString();
		|            System.Char char1 = Convert.ToChar(str.Substring(0, 1));
		|            Application.Driver.SendKeys(char1, System.ConsoleKey.RightArrow, false, false, false);
		|        }
		|
		|        [ContextMethod(""СтрелкаВлево"", ""LeftArrow"")]
		|        public void LeftArrow()
		|        {
		|            string str = Application.Driver.LeftArrow.ToString();
		|            System.Char char1 = Convert.ToChar(str.Substring(0, 1));
		|            Application.Driver.SendKeys(char1, System.ConsoleKey.LeftArrow, false, false, false);
		|        }
		|
		|        [ContextMethod(""СтрелкаВниз"", ""DownArrow"")]
		|        public void DownArrow()
		|        {
		|            string str = Application.Driver.DownArrow.ToString();
		|            System.Char char1 = Convert.ToChar(str.Substring(0, 1));
		|            Application.Driver.SendKeys(char1, System.ConsoleKey.DownArrow, false, false, false);
		|        }
		|
		|        [ContextMethod(""СтрелкаВверх"", ""UpArrow"")]
		|        public void UpArrow()
		|        {
		|            string str = Application.Driver.UpArrow.ToString();
		|            System.Char char1 = Convert.ToChar(str.Substring(0, 1));
		|            Application.Driver.SendKeys(char1, System.ConsoleKey.UpArrow, false, false, false);
		|        }
		|
		|        [ContextMethod(""ПраваяКвадратная"", ""RightBracket"")]
		|        public string RightBracket()
		|        {
		|            return Application.Driver.RightBracket.ToString();
		|        }
		|
		|        [ContextMethod(""ЛеваяКвадратная"", ""LeftBracket"")]
		|        public string LeftBracket()
		|        {
		|            return Application.Driver.LeftBracket.ToString();
		|        }
		|
		|        [ContextMethod(""МалыйБлок"", ""BlocksMeterSegment"")]
		|        public string BlocksMeterSegment()
		|        {
		|            return Application.Driver.BlocksMeterSegment.ToString();
		|        }
		|
		|        [ContextMethod(""БольшойБлок"", ""ContinuousMeterSegment"")]
		|        public string ContinuousMeterSegment()
		|        {
		|            return Application.Driver.ContinuousMeterSegment.ToString();
		|        }
		|
		|        [ContextMethod(""ЛевыйИндикатор"", ""LeftDefaultIndicator"")]
		|        public string LeftDefaultIndicator()
		|        {
		|            return Application.Driver.LeftDefaultIndicator.ToString();
		|        }
		|
		|        [ContextMethod(""ПравыйИндикатор"", ""RightDefaultIndicator"")]
		|        public string RightDefaultIndicator()
		|        {
		|            return Application.Driver.RightDefaultIndicator.ToString();
		|        }
		|
		|        [ContextMethod(""ВерхняяСтрелка"", ""ArrowUp"")]
		|        public string ArrowUp()
		|        {
		|            return Application.Driver.UpArrow.ToString();
		|        }
		|
		|        [ContextMethod(""ЛеваяСтрелка"", ""ArrowLeft"")]
		|        public string ArrowLeft()
		|        {
		|            return Application.Driver.LeftArrow.ToString();
		|        }
		|
		|       [ContextMethod(""НижняяСтрелка"", ""ArrowDown"")]
		|        public string ArrowDown()
		|        {
		|            return Application.Driver.DownArrow.ToString();
		|        }
		|
		|       [ContextMethod(""ПраваяСтрелка"", ""ArrowRight"")]
		|        public string ArrowRight()
		|        {
		|            return Application.Driver.RightArrow.ToString();
		|        }
		|
		|       [ContextMethod(""ВерхнийТройник"", ""TopTee"")]
		|        public string TopTee()
		|        {
		|            return Application.Driver.TopTee.ToString();
		|        }
		|
		|       [ContextMethod(""ЛевыйТройник"", ""LeftTee"")]
		|        public string LeftTee()
		|        {
		|            return Application.Driver.LeftTee.ToString();
		|        }
		|
		|       [ContextMethod(""НижнийТройник"", ""BottomTee"")]
		|        public string BottomTee()
		|        {
		|            return Application.Driver.BottomTee.ToString();
		|        }
		|
		|       [ContextMethod(""ПравыйТройник"", ""RightTee"")]
		|        public string RightTee()
		|        {
		|            return Application.Driver.RightTee.ToString();
		|        }
		|
		|        [ContextMethod(""Пометка"", ""Checked"")]
		|        public string Checked()
		|        {
		|            return Application.Driver.Checked.ToString();
		|        }
		|
		|        [ContextMethod(""Алмаз"", ""Diamond"")]
		|        public string Diamond()
		|        {
		|            return Application.Driver.Diamond.ToString();
		|        }
		|
		|        [ContextMethod(""ДвойнаяГоризонтальная"", ""HDLine"")]
		|        public string HDLine()
		|        {
		|            return Application.Driver.HDLine.ToString();
		|        }
		|
		|        [ContextMethod(""Горизонтальная"", ""HLine"")]
		|        public string HLine()
		|        {
		|            return Application.Driver.HLine.ToString();
		|        }
		|
		|        [ContextMethod(""ГоризонтальнаяСЗакругленнымиУглами"", ""HRLine"")]
		|        public string HRLine()
		|        {
		|            return Application.Driver.HRLine.ToString();
		|        }
		|
		|        [ContextMethod(""НижнийЛевыйУгол"", ""LLCorner"")]
		|        public string LLCorner()
		|        {
		|            return Application.Driver.LLCorner.ToString();
		|        }
		|
		|        [ContextMethod(""НижнийЛевыйДвойнойУгол"", ""LLDCorner"")]
		|        public string LLDCorner()
		|        {
		|            return Application.Driver.LLDCorner.ToString();
		|        }
		|
		|        [ContextMethod(""НижнийЛевыйЗакругленныйУгол"", ""LLRCorner"")]
		|        public string LLRCorner()
		|        {
		|            return Application.Driver.LLRCorner.ToString();
		|        }
		|
		|        [ContextMethod(""НижнийПравыйУгол"", ""LRCorner"")]
		|        public string LRCorner()
		|        {
		|            return Application.Driver.LRCorner.ToString();
		|        }
		|
		|        [ContextMethod(""НижнийПравыйДвойнойУгол"", ""LRDCorner"")]
		|        public string LRDCorner()
		|        {
		|            return Application.Driver.LRDCorner.ToString();
		|        }
		|
		|        [ContextMethod(""НижнийПравыйЗакругленныйУгол"", ""LRRCorner"")]
		|        public string LRRCorner()
		|        {
		|            return Application.Driver.LRRCorner.ToString();
		|        }
		|
		|        [ContextMethod(""Выделенный"", ""Selected"")]
		|        public string Selected()
		|        {
		|            return Application.Driver.Selected.ToString();
		|        }
		|
		|        [ContextMethod(""Точечный"", ""Stipple"")]
		|        public string Stipple()
		|        {
		|            return Application.Driver.Stipple.ToString();
		|        }
		|
		|        [ContextMethod(""ВерхнийЛевыйУгол"", ""ULCorner"")]
		|        public string ULCorner()
		|        {
		|            return Application.Driver.ULCorner.ToString();
		|        }
		|
		|        [ContextMethod(""ВерхнийЛевыйДвойнойУгол"", ""ULDCorner"")]
		|        public string ULDCorner()
		|        {
		|            return Application.Driver.ULDCorner.ToString();
		|        }
		|
		|        [ContextMethod(""ВерхнийЛевыйЗакругленныйУгол"", ""ULRCorner"")]
		|        public string ULRCorner()
		|        {
		|            return Application.Driver.ULRCorner.ToString();
		|        }
		|
		|        [ContextMethod(""БезПометки"", ""UnChecked"")]
		|        public string UnChecked()
		|        {
		|            return Application.Driver.UnChecked.ToString();
		|        }
		|
		|        [ContextMethod(""БезВыделения"", ""UnSelected"")]
		|        public string UnSelected()
		|        {
		|            return Application.Driver.UnSelected.ToString();
		|        }
		|
		|        [ContextMethod(""ВерхнийПравыйУгол"", ""URCorner"")]
		|        public string URCorner()
		|        {
		|            return Application.Driver.URCorner.ToString();
		|        }
		|
		|        [ContextMethod(""ВерхнийПравыйДвойнойУгол"", ""URDCorner"")]
		|        public string URDCorner()
		|        {
		|            return Application.Driver.URDCorner.ToString();
		|        }
		|
		|        [ContextMethod(""ВерхнийПравыйЗакругленныйУгол"", ""URRCorner"")]
		|        public string URRCorner()
		|        {
		|            return Application.Driver.URRCorner.ToString();
		|        }
		|
		|        [ContextMethod(""ВертикальнаяДвойная"", ""VDLine"")]
		|        public string VDLine()
		|        {
		|            return Application.Driver.VDLine.ToString();
		|        }
		|
		|        [ContextMethod(""Вертикальная"", ""VLine"")]
		|        public string VLine()
		|        {
		|            return Application.Driver.VLine.ToString();
		|        }
		|
		|        [ContextMethod(""ВертикальнаяСЗакругленнымиУглами"", ""VRLine"")]
		|        public string VRLine()
		|        {
		|            return Application.Driver.VRLine.ToString();
		|        }
		|
		|        [ContextMethod(""Таймер"", ""Timer"")]
		|        public TfTimer Timer()
		|        {
		|            return new TfTimer();
		|        }
		|
		|        [ContextMethod(""ОкноСообщений"", ""MessageBox"")]
		|        public TfMessageBox MessageBox()
		|        {
		|            return new TfMessageBox();
		|        }
		|
		|        [ContextMethod(""ОтправитьКлавиши"", ""SendKeys"")]
		|        public void SendKeys(string p1, bool p3, bool p4, bool p5)
		|        {
		|            System.Char char1 = Convert.ToChar(p1.Substring(0, 1));
		|            Application.Driver.SendKeys(char1, (System.ConsoleKey)0, p3, p4, p5);
		|        }
		|
		|        [ContextProperty(""ТекстБуфераОбмена"", ""ClipboardText"")]
		|        public string ClipboardText
		|        {
		|            get { return Terminal.Gui.Clipboard.Contents.ToString(); }
		|            set { Terminal.Gui.Clipboard.Contents = value; }
		|        }
		|
		|        [ContextMethod(""СтрокаСостояния"", ""StatusBar"")]
		|        public TfStatusBar StatusBar()
		|        {
		|            return new TfStatusBar();
		|        }
		|
		|        [ContextMethod(""Выполнить"", ""Execute"")]
		|        public IValue Execute(TfAction p1)
		|        {
		|            TfEventArgs eventArgs = new TfEventArgs();
		|            eventArgs.sender = instance;
		|            eventArgs.parameter = OneScriptTerminalGui.GetEventParameter(p1);
		|            Event = eventArgs;
		|
		|            TfAction Action1 = p1;
		|            IRuntimeContextInstance script = Action1.Script;
		|            string method = Action1.MethodName;
		|            ReflectorContext reflector = new ReflectorContext();
		|            IValue res = ValueFactory.Create();
		|            try
		|            {
		|                res = reflector.CallMethod(script, method, null);
		|            }
		|            catch (Exception ex)
		|            {
		|                GlobalContext().Echo(""Ошибка2: "" + ex.Message);
		|            }
		|            return res;
		|        }
		|
		|        [ContextProperty(""ПриОткрытии"", ""NotifyNewRunState"")]
		|        public TfAction NotifyNewRunState { get; set; }
		|
		|        [ContextMethod(""ЭлементМеню"", ""MenuItem"")]
		|        public TfMenuItem MenuItem()
		|        {
		|            return new TfMenuItem();
		|        }
		|
		|        [ContextProperty(""Цвета"", ""Colors"")]
		|        public TfColors Colors
		|        {
		|            get { return new TfColors(); }
		|        }
		|
		|        [ContextMethod(""Толщина"", ""Thickness"")]
		|        public TfThickness Thickness(IValue p1, IValue p2 = null, IValue p3 = null, IValue p4 = null)
		|        {
		|            if (p2 != null && p3 != null && p4 != null)
		|            {
		|                return new TfThickness(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()), Convert.ToInt32(p3.AsNumber()), Convert.ToInt32(p4.AsNumber()));
		|            }
		|            return new TfThickness(Convert.ToInt32(p1.AsNumber()));
		|        }
		|
		|        [ContextMethod(""ЭлементСтрокиСостояния"", ""StatusItem"")]
		|        public TfStatusItem StatusItem(int p1, string p2)
		|        {
		|            return new TfStatusItem(p1, p2);
		|        }
		|
		|        [ContextMethod(""ПунктМеню"", ""MenuBarItem"")]
		|        public TfMenuBarItem MenuBarItem()
		|        {
		|            return new TfMenuBarItem();
		|        }
		|
		|        [ContextMethod(""ПанельМеню"", ""MenuBar"")]
		|        public TfMenuBar MenuBar()
		|        {
		|            return new TfMenuBar();
		|        }
		|
		|        [ContextMethod(""ДобавитьВесьТекст"", ""AppendAllText"")]
		|        public void AppendAllText(string p1, string p2)
		|        {
		|            File.AppendAllText(p1, p2, Encoding.UTF8);
		|        }
		|
		|        [ContextProperty(""Величина"", ""Dim"")]
		|        public TfDim Dim
		|        {
		|            get { return new TfDim(); }
		|        }
		|
		|        [ContextProperty(""Позиция"", ""Pos"")]
		|        public TfPos Pos
		|        {
		|            get { return new TfPos(); }
		|        }
		|
		|        [ContextMethod(""Обновить"", ""Refresh"")]
		|        public void Refresh()
		|        {
		|            Application.Refresh();
		|        }
		|
		|        [ContextMethod(""Завершить"", ""Shutdown"")]
		|        public void Shutdown()
		|        {
		|            //Application.Shutdown();
		|            Application.RequestStop(Top.Base_obj.M_Toplevel);
		|        }
		|
		|        [ContextMethod(""ОформительТекста"", ""TextFormatter"")]
		|        public TfTextFormatter TextFormatter()
		|        {
		|            return new TfTextFormatter();
		|        }
		|
		|        [ContextMethod(""ЦветоваяСхема"", ""ColorScheme"")]
		|        public TfColorScheme ColorScheme()
		|        {
		|            return new TfColorScheme();
		|        }
		|
		|        [ContextMethod(""Атрибут"", ""Attribute"")]
		|        public TfAttribute Attribute(IValue p1 = null, IValue p2 = null, IValue p3 = null)
		|        {
		|            if (p1 == null && p2 == null && p3 == null)
		|            {
		|                return new TfAttribute();
		|            }
		|            else if (p1 != null && p2 == null && p3 == null)
		|            {
		|                if (p1.SystemType.Name == ""Число"")
		|                {
		|                    return new TfAttribute(Convert.ToInt32(p1.AsNumber()));
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else if (p1 != null && p2 != null && p3 == null)
		|            {
		|                if (p1.SystemType.Name == ""Число"")
		|                {
		|                    return new TfAttribute(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()));
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else if (p1 != null && p2 != null && p3 != null)
		|            {
		|                if (p1.SystemType.Name == ""Число"")
		|                {
		|                    return new TfAttribute(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()), Convert.ToInt32(p3.AsNumber()));
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else
		|            {
		|                return null;
		|            }
		|        }
		|
		|        [ContextMethod(""ЗапуститьИЗавершить"", ""RunAndShutdown"")]
		|        public void RunAndShutdown()
		|        {
		|            //Top.CorrectionZet(); // Конфликтует с созданием меню.
		|            Application.Begin(top.Base_obj.M_Toplevel);
		|        }
		|
		|        [ContextMethod(""Запуск"", ""Run"")]
		|        public void Run()
		|        {
		|            //Top.CorrectionZet(); // Конфликтует с созданием меню.
		|            Application.Run();
		|        }
		|
		|        [ContextProperty(""РазрешитьСобытия"", ""AllowEvents"")]
		|        public bool HandleEvents
		|        {
		|            get { return handleEvents; }
		|            set { handleEvents = value; }
		|        }
		|
		|        public static dynamic GetEventParameter(dynamic dll_objEvent)
		|        {
		|            if (dll_objEvent != null)
		|            {
		|                dynamic eventType = dll_objEvent.GetType();
		|                if (eventType == typeof(DelegateAction))
		|                {
		|                    return null;
		|                }
		|                else if (eventType == typeof(TfAction))
		|                {
		|                    return ((TfAction)dll_objEvent).Parameter;
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else
		|            {
		|                return null;
		|            }
		|        }
		|
		|        public static void ExecuteEvent(TfAction action)
		|        {
		|            if (!handleEvents)
		|            {
		|                return;
		|            }
		|            if (action == null)
		|            {
		|                return;
		|            }
		|            ReflectorContext reflector = new ReflectorContext();
		|            try
		|            {
		|                reflector.CallMethod(action.Script, action.MethodName, null);
		|            }
		|            catch (Exception ex)
		|            {
		|                GlobalContext().Echo(""Обработчик не выполнен: "" + action.MethodName + Environment.NewLine + ex.StackTrace);
		|            }
		|            Event = null;
		|            Application.Refresh();
		|        }
		|
		|        [ContextProperty(""Отправитель"", ""Sender"")]
		|        public IValue Sender
		|        {
		|            get { return RevertEqualsObj(Event.Sender); }
		|        }
		|
		|        [ContextProperty(""АргументыСобытия"", ""EventArgs"")]
		|        public TfEventArgs EventArgs
		|        {
		|            get { return Event; }
		|        }
		|
		|        private static TfVerticalTextAlignment tf_VerticalTextAlignment = new TfVerticalTextAlignment();
		|        [ContextProperty(""ВертикальноеВыравниваниеТекста"", ""VerticalTextAlignment"")]
		|        public TfVerticalTextAlignment VerticalTextAlignment
		|        {
		|            get { return tf_VerticalTextAlignment; }
		|        }
		|
		|        private static TfCursorVisibility tf_CursorVisibility = new TfCursorVisibility();
		|        [ContextProperty(""ВидКурсора"", ""CursorVisibility"")]
		|        public TfCursorVisibility CursorVisibility
		|        {
		|            get { return tf_CursorVisibility; }
		|        }
		|
		|        private static TfTextAlignment tf_TextAlignment = new TfTextAlignment();
		|        [ContextProperty(""ВыравниваниеТекста"", ""TextAlignment"")]
		|        public TfTextAlignment TextAlignment
		|        {
		|            get { return tf_TextAlignment; }
		|        }
		|
		|        private static TfKeys tf_Keys = new TfKeys();
		|        [ContextProperty(""Клавиши"", ""Keys"")]
		|        public TfKeys Keys
		|        {
		|            get { return tf_Keys; }
		|        }
		|
		|        private static TfTextDirection tf_TextDirection = new TfTextDirection();
		|        [ContextProperty(""НаправлениеТекста"", ""TextDirection"")]
		|        public TfTextDirection TextDirection
		|        {
		|            get { return tf_TextDirection; }
		|        }
		|
		|        private static TfLayoutStyle tf_LayoutStyle = new TfLayoutStyle();
		|        [ContextProperty(""СтильКомпоновки"", ""LayoutStyle"")]
		|        public TfLayoutStyle LayoutStyle
		|        {
		|            get { return tf_LayoutStyle; }
		|        }
		|
		|        private static TfMenuItemCheckStyle tf_MenuItemCheckStyle = new TfMenuItemCheckStyle();
		|        [ContextProperty(""СтильФлажкаЭлементаМеню"", ""MenuItemCheckStyle"")]
		|        public TfMenuItemCheckStyle MenuItemCheckStyle
		|        {
		|            get { return tf_MenuItemCheckStyle; }
		|        }
		|
		|        private static TfMouseFlags tf_MouseFlags = new TfMouseFlags();
		|        [ContextProperty(""ФлагиМыши"", ""MouseFlags"")]
		|        public TfMouseFlags MouseFlags
		|        {
		|            get { return tf_MouseFlags; }
		|        }
		|
		|        private static TfColor tf_Color = new TfColor();
		|        [ContextProperty(""Цвет"", ""Color"")]
		|        public TfColor Color
		|        {
		|            get { return tf_Color; }
		|        }
		|
		|        private static TfBorderStyle tf_BorderStyle = new TfBorderStyle();
		|        [ContextProperty(""СтильГраницы"", ""BorderStyle"")]
		|        public TfBorderStyle BorderStyle
		|        {
		|            get { return tf_BorderStyle; }
		|        }
		|
		|        [ContextMethod(""Граница"", ""Border"")]
		|        public TfBorder Border()
		|        {
		|            return new TfBorder();
		|        }
		|
		|        [ContextMethod(""Окно"", ""Window"")]
		|        public TfWindow Window(IValue p1 = null, IValue p2 = null, IValue p3 = null, IValue p4 = null)
		|        {
		|            if (p1 == null && p2 == null && p3 == null && p4 == null)
		|            {
		|                return new TfWindow();
		|            }
		|            else if (p1 != null && p2 == null && p3 == null && p4 == null)
		|            {
		|                if (p1.SystemType.Name == ""Строка"")
		|                {
		|                    return new TfWindow(p1.AsString());
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else if (p1 != null && p2 != null && p3 == null && p4 == null)
		|            {
		|                if (p1.GetType() == typeof(TfRect) && p2.SystemType.Name == ""Строка"")
		|                {
		|                    return new TfWindow((TfRect)p1, p2.AsString());
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else if (p1 != null && p2 != null && p3 != null && p4 == null)
		|            {
		|                if (p1.SystemType.Name == ""Строка"" && p2.SystemType.Name == ""Число"" && p3.GetType() == typeof(TfBorder))
		|                {
		|                    return new TfWindow(p1.AsString(), Convert.ToInt32(p2.AsNumber()), (TfBorder)p3);
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else if (p1 != null && p2 != null && p3 != null && p4 != null)
		|            {
		|                if (p1.GetType() == typeof(TfRect) && p2.SystemType.Name == ""Строка"" && p3.SystemType.Name == ""Число"" && p4.GetType() == typeof(TfBorder))
		|                {
		|                    return new TfWindow((TfRect)p1, p2.AsString(), Convert.ToInt32(p3.AsNumber()), (TfBorder)p4);
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else
		|            {
		|                return null;
		|            }
		|        }
		|
		|        [ContextMethod(""Действие"", ""Action"")]
		|        public TfAction Action(IRuntimeContextInstance script = null, string methodName = null, IValue param = null)
		|        {
		|            return new TfAction(script, methodName, param);
		|        }
		|
		|        [ContextMethod(""Активировать"", ""Init"")]
		|        public void Init()
		|        {
		|            Application.Init();
		|            try
		|            {
		|                Application.NotifyNewRunState += Application_NotifyNewRunState;
		|                top = new TfToplevel(Application.Top);
		|            }
		|            catch { }
		|        }
		|
		|        [ContextProperty(""Верхний"", ""Top"")]
		|        public TfToplevel Top
		|        {
		|            get { return top; }
		|        }
		|
		|        [ContextMethod(""Кнопка"", ""Button"")]
		|        public TfButton Button(IValue p1 = null, IValue p2 = null, IValue p3 = null, IValue p4 = null)
		|        {
		|            if (p1 == null && p2 == null && p3 == null && p4 == null)
		|            {
		|                return new TfButton();
		|            }
		|            else if (p1 != null && p2 != null && p3 != null && p4 != null)
		|            {
		|                if (p1.SystemType.Name == ""Число"" && p2.SystemType.Name == ""Число"" && p3.SystemType.Name == ""Строка"" && p4.SystemType.Name == ""Булево"")
		|                {
		|                    return new TfButton(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()), p3.AsString(), p4.AsBoolean());
		|                }
		|                else if (p1.SystemType.Name == ""Число"" && p2.SystemType.Name == ""Число"" && p3.SystemType.Name == ""Число"" && p4.SystemType.Name == ""Число"")
		|                {
		|                    TfButton TfButton1 = new TfButton();
		|                    TfButton1.Base_obj.M_Button.X = Terminal.Gui.Pos.At(Convert.ToInt32(p1.AsNumber()));
		|                    TfButton1.Base_obj.M_Button.Y = Terminal.Gui.Pos.At(Convert.ToInt32(p2.AsNumber()));
		|                    TfButton1.Base_obj.M_Button.Width = Terminal.Gui.Dim.Sized(Convert.ToInt32(p3.AsNumber()));
		|                    TfButton1.Base_obj.M_Button.Height = Terminal.Gui.Dim.Sized(Convert.ToInt32(p4.AsNumber()));
		|                    return TfButton1;
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else if (p1 != null && p2 != null && p3 != null && p4 == null)
		|            {
		|                if (p1.SystemType.Name == ""Число"" && p2.SystemType.Name == ""Число"" && p3.SystemType.Name == ""Строка"")
		|                {
		|                    return new TfButton(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()), p3.AsString());
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else if (p1 != null && p2 != null && p3 == null && p4 == null)
		|            {
		|                if (p1.SystemType.Name == ""Строка"" && p2.SystemType.Name == ""Булево"")
		|                {
		|                    return new TfButton(p1.AsString(), p2.AsBoolean());
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else if (p1 != null && p2 == null && p3 == null && p4 == null)
		|            {
		|                if (p1.SystemType.Name == ""Строка"")
		|                {
		|                    return new TfButton(p1.AsString());
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else
		|            {
		|                return null;
		|            }
		|        }
		|
		|        public TfView View(IValue p1 = null, IValue p2 = null, IValue p3 = null)
		|        {
		|            if (p1 == null && p2 == null && p3 == null)
		|            {
		|                return new TfView();
		|            }
		|            else if (p1 != null && p2 == null && p3 == null)
		|            {
		|                if (p1.GetType() == typeof(TfRect))
		|                {
		|                    return new TfView((TfRect)p1);
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else if (p1 != null && p2 != null && p3 != null)
		|            {
		|                if (p1.SystemType.Name == ""Число"" && p2.SystemType.Name == ""Число"" && p3.SystemType.Name == ""Строка"")
		|                {
		|                    return new TfView(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()), p3.AsString());
		|                }
		|                else if (p1.GetType() == typeof(TfRect) && p2.SystemType.Name == ""Строка"" && p3.GetType() == typeof(TfBorder))
		|                {
		|                    return new TfView((TfRect)p1, p2.AsString(), (TfBorder)p3);
		|                }
		|                else if (p1.SystemType.Name == ""Строка"" && p2.SystemType.Name == ""Число"" && p3.GetType() == typeof(TfBorder))
		|                {
		|                    return new TfView(p1.AsString(), Convert.ToInt32(p2.AsNumber()), (TfBorder)p3);
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else
		|            {
		|                return null;
		|            }
		|        }
		|
		|        [ContextMethod(""Размер"", ""Size"")]
		|        public TfSize Size(IValue p1 = null, IValue p2 = null)
		|        {
		|            if (p1 == null && p2 == null)
		|            {
		|                return new TfSize();
		|            }
		|            else if (p1 != null && p2 != null)
		|            {
		|                if (p1.SystemType.Name == ""Число"" && p2.SystemType.Name == ""Число"")
		|                {
		|                    return new TfSize(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()));
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else
		|            {
		|                return null;
		|            }
		|        }
		|
		|        [ContextMethod(""Прямоугольник"", ""Rect"")]
		|        public TfRect Rect(IValue p1 = null, IValue p2 = null, IValue p3 = null, IValue p4 = null)
		|        {
		|            if (p1 == null && p2 == null && p3 == null && p4 == null)
		|            {
		|                return new TfRect();
		|            }
		|            else if (p1 != null && p2 != null && p3 == null && p4 == null)
		|            {
		|                if (p1.GetType() == typeof(TfPoint) && p2.GetType() == typeof(TfSize))
		|                {
		|                    return new TfRect((TfPoint)p1, (TfSize)p2);
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else if (p1 != null && p2 != null && p3 != null && p4 != null)
		|            {
		|                if (p1.SystemType.Name == ""Число"" && p2.SystemType.Name == ""Число"" && p3.SystemType.Name == ""Число"" && p4.SystemType.Name == ""Число"")
		|                {
		|                    return new TfRect(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()), Convert.ToInt32(p3.AsNumber()), Convert.ToInt32(p4.AsNumber()));
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else
		|            {
		|                return null;
		|            }
		|        }
		|
		|        [ContextMethod(""Точка"", ""Point"")]
		|        public TfPoint Rect(IValue p1 = null, IValue p2 = null)
		|        {
		|            if (p1 == null && p2 == null)
		|            {
		|                return new TfPoint();
		|            }
		|            else if (p1 != null && p2 != null)
		|            {
		|                if (p1.SystemType.Name == ""Число"" && p2.SystemType.Name == ""Число"")
		|                {
		|                    return new TfPoint(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()));
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else if (p1 != null && p2 == null)
		|            {
		|                if (p1.GetType() == typeof(TfSize))
		|                {
		|                    return new TfPoint((TfSize)p1);
		|                }
		|                else
		|                {
		|                    return null;
		|                }
		|            }
		|            else
		|            {
		|                return null;
		|            }
		|        }
		|
		|        [ContextMethod(""Верхний"", ""Toplevel"")]
		|        public TfToplevel Toplevel(IValue p1 = null, IValue p2 = null, IValue p3 = null, IValue p4 = null)
		|        {
		|            if (p1 != null)
		|            {
		|                if (p1.GetType() == typeof(TfRect))
		|                {
		|                    return new TfToplevel((TfRect)p1);
		|                }
		|                else if (p1.SystemType.Name == ""Число"")
		|                {
		|                    TfRect TfRect1 = new TfRect(Convert.ToInt32(p1.AsNumber()), Convert.ToInt32(p2.AsNumber()), Convert.ToInt32(p3.AsNumber()), Convert.ToInt32(p4.AsNumber()));
		|                    return new TfToplevel(TfRect1);
		|                }
		|            }
		|            return new TfToplevel();
		|        }
		|
		|        public static System.Collections.ArrayList StrFindBetween(string p1, string p2 = null, string p3 = null, bool p4 = true, bool p5 = true)
		|        {
		|            //p1 - исходная строка
		|            //p2 - подстрока поиска от которой ведем поиск
		|            //p3 - подстрока поиска до которой ведем поиск
		|            //p4 - не включать p2 и p3 в результат
		|            //p5 - в результат не будут включены участки, содержащие другие найденные участки, удовлетворяющие переданным параметрам
		|            //функция возвращает массив строк
		|            string str1 = p1;
		|            int Position1;
		|            System.Collections.ArrayList ArrayList1 = new System.Collections.ArrayList();
		|            if (p2 != null && p3 == null)
		|            {
		|                Position1 = str1.IndexOf(p2);
		|                while (Position1 >= 0)
		|                {
		|                    ArrayList1.Add(ValueFactory.Create("""" + ((p4) ? str1.Substring(Position1 + p2.Length) : str1.Substring(Position1))));
		|                    str1 = str1.Substring(Position1 + 1);
		|                    Position1 = str1.IndexOf(p2);
		|                }
		|            }
		|            else if (p2 == null && p3 != null)
		|            {
		|                Position1 = str1.IndexOf(p3) + 1;
		|                int SumPosition1 = Position1;
		|                while (Position1 > 0)
		|                {
		|                    ArrayList1.Add(ValueFactory.Create("""" + ((p4) ? str1.Substring(0, SumPosition1 - 1) : str1.Substring(0, SumPosition1 - 1 + p3.Length))));
		|                    try
		|                    {
		|                        Position1 = str1.Substring(SumPosition1 + 1).IndexOf(p3) + 1;
		|                        SumPosition1 = SumPosition1 + Position1 + 1;
		|                    }
		|                    catch
		|                    {
		|                        break;
		|                    }
		|                }
		|            }
		|            else if (p2 != null && p3 != null)
		|            {
		|                Position1 = str1.IndexOf(p2);
		|                while (Position1 >= 0)
		|                {
		|                    string Стр2;
		|                    Стр2 = (p4) ? str1.Substring(Position1 + p2.Length) : str1.Substring(Position1);
		|                    int Position2 = Стр2.IndexOf(p3) + 1;
		|                    int SumPosition2 = Position2;
		|                    while (Position2 > 0)
		|                    {
		|                        if (p5)
		|                        {
		|                            if (Стр2.Substring(0, SumPosition2 - 1).IndexOf(p3) <= -1)
		|                            {
		|                                ArrayList1.Add(ValueFactory.Create("""" + ((p4) ? Стр2.Substring(0, SumPosition2 - 1) : Стр2.Substring(0, SumPosition2 - 1 + p3.Length))));
		|                            }
		|                        }
		|                        else
		|                        {
		|                            ArrayList1.Add(ValueFactory.Create("""" + ((p4) ? Стр2.Substring(0, SumPosition2 - 1) : Стр2.Substring(0, SumPosition2 - 1 + p3.Length))));
		|                        }
		|                        try
		|                        {
		|                            Position2 = Стр2.Substring(SumPosition2 + 1).IndexOf(p3) + 1;
		|                            SumPosition2 = SumPosition2 + Position2 + 1;
		|                        }
		|                        catch
		|                        {
		|                            break;
		|
		|                        }
		|                    }
		|                    str1 = str1.Substring(Position1 + 1);
		|                    Position1 = str1.IndexOf(p2);
		|                }
		|            }
		|            return ArrayList1;
		|        }
		|
		|        public static void AddToHashtable(dynamic p1, dynamic p2)
		|        {
		|            if (!hashtable.ContainsKey(p1))
		|            {
		|                hashtable.Add(p1, p2);
		|            }
		|            else
		|            {
		|                if (!((object)hashtable[p1]).Equals(p2))
		|                {
		|                    hashtable[p1] = p2;
		|                }
		|            }
		|        }
		|
		|        public static dynamic RevertEqualsObj(dynamic initialObject)
		|        {
		|            try
		|            {
		|                return hashtable[initialObject];
		|            }
		|            catch
		|            {
		|                return null;
		|            }
		|        }
		|
		|        public static IValue RevertObj(dynamic initialObject)
		|        {
		|            //ScriptEngine.Machine.Values.NullValue NullValue1;
		|            //ScriptEngine.Machine.Values.BooleanValue BooleanValue1;
		|            //ScriptEngine.Machine.Values.DateValue DateValue1;
		|            //ScriptEngine.Machine.Values.NumberValue NumberValue1;
		|            //ScriptEngine.Machine.Values.StringValue StringValue1;
		|
		|            //ScriptEngine.Machine.Values.GenericValue GenericValue1;
		|            //ScriptEngine.Machine.Values.TypeTypeValue TypeTypeValue1;
		|            //ScriptEngine.Machine.Values.UndefinedValue UndefinedValue1;
		|
		|            // Если initialObject равен null.
		|            try
		|            {
		|                if (initialObject == null)
		|                {
		|                    return (IValue)null;
		|                }
		|            }
		|            catch { }
		|            // Если initialObject равен null.
		|            try
		|            {
		|                string str_initialObject = initialObject.GetType().ToString();
		|            }
		|            catch
		|            {
		|                return (IValue)null;
		|            }
		|            // initialObject не равен null
		|            dynamic Obj1 = null;
		|            string str1 = initialObject.GetType().ToString();
		|            // Если initialObject второго уровня и у него есть ссылка на третий уровень.
		|            try
		|            {
		|                Obj1 = initialObject.dll_obj;
		|            }
		|            catch { }
		|            if (Obj1 != null)
		|            {
		|                return (IValue)Obj1;
		|            }
		|            // если initialObject не из пространства имен onescriptgui, то есть Уровень1 и у него есть аналог в
		|            // пространстве имен ostgui с конструктором принимающим параметром initialObject
		|            try
		|            {
		|                if (!str1.Contains(""ostgui.""))
		|                {
		|                    string str2 = ""ostgui.Tf"" + str1.Substring(str1.LastIndexOf(""."") + 1);
		|                    System.Type TestType = System.Type.GetType(str2, false, true);
		|                    object[] args = { initialObject };
		|                    Obj1 = Activator.CreateInstance(TestType, args);
		|                }
		|            }
		|            catch { }
		|            if (Obj1 != null)
		|            {
		|                return (IValue)Obj1;
		|            }
		|            // если initialObject из пространства имен onescriptgui, то есть Уровень2 и у него есть аналог в
		|            // пространстве имен ostgui с конструктором принимающим параметром initialObject
		|            try
		|            {
		|                if (str1.Contains(""ostgui.""))
		|                {
		|                    string str3 = str1.Replace(""ostgui."", ""ostgui.Tf"");
		|                    System.Type TestType = System.Type.GetType(str3, false, true);
		|                    object[] args = { initialObject };
		|                    Obj1 = Activator.CreateInstance(TestType, args);
		|                }
		|            }
		|            catch { }
		|            if (Obj1 != null)
		|            {
		|                return (IValue)Obj1;
		|            }
		|            // Если initialObject с возможными другими типами.
		|            string str4 = null;
		|            try
		|            {
		|                str4 = initialObject.SystemType.Name;
		|            }
		|            catch
		|            {
		|                if ((str1 == ""System.String"") ||
		|                (str1 == ""System.Decimal"") ||
		|                (str1 == ""System.Int32"") ||
		|                (str1 == ""System.Boolean"") ||
		|                (str1 == ""System.DateTime""))
		|                {
		|                    return (IValue)ValueFactory.Create(initialObject);
		|                }
		|                else if (str1 == ""System.Byte"")
		|                {
		|                    int vOut = Convert.ToInt32(initialObject);
		|                    return (IValue)ValueFactory.Create(vOut);
		|                }
		|                else if (str1 == ""System.DBNull"")
		|                {
		|                    string vOut = Convert.ToString(initialObject);
		|                    return (IValue)ValueFactory.Create(vOut);
		|                }
		|            }
		|            // Если тип initialObject определяется односкриптом.
		|            if (str4 == ""Неопределено"")
		|            {
		|                return (IValue)null;
		|            }
		|            if (str4 == ""Булево"")
		|            {
		|                return (IValue)initialObject;
		|            }
		|            if (str4 == ""Дата"")
		|            {
		|                return (IValue)initialObject;
		|            }
		|            if (str4 == ""Число"")
		|            {
		|                return (IValue)initialObject;
		|            }
		|            if (str4 == ""Строка"")
		|            {
		|                return (IValue)initialObject;
		|            }
		|            // Если ничего не подходит.
		|            return (IValue)initialObject;
		|        }
		|
		|        public static void WriteToFile(string str)
		|        {
		|            // добавление в файл
		|            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(""C:\\444\\Ошибки.txt"", true, Encoding.UTF8))
		|            {
		|                writer.WriteLineAsync(str);
		|            }
		|        }
		|    }
		|}
		|";
		ТекстДокХХХ = Новый ТекстовыйДокумент;
		ТекстДокХХХ.УстановитьТекст(СтрВыгрузки);
		ТекстДокХХХ.Записать(КаталогВыгрузки + "\" + ИмяФайлаТФ + ".cs");
	КонецЕсли;
КонецПроцедуры//СоздатьФайлТФ

КаталогСправки = "C:\444\OneScriptTerminalGui\docs\OSTGui";// без слэша в конце
КаталогВыгрузки = "C:\444\ВыгрузкаTUI";// без слэша в конце
СписокНеизменныхКлассов = Новый СписокЗначений();

ВыгрузкаTUI();
