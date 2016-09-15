namespace MathDraw {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.drawBtn = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.randomizeCheckBox = new System.Windows.Forms.CheckBox();
            this.randomizeBtn = new System.Windows.Forms.Button();
            this.formula = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.charmod = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fontColorBtn = new System.Windows.Forms.Button();
            this.backgroundBtn = new System.Windows.Forms.Button();
            this.fontBtn = new System.Windows.Forms.Button();
            this.maxNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.minNum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabsHolder = new System.Windows.Forms.TabControl();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawBtn
            // 
            this.drawBtn.AutoSize = true;
            this.drawBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.drawBtn.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.drawBtn.Location = new System.Drawing.Point(0, 517);
            this.drawBtn.Name = "drawBtn";
            this.drawBtn.Size = new System.Drawing.Size(775, 39);
            this.drawBtn.TabIndex = 0;
            this.drawBtn.Text = "draw";
            this.drawBtn.UseVisualStyleBackColor = true;
            this.drawBtn.Click += new System.EventHandler(this.drawBtn_Clicked);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabsHolder);
            this.splitContainer1.Size = new System.Drawing.Size(775, 491);
            this.splitContainer1.SplitterDistance = 125;
            this.splitContainer1.SplitterIncrement = 2;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.randomizeCheckBox);
            this.groupBox1.Controls.Add(this.randomizeBtn);
            this.groupBox1.Controls.Add(this.formula);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.charmod);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.fontColorBtn);
            this.groupBox1.Controls.Add(this.backgroundBtn);
            this.groupBox1.Controls.Add(this.fontBtn);
            this.groupBox1.Controls.Add(this.maxNum);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.minNum);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(125, 491);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings: ";
            // 
            // randomizeCheckBox
            // 
            this.randomizeCheckBox.AutoSize = true;
            this.randomizeCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.randomizeCheckBox.Location = new System.Drawing.Point(3, 170);
            this.randomizeCheckBox.Name = "randomizeCheckBox";
            this.randomizeCheckBox.Size = new System.Drawing.Size(119, 18);
            this.randomizeCheckBox.TabIndex = 23;
            this.randomizeCheckBox.Text = "randomize each draw";
            this.randomizeCheckBox.UseVisualStyleBackColor = true;
            // 
            // randomizeBtn
            // 
            this.randomizeBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.randomizeBtn.Location = new System.Drawing.Point(3, 147);
            this.randomizeBtn.Name = "randomizeBtn";
            this.randomizeBtn.Size = new System.Drawing.Size(119, 23);
            this.randomizeBtn.TabIndex = 22;
            this.randomizeBtn.Text = "randomize";
            this.randomizeBtn.UseVisualStyleBackColor = true;
            this.randomizeBtn.Click += new System.EventHandler(this.RandomizeBtn_Clicked);
            // 
            // formula
            // 
            this.formula.Dock = System.Windows.Forms.DockStyle.Top;
            this.formula.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.formula.Location = new System.Drawing.Point(3, 128);
            this.formula.MaxLength = 40;
            this.formula.Name = "formula";
            this.formula.Size = new System.Drawing.Size(119, 19);
            this.formula.TabIndex = 21;
            this.formula.Text = "i * j";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.Location = new System.Drawing.Point(3, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 14);
            this.label5.TabIndex = 20;
            this.label5.Text = "formula";
            // 
            // charmod
            // 
            this.charmod.Dock = System.Windows.Forms.DockStyle.Top;
            this.charmod.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.charmod.Location = new System.Drawing.Point(3, 95);
            this.charmod.MaxLength = 40;
            this.charmod.Name = "charmod";
            this.charmod.Size = new System.Drawing.Size(119, 19);
            this.charmod.TabIndex = 17;
            this.charmod.Text = "32";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(3, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 14);
            this.label1.TabIndex = 16;
            this.label1.Text = "Char modifier";
            // 
            // fontColorBtn
            // 
            this.fontColorBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fontColorBtn.Location = new System.Drawing.Point(3, 419);
            this.fontColorBtn.Name = "fontColorBtn";
            this.fontColorBtn.Size = new System.Drawing.Size(119, 23);
            this.fontColorBtn.TabIndex = 13;
            this.fontColorBtn.Text = "font color";
            this.fontColorBtn.UseVisualStyleBackColor = true;
            this.fontColorBtn.Click += new System.EventHandler(this.fontColorBtn_Clicked);
            // 
            // backgroundBtn
            // 
            this.backgroundBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.backgroundBtn.Location = new System.Drawing.Point(3, 442);
            this.backgroundBtn.Name = "backgroundBtn";
            this.backgroundBtn.Size = new System.Drawing.Size(119, 23);
            this.backgroundBtn.TabIndex = 12;
            this.backgroundBtn.Text = "background color";
            this.backgroundBtn.UseVisualStyleBackColor = true;
            this.backgroundBtn.Click += new System.EventHandler(this.backgroundColorBtn_Clicked);
            // 
            // fontBtn
            // 
            this.fontBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fontBtn.Location = new System.Drawing.Point(3, 465);
            this.fontBtn.Name = "fontBtn";
            this.fontBtn.Size = new System.Drawing.Size(119, 23);
            this.fontBtn.TabIndex = 11;
            this.fontBtn.Text = "font";
            this.fontBtn.UseVisualStyleBackColor = true;
            this.fontBtn.Click += new System.EventHandler(this.fontBtn_Clicked);
            // 
            // maxNum
            // 
            this.maxNum.Dock = System.Windows.Forms.DockStyle.Top;
            this.maxNum.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.maxNum.Location = new System.Drawing.Point(3, 62);
            this.maxNum.MaxLength = 5;
            this.maxNum.Name = "maxNum";
            this.maxNum.Size = new System.Drawing.Size(119, 19);
            this.maxNum.TabIndex = 7;
            this.maxNum.Text = "15";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(3, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "end";
            // 
            // minNum
            // 
            this.minNum.Dock = System.Windows.Forms.DockStyle.Top;
            this.minNum.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.minNum.Location = new System.Drawing.Point(3, 29);
            this.minNum.MaxLength = 5;
            this.minNum.Name = "minNum";
            this.minNum.Size = new System.Drawing.Size(119, 19);
            this.minNum.TabIndex = 5;
            this.minNum.Text = "-15";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(3, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "start";
            // 
            // tabsHolder
            // 
            this.tabsHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabsHolder.Location = new System.Drawing.Point(0, 0);
            this.tabsHolder.Name = "tabsHolder";
            this.tabsHolder.SelectedIndex = 0;
            this.tabsHolder.Size = new System.Drawing.Size(649, 491);
            this.tabsHolder.TabIndex = 1;
            // 
            // fontDialog1
            // 
            this.fontDialog1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            // 
            // colorDialog2
            // 
            this.colorDialog2.Color = System.Drawing.Color.Lime;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.menuStrip1);
            this.splitContainer2.Panel1MinSize = 21;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(775, 517);
            this.splitContainer2.SplitterDistance = 25;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Silver;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(775, 25);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Desktop;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Desktop;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 556);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.drawBtn);
            this.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Math Draw";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button drawBtn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox randomizeCheckBox;
        private System.Windows.Forms.Button randomizeBtn;
        private System.Windows.Forms.TextBox formula;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox charmod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button fontColorBtn;
        private System.Windows.Forms.Button backgroundBtn;
        private System.Windows.Forms.Button fontBtn;
        private System.Windows.Forms.TextBox maxNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox minNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabsHolder;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

