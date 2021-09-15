using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TrainWindowsFormsApp
{
    public partial class WarmUpForm : Form
    {
        private readonly int height = 60;           // Высота ЭУ
        private readonly int indentBetween = 15;    // Расстояние между ЭУ по горизонтали,
        private readonly int indentUpEdge = 60;     // то же по вертикали.

        private Exercise[] exercises;
        private List<string> warmUp;
        private int warmUpCount;

        private static Random random = new Random();

        public WarmUpForm(Exercise[] array, Form form)
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
                CreateLabel(50, i, 600);
            }

            Show();
        }

        private string GetModifiers()
        {
            var modifiers = new List<string>
            {
                "Блок : ",
                "Уворот " + AddOptions("вправо : ", "влево : ", "вниз : "),
                "Шаг " + AddOptions("вперёд : ", "назад : ", "вправо : ", "влево : "),
                "Повтор удара : ",
                AddOptions("5") + " сильных ударов : ",
                AddOptions("10") + " быстрых ударов : "
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

        private Label CreateLabel(int indentLeftEdge, int indexRow, int width)
        {   // Создание ячеек
            int x = indentLeftEdge;
            int y = indentUpEdge + indexRow * (indentBetween + height); // Формула расчёта координат эллемента по ординате

            var label = new Label
            {
                BackColor = Color.AntiqueWhite,
                Font = new Font("Comic Sans MS", 18F, FontStyle.Bold, GraphicsUnit.Point, 204),
                Size = new Size(width, height),
                Text = warmUp[indexRow],
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(x, y)
            };
            Controls.Add(label);
            return label;
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
