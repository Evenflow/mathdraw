using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathDraw {
    public partial class Form1 : Form {

        delegate void SetTextCallback(string text);

        Thread t;

        public Form1() {
            InitializeComponent();
        }

        private void SetText(string text) {

            if (this.richTextBox1.InvokeRequired) {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            } else {
                this.richTextBox1.Text += text;
            }
        }

        private void Draw(int start, int end, int randmin, int randmax, RichTextBox txt, Thread t) {

            Random rand = new Random();

            int multiplier = rand.Next(randmin, randmax);

            string result = "";

            for (int x = start; x <= end; x++) {

                if (x > start)
                    SetText("\n");

                for (int y = start; y <= end; y++) {
                    result = (Formula_Parser(formula.Text, x, y, t)).ToString();
                    SetText(((char)(result.Length * multiplier)).ToString());
                }

                for (int y = end - 1; y >= start; y--) {
                    result = (Formula_Parser(formula.Text, x, y, t)).ToString();
                    SetText(((char)(result.Length * multiplier)).ToString());
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {

            try {
                int minr = int.Parse(minRand.Text);
                int maxr = int.Parse(maxRand.Text);
                int minn = int.Parse(minNum.Text);
                int maxn = int.Parse(maxNum.Text);

                richTextBox1.Text = "";
                StartTheThread(minn, maxn, minr, maxr);
            } catch (Exception v) {

                MessageBox.Show(v.ToString());
            }
        }

        private int Formula_Parser(string formula, int x, int y, Thread t) {

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

            return (int)Evaluate(formula, t);
        }

        public double Evaluate(string expression, Thread t) {

            try {
                DataTable table = new DataTable();
                table.Columns.Add("expression", typeof(string), expression);
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                return double.Parse((string)row["expression"]);

            } catch (Exception v) {

                MessageBox.Show(v.ToString());
                t.Abort();
                return 0;
            }
        }

        public Thread StartTheThread(int param1, int param2, int param3, int param4) {

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
    }
}
