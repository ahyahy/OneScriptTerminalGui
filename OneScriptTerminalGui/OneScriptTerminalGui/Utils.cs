using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptEngine.Machine;
using System.Reflection;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.HostedScript.Library;
using ScriptEngine.Machine.Values;
using NStack;

namespace ostgui
{
    public static class Utils
    {
        public static Hashtable hashtable = new Hashtable();
        public static Dictionary<decimal, ArrayList> shortcutDictionary = new Dictionary<decimal, ArrayList>();
        public static long lastEventTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
        public static bool noMouseEvent = false;
        public static int lastMeX = -1;
        public static int lastMeY = -1;
        public static int minCols;
        public static int minRows;
        public static IRuntimeContextInstance startupScript = GlobalContext().StartupScript();
        public static string pathStartupScript = startupScript.GetPropValue(startupScript.FindProperty("Path")).AsString();
        public static string pathLog = Path.Combine(Utils.pathStartupScript, "logtui.txt");
        public static string nameStartupScript = startupScript.GetPropValue(startupScript.FindProperty("Source")).AsString();

        //ScriptEngine.Machine.Values.NullValue NullValue1;
        //ScriptEngine.Machine.Values.BooleanValue BooleanValue1;
        //ScriptEngine.Machine.Values.DateValue DateValue1;
        //ScriptEngine.Machine.Values.NumberValue NumberValue1;
        //ScriptEngine.Machine.Values.StringValue StringValue1;

        //ScriptEngine.Machine.Values.GenericValue GenericValue1;
        //ScriptEngine.Machine.Values.TypeTypeValue TypeTypeValue1;
        //ScriptEngine.Machine.Values.UndefinedValue UndefinedValue1;
        public static bool IsString(IValue value) => value?.SystemType.Name == "Строка";
        public static bool IsNumber(IValue value) => value?.SystemType.Name == "Число";
        public static bool IsBoolean(IValue value) => value?.SystemType.Name == "Булево";
        public static bool IsDateTime(IValue value) => value?.SystemType.Name == "Дата";
        public static bool IsType<T>(IValue value) => value?.GetType() == typeof(T);
        public static bool IsType<T>(object value) => value?.GetType() == typeof(T);

        public static ustring[] ArrayToUstring(ArrayImpl array)
        {
            int count = array.Count();
            ustring[] result = new ustring[count];
            for (int i = 0; i < count; i++)
            {
                ustring element = array.Get(i).AsString();
                result[i] = element;
            }
            return result;
        }

        public static short[] ArrayToShort(ArrayImpl array)
        {
            int count = array.Count();
            short[] result = new short[count];
            for (int i = 0; i < count; i++)
            {
                // Получаем элемент массива
                IValue element = array.Get(i);

                // Преобразуем в число и затем в short
                decimal numberValue = element.AsNumber();
                result[i] = (short)numberValue;
            }
            return result;
        }

        public static ArrayImpl ShortToArray(short[] shortArray)
        {
            ArrayImpl array = new ArrayImpl();
            foreach (short value in shortArray)
            {
                // Добавляем short значение в массив OneScript
                array.Add(ValueFactory.Create((decimal)value));
            }
            return array;
        }

        public static string[] ArrayToString(ArrayImpl array)
        {
            int count = array.Count();
            string[] result = new string[count];
            for (int i = 0; i < count; i++)
            {
                string element = array.Get(i).AsString();
                result[i] = element;
            }
            return result;
        }

        public static ArrayImpl StringToArray(string[] stringArray)
        {
            ArrayImpl array = new ArrayImpl();
            foreach (string value in stringArray)
            {
                array.Add(ValueFactory.Create(value));
            }
            return array;
        }

        public static ArrayImpl TreeNodeToArray(IList<Terminal.Gui.Trees.ITreeNode> listTreeNode)
        {
            ArrayImpl array = new ArrayImpl();
            Terminal.Gui.Trees.ITreeNode[] treeNodeArray = listTreeNode.ToArray();
            foreach (var value in treeNodeArray)
            {
                array.Add(ValueFactory.Create((TfTreeNode)RevertEqualsObj(value).dll_obj));
            }
            return array;
        }

