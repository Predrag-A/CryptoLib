namespace CryptoApp
{
    partial class MainForm
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
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fSWOnOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.fSWInputFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fSWOutputFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algorithmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doubleTranspositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xTEAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oFBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.knapsackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mD5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fbdInput = new System.Windows.Forms.FolderBrowserDialog();
            this.fbdOutput = new System.Windows.Forms.FolderBrowserDialog();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.algorithmToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(800, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fSWOnOffToolStripMenuItem,
            this.toolStripMenuItem1,
            this.fSWInputFolderToolStripMenuItem,
            this.fSWOutputFolderToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // fSWOnOffToolStripMenuItem
            // 
            this.fSWOnOffToolStripMenuItem.Name = "fSWOnOffToolStripMenuItem";
            this.fSWOnOffToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fSWOnOffToolStripMenuItem.Text = "FSW On/Off";
            this.fSWOnOffToolStripMenuItem.Click += new System.EventHandler(this.fSWOnOffToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // fSWInputFolderToolStripMenuItem
            // 
            this.fSWInputFolderToolStripMenuItem.Name = "fSWInputFolderToolStripMenuItem";
            this.fSWInputFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fSWInputFolderToolStripMenuItem.Text = "FSW Input Folder";
            this.fSWInputFolderToolStripMenuItem.Click += new System.EventHandler(this.fSWInputFolderToolStripMenuItem_Click);
            // 
            // fSWOutputFolderToolStripMenuItem
            // 
            this.fSWOutputFolderToolStripMenuItem.Name = "fSWOutputFolderToolStripMenuItem";
            this.fSWOutputFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fSWOutputFolderToolStripMenuItem.Text = "FSW Output Folder";
            this.fSWOutputFolderToolStripMenuItem.Click += new System.EventHandler(this.fSWOutputFolderToolStripMenuItem_Click);
            // 
            // algorithmToolStripMenuItem
            // 
            this.algorithmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.doubleTranspositionToolStripMenuItem,
            this.xTEAToolStripMenuItem,
            this.oFBToolStripMenuItem,
            this.knapsackToolStripMenuItem,
            this.mD5ToolStripMenuItem});
            this.algorithmToolStripMenuItem.Name = "algorithmToolStripMenuItem";
            this.algorithmToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.algorithmToolStripMenuItem.Text = "Algorithm";
            // 
            // doubleTranspositionToolStripMenuItem
            // 
            this.doubleTranspositionToolStripMenuItem.Name = "doubleTranspositionToolStripMenuItem";
            this.doubleTranspositionToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.doubleTranspositionToolStripMenuItem.Text = "Double Transposition";
            this.doubleTranspositionToolStripMenuItem.Click += new System.EventHandler(this.doubleTranspositionToolStripMenuItem_Click);
            // 
            // xTEAToolStripMenuItem
            // 
            this.xTEAToolStripMenuItem.Name = "xTEAToolStripMenuItem";
            this.xTEAToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.xTEAToolStripMenuItem.Text = "XTEA";
            this.xTEAToolStripMenuItem.Click += new System.EventHandler(this.xTEAToolStripMenuItem_Click);
            // 
            // oFBToolStripMenuItem
            // 
            this.oFBToolStripMenuItem.Name = "oFBToolStripMenuItem";
            this.oFBToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.oFBToolStripMenuItem.Text = "OFB";
            this.oFBToolStripMenuItem.Click += new System.EventHandler(this.oFBToolStripMenuItem_Click);
            // 
            // knapsackToolStripMenuItem
            // 
            this.knapsackToolStripMenuItem.Name = "knapsackToolStripMenuItem";
            this.knapsackToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.knapsackToolStripMenuItem.Text = "Knapsack";
            this.knapsackToolStripMenuItem.Click += new System.EventHandler(this.knapsackToolStripMenuItem_Click);
            // 
            // mD5ToolStripMenuItem
            // 
            this.mD5ToolStripMenuItem.Name = "mD5ToolStripMenuItem";
            this.mD5ToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.mD5ToolStripMenuItem.Text = "MD5";
            this.mD5ToolStripMenuItem.Click += new System.EventHandler(this.mD5ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "MainForm";
            this.Text = "Crypto Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fSWInputFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fSWOutputFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fSWOnOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem algorithmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doubleTranspositionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xTEAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oFBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem knapsackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mD5ToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog fbdInput;
        private System.Windows.Forms.FolderBrowserDialog fbdOutput;
    }
}

