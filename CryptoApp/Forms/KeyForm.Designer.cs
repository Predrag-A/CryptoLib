namespace CryptoApp.Forms
{
    partial class KeyForm
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
            this.groupDT = new System.Windows.Forms.GroupBox();
            this.btnKeyDT = new System.Windows.Forms.Button();
            this.txtKeyRowDT = new System.Windows.Forms.TextBox();
            this.txtKeyColumnDT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupXTEA = new System.Windows.Forms.GroupBox();
            this.lbl3 = new System.Windows.Forms.Label();
            this.numRoundsXTEA = new System.Windows.Forms.NumericUpDown();
            this.btnParamXTEA = new System.Windows.Forms.Button();
            this.btnKeyXTEA = new System.Windows.Forms.Button();
            this.txtKeyXTEA = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupOFB = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelTBD = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numKS7 = new System.Windows.Forms.NumericUpDown();
            this.numKS6 = new System.Windows.Forms.NumericUpDown();
            this.numKS5 = new System.Windows.Forms.NumericUpDown();
            this.numKS4 = new System.Windows.Forms.NumericUpDown();
            this.numKS3 = new System.Windows.Forms.NumericUpDown();
            this.numKS2 = new System.Windows.Forms.NumericUpDown();
            this.numKS1 = new System.Windows.Forms.NumericUpDown();
            this.numKS0 = new System.Windows.Forms.NumericUpDown();
            this.btnKeyKS = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnRandom = new System.Windows.Forms.Button();
            this.groupDT.SuspendLayout();
            this.groupXTEA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRoundsXTEA)).BeginInit();
            this.groupOFB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numKS7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKS6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKS5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKS4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKS3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKS2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKS1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKS0)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupDT
            // 
            this.groupDT.Controls.Add(this.btnKeyDT);
            this.groupDT.Controls.Add(this.txtKeyRowDT);
            this.groupDT.Controls.Add(this.txtKeyColumnDT);
            this.groupDT.Controls.Add(this.label2);
            this.groupDT.Controls.Add(this.label1);
            this.groupDT.Location = new System.Drawing.Point(13, 13);
            this.groupDT.Name = "groupDT";
            this.groupDT.Size = new System.Drawing.Size(598, 91);
            this.groupDT.TabIndex = 0;
            this.groupDT.TabStop = false;
            this.groupDT.Text = "Double Transposition";
            // 
            // btnKeyDT
            // 
            this.btnKeyDT.Location = new System.Drawing.Point(496, 37);
            this.btnKeyDT.Name = "btnKeyDT";
            this.btnKeyDT.Size = new System.Drawing.Size(90, 23);
            this.btnKeyDT.TabIndex = 4;
            this.btnKeyDT.Text = "Set Key";
            this.btnKeyDT.UseVisualStyleBackColor = true;
            this.btnKeyDT.Click += new System.EventHandler(this.btnKeyDT_Click);
            // 
            // txtKeyRowDT
            // 
            this.txtKeyRowDT.Location = new System.Drawing.Point(115, 57);
            this.txtKeyRowDT.Name = "txtKeyRowDT";
            this.txtKeyRowDT.Size = new System.Drawing.Size(375, 20);
            this.txtKeyRowDT.TabIndex = 3;
            this.txtKeyRowDT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDT_KeyPress);
            // 
            // txtKeyColumnDT
            // 
            this.txtKeyColumnDT.Location = new System.Drawing.Point(115, 25);
            this.txtKeyColumnDT.Name = "txtKeyColumnDT";
            this.txtKeyColumnDT.Size = new System.Drawing.Size(375, 20);
            this.txtKeyColumnDT.TabIndex = 2;
            this.txtKeyColumnDT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDT_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Row Key";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Column Key";
            // 
            // groupXTEA
            // 
            this.groupXTEA.Controls.Add(this.lbl3);
            this.groupXTEA.Controls.Add(this.numRoundsXTEA);
            this.groupXTEA.Controls.Add(this.btnParamXTEA);
            this.groupXTEA.Controls.Add(this.btnKeyXTEA);
            this.groupXTEA.Controls.Add(this.txtKeyXTEA);
            this.groupXTEA.Controls.Add(this.label4);
            this.groupXTEA.Location = new System.Drawing.Point(12, 110);
            this.groupXTEA.Name = "groupXTEA";
            this.groupXTEA.Size = new System.Drawing.Size(599, 89);
            this.groupXTEA.TabIndex = 5;
            this.groupXTEA.TabStop = false;
            this.groupXTEA.Text = "XTEA";
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Location = new System.Drawing.Point(6, 55);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(44, 13);
            this.lbl3.TabIndex = 7;
            this.lbl3.Text = "Rounds";
            // 
            // numRoundsXTEA
            // 
            this.numRoundsXTEA.Location = new System.Drawing.Point(116, 55);
            this.numRoundsXTEA.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numRoundsXTEA.Name = "numRoundsXTEA";
            this.numRoundsXTEA.Size = new System.Drawing.Size(374, 20);
            this.numRoundsXTEA.TabIndex = 6;
            // 
            // btnParamXTEA
            // 
            this.btnParamXTEA.Enabled = false;
            this.btnParamXTEA.Location = new System.Drawing.Point(496, 55);
            this.btnParamXTEA.Name = "btnParamXTEA";
            this.btnParamXTEA.Size = new System.Drawing.Size(91, 23);
            this.btnParamXTEA.TabIndex = 5;
            this.btnParamXTEA.Text = "Set  Parameters";
            this.btnParamXTEA.UseVisualStyleBackColor = true;
            this.btnParamXTEA.Click += new System.EventHandler(this.btnParamXTEA_Click);
            // 
            // btnKeyXTEA
            // 
            this.btnKeyXTEA.Location = new System.Drawing.Point(496, 25);
            this.btnKeyXTEA.Name = "btnKeyXTEA";
            this.btnKeyXTEA.Size = new System.Drawing.Size(91, 23);
            this.btnKeyXTEA.TabIndex = 4;
            this.btnKeyXTEA.Text = "Set Key";
            this.btnKeyXTEA.UseVisualStyleBackColor = true;
            this.btnKeyXTEA.Click += new System.EventHandler(this.btnKeyXTEA_Click);
            // 
            // txtKeyXTEA
            // 
            this.txtKeyXTEA.Location = new System.Drawing.Point(115, 25);
            this.txtKeyXTEA.Name = "txtKeyXTEA";
            this.txtKeyXTEA.Size = new System.Drawing.Size(375, 20);
            this.txtKeyXTEA.TabIndex = 2;
            this.txtKeyXTEA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDT_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Key";
            // 
            // groupOFB
            // 
            this.groupOFB.Controls.Add(this.button1);
            this.groupOFB.Controls.Add(this.textBox1);
            this.groupOFB.Controls.Add(this.textBox2);
            this.groupOFB.Controls.Add(this.label3);
            this.groupOFB.Controls.Add(this.labelTBD);
            this.groupOFB.Location = new System.Drawing.Point(13, 205);
            this.groupOFB.Name = "groupOFB";
            this.groupOFB.Size = new System.Drawing.Size(598, 91);
            this.groupOFB.TabIndex = 6;
            this.groupOFB.TabStop = false;
            this.groupOFB.Text = "OFB Mode";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(496, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Set IV";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(115, 57);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(375, 20);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(115, 25);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(375, 20);
            this.textBox2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "TBD";
            // 
            // labelTBD
            // 
            this.labelTBD.AutoSize = true;
            this.labelTBD.Location = new System.Drawing.Point(6, 25);
            this.labelTBD.Name = "labelTBD";
            this.labelTBD.Size = new System.Drawing.Size(95, 13);
            this.labelTBD.TabIndex = 0;
            this.labelTBD.Text = "Initialization Vector";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numKS7);
            this.groupBox1.Controls.Add(this.numKS6);
            this.groupBox1.Controls.Add(this.numKS5);
            this.groupBox1.Controls.Add(this.numKS4);
            this.groupBox1.Controls.Add(this.numKS3);
            this.groupBox1.Controls.Add(this.numKS2);
            this.groupBox1.Controls.Add(this.numKS1);
            this.groupBox1.Controls.Add(this.numKS0);
            this.groupBox1.Controls.Add(this.btnKeyKS);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(13, 302);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(598, 137);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Knapsack";
            // 
            // numKS7
            // 
            this.numKS7.Location = new System.Drawing.Point(443, 19);
            this.numKS7.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numKS7.Name = "numKS7";
            this.numKS7.Size = new System.Drawing.Size(47, 20);
            this.numKS7.TabIndex = 12;
            // 
            // numKS6
            // 
            this.numKS6.Location = new System.Drawing.Point(390, 19);
            this.numKS6.Maximum = new decimal(new int[] {
            4999,
            0,
            0,
            0});
            this.numKS6.Name = "numKS6";
            this.numKS6.Size = new System.Drawing.Size(47, 20);
            this.numKS6.TabIndex = 11;
            // 
            // numKS5
            // 
            this.numKS5.Location = new System.Drawing.Point(337, 19);
            this.numKS5.Maximum = new decimal(new int[] {
            2499,
            0,
            0,
            0});
            this.numKS5.Name = "numKS5";
            this.numKS5.Size = new System.Drawing.Size(47, 20);
            this.numKS5.TabIndex = 10;
            // 
            // numKS4
            // 
            this.numKS4.Location = new System.Drawing.Point(284, 19);
            this.numKS4.Maximum = new decimal(new int[] {
            1249,
            0,
            0,
            0});
            this.numKS4.Name = "numKS4";
            this.numKS4.Size = new System.Drawing.Size(47, 20);
            this.numKS4.TabIndex = 9;
            // 
            // numKS3
            // 
            this.numKS3.Location = new System.Drawing.Point(231, 19);
            this.numKS3.Maximum = new decimal(new int[] {
            624,
            0,
            0,
            0});
            this.numKS3.Name = "numKS3";
            this.numKS3.Size = new System.Drawing.Size(47, 20);
            this.numKS3.TabIndex = 8;
            // 
            // numKS2
            // 
            this.numKS2.Location = new System.Drawing.Point(178, 19);
            this.numKS2.Maximum = new decimal(new int[] {
            311,
            0,
            0,
            0});
            this.numKS2.Name = "numKS2";
            this.numKS2.Size = new System.Drawing.Size(47, 20);
            this.numKS2.TabIndex = 7;
            // 
            // numKS1
            // 
            this.numKS1.Location = new System.Drawing.Point(125, 19);
            this.numKS1.Maximum = new decimal(new int[] {
            155,
            0,
            0,
            0});
            this.numKS1.Name = "numKS1";
            this.numKS1.Size = new System.Drawing.Size(47, 20);
            this.numKS1.TabIndex = 6;
            // 
            // numKS0
            // 
            this.numKS0.Location = new System.Drawing.Point(72, 19);
            this.numKS0.Maximum = new decimal(new int[] {
            77,
            0,
            0,
            0});
            this.numKS0.Name = "numKS0";
            this.numKS0.Size = new System.Drawing.Size(47, 20);
            this.numKS0.TabIndex = 5;
            // 
            // btnKeyKS
            // 
            this.btnKeyKS.Enabled = false;
            this.btnKeyKS.Location = new System.Drawing.Point(495, 16);
            this.btnKeyKS.Name = "btnKeyKS";
            this.btnKeyKS.Size = new System.Drawing.Size(90, 23);
            this.btnKeyKS.TabIndex = 4;
            this.btnKeyKS.Text = "Set Key";
            this.btnKeyKS.UseVisualStyleBackColor = true;
            this.btnKeyKS.Click += new System.EventHandler(this.btnKeyKS_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Private Key";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLbl});
            this.statusStrip.Location = new System.Drawing.Point(0, 471);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(621, 22);
            this.statusStrip.TabIndex = 8;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusLbl
            // 
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(39, 17);
            this.statusLbl.Text = "Status";
            // 
            // btnRandom
            // 
            this.btnRandom.Enabled = false;
            this.btnRandom.Location = new System.Drawing.Point(512, 445);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(99, 23);
            this.btnRandom.TabIndex = 9;
            this.btnRandom.Text = "Randomize Keys";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // KeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 493);
            this.Controls.Add(this.btnRandom);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupOFB);
            this.Controls.Add(this.groupXTEA);
            this.Controls.Add(this.groupDT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "KeyForm";
            this.Text = "Algorithm Parameters";
            this.groupDT.ResumeLayout(false);
            this.groupDT.PerformLayout();
            this.groupXTEA.ResumeLayout(false);
            this.groupXTEA.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRoundsXTEA)).EndInit();
            this.groupOFB.ResumeLayout(false);
            this.groupOFB.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numKS7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKS6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKS5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKS4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKS3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKS2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKS1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKS0)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupDT;
        private System.Windows.Forms.TextBox txtKeyRowDT;
        private System.Windows.Forms.TextBox txtKeyColumnDT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnKeyDT;
        private System.Windows.Forms.GroupBox groupXTEA;
        private System.Windows.Forms.Button btnKeyXTEA;
        private System.Windows.Forms.TextBox txtKeyXTEA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numRoundsXTEA;
        private System.Windows.Forms.Button btnParamXTEA;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.GroupBox groupOFB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelTBD;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnKeyKS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLbl;
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.NumericUpDown numKS7;
        private System.Windows.Forms.NumericUpDown numKS6;
        private System.Windows.Forms.NumericUpDown numKS5;
        private System.Windows.Forms.NumericUpDown numKS4;
        private System.Windows.Forms.NumericUpDown numKS3;
        private System.Windows.Forms.NumericUpDown numKS2;
        private System.Windows.Forms.NumericUpDown numKS1;
        private System.Windows.Forms.NumericUpDown numKS0;
    }
}