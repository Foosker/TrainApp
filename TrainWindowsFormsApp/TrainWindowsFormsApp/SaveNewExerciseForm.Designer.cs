namespace TrainWindowsFormsApp
{
    partial class SaveNewExerciseForm
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
            this.components = new System.ComponentModel.Container();
            this.textTextBox = new System.Windows.Forms.TextBox();
            this.repeatTextBox = new System.Windows.Forms.TextBox();
            this.loadTextBox = new System.Windows.Forms.TextBox();
            this.remarkTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.exercisesTypeListBox = new System.Windows.Forms.ListBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.maxRepeatTextBox = new System.Windows.Forms.TextBox();
            this.exerciseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.exerciseBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // textTextBox
            // 
            this.textTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textTextBox.Location = new System.Drawing.Point(339, 355);
            this.textTextBox.Name = "textTextBox";
            this.textTextBox.Size = new System.Drawing.Size(1200, 53);
            this.textTextBox.TabIndex = 0;
            this.textTextBox.Text = "Введи название упражнения";
            this.textTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // repeatTextBox
            // 
            this.repeatTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repeatTextBox.Location = new System.Drawing.Point(838, 566);
            this.repeatTextBox.Name = "repeatTextBox";
            this.repeatTextBox.Size = new System.Drawing.Size(100, 53);
            this.repeatTextBox.TabIndex = 0;
            this.repeatTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // loadTextBox
            // 
            this.loadTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadTextBox.Location = new System.Drawing.Point(484, 645);
            this.loadTextBox.Name = "loadTextBox";
            this.loadTextBox.Size = new System.Drawing.Size(900, 53);
            this.loadTextBox.TabIndex = 0;
            this.loadTextBox.Text = "Какая нагрузка";
            this.loadTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // remarkTextBox
            // 
            this.remarkTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.remarkTextBox.Location = new System.Drawing.Point(284, 737);
            this.remarkTextBox.Name = "remarkTextBox";
            this.remarkTextBox.Size = new System.Drawing.Size(1300, 53);
            this.remarkTextBox.TabIndex = 0;
            this.remarkTextBox.Text = "Введи примечание к упражнению";
            this.remarkTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(627, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(625, 46);
            this.label1.TabIndex = 1;
            this.label1.Text = "Сохранение нового упражнения";
            // 
            // exercisesTypeListBox
            // 
            this.exercisesTypeListBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.exercisesTypeListBox.ColumnWidth = 200;
            this.exercisesTypeListBox.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exercisesTypeListBox.FormattingEnabled = true;
            this.exercisesTypeListBox.ItemHeight = 39;
            this.exercisesTypeListBox.Location = new System.Drawing.Point(12, 12);
            this.exercisesTypeListBox.MultiColumn = true;
            this.exercisesTypeListBox.Name = "exercisesTypeListBox";
            this.exercisesTypeListBox.Size = new System.Drawing.Size(1896, 160);
            this.exercisesTypeListBox.TabIndex = 2;
            this.exercisesTypeListBox.SelectedIndexChanged += new System.EventHandler(this.exercisesTypeListBox_SelectedIndexChanged);
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveButton.Location = new System.Drawing.Point(629, 880);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(150, 100);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exitButton.Location = new System.Drawing.Point(1096, 880);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(150, 100);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Закрыть";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // maxRepeatTextBox
            // 
            this.maxRepeatTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.maxRepeatTextBox.Location = new System.Drawing.Point(944, 566);
            this.maxRepeatTextBox.Name = "maxRepeatTextBox";
            this.maxRepeatTextBox.Size = new System.Drawing.Size(100, 53);
            this.maxRepeatTextBox.TabIndex = 0;
            this.maxRepeatTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // exerciseBindingSource
            // 
            this.exerciseBindingSource.DataSource = typeof(TrainWindowsFormsApp.Exercise);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(404, 534);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1117, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "Количество повторов и максимальных повторов для перехода к следующему уровню нагр" +
    "узки";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox1.Location = new System.Drawing.Point(796, 451);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(321, 33);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Базовое ли упражнение";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // SaveNewExerciseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.exercisesTypeListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.remarkTextBox);
            this.Controls.Add(this.loadTextBox);
            this.Controls.Add(this.maxRepeatTextBox);
            this.Controls.Add(this.repeatTextBox);
            this.Controls.Add(this.textTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SaveNewExerciseForm";
            this.Text = "Сохранение нового вопроса";
            this.Load += new System.EventHandler(this.SaveNewExercise_Load);
            ((System.ComponentModel.ISupportInitialize)(this.exerciseBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textTextBox;
        private System.Windows.Forms.TextBox repeatTextBox;
        private System.Windows.Forms.TextBox loadTextBox;
        private System.Windows.Forms.TextBox remarkTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox exercisesTypeListBox;
        private System.Windows.Forms.BindingSource exerciseBindingSource;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TextBox maxRepeatTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}