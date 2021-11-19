using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TrainWindowsFormsApp
{
    public partial class SetNewLoadForm : Form
    {
        Dictionary<string, string> exercise;
        MyMessageBox message;

        public string NewLoad;

        public SetNewLoadForm(Dictionary<string, string> exercise)
        {
            InitializeComponent();

            this.exercise = exercise;
        }

        private void SetNewLoadForm_Load(object sender, EventArgs e)
        {
            currentExerciseLabel.Text = exercise["name"];
            oldLoadLabel.Text = "Прежняя нагрузка: " + exercise["load"];
        }

        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(newLoadTextBox.Text))
            {
                message = new MyMessageBox();
                message.ShowText("Введите нагрузку!");
                return false;
            }
            return true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                NewLoad = newLoadTextBox.Text;
                if (exercise.ContainsKey("repeats"))
                {
                    int repeatsNewValue = Convert.ToInt32(exercise["maxRepeats"]) / 2;
                    if (exercise["typeTrain"] == "Strength")
                    {
                        repeatsNewValue /= 2;
                    }
                    exercise["repeats"] = Convert.ToString(repeatsNewValue);
                }
                Close();
            }
        }

        private void newLoadTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            newLoadTextBox.Text = null;
        }
    }
}
