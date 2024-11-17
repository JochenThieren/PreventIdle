namespace PreventIdle
{
    partial class frmPreventIdle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPreventIdle));
            this.chkStartPreventIdle = new System.Windows.Forms.CheckBox();
            this.nifMin = new System.Windows.Forms.NotifyIcon(this.components);
            this.cboTime = new System.Windows.Forms.ComboBox();
            this.chkUserLogOff = new System.Windows.Forms.CheckBox();
            this.lblRemainingTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkStartPreventIdle
            // 
            this.chkStartPreventIdle.AutoSize = true;
            this.chkStartPreventIdle.Location = new System.Drawing.Point(6, 65);
            this.chkStartPreventIdle.Name = "chkStartPreventIdle";
            this.chkStartPreventIdle.Size = new System.Drawing.Size(56, 17);
            this.chkStartPreventIdle.TabIndex = 1;
            this.chkStartPreventIdle.Text = "Active";
            this.chkStartPreventIdle.UseVisualStyleBackColor = true;
            this.chkStartPreventIdle.CheckedChanged += new System.EventHandler(this.chkStartPreventIdle_CheckedChanged);
            // 
            // nifMin
            // 
            this.nifMin.Icon = ((System.Drawing.Icon)(resources.GetObject("nifMin.Icon")));
            this.nifMin.Text = "notifyIcon1";
            this.nifMin.Visible = true;
            this.nifMin.DoubleClick += new System.EventHandler(this.nifMin_DoubleClick);
            // 
            // cboTime
            // 
            this.cboTime.FormattingEnabled = true;
            this.cboTime.Location = new System.Drawing.Point(6, 12);
            this.cboTime.Name = "cboTime";
            this.cboTime.Size = new System.Drawing.Size(119, 21);
            this.cboTime.TabIndex = 3;
            this.cboTime.SelectedIndexChanged += new System.EventHandler(this.cboTime_SelectedIndexChanged);
            // 
            // chkUserLogOff
            // 
            this.chkUserLogOff.AutoSize = true;
            this.chkUserLogOff.Location = new System.Drawing.Point(6, 39);
            this.chkUserLogOff.Name = "chkUserLogOff";
            this.chkUserLogOff.Size = new System.Drawing.Size(80, 17);
            this.chkUserLogOff.TabIndex = 4;
            this.chkUserLogOff.Text = "User log off";
            this.chkUserLogOff.UseVisualStyleBackColor = true;
            // 
            // lblRemainingTime
            // 
            this.lblRemainingTime.ForeColor = System.Drawing.Color.DarkMagenta;
            this.lblRemainingTime.Location = new System.Drawing.Point(3, 85);
            this.lblRemainingTime.Name = "lblRemainingTime";
            this.lblRemainingTime.Size = new System.Drawing.Size(128, 44);
            this.lblRemainingTime.TabIndex = 5;
            this.lblRemainingTime.Text = "Start\r\nPassed\r\nRemTime";
            this.lblRemainingTime.Click += new System.EventHandler(this.lblRemainingTime_Click);
            // 
            // frmPreventIdle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(135, 130);
            this.Controls.Add(this.lblRemainingTime);
            this.Controls.Add(this.chkUserLogOff);
            this.Controls.Add(this.cboTime);
            this.Controls.Add(this.chkStartPreventIdle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPreventIdle";
            this.Text = "PreventIdle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPreventIdle_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chkStartPreventIdle;
        private System.Windows.Forms.NotifyIcon nifMin;
        private System.Windows.Forms.ComboBox cboTime;
        private System.Windows.Forms.CheckBox chkUserLogOff;
        private System.Windows.Forms.Label lblRemainingTime;
    }
}

