using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Linq;

namespace MathDraw
{
    public partial class Form1 : Form
    {

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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            textBoxList = new List<RichTextBox>();
            threadList = new List<Thread>();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

            foreach (var item in threadList)
            {
                if (item != null)
                    item.Abort();
            }
        }

        #endregion

        #region draw main methods

        private void SetLabel(string text, Label label)
        {
            try
            {
                if (!String.IsNullOrEmpty(text))
                {
                    if (label.InvokeRequired)
                    {
                        SetLabelCallback delgt = new SetLabelCallback(SetLabel);
                        this.Invoke(delgt, new object[] { text, label });
                    }
                    else
                    {
                        label.Text = text;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Instance.MessageBoxDebug("SetLabel: " + ex.ToString());
            }
        }

        private void SetText(string text, int selectedTab)
        {
            try
            {
                if (!String.IsNullOrEmpty(text) && textBoxList.Count > selectedTab)
                {

                    if (textBoxList[selectedTab].InvokeRequired)
                    {
                        SetTextCallback delgt = new SetTextCallback(SetText);
                        this.Invoke(delgt, new object[] { text, selectedTab });
                    }
                    else
                    {
                        textBoxList[selectedTab].AppendText(text);
                    }
                }

            }
            catch (Exception ex)
            {
                Utils.Instance.MessageBoxDebug("SetText: " + ex.ToString());
            }
        }

        private void Draw(string formula, int start, int end, Thread thread, int selectedTab, ProgressBar pb, TabPage page, Button stopBtn, Label timeLeftLabel)
        {
            string holder = "";

            try
            {

                float diff = Math.Abs(start - end);

                DateTime started = DateTime.Now;

                SetText(formula + "\n\n", selectedTab);

                int counter = 0;

                for (int i = start; i <= end; i++)
                {

                    holder = "";

                    SetProgress((int)((counter / diff) * 100), pb, page, stopBtn, timeLeftLabel);

                    string eta = Utils.Instance.CalculateEta(started, (int)diff, counter);

                    SetLabel(eta, timeLeftLabel);

                    counter++;

                    if (i > start)
                        SetText("\n", selectedTab);

                    for (int j = start; j <= end; j++)
                    {

                        holder += CharGenerator(formula, thread, selectedTab, i, j);
                    }

                    holder = Utils.Instance.Reverse(holder);

                    SetText(holder, selectedTab);
                }

                page.Controls.Remove(pb);
                threadList.Remove(thread);

            }
            catch (Exception ex)
            {

                thread.Abort();
            }
        }

        private string CharGenerator(string formula, Thread thread, int selectedTab, int i, int j)
        {
            string result = (NCalc.Instance.Formula_Parser(formula, i, j, thread)).ToString();

            int len = result.Length;

            while (len > 3)
            {
                len -= 2;
            }

            int charnum = len * int.Parse(charmod.Text);

            string text = ((char)(charnum)).ToString();

            SetText(text, selectedTab);

            return text;
        }

        #endregion

        #region button clicks

        private void drawBtn_Clicked(object sender, EventArgs e)
        {

            try
            {
                labelCounter++;

                TabPage page = new TabPage("Drawing " + (labelCounter).ToString());

                RichTextBox richTextBox = new RichTextBox();

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

                int minn = int.Parse(minNum.Text);
                int maxn = int.Parse(maxNum.Text);

                textBoxList[currentTabIndex].Text = "";

                StartTheThread(minn, maxn, currentTabIndex, page, richTextBox);

                currentTabIndex++;

            }
            catch (Exception ex)
            {
                Utils.Instance.MessageBoxDebug("drawBtn_Clicked: " + ex.ToString());
            }
        }

        private void fontBtn_Clicked(object sender, EventArgs e)
        {
            DialogResult result = fontDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {

                var font = fontDialog1.Font;

                foreach (var item in textBoxList)
                {
                    if (item != null)
                        item.Font = font;
                }
            }
        }

        private void fontColorBtn_Clicked(object sender, EventArgs e)
        {
            DialogResult result = colorDialog2.ShowDialog();

            if (result == DialogResult.OK)
            {
                var color = colorDialog2.Color;

                foreach (var item in textBoxList)
                {
                    if (item != null)
                        item.ForeColor = color;
                }
            }
        }

        private void backgroundColorBtn_Clicked(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {

                var color = colorDialog1.Color;

                foreach (var item in textBoxList)
                {
                    if (item != null)
                        item.BackColor = color;
                }
            }
        }

        private void randomizeBtn_Clicked(object sender, EventArgs e)
        {
            Randomize();
        }

        #endregion

        #region draw additional methods

        private void SetProgress(int progress, ProgressBar pb, TabPage page, Button stopBtn, Label label)
        {

            try
            {
                if (pb.InvokeRequired)
                {
                    SetProgressCallback delgt = new SetProgressCallback(SetProgress);
                    this.Invoke(delgt, new object[] { progress, pb, page, stopBtn, label });
                }
                else
                {
                    pb.Value = progress;
                }

                if (progress == 100)
                {
                    page.Controls.Remove(label);
                    page.Controls.Remove(pb);
                    page.Controls.Remove(stopBtn);
                }

            }
            catch (Exception ex)
            {

                Utils.Instance.MessageBoxDebug("SetProgress: " + ex.ToString());
            }
        }

        private void Stop(object sender, EventArgs e, TabPage page, Thread thread, Button btn)
        {

            thread.Abort();
            threadList.Remove(thread);
            page.Controls.Remove(btn);
        }

        private void Close(object sender, EventArgs e, TabPage page, Thread thread, RichTextBox richTextBox)
        {

            textBoxList.Remove(richTextBox);
            tabsHolder.TabPages.Remove(page);
            currentTabIndex--;
            thread.Abort();
            threadList.Remove(thread);
        }

        private Thread StartTheThread(int minn, int maxn, int selectedTab, TabPage page, RichTextBox richTextBox)
        {

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

            Thread thread = new Thread(() => Draw(formula.Text, minn, maxn, threadList[threadList.Count - 1], selectedTab, pb, page, stopBtn, timeLeftLabel));

            thread.Start();

            stopBtn.Click += (sender, EventArgs) => { Stop(sender, EventArgs, page, thread, stopBtn); };
            btnClose.Click += (sender, EventArgs) => { Close(sender, EventArgs, page, thread, richTextBox); };

            threadList.Add(thread);
            return thread;
        }

        private void Randomize()
        {
            Random rand = new Random();

            int range = 128;

            int imod = rand.Next(-range, range);
            int jmod = rand.Next(-range, range);

            int zmod = rand.Next(0, range);

            int randomroll = rand.Next(-1, 1);

            if (randomroll == 0)
                formula.Text = "(i + (" + imod + ")) * (j + (" + jmod + ")) * (" + zmod + ")";
            else
                formula.Text = "(i + (" + imod + ")) * (j + (" + jmod + ")) / (" + zmod + ")";
        }

        #endregion
    }
}
