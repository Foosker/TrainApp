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
            oldLoadLabel.Text = exercise["load"];
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
