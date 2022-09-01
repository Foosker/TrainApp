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
    public partial class SetNewLoadForm : Form
    {
        public string load;

        public SetNewLoadForm()
        {
            InitializeComponent();
        }
        public void HandOverOldLoad(string oldLoad)
        {
            load = oldLoad;
            oldLoadLabel.Text = load;
        }
        private void NewLoadButton_Click(object sender, EventArgs e)
        {
            load = newLoadTextBox.Text;
            Close();
        }
    }
}
