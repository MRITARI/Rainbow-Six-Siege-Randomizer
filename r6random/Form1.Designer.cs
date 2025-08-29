namespace r6random
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Btn_Randomize = new System.Windows.Forms.Button();
            this.Btn_Settings = new System.Windows.Forms.Button();
            this.Btn_Attackers = new System.Windows.Forms.Button();
            this.Btn_Defenders = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ResetRandomized = new System.Windows.Forms.Button();
            this.Btn_Help = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(27, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(429, 376);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Btn_Randomize
            // 
            this.Btn_Randomize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Randomize.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Randomize.ForeColor = System.Drawing.Color.GreenYellow;
            this.Btn_Randomize.Location = new System.Drawing.Point(27, 475);
            this.Btn_Randomize.Name = "Btn_Randomize";
            this.Btn_Randomize.Size = new System.Drawing.Size(275, 85);
            this.Btn_Randomize.TabIndex = 1;
            this.Btn_Randomize.Text = "Randomize";
            this.Btn_Randomize.UseVisualStyleBackColor = true;
            this.Btn_Randomize.Click += new System.EventHandler(this.Btn_Randomize_Click);
            // 
            // Btn_Settings
            // 
            this.Btn_Settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Settings.ForeColor = System.Drawing.Color.White;
            this.Btn_Settings.Location = new System.Drawing.Point(304, 475);
            this.Btn_Settings.Name = "Btn_Settings";
            this.Btn_Settings.Size = new System.Drawing.Size(152, 85);
            this.Btn_Settings.TabIndex = 2;
            this.Btn_Settings.Text = "Settings";
            this.Btn_Settings.UseVisualStyleBackColor = true;
            this.Btn_Settings.Click += new System.EventHandler(this.Btn_Settings_Click);
            // 
            // Btn_Attackers
            // 
            this.Btn_Attackers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Attackers.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Attackers.ForeColor = System.Drawing.Color.White;
            this.Btn_Attackers.Location = new System.Drawing.Point(27, 427);
            this.Btn_Attackers.Name = "Btn_Attackers";
            this.Btn_Attackers.Size = new System.Drawing.Size(215, 46);
            this.Btn_Attackers.TabIndex = 3;
            this.Btn_Attackers.Text = "Attackers";
            this.Btn_Attackers.UseVisualStyleBackColor = true;
            this.Btn_Attackers.Click += new System.EventHandler(this.Btn_Attackers_Click);
            // 
            // Btn_Defenders
            // 
            this.Btn_Defenders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Defenders.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Defenders.ForeColor = System.Drawing.Color.White;
            this.Btn_Defenders.Location = new System.Drawing.Point(244, 427);
            this.Btn_Defenders.Name = "Btn_Defenders";
            this.Btn_Defenders.Size = new System.Drawing.Size(212, 46);
            this.Btn_Defenders.TabIndex = 4;
            this.Btn_Defenders.Text = "Defenders";
            this.Btn_Defenders.UseVisualStyleBackColor = true;
            this.Btn_Defenders.Click += new System.EventHandler(this.Btn_Defenders_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Randomized: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(163, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Last: ";
            // 
            // ResetRandomized
            // 
            this.ResetRandomized.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetRandomized.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResetRandomized.ForeColor = System.Drawing.Color.Tomato;
            this.ResetRandomized.Location = new System.Drawing.Point(388, 1);
            this.ResetRandomized.Name = "ResetRandomized";
            this.ResetRandomized.Size = new System.Drawing.Size(68, 29);
            this.ResetRandomized.TabIndex = 7;
            this.ResetRandomized.Text = "Reset";
            this.ResetRandomized.UseVisualStyleBackColor = true;
            this.ResetRandomized.Click += new System.EventHandler(this.ResetRandomized_Click);
            // 
            // Btn_Help
            // 
            this.Btn_Help.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Help.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Help.ForeColor = System.Drawing.Color.DodgerBlue;
            this.Btn_Help.Location = new System.Drawing.Point(342, 1);
            this.Btn_Help.Name = "Btn_Help";
            this.Btn_Help.Size = new System.Drawing.Size(40, 29);
            this.Btn_Help.TabIndex = 8;
            this.Btn_Help.Text = "?";
            this.Btn_Help.UseVisualStyleBackColor = true;
            this.Btn_Help.Click += new System.EventHandler(this.Btn_Help_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.ForeColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(0, 30);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(484, 531);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            this.richTextBox1.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(27, 406);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(429, 20);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "By: MRITARI, Version: 1.0";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImage = global::r6random.Properties.Resources._907782;
            this.ClientSize = new System.Drawing.Size(484, 561);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Btn_Help);
            this.Controls.Add(this.ResetRandomized);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Btn_Defenders);
            this.Controls.Add(this.Btn_Attackers);
            this.Controls.Add(this.Btn_Settings);
            this.Controls.Add(this.Btn_Randomize);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Btn_Randomize;
        private System.Windows.Forms.Button Btn_Settings;
        private System.Windows.Forms.Button Btn_Attackers;
        private System.Windows.Forms.Button Btn_Defenders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ResetRandomized;
        private System.Windows.Forms.Button Btn_Help;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

