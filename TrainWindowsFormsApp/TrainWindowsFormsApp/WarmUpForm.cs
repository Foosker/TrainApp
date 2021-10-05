using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TrainWindowsFormsApp
{
    public partial class WarmUpForm : Form
    {
        private OldExercise[] exercises;
        private List<string> warmUp;
        private int warmUpCount;

        private static Random random = new Random();

        public WarmUpForm(OldExercise[] array, Form form)
        {            
            InitializeComponent();

            Location = new Point(form.Right, form.Top);

            exercises = array;
        }

        private void warmUpForm_Load(object sender, EventArgs e)
        {
            var butt = new Button();
            butt.Click += Butt_Click;
            CancelButton = butt;
        }

        private void Butt_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowLabels()
        {
            for (int i = 0; i < warmUpCount; i++)
            {
                var label = TrainCommon.CreateLabel(this, 50, i, 600);
                label.Text = warmUp[i];
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
    }
}
