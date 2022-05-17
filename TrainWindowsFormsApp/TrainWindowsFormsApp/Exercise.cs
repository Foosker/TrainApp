using System.Collections.Generic;

namespace TrainWindowsFormsApp
{
    public class Exercise
    {
        // Тренировки выстроены на периодичность: сила, виносливость(многоповторность) и по принципу Табата (20сек. нагрузки, 10сек. отдыха)
        public string Name;                 // Название упражнения
        public List<Dictionary<string, string>> typesTrainingList = new List<Dictionary<string, string>>();  // Список, содержащий словари с типами тренировк
        public float MaxRepeat;               // Максимально количество повторений, при достижении которого следует указать новую нагрузку
        public string Remark;               // Примечания

        public Exercise(string name,
                        string strengthRepeat, string staminaRepeat,
                        float maxRepeat,
                        string strengthLoad, string staminaLoad, string tabataLoad, string intervalLoad,
                        string intervalExercise,
                        string remark = null)
        {
            Name = name;
            MaxRepeat = maxRepeat;
            Remark = remark;
            if (strengthLoad != null)
            {
                var Strength = new Dictionary<string, string>(3)
                {
                    { "name", "Strength" },
                    { "load", strengthLoad },
                    { "repeats", strengthRepeat }
                };
                typesTrainingList.Add(Strength);
            }
            if (staminaLoad != null)
            {
                var Stamina = new Dictionary<string, string>(3)
                {
                    { "name", "Stamina" },
                    { "load", staminaLoad },
                    { "repeats", staminaRepeat }
                };
                typesTrainingList.Add(Stamina);
            }
            if (tabataLoad != null)
            {
                var Tabata = new Dictionary<string, string>(2)
                {
                    { "name", "Tabata" },
                    { "load", tabataLoad }
                };
                typesTrainingList.Add(Tabata);
            }
            if (intervalLoad != null)
            {
                var Interval = new Dictionary<string, string>(2)
                {
                    { "name", "Interval" },
                    { "exercises", intervalExercise }
                };
                typesTrainingList.Add(Interval);
            }
        }

        public Exercise() { }
    }
}