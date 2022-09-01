using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TrainWindowsFormsApp
{
    static class TrainCommon
    {
        static private readonly string pathToProgressFile = "progress.txt";
        static public int progress = GetProgress();

        static public List<string> pathsList = new List<string>();  // Массив для хранения всех путей к файлам нужен для сохранения результатов в конце тренировки

        // Свойства элементов управления
        public static int indentBetween = 12;   // Расстояние между ЭУ по горизонтали,
        public static int indentUpEdge = 25;    // то же по вертикали.

        private static readonly Random random = new Random();

        private static List<Color> colors = new List<Color>()
        {
            // Синие и зелёные
            SystemColors.Highlight,
            Color.LightSeaGreen,
            Color.GreenYellow,
            Color.LawnGreen,
                
            // Красные, розовые и фиолетовые
            Color.Firebrick,
            Color.HotPink,
            Color.MediumOrchid,
            Color.Violet,

            // Жёлтые и оранжевые
            Color.Gold,
            Color.Goldenrod,
            Color.DarkOrange,
            Color.DarkTurquoise,
        };
        static internal readonly List<double> loadNumber = new List<double>()
        {// Нагрузка в числах сразу в формате строки, чтобы потом лишний раз не конвертировать
            0, 1.5, 2, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10, 10.5, 11, 11.5, 12, 12.5, 13, 14, 14.5, 16, 
            17.5, 18, 19, 19.5, 20, 20.5, 21, 21.5, 22, 22.5, 23, 23.5, 24, 24.5, 25, 25.5, 26, 26.5, 27, 27.5, 28, 28.5, 29, 30, 30.5, 31, 32
        };
        static private readonly List<string> loadBands = new List<string>()
        {// Дополнительная нагрузка в резиновых петлях
            "МП", "СП", "НСП", "Р", "БП", "Р*2", "БП*2"
        };
        static private Color GetColor()
        {
            var chosenColor = colors[random.Next(colors.Count)];
            colors.RemoveAll(x=>  colors.IndexOf(x) / 4 == colors.IndexOf(chosenColor) / 4);
            return chosenColor;
        }
        public static Color buttonsColor = GetColor();
        public static Color labelsColor = GetColor();
        public static Color textBoxColor = GetColor();
        static public Button CreateButton(Form form, int indentLeftEdge, float indexRow, int width, int _height = 70, string initialText = "")
        {   // Создание кнопок 
            var x = indentLeftEdge;
            var y = indentUpEdge + indexRow * (indentBetween + _height);  // Формула расчёта координат эллемента по ординате

            var button = new Button
            {
                BackColor = buttonsColor,
                Font = new Font("Tahoma", 18F, FontStyle.Regular, GraphicsUnit.Point, 204),
                Text = initialText,
                Size = new Size(width, _height),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(x, (int)y)
            };
            form.Controls.Add(button);
            return button;
        }

        static public TextBox CreateTextBox(Form form, int indentLeftEdge, float indexRow, int width, int _height = 70, string initialText = "")
        {   // Создание кнопок 
            var x = indentLeftEdge;
            var y = indentUpEdge + indexRow * (indentBetween + _height);  // Формула расчёта координат эллемента по ординате

            var textBox = new TextBox
            {
                BackColor = textBoxColor,
                Font = new Font("Segoe Script", 30F, FontStyle.Regular, GraphicsUnit.Point, 204),
                Text = initialText,
                Size = new Size(width, _height),
                TextAlign = HorizontalAlignment.Center,
                Location = new Point(x, (int)y)
            };
            form.Controls.Add(textBox);
            return textBox;
        }

        static public Label CreateLabel(Form form, int indentLeftEdge, float indexRow, int width, int _height, string initialText = "")
        {   // Создание ячеек
            var x = indentLeftEdge;
            var y = indentUpEdge + indexRow * (indentBetween + _height); // Формула расчёта координат эллемента по ординате

            var label = new Label
            {
                BackColor = labelsColor,
                Font = new Font("Gabriola", 30F, FontStyle.Bold, GraphicsUnit.Point, 204),
                Size = new Size(width, _height),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = initialText,
                Location = new Point(x, (int)y)
            };
            form.Controls.Add(label);
            return label;
        }
        static public void DarkenControlElement(Control controlElement)
        {// Затемняет контрол-элемент
            controlElement.BackColor = Color.Gray;
            controlElement.ForeColor = Color.DarkGray;
        }
        //
        // Работа с файлами
        //
        static public int GetProgress()
        {
            var isExistFile = FileProvider.TryGet(pathToProgressFile, out var data);

            if (!isExistFile)
            {
                FileProvider.Save(pathToProgressFile, "1");
                data = "1";
            }
            return int.Parse(data);
        }
        static public void SaveProgress()
        {
            progress++;
            var data = progress.ToString();

            FileProvider.Save(pathToProgressFile, data);
        }
        
        static public void SaveTrainResults(List<Dictionary<string, string>> exercises)
        {
            pathsList = TrainDay.pathList.Distinct().ToList();
            for (int i = 0; i < pathsList.Count; i++)
            {
                var deserializableData = GetDeserializedData(pathsList[i]);
                foreach (Exercise ex in deserializableData)
                {                                             // Сравнить каждое упражнение в десериализованной дате
                    for (int j = 0; j < exercises.Count; j++) // с каждым упражнением в списке словарей в конце тренировки.
                    {
                        if (ex.Name == exercises[j]["name"]) // Если имя упражнения из десер.даты совпадает с именем из списка,
                        {
                            foreach (Dictionary<string, string> type in ex.typesTrainingList)
                            {  // если имена похожи и что-то из нагрузки или повторов - нет,
                                if (type["name"] == exercises[j]["typeTrain"])
                                {
                                    if (type["load"] != exercises[j]["load"])
                                    { // то меняем их
                                       type["load"] = exercises[j]["load"];
                                    }
                                    if (type.ContainsKey("repeats"))
                                    {
                                        type["repeats"] = exercises[j]["repeats"];
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                var serializableData = JsonConvert.SerializeObject(deserializableData, Formatting.Indented);    // Сериализуем данные
                FileProvider.Save(pathsList[i], serializableData);                                              // и перезаписываем.
            }
        }        
        static public int GetTrainingTypeIndex(Exercise ex)
        {
            return progress % ex.typesTrainingList.Count;
        }
                
        static public List<Exercise> GetDeserializedData(string path)
        {   // Получение данных из файла
            var dataExercises = FileProvider.GetData(path);
            // и десериализация в список.
            var deserializableDataExercises = JsonConvert.DeserializeObject<List<Exercise>>(dataExercises);

            return deserializableDataExercises;
        }
        internal static string LoadIncrease(string oldLoad)
        {// Функция увеличения нагрузки
            if (oldLoad[0] <= '9')
            {// Если в нагрузке первой идёт цифра
                var plusIndex = oldLoad.IndexOf('+'); // Индекс плюса в строке
                if (plusIndex >= 0)
                {// Если индекс не равняется -1, т.е там есть нагрузка и в цифрах и в резинках
                    var lN = Convert.ToDouble(oldLoad.Substring(0, plusIndex)); // Срез строки состоящей из только цифр
                    if (lN != loadNumber[loadNumber.Count - 1])
                    {// Если число нагрузки не является последним в списке нагрузок
                        lN = loadNumber[loadNumber.IndexOf(lN) + 1];
                        return lN + oldLoad.Substring(plusIndex, oldLoad.Length - plusIndex);
                    }
                }
                else if (Double.TryParse(oldLoad, out var newLoad) & newLoad != loadNumber[loadNumber.Count - 1])
                {// Если плюса нет, т.е. состоит только из цифр, а также не достигла максимума
                    return Convert.ToString(loadNumber[loadNumber.IndexOf(newLoad) + 1]); // Находим его индекс в списке, преобразуем в строку и возвращаем следующий
                }
                else
                {
                    var newLoadForm = new SetNewLoadForm();
                    newLoadForm.HandOverOldLoad(oldLoad);
                    newLoadForm.ShowDialog();
                    return newLoadForm.load;
                }
            }
            // Этот кусок кода достигается если нагрузка состоит только из резинок
           return loadBands[loadBands.IndexOf(oldLoad) + 1];  // Возвращается следующий индекс нагрузки резинкой            
        }
        /*static public void ConvertExercisesToNewFormat()
        {
            var arrayExercises = (ExercisesType[])Enum.GetValues(typeof(ExercisesType));

            for (int i = 3; i < arrayExercises.Length; i++)
            {
                var path = "ExercisesType\\" + Convert.ToString(arrayExercises[i]) + ".json";
                var dD = GetDeserializedData(path);
                var nD = new List<Exercise>();
                for (int j = 0; j < dD.Count; j++)
                {
                    var newExercise = new Exercise(dD[j].Name,
                         dD[j].StrengthRepeat, dD[j].StaminaRepeat, dD[j].IntervalRepeat, dD[j].MaxRepeat,
                         dD[j].StrengthLoad, dD[j].StaminaLoad, dD[j].TabataLoad, dD[j].IntervalLoad,
                         dD[j].IntervalExercise, dD[j].SuperSplitExercise,
                         dD[j].Remark);
                    nD.Add(newExercise);
                }
                var sD = JsonConvert.SerializeObject(nD, Formatting.Indented);
                FileProvider.Save(path, sD);
            }
        }*/
    }
}
