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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fSWOnOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.fSWInputFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fSWOutputFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.encryptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decryptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algorithmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doubleTranspositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xTEAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.knapsackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mD5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.parametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloudToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fbdInput = new System.Windows.Forms.FolderBrowserDialog();
            this.fbdOutput = new System.Windows.Forms.FolderBrowserDialog();
            this.inputText = new System.Windows.Forms.TextBox();
            this.outputText = new System.Windows.Forms.TextBox();
            this.encryptBtn = new System.Windows.Forms.Button();
            this.decryptBtn = new System.Windows.Forms.Button();
            this.swapBtn = new System.Windows.Forms.Button();
            this.lblInput = new System.Windows.Forms.Label();
            this.lblOutput = new System.Windows.Forms.Label();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.algorithmToolStripMenuItem,
            this.cloudToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(804, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fSWOnOffToolStripMenuItem,
            this.toolStripMenuItem1,
            this.fSWInputFolderToolStripMenuItem,
            this.fSWOutputFolderToolStripMenuItem,
            this.toolStripMenuItem2,
            this.encryptToolStripMenuItem,
            this.decryptToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(125, 20);
            this.fileToolStripMenuItem.Text = "File System Watcher";
            // 
            // fSWOnOffToolStripMenuItem
            // 
            this.fSWOnOffToolStripMenuItem.Name = "fSWOnOffToolStripMenuItem";
            this.fSWOnOffToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.fSWOnOffToolStripMenuItem.Text = "On/Off";
            this.fSWOnOffToolStripMenuItem.Click += new System.EventHandler(this.fSWOnOffToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(145, 6);
            // 
            // fSWInputFolderToolStripMenuItem
            // 
            this.fSWInputFolderToolStripMenuItem.Name = "fSWInputFolderToolStripMenuItem";
            this.fSWInputFolderToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.fSWInputFolderToolStripMenuItem.Text = "Input Folder";
            this.fSWInputFolderToolStripMenuItem.Click += new System.EventHandler(this.fSWInputFolderToolStripMenuItem_Click);
            // 
            // fSWOutputFolderToolStripMenuItem
            // 
            this.fSWOutputFolderToolStripMenuItem.Name = "fSWOutputFolderToolStripMenuItem";
            this.fSWOutputFolderToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.fSWOutputFolderToolStripMenuItem.Text = "Output Folder";
            this.fSWOutputFolderToolStripMenuItem.Click += new System.EventHandler(this.fSWOutputFolderToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(145, 6);
            // 
            // encryptToolStripMenuItem
            // 
            this.encryptToolStripMenuItem.Name = "encryptToolStripMenuItem";
            this.encryptToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.encryptToolStripMenuItem.Text = "Encrypt";
            this.encryptToolStripMenuItem.Click += new System.EventHandler(this.encryptToolStripMenuItem_Click);
            // 
            // decryptToolStripMenuItem
            // 
            this.decryptToolStripMenuItem.Name = "decryptToolStripMenuItem";
            this.decryptToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.decryptToolStripMenuItem.Text = "Decrypt";
            this.decryptToolStripMenuItem.Click += new System.EventHandler(this.decryptToolStripMenuItem_Click);
            // 
            // algorithmToolStripMenuItem
            // 
            this.algorithmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.doubleTranspositionToolStripMenuItem,
            this.xTEAToolStripMenuItem,
            this.knapsackToolStripMenuItem,
            this.mD5ToolStripMenuItem,
            this.toolStripMenuItem3,
            this.parametersToolStripMenuItem});
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
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(183, 6);
            // 
            // parametersToolStripMenuItem
            // 
            this.parametersToolStripMenuItem.Name = "parametersToolStripMenuItem";
            this.parametersToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.parametersToolStripMenuItem.Text = "Parameters";
            this.parametersToolStripMenuItem.Click += new System.EventHandler(this.parametersToolStripMenuItem_Click);
            // 
            // cloudToolStripMenuItem
            // 
            this.cloudToolStripMenuItem.Name = "cloudToolStripMenuItem";
            this.cloudToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.cloudToolStripMenuItem.Text = "Cloud";
            this.cloudToolStripMenuItem.ToolTipText = "Cloud Client";
            this.cloudToolStripMenuItem.Click += new System.EventHandler(this.cloudToolStripMenuItem_Click);
            // 
            // inputText
            // 
            this.inputText.Location = new System.Drawing.Point(12, 85);
            this.inputText.Multiline = true;
            this.inputText.Name = "inputText";
            this.inputText.Size = new System.Drawing.Size(326, 164);
            this.inputText.TabIndex = 1;
            // 
            // outputText
            // 
            this.outputText.Enabled = false;
            this.outputText.Location = new System.Drawing.Point(466, 85);
            this.outputText.Multiline = true;
            this.outputText.Name = "outputText";
            this.outputText.Size = new System.Drawing.Size(326, 164);
            this.outputText.TabIndex = 2;
            // 
            // encryptBtn
            // 
            this.encryptBtn.Location = new System.Drawing.Point(364, 98);
            this.encryptBtn.Name = "encryptBtn";
            this.encryptBtn.Size = new System.Drawing.Size(75, 23);
            this.encryptBtn.TabIndex = 3;
            this.encryptBtn.Text = "Encrypt";
            this.encryptBtn.UseVisualStyleBackColor = true;
            this.encryptBtn.Click += new System.EventHandler(this.encryptBtn_Click);
            // 
            // decryptBtn
            // 
            this.decryptBtn.Location = new System.Drawing.Point(364, 127);
            this.decryptBtn.Name = "decryptBtn";
            this.decryptBtn.Size = new System.Drawing.Size(75, 23);
            this.decryptBtn.TabIndex = 4;
            this.decryptBtn.Text = "Decrypt";
            this.decryptBtn.UseVisualStyleBackColor = true;
            this.decryptBtn.Click += new System.EventHandler(this.decryptBtn_Click);
            // 
            // swapBtn
            // 
            this.swapBtn.Location = new System.Drawing.Point(364, 157);
            this.swapBtn.Name = "swapBtn";
            this.swapBtn.Size = new System.Drawing.Size(75, 23);
            this.swapBtn.TabIndex = 5;
            this.swapBtn.Text = "Swap";
            this.swapBtn.UseVisualStyleBackColor = true;
            this.swapBtn.Click += new System.EventHandler(this.swapBtn_Click);
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInput.Location = new System.Drawing.Point(130, 56);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(60, 26);
            this.lblInput.TabIndex = 6;
            this.lblInput.Text = "Input";
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutput.Location = new System.Drawing.Point(589, 56);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(77, 26);
            this.lblOutput.TabIndex = 7;
            this.lblOutput.Text = "Output";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 265);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.lblInput);
            this.Controls.Add(this.swapBtn);
            this.Controls.Add(this.decryptBtn);
            this.Controls.Add(this.encryptBtn);
            this.Controls.Add(this.outputText);
            this.Controls.Add(this.inputText);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Crypto Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
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
        private System.Windows.Forms.ToolStripMenuItem knapsackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mD5ToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog fbdInput;
        private System.Windows.Forms.FolderBrowserDialog fbdOutput;
        private System.Windows.Forms.TextBox inputText;
        private System.Windows.Forms.TextBox outputText;
        private System.Windows.Forms.Button encryptBtn;
        private System.Windows.Forms.Button decryptBtn;
        private System.Windows.Forms.Button swapBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem encryptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decryptToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem parametersToolStripMenuItem;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.ToolStripMenuItem cloudToolStripMenuItem;
    }
}