        public static ArrayImpl TreeViewObjectsToArray(IEnumerable<Terminal.Gui.Trees.ITreeNode> listTreeNode)
        {
            ArrayImpl array = new ArrayImpl();
            Terminal.Gui.Trees.ITreeNode[] treeNodeArray = listTreeNode.ToArray();
            foreach (var value in treeNodeArray)
            {
                array.Add(ValueFactory.Create((TfTreeNode)RevertEqualsObj(value).dll_obj));
            }
            return array;
        }

        public static Terminal.Gui.TabView.Tab[] IReadOnlyCollectionToArray(IReadOnlyCollection<Terminal.Gui.TabView.Tab> tabs)
        {
            Terminal.Gui.TabView.Tab[] array = new Terminal.Gui.TabView.Tab[tabs.Count];
            int num = 0;
            foreach (var item in tabs)
            {
                array[num] = item;
                num++;
            }
            return array;
        }

        public static Terminal.Gui.Button[] ArrayToButton(ArrayImpl array)
        {
            if (array == null)
            {
                return null;
            }
            int count = array.Count();
            Terminal.Gui.Button[] result = new Terminal.Gui.Button[count];
            for (int i = 0; i < count; i++)
            {
                IValue element = array.Get(i);
                result[i] = ((dynamic)element).Base_obj.M_Button;
            }
            return result;
        }

        public static Terminal.Gui.View[] ArrayToView(ArrayImpl array)
        {
            if (array == null)
            {
                return null;
            }
            int count = array.Count();
            Terminal.Gui.View[] result = new Terminal.Gui.View[count];
            for (int i = 0; i < count; i++)
            {
                IValue element = array.Get(i);
                result[i] = ((dynamic)element).Base_obj.M_View;
            }
            return result;
        }

        public static IList<Terminal.Gui.Trees.ITreeNode> ArrayToTreeNode(ArrayImpl array)
        {
            int count = array.Count();
            List<Terminal.Gui.Trees.ITreeNode> result = new List<Terminal.Gui.Trees.ITreeNode>();
            for (int i = 0; i < count; i++)
            {
                Terminal.Gui.Trees.ITreeNode element = (Terminal.Gui.Trees.ITreeNode)((TfTreeNode)array.Get(i)).Base_obj.M_TreeNode;
                result.Add(element);
            }
            return result;
        }

        public static ArrayImpl ListToArray(List<IValue> listValue)
        {
            ArrayImpl array = new ArrayImpl();
            IValue[] valueArray = listValue.ToArray();
            foreach (var value in valueArray)
            {
                array.Add(value);
            }
            return array;
        }

        public static ArrayImpl ListToArray(List<string> listString)
        {
            ArrayImpl array = new ArrayImpl();
            string[] stringArray = listString.ToArray();
            foreach (var value in stringArray)
            {
                array.Add(ValueFactory.Create(value));
            }
            return array;
        }

        public static List<string> ArrayToList(ArrayImpl array)
        {
            int count = array.Count();
            List<string> result = new List<string>();
            for (int i = 0; i < count; i++)
            {
                string element = array.Get(i).AsString();
                result.Add(element);
            }
            return result;
        }

        public static int[] ArrayToInt32(ArrayImpl array)
        {
            int count = array.Count();
            int[] result = new int[count];
            for (int i = 0; i < count; i++)
            {
                IValue element = array.Get(i);
                result[i] = ToInt32(element);
            }
            return result;
        }

        public static ArrayImpl IntToArray(int[] intArray)
        {
            ArrayImpl array = new ArrayImpl();
            foreach (int value in intArray)
            {
                array.Add(ValueFactory.Create(value));
            }
            return array;
        }

        public static ArrayImpl FloatToArray(float[] floatArray)
        {
            ArrayImpl array = new ArrayImpl();
            foreach (float value in floatArray)
            {
                array.Add(ValueFactory.Create(ToDecimal(value)));
            }
            return array;
        }

