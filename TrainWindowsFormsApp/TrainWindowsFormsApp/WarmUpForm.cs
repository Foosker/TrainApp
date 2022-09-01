using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TrainWindowsFormsApp
{
    public partial class WarmUpForm : Form
    {
        private List<Exercise> exercises;
        private List<string> warmUp;
        private int warmUpCount;

        private static Random random = new Random();

        public WarmUpForm(List<Exercise> list, Form form)
        {            
            InitializeComponent();

            Location = new Point(form.Right / 2, form.Bottom / 2);

            exercises = list;
        }
        private void warmUpForm_Load(object sender, EventArgs e)
        {
            var butt = new Button();
            CancelButton = butt;
            butt.Click += Butt_Click;
        }
        private void Butt_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void ShowLabelsWorkOut()
        {
            var labelsList = new List<Label>();
            var splitsCount = 10;
            var splitLabel = TrainCommon.CreateLabel(this, 5, 0, 1000, 70, splitsCount.ToString() + " кругов");
            for (int i = 0; i < warmUpCount; i++)
            {
                labelsList.Add(TrainCommon.CreateLabel(this, 5, i + 2, 800, 70, warmUp[i]));
            }

            string addedExercise;
            switch (TrainDay.progress % 5)
            {            
                case 0:  addedExercise = "Разведение рук с петлёй"; break; 
                case 1:  addedExercise = "Махи руками в стороны"; break;
                case 2:  addedExercise = "Тяга в наклоне с выпрямлением тела"; break;
                case 3:  addedExercise = "Приседания с подъёмом на носки"; break;
                default: addedExercise = "Отжимания с притягиванием ног к груди"; break;
            }
            labelsList.Add(TrainCommon.CreateLabel(this, 5, 5, 800, 70, addedExercise + " 20 повт."));
            
            var timerLabel = TrainCommon.CreateLabel(this, 425, 6, 155, 70, "Таймер");
            timerLabel.Click += TimerLabel_Click;
            var seconds = 0;

            timer.Tick += Timer_Tick;
            Show();

            void TimerLabel_Click(object sender, EventArgs e)
            {
                timer.Enabled = !timer.Enabled;
                if (!timer.Enabled) timerLabel.Text = "Пауза";
                FocusOnFirstExercise();
            }

            void Timer_Tick(object sender, EventArgs e)
            {
                seconds--;
                timerLabel.Text = seconds.ToString();

                if (seconds <= 0)
                {
                    if (labelsList[1].Visible)
                    {
                        splitsCount--;
                        FocusOnFirstExercise();                    
                        if (splitsCount >= 5) splitLabel.Text = splitsCount.ToString() + " кругов";
                        else if (splitsCount > 1) splitLabel.Text = splitsCount.ToString() + " круга";
                        else if (splitsCount == 1) splitLabel.Text = splitsCount.ToString() + " круг";
                        else
                        {
                            FocusOnThirdExercise();
                        }
                    }
                    else
                    {
                        FocusOnSecondExercise();
                    }
                
                }
            };
            void FocusOnFirstExercise()
            {
                seconds = 25;
                labelsList[0].Visible = true;
                labelsList[1].Visible = false;;
                labelsList[2].Visible = false;;
            };
            void FocusOnSecondExercise()
            {
                seconds = 25;
                labelsList[0].Visible = false;
                labelsList[1].Visible = true;
                labelsList[2].Visible = false;
            };
            void FocusOnThirdExercise()
            {
                timer.Stop();
                splitLabel.Visible = false;
                timerLabel.Visible = false;
                labelsList[0].Visible = false;
                labelsList[1].Visible = false;
                labelsList[2].Visible = true;
            };

        }


        private void ShowLabels()
        {
            for (int i = 0; i < warmUpCount; i++)
            {
                var row = i >= 3 ? i + 1 : i;
                TrainCommon.CreateLabel(this, 50, row, 600, 70, warmUp[i]);                
            }
            Show();
        }

        private string GetModifiers()
        {
            var modifiers = new List<string>
            {
                "Из блока : ",
                "С уворотом " + AddOptions("в сторону : ", "вниз : "),
                "С шагом " + AddOptions("вперёд : ", "назад : ", "в сторону : "),
                "С повтором : ",
                "Cильный : ",
                "Быстрый : "
            };
            return modifiers[random.Next(modifiers.Count())];
        }

        private static string AddOptions(params string[] options)
        {
            if (Int32.TryParse(options[0], out var result))
            {
                return random.Next(3, result).ToString();
            }
            return options[random.Next(options.Length)];            
        }

        public void GetHitch()
        {
            warmUp = new List<string>();

            foreach (var exercise in exercises)
            {
                warmUp.Add(GetModifiers() + exercise.Name);
            }
            warmUpCount = warmUp.Count();

            ShowLabels();
        }

        public void GetWarmUp()
        {
            warmUp = new List<string>();

            foreach (var exercise in exercises)
            {
                warmUp.Add(exercise.Name);
            }
            warmUpCount = warmUp.Count();

            ShowLabels();
        }

        internal void GetWorkOut()
        {
            warmUp = new List<string>();

            foreach (var exercise in exercises)
            {
                warmUp.Add(exercise.Name);
            }
            warmUpCount = warmUp.Count();

            ShowLabelsWorkOut();
        }
    }
}
