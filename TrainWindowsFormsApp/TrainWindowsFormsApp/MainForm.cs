using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TrainWindowsFormsApp.Properties;

namespace TrainWindowsFormsApp
{
    public partial class trainMainForm : Form
    {
        private readonly string pathToProgressFile = "progress.txt";
        // Свойства элементов управления
        private readonly int height = 60;           // Высота ЭУ
        private readonly int indentBetween = 10;    // Расстояние между ЭУ по горизонтали,
        private int indentUpEdge;                   // то же по вертикали.
        private List<int> increaseIndentUpEdge;
        // Списки лейблов/кнопок
        private Label[] labelsMap;                  // Все лейблы
        private Button[] exercisesChangeButtons;    // Кнопки для смены упражнений
        private Button[] repeatButtons;             // Кнопки выполнения упражнений и увеличения количества повторов на 1
        private Button[] megaPlusButtons;           // То же, только увеличение больше

        private MyMessageBox message;

        private Exercise[] exercises;   // Массив, содержащий все упражнения тренировки
        private int numberOfExercises;  // и их количество
                                        
        Tuple<string, float>[] modes = // Режим тренировки
        {
                new Tuple<string, float>("Нижние 1,5", 0.7f),
                new Tuple<string, float>("Убывающее удержание", 0.8f),
                new Tuple<string, float>("Всё и сразу", 5),
                new Tuple<string, float>("Обычный", 1),
                new Tuple<string, float>("Быстрый режим", 1.2f),
                new Tuple<string, float>("Верхние 1,5", 0.7f),
                new Tuple<string, float>("0,5 + 1 + 0,5", 0.6f)
            };
        private int selectedMode;       // индекс из списка модов

        private List<string> pathsList = new List<string>();  // Массив для хранения всех путей к файлам нужен для сохранения результатов в конце тренировки
        private int progress;

        private static Random random = new Random();

        private int timerCounter;
        // Для смены упражнения
        private List<Exercise> exerciseChangeList;  // Список упражнений из конкретного файла для смены упражнения
        private int indexInCurExL;  // Индекс упражнения в списке упражнений на тренировке
        private int indexInExChL;   // Индекс того же упражнения в файле
        private Button nextExerciseButton;  // Кнопка для перехода к следующему упражнению
        private Button closeExChButton;     // Кнопка закрытия режима смены упражнения

        public trainMainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var start = new StartForm();
            start.ShowDialog();

            progress = GetProgress();
            exercises = GetExercises();
            InitMap();
            GetMode();
            FillInTheTable();
            //var test = new SetNewProgramForm();
            //test.ShowDialog();
        }
        //
        // Клиентская область
        //
        private void InitMap()
        {   // Заполнение формы ячейками и кнопками
            numberOfExercises = exercises.Count();

            labelsMap = new Label[numberOfExercises * 2];   // Количество упражнений умноженное на количество лейблов
            exercisesChangeButtons = new Button[numberOfExercises];
            repeatButtons = new Button[numberOfExercises];
            megaPlusButtons = new Button[numberOfExercises];

            indentUpEdge = 60;

            for (int i = 0; i < numberOfExercises; i++)
            {   // Отступ между сетами упражнений
                // Надо будет потом доработать
                if (increaseIndentUpEdge.Contains(i)) indentUpEdge += 40;
                // Кнопка начала режима смены упражнения
                var exerciseChangeButton = CreateButton(10, i, 30, "⭯");
                exercisesChangeButtons[i] = exerciseChangeButton;
                exerciseChangeButton.Click += ExerciseChangeButton_Click;

                var textLabel = CreateLabel(50, i, 650);
                labelsMap[i] = textLabel;
                textLabel.MouseClick += ExerciseName_MouseClick;   // Событие нажатия на текст с названием упражнения

                var loadLabel = CreateLabel(725, i, 200);
                labelsMap[i + numberOfExercises] = loadLabel;

                var repeatButton = CreateButton(950, i, 100);
                repeatButtons[i] = repeatButton;
                repeatButton.Click += RepeatButton_Click;  // Событие нажатия на кнопкуx

                var megaPlusButton = CreateButton(1060, i, 50, "💣");
                megaPlusButtons[i] = megaPlusButton;
                megaPlusButton.Click += MegaPlusButton_Click;

            }
        }

        private void FillInTheTable()
        {   // Заполнение ячеек
            for (int i = 0; i < numberOfExercises; i++)
            {
                labelsMap[i].Text = exercises[i].Name;                      // название упражнения
                labelsMap[i + numberOfExercises].Text = exercises[i].Load;  // нагрузка
                repeatButtons[i].Text = ModeRepeat(exercises[i].Repeat);     // повторения

            }
        }

