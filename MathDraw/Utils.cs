using System;
using System.Windows.Forms;

namespace MathDraw {

    class Utils {

        static bool msgBoxDebug = false;

        public static Utils instance;

        static Utils() {

            instance = new Utils();
        }

        public void MessageBoxDebug(string text) {

            if (msgBoxDebug)
                MessageBox.Show(text);
        }

        public string CalculateEta(DateTime processStarted, int totalElements, int processedElements) {

            string seconds = TimeSpan.FromTicks(DateTime.Now.Subtract(processStarted).Ticks * (totalElements - (processedElements+1)) / (processedElements+1)).Seconds.ToString();
            string minutes = TimeSpan.FromTicks(DateTime.Now.Subtract(processStarted).Ticks * (totalElements - (processedElements+1)) / (processedElements+1)).Minutes.ToString();
            string hours = TimeSpan.FromTicks(DateTime.Now.Subtract(processStarted).Ticks * (totalElements - (processedElements+1)) / (processedElements+1)).Hours.ToString();

            return string.Format("Time left: {0}:{1}:{2}", AddZero(hours), AddZero(minutes), AddZero(seconds));
        }

        public string AddZero(string timeInput) {

            int length = timeInput.Length;

            if (length == 1)
                return ("0" + timeInput.ToString());
            else
                return (timeInput.ToString());
        }

    }
}
