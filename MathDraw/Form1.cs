using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MathDraw {

    public partial class Form1 : Form {

        #region vars

        delegate void SetLabelCallback(string text, Label label);
        delegate void SetTextCallback(string text, int selectedTab);
        delegate void SetProgressCallback(int progress, ProgressBar pb, TabPage page, Button stopBtn, Label label);

        List<Thread> threadList;
        List<RichTextBox> textBoxList;

        int currentTabIndex = 0;

        bool msgBoxDebug = true;

        NCalc calc;

        #endregion

        #region form1 stuff

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {

            textBoxList = new List<RichTextBox>();
            threadList = new List<Thread>();
            calc = new NCalc(MessageBoxDebug);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {

            foreach (var item in threadList) {
                if (item != null)
                    item.Abort();
            }
        }

        #endregion

        #region draw main methods

        private void SetLabel(string text, Label label) {

            if (!String.IsNullOrEmpty(text)) {

                if (label.InvokeRequired) {
                    SetLabelCallback d = new SetLabelCallback(SetLabel);
                    this.Invoke(d, new object[] { text, label });
                } else {
                    label.Text = text;
                }
            }
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

        private void Draw(string formula, int start, int end, Thread thread, int selectedTab, ProgressBar pb, TabPage page, Button stopBtn, Label timeLeftLabel) {

            try {

                float diff = Math.Abs(start - end);

                DateTime started = DateTime.Now;

                SetText(formula + "\n\n", selectedTab);

                int counter = 0;

                for (int i = start; i <= end; i++) {

                    SetProgress((int)((counter / diff)* 100), pb, page, stopBtn, timeLeftLabel);

                    string eta = CalculateEta(started, (int)diff, counter);

                    SetLabel(eta, timeLeftLabel);

                    counter++;

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
            string result = (calc.Formula_Parser(formula, i, j, thread)).ToString();

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

        #endregion

        #region button clicks

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

        private void RandomizeBtn_Clicked(object sender, EventArgs e) {
            Randomize();
        }

        #endregion

        #region draw additional methods

        private void SetProgress(int progress, ProgressBar pb, TabPage page, Button stopBtn, Label label) {

            if (pb.InvokeRequired) {
                SetProgressCallback d = new SetProgressCallback(SetProgress);
                this.Invoke(d, new object[] { progress, pb, page, stopBtn, label });
            } else {
                pb.Value = progress;
            }

            if (progress == 100) {
                page.Controls.Remove(label);
                page.Controls.Remove(pb);
                page.Controls.Remove(stopBtn);
            }
        }

        private void Stop(object sender, EventArgs e, TabPage page, Thread thread, Button btn) {
            if (thread != null)
                thread.Abort();

            if (page != null)
                page.Controls.Remove(btn);
        }

        private void Close(object sender, EventArgs e, TabPage page, Thread thread) {
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
            pb.Dock = DockStyle.Top;

            Label timeLeftLabel = new Label();
            timeLeftLabel.Text = "test!";
            timeLeftLabel.Dock = DockStyle.Top;
            timeLeftLabel.TextAlign = ContentAlignment.MiddleCenter;

            page.Controls.Add(stopBtn);
            page.Controls.Add(btnClose);
            page.Controls.Add(timeLeftLabel);
            page.Controls.Add(pb);

            Thread newt = new Thread(() => Draw(formula.Text, minn, maxn, threadList[threadList.Count - 1], selectedTab, pb, page, stopBtn, timeLeftLabel));
            newt.Start();

            stopBtn.Click += (sender, EventArgs) => { Stop(sender, EventArgs, page, newt, stopBtn); };
            btnClose.Click += (sender, EventArgs) => { Close(sender, EventArgs, page, newt); };

            threadList.Add(newt);
            return newt;
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

        #endregion

        #region utils

        private void MessageBoxDebug(string text) {
            if (msgBoxDebug)
                MessageBox.Show(text);
        }


        private string CalculateEta(DateTime processStarted, int totalElements, int processedElements) {

            string seconds = TimeSpan.FromTicks(DateTime.Now.Subtract(processStarted).Ticks * (totalElements - (processedElements+1)) / (processedElements+1)).Seconds.ToString();
            string minutes = TimeSpan.FromTicks(DateTime.Now.Subtract(processStarted).Ticks * (totalElements - (processedElements+1)) / (processedElements+1)).Minutes.ToString();
            string hours = TimeSpan.FromTicks(DateTime.Now.Subtract(processStarted).Ticks * (totalElements - (processedElements+1)) / (processedElements+1)).Hours.ToString();



            return string.Format("Time left: {0}:{1}:{2}", AddZero(hours), AddZero(minutes), AddZero(seconds));
        }

        private string AddZero(string timeInput) {

            int length = timeInput.Length;

            if (length == 1)
                return ("0" + timeInput.ToString());
            else
                return (timeInput.ToString());
        }

        #endregion
    }
}
