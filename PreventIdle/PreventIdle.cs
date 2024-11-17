// ==================================================================================
// Program   : PreventIdle
// Version   : 1.0.0.0
// Author    : Thieren Jochen
// Date      : November 2024
// ----------------------------------------------------------------------------------
// Description:
// PreventIdle is a lightweight tool designed to keep your PC active and prevent it
// from entering sleep or idle states while you wait for tasks to complete.
// It achieves this by utilizing the Windows API function `SetThreadExecutionState`
// to signal system activity and performing minor, imperceptible mouse movements
// to ensure the system remains active.
//
// The program is intentionally designed to operate without requiring administrative
// privileges. This avoids any need for creating log files, modifying system registry
// keys, or other operations that may require elevated permissions.
//
// This software is provided "as is," without any express or implied warranties.
// The author assumes no responsibility for errors, omissions, or any consequences
// arising from the use or misuse of this tool. Users accept full responsibility
// for using it at their own risk.
// ----------------------------------------------------------------------------------
// License:
// PreventIdle is released into the public domain. No fees or licenses are required
// for its use, modification, or distribution. Users are encouraged to adapt the
// code as needed but assume full responsibility for any outcomes. By using this
// software, you agree to these terms and conditions.
// ----------------------------------------------------------------------------------
// Notes:
// - This program was developed as a personal project and is shared for educational
//   and informational purposes. It may not adhere to production-grade standards
//   or industry best practices.
// - Feedback and suggestions for improvement are welcome but not mandatory. Enjoy!
// ==================================================================================

