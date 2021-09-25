
namespace TrainWindowsFormsApp
{
    partial class StartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RegularTrainButton = new System.Windows.Forms.Button();
            this.FastTrainButton = new System.Windows.Forms.Button();
            this.QuitButton = new System.Windows.Forms.Button();
            this.warmUpButton = new System.Windows.Forms.Button();
            this.hitchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RegularTrainButton
            // 
            this.RegularTrainButton.BackColor = System.Drawing.Color.Gold;
            this.RegularTrainButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RegularTrainButton.Font = new System.Drawing.Font("Segoe Print", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RegularTrainButton.ForeColor = System.Drawing.Color.MediumBlue;
            this.RegularTrainButton.Location = new System.Drawing.Point(15, 560);
            this.RegularTrainButton.Name = "RegularTrainButton";
            this.RegularTrainButton.Size = new System.Drawing.Size(250, 180);
            this.RegularTrainButton.TabIndex = 0;
            this.RegularTrainButton.Text = "Тренировка";
            this.RegularTrainButton.UseVisualStyleBackColor = false;
            this.RegularTrainButton.Click += new System.EventHandler(this.RegularTrainButton_Click);
            // 
            // FastTrainButton
            // 
            this.FastTrainButton.BackColor = System.Drawing.Color.LawnGreen;
            this.FastTrainButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.FastTrainButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FastTrainButton.Font = new System.Drawing.Font("Segoe Print", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FastTrainButton.ForeColor = System.Drawing.Color.DarkRed;
            this.FastTrainButton.Location = new System.Drawing.Point(735, 560);
            this.FastTrainButton.Name = "FastTrainButton";
            this.FastTrainButton.Size = new System.Drawing.Size(250, 180);
            this.FastTrainButton.TabIndex = 1;
            this.FastTrainButton.Text = "Список упражнений";
            this.FastTrainButton.UseVisualStyleBackColor = false;
            this.FastTrainButton.Click += new System.EventHandler(this.FastTrainButton_Click);
            // 
            // QuitButton
            // 
            this.QuitButton.BackColor = System.Drawing.Color.Maroon;
            this.QuitButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.QuitButton.Font = new System.Drawing.Font("Segoe Print", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.QuitButton.Location = new System.Drawing.Point(325, 900);
            this.QuitButton.Name = "QuitButton";
            this.QuitButton.Size = new System.Drawing.Size(350, 100);
            this.QuitButton.TabIndex = 2;
            this.QuitButton.Text = "Выход";
            this.QuitButton.UseVisualStyleBackColor = false;
            this.QuitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // warmUpButton
            // 
            this.warmUpButton.BackColor = System.Drawing.Color.Orange;
            this.warmUpButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.warmUpButton.Font = new System.Drawing.Font("Segoe Print", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.warmUpButton.ForeColor = System.Drawing.Color.Chartreuse;
            this.warmUpButton.Location = new System.Drawing.Point(200, 190);
            this.warmUpButton.Name = "warmUpButton";
            this.warmUpButton.Size = new System.Drawing.Size(210, 130);
            this.warmUpButton.TabIndex = 3;
            this.warmUpButton.Text = "Разминка";
            this.warmUpButton.UseVisualStyleBackColor = false;
            this.warmUpButton.Click += new System.EventHandler(this.warmUpButton_Click);
            // 
            // hitchButton
            // 
            this.hitchButton.BackColor = System.Drawing.Color.DarkTurquoise;
            this.hitchButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.hitchButton.Font = new System.Drawing.Font("Segoe Print", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hitchButton.ForeColor = System.Drawing.Color.Ivory;
            this.hitchButton.Location = new System.Drawing.Point(590, 190);
            this.hitchButton.Name = "hitchButton";
            this.hitchButton.Size = new System.Drawing.Size(210, 130);
            this.hitchButton.TabIndex = 3;
            this.hitchButton.Text = "Заминка";
            this.hitchButton.UseVisualStyleBackColor = false;
            this.hitchButton.Click += new System.EventHandler(this.hitchButton_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::TrainWindowsFormsApp.Properties.Resources.TrainHard11;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1000, 1200);
            this.Controls.Add(this.hitchButton);
            this.Controls.Add(this.warmUpButton);
            this.Controls.Add(this.QuitButton);
            this.Controls.Add(this.FastTrainButton);
            this.Controls.Add(this.RegularTrainButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StartForm";
            this.Text = "StartForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RegularTrainButton;
        private System.Windows.Forms.Button FastTrainButton;
        private System.Windows.Forms.Button QuitButton;
        private System.Windows.Forms.Button warmUpButton;
        private System.Windows.Forms.Button hitchButton;
    }
}