        private Button CreateButton(int indentLeftEdge, int indexRow, int width, string initialText = "")
        {   // Создание кнопок 
            int x = indentLeftEdge;
            int y = indentUpEdge + indexRow * (indentBetween + height);  // Формула расчёта координат эллемента по ординате

            var button = new Button
            {
                BackColor = Color.MediumOrchid,
                Font = new Font("Tahoma", 18F, FontStyle.Regular, GraphicsUnit.Point, 204),
                Text = initialText,
                Size = new Size(width, height),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(x, y)
            };
            Controls.Add(button);
            button.BringToFront();
            return button;
        }

        private Label CreateLabel(int indentLeftEdge, int indexRow, int width)
        {   // Создание ячеек
            int x = indentLeftEdge;
            int y = indentUpEdge + indexRow * (indentBetween + height); // Формула расчёта координат эллемента по ординате

            var label = new Label
            {
                BackColor = Color.LightSeaGreen,
                Font = new Font("Gabriola", 30F, FontStyle.Bold, GraphicsUnit.Point, 204),
                Size = new Size(width, height),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(x, y)
            };
            Controls.Add(label);
            label.BringToFront();
            return label;
        }

        private void GetMode()
        {   
            selectedMode = progress % modes.Count();

            var modeLabel = CreateLabel(350, numberOfExercises + 1, 600);
            modeLabel.Font = new Font("Segoe UI Black", 40F, FontStyle.Bold, GraphicsUnit.Point, 204);
            modeLabel.ForeColor = Color.DarkOrange;
            modeLabel.Height = 200;
            modeLabel.Text = modes[selectedMode].Item1;
        }

        private string ModeRepeat(int buttonText)
        {
            var num = (float)buttonText;
            num *= modes[selectedMode].Item2;
            num = (float)Math.Round(num);            

            return Convert.ToString(num);
        }
        //
        // Работа с файлами
        //
        private int GetProgress()
        {
            var isExistFile = FileProvider.TryGet(pathToProgressFile, out var data);

            if (!isExistFile)
            {
                FileProvider.Save(pathToProgressFile, "1");
                data = "1";
            }
            return int.Parse(data);
        }

        private void SaveProgress()
        {
            progress++;
            var data = progress.ToString();

            FileProvider.Save(pathToProgressFile, data);
        }

        private void SaveTrainResults()
        {
            for (int i = 0; i < pathsList.Count; i++)
            {
                var deserializableData = GetDeserializedData(pathsList[i]);

                for (int j = 0; j < exercises.Length; j++)
                {   // Здесь сравнение количества повторов с максимальным количеством
                    if (exercises[j].Repeat > exercises[j].MaxRepeat)
                    {
                        var form = new SetNewLoadForm(exercises[j]);
                        form.ShowDialog();
                        exercises[j].Repeat = 10;
                        exercises[j].Load = form.NewLoad;
                    }

                    foreach (var exerc in deserializableData)
                    {
                        if (exerc.Name == exercises[j].Name)
                        {
                            exerc.Repeat = exercises[j].Repeat;
                            exerc.Load = exercises[j].Load;
                        }
                    }
                }
                var serializableData = JsonConvert.SerializeObject(deserializableData, Formatting.Indented);
                FileProvider.Save(pathsList[i], serializableData);
            }
        }

