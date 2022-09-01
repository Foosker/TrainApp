namespace TrainWindowsFormsApp
{
    partial class SetNewLoadForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetNewLoadForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.oldLoadLabel = new System.Windows.Forms.Label();
            this.newLoadTextBox = new System.Windows.Forms.TextBox();
            this.newLoadButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(387, 545);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(375, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "Новая нагрузка";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(498, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(399, 55);
            this.label2.TabIndex = 0;
            this.label2.Text = "Старая нагрузка";
            // 
            // oldLoadLabel
            // 
            this.oldLoadLabel.AutoSize = true;
            this.oldLoadLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.oldLoadLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.oldLoadLabel.Location = new System.Drawing.Point(903, 214);
            this.oldLoadLabel.Name = "oldLoadLabel";
            this.oldLoadLabel.Size = new System.Drawing.Size(40, 55);
            this.oldLoadLabel.TabIndex = 0;
            this.oldLoadLabel.Text = "-";
            // 
            // newLoadTextBox
            // 
            this.newLoadTextBox.BackColor = System.Drawing.SystemColors.Info;
            this.newLoadTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.newLoadTextBox.Location = new System.Drawing.Point(769, 545);
            this.newLoadTextBox.Name = "newLoadTextBox";
            this.newLoadTextBox.Size = new System.Drawing.Size(172, 57);
            this.newLoadTextBox.TabIndex = 1;
            // 
            // newLoadButton
            // 
            this.newLoadButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.newLoadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.newLoadButton.Location = new System.Drawing.Point(713, 781);
            this.newLoadButton.Name = "newLoadButton";
            this.newLoadButton.Size = new System.Drawing.Size(114, 61);
            this.newLoadButton.TabIndex = 2;
            this.newLoadButton.Text = "OK";
            this.newLoadButton.UseVisualStyleBackColor = false;
            this.newLoadButton.Click += new System.EventHandler(this.NewLoadButton_Click);
            // 
            // SetNewLoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1457, 869);
            this.Controls.Add(this.newLoadButton);
            this.Controls.Add(this.newLoadTextBox);
            this.Controls.Add(this.oldLoadLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SetNewLoadForm";
            this.Text = "SetNewLoadForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label oldLoadLabel;
        private System.Windows.Forms.TextBox newLoadTextBox;
        private System.Windows.Forms.Button newLoadButton;
    }
}