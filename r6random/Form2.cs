using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;

namespace r6random
{
    public partial class Form2 : Form
    {
        private List<OperatorInfo> _operators;

        public List<OperatorInfo> Operators => _operators;

        public Form2(List<OperatorInfo> operators)
        {
            InitializeComponent();

            // --- LOAD SETTINGS ---
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            primaryWeaponsToolStripMenuItem.Checked = bool.Parse(config.AppSettings.Settings["Primary"].Value);
            secondaryWeaponsToolStripMenuItem.Checked = bool.Parse(config.AppSettings.Settings["Secondary"].Value);
            attachmentsToolStripMenuItem.Checked = bool.Parse(config.AppSettings.Settings["Attachments"].Value);
            gripsToolStripMenuItem.Checked = bool.Parse(config.AppSettings.Settings["Grips"].Value);
            scopesToolStripMenuItem.Checked = bool.Parse(config.AppSettings.Settings["Scopes"].Value);
            gadgetsToolStripMenuItem.Checked = bool.Parse(config.AppSettings.Settings["Gadgets"].Value);

            this.Text = "Select Operators";
            byte[] iconBytes = Properties.Resources.icon;
            using (var ms = new MemoryStream(iconBytes))
            {
                this.Icon = new Icon(ms);
            }

            // --- 1. PERFECT THE LIST ORDER ---
            // Force Attackers first (Boxes 1-39), Defenders second (Boxes 40-78)
            var attackers = operators.Where(op => op.Role.Equals("attacker", StringComparison.OrdinalIgnoreCase)).ToList();
            var defenders = operators.Where(op => op.Role.Equals("defender", StringComparison.OrdinalIgnoreCase)).ToList();

            _operators = new List<OperatorInfo>();
            _operators.AddRange(attackers);
            _operators.AddRange(defenders);

            // --- 2. HOOK UP ALL 78 BOXES ---
            for (int i = 1; i <= 78; i++)
            {
                var pb = Controls.Find($"pictureBox_{i}", true).FirstOrDefault() as PictureBox;
                if (pb != null)
                {
                    int idx = i - 1; // Translates Box 1 -> Index 0

                    if (idx < _operators.Count)
                    {
                        // Attach Click Event
                        pb.Click += (s, e) =>
                        {
                            _operators[idx].Enabled = !_operators[idx].Enabled;
                            pb.Invalidate();
                            SaveOperatorStatesToConfig();
                        };

                        // Attach Border Painting (Green/Red)
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
            }

            // Load the pictures into the boxes
            LoadOperatorIcons();
        }

        private void LoadOperatorIcons()
        {
            for (int i = 0; i < _operators.Count && i < 78; i++)
            {
                var pb = Controls.Find($"pictureBox_{i + 1}", true).FirstOrDefault() as PictureBox;
                if (pb == null) continue;

                if (File.Exists(_operators[i].IconPath))
                {
                    // Image found! Show it.
                    pb.Image = Image.FromFile(_operators[i].IconPath);
                    pb.BackColor = Color.FromArgb(20, 20, 20);
                }
                else
                {
                    // Image missing! Wipe the ghost image and show a blank red box.
                    pb.Image = null;
                    pb.BackColor = Color.Red;
                }

                pb.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void SaveOperatorStatesToConfig()
        {
            var enabledNames = _operators.Where(op => op.Enabled).Select(op => op.Name);
            string enabledList = string.Join(",", enabledNames);

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["EnabledOperators"].Value = enabledList;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void LoadOperatorStatesFromConfig(List<OperatorInfo> operators)
        {
            string enabledList = ConfigurationManager.AppSettings["EnabledOperators"];
            if (string.IsNullOrEmpty(enabledList))
                return;

            var enabledNames = new HashSet<string>(enabledList.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            foreach (var op in operators)
            {
                op.Enabled = enabledNames.Contains(op.Name);
            }
        }

        private void primaryWeaponsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            bool primary = bool.Parse(config.AppSettings.Settings["Primary"].Value);
            primary = !primary;
            config.AppSettings.Settings["Primary"].Value = primary.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            if (primary)
            {
                primaryWeaponsToolStripMenuItem.Checked = true;
            }
            else
            {
                primaryWeaponsToolStripMenuItem.Checked = false;

            }
        }

        private void secondaryWeaponsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            bool secondary = bool.Parse(config.AppSettings.Settings["Secondary"].Value);
            secondary = !secondary;
            config.AppSettings.Settings["Secondary"].Value = secondary.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            if (secondary)
            {
                secondaryWeaponsToolStripMenuItem.Checked = true;
            }
            else
            {
                secondaryWeaponsToolStripMenuItem.Checked = false;
            }

        }

        private void attachmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            bool attachments = bool.Parse(config.AppSettings.Settings["Attachments"].Value);
            attachments = !attachments;
            config.AppSettings.Settings["Attachments"].Value = attachments.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            if (attachments)
            {
                attachmentsToolStripMenuItem.Checked = true;
            }
            else
            {
                attachmentsToolStripMenuItem.Checked = false;
            }

        }

        private void gripsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            bool grips = bool.Parse(config.AppSettings.Settings["Grips"].Value);
            grips = !grips;
            config.AppSettings.Settings["Grips"].Value = grips.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            if (grips)
            {
                gripsToolStripMenuItem.Checked = true;
            }
            else
            {
                gripsToolStripMenuItem.Checked = false;
            }

        }

        private void scopesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            bool scopes = bool.Parse(config.AppSettings.Settings["Scopes"].Value);
            scopes = !scopes;
            config.AppSettings.Settings["Scopes"].Value = scopes.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            if (scopes)
            {
                scopesToolStripMenuItem.Checked = true;
            }
            else
            {
                scopesToolStripMenuItem.Checked = false;
            }

        }

        private void gadgetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            bool gadgets = bool.Parse(config.AppSettings.Settings["Gadgets"].Value);
            gadgets = !gadgets;
            config.AppSettings.Settings["Gadgets"].Value = gadgets.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            if (gadgets)
            {
                gadgetsToolStripMenuItem.Checked = true;
            }
            else
            {
                gadgetsToolStripMenuItem.Checked = false;
            }

        }

        private void pictureBox_78_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_67_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_68_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_69_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_70_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_71_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_72_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_73_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_74_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_75_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_76_Click(object sender, EventArgs e)
        {

        }
    }
}
