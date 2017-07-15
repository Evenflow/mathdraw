using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MathDraw {
    public partial class Form1 : Form {
        private delegate void SetLabelCallback(string text, Label label);
        private delegate void SetTextCallback(string text, int selectedTab);
        private delegate void SetProgressCallback(int progress, ProgressBar pb, TabPage page, Button stopBtn, Label label);

        #region vars

        private List<Thread> threadList;
        private List<RichTextBox> textBoxList;

        private int currentTabIndex = 0;
        private int labelCounter = 0;

        #endregion

        #region form1 stuff

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            textBoxList = new List<RichTextBox>();
            threadList = new List<Thread>();
            this.Font = fontReference.Font;
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
            try {
                if (!string.IsNullOrEmpty(text)) {
                    if (label.InvokeRequired) {
                        var delgt = new SetLabelCallback(SetLabel);
                        Invoke(delgt, new object[] { text, label });
                    } else {
                        label.Text = text;
                    }
                }
            } catch (Exception ex) {
                Utils.Instance.MessageBoxDebug("SetLabel: " + ex.ToString());
            }
        }

        private void SetText(string text, int selectedTab) {
            try {
                if (!string.IsNullOrEmpty(text) && textBoxList.Count > selectedTab) {

                    if (textBoxList[selectedTab].InvokeRequired) {
                        var delgt = new SetTextCallback(SetText);
                        Invoke(delgt, new object[] { text, selectedTab });
                    } else {
                        textBoxList[selectedTab].AppendText(text);
                    }
                }

            } catch (Exception ex) {
                Utils.Instance.MessageBoxDebug("SetText: " + ex.ToString());
            }
        }

        private void Draw(string formula, int start, int end, Thread thread, int selectedTab, ProgressBar pb, TabPage page, Button stopBtn, Label timeLeftLabel) {
            string holder = "";

            try {
                var diff = Math.Abs(start - (float)end);

                var started = DateTime.Now;

                SetText(formula + "\n\n", selectedTab);

                var counter = 0;

                for (var i = start; i <= end; i++) {

                    holder = "";

                    SetProgress((int)((counter / diff) * 100), pb, page, stopBtn, timeLeftLabel);

                    var eta = Utils.Instance.CalculateEta(started, (int)diff, counter);

                    SetLabel(eta, timeLeftLabel);

                    counter++;

                    if (i > start)
                        SetText("\n", selectedTab);

                    for (var j = start; j <= end; j++) {

                        holder += CharGenerator(formula, thread, selectedTab, i, j);
                    }

                    holder = Utils.Instance.ReverseString(holder);

                    SetText(holder, selectedTab);
                }

                page.Controls.Remove(pb);
                threadList.Remove(thread);
            } catch (Exception ex) {
                Utils.Instance.MessageBoxDebug("Draw: " + ex.ToString());
                thread.Abort();
            }
        }

        private string CharGenerator(string formula, Thread thread, int selectedTab, int i, int j) {
            var result = (NCalc.Instance.Formula_Parser(formula, i, j, thread)).ToString();

            var len = result.Length;

            while (len > 3) {
                len -= 2;
            }

            var charnum = len * int.Parse(charmod.Text);

            var text = ((char)(charnum)).ToString();

            SetText(text, selectedTab);

            return text;
        }

        #endregion

        #region button clicks

        private void drawBtn_Clicked(object sender, EventArgs e) {
            try {
                labelCounter++;

                var page = new TabPage("Drawing " + (labelCounter).ToString());

                var richTextBox = new RichTextBox();

                textBoxList.Add(richTextBox);
                richTextBox.Dock = DockStyle.Fill;
                richTextBox.Font = fontDialog1.Font;
                richTextBox.ForeColor = colorDialog2.Color;
                richTextBox.BackColor = colorDialog1.Color;

                page.Controls.Add(richTextBox);
                tabsHolder.TabPages.Add(page);
                tabsHolder.SelectedTab = page;

                if (randomizeCheckBox.Checked)
                    Randomize();

                var minn = int.Parse(minNum.Text);
                var maxn = int.Parse(maxNum.Text);

                textBoxList[currentTabIndex].Text = "";

                StartTheThread(minn, maxn, currentTabIndex, page, richTextBox);

                currentTabIndex++;

            } catch (Exception ex) {
                Utils.Instance.MessageBoxDebug("drawBtn_Clicked: " + ex.ToString());
            }
        }

        private void fontBtn_Clicked(object sender, EventArgs e) {
            var result = fontDialog1.ShowDialog();

            if (result == DialogResult.OK) {
                var font = fontDialog1.Font;

                foreach (var item in textBoxList) {
                    if (item != null)
                        item.Font = font;
                }
            }
        }

        private void fontColorBtn_Clicked(object sender, EventArgs e) {
            var result = colorDialog2.ShowDialog();

            if (result == DialogResult.OK) {
                var color = colorDialog2.Color;

                foreach (var item in textBoxList) {
                    if (item != null)
                        item.ForeColor = color;
                }
            }
        }

        private void backgroundColorBtn_Clicked(object sender, EventArgs e) {
            var result = colorDialog1.ShowDialog();

            if (result == DialogResult.OK) {
                var color = colorDialog1.Color;

                foreach (var item in textBoxList) {
                    if (item != null)
                        item.BackColor = color;
                }
            }
        }

        private void randomizeBtn_Clicked(object sender, EventArgs e) {
            Randomize();
        }

        #endregion

        #region draw additional methods

        private void SetProgress(int progress, ProgressBar pb, TabPage page, Button stopBtn, Label label) {
            try {
                if (pb.InvokeRequired) {
                    var delgt = new SetProgressCallback(SetProgress);
                    Invoke(delgt, new object[] { progress, pb, page, stopBtn, label });
                } else {
                    pb.Value = progress;
                }

                if (progress == 100) {
                    page.Controls.Remove(label);
                    page.Controls.Remove(pb);
                    page.Controls.Remove(stopBtn);
                }

            } catch (Exception ex) {
                Utils.Instance.MessageBoxDebug("SetProgress: " + ex.ToString());
            }
        }

        private void Stop(object sender, EventArgs e, TabPage page, Thread thread, Button btn) {
            thread.Abort();
            threadList.Remove(thread);
            page.Controls.Remove(btn);
        }

        private void Close(object sender, EventArgs e, TabPage page, Thread thread, RichTextBox richTextBox) {
            textBoxList.Remove(richTextBox);
            tabsHolder.TabPages.Remove(page);
            currentTabIndex--;
            thread.Abort();
            threadList.Remove(thread);
        }

        private Thread StartTheThread(int minn, int maxn, int selectedTab, TabPage page, RichTextBox richTextBox) {
            var stopBtn = new Button();

            stopBtn.Text = "Stop";
            stopBtn.Dock = DockStyle.Bottom;

            var btnClose = new Button();

            btnClose.Text = "Close";
            btnClose.Dock = DockStyle.Bottom;

            var pb = new ProgressBar();
            pb.Dock = DockStyle.Top;

            var timeLeftLabel = new Label();
            timeLeftLabel.Text = "test!";
            timeLeftLabel.Dock = DockStyle.Top;
            timeLeftLabel.TextAlign = ContentAlignment.MiddleCenter;

            page.Controls.Add(stopBtn);
            page.Controls.Add(btnClose);
            page.Controls.Add(timeLeftLabel);
            page.Controls.Add(pb);

            var thread = new Thread(() => Draw(formula.Text, minn, maxn, threadList[threadList.Count - 1], selectedTab, pb, page, stopBtn, timeLeftLabel));

            thread.Start();

            stopBtn.Click += (sender, EventArgs) => { Stop(sender, EventArgs, page, thread, stopBtn); };
            btnClose.Click += (sender, EventArgs) => { Close(sender, EventArgs, page, thread, richTextBox); };

            threadList.Add(thread);

            return thread;
        }

        private void Randomize() {
            var rand = new Random();

            var range = 128;

            var imod = rand.Next(-range, range);
            var jmod = rand.Next(-range, range);

            var zmod = rand.Next(0, range);

            var randomroll = rand.Next(-1, 1);

            if (randomroll == 0)
                formula.Text = "(i + (" + imod + ")) * (j + (" + jmod + ")) * (" + zmod + ")";
            else
                formula.Text = "(i + (" + imod + ")) * (j + (" + jmod + ")) / (" + zmod + ")";
        }

        #endregion
    }
}
