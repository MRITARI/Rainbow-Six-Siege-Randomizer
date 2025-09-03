using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace r6random
{

    public partial class Form1 : Form
    {
        private const string GitHubRepoOwner = "MRITARI";
        private const string GitHubRepoName = "Rainbow-Six-Siege-Randomizer";
        private static readonly Version CurrentVersion = new Version("1.2.1");
        private static readonly HttpClient client = new HttpClient();

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        private IntPtr _hookID = IntPtr.Zero;

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private LowLevelKeyboardProc _proc;


        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int WS_EX_LAYERED = 0x80000;

        private async Task CheckForUpdatesAsync()
        {
            try
            {

                string url = $"https://api.github.com/repos/{GitHubRepoOwner}/{GitHubRepoName}/releases/latest";


                client.DefaultRequestHeaders.UserAgent.ParseAdd("C# App");

                var response = await client.GetStringAsync(url);
                dynamic releaseInfo = JsonConvert.DeserializeObject(response);

                string latestTagName = releaseInfo.tag_name;
                Version latestVersion = new Version(latestTagName.TrimStart('v', 'V'));

                if (latestVersion > CurrentVersion)
                {
                    MessageBox.Show(
                        $"A new version ({latestVersion}) is available on GitHub!",
                        "Update Available",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"Error checking for updates: {ex.Message}");
            }
        }
        private void MakeClickThrough()
        {
            int style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, style | WS_EX_TRANSPARENT | WS_EX_LAYERED);
        }

        private void RemoveClickThrough()
        {
            int style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, style & ~WS_EX_TRANSPARENT);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);


        private List<OperatorInfo> _operators;
        private List<OperatorInfo> randomizedAttackers = new List<OperatorInfo>();
        private List<OperatorInfo> randomizedDefenders = new List<OperatorInfo>();


        private Random _random = new Random();
        private Form2 _form2;
        bool attackers = true;
        bool help = false;
        string lastDefender = "";
        string lastAttacker = "";
        string defenderCount = "";
        string attackerCount = "";

        string lastPrimary = "Gun: N/A";
        string lastSecondary = "Secondary: N/A";
        string lastAttachment = "Attachment: N/A";
        string lastGrip = "Grip: N/A";
        string lastScope = "Scope: N/A";
        string lastGadget = "Gadget: N/A";
        string lastSecondaryAttachment = "Attachment: N/A";

        string lastDefenderPrimary = "Gun: N/A";
        string lastDefenderSecondary = "Secondary: N/A";
        string lastDefenderAttachment = "Attachment: N/A";
        string lastDefenderGrip = "Grip: N/A";
        string lastDefenderScope = "Scope: N/A";
        string lastDefenderGadget = "Gadget: N/A";
        string lastDefenderSecondaryAttachment = "Attachment: N/A";



        public Form1()
        {

            InitializeComponent();
            _ = CheckForUpdatesAsync();

            // Remove borders and taskbar icon
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;

            // Always stay on top
            this.TopMost = true;



            this.Opacity = 0.9;


            this.Text = "R6 Randomizer v1.2.1";

            label3.Text = $"Gun: N/A";
            label4.Text = $"Attachment: N/A";
            label5.Text = $"Grip: N/A";
            label6.Text = $"Scope: N/A";
            label7.Text = $"Secondary: N/A";
            label8.Text = $"Attachment: N/A";
            label9.Text = $"Gadget: N/A";
            byte[] iconBytes = Properties.Resources.icon;
            using (var ms = new MemoryStream(iconBytes))
            {
                this.Icon = new Icon(ms);
            }
            

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            bool primary = bool.Parse(config.AppSettings.Settings["Primary"].Value);
            bool secondary = bool.Parse(config.AppSettings.Settings["Secondary"].Value);
            bool attachments = bool.Parse(config.AppSettings.Settings["Attachments"].Value);
            bool grips = bool.Parse(config.AppSettings.Settings["Grips"].Value);
            bool scopes = bool.Parse(config.AppSettings.Settings["Scopes"].Value);
            bool gadgetsEnabled = bool.Parse(config.AppSettings.Settings["Gadgets"].Value);
            if (!primary & !secondary & !attachments & !grips & !scopes & !gadgetsEnabled)
            {
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                richTextBox2.Visible = false;
            }
            else
            {
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                richTextBox2.Visible = true;
                for (int i = 3; i <= 9; i++)
                {
                    var label = this.Controls.Find($"label{i}", true)
                                             .FirstOrDefault() as System.Windows.Forms.Label;


                    label.ForeColor = Color.Gray;

                }
            }

            LoadHelpText();
            _proc = HookCallback;
            using (var process = Process.GetCurrentProcess())
            using (var module = process.MainModule)
            {
                _hookID = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, GetModuleHandle(module.ModuleName), 0);
            }
            this.KeyPreview = true;
            Btn_Attackers.ForeColor = Color.Tomato;
            Btn_Attackers.BackColor = Color.FromArgb(20, 20, 20);
            Btn_Defenders.BackColor = Color.FromArgb(20, 20, 20);
            Btn_Randomize.BackColor = Color.FromArgb(20, 20, 20);
            Btn_Settings.BackColor = Color.FromArgb(20, 20, 20);
            closeBtn.BackColor = Color.FromArgb(20, 20, 20);
            ResetRandomized.BackColor = Color.FromArgb(20, 20, 20);
            Btn_Help.BackColor = Color.FromArgb(20, 20, 20);
            richTextBox1.BackColor = Color.FromArgb(20, 20, 20);
            textBox1.BackColor = Color.FromArgb(20, 20, 20);
            richTextBox2.BackColor = Color.FromArgb(20, 20, 20);

            label3.BackColor = Color.FromArgb(20, 20, 20);
            label4.BackColor = Color.FromArgb(20, 20, 20);
            label5.BackColor = Color.FromArgb(20, 20, 20);
            label6.BackColor = Color.FromArgb(20, 20, 20);
            label7.BackColor = Color.FromArgb(20, 20, 20);
            label8.BackColor = Color.FromArgb(20, 20, 20);
            label9.BackColor = Color.FromArgb(20, 20, 20);


            if (File.Exists("res/operators.json"))
            {
                var json = File.ReadAllText("res/operators.json");
                _operators = JsonConvert.DeserializeObject<List<OperatorInfo>>(json);
                Form2.LoadOperatorStatesFromConfig(_operators);
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
                ? enabledOperators.Where(op => op.Role == "attacker").ToList()
                : enabledOperators.Where(op => op.Role == "defender").ToList();

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


            string randomGun = "No Gun";
            string randomAttachment = "No Attachment";
            string randomSecondary = "No Secondary";
            string randomSecondaryattachment = "No Attachment";
            string randomGrip = "No Grip";
            string randomScope = "No Scope";
            string randomGadget = "No Gadget";


            var primaryWeapons = randomOperator.Weapons.Where(w => w.WeaponType == "primary").ToList();
            if (primaryWeapons.Any())
            {
                var randomPrimaryWeapon = primaryWeapons[_random.Next(primaryWeapons.Count)];
                randomGun = randomPrimaryWeapon.WeaponName;


                if (randomPrimaryWeapon.Attachments.Any())
                {
                    randomAttachment = randomPrimaryWeapon.Attachments[_random.Next(randomPrimaryWeapon.Attachments.Count)];
                }
                if (randomPrimaryWeapon.Grips.Any())
                {
                    randomGrip = randomPrimaryWeapon.Grips[_random.Next(randomPrimaryWeapon.Grips.Count)];
                }
                if (randomPrimaryWeapon.Scopes.Any())
                {
                    randomScope = randomPrimaryWeapon.Scopes[_random.Next(randomPrimaryWeapon.Scopes.Count)];
                }

            }
            var secondaryWeapons = randomOperator.Weapons.Where(w => w.WeaponType == "secondary").ToList();
            if (secondaryWeapons.Any())
            {
                var randomSecondaryWeapon = secondaryWeapons[_random.Next(secondaryWeapons.Count)];
                randomSecondary = randomSecondaryWeapon.WeaponName;
                if (randomSecondaryWeapon.Attachments.Any())
                {
                    randomSecondaryattachment = randomSecondaryWeapon.Attachments[_random.Next(randomSecondaryWeapon.Attachments.Count)];
                }
            }
            var gadgets = randomOperator.Gadgets;
            if (gadgets.Any())
            {
                randomGadget = gadgets[_random.Next(gadgets.Count)].GadgetName;
            }

            if (randomScope.StartsWith("Holo") || randomScope.StartsWith("Reflex") || randomScope.StartsWith("Red Dot"))
            {
                randomScope = "1.0x";
            }
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            bool primary = bool.Parse(config.AppSettings.Settings["Primary"].Value);
            bool secondary = bool.Parse(config.AppSettings.Settings["Secondary"].Value);
            bool attachments = bool.Parse(config.AppSettings.Settings["Attachments"].Value);
            bool grips = bool.Parse(config.AppSettings.Settings["Grips"].Value);
            bool scopes = bool.Parse(config.AppSettings.Settings["Scopes"].Value);
            bool gadgetsEnabled = bool.Parse(config.AppSettings.Settings["Gadgets"].Value);
            
            if (!primary & !secondary & !attachments & !grips & !scopes & !gadgetsEnabled)
            {
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                richTextBox2.Visible = false;
            }
            else
            {
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                richTextBox2.Visible = true;
            }
            if (!primary)
            {
                randomGun = "N/A";
                label3.ForeColor = Color.Gray;
            }
            else
            {
                label3.ForeColor = Color.LightGreen;
            }
            if (!secondary)
            {
                randomSecondary = "N/A";
                label7.ForeColor = Color.Gray;
            }
            else
            {
                label7.ForeColor = Color.LightGreen;
            }
            if (!attachments)
            {
                randomAttachment = "N/A";
                randomSecondaryattachment = "N/A";
                label4.ForeColor = Color.Gray;
                label8.ForeColor = Color.Gray;
            }
            else
            {
                label4.ForeColor = Color.LightGreen;
                label8.ForeColor = Color.LightGreen;
            }
            if (!grips)
            {
                randomGrip = "N/A";
                label5.ForeColor = Color.Gray;
            }
            else
            {
                label5.ForeColor = Color.LightGreen;
            }
            if (!scopes)
            {
                randomScope = "N/A";
                label6.ForeColor = Color.Gray;
            }
            else
            {
                label6.ForeColor = Color.LightGreen;
            }
            if (!gadgetsEnabled)
            {
                randomGadget = "N/A";
                label9.ForeColor = Color.Gray;
            }
            else
            {
                label9.ForeColor = Color.LightGreen;
            }
            if (!secondary)
            {
                randomSecondaryattachment = "N/A";
                label8.ForeColor = Color.Gray;
            }



            label1.Text = $"Randomized: {randomizedList.Count}";
            label2.Text = $"Last: {randomOperator.Name}";

            label3.Text = $"Gun: {randomGun}";
            label4.Text = $"Attachment: {randomAttachment}";
            label5.Text = $"Grip: {randomGrip}";
            label6.Text = $"Scope: {randomScope}";
            label7.Text = $"Secondary: {randomSecondary}";
            label8.Text = $"Attachment: {randomSecondaryattachment}";
            label9.Text = $"Gadget: {randomGadget}";


            if (attackers)
            {
                lastAttacker = randomOperator.Name;
                attackerCount = randomizedList.Count.ToString();
                lastAttachment = label4.Text;
                lastPrimary = label3.Text;
                lastGrip = label5.Text;
                lastScope = label6.Text;
                lastSecondary = label7.Text;
                lastSecondaryAttachment = label8.Text;
                lastGadget = label9.Text;

            }
            else
            {
                lastDefender = randomOperator.Name;
                defenderCount = randomizedList.Count.ToString();
                lastDefenderAttachment = label4.Text;
                lastDefenderPrimary = label3.Text;
                lastDefenderGrip = label5.Text;
                lastDefenderScope = label6.Text;
                lastDefenderSecondary = label7.Text;
                lastDefenderSecondaryAttachment = label8.Text;
                lastDefenderGadget = label9.Text;
            }


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
            label1.Text = $"Randomized: {randomizedAttackers.Count}";
            label2.Text = $"Last: {lastAttacker}";
            label3.Text = $"{lastPrimary}";
            label4.Text = $"{lastAttachment}";
            label5.Text = $"{lastGrip}";
            label6.Text = $"{lastScope}";
            label7.Text = $"{lastSecondary}";
            label8.Text = $"{lastSecondaryAttachment}";
            label9.Text = $"{lastGadget}";

            for (int i = 3; i <= 9; i++)
            {
                var label = this.Controls.Find($"label{i}", true)
                                         .FirstOrDefault() as System.Windows.Forms.Label;


                if (label.Text.Contains("N/A"))
                {
                    label.ForeColor = Color.Gray;
                }
                else
                {
                    label.ForeColor = Color.LightGreen;
                }

            }
            if (!string.IsNullOrEmpty(lastAttacker))
            {
                var lastOp = _operators.FirstOrDefault(op => op.Name == lastAttacker && op.Role == "attacker");
                if (lastOp != null && File.Exists(lastOp.ImagePath))
                {
                    
                    using (var stream = new MemoryStream(File.ReadAllBytes(lastOp.ImagePath)))
                    {
                        pictureBox1.Image = Image.FromStream(stream);
                    }
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void Btn_Defenders_Click(object sender, EventArgs e)
        {
            attackers = false;
            Btn_Defenders.ForeColor = Color.Tomato;
            Btn_Attackers.ForeColor = Color.White;
            label1.Text = $"Randomized: {randomizedDefenders.Count}";
            label2.Text = $"Last: {lastDefender}";
            label3.Text = $"{lastDefenderPrimary}";
            label4.Text = $"{lastDefenderAttachment}";
            label5.Text = $"{lastDefenderGrip}";
            label6.Text = $"{lastDefenderScope}";
            label7.Text = $"{lastDefenderSecondary}";
            label8.Text = $"{lastDefenderSecondaryAttachment}";
            label9.Text = $"{lastDefenderGadget}";
            for (int i = 3; i <= 9; i++)
            {
                var label = this.Controls.Find($"label{i}", true)
                                         .FirstOrDefault() as System.Windows.Forms.Label;


                if (label.Text.Contains("N/A"))
                {
                    label.ForeColor = Color.Gray;
                }
                else
                {
                    label.ForeColor = Color.LightGreen;
                }

            }
            if (!string.IsNullOrEmpty(lastDefender))
            {
                var lastOp = _operators.FirstOrDefault(op => op.Name == lastDefender && op.Role == "defender");
                if (lastOp != null && File.Exists(lastOp.ImagePath))
                {
                    
                    using (var stream = new MemoryStream(File.ReadAllBytes(lastOp.ImagePath)))
                    {
                        pictureBox1.Image = Image.FromStream(stream);
                    }
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }

        }

        private void ResetRandomized_Click(object sender, EventArgs e)
        {
            randomizedAttackers.Clear();
            randomizedDefenders.Clear();
            label3.Text = $"Gun: N/A";
            label4.Text = $"Attachment: N/A";
            label5.Text = $"Grip: N/A";
            label6.Text = $"Scope: N/A";
            label7.Text = $"Secondary: N/A";
            label8.Text = $"Attachment: N/A";
            label9.Text = $"Gadget: N/A";
            lastAttacker = "";
            lastDefender = "";
            defenderCount = "";
            attackerCount = "";
            label1.Text = $"Randomized: ";
            label2.Text = $"Last: ";
            lastPrimary = "Gun: N/A";
            lastAttachment = "Attachment: N/A";
            lastGrip = "Grip: N/A";
            lastScope = "Scope: N/A";
            lastSecondary = "Secondary: N/A";
            lastSecondaryAttachment = "Attachment: N/A";
            lastGadget = "Gadget: N/A";
            lastDefenderPrimary = "Gun: N/A";
            lastDefenderAttachment = "Attachment: N/A";
            lastDefenderGrip = "Grip: N/A";
            lastDefenderScope = "Scope: N/A";
            lastDefenderSecondary = "Secondary: N/A";
            lastDefenderSecondaryAttachment = "Attachment: N/A";
            lastDefenderGadget = "Gadget: N/A";
            pictureBox1.Image = null;
            for (int i = 3; i <= 9; i++)
            {
                var label = this.Controls.Find($"label{i}", true)
                                         .FirstOrDefault() as System.Windows.Forms.Label;


                label.ForeColor = Color.Gray;

            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;

                // Check for Shift + F5 key press
                if (key == Keys.F5 && (GetAsyncKeyState(Keys.ShiftKey) & 0x8000) != 0)
                {
                    ToggleMinimizeRestore();
                    return (IntPtr)1; // Indicate that we've processed the key
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void ToggleMinimizeRestore()
        {
            if (!this.Visible)
            {
                // Move to bottom-right corner
                var screen = Screen.PrimaryScreen.WorkingArea;
                this.Location = new Point(
                    screen.Right - this.Width,
                    screen.Top
                );

                this.Visible = true;
                RemoveClickThrough(); // Make it interactable
                this.Activate();
            }
            else
            {
                MakeClickThrough(); // Prevent interactions
                this.Visible = false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            var screen = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(
                screen.Right - this.Width,
                screen.Top
            );


        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
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
                new { Header = "Rainbow Six Siege Randomizer – Help", SubHeader = "Overview", Body = "The R6 Randomizer is your go-to companion for Rainbow Six Siege, helping you pick an operator at random. It's a handy tool to keep your gameplay fresh and unpredictable." },
                new { Header = "Key Features", SubHeader = "", Body = "" },
                new { Header = "", SubHeader = "Randomize Button", Body = "Hit the Randomize button to get a random operator from your enabled list. The chosen operator's portrait and details will be displayed." },
                new { Header = "", SubHeader = "Operator Management", Body = "Click 'Settings' to open the full operator roster. Every operator icon has a border to show its status: a **green** border means they're active in your pool, while a **red** border means they're disabled. Just click an icon to toggle its state. The changes save automatically." },
                new { Header = "", SubHeader = "In-Game Overlay", Body = "Use the hotkey **Shift + F5** to toggle the app's visibility while in-game. This feature works best with Rainbow Six Siege running in **borderless** windowed mode, as it may not function reliably in exclusive fullscreen." },
                new { Header = "", SubHeader = "Customizable Roster", Body = "By default, all operators are included in the randomization pool. You have full control to disable any operator you don't want to play." },
                new { Header = "", SubHeader = "Troubleshooting", Body = "If you encounter an issue, like a missing operator image, the app will let you know. If the app can't find an image, the icon will have a red background. You also need to enable at least one operator for the randomizer to work." },
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

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}