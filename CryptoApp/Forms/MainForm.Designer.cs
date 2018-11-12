﻿namespace CryptoApp
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
            this.inputText = new System.Windows.Forms.TextBox();
            this.outputText = new System.Windows.Forms.TextBox();
            this.encryptBtn = new System.Windows.Forms.Button();
            this.decryptBtn = new System.Windows.Forms.Button();
            this.swapBtn = new System.Windows.Forms.Button();
            this.keyBox = new System.Windows.Forms.TextBox();
            this.lblKey = new System.Windows.Forms.Label();
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
            this.fSWOutputFolderToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // fSWOnOffToolStripMenuItem
            // 
            this.fSWOnOffToolStripMenuItem.Name = "fSWOnOffToolStripMenuItem";
            this.fSWOnOffToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.fSWOnOffToolStripMenuItem.Text = "FSW On/Off";
            this.fSWOnOffToolStripMenuItem.Click += new System.EventHandler(this.fSWOnOffToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(171, 6);
            // 
            // fSWInputFolderToolStripMenuItem
            // 
            this.fSWInputFolderToolStripMenuItem.Name = "fSWInputFolderToolStripMenuItem";
            this.fSWInputFolderToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.fSWInputFolderToolStripMenuItem.Text = "FSW Input Folder";
            this.fSWInputFolderToolStripMenuItem.Click += new System.EventHandler(this.fSWInputFolderToolStripMenuItem_Click);
            // 
            // fSWOutputFolderToolStripMenuItem
            // 
            this.fSWOutputFolderToolStripMenuItem.Name = "fSWOutputFolderToolStripMenuItem";
            this.fSWOutputFolderToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
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
            // keyBox
            // 
            this.keyBox.Location = new System.Drawing.Point(12, 283);
            this.keyBox.Name = "keyBox";
            this.keyBox.Size = new System.Drawing.Size(326, 20);
            this.keyBox.TabIndex = 7;
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(127, 267);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(78, 13);
            this.lblKey.TabIndex = 8;
            this.lblKey.Text = "Encryption Key";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 315);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.keyBox);
            this.Controls.Add(this.swapBtn);
            this.Controls.Add(this.decryptBtn);
            this.Controls.Add(this.encryptBtn);
            this.Controls.Add(this.outputText);
            this.Controls.Add(this.inputText);
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
        private System.Windows.Forms.TextBox inputText;
        private System.Windows.Forms.TextBox outputText;
        private System.Windows.Forms.Button encryptBtn;
        private System.Windows.Forms.Button decryptBtn;
        private System.Windows.Forms.Button swapBtn;
        private System.Windows.Forms.TextBox keyBox;
        private System.Windows.Forms.Label lblKey;
    }
}

