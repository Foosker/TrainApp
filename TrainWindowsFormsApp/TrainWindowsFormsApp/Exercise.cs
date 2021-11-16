using System.Collections.Generic;

namespace TrainWindowsFormsApp
{
    public class Exercise
    {
        // Тренировки выстроены на периодичность: сила, виносливость(многоповторность) и по принципу Табата (20сек. нагрузки, 10сек. отдыха)
        public string Name;                 // Название упражнения
        public List<Dictionary<string, object>> typesTrainingList = new List<Dictionary<string, object>>();  // Список, содержащий словари с типами тренировк
        public int MaxRepeat;               // Максимально количество повторений, при достижении которого следует указать новую нагрузку
        public string Remark;               // Примечания

        public Exercise(string name,
                        int strengthRepeat, int staminaRepeat, int intervalRepeat,
                        int maxRepeat,
                        string strengthLoad, string staminaLoad, string tabataLoad, string intervalLoad,
                        string intervalExercise, string superSplitExercise,
                        string remark = null)
        {
            Name = name;
            MaxRepeat = maxRepeat;
            Remark = remark;
            if (strengthLoad != null)
            {
                var Strength = new Dictionary<string, object>(3)
                {
                    { "name", "Strength" },
                    { "load", strengthLoad },
                    { "repeats", strengthRepeat }
                };
                typesTrainingList.Add(Strength);
            }
            if (staminaLoad != null)
            {
                var Stamina = new Dictionary<string, object>(3)
                {
                    { "name", "Stamina" },
                    { "load", staminaLoad },
                    { "repeats", staminaRepeat }
                };
                typesTrainingList.Add(Stamina);
            }
            if (tabataLoad != null)
            {
                var Tabata = new Dictionary<string, object>(2)
                {
                    { "name", "Tabata" },
                    { "load", tabataLoad }
                };
                typesTrainingList.Add(Tabata);
            }
            if (intervalLoad != null)
            {
                var Interval = new Dictionary<string, object>(4)
                {
                    { "name", "Interval" },
                    { "load", intervalLoad },
                    { "repeats", intervalRepeat },
                    { "exercises", new List<string> { intervalExercise, superSplitExercise } }
                };
                typesTrainingList.Add(Interval);
            }
        }

        public Exercise() { }
    }
}