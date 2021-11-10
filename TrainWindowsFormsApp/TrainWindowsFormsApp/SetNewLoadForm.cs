using System;
using System.Windows.Forms;

namespace TrainWindowsFormsApp
{
    public partial class SetNewLoadForm : Form
    {
        Exercise exercise;
        MyMessageBox message;

        public string NewLoad;

        public SetNewLoadForm(Exercise exercise)
        {
            InitializeComponent();

            this.exercise = exercise;
        }

        private void SetNewLoadForm_Load(object sender, EventArgs e)
        {/*
            currentExerciseLabel.Text = exercise.Name;
            oldLoadLabel.Text = exercise.StrengthLoad;
            oldLoadLabel.Text = exercise.StaminaLoad;
            oldLoadLabel.Text = exercise.TabataLoad;*/
        }

        private bool IsValid()
        {
            if (String.IsNullOrWhiteSpace(newLoadTextBox.Text))
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
                Close();
            }
        }

        private void newLoadTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            newLoadTextBox.Text = null;
        }
    }
}