        public static float[] ArrayToFloat(ArrayImpl array)
        {
            int count = array.Count();
            float[] result = new float[count];
            for (int i = 0; i < count; i++)
            {
                IValue element = array.Get(i);
                result[i] = ToFloat(element);
            }
            return result;
        }

        public static byte[] ArrayToByte(ArrayImpl array)
        {
            int count = array.Count();
            byte[] result = new byte[count];
            for (int i = 0; i < count; i++)
            {
                IValue element = array.Get(i);
                decimal numberValue = element.AsNumber();
                result[i] = (byte)numberValue;
            }
            return result;
        }

        public static ArrayImpl ByteToArray(byte[] byteArray)
        {
            ArrayImpl array = new ArrayImpl();
            foreach (byte value in byteArray)
            {
                array.Add(ValueFactory.Create((decimal)value));
            }
            return array;
        }

        public static float ToFloat(object num)
        {
            if (IsType<NumberValue>(num))
            {
                return Convert.ToSingle(((NumberValue)num).AsNumber());
            }
            return Convert.ToSingle(num);
        }

        public static byte ToByte(object num)
        {
            if (IsType<NumberValue>(num))
            {
                return Convert.ToByte(((NumberValue)num).AsNumber());
            }
            return Convert.ToByte(num);
        }

        public static bool ToBoolean(object b)
        {
            if (IsType<BooleanValue>(b))
            {
                return Convert.ToBoolean(((BooleanValue)b).AsBoolean());
            }
            return Convert.ToBoolean(b);
        }

        public static string ToString(object str)
        {
            if (IsType<StringValue>(str))
            {
                return Convert.ToString(((StringValue)str).AsString());
            }
            return Convert.ToString(str);
        }

        public static int ToInt32(object num)
        {
            if (IsType<NumberValue>(num))
            {
                return Convert.ToInt32(((NumberValue)num).AsNumber());
            }
            return Convert.ToInt32(num);
        }

        public static decimal ToDecimal(object num)
        {
            if (IsType<NumberValue>(num))
            {
                return Convert.ToDecimal(((NumberValue)num).AsNumber());
            }
            return Convert.ToDecimal(num);
        }

        public static uint ToUInt32(object num)
        {
            if (IsType<NumberValue>(num))
            {
                return Convert.ToUInt32(((NumberValue)num).AsNumber());
            }
            return Convert.ToUInt32(num);
        }

        public static double ToDouble(object num)
        {
            if (IsType<NumberValue>(num))
            {
                return Convert.ToDouble(((NumberValue)num).AsNumber());
            }
            return Convert.ToDouble(num);
        }

        public static ArrayImpl NamesArray(Type _type)
        {
            ArrayImpl arrayImpl = new ArrayImpl();
            List<string> list = new List<string>();
            Type type = _type;
            PropertyInfo[] myPropertyInfo = type.GetProperties();
            for (int i = 0; i < myPropertyInfo.Length; i++)
            {
                if (myPropertyInfo[i].CustomAttributes.Count() == 1)
                {
                    string NameRu = myPropertyInfo[i].GetCustomAttribute<ContextPropertyAttribute>().GetName();
                    string NameEn = myPropertyInfo[i].GetCustomAttribute<ContextPropertyAttribute>().GetAlias();
                    try
                    {
                        list.Add(NameRu + " " + NameEn + " " + type.GetProperty(NameEn).GetValue(type));
                    }
                    catch { }
                }
            }
            list.Sort();
            for (int i = 0; i < list.Count; i++)
            {
                arrayImpl.Add(ValueFactory.Create(list[i]));
            }
            return arrayImpl;
        }

        public static SystemGlobalContext GlobalContext()
        {
            return GlobalsManager.GetGlobalContext<SystemGlobalContext>();
        }

        // Метод, заменяющий мне дебагер для анализа ошибок.
        public static void WriteToFile(string str)
        {
            using (StreamWriter writer = new StreamWriter(PathLog, true, Encoding.UTF8))
            {
                writer.WriteLineAsync("" + Environment.NewLine + DateTime.Now.ToString() + Environment.NewLine + str);
            }
        }

