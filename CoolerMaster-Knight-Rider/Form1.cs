using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CoolerMaster_SDK_Test
{
    public partial class MainForm : Form
    {
        private static bool isActivateWhenWindowsLocked = false;
        private static EffectSettings settings;
        private static KnightRider knightRider;
        private static Color effectColor;
        private static Color bgColor;
        private static int[] rows;

        public MainForm()
        {
            InitializeComponent();

            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);

            this.notifyIcon1.ShowBalloonTip(1500);

            settings = new EffectSettings();
            rows = settings._effectRows;
            effectColor = Color.FromArgb(settings._color[0], settings._color[1], settings._color[2]);
            bgColor = Color.FromArgb(settings._backgroundColor[0], settings._backgroundColor[1], settings._backgroundColor[2]);
            knightRider = new KnightRider(effectColor, bgColor, rows);

            button1.BackColor = effectColor;
            button2.BackColor = bgColor;

            foreach (int i in settings._effectRows) {
                checkedListBox1.SetItemChecked(i - 1, true);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = colorDialog1.Color;
                effectColor = colorDialog1.Color;
                if (knightRider != null) {
                    knightRider.changeLedColor(effectColor, bgColor);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button2.BackColor = colorDialog1.Color;
                bgColor = colorDialog1.Color;
                if (knightRider != null)
                {
                    knightRider.changeLedColor(effectColor, bgColor);
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CoolerMasterDLL.EnableLedControl(false);
            knightRider = null;
        }

        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            settings._effectRows = new int[checkedListBox1.CheckedIndices.Count];
            for (int i = 0; i < checkedListBox1.CheckedIndices.Count; i++) {
                settings._effectRows[i] = checkedListBox1.CheckedIndices[i] + 1;
            }
            knightRider.changeUpdateRowIndex(settings._effectRows);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {                
                notifyIcon1.Visible = true;
                this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void menuItem1_Click(object Sender, EventArgs e)
        {
            this.Close();
        }

        static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (isActivateWhenWindowsLocked)
            {
                if (e.Reason == SessionSwitchReason.SessionLock)
                {
                    CoolerMasterDLL.EnableLedControl(true);
                    if (knightRider != null)
                    {
                        knightRider.start();
                    }
                    else
                    {
                        knightRider = new KnightRider(effectColor, bgColor, rows);
                    }
                }
                else
                {
                    CoolerMasterDLL.EnableLedControl(false);
                    if (knightRider != null)
                    {
                        knightRider.stop();
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            isActivateWhenWindowsLocked = (checkBox1.Checked);
            if (isActivateWhenWindowsLocked)
            {
                CoolerMasterDLL.EnableLedControl(false);
                if (knightRider != null)
                {
                    knightRider.stop();
                }
            }
            else
            {
                CoolerMasterDLL.EnableLedControl(true);
                if (knightRider != null)
                {
                    knightRider.start();
                }
                else {
                    knightRider = new KnightRider(effectColor, bgColor, rows);
                }
            }

        }
    }
}
