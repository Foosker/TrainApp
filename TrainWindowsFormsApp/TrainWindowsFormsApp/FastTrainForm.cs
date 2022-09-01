using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrainWindowsFormsApp
{
    public partial class FastTrainForm : Form
    {
        private bool menuMode = true;

        private ExercisesType[] arrayExercises = new ExercisesType[19];
        private int numberExercises;
        public string[] ExercisesTypeRus = new string[19]
        {
        "Удары рукой",
        "Удары ногой",

        "Грудь базовые",
        "Грудь Изол.",
        "Широчайшие",
        "Шея",
        "Трапеции",
        "Передние дельты",
        "Средние дельты",
        "Задние дельты",
        "Бицепсы",
        "Трицепсы",
        "Предплечья",
        "Квадрицепсы",
        "Бицепсы бедра",
        "Пресс",
        "Разгиб. спины",
        "Икры",
        "Голень"
        };

        private string pathExercisesPath;

        private List<Exercise> exercisesData;
        private Button[] exercisesButtons = new Button[19];
        private List<Label> nameLabels;     // Лейблы с именами упражнений,
        private List<Label> loadLabels;     // лейблы с нагрузкой - нужны для удаления из Controls,
        private List<Label> repeatLabels;   // текст-бокс

        public FastTrainForm()
        {
            InitializeComponent();
        }
        private void FastTrainForm_Load(object sender, EventArgs e)
        {
            // Установка координат кнопок Сохранение, Назад и Выход
            var formWidth = ClientSize.Width;   // Получение ширины
            var formHeight = ClientSize.Height; // и высоты формы
            // Кнопка Выход: X = ширина формы - ширина кнопки - заданный отступ от края, Y - тоже самое, только с высотой.
            QuitButton.Location = new Point(x: formWidth - QuitButton.Width - 25, y: formHeight - QuitButton.Height - 25);
            // Кнопка Назад: X = координаты кнопки Выход, Y = координаты кнопки Выход - высота кнопки Назад - заданный отступ.
            BackButton.Location = new Point(x: QuitButton.Location.X, y: QuitButton.Location.Y - BackButton.Height - 10);
            
            arrayExercises = (ExercisesType[])Enum.GetValues(typeof(ExercisesType));
            numberExercises = arrayExercises.Length - 1;
            
            InitMap();
        }

        private void InitMap()
        {
            var indentLeftEdge = 10;
            var width = 200;

            for (int i = 0; i < numberExercises; i++)
            {                
                if (i % 6 == 0)
                { 
                    indentLeftEdge += width;
                }

                var button = TrainCommon.CreateButton(this, indentLeftEdge, i % 6, width, 100, ExercisesTypeRus[i]);
                exercisesButtons[i] = button;
                button.Click += ExercisesButton_Click;
            }
        }
        private void HideOrShowAllMenuButtons()
        {
            menuMode = !menuMode;

            foreach (Button b in exercisesButtons)
            {
                b.Visible = !b.Visible;
            }
        }
        private void ClearExercises()
        {
            nameLabels.ForEach(x => Controls.Remove(x));
            loadLabels.ForEach(x => Controls.Remove(x));
            repeatLabels.ForEach(x => Controls.Remove(x));
            HideOrShowAllMenuButtons();
        }
        private void ExercisesButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var index = Array.IndexOf(exercisesButtons, button);
            
            HideOrShowAllMenuButtons();

            exercisesData = new List<Exercise>();
            nameLabels = new List<Label>();
            loadLabels = new List<Label>();
            repeatLabels = new List<Label>();

            pathExercisesPath = "ExercisesType/" + Convert.ToString(arrayExercises[index]) + ".json";
            exercisesData = TrainCommon.GetDeserializedData(pathExercisesPath);

            var row = 0;
            for (int i = 0; i < exercisesData.Count; i++, row++)
            {
                var nameLabel = TrainCommon.CreateLabel(this, 10, row, 650, 70, exercisesData[i].Name);
                nameLabels.Add(nameLabel);
                nameLabel.Click += NameLabel_Click;

                for (int j = 0; j < exercisesData[i].typesTrainingList.Count; j++, row++)
                {
                    var loadLabel = TrainCommon.CreateLabel(this, 685, row, 170, 70, exercisesData[i].typesTrainingList[j]["load"]);
                    loadLabels.Add(loadLabel);

                    if (exercisesData[i].typesTrainingList[j].ContainsKey("repeats"))
                    {
                        var repeatLabel= TrainCommon.CreateLabel(this, 880, row, 100, 70, exercisesData[i].typesTrainingList[j]["repeats"]);
                        repeatLabels.Add(repeatLabel);
                    }
                }
            }
        }
        private void NameLabel_Click(object sender, EventArgs e)
        {
            var nameLabel = sender as Label;
            var index = nameLabels.IndexOf(nameLabel);

            var remark = new MyMessageBox();
            remark.ShowText(exercisesData[index].Remark);
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
           if (menuMode) Close();
           else  ClearExercises();        
        }
        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }        
    }
}