        public static string TempName
        {
            get { return $"{Guid.NewGuid():N}"; }
        }

        public static string NameStartupScript
        {
            get { return nameStartupScript; }
        }

        public static string PathLog
        {
            get { return pathLog; }
            set { pathLog = value; }
        }

        public static bool AllNull(params IValue[] values)
        {
            foreach (var value in values)
            {
                if (value != null) return false;
            }
            return true;
        }

        public static bool AllNotNull(params IValue[] values)
        {
            foreach (var value in values)
            {
                if (value == null) return false;
            }
            return true;
        }

        public static bool AllNotNull(params string[] values)
        {
            foreach (var value in values)
            {
                if (value == null) return false;
            }
            return true;
        }

        public static bool AllNotNull(params object[] values)
        {
            foreach (var value in values)
            {
                if (value == null) return false;
            }
            return true;
        }

        public static ArrayList StrFindBetween(string p1, string p2 = null, string p3 = null, bool p4 = true, bool p5 = true)
        {
            //p1 - исходная строка
            //p2 - подстрока поиска от которой ведем поиск
            //p3 - подстрока поиска до которой ведем поиск
            //p4 - не включать p2 и p3 в результат
            //p5 - в результат не будут включены участки, содержащие другие найденные участки, удовлетворяющие переданным параметрам
            //функция возвращает массив строк
            string str1 = p1;
            int Position1;
            ArrayList ArrayList1 = new ArrayList();
            if (p2 != null && p3 == null)
            {
                Position1 = str1.IndexOf(p2);
                while (Position1 >= 0)
                {
                    ArrayList1.Add(ValueFactory.Create("" + ((p4) ? str1.Substring(Position1 + p2.Length) : str1.Substring(Position1))));
                    str1 = str1.Substring(Position1 + 1);
                    Position1 = str1.IndexOf(p2);
                }
            }
            else if (p2 == null && p3 != null)
            {
                Position1 = str1.IndexOf(p3) + 1;
                int SumPosition1 = Position1;
                while (Position1 > 0)
                {
                    ArrayList1.Add(ValueFactory.Create("" + ((p4) ? str1.Substring(0, SumPosition1 - 1) : str1.Substring(0, SumPosition1 - 1 + p3.Length))));
                    try
                    {
                        Position1 = str1.Substring(SumPosition1 + 1).IndexOf(p3) + 1;
                        SumPosition1 = SumPosition1 + Position1 + 1;
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            else if (p2 != null && p3 != null)
            {
                Position1 = str1.IndexOf(p2);
                while (Position1 >= 0)
                {
                    string Стр2;
                    Стр2 = (p4) ? str1.Substring(Position1 + p2.Length) : str1.Substring(Position1);
                    int Position2 = Стр2.IndexOf(p3) + 1;
                    int SumPosition2 = Position2;
                    while (Position2 > 0)
                    {
                        if (p5)
                        {
                            if (Стр2.Substring(0, SumPosition2 - 1).IndexOf(p3) <= -1)
                            {
                                ArrayList1.Add(ValueFactory.Create("" + ((p4) ? Стр2.Substring(0, SumPosition2 - 1) : Стр2.Substring(0, SumPosition2 - 1 + p3.Length))));
                            }
                        }
                        else
                        {
                            ArrayList1.Add(ValueFactory.Create("" + ((p4) ? Стр2.Substring(0, SumPosition2 - 1) : Стр2.Substring(0, SumPosition2 - 1 + p3.Length))));
                        }
                        try
                        {
                            Position2 = Стр2.Substring(SumPosition2 + 1).IndexOf(p3) + 1;
                            SumPosition2 = SumPosition2 + Position2 + 1;
                        }
                        catch
                        {
                            break;
                        }
                    }
                    str1 = str1.Substring(Position1 + 1);
                    Position1 = str1.IndexOf(p2);
                }
            }
            return ArrayList1;
        }

        public static dynamic DefineTypeIValue(dynamic p1)
        {
            if (IsType<StringValue>(p1))
            {
                return p1.AsString();
            }
            else if (IsType<NumberValue>(p1))
            {
                return p1.AsNumber();
            }
            else if (IsType<BooleanValue>(p1))
            {
                return p1.AsBoolean();
            }
            else if (IsType<DateValue>(p1))
            {
                return p1.AsDate();
            }
            else
            {
                return p1;
            }
        }

        public static byte[] StreamToBytes(Stream input)
        {
            var capacity = input.CanSeek ? (int)input.Length : 0;
            using (var output = new MemoryStream(capacity))
            {
                int readLength;
                var buffer = new byte[4096];
                do
                {
                    readLength = input.Read(buffer, 0, buffer.Length);
                    output.Write(buffer, 0, readLength);
                }
                while (readLength != 0);
                return output.ToArray();
            }
        }

        public static void AddToShortcutDictionary(decimal p1, IValue p2)
        {
            if (!shortcutDictionary.ContainsKey(p1))
            {
                ArrayList ArrayList1 = new ArrayList();
                ArrayList1.Add(p2);
                shortcutDictionary.Add(p1, ArrayList1);
            }
            else
            {
                ArrayList ArrayList1 = shortcutDictionary[p1];
                if (!ArrayList1.Contains(p2))
                {
                    ArrayList1.Add(p2);
                }
            }
        }

        public static void RemoveFromShortcutDictionary(decimal p1, IValue p2)
        {
            if (shortcutDictionary.ContainsKey(p1))
            {
                try
                {
                    shortcutDictionary[p1].Remove(p2);
                }
                catch { }
            }
        }

        public static ArrayList GetFromShortcutDictionary(IValue p1)
        {
            ArrayList ArrayList1 = new ArrayList();
            foreach (var item in shortcutDictionary)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    if (item.Value[i] == p1)
                    {
                        ArrayList1.Add(item.Key);
                    }
                }
            }
            return ArrayList1;
        }

        public static dynamic RevertShortcut(dynamic shortcut)
        {
            try
            {
                return shortcutDictionary[shortcut];
            }
            catch
            {
                return null;
            }
        }

        public static void AddToHashtable(dynamic p1, dynamic p2)
        {
            if (!hashtable.ContainsKey(p1))
            {
                hashtable.Add(p1, p2);
            }
            else
            {
                if (!((object)hashtable[p1]).Equals(p2))
                {
                    hashtable[p1] = p2;
                }
            }
        }

        public static dynamic RevertEqualsObj(dynamic initialObject)
        {
            try
            {
                return hashtable[initialObject];
            }
            catch
            {
                return null;
            }
        }

        public static IValue RevertObj(dynamic initialObject)
        {
            // Если initialObject равен null.
            try
            {
                if (initialObject == null)
                {
                    return null;
                }
            }
            catch { }
            // Если initialObject равен null.
            try
            {
                string str_initialObject = initialObject.GetType().ToString();
            }
            catch
            {
                return null;
            }
            // initialObject не равен null
            dynamic Obj1 = null;
            string str1 = initialObject.GetType().ToString();
            // Если initialObject второго уровня и у него есть ссылка на третий уровень.
            try
            {
                Obj1 = initialObject.dll_obj;
            }
            catch { }
            if (Obj1 != null)
            {
                return (IValue)Obj1;
            }
            // Если initialObject не из пространства имен onescriptgui, то есть Уровень1 и у него есть аналог в
            // пространстве имен ostgui с конструктором принимающим параметром initialObject.
            try
            {
                if (!str1.Contains("ostgui."))
                {
                    Type TestType = Type.GetType(
                        "ostgui.Tf" + str1.Substring(str1.LastIndexOf(".") + 1),
                        false,
                        true);
                    object[] args = { initialObject };
                    Obj1 = Activator.CreateInstance(TestType, args);
                }
            }
            catch { }
            if (Obj1 != null)
            {
                return (IValue)Obj1;
            }
            // Если initialObject из пространства имен onescriptgui, то есть Уровень2 и у него есть аналог в
            // пространстве имен ostgui с конструктором принимающим параметром initialObject.
            try
            {
                if (str1.Contains("ostgui."))
                {
                    Type TestType = Type.GetType(
                        str1.Replace("ostgui.", "ostgui.Tf"),
                        false,
                        true);
                    object[] args = { initialObject };
                    Obj1 = Activator.CreateInstance(TestType, args);
                }
            }
            catch { }
            if (Obj1 != null)
            {
                return (IValue)Obj1;
            }
            // Если initialObject с возможными другими типами.
            string str4 = null;
            try
            {
                str4 = initialObject.SystemType.Name;
            }
            catch
            {
                if (str1 == "System.String" ||
                    str1 == "System.Decimal" ||
                    str1 == "System.Int32" ||
                    str1 == "System.Boolean" ||
                    str1 == "System.DateTime")
                {
                    return (IValue)ValueFactory.Create(initialObject);
                }
                else if (str1 == "System.Byte")
                {
                    int vOut = Convert.ToInt32(initialObject);
                    return ValueFactory.Create(vOut);
                }
                else if (str1 == "System.DBNull")
                {
                    string vOut = Convert.ToString(initialObject);
                    return ValueFactory.Create(vOut);
                }
            }
            // Если тип initialObject определяется односкриптом.
            if (str4 == "Неопределено")
            {
                return null;
            }
            if (str4 == "Булево" || str4 == "Дата" || str4 == "Число" || str4 == "Строка")
            {
                return (IValue)initialObject;
            }
            // Если ничего не подходит.
            return (IValue)initialObject;
        }

        // Для надписей на элементах. Язык надписей определен в ресурсах исходной библиотеки Terminal.Gui. 
        // Не ломая этот алгоритм добавил выбор между английским и русским языком с помощью свойства 
        // ТерминалФормыДляОдноСкрипта.ЯзыкНадписей (OneScriptTerminalGui.LabelLanguage).
        public static Dictionary<string, string> labelRusEn = new Dictionary<string, string>
            {
                {"_Copy", "_Копировать" },
                {"Cu_t", "Выреза_ть" },
                {"_Delete All", "_Удалить все" },
                {"_Paste", "_Вставить" },
                {"_Redo", "_Повтор" },
                {"_Select All", "Вы_брать все" },
                {"_Undo", "_Отмена" },
                {"Directory", "Каталог" },
                {"File", "Файл" },
                {"Open", "Открыть" },
                {"Save", "Сохранить" },
                {"Save as", "Сохранить как" },
                {"Select folder", "Выбор папки" },
                {"Select Mixed", "Смешанный выбор" },
                {"_Back", "_Назад" },
                {"Fi_nish", "_Завершить" },
                {"_Next...", "_Следующий..." },
        };

        public static ArrayImpl SplitString(string p1, string p2)
        {
            ArrayImpl array = new ArrayImpl();
            string str = p1.Replace(p2, Environment.NewLine);
            string[] result = str.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < result.Length; i++)
            {
                array.Add(ValueFactory.Create(result[i]));
            }
            return array;
        }

        public static ArrayImpl GetAllSelectedCells(IEnumerable<Terminal.Gui.Point> pointArray)
        {
            ArrayImpl array = new ArrayImpl();
            Terminal.Gui.Point[] treeNodeArray = pointArray.ToArray();
            foreach (var value in pointArray)
            {
                array.Add(new TfPoint(value.X, value.Y));
            }
            return array;
        }

        public static ArrayImpl MultiSelectedRegions(Stack<Terminal.Gui.TableView.TableSelection> pointRect)
        {
            ArrayImpl array = new ArrayImpl();
            Terminal.Gui.TableView.TableSelection[] treeNodeArray = pointRect.ToArray();
            foreach (var value in pointRect)
            {
                array.Add(new TfRect(value.Rect.X, value.Rect.Y, value.Rect.Width, value.Rect.Height));
            }
            return array;
        }

        public static string NewLine
        {
            get { return Environment.NewLine; }
        }
    }
}
