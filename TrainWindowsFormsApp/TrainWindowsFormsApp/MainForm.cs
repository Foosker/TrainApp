using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TrainWindowsFormsApp
{
    public partial class MainForm : Form
    {
        // Списки лейблов/кнопок
        private Label[] labelsMap;                  // Все лейблы
        // private Button[] exercisesChangeButtons;    // Кнопки для смены упражнений
        private TextBox[] repeatTextBoxes;          // ТекстБокс для повторов
        //private Button[] repeatButtons;             // Кнопки выполнения упражнений и увеличения количества повторов на 1
        private Button[] megaPlusButtons;           // То же, только увеличение больше

        private MyMessageBox message;

        private List<Dictionary<string, string>> exercises;   // Массив, содержащий все упражнения тренировки
        private int numberOfExercises;  // и их количество

        private static Color[] labelsColors =
        {
            Color.OrangeRed,
            Color.CadetBlue
        };

        readonly static Tuple<double, double>[] loadAndRepeatsModsList =
        {
            new Tuple<double, double>(0.2, 1.5),
            new Tuple<double, double>(0.3, 1),
            new Tuple<double, double>(0.7, 0.7),
            new Tuple<double, double>(1.2, 0.3),  // Этот и предыдущий подход - разминочные
            new Tuple<double, double>(1, 1),
            new Tuple<double, double>(1, 0.8),
            new Tuple<double, double>(1, 0.7),
            new Tuple<double, double>(1, 0.6),
            new Tuple<double, double>(1, 0.5)
        };
        readonly static Tuple<string, float>[] modes = // Режим тренировки
        {
                new Tuple<string, float>("Нижние 1,5", 0.8f),
                new Tuple<string, float>("Верхние 1,5", 0.7f),
                new Tuple<string, float>("0,5 + 1 + 0,5", 0.6f),
                new Tuple<string, float>("Нижние очерёдные 1.5", 0.6f),
                new Tuple<string, float>("Верхние очерёдные 1.5", 0.5f),
                new Tuple<string, float>("Очерёдные 1.5", 0.4f),
                new Tuple<string, float>("Всё сразу", 4)
        };
        private readonly int selectedMode = TrainCommon.progress % modes.Count();       // индекс из списка модов
        public bool isRegular;  // Обычная ли тренировка
        // Для смены упражнения
        //private List<Exercise> exerciseChangeList;  // Список упражнений из конкретного файла для смены упражнения
        //private int indexInCurExL;  // Индекс упражнения в списке упражнений на тренировке
        //private int indexInExChL;   // Индекс того же упражнения в файле
        //private Button nextExerciseButton;  // Кнопка для перехода к следующему упражнению
        //private Button closeExChButton;     // Кнопка закрытия режима смены упражнения
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (isRegular) exercises = TrainDay.GetTrain();            
            else exercises = TrainDay.GetAdvancedTrain();            
            InitMap();
            backgroundPictureBox.SendToBack();
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
            //exercisesChangeButtons = new Button[numberOfExercises];
            repeatTextBoxes = new TextBox[numberOfExercises];
            //repeatButtons = new Button[numberOfExercises];
            megaPlusButtons = new Button[numberOfExercises];

            for (int i = 0; i < numberOfExercises; i++)
            {   // Отступ между сетами упражнений
                if (TrainDay.indentBetweenExercises.Contains(i)) TrainCommon.indentUpEdge += 100;
                // Кнопка начала режима смены упражнения
                /*var exerciseChangeButton = TrainCommon.CreateButton(this, 10, i, 30, "⭯");
                exercisesChangeButtons[i] = exerciseChangeButton;
                exerciseChangeButton.Click += ExerciseChangeButton_Click;
                */
                var textLabel = TrainCommon.CreateLabel(this, 50, i + 0.5f, 650, 70, exercises[i]["name"]);
                textLabel.BackColor = labelsColors[i % labelsColors.Length];
                labelsMap[i] = textLabel;
                textLabel.MouseClick += ExerciseName_MouseClick;   // Событие нажатия на текст с названием упражнения
                for (int j = 0; ; j++)
                {
                    var loadAndRepeat = LoadAndRepeatsMod(exercises[i]["load"], RoundNumber(exercises[i]["repeats"]), j);
                    var loadLabel = TrainCommon.CreateLabel(this, 705 + (180 * j), i, 170, 70, loadAndRepeat[0]);
                    loadLabel.BackColor = labelsColors[i % labelsColors.Length];
                    loadLabel.Click += LoadLabel_Click;

                    var repeatLabel = TrainCommon.CreateLabel(this, 705 + (180 * j), i + 1, 170, 70, loadAndRepeat[1]);
                    repeatLabel.BackColor = labelsColors[i % labelsColors.Length];
                    if (j == 7)
                    {
                        loadAndRepeat = LoadAndRepeatsMod(exercises[i]["load"], RoundNumber(exercises[i]["repeats"]), ++j);

                        loadLabel = TrainCommon.CreateLabel(this, 705 + (180 * j), i, 170, 70, loadAndRepeat[0]);
                        loadLabel.BackColor = labelsColors[i % labelsColors.Length];

                        var repeatTextBox = TrainCommon.CreateTextBox(this, 705 + (180 * j), i + 1, 170, 70, loadAndRepeat[1]);
                        repeatTextBoxes[i] = repeatTextBox;

                        var megaPlusButton = TrainCommon.CreateButton(this, 705 + (180 * ++j), i + 0.5f, 50, 70, "🔥");
                        megaPlusButtons[i] = megaPlusButton;
                        megaPlusButton.Click += MegaPlusButton_Click;   // Событие нажатие на кнопку мега-увеличения повторов
                        break;
                    }
                }
            }
            TrainCommon.indentUpEdge = 25;
        }
        private string[] LoadAndRepeatsMod(string load, string repeats, int modIndex)
        {
            string[] result = new string[2];
            if (loadAndRepeatsModsList[modIndex].Item1 == 1) result[0] = load;
            else if (load[0] == '0')
            {
                if (modIndex <= 2) result[0] = "Разминка";
                else if (modIndex == 3) result[0] = "2";
                else if (modIndex == 6) result[0] = "1.5";
                else result[0] = "0";
            }
            else if (load.Contains('+') &&                                              // Eсли в строке нагрузки есть плюс и
                Double.TryParse(load.Substring(0, load.IndexOf('+')), out var loadNum)) // можно преобразовать в число с точкой, то преобразуем и записываем в loadNum
            {
                var newLoad = loadNum * loadAndRepeatsModsList[modIndex].Item1;
                foreach (double numDouble in TrainCommon.loadNumber)
                {
                    if (numDouble >= newLoad & newLoad != loadNum)
                    {
                        result[0] = numDouble.ToString() + load.Substring(load.IndexOf('+'), load.Length - load.IndexOf('+'));
                        break;
                    }
                }
            }
            else if(Double.TryParse(load, out loadNum))                         // Если нагрузка состоит только из цифр, то преобразуем
            {
                loadNum *= loadAndRepeatsModsList[modIndex].Item1;
                foreach (double numDouble in TrainCommon.loadNumber)
                {
                    if (numDouble >= loadNum)
                    {
                        result[0] = numDouble.ToString();
                        break;
                    }
                }
            }
            else result[0] = "Разминка";
            result[1] = RoundNumber(repeats, loadAndRepeatsModsList[modIndex].Item2);
            return result;
        }
        private string RoundNumber(string repeats, double mod = 1)
        {// Сначала конвертирует строку в число с точкой, потом округляет до ближайшего целого и конвертирует в строку
            return Convert.ToString(Math.Ceiling(Convert.ToDouble(repeats) * mod));
        }
        private void GetMode()
        {   
            var modeLabel = TrainCommon.CreateLabel(this, 350, numberOfExercises + 1, 600, 70);
            modeLabel.Font = new Font("Segoe UI Black", 30F, FontStyle.Bold, GraphicsUnit.Point, 204);
            modeLabel.ForeColor = Color.Black;
            modeLabel.Height = 100;
            modeLabel.Text = modes[selectedMode].Item1;
            modeLabel.Location = new Point(ClientSize.Width / 2 - modeLabel.Width / 2, ClientSize.Height);
            modeLabel.BringToFront();
        }
        private string ModeRepeat(string buttonText)
        {
            var num = Convert.ToDouble(buttonText);
            num *= modes[selectedMode].Item2;
            num = Math.Floor(num);

            return Convert.ToString(num);
        }
        //
        // События
        //
        private void ExerciseName_MouseClick(object sender, MouseEventArgs e)
        {   // Показ примечания к упражнению
            message = new MyMessageBox();
            var index = Array.IndexOf(labelsMap, sender); // Получаем индекс лейбла, на который нажали
            message.ShowText(exercises[index]["remark"]);     // и выводим примечание к упражнению по полученному индексу.            
        }
        /*
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

            foreach (var path in TrainCommon.pathsList)
            {
                var found = false;

                exerciseChangeList = TrainCommon.GetDeserializedData(path);                
                for (indexInExChL = 0; indexInExChL < exerciseChangeList.Count(); indexInExChL++)
                {
                    if (exerciseChangeList[indexInExChL].Name == currentExercise["name"])
                    {
                        found = true;
                        break;
                    }
                }
                if (found) break;
            }

            nextExerciseButton = TrainCommon.CreateButton(this, 0, 0, button.Bounds.Width, ">");
            nextExerciseButton.Location = button.Location;
            nextExerciseButton.Height = button.Bounds.Height / 2;
            nextExerciseButton.Click += NextExerciseButton_Click;

            closeExChButton = TrainCommon.CreateButton(this, 0, 0, button.Bounds.Width, "X");
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

            if (TrainCommon.option == "strength")
            {
                labelsMap[indexInCurExL + numberOfExercises].Text = exerciseChangeList[indexInExChL].Strength["load"].ToString();
                repeatButtons[indexInCurExL].Text = ModeRepeat(exerciseChangeList[indexInExChL].Strength["repeats"]);
            }
            else if (TrainCommon.option == "stamina")
            {
                labelsMap[indexInCurExL + numberOfExercises].Text = exerciseChangeList[indexInExChL].Stamina["load"].ToString();
                repeatButtons[indexInCurExL].Text = ModeRepeat(exerciseChangeList[indexInExChL].Stamina["repeats"]);
            }
            else
            {
                labelsMap[indexInCurExL + numberOfExercises].Text = exerciseChangeList[indexInExChL].Tabata["load"].ToString();
            }
        }
        private void CloseModeChangeExercise_Click(object sender, EventArgs e)
        {
            exercises[indexInCurExL] = exerciseChangeList[indexInExChL];

            Controls.Remove(nextExerciseButton);
            Controls.Remove(closeExChButton);

            exercisesChangeButtons[indexInCurExL].Visible = true;
            exercisesChangeButtons[indexInCurExL].Enabled = true;
        }
        */
        private void LoadLabel_Click(object sender, EventArgs e)
        {// Нажатие на лейбл нагрузки
            var label = (Label)sender;
            TrainCommon.DarkenControlElement(label);                                    // Затемнение лейблов нагрузки
            TrainCommon.DarkenControlElement(Controls[Controls.IndexOf(label) + 1]);    // и повторов
        }/*
        private void RepeatButton_Click(object sender, EventArgs e)
        {   // Нажатие на кнопку выполнения упражнения
            var doneButton = (sender as Button);        // Обращается к кнопке,
            doneButton.BackColor = Color.ForestGreen;   // меняет окраску кнопки.
            doneButton.Enabled = false;                 // Отключение кнопки после нажатия.

            var index = Array.IndexOf(repeatButtons, doneButton); // Получаем индекс кнопки в её специальном массиве,
            var newValue = Convert.ToDouble(exercises[index]["repeats"]) + 1;   // Увеличиваем количество повторений

            if ((exercises[index]["typeTrain"] == "Strength" && newValue > Convert.ToDouble(exercises[index]["maxRepeats"]) / 2)
                || newValue > Convert.ToDouble(exercises[index]["maxRepeats"]))
            {// Если новое количество равняется максимуму
                if (exercises[index]["typeTrain"] == "Strength") newValue = Convert.ToDouble(exercises[index]["maxRepeats"]) / 4;
                else newValue = Convert.ToDouble(exercises[index]["maxRepeats"]) / 2;
                exercises[index]["load"] = TrainCommon.LoadIncrease(exercises[index]["load"]);
            }
            exercises[index]["repeats"] = Convert.ToString(newValue);           // и записываем
            index = Controls.IndexOf(doneButton);
            for (int i = 1; i <= 14; i++)
            {
                TrainCommon.DarkenControlElement(Controls[index - i]);
            }
        }*/
        private void MegaPlusButton_Click(object sender, EventArgs e)
        {   // Нажатие на кнопку 
            var megaPlusButton = (sender as Button);        // Обращается к кнопке,
            megaPlusButton.BackColor = Color.ForestGreen;   // меняет окраску кнопки.
            megaPlusButton.Enabled = false;                 // Отключение кнопки после нажатия.

            var index = Array.IndexOf(megaPlusButtons, megaPlusButton); // Получаем индекс кнопки в её специальном массиве,
            // меняем значение числа повторов:
            // старое значение умножаем на 3, из нового значения убираем модификатор мода, складываем их, затем делим на 4
            var oldValue = Convert.ToDouble(exercises[index]["repeats"]);
            var lastRepeatsValue = Convert.ToDouble(repeatTextBoxes[index].Text);
            double newValue;
            if (lastRepeatsValue > oldValue) newValue = lastRepeatsValue;
            else newValue = Math.Round(oldValue + (lastRepeatsValue / Math.Ceiling(oldValue * loadAndRepeatsModsList[8].Item2)), 1);
            // Если количество изменённых повторов стало больше максимально допустимого пишем "МАХ",
            if ((exercises[index]["typeTrain"] == "Strength" && newValue > Convert.ToDouble(exercises[index]["maxRepeats"]) / 2)
                || newValue > Convert.ToDouble(exercises[index]["maxRepeats"]))
            {
                repeatTextBoxes[index].Text = "MAX";
                if (exercises[index]["typeTrain"] == "Strength") newValue = 5;
                else newValue = 10;
                exercises[index]["load"] = TrainCommon.LoadIncrease(exercises[index]["load"]);
            }
            // если нет - то меняем на новое значение.
            else megaPlusButton.Text = "✓";
            // вписываем новое значение повторов в exercises
            exercises[index]["repeats"] = Convert.ToString(newValue);
            repeatTextBoxes[index].Enabled = false;

            TrainCommon.DarkenControlElement(labelsMap[index]);
            index = Controls.IndexOf(megaPlusButton);
            for (int i = 1; i <= 19; i++)
            {
                TrainCommon.DarkenControlElement(Controls[index - i]);
            }

        }
        private void ДобавитьМодификациюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetMode();
            foreach (var textBox in repeatTextBoxes)
            {
                textBox.Text = ModeRepeat(textBox.Text);
            }
        }
        private void РазминкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var warmUp = TrainDay.GetWarmUpList();
            var wF = new WarmUpForm(warmUp, this);
            wF.GetWarmUp();
        }
        private void ЗаминкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var warmUp = TrainDay.GetWarmUpList();
            var wF = new WarmUpForm(warmUp, this);
            wF.GetHitch();
        }
        private void СоздатьНовуюПрограммуТренировокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newProgrForm = new SetNewProgramForm();
            newProgrForm.ShowDialog();
        }
        private void ЗакончитьТренировкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrainCommon.SaveTrainResults(exercises);
            TrainCommon.SaveProgress();
            Close();
        }
        private void ВыходБезСохраненияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