        private Exercise[] GetExercises(string option = "train")
        {   // Получаем список тренируемых мышц
            List<ExercisesType> exercisesList;

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

        private List<Exercise> GetDeserializedData(string path)
        {   // Получение данных из файла
            var dataExercises = FileProvider.GetData(path);
            // и десериализация в список.
            var deserializableDataExercises = JsonConvert.DeserializeObject<List<Exercise>>(dataExercises);

            return deserializableDataExercises;
        }
        //
        // События
        //
        private void ExerciseName_MouseClick(object sender, MouseEventArgs e)
        {   // Показ примечания к упражнению
            message = new MyMessageBox();
            var index = Array.IndexOf(labelsMap, sender); // Получаем индекс лейбла, на который нажали
            message.ShowText(exercises[index].Remark);     // и выводим примечание к упражнению по полученному индексу.            
        }

        private void ExerciseChangeButton_Click(object sender, EventArgs e)
        {
            if (Controls.Contains(nextExerciseButton) || Controls.Contains(closeExChButton))
            {
                Controls.Remove(nextExerciseButton);
                Controls.Remove(closeExChButton);
                foreach (var butt in exercisesChangeButtons)
                {
                    butt.Enabled = true;
                    butt.Visible = true;
                } 
            }

            // Нажати кнопки смены упражнения
            var button = (sender as Button);
            button.Enabled = false;
            button.Visible = false;

            indexInCurExL = Array.IndexOf(exercisesChangeButtons, button);
            // По следующей строке будем искать в файлах упражнение
            var currentExercise = exercises[indexInCurExL];

            foreach (var path in pathsList)
            {
                var found = false;

                exerciseChangeList = GetDeserializedData(path);                
                for (indexInExChL = 0; indexInExChL < exerciseChangeList.Count(); indexInExChL++)
                {
                    if (exerciseChangeList[indexInExChL].Name == currentExercise.Name)
                    {
                        found = true;
                        break;
                    }
                }
                if (found) break;
            }

            nextExerciseButton = CreateButton(0, 0, button.Bounds.Width, ">");
            nextExerciseButton.Location = button.Location;
            nextExerciseButton.Height = button.Bounds.Height / 2;
            nextExerciseButton.Click += NextExerciseButton_Click;

            closeExChButton = CreateButton(0, 0, button.Bounds.Width, "X");
            closeExChButton.Location = new Point(button.Bounds.X, button.Bounds.Y + 30);
            closeExChButton.Height = button.Bounds.Height / 2;
            closeExChButton.Click += CloseModeChangeExercise_Click;

        }

        private void NextExerciseButton_Click(object sender, EventArgs e)
        {
            indexInExChL++;
            if (indexInExChL >= exerciseChangeList.Count())
            {
                indexInExChL = 0;
            }

            labelsMap[indexInCurExL].Text = exerciseChangeList[indexInExChL].Name;
            labelsMap[indexInCurExL + numberOfExercises].Text = exerciseChangeList[indexInExChL].Load.ToString();
            repeatButtons[indexInCurExL].Text = ModeRepeat(exerciseChangeList[indexInExChL].Repeat);
        }

        private void CloseModeChangeExercise_Click(object sender, EventArgs e)
        {
            exercises[indexInCurExL] = exerciseChangeList[indexInExChL];

            Controls.Remove(nextExerciseButton);
            Controls.Remove(closeExChButton);

            exercisesChangeButtons[indexInCurExL].Visible = true;
            exercisesChangeButtons[indexInCurExL].Enabled = true;
        }

        private void RepeatButton_Click(object sender, EventArgs e)
        {   // Нажатие на кнопку выполнения упражнения
            var doneButton = (sender as Button);        // Обращается к кнопке,
            doneButton.BackColor = Color.ForestGreen;   // меняет окраску кнопки.
            doneButton.Enabled = false;                 // Отключение кнопки после нажатия.
                                      
            var index = Array.IndexOf(repeatButtons, doneButton);              // Получаем индекс кнопки в её специальном массиве,
            exercises[index].Repeat++;                                       // меняем значение числа повторов.

            var megaButton = megaPlusButtons[index];
            megaButton.BackColor = Color.Gray;
            megaButton.Text = "❌";      
            megaButton.Enabled = false;

            // Если количество изменённых повторов стало больше максимально допустимого пишем "МАХ",
            if (exercises[index].Repeat > exercises[index].MaxRepeat) doneButton.Text = "MAX";
            // если нет - то меняем на новое значение.
            else doneButton.Text = "✓";
        }

        private void MegaPlusButton_Click(object sender, EventArgs e)
        {   // Нажатие на кнопку 
            var megaButton = (sender as Button);     // Обращается к кнопке,
            megaButton.BackColor = Color.Gold;       // меняет окраску кнопки
            megaButton.Text = "💥";                 // и текст на ней.
            megaButton.Enabled = false;             // Отключение кнопки после нажатия.

            var index = Array.IndexOf(megaPlusButtons, megaButton); // Получаем индекс кнопки в её специальном массиве,
            var megaRepeat = exercises[index].MaxRepeat / 10 + 1;   // получаем большее число чем 1,
            exercises[index].Repeat += megaRepeat;                  // и меняем значение числа повторов.

            // Отключение обычных кнопок
            var doneButton = repeatButtons[index];
            doneButton.BackColor = Color.ForestGreen;
            doneButton.Enabled = false;

            // Если количество изменённых повторов стало больше максимально допустимого пишем "МАХ",
            if (exercises[index].Repeat > exercises[index].MaxRepeat) doneButton.Text = "MAX";
            // если нет - то меняем на новое значение.
            else doneButton.Text = "✓";
        }

        private void разминкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exercise[] warmUp = GetExercises("warmUp");
            var wF = new WarmUpForm(warmUp, this);
            wF.GetWarmUp();
        }

        private void заминкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exercise[] warmUp = GetExercises("warmUp");
            var wF = new WarmUpForm(warmUp, this);
            wF.GetHitch();
        }

        private void мнеНехерДелатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveTrainResults();
            Hide();
            var fastTrain = new FastTrainForm();
            fastTrain.ShowDialog();
        }

        private void создатьНовуюПрограммуТренировокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newProgrForm = new SetNewProgramForm();
            newProgrForm.ShowDialog();
        }

        private void сохранитьНовоеУпражнениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveExerc = new SaveNewExerciseForm();
            saveExerc.Show();
        }

        private void закончитьТренировкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProgress();
            SaveTrainResults();
            Close();
        }

        private void выходБезСохраненияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
