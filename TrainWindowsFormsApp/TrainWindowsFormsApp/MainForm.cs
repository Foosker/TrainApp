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
        private Button[] exercisesChangeButtons;    // Кнопки для смены упражнений
        private Button[] repeatButtons;             // Кнопки выполнения упражнений и увеличения количества повторов на 1
        private Button[] megaPlusButtons;           // То же, только увеличение больше

        private MyMessageBox message;

        private Exercise[] exercises;   // Массив, содержащий все упражнения тренировки
        private int numberOfExercises;  // и их количество
                                        
        Tuple<string, float>[] modes = // Режим тренировки
        {
                new Tuple<string, float>("Нижние 1,5", 0.8f),
                new Tuple<string, float>("Обычный", 1),
                new Tuple<string, float>("Всё и сразу", 5),
                new Tuple<string, float>("Быстрый режим", 1.2f),
                new Tuple<string, float>("Обычный", 1),
                new Tuple<string, float>("Верхние 1,5", 0.7f),
                new Tuple<string, float>("0,5 + 1 + 0,5", 0.6f)
        };

        private int selectedMode;       // индекс из списка модов

        // Для смены упражнения
        private List<Exercise> exerciseChangeList;  // Список упражнений из конкретного файла для смены упражнения
        private int indexInCurExL;  // Индекс упражнения в списке упражнений на тренировке
        private int indexInExChL;   // Индекс того же упражнения в файле
        private Button nextExerciseButton;  // Кнопка для перехода к следующему упражнению
        private Button closeExChButton;     // Кнопка закрытия режима смены упражнения

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var start = new StartForm();
            start.ShowDialog();

            exercises = TrainCommon.GetExercises();
            InitMap();
            GetMode();
            FillInTheTable();
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
            exercisesChangeButtons = new Button[numberOfExercises];
            repeatButtons = new Button[numberOfExercises];
            megaPlusButtons = new Button[numberOfExercises];

            for (int i = 0; i < numberOfExercises; i++)
            {   // Отступ между сетами упражнений
                if (TrainCommon.increaseIndentUpEdge.Contains(i)) TrainCommon.indentUpEdge += 40;
                // Кнопка начала режима смены упражнения
                var exerciseChangeButton = TrainCommon.CreateButton(this, 10, i, 30, "⭯");
                exercisesChangeButtons[i] = exerciseChangeButton;
                exerciseChangeButton.Click += ExerciseChangeButton_Click;

                var textLabel = TrainCommon.CreateLabel(this, 50, i, 650);
                labelsMap[i] = textLabel;
                textLabel.MouseClick += ExerciseName_MouseClick;   // Событие нажатия на текст с названием упражнения

                var loadLabel = TrainCommon.CreateLabel(this, 725, i, 200);
                labelsMap[i + numberOfExercises] = loadLabel;

                if (TrainCommon.option != "tabata")
                {
                    var repeatButton = TrainCommon.CreateButton(this, 950, i, 100);
                    repeatButtons[i] = repeatButton;
                    repeatButton.Click += RepeatButton_Click;  // Событие нажатия на кнопкуx

                    var megaPlusButton = TrainCommon.CreateButton(this, 1060, i, 50, "💣");
                    megaPlusButtons[i] = megaPlusButton;
                    megaPlusButton.Click += MegaPlusButton_Click;
                }
            }
        }

        private void FillInTheTable()
        {   // Заполнение ячеек
            if (TrainCommon.option == "strength")
            {   // Для тренировки на силу
                for (int i = 0; i < numberOfExercises; i++)
                {
                    labelsMap[i].Text = exercises[i].Name;                              // название упражнения
                    labelsMap[i + numberOfExercises].Text = exercises[i].StrengthLoad;  // нагрузка
                    repeatButtons[i].Text = ModeRepeat(exercises[i].StrengthRepeat);    // повторения
                }
            }
            else if (TrainCommon.option == "stamina")            
            {   // Для тренировки на выносливость
                for (int i = 0; i < numberOfExercises; i++)
                    {
                    labelsMap[i].Text = exercises[i].Name;                              // название упражнения
                    labelsMap[i + numberOfExercises].Text = exercises[i].StaminaLoad;   // нагрузка
                    repeatButtons[i].Text = ModeRepeat(exercises[i].StaminaRepeat);     // повторения
                }
            }
            else
            {   // Для табата-тренировки
                for (int i = 0; i < numberOfExercises; i++)
                {
                    labelsMap[i].Text = exercises[i].Name;                              // название упражнения
                    labelsMap[i + numberOfExercises].Text = exercises[i].TabataLoad;    // нагрузка
                    repeatButtons[i].Text = "Tabata";                                   // повторения
                }
            }
        }
        
        private void GetMode()
        {   
            selectedMode = TrainCommon.progress % modes.Count();

            var modeLabel = TrainCommon.CreateLabel(this, 350, numberOfExercises + 1, 600);
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

            foreach (var path in TrainCommon.pathsList)
            {
                var found = false;

                exerciseChangeList = TrainCommon.GetDeserializedData(path);                
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
                labelsMap[indexInCurExL + numberOfExercises].Text = exerciseChangeList[indexInExChL].StrengthLoad.ToString();
                repeatButtons[indexInCurExL].Text = ModeRepeat(exerciseChangeList[indexInExChL].StrengthRepeat);
            }
            else if (TrainCommon.option == "stamina")
            {
                labelsMap[indexInCurExL + numberOfExercises].Text = exerciseChangeList[indexInExChL].StaminaLoad.ToString();
                repeatButtons[indexInCurExL].Text = ModeRepeat(exerciseChangeList[indexInExChL].StaminaRepeat);
            }
            else
            {
                labelsMap[indexInCurExL + numberOfExercises].Text = exerciseChangeList[indexInExChL].TabataLoad.ToString();
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

        private void RepeatButton_Click(object sender, EventArgs e)
        {   // Нажатие на кнопку выполнения упражнения
            var doneButton = (sender as Button);        // Обращается к кнопке,
            doneButton.BackColor = Color.ForestGreen;   // меняет окраску кнопки.
            doneButton.Enabled = false;                 // Отключение кнопки после нажатия.
                                      
            var index = Array.IndexOf(repeatButtons, doneButton);              // Получаем индекс кнопки в её специальном массиве,
            
            // меняем значение числа повторов.
            if (TrainCommon.option == "strength")
            {
                exercises[index].StrengthRepeat++;
                // Если количество изменённых повторов стало больше максимально допустимого пишем "МАХ",
                if (exercises[index].StrengthRepeat > exercises[index].MaxRepeat / 2) doneButton.Text = "MAX";
                // если нет - то меняем на новое значение.
                else doneButton.Text = "✓";
            }
            else
            {
                exercises[index].StaminaRepeat++;
                // Если количество изменённых повторов стало больше максимально допустимого пишем "МАХ",
                if (exercises[index].StaminaRepeat > exercises[index].MaxRepeat) doneButton.Text = "MAX";
                // если нет - то меняем на новое значение.
                else doneButton.Text = "✓";
            }

            var megaButton = megaPlusButtons[index];
            megaButton.BackColor = Color.Gray;
            megaButton.Text = "❌";
            megaButton.Enabled = false;
        }

        private void MegaPlusButton_Click(object sender, EventArgs e)
        {   // Нажатие на кнопку 
            var megaButton = (sender as Button);     // Обращается к кнопке,
            megaButton.BackColor = Color.Gold;       // меняет окраску кнопки
            megaButton.Text = "💥";                 // и текст на ней.
            megaButton.Enabled = false;             // Отключение кнопки после нажатия.

            var index = Array.IndexOf(megaPlusButtons, megaButton); // Получаем индекс кнопки в её специальном массиве,
            var megaRepeat = exercises[index].MaxRepeat / 10 + 1;   // получаем большее число чем 1,


            // Отключение обычных кнопок
            var doneButton = repeatButtons[index];
            doneButton.BackColor = Color.ForestGreen;
            doneButton.Enabled = false;

            // и меняем значение числа повторов.
            if (TrainCommon.option == "strength")
            {
                exercises[index].StrengthRepeat += megaRepeat;
                // Если количество изменённых повторов стало больше максимально допустимого пишем "МАХ",
                if (exercises[index].StrengthRepeat > exercises[index].MaxRepeat / 2) doneButton.Text = "MAX";
                // если нет - то меняем на новое значение.
                else doneButton.Text = "✓";
            }
            else
            {
                exercises[index].StaminaRepeat += megaRepeat;
                // Если количество изменённых повторов стало больше максимально допустимого пишем "МАХ",
                if (exercises[index].StaminaRepeat > exercises[index].MaxRepeat) doneButton.Text = "MAX";
                // если нет - то меняем на новое значение.
                else doneButton.Text = "✓";
            }
        }

        private void разминкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exercise[] warmUp = TrainCommon.GetExercises("warmUp");
            var wF = new WarmUpForm(warmUp, this);
            wF.GetWarmUp();
        }

        private void заминкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exercise[] warmUp = TrainCommon.GetExercises("warmUp");
            var wF = new WarmUpForm(warmUp, this);
            wF.GetHitch();
        }

        private void мнеНехерДелатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrainCommon.SaveProgress();
            TrainCommon.SaveTrainResults(exercises);
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
            TrainCommon.SaveProgress();
            TrainCommon.SaveTrainResults(exercises);
            Close();
        }

        private void выходБезСохраненияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
