using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Runtime.InteropServices;




namespace r6random
{

    public partial class Form1 : Form
    {


        private const int HOTKEY_ID = 9000; 

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private List<OperatorInfo> _operators;
        private List<OperatorInfo> randomizedAttackers = new List<OperatorInfo>();
        private List<OperatorInfo> randomizedDefenders = new List<OperatorInfo>();


        private Random _random = new Random();
        private Form2 _form2;
        bool attackers = true;
        bool help = false;
        


        public Form1()
        {

            InitializeComponent();
            this.Text = "R6 Randomizer v1.0.0";
            this.Icon = new Icon("rainbow-six-siege-logo-png_seeklogo-325646.ico");
            LoadHelpText();
            RegisterHotKey(this.Handle, HOTKEY_ID, 0x0002 | 0x0001, (uint)Keys.R);
            this.KeyPreview = true; 
            Btn_Attackers.ForeColor = Color.Tomato;
            Btn_Attackers.BackColor = Color.FromArgb(20, 20, 20);
            Btn_Defenders.BackColor = Color.FromArgb(20, 20, 20);
            Btn_Randomize.BackColor = Color.FromArgb(20, 20, 20);
            Btn_Settings.BackColor = Color.FromArgb(20, 20, 20);
            ResetRandomized.BackColor = Color.FromArgb(20, 20, 20);
            Btn_Help.BackColor = Color.FromArgb(20, 20, 20);
            richTextBox1.BackColor = Color.FromArgb(20, 20, 20);
            textBox1.BackColor = Color.FromArgb(20, 20, 20);

            
            if (File.Exists("operators.json"))
            {
                var json = File.ReadAllText("operators.json");
                _operators = JsonConvert.DeserializeObject<List<OperatorInfo>>(json);
            }
            else
            {
                MessageBox.Show("operators.json not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _operators = new List<OperatorInfo>();
            }
        }

        private void Btn_Settings_Click(object sender, EventArgs e)
        {
            if (_form2 == null || _form2.IsDisposed)
            {
                _form2 = new Form2(_operators); 
                _form2.Show();
            }
            else
            {
                _form2.Visible = !_form2.Visible;
            }
        }



        private void Btn_Randomize_Click(object sender, EventArgs e)
        {
            if (_operators == null || _operators.Count == 0) return;
            

            var enabledOperators = _operators.Where(op => op.Enabled).ToList();
            enabledOperators = attackers
                ? enabledOperators.Where(op => op.Role == "Attacker").ToList()
                : enabledOperators.Where(op => op.Role == "Defender").ToList();

            
            var randomizedList = attackers ? randomizedAttackers : randomizedDefenders;
            label1.Text = $"Randomized: {randomizedList.Count}";
            
            if (randomizedList.Count >= enabledOperators.Count)
            {
                MessageBox.Show("All enabled operators have been randomized.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OperatorInfo randomOperator;

            
            do
            {
                randomOperator = enabledOperators[_random.Next(enabledOperators.Count)];
            } while (randomizedList.Contains(randomOperator));

            randomizedList.Add(randomOperator);

            
            label1.Text = $"Randomized: {randomizedList.Count}";
            label2.Text = $"Last: {randomOperator.Name}";

            
            if (File.Exists(randomOperator.ImagePath))
            {
                pictureBox1.Image = Image.FromFile(randomOperator.ImagePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }


        private void Btn_Attackers_Click(object sender, EventArgs e)
        {
            attackers = true;
            Btn_Attackers.ForeColor = Color.Tomato;
            Btn_Defenders.ForeColor = Color.White;


        }

        private void Btn_Defenders_Click(object sender, EventArgs e)
        {
            attackers = false;
            Btn_Defenders.ForeColor = Color.Tomato;
            Btn_Attackers.ForeColor = Color.White;


        }

        private void ResetRandomized_Click(object sender, EventArgs e)
        {
            randomizedAttackers.Clear();
            randomizedDefenders.Clear();
            label1.Text = $"Randomized: ";
            label2.Text = $"Last: ";

        }
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
            {
                ToggleMinimizeRestore();
            }
            base.WndProc(ref m);
        }

        private void ToggleMinimizeRestore()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal; // restore
                this.BringToFront();                        // bring on top
                this.Activate();                             // focus
            }
            else
            {
                this.WindowState = FormWindowState.Minimized; // minimize
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, HOTKEY_ID);
        }

        private void Btn_Help_Click(object sender, EventArgs e)
        {
            
            help = !help;
            if (help)
            {
                richTextBox1.Visible = true;
                Btn_Help.ForeColor = Color.Tomato;
            }
            else
            {
                richTextBox1.Visible = false;
                Btn_Help.ForeColor = Color.DodgerBlue;
            }
        }
        
        private void LoadHelpText()
        {
            richTextBox1.Clear();
            richTextBox1.ReadOnly = true;
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;

            
            var helpSections = new[]
            {
        new { Header = "Rainbow Six Siege Randomizer – Help", SubHeader = "Overview", Body = "The R6 Randomizer app allows you to randomly select an operator for attackers or defenders in Rainbow Six Siege. You can enable or disable specific operators and view their icons in the settings." },
        new { Header = "Main Features", SubHeader = "", Body = "" },
        new { Header = "", SubHeader = "Randomize Button", Body = "Click Randomize to pick a random operator from the currently enabled list.\nThe selected operator’s image will appear in the main picture box." },
        new { Header = "", SubHeader = "Attacker / Defender Toggle", Body = "Click Attackers or Defenders to choose which side the random operator should be selected from.\nOnly enabled operators from the chosen side will be included." },
        new { Header = "", SubHeader = "Settings", Body = "Click Settings to open the operator list.\nEach operator has an icon (green border = enabled, red border = disabled).\nClick an operator icon to toggle whether it is included in randomization.\nYou can minimize or restore the settings window without losing changes." },
        new { Header = "", SubHeader = "Global Hotkey", Body = "Press AltGr + R to minimize or restore the main app window while playing the game.\nWorks even if Rainbow Six Siege is running in fullscreen." },
        new { Header = "", SubHeader = "Operator List", Body = "All operators are enabled by default. Disabled operators are skipped in randomization." },
        new { Header = "", SubHeader = "Error Handling", Body = "No operators enabled: You must enable at least one operator in settings.\nImage not found: The app will warn you if the operator image is missing." },
    };

            foreach (var section in helpSections)
            {
                // Main Title
                if (!string.IsNullOrEmpty(section.Header))
                {
                    if (section.Header == "Rainbow Six Siege Randomizer – Help" || section.Header == "Main Features")
                        richTextBox1.SelectionFont = new Font("Segoe UI", 14, FontStyle.Bold);
                    else
                        richTextBox1.SelectionFont = new Font("Segoe UI", 12, FontStyle.Bold);

                    richTextBox1.SelectionColor = Color.Tomato;
                    richTextBox1.AppendText(section.Header + Environment.NewLine);
                }

                // Subheader
                if (!string.IsNullOrEmpty(section.SubHeader))
                {
                    richTextBox1.SelectionFont = new Font("Segoe UI", 11, FontStyle.Bold);
                    richTextBox1.SelectionColor = Color.White;
                    richTextBox1.AppendText(section.SubHeader + Environment.NewLine);
                }

                // Body
                if (!string.IsNullOrEmpty(section.Body))
                {
                    richTextBox1.SelectionFont = new Font("Segoe UI", 10, FontStyle.Regular);
                    richTextBox1.SelectionColor = Color.White;
                    richTextBox1.AppendText(section.Body + Environment.NewLine + Environment.NewLine);
                }
            }

            // Reset selection
            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectionLength = 0;
            richTextBox1.SelectionColor = richTextBox1.ForeColor;
        }




    }
}