using PreventIdle.Objects;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PreventIdle
{
    public partial class frmPreventIdle : Form
    {
        private Timer exitTimer;  // Timer for auto-stopping and exiting
        private Timer remainingTimeTimer;  // Timer for updating lblRemainingTime
        private DateTime activationTime; //Timer for activation time
        private const int exitDuration = 600000; // Set to 10 minutes (600000 ms) or any duration in milliseconds
        private int remainingTimeInMilliseconds; // To track the remaining time for the exit

        private Random random = new Random();

        public frmPreventIdle()
        {
            InitializeComponent();

            // Set up NotifyIcon
            nifMin.Icon = Properties.Resources.Creative_Freedom_Shimmer_Recycle;
            nifMin.Text = "PreventIdle";
            nifMin.Visible = false;
            nifMin.DoubleClick += nifMin_DoubleClick;

            // Remove minimize and maximize buttons
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            // Initialize exit timer
            exitTimer = new Timer();
            exitTimer.Interval = exitDuration;  // Set duration in milliseconds
            exitTimer.Tick += ExitTimer_Tick;

            // Initialize remaining time timer
            remainingTimeTimer = new Timer();
            remainingTimeTimer.Interval = 1000;  // Update every second
            remainingTimeTimer.Tick += RemainingTimeTimer_Tick;

            lblRemainingTime.Text = "...";
        }

        private void chkStartPreventIdle_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStartPreventIdle.Checked)
            {
                _PreventIdle.StartKeepingAlive();
                activationTime = DateTime.Now; // Store the activation timestamp

                lblRemainingTime.Text = $"Activated at: {activationTime:HH:mm:ss}";

                // Start the timer only if a time value is selected
                if (cboTime.SelectedItem.ToString() != "KeepAlive")
                {
                    remainingTimeInMilliseconds = exitTimer.Interval;
                    exitTimer.Start();
                    remainingTimeTimer.Start(); // Start updating lblRemainingTime
                }
            }
            else
            {
                _PreventIdle.StopKeepingAlive();
                exitTimer.Stop();
                remainingTimeTimer.Stop();
                lblRemainingTime.Text = "...";
            }
        }

        private void ExitTimer_Tick(object sender, EventArgs e)
        {
            _PreventIdle.StopKeepingAlive();
            exitTimer.Stop();
            remainingTimeTimer.Stop();
            lblRemainingTime.Text = "Time's up!";

            if (chkUserLogOff.Checked)
            {
                System.Diagnostics.Process.Start("shutdown", "/l");
            }
            else
            {
                this.Close();
            }
        }

        private void RemainingTimeTimer_Tick(object sender, EventArgs e)
        {
            // Decrease the remaining time by 1 second (1000 ms)
            remainingTimeInMilliseconds -= 1000;

            // Calculate elapsed time
            TimeSpan elapsedTime = DateTime.Now - activationTime;

            if (remainingTimeInMilliseconds > 0)
            {
                TimeSpan remainingTime = TimeSpan.FromMilliseconds(remainingTimeInMilliseconds);
                lblRemainingTime.Text = $"Activated at: {activationTime:HH:mm:ss}\n" +
                                        $"Elapsed: {elapsedTime.Hours:D2}:{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}\n" +
                                        $"Remaining: {remainingTime.Hours:D2}:{remainingTime.Minutes:D2}:{remainingTime.Seconds:D2}";

                nifMin.Text = "PreventIdle " + $"T-{remainingTime.Hours:D2}:{remainingTime.Minutes:D2}:{remainingTime.Seconds:D2}"; //Only single line of text
            }
            else
            {
                remainingTimeTimer.Stop();
                lblRemainingTime.Text = "Time's up!";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.25; // Sets the opacity for the form, my personal choice is near invisible

            // Set the form's start position to manual
            this.StartPosition = FormStartPosition.Manual;

            // Get the screen's working area (excluding taskbar, etc.)
            var workingArea = Screen.PrimaryScreen.WorkingArea;

            // Calculate the desired position
            int x = workingArea.Width - this.Width - 5; // 150 pixels from the right
            int y = workingArea.Height - this.Height - 20; // 150 pixels from the bottom

            // Set the form's location
            this.Location = new Point(x, y);

            // Additional setup if needed
            cboTime.Items.Clear();
            cboTime.Items.Add("KeepAlive");
            cboTime.Items.Add("5 Mins");
            cboTime.Items.Add("15 Mins");
            cboTime.Items.Add("30 Mins");
            cboTime.Items.Add("45 Mins");
            cboTime.Items.Add("60 Mins");
            cboTime.Items.Add("90 Mins");
            cboTime.Items.Add("120 Mins");
            cboTime.Items.Add("180 Mins");
            cboTime.Items.Add("240 Mins");
            cboTime.Items.Add("300 Mins");
            cboTime.SelectedIndex = 0;
        }

        private void nifMin_DoubleClick(object sender, EventArgs e)
        {
            // Restore the window
            this.Visible = true;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            nifMin.Visible = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (this.WindowState == FormWindowState.Minimized)
            {
                // cmdToTray_Click(null, null);
            }
        }

        private void cboTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTime = cboTime.SelectedItem.ToString();
            int baseTimeInMinutes = 0;

            // Set base time for each selection
            switch (selectedTime)
            {
                case "5 Mins":
                    baseTimeInMinutes = 5;
                    break;

                case "15 Mins":
                    baseTimeInMinutes = 15;
                    break;

                case "30 Mins":
                    baseTimeInMinutes = 30;
                    break;

                case "45 Mins":
                    baseTimeInMinutes = 45;
                    break;

                case "60 Mins":
                    baseTimeInMinutes = 60;
                    break;

                case "90 Mins":
                    baseTimeInMinutes = 90;
                    break;

                case "120 Mins":
                    baseTimeInMinutes = 120;
                    break;

                case "180 Mins":
                    baseTimeInMinutes = 180;
                    break;

                case "240 Mins":
                    baseTimeInMinutes = 240;
                    break;

                case "300 Mins":
                    baseTimeInMinutes = 300;
                    break;

                default:
                    baseTimeInMinutes = 0; // No auto-exit for "KeepAlive"
                    break;
            }

            int randomOffset = random.Next(3, 10);
            int totalTimeInMilliseconds = (baseTimeInMinutes + randomOffset) * 60 * 1000;

            if (totalTimeInMilliseconds > 0)
            {
                exitTimer.Interval = totalTimeInMilliseconds;
                remainingTimeInMilliseconds = totalTimeInMilliseconds;
            }
            else
            {
                exitTimer.Stop();
                remainingTimeTimer.Stop();
                lblRemainingTime.Text = "...";
            }
        }

        private void frmPreventIdle_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop the keep-alive feature if active
            _PreventIdle.StopKeepingAlive();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Check if the pressed key is Space, any arrow key, or Shift
            if (keyData == Keys.Space ||
                keyData == Keys.Left ||
                keyData == Keys.Right ||
                keyData == Keys.Up ||
                keyData == Keys.Down ||
                keyData == Keys.Shift)
            {
                // Reset the exit timer
                exitTimer.Stop();
                exitTimer.Start();
                remainingTimeInMilliseconds = exitTimer.Interval;  // Reset remaining time
                activationTime = DateTime.Now; // Reset the activation time
                                               //MessageBox.Show("Timer reset due to key press.", "Timer Reset");

                // Update the label to reflect the reset state
                lblRemainingTime.Text = $"Activated at: {activationTime:HH:mm:ss}\n" +
                                        "Elapsed: 00:00:00\n" +
                                        $"Remaining: {TimeSpan.FromMilliseconds(remainingTimeInMilliseconds).ToString(@"hh\:mm\:ss")}";
                nifMin.Text = "PreventIdle " + $"T-{TimeSpan.FromMilliseconds(remainingTimeInMilliseconds).ToString(@"hh\:mm\:ss")}";

                return true; // Indicate that the key press has been handled
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void lblRemainingTime_Click(object sender, EventArgs e)
        {
            if (chkStartPreventIdle.Checked)
            {
                // Minimize to tray
                this.Visible = false;         // Hide the form
                this.ShowInTaskbar = false;   // Remove from taskbar
                nifMin.Visible = true;        // Show tray icon
            }
        }
    }
}