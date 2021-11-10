using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TrainWindowsFormsApp
{
    public static class TrainDay
    {
        private static int progress;
        public static List<int> indentBetweenExercises = new List<int>();
        private static List<ExercisesType> train = new List<ExercisesType>();

        public static List<string> pathList = new List<string>();

        private static void GetChestTrain()
        {
            for (int i = 0; i < 2; i++)
            {
                train.AddRange(new List<ExercisesType> { ExercisesType.ChestBase, ExercisesType.DeltoidMid, ExercisesType.Core});
            }
        }

        private static void GetBackTrain()
        {
            train.AddRange(new List<ExercisesType> { ExercisesType.DeltoidRear, ExercisesType.BackExtensor, ExercisesType.Latissimus,
            ExercisesType.DeltoidRear, ExercisesType.BackExtensor, ExercisesType.Biceps});        
            
        }

        private static void GetLegsTrain()
        {
            var legs = new List<ExercisesType> { ExercisesType.Quads, ExercisesType.Calf };
            var index = progress / legs.Count % legs.Count;
            for(int i = 0; i < 2; i++)
            {
                train.Add(legs[index]);
            }
        }

        public static List<Dictionary<string, string>> GetTrain()
        {
            switch (progress % 4)
            {
                case (0):
                    GetChestTrain();
                    break;
                case (2):
                    GetBackTrain();
                    break;
                default:
                    GetLegsTrain();
                    break;
            }
            indentBetweenExercises = Enumerable.Range(1, train.Count - 1).ToList();
            return GetExercisesInTrain(train);
        }

        private static List<Exercise> GetExerciseData(string path)
        {
            return TrainCommon.GetDeserializedData(path);
        }

        private static int GetTypeIndex(Exercise ex)
        {
            return progress % ex.typesTrainingList.Count;
        }

        private static Exercise ChoseExercise(List<Exercise> deserializedList)
        {
            var index = progress / deserializedList.Count % deserializedList.Count;
            return deserializedList[index];
        }

        public static List<string> CreatePathList(List<ExercisesType> exercisesTypeList)
        {
            return exercisesTypeList.ConvertAll(x => "ExercisesType/" + Convert.ToString(x) + ".json");
        }

        public static List<Dictionary<string, string>> GetExercisesInTrain(List<ExercisesType> list)
        {
            var gotInterval = false;            // Для добавления доп. 
            var addedList = new List<object>(); // упражнения интервальной тренировки

            var exercisesTypeList = new List<ExercisesType>(list);  // Список разных типов упражнений,
            pathList = CreatePathList(exercisesTypeList);      // по нему делаем список путей к файлам.
            var finishedList = new List<Dictionary<string, string>>(); // Финальный массив словарей.
            for (int i = 0; i < exercisesTypeList.Distinct().Count(); i++)  // Цикл по количеству разных типов упражнений.
            {
                var exercisesList = GetExerciseData(pathList[i]);  // Десериализованный список упражнений.
                int countOccurences = list.FindAll(x => x == exercisesTypeList[i]).Count;  // Количество вхождений этой группы мышц в основном списке тренировки,
                for (int j = 0; j < countOccurences; j++)                                  // такое количество раз прогоним этот цикл.
                {
                    var chosenExercise = ChoseExercise(exercisesList);  // Выбираем упражнение из десериализованного списка
                    exercisesList.Remove(chosenExercise);               // и удаляем оттуда.
                    var chosenTypeExercise = chosenExercise.typesTrainingList[GetTypeIndex(chosenExercise)];  // Выбираем тип тренировки (сила, выносливость и т.д.)
                    var dict = new Dictionary<string, string>                               // Словарь для выбранного типа упражнения,
                    {
                        { "name", chosenExercise.Name },                          // в него добавляем название упражнения,
                        { "load", Convert.ToString(chosenTypeExercise["load"]) }, // нагрузку,
                        { "remark", chosenExercise.Remark }                       // ремарку.
                    };                            
                    if (chosenTypeExercise.ContainsKey("repeats"))                           // если есть количество повторений (нет в Табата),
                    { 
                        dict.Add("repeats", Convert.ToString(chosenTypeExercise["repeats"])); // то добавляем повторения
                        dict.Add("maxRepeats", Convert.ToString(chosenExercise.MaxRepeat));  // и их максимальное количество.
                    }

                    var indexExTypeInStartedList = list.IndexOf(exercisesTypeList[i]);  // Находим в основном списке индекс этого типа упражнения,

                    // Если выбранный тип - интервальная тренировка
                    if (chosenTypeExercise.ContainsValue("interval"))
                    {
                        AddExercInList(chosenTypeExercise["exercises"], i + 1);
                        gotInterval = true;
                        for (int k = indexExTypeInStartedList; k < indentBetweenExercises.Count; k++) indentBetweenExercises[k]++; // Увеличение всех номеров для отступа от лейбла в главной форме начиная от этого номера
                    }

                    try
                    {
                        finishedList.Insert(indexExTypeInStartedList, dict);                     // добавляем по этому индексу в финальный массив
                    }
                    catch
                    { 
                        finishedList.Add(dict);
                    }
                    list[indexExTypeInStartedList] = ExercisesType.Forearm;                          // и удаляем из основного
                }
            }
            if (gotInterval)
            {
                AddExercForIntervalTrain();
            }

            void AddExercInList(object addPaths, int addIndex)
            {
                var addedPathsList = addPaths as List<string>;
                addedList.Add(addedPathsList[progress / addedPathsList.Count % addedPathsList.Count]);
                addedList.Add(addIndex);
            }

            void AddExercForIntervalTrain()
            {
                for (int i = 0; i < addedList.Count; i += 2)
                {
                    if (!pathList.Contains(addedList[i]))
                    {
                        pathList.Add((string)addedList[i]);
                    }
                    var exercisesList = GetExerciseData((string)addedList[i]);  // Десериализованный список упражнений.
                    var chosenExercise = ChoseExercise(exercisesList);  // Выбираем упражнение из десериализованного списка
                    var random = new Random();
                    var chosenTypeExercise = chosenExercise.typesTrainingList[random.Next(chosenExercise.typesTrainingList.Count)];  // Выбираем тип тренировки (сила, выносливость и т.д.)
                    var dict = new Dictionary<string, string>                               // Словарь для выбранного типа упражнения,
                    {
                        { "name", chosenExercise.Name },                          // в него добавляем название упражнения,
                        { "load", Convert.ToString(chosenTypeExercise["load"]) }, // нагрузку,
                        { "remark", chosenExercise.Remark }                       // ремарку.
                    };
                    if (chosenTypeExercise.ContainsKey("repeat"))                           // если есть количество повторений (нет в Табата),
                    {
                        dict.Add("repeat", Convert.ToString(chosenTypeExercise["repeat"])); // то добавляем повторения
                        dict.Add("maxRepeat", Convert.ToString(chosenExercise.MaxRepeat));  // и их максимальное количество.
                    }
                    finishedList.Insert(i + 1, dict);
                }
            }
            return finishedList;
        }

        public static List<ExercisesType> GetWarmUpList()
        {
            var warmUp = new List<ExercisesType>()
            {
                ExercisesType.CombatArms,
                ExercisesType.CombatArms,
                ExercisesType.CombatLegs,
                ExercisesType.CombatArms,
                ExercisesType.CombatArms,
                ExercisesType.CombatLegs
            };
            return warmUp;
        }
    }
}

