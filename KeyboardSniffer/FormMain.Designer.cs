namespace KeyboardSniffer
{
    partial class FormMain
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
            ((System.IDisposable)kbdHook).Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonTopMost = new System.Windows.Forms.ToolStripButton();
            this.textBoxKeyLog = new System.Windows.Forms.TextBox();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.AllowItemReorder = true;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonClear,
            this.toolStripSeparator1,
            this.toolStripButtonCopy,
            this.toolStripButtonInfo,
            this.toolStripSeparator2,
            this.toolStripButtonTopMost});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(382, 25);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "Tool bar";
            // 
            // toolStripButtonClear
            // 
            this.toolStripButtonClear.Image = global::KeyboardSniffer.Properties.Resources.IconDelete;
            this.toolStripButtonClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClear.Name = "toolStripButtonClear";
            this.toolStripButtonClear.Size = new System.Drawing.Size(54, 22);
            this.toolStripButtonClear.Text = "Clear";
            this.toolStripButtonClear.Click += new System.EventHandler(this.toolStripButtonClear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonCopy
            // 
            this.toolStripButtonCopy.Image = global::KeyboardSniffer.Properties.Resources.IconCopy;
            this.toolStripButtonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCopy.Name = "toolStripButtonCopy";
            this.toolStripButtonCopy.Size = new System.Drawing.Size(55, 22);
            this.toolStripButtonCopy.Text = "Copy";
            this.toolStripButtonCopy.Click += new System.EventHandler(this.toolStripButtonCopy_Click);
            // 
            // toolStripButtonInfo
            // 
            this.toolStripButtonInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonInfo.Image = global::KeyboardSniffer.Properties.Resources.IconInformation;
            this.toolStripButtonInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInfo.Name = "toolStripButtonInfo";
            this.toolStripButtonInfo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonInfo.Text = "Info";
            this.toolStripButtonInfo.Click += new System.EventHandler(this.toolStripButtonInfo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonTopMost
            // 
            this.toolStripButtonTopMost.Image = global::KeyboardSniffer.Properties.Resources.IconLock;
            this.toolStripButtonTopMost.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTopMost.Name = "toolStripButtonTopMost";
            this.toolStripButtonTopMost.Size = new System.Drawing.Size(94, 22);
            this.toolStripButtonTopMost.Text = "Show on top";
            this.toolStripButtonTopMost.Click += new System.EventHandler(this.toolStripButtonTopMost_Click);
            // 
            // textBoxKeyLog
            // 
            this.textBoxKeyLog.CausesValidation = false;
            this.textBoxKeyLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxKeyLog.Location = new System.Drawing.Point(0, 25);
            this.textBoxKeyLog.Multiline = true;
            this.textBoxKeyLog.Name = "textBoxKeyLog";
            this.textBoxKeyLog.ReadOnly = true;
            this.textBoxKeyLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxKeyLog.Size = new System.Drawing.Size(382, 270);
            this.textBoxKeyLog.TabIndex = 1;
            this.textBoxKeyLog.WordWrap = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 295);
            this.Controls.Add(this.textBoxKeyLog);
            this.Controls.Add(this.toolStrip);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "Keyboard events";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonClear;
        private System.Windows.Forms.ToolStripButton toolStripButtonCopy;
        private System.Windows.Forms.TextBox textBoxKeyLog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonInfo;
        private System.Windows.Forms.ToolStripButton toolStripButtonTopMost;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

