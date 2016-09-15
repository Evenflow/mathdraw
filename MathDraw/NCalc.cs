using NCalc;
using System;
using System.Threading;
using System.Windows.Forms;

namespace MathDraw {

    class NCalc {

        Action<string> MessageBoxDebug;

        public NCalc(Action<string> MessageBoxDebug) {

            this.MessageBoxDebug = MessageBoxDebug;
        }

        public double Formula_Parser(string formula, int x, int y, Thread thread) {

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
    }
}
