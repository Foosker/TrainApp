﻿using Newtonsoft.Json;
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
        public static string option = GetOption();

        static public List<string> pathsList = new List<string>();  // Массив для хранения всех путей к файлам нужен для сохранения результатов в конце тренировки

        // Свойства элементов управления
        public static int height = 70;          // Высота ЭУ
        public static int indentBetween = 12;    // Расстояние между ЭУ по горизонтали,
        public static int indentUpEdge = 25;                   // то же по вертикали.
        public static List<int> increaseIndentUpEdge;

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

        static private string GetOption()
        {
            List<string> options = new List<string> { "strength", "stamina", "tabata" };
            return options[progress / 8 % options.Count];
        }

        static public void SaveProgress()
        {
            progress++;
            var data = progress.ToString();

            FileProvider.Save(pathToProgressFile, data);
        }

        static public void SaveTrainResults(Exercise[] exercises)
        {
            for (int i = 0; i < pathsList.Count; i++)
            {
                var deserializableData = GetDeserializedData(pathsList[i]);

                for (int j = 0; j < exercises.Length; j++)
                {   // Здесь сравнение количества повторов с максимальным количеством
                    if (exercises[j].StrengthRepeat > exercises[j].MaxRepeat / 2)
                    {
                        var form = new SetNewLoadForm(exercises[j]);
                        form.ShowDialog();
                        exercises[j].StrengthRepeat = 3;
                        exercises[j].StrengthLoad = form.NewLoad;
                    }
                    else if (exercises[j].StaminaRepeat > exercises[j].MaxRepeat)
                    {
                        var form = new SetNewLoadForm(exercises[j]);
                        form.ShowDialog();
                        exercises[j].StaminaRepeat = 10;
                        exercises[j].StaminaLoad = form.NewLoad;
                    }

                    foreach (var exerc in deserializableData)
                    {
                        if (exerc.Name == exercises[j].Name)
                        {
                            exerc.StrengthRepeat = exercises[j].StrengthRepeat;
                            exerc.StrengthLoad = exercises[j].StrengthLoad;
                            exerc.StaminaRepeat = exercises[j].StaminaRepeat;
                            exerc.StaminaLoad = exercises[j].StaminaLoad;
                        }
                    }
                }
                var serializableData = JsonConvert.SerializeObject(deserializableData, Formatting.Indented);
                FileProvider.Save(pathsList[i], serializableData);
            }
        }

        static public Exercise[] GetExercises(string option = "train")
        {   // Получаем список тренируемых мышц
            List<ExercisesType> exercisesList;

            increaseIndentUpEdge = TrainDay.indentBetweenExercises;

            if (option == "train")
            {
                exercisesList = TrainDay.GetTrain(progress);
                increaseIndentUpEdge = TrainDay.indentBetweenExercises;
            }
            else
            {
                exercisesList = TrainDay.GetWarmUpList();
            }

            var exercisesCount = exercisesList.Count();

            var differentExecriseTypes = exercisesList.Distinct().ToList<ExercisesType>();
            var numberDifferentExercises = differentExecriseTypes.Count();

            var exerciseArray = new Exercise[exercisesCount];  // Создание массива, куда будут добавляться упражнения

            for (int i = 0; i < numberDifferentExercises; i++)
            {   // Название упражнения преобразуем в путь к файлу
                var pathExerciseFile = "ExercisesType/" + differentExecriseTypes[i].ToString() + ".json";
                // и добавляем в список всех путей, если это тренировка или дополнение к ней
                if (option != "warmUp") pathsList.Add(pathExerciseFile);

                var deserializableDataExercises = GetDeserializedData(pathExerciseFile);

                for (int j = i; j < exercisesCount; j++)
                {
                    if (differentExecriseTypes[i] == exercisesList[j])
                    {
                        int exerciseIndex;  // Индекс упражнения
                        if (option == "warmUp") exerciseIndex = random.Next(deserializableDataExercises.Count());
                        else exerciseIndex = (progress) / deserializableDataExercises.Count % deserializableDataExercises.Count();

                        var exercise = deserializableDataExercises[exerciseIndex];  // Выбор упражнения по индексу,
                        exerciseArray[j] = exercise;                                // добавление его в основной массив,
                        deserializableDataExercises.Remove(exercise);               // удаление из списка файла.
                    }
                }

            }
            return exerciseArray;
        }

        static public List<Exercise> GetDeserializedData(string path)
        {   // Получение данных из файла
            var dataExercises = FileProvider.GetData(path);
            // и десериализация в список.
            var deserializableDataExercises = JsonConvert.DeserializeObject<List<Exercise>>(dataExercises);

            return deserializableDataExercises;
        }
    }
}
