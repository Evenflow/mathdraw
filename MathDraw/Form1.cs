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

        delegate void SetTextCallback(string text, int selecttab);

        List<Thread> t;
        List<RichTextBox> currentTextbox;

        int currentTabIndex = 0;

        public Form1() {
            InitializeComponent();
        }

        private void SetText(string text, int selecttab) {

            if (!String.IsNullOrEmpty(text)) {

                if (this.currentTextbox[selecttab].InvokeRequired) {
                    SetTextCallback d = new SetTextCallback(SetText);
                    this.Invoke(d, new object[] { text, selecttab });
                } else {
                    this.currentTextbox[selecttab].Text += text;
                }
            }
        }

        private void Draw(string formula, int start, int end, Thread t, int selecttab) {

            try {

                SetText(formula + "\n\n", selecttab);

                for (int i = start; i <= end; i++) {

                    if (i > start)
                        SetText("\n", selecttab);

                    for (int j = start; j <= end; j++) {

                        string result = (Formula_Parser(formula, i, j, t)).ToString();

                        int len = result.Length;

                        while (len > 5) {
                            len = len - 5;
                        }

                        //MessageBox.Show(len.ToString());

                        int charnum = len * int.Parse(charmod.Text);

                        string text = ((char)(charnum)).ToString();

                        SetText(text, selecttab);
                    }

                    for (int j = end - 1; j >= start; j--) {

                        string result = (Formula_Parser(formula, i, j, t)).ToString();

                        int len = result.Length;

                        while (len > 5) {
                            len = len - 5;
                        }

                        //MessageBox.Show(len.ToString());

                        int charnum = len * int.Parse(charmod.Text);

                        string text = ((char)(charnum)).ToString();

                        SetText(text, selecttab);
                    }
                }
            } catch (Exception ex) {

                Console.WriteLine(ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e) {

            currentTextbox = new List<RichTextBox>();
            t = new List<Thread>();
        }

        private void button1_Click(object sender, EventArgs e) {

            try {

                TabPage page = new TabPage("Drawing " + (currentTabIndex + 1).ToString());

                currentTextbox.Add(new RichTextBox());
                currentTextbox[currentTabIndex].Dock = DockStyle.Fill;
                currentTextbox[currentTabIndex].Font = fontDialog1.Font;
                currentTextbox[currentTabIndex].ForeColor = colorDialog2.Color;
                currentTextbox[currentTabIndex].BackColor = colorDialog1.Color;

                page.Controls.Add(currentTextbox[currentTabIndex]);
                tabControl1.TabPages.Add(page);
                tabControl1.SelectedTab = page;

                if (checkBox1.Checked)
                    Randomize();

                int minn = int.Parse(minNum.Text);
                int maxn = int.Parse(maxNum.Text);

                currentTextbox[currentTabIndex].Text = "";

                StartTheThread(minn, maxn, currentTabIndex);

                currentTabIndex++;

            } catch (Exception ex) {

                Console.WriteLine(ex.ToString());
            }
        }

        private double Formula_Parser(string formula, int x, int y, Thread t) {

            formula = formula.Trim();

            if (String.IsNullOrEmpty(formula)) {

                MessageBox.Show("Formula cannot be empty!");
                t.Abort();
                return 0;
            }

            formula = formula.Replace("i", x.ToString());
            formula = formula.Replace("j", y.ToString());

            if (Regex.Matches(formula, @"[a-zA-Z!`?<>{}_!@#$%^&=[],'\;:""]").Count > 0) {
                MessageBox.Show("Bad formula!");
                t.Abort();
                return 0;
            }

            return Evaluate(formula, t);
        }

        private double Evaluate(string expression, Thread t) {

            try {

                Expression e = new Expression(expression);
                return Double.Parse(e.Evaluate().ToString());

            } catch (Exception ex) {

                t.Abort();
                return 0;
            }
        }

        private Thread StartTheThread(int minn, int maxn, int selecttab) {

            Thread newt = new Thread(() => Draw(formula.Text, minn, maxn, t[t.Count - 1], selecttab));
            newt.Start();
            t.Add(newt);
            return newt;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {

            foreach (var item in t) {
                if (item != null)
                    item.Abort();
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            DialogResult result = colorDialog1.ShowDialog();

            if (result == DialogResult.OK) {

                var color = colorDialog1.Color;

                foreach (var item in currentTextbox) {
                    item.BackColor = color;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            DialogResult result = fontDialog1.ShowDialog();

            if (result == DialogResult.OK) {

                var font = fontDialog1.Font;

                foreach (var item in currentTextbox) {
                    item.Font = font;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            DialogResult result = colorDialog2.ShowDialog();

            if (result == DialogResult.OK) {

                var color = colorDialog2.Color;

                foreach (var item in currentTextbox) {
                    item.ForeColor = color;
                }
            }
        }

        private void Randomize() {
            Random rand = new Random();

            int range = 128;
            int sizerange = 50;

            int imod = rand.Next(-range, range);
            int jmod = rand.Next(-range, range);

            int midpoint = rand.Next(-sizerange, sizerange);

            int zmod = rand.Next(0, range);

            int randomroll = rand.Next(-1, 1);

            if (randomroll == 0) {
                formula.Text = "(i + (" + imod + ")) * (j + (" + jmod + ")) * (" + zmod + ")";
            } else {
                formula.Text = "(i + (" + imod + ")) * (j + (" + jmod + ")) / (" + zmod + ")";
            }
        }

        private void button5_Click_1(object sender, EventArgs e) {
            Randomize();
        }
    }
}
