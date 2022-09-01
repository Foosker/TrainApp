using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrainWindowsFormsApp
{
    public class MyMessageBox : Form
    {
        public MyMessageBox()
        {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            StartPosition = FormStartPosition.Manual;
            Location = new Point(MousePosition.X, MousePosition.Y);

            FormBorderStyle = FormBorderStyle.None;  // None - yбирает шапку формы
            ShowIcon = false;

            KeyPress += MyMessageBox_KeyPress;
        }

        private void MyMessageBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)  // Если нажатая клавиша Escape
            {
                Close();
            }
        }

        public void ShowText(string message)
        {
            if (message != string.Empty)
            {
                var label = new Label()
                {
                    Location = new Point(0, 0),
                    Size = new Size(Height, Width),  // Размер лейбла во всю форму
                    AutoSize = true,

                    BackColor = Color.Ivory,
                    Font = new Font("Microsoft Sans Serif", 25F, FontStyle.Bold, GraphicsUnit.Point, (byte)204),
                    Text = message,
                    TextAlign = ContentAlignment.TopLeft
                };
                Controls.Add(label);
                ShowDialog();
            }
        }
    }
}
