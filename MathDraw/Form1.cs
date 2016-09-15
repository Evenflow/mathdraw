using NCalc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace MathDraw {
    public partial class Form1 : Form {

        delegate void SetTextCallback(string text, int selectedTab);
        delegate void SetProgressCallback(int progress, ProgressBar pb, TabPage page, Button stopBtn);

        List<Thread> threadList;
        List<RichTextBox> textBoxList;

        int currentTabIndex = 0;

        bool msgBoxDebug = true;

        public Form1() {
            InitializeComponent();
        }

        private void SetText(string text, int selectedTab) {

            if (!String.IsNullOrEmpty(text)) {

                if (this.textBoxList[selectedTab].InvokeRequired) {
                    SetTextCallback d = new SetTextCallback(SetText);
                    this.Invoke(d, new object[] { text, selectedTab });
                } else {
                    this.textBoxList[selectedTab].Text += text;
                }
            }
        }

        private void SetProgress(int progress, ProgressBar pb, TabPage page, Button stopBtn) {

            if (pb.InvokeRequired) {
                SetProgressCallback d = new SetProgressCallback(SetProgress);
                this.Invoke(d, new object[] { progress, pb, page, stopBtn });
            } else {
                pb.Value = progress;
            }

            if (progress == 100) {
                page.Controls.Remove(pb);
                page.Controls.Remove(stopBtn);
            }
        }

        /*
        private string CalculateEta(DateTime processStarted, int totalElements, int processedElements) {

            float timePassed = (processStarted - DateTime.Now).Milliseconds;

            float itemsPerMiliSecond = processedElements / Math.Abs(timePassed);

            float miliSecondsRemaining = (totalElements - processedElements) / (itemsPerMiliSecond);

            return new TimeSpan(0, 0, (int)miliSecondsRemaining * 1000).ToString();
        }
        */

        private void Draw(string formula, int start, int end, Thread thread, int selectedTab, ProgressBar pb, TabPage page, Button stopBtn) {

            try {

                float diff = Math.Abs(start - end);

                SetText(formula + "\n\n", selectedTab);

                int proc = 0;

                for (int i = start; i <= end; i++) {

                    SetProgress((int)((proc / diff)* 100), pb, page, stopBtn);

                    proc++;

                    if (i > start)
                        SetText("\n", selectedTab);

                    for (int j = start; j <= end; j++) {

                        CharGenerator(formula, thread, selectedTab, i, j);
                    }

                    for (int j = end - 1; j >= start; j--) {

                        CharGenerator(formula, thread, selectedTab, i, j);
                    }
                }

                page.Controls.Remove(pb);

            } catch (Exception ex) {

                thread.Abort();
                MessageBoxDebug("Draw: " + ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }

        private void CharGenerator(string formula, Thread thread, int selectedTab, int i, int j) {
            string result = (Formula_Parser(formula, i, j, thread)).ToString();

            int len = result.Length;

            while (len > 5) {
                len -= 4;
                if (len == 0) {
                    len = 2;
                }
            }

            int charnum = len * int.Parse(charmod.Text);

            string text = ((char)(charnum)).ToString();

            SetText(text, selectedTab);
        }

        private void Form1_Load(object sender, EventArgs e) {

            textBoxList = new List<RichTextBox>();
            threadList = new List<Thread>();
        }

        private void drawBtn_Clicked(object sender, EventArgs e) {

            try {

                TabPage page = new TabPage("Drawing " + (currentTabIndex + 1).ToString());

                textBoxList.Add(new RichTextBox());
                textBoxList[currentTabIndex].Dock = DockStyle.Fill;
                textBoxList[currentTabIndex].Font = fontDialog1.Font;
                textBoxList[currentTabIndex].ForeColor = colorDialog2.Color;
                textBoxList[currentTabIndex].BackColor = colorDialog1.Color;


                page.Controls.Add(textBoxList[currentTabIndex]);
                tabsHolder.TabPages.Add(page);
                tabsHolder.SelectedTab = page;

                if (randomizeCheckBox.Checked)
                    Randomize();

                int minn = int.Parse(minNum.Text);
                int maxn = int.Parse(maxNum.Text);

                textBoxList[currentTabIndex].Text = "";

                StartTheThread(minn, maxn, currentTabIndex, page);

                currentTabIndex++;

            } catch (Exception ex) {

                MessageBoxDebug("drawBtn_Clicked: " + ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }

        private double Formula_Parser(string formula, int x, int y, Thread thread) {

            formula = formula.Trim();

            if (String.IsNullOrEmpty(formula)) {

                MessageBox.Show("Formula cannot be empty!");
                thread.Abort();
                return 0;
            }

            formula = formula.Replace("i", x.ToString());
            formula = formula.Replace("j", y.ToString());

            return Evaluate(formula, thread);
        }

        private double Evaluate(string expression, Thread thread) {

            try {

                Expression e = new Expression(expression);
                return Double.Parse(e.Evaluate().ToString());

            } catch (Exception ex) {

                thread.Abort();
                Console.WriteLine(ex.ToString());
                MessageBoxDebug("Evaluate: " + ex.ToString());
                return 0;
            }
        }

        void Stop(object sender, EventArgs e, TabPage page, Thread thread, Button btn) {
            if (thread != null)
                thread.Abort();

            if (page != null)
                page.Controls.Remove(btn);
        }

        void Close(object sender, EventArgs e, TabPage page, Thread thread) {
            if (page != null)
                tabsHolder.TabPages.Remove(page);

            if (thread != null)
                thread.Abort();
        }

        private Thread StartTheThread(int minn, int maxn, int selectedTab, TabPage page) {

            Button stopBtn = new Button();

            stopBtn.Text = "Stop";
            stopBtn.Dock = DockStyle.Bottom;

            Button btnClose = new Button();

            btnClose.Text = "Close";
            btnClose.Dock = DockStyle.Bottom;

            ProgressBar pb = new ProgressBar();
            pb.Dock = DockStyle.Bottom;

            page.Controls.Add(stopBtn);
            page.Controls.Add(btnClose);
            page.Controls.Add(pb);

            Thread newt = new Thread(() => Draw(formula.Text, minn, maxn, threadList[threadList.Count - 1], selectedTab, pb, page, stopBtn));
            newt.Start();

            stopBtn.Click += (sender, EventArgs) => { Stop(sender, EventArgs, page, newt, stopBtn); };
            btnClose.Click += (sender, EventArgs) => { Close(sender, EventArgs, page, newt); };

            threadList.Add(newt);
            return newt;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {

            foreach (var item in threadList) {
                if (item != null)
                    item.Abort();
            }
        }

        private void backgroundColorBtn_Clicked(object sender, EventArgs e) {
            DialogResult result = colorDialog1.ShowDialog();

            if (result == DialogResult.OK) {

                var color = colorDialog1.Color;

                foreach (var item in textBoxList) {
                    if (item != null)
                        item.BackColor = color;
                }
            }
        }

        private void fontBtn_Clicked(object sender, EventArgs e) {
            DialogResult result = fontDialog1.ShowDialog();

            if (result == DialogResult.OK) {

                var font = fontDialog1.Font;

                foreach (var item in textBoxList) {
                    if (item != null)
                        item.Font = font;
                }
            }
        }

        private void fontColorBtn_Clicked(object sender, EventArgs e) {
            DialogResult result = colorDialog2.ShowDialog();

            if (result == DialogResult.OK) {

                var color = colorDialog2.Color;

                foreach (var item in textBoxList) {
                    if (item != null)
                        item.ForeColor = color;
                }
            }
        }

        private void Randomize() {
            Random rand = new Random();

            int range = 128;

            int imod = rand.Next(-range, range);
            int jmod = rand.Next(-range, range);

            int zmod = rand.Next(0, range);

            int randomroll = rand.Next(-1, 1);

            if (randomroll == 0) {
                formula.Text = "(i + (" + imod + ")) * (j + (" + jmod + ")) * (" + zmod + ")";
            } else {
                formula.Text = "(i + (" + imod + ")) * (j + (" + jmod + ")) / (" + zmod + ")";
            }
        }

        private void RandomizeBtn_Clicked(object sender, EventArgs e) {
            Randomize();
        }

        private void MessageBoxDebug(string text) {
            if (msgBoxDebug)
                MessageBox.Show(text);
        }
    }
}
