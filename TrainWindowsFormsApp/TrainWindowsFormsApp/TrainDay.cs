using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainWindowsFormsApp
{
    public static class TrainDay
    {
        public static int progress = TrainCommon.GetProgress();
        public static List<int> indentBetweenExercises = new List<int>();
        private static List<ExercisesType> train = new List<ExercisesType>();

        public static List<string> pathList = new List<string>();

        private static readonly List<List<ExercisesType>> trainDays = new List<List<ExercisesType>>()
        {
            new List<ExercisesType>() { ExercisesType.ChestBase, ExercisesType.Latissimus, ExercisesType.Quads, ExercisesType.Core, ExercisesType.Triceps, ExercisesType.Biceps },
            new List<ExercisesType>() { ExercisesType.BackExtensor, ExercisesType.Trapezius, ExercisesType.DeltoidMid, ExercisesType.Neck },
            new List<ExercisesType>() { ExercisesType.Calf, ExercisesType.Calf, ExercisesType.Forearm }
        };
        public static List<Dictionary<string, string>> GetTrain()
        {
            train = trainDays[progress % trainDays.Count];
            indentBetweenExercises = Enumerable.Range(0, train.Count).ToList();
            return GetExercisesInTrain(train);
        }
        public static List<Dictionary<string, string>> GetAdvancedTrain()
        {
            train = trainDays[progress % trainDays.Count];
            switch (progress % trainDays.Count)
            {
                case 0:
                    train.InsertRange(4, new List<ExercisesType>() { ExercisesType.ChestBase, ExercisesType.Latissimus, ExercisesType.Quads, ExercisesType.Core });
                    break;
                case 1:
                    train.InsertRange(2, new List<ExercisesType>() { ExercisesType.BackExtensor, ExercisesType.Trapezius, ExercisesType.DeltoidMid, ExercisesType.Neck }); break;
                default:
                    train.InsertRange(1, new List<ExercisesType>() { ExercisesType.Shin, ExercisesType.Calf, ExercisesType.Forearm }); break;
            }
            indentBetweenExercises = Enumerable.Range(0, train.Count).ToList();
            return GetExercisesInAdvancedTrain(train);
        }
        private static List<Exercise> GetExerciseData(string path)
        {
            return TrainCommon.GetDeserializedData(path);
        }
        private static int GetTypeIndex(Exercise ex, int mod = 0)
        {
            return (progress + mod) / trainDays.Count / ex.typesTrainingList.Count % ex.typesTrainingList.Count;
        }
        private static Exercise ChoseExercise(List<Exercise> deserializedList, int mod = 0)
        {
            int index = (progress + mod) / trainDays.Count / deserializedList.Count % deserializedList.Count;
            return deserializedList[index];
        }
        public static List<string> CreatePathList(List<ExercisesType> exercisesTypeList)
        {
            var path = exercisesTypeList.ConvertAll(x => "ExercisesType/" + Convert.ToString(x) + ".json");
            return path.Distinct().ToList();
        }
        public static List<Dictionary<string, string>> GetExercisesInTrain(List<ExercisesType> list)
        {
            var exercisesTypeList = new List<ExercisesType>(list).Distinct().ToList();  // Список разных типов упражнений,
            pathList = CreatePathList(exercisesTypeList);      // по нему делаем список путей к файлам.
            var finishedList = new List<Dictionary<string, string>>(); // Финальный массив словарей.
            for (int i = 0; i < exercisesTypeList.Distinct().Count(); i++)  // Цикл по количеству разных типов упражнений.
            {
                var exercisesList = GetExerciseData(pathList[i]);  // Десериализованный список упражнений.
                int countOccurences = list.FindAll(x => x == exercisesTypeList[i]).Count;  // Количество вхождений этой группы мышц в основном списке тренировки,
                for (int j = 0; j < countOccurences; j++)                                  // такое количество раз прогоним этот цикл.
                {
                    var mod = 0;
                    if (exercisesList.Count == 0)
                    {// Если десериалезованный список с упражнениями оказался пуст, то заполняем снова
                        exercisesList = GetExerciseData(pathList[i]);
                        mod = 6;
                    }
                    var chosenExercise = exercisesList[0];  // Выбираем упражнение из десериализованного списка
                    exercisesList.Remove(chosenExercise);   // и удаляем оттуда.
                    var chosenTypeExercise = chosenExercise.typesTrainingList[GetTypeIndex(chosenExercise, mod)];  // Выбираем тип тренировки (сила, выносливость и т.д.)
                    var dict = new Dictionary<string, string>                           // Словарь для выбранного типа упражнения,
                    {
                        { "name", chosenExercise.Name },                                // в него добавляем название упражнения,
                        { "typeTrain", Convert.ToString(chosenTypeExercise["name"]) },  // тип тренировки,
                        { "load", Convert.ToString(chosenTypeExercise["load"]) },       // нагрузку,
                        { "remark", chosenExercise.Remark },                            // ремарку,
                    };
                    if (chosenTypeExercise.ContainsKey("repeats"))                           // если есть количество повторений (нет в Табата),
                    {
                        dict.Add("repeats", Convert.ToString(chosenTypeExercise["repeats"])); // то добавляем повторения
                        dict.Add("maxRepeats", Convert.ToString(chosenExercise.MaxRepeat));   // и их максимальное количество.
                    }
                    var indexExTypeInStartedList = list.IndexOf(exercisesTypeList[i]);  // Находим в основном списке индекс этого типа упражнения,
                    try
                    {
                        finishedList.Insert(indexExTypeInStartedList, dict);                     // добавляем по этому индексу в финальный массив
                    }
                    catch
                    {
                        finishedList.Add(dict);
                    }
                    list[indexExTypeInStartedList] = ExercisesType.None;                          // и удаляем из основного
                }
            }
            return finishedList;
        }
        public static List<Dictionary<string, string>> GetExercisesInAdvancedTrain(List<ExercisesType> list)
        {
            var exercisesTypeList = new List<ExercisesType>(list).Distinct().ToList();  // Список разных типов упражнений,
            pathList = CreatePathList(exercisesTypeList);      // по нему делаем список путей к файлам.
            var finishedList = new List<Dictionary<string, string>>(); // Финальный массив словарей.
            for (int i = 0; i < exercisesTypeList.Distinct().Count(); i++)  // Цикл по количеству разных типов упражнений.
            {
                var exercisesList = GetExerciseData(pathList[i]);  // Десериализованный список упражнений.
                int countOccurences = list.FindAll(x => x == exercisesTypeList[i]).Count;  // Количество вхождений этой группы мышц в основном списке тренировки,
                for (int j = 0; j < countOccurences; j++)                                  // такое количество раз прогоним этот цикл.
                {
                    var mod = 0;
                    if (exercisesList.Count == 0)
                    {// Если десериалезованный список с упражнениями оказался пуст, то заполняем снова
                        exercisesList = GetExerciseData(pathList[i]);
                        mod = 8;
                    }
                    var chosenExercise = ChoseExercise(exercisesList);  // Выбираем упражнение из десериализованного списка
                    exercisesList.Remove(chosenExercise);               // и удаляем оттуда.
                    var chosenTypeExercise = chosenExercise.typesTrainingList[GetTypeIndex(chosenExercise, mod)];  // Выбираем тип тренировки (сила, выносливость и т.д.)
                    var dict = new Dictionary<string, string>                           // Словарь для выбранного типа упражнения,
                    {
                        { "name", chosenExercise.Name },                                // в него добавляем название упражнения,
                        { "typeTrain", Convert.ToString(chosenTypeExercise["name"]) },  // тип тренировки,
                        { "load", Convert.ToString(chosenTypeExercise["load"]) },       // нагрузку,
                        { "remark", chosenExercise.Remark },                            // ремарку,
                    };
                    if (chosenTypeExercise.ContainsKey("repeats"))                           // если есть количество повторений (нет в Табата),
                    {
                        dict.Add("repeats", Convert.ToString(chosenTypeExercise["repeats"])); // то добавляем повторения
                        dict.Add("maxRepeats", Convert.ToString(chosenExercise.MaxRepeat));   // и их максимальное количество.
                    }
                    var indexExTypeInStartedList = list.IndexOf(exercisesTypeList[i]);  // Находим в основном списке индекс этого типа упражнения,
                    try
                    {
                        finishedList.Insert(indexExTypeInStartedList, dict);                     // добавляем по этому индексу в финальный массив
                    }
                    catch
                    {
                        finishedList.Add(dict);
                    }
                    list[indexExTypeInStartedList] = ExercisesType.None;                          // и удаляем из основного
                }
            }
            return finishedList;
        }
        public static List<Exercise> GetWarmUpList()
        {
            var random = new Random();
            var warmUp = new List<ExercisesType>()
            {
                ExercisesType.CombatArms,
                ExercisesType.CombatLegs,
            };
            var warmUpPathList = CreatePathList(warmUp);
            var result = new List<Exercise>();
            foreach (string path in warmUpPathList)
            {
                var exList = GetExerciseData(path);
                for (int i = 0; i < 3; i++)
                {
                    var ex = ChoseExercise(exList);
                    try
                    {
                        result.Insert(random.Next(warmUpPathList.Count), ex);
                    }
                    catch
                    {
                        result.Add(ex);
                    }
                    exList.Remove(ex);
                }
            }
            
            return result;
        }
        public static List<Exercise> GetMorningWorkOut()
        {
            List<ExercisesType> workOut = new List<ExercisesType>() { ExercisesType.CombatArms, ExercisesType.CombatLegs};

            var workOutPathList = CreatePathList(workOut);
            var result = new List<Exercise>();
            foreach(string path in workOutPathList)
            {
                var exList = GetExerciseData(path);
                result.Add(exList[progress / exList.Count % exList.Count]);
            }

            return result;
        }
    }
}

