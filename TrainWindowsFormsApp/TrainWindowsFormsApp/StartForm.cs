using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrainWindowsFormsApp
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void RegularTrainButton_Click(object sender, EventArgs e)
        {
            Close();
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
            //var warmUpList = TrainDay.GetWarmUpList();
            //var warmUpForm = new WarmUpForm(warmUpList, )
        }

        private void hitchButton_Click(object sender, EventArgs e)
        {

        }
    }
}
