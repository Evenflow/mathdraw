using NCalc;
using System;
using System.Threading;
using System.Windows.Forms;

namespace MathDraw
{
    public sealed class NCalc
    {
        private static readonly object s_lock = new object();
        private static NCalc instance = null;

        private NCalc()
        {
        }

        public static NCalc Instance
        {
            get
            {
                if (instance != null) return instance;
                Monitor.Enter(s_lock);
                var temp = new NCalc();
                Interlocked.Exchange(ref instance, temp);
                Monitor.Exit(s_lock);
                return instance;
            }
        }

        public double Formula_Parser(string formula, int x, int y, Thread thread = null)
        {
            formula = formula.Trim();

            if (string.IsNullOrEmpty(formula))
            {
                MessageBox.Show("Formula cannot be empty!");
                if (thread != null)
                    thread.Abort();
                return 0;
            }

            formula = formula.Replace("i", x.ToString());
            formula = formula.Replace("j", y.ToString());

            return Evaluate(formula, thread);
        }

        private double Evaluate(string expression, Thread thread)
        {
            try
            {
                Expression e = new Expression(expression);
                return double.Parse(e.Evaluate().ToString());
            }
            catch (Exception ex)
            {
                Utils.Instance.MessageBoxDebug("Evaluate: " + ex.ToString());
                return 0;
            }
        }
    }
}
