namespace TrainWindowsFormsApp
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.backgroundPictureBox = new System.Windows.Forms.PictureBox();
            this.выходБезСохраненияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.закончитьТренировкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.мнеНехерДелатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.разминкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заминкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьНовуюПрограммуТренировокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundPictureBox
            // 
            this.backgroundPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.backgroundPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backgroundPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("backgroundPictureBox.Image")));
            this.backgroundPictureBox.Location = new System.Drawing.Point(0, 0);
            this.backgroundPictureBox.Name = "backgroundPictureBox";
            this.backgroundPictureBox.Size = new System.Drawing.Size(1920, 1080);
            this.backgroundPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.backgroundPictureBox.TabIndex = 1;
            this.backgroundPictureBox.TabStop = false;
            // 
            // выходБезСохраненияToolStripMenuItem
            // 
            this.выходБезСохраненияToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.выходБезСохраненияToolStripMenuItem.BackColor = System.Drawing.Color.Firebrick;
            this.выходБезСохраненияToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.выходБезСохраненияToolStripMenuItem.Name = "выходБезСохраненияToolStripMenuItem";
            this.выходБезСохраненияToolStripMenuItem.Size = new System.Drawing.Size(212, 29);
            this.выходБезСохраненияToolStripMenuItem.Text = "Выход без сохранения";
            this.выходБезСохраненияToolStripMenuItem.Click += new System.EventHandler(this.выходБезСохраненияToolStripMenuItem_Click);
            // 
            // закончитьТренировкуToolStripMenuItem
            // 
            this.закончитьТренировкуToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.закончитьТренировкуToolStripMenuItem.BackColor = System.Drawing.Color.SeaGreen;
            this.закончитьТренировкуToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.закончитьТренировкуToolStripMenuItem.Name = "закончитьТренировкуToolStripMenuItem";
            this.закончитьТренировкуToolStripMenuItem.Size = new System.Drawing.Size(215, 29);
            this.закончитьТренировкуToolStripMenuItem.Text = "Закончить тренировку";
            this.закончитьТренировкуToolStripMenuItem.Click += new System.EventHandler(this.закончитьТренировкуToolStripMenuItem_Click);
            // 
            // мнеНехерДелатьToolStripMenuItem
            // 
            this.мнеНехерДелатьToolStripMenuItem.BackColor = System.Drawing.Color.MediumTurquoise;
            this.мнеНехерДелатьToolStripMenuItem.Name = "мнеНехерДелатьToolStripMenuItem";
            this.мнеНехерДелатьToolStripMenuItem.Size = new System.Drawing.Size(173, 29);
            this.мнеНехерДелатьToolStripMenuItem.Text = "Мне нехер делать";
            this.мнеНехерДелатьToolStripMenuItem.Click += new System.EventHandler(this.мнеНехерДелатьToolStripMenuItem_Click);
            // 
            // разминкаToolStripMenuItem
            // 
            this.разминкаToolStripMenuItem.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.разминкаToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.разминкаToolStripMenuItem.Name = "разминкаToolStripMenuItem";
            this.разминкаToolStripMenuItem.Size = new System.Drawing.Size(106, 29);
            this.разминкаToolStripMenuItem.Text = "Разминка";
            this.разминкаToolStripMenuItem.Click += new System.EventHandler(this.разминкаToolStripMenuItem_Click);
            // 
            // заминкаToolStripMenuItem
            // 
            this.заминкаToolStripMenuItem.BackColor = System.Drawing.Color.Bisque;
            this.заминкаToolStripMenuItem.Name = "заминкаToolStripMenuItem";
            this.заминкаToolStripMenuItem.Size = new System.Drawing.Size(98, 29);
            this.заминкаToolStripMenuItem.Text = "Заминка";
            this.заминкаToolStripMenuItem.Click += new System.EventHandler(this.заминкаToolStripMenuItem_Click);
            // 
            // создатьНовуюПрограммуТренировокToolStripMenuItem
            // 
            this.создатьНовуюПрограммуТренировокToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.создатьНовуюПрограммуТренировокToolStripMenuItem.BackColor = System.Drawing.Color.DimGray;
            this.создатьНовуюПрограммуТренировокToolStripMenuItem.Name = "создатьНовуюПрограммуТренировокToolStripMenuItem";
            this.создатьНовуюПрограммуТренировокToolStripMenuItem.Size = new System.Drawing.Size(356, 29);
            this.создатьНовуюПрограммуТренировокToolStripMenuItem.Text = "Создать новую программу тренировок";
            this.создатьНовуюПрограммуТренировокToolStripMenuItem.Click += new System.EventHandler(this.создатьНовуюПрограммуТренировокToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Black;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выходБезСохраненияToolStripMenuItem,
            this.закончитьТренировкуToolStripMenuItem,
            this.мнеНехерДелатьToolStripMenuItem,
            this.разминкаToolStripMenuItem,
            this.заминкаToolStripMenuItem,
            this.создатьНовуюПрограммуТренировокToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1920, 33);
            this.menuStrip1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.backgroundPictureBox);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(20, 0);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Let\'s Rock";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox backgroundPictureBox;
        private System.Windows.Forms.ToolStripMenuItem выходБезСохраненияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem закончитьТренировкуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem мнеНехерДелатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem разминкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заминкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьНовуюПрограммуТренировокToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}

