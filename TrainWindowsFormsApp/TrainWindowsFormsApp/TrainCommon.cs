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
        public static int height = 70;          // Высота ЭУ
        public static int indentBetween = 12;    // Расстояние между ЭУ по горизонтали,
        public static int indentUpEdge = 25;                   // то же по вертикали.

        private static Random random = new Random();

        static private Color GetColor()
        {
            List<Color> colors = new List<Color>()
            {
                // Синие и зелёные
                SystemColors.Highlight,
                Color.LightSeaGreen,
                Color.GreenYellow,
                Color.LawnGreen,
                    
                // Красные ,розовые и фиолетовые
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
            return colors[random.Next(colors.Count)];
        }

        public static Color buttonsColor = GetColor();
        public static Color labelsColor = GetColor();

        static public Button CreateButton(Form form, int indentLeftEdge, int indexRow, int width, string initialText = "")
        {   // Создание кнопок 
            int x = indentLeftEdge;
            int y = indentUpEdge + indexRow * (indentBetween + height);  // Формула расчёта координат эллемента по ординате

            var button = new Button
            {
                BackColor = buttonsColor,
                Font = new Font("Tahoma", 18F, FontStyle.Regular, GraphicsUnit.Point, 204),
                Text = initialText,
                Size = new Size(width, height),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(x, y)
            };
            form.Controls.Add(button);
            return button;
        }

        static public Label CreateLabel(Form form, int indentLeftEdge, int indexRow, int width, string text="")
        {   // Создание ячеек
            int x = indentLeftEdge;
            int y = indentUpEdge + indexRow * (indentBetween + height); // Формула расчёта координат эллемента по ординате

            var label = new Label
            {
                BackColor = labelsColor,
                Font = new Font("Gabriola", 30F, FontStyle.Bold, GraphicsUnit.Point, 204),
                Size = new Size(width, height),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = text,
                Location = new Point(x, y)
            };
            form.Controls.Add(label);
            return label;
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
        /*
        static public void SaveTrainResults(Exercise[] exercises)
        {
            for (int i = 0; i < pathsList.Count; i++)
            {
                var deserializableData = GetDeserializedData(pathsList[i]);
                for (int j = 0; j < exercises.Length; j++)
                {   // Здесь сравнение количества повторов с максимальным количеством
                    

                    if (exercises[j].Strength != null && (int)exercises[j].Strength["repeats"] > exercises[j].MaxRepeat / 2)
                    {
                        var form = new SetNewLoadForm(exercises[j]);
                        form.ShowDialog();
                        exercises[j].Strength["repeats"] = 3;
                        exercises[j].Strength["load"] = form.NewLoad;

                    }
                    else if (exercises[j].Stamina != null && (int)exercises[j].Stamina["repeats"] > exercises[j].MaxRepeat)
                    {
                        var form = new SetNewLoadForm(exercises[j]);
                        form.ShowDialog();
                        exercises[j].Stamina["repeats"] = 10;
                        exercises[j].Stamina["load"] = form.NewLoad;
                    }

                    foreach (var exerc in deserializableData)
                    {
                        if (exerc.Name == exercises[j].Name)
                        {
                            exerc.Strength["repeats"] = exercises[j].Strength["repeats"];
                            exerc.Strength["load"] = exercises[j].Strength["load"];
                            exerc.Stamina["repeats"] = exercises[j].Stamina["repeats"];
                            exerc.Stamina["load"] = exercises[j].Stamina["load"];
                        }
                    }
                }
                var serializableData = JsonConvert.SerializeObject(deserializableData, Formatting.Indented);
                FileProvider.Save(pathsList[i], serializableData);
            }
        }*/
        
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
