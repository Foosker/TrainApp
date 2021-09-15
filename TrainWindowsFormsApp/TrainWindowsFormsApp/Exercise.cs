namespace TrainWindowsFormsApp
{
    public class Exercise
    {
        public string Name;     // Название упражнения
        public bool IsBase;     // Базовое ли упражнение
        public int Repeat;      // Количество повторенеий
        public int MaxRepeat;   // Максимально количество повторений, при достижении которого следует указать новую нагрузку
        public string Load;     // Нагрузка
        public string Remark;   // Примечания

        public Exercise(string name, bool isBase, int repeat, int maxRepeat, string load, string remark = null)
        {
            Name = name;
            IsBase = isBase;
            Repeat = repeat;
            MaxRepeat = maxRepeat;
            Load = load;
            Remark = remark;
        }

        public Exercise() { }
    }
}
