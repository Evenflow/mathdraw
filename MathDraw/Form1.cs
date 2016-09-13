using NCalc;
using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace MathDraw {
    public partial class Form1 : Form {

        delegate void SetTextCallback(string text);

        Thread t;

        public Form1() {
            InitializeComponent();
        }

        private void SetText(string text) {

            if (!String.IsNullOrEmpty(text)) {

                if (this.richTextBox1.InvokeRequired) {
                    SetTextCallback d = new SetTextCallback(SetText);
                    this.Invoke(d, new object[] { text });
                } else {
                    this.richTextBox1.Text += text;
                }
            }
        }

        private void WriteToTextBox(int multiplier, int i, int j, Thread t, int end) {

            string result = (Formula_Parser(formula.Text, i, j, t)).ToString();

            int charnum = result.Length * multiplier;

            string text = ((char)(charnum)).ToString();

            SetText(text);
        }

        private void Draw(int start, int end, int randmin, int randmax, RichTextBox txt, Thread t) {

            Random rand = new Random();
            try {

                int multiplier = rand.Next(randmin, randmax);

                Console.WriteLine(multiplier.ToString());

                for (int i = start; i <= end; i++) {

                    if (i > start)
                        SetText("\n");

                    for (int j = start; j <= end; j++) {

                        WriteToTextBox(multiplier, i, j, t, end);
                    }

                    for (int j = end - 1; j >= start; j--) {

                        WriteToTextBox(multiplier, i, j, t, end);
                    }
                }
            } catch (Exception ex) {

                Console.WriteLine(ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {

            if (checkBox1.Checked)
                Randomize();

            try {
                int minr = int.Parse(minRand.Text);
                int maxr = int.Parse(maxRand.Text);
                int minn = int.Parse(minNum.Text);
                int maxn = int.Parse(maxNum.Text);

                richTextBox1.Text = "";
                StartTheThread(minn, maxn, minr, maxr);

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

        private Thread StartTheThread(int param1, int param2, int param3, int param4) {

            if (t == null) {
                t = new Thread(() => Draw(param1, param2, param3, param4, richTextBox1, t));
                t.Start();
                return t;
            } else {
                t.Abort();
                t = new Thread(() => Draw(param1, param2, param3, param4, richTextBox1, t));
                t.Start();
                return t;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            if (t != null) {
                t.Abort();
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            DialogResult result = colorDialog1.ShowDialog();

            if (result == DialogResult.OK) {

                var color = colorDialog1.Color;
                richTextBox1.BackColor = color;
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            DialogResult result = fontDialog1.ShowDialog();

            if (result == DialogResult.OK) {

                var font = fontDialog1.Font;
                richTextBox1.Font = font;
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            DialogResult result = colorDialog2.ShowDialog();

            if (result == DialogResult.OK) {

                var color = colorDialog2.Color;
                richTextBox1.ForeColor = color;
            }
        }

        private void button5_Click(object sender, EventArgs e) {

            Randomize();
        }

        private void Randomize() {
            Random rand = new Random();

            int range = 128;

            int midpoint = rand.Next(-range, range);

            int size = 64;

            int low = midpoint - size;
            int high = midpoint + size;

            minNum.Text = rand.Next(low, midpoint).ToString();
            maxNum.Text = rand.Next(midpoint, high).ToString();
        }
    }
}
