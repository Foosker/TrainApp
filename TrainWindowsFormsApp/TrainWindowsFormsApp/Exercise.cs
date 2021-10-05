using System.Collections.Generic;

namespace TrainWindowsFormsApp
{
    public class Exercise
    {
        // Тренировки выстроены на периодичность: сила, виносливость(многоповторность) и по принципу Табата (20сек. нагрузки, 10сек. отдыха)
        public string Name;     // Название упражнения
        public int StrengthRepeat;      // Количество повторений для силы
        public int StaminaRepeat;      // Количество повторений для выносливости
        public int MaxRepeat;   // Максимально количество повторений, при достижении которого следует указать новую нагрузку
        public string StrengthLoad;     // Нагрузка
        public string StaminaLoad;     // Нагрузка
        public string TabataLoad;     // Нагрузка
        public string Remark;   // Примечания

        public Exercise(string name, 
                        int strengthRepeat, int staminaRepeat, 
                        int maxRepeat, 
                        string strengthLoad, string staminaLoad, string tabataLoad, 
                        string remark = null)
        {
            Name = name;
            StrengthRepeat = strengthRepeat;
            StaminaRepeat = staminaRepeat;
            MaxRepeat = maxRepeat;
            StrengthLoad = strengthLoad;
            StaminaLoad = staminaLoad;
            TabataLoad = tabataLoad;
            Remark = remark;
        }

        public Exercise() { }
    }
}