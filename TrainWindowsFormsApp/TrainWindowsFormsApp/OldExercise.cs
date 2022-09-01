namespace TrainWindowsFormsApp
{
    public class OldExercise
    {
        // Тренировки выстроены на периодичность: сила, виносливость(многоповторность) и по принципу Табата (20сек. нагрузки, 10сек. отдыха)
        public string Name;                 // Название упражнения
        public int    StrengthRepeat;
        public int    StaminaRepeat;
        public int    IntervalRepeat;
        public string StrengthLoad; 
        public string StaminaLoad; 
        public string TabataLoad;
        public string IntervalLoad;
        public string IntervalExercise; 
        public string SuperSplitExercise;
        public int    MaxRepeat;            // Максимально количество повторений, при достижении которого следует указать новую нагрузку
        public string Remark;               // Примечания

        public OldExercise(string name,
                        int strengthRepeat, int staminaRepeat, int intervalRepeat,
                        int maxRepeat,
                        string strengthLoad, string staminaLoad, string tabataLoad, string intervalLoad,
                        string intervalExercise, string superSplitExercise,
                        string remark = null)
        {
            Name = name;
            StrengthRepeat = strengthRepeat;
            StaminaRepeat = staminaRepeat;
            IntervalRepeat = intervalRepeat;
            StrengthLoad = strengthLoad;
            StaminaLoad = staminaLoad;
            TabataLoad = tabataLoad;
            IntervalLoad = intervalLoad;
            IntervalExercise = intervalExercise;
            SuperSplitExercise = superSplitExercise;
            MaxRepeat = maxRepeat;
            Remark = remark;
        }

        public OldExercise() { }        
    }
}
