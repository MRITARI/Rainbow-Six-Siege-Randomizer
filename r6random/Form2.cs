using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace r6random
{
    public partial class Form2 : Form
    {
        private List<OperatorInfo> _operators;

        public List<OperatorInfo> Operators => _operators; // expose to Form1

        public Form2(List<OperatorInfo> operators)
        {
            InitializeComponent();
            this.Text = "Select Operators";
            this.Icon = new Icon("rainbow-six-siege-logo-png_seeklogo-325646.ico");
            _operators = operators;

            // Attach click handlers for all 75 picture boxes
            for (int i = 1; i <= 75; i++)
            {
                var pb = Controls.Find($"pictureBox_{i}", true).FirstOrDefault() as PictureBox;
                if (pb != null)
                {
                    int idx = i - 1; // capture index

                    pb.Click += (s, e) =>
                    {
                        _operators[idx].Enabled = !_operators[idx].Enabled;
                        pb.Invalidate();
                    };

                    pb.Paint += (s, e) =>
                    {
                        Color borderColor = _operators[idx].Enabled ? Color.Green : Color.Red;
                        using (Pen pen = new Pen(borderColor, 3))
                        {
                            e.Graphics.DrawRectangle(pen, 0, 0, pb.Width - 1, pb.Height - 1);
                        }
                    };
                }
            }

            LoadOperatorIcons();
        }

        private void LoadOperatorIcons()
        {
            for (int i = 0; i < _operators.Count && i < 75; i++)
            {
                var pb = Controls.Find($"pictureBox_{i + 1}", true).FirstOrDefault() as PictureBox;
                if (pb == null) continue;

                if (File.Exists(_operators[i].IconPath))
                {
                    pb.Image = Image.FromFile(_operators[i].IconPath);
                    
                    pb.BackColor = Color.FromArgb(20, 20, 20);
                }
                else
                    pb.BackColor = Color.Red;

                pb.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
