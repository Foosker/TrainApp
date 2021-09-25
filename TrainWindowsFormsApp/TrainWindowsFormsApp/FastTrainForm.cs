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
        "Лопатки",
        "Трапеции",
        "Передние дельты",
        "Средние дельты",
        "Задние дельты",
        "Бицепсы",
        "Трицепсы",
        "Предплечья",
        "Квадрицепсы",
        "Быцепсы бедра",
        "Пресс",
        "Разгиб. спины",
        "Икры",
        "Голень"
        };

        // Свойства элементов управления
        private int height = 60;
        private int indentBetween = 75;
        private int y;

        private string pathExercisesPath;

        private List<Exercise> exercisesData;
        private Button[] exercisesButtons = new Button[19];

        private List<Label> nameLabels;         // Лейблы с именами упражнений,
        private List<Label> loadLabels;         // лейблы с нагрузкой - нужны для удаления из Controls,
        private List<Button> repeatButtons;     // кнопки с повторениями - увеличенивают повторов на 1,
        private List<Button> megaPlusButtons;   // кнопки с увеличением повторов на 2.

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
            // Кнопка Сохранить: тоже самое относительно кнопки Назад.
            SaveButton.Location = new Point(x: BackButton.Location.X, y: BackButton.Location.Y - SaveButton.Height - 10);
            SaveButton.Visible = false;  // Отключение кнопки сохранить пока не будет выбрана группа мышц
            // Кнопка Увеличения прогресса: таже херня, относительно предыдущей кнопки
            ProgressPlusButton.Location = new Point(x: SaveButton.Location.X, y: SaveButton.Location.Y - ProgressPlusButton.Height - 10);

            arrayExercises = (ExercisesType[])Enum.GetValues(typeof(ExercisesType));
            numberExercises = arrayExercises.Length;

            InitMap();
            ShowNextTrain();
        }

        private void InitMap()
        {
            var indentLeftEdge = 10;
            var width = 150;

            for (int i = 0; i < numberExercises; i++)
            {
                y = indentBetween + i % 6 * height;  // Формула расчёта координат эллемента по ординате
                
                if (i % 6 == 0)
                { 
                    indentLeftEdge += width;
                }

                var button = TrainCommon.CreateButton(this, indentLeftEdge, i % 6, width, ExercisesTypeRus[i]);
                exercisesButtons[i] = button;
                button.Click += ExercisesButton_Click;
            }
        }

        private void ShowNextTrain()
        {
            // Сначала получает массив мышечных групп на следующей тренировке, потом вибирает уникальные эллементы массива
            var nextTrain = TrainDay.GetTrain(TrainCommon.GetProgress()).Distinct().ToArray();
            // Список с индексами мышц, что будут тренироваться на следующей тренировке, в массиве мышц
            List<int> indexesMuscles = new List<int>();

            foreach (ExercisesType muscle in nextTrain)
            {
                // Находим индекс мышцы в массиве
                var index = Array.IndexOf(arrayExercises, muscle);
                // Добавляем в список индексов
                indexesMuscles.Add(index);
            }
            // Сортируем список индексов по возрастанию
            indexesMuscles.Sort();
            // От минимального значения индекса до максимального
            for (int i = indexesMuscles.Min(); i <= indexesMuscles.Max(); i++)
            {
                if (indexesMuscles.Contains(i))
                {   // Если i есть в списке индексов, то меняем цвет кнопки по этому же индексу
                    exercisesButtons[i].BackColor = Color.DeepSkyBlue;
                }
            }
        }

        private void HideOrShowAllMenuButtons()
        {
            menuMode = !menuMode;
            SaveButton.Visible = !SaveButton.Visible;  

            foreach (Button b in exercisesButtons)
            {
                b.Visible = !b.Visible;
            }
        }

        private void ClearExercises()
        {
            for (int i = 0; i < nameLabels.Count; i++)
            {
                Controls.Remove(nameLabels[i]);
                Controls.Remove(loadLabels[i]);
                Controls.Remove(repeatButtons[i]);
                Controls.Remove(megaPlusButtons[i]);
            }
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
            repeatButtons = new List<Button>();
            megaPlusButtons = new List<Button>();

            pathExercisesPath = "ExercisesType/" + Convert.ToString(arrayExercises[index]) + ".json";
            exercisesData = TrainCommon.GetDeserializedData(pathExercisesPath);

            height = 90;
            indentBetween = 25;

            for (int i = 0; i < exercisesData.Count; i++)
            {
                y = 25 + i * (indentBetween + height); // Формула расчёта координат эллемента по ординате

                var nameLabel = TrainCommon.CreateLabel(this, 10, i, 500, exercisesData[i].Name);
                nameLabels.Add(nameLabel);
                nameLabel.Click += NameLabel_Click;

                var loadLabel = TrainCommon.CreateLabel(this, 520, i, 200, exercisesData[i].Load);
                loadLabels.Add(loadLabel);

                var repeatButton = TrainCommon.CreateButton(this, 730, i, 50, Convert.ToString(exercisesData[i].Repeat));
                repeatButton.Font = new Font("Bahnschrift", 20F, FontStyle.Regular, GraphicsUnit.Point, 204);
                repeatButtons.Add(repeatButton);
                repeatButton.Click += RepeatButton_Click;

                var megaPlusButton = TrainCommon.CreateButton(this, 790, i, 50, "💣");
                megaPlusButtons.Add(megaPlusButton);
                megaPlusButton.Click += MegaPlusButton_Click;
            }
        }

        private void NameLabel_Click(object sender, EventArgs e)
        {
            var nameLabel = sender as Label;
            var index = nameLabels.IndexOf(nameLabel);

            var remark = new MyMessageBox();
            remark.ShowText(exercisesData[index].Remark);
        }

        private void RepeatButton_Click(object sender, EventArgs e)
        {
            var repeatButton = sender as Button;
            repeatButton.Text = "✓";
            repeatButton.BackColor = Color.GreenYellow;
            repeatButton.Enabled = false;

            var index = repeatButtons.IndexOf(repeatButton);

            var megaPlusButton = megaPlusButtons[index];
            megaPlusButton.Text = "❌";
            megaPlusButton.BackColor = Color.IndianRed;
            megaPlusButton.Enabled = false;


            exercisesData[index].Repeat++;
        }

        private void MegaPlusButton_Click(object sender, EventArgs e)
        {

            var megaPlusButton = sender as Button;
            megaPlusButton.Text = "💥";
            megaPlusButton.BackColor = Color.Gold;
            megaPlusButton.Enabled = false;

            var index = megaPlusButtons.IndexOf(megaPlusButton);

            var repeatButton = repeatButtons[index];
            repeatButton.Text = "✓";
            repeatButton.BackColor = Color.Gold;
            repeatButton.Enabled = false;

            exercisesData[index].Repeat += 2;
        }

        private void ProgressPlusButton_Click(object sender, EventArgs e)
        {
            foreach (Button button in exercisesButtons)
            {
                if (button.BackColor != Color.MediumOrchid) button.BackColor = Color.MediumOrchid;
            }
            TrainCommon.SaveProgress();
            ShowNextTrain();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var serializedData = JsonConvert.SerializeObject(exercisesData, Formatting.Indented);

            FileProvider.Save(pathExercisesPath, serializedData);

            ClearExercises();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (menuMode)
            {
                Close();
            }
            else
            {
                ClearExercises();
            }
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
