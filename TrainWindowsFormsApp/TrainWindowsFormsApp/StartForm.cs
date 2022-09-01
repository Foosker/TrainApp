using System;
using System.Windows.Forms;

namespace TrainWindowsFormsApp
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        public void RegularTrainButton_Click(object sender, EventArgs e)
        {
            var mainForm = new MainForm();
            mainForm.isRegular = true;
            mainForm.ShowDialog();
        }

        public void PlusTrainButton_Click(object sender, EventArgs e)
        {
            var mainForm = new MainForm();
            mainForm.isRegular = false;
            mainForm.ShowDialog();
        }

        private void FastTrainButton_Click(object sender, EventArgs e)
        {
            var fastTrain = new FastTrainForm();
            fastTrain.Show();
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void warmUpButton_Click(object sender, EventArgs e)
        {
            var warmUp = TrainDay.GetWarmUpList();
            var wF = new WarmUpForm(warmUp, this);
            wF.GetWarmUp();
        }

        private void hitchButton_Click(object sender, EventArgs e)
        {
            var warmUp = TrainDay.GetWarmUpList();
            var wF = new WarmUpForm(warmUp, this);
            wF.GetHitch();
        }

        private void morningWorkOutButton_Click(object sender, EventArgs e)
        {
            var warmUp = TrainDay.GetMorningWorkOut();
            var mornWO = new WarmUpForm(warmUp, this);
            mornWO.GetWorkOut();
        }
    }
}
