﻿using System;
using System.Threading;
using System.Windows.Forms;

namespace MathDraw
{
    public sealed class Utils
    {
        public bool msgBoxDebug = false;

        private static readonly object s_lock = new object();
        private static Utils instance = null;

        private Utils()
        {
        }

        public static Utils Instance
        {
            get
            {
                if (instance != null) return instance;
                Monitor.Enter(s_lock);
                var temp = new Utils();
                Interlocked.Exchange(ref instance, temp);
                Monitor.Exit(s_lock);
                return instance;
            }
        }

        public void MessageBoxDebug(string text)
        {
            if (msgBoxDebug)
                MessageBox.Show(text);
        }

        public string CalculateEta(DateTime processStarted, int totalElements, int processedElements)
        {
            var seconds = TimeSpan.FromTicks(
                DateTime.Now.Subtract(processStarted).Ticks * (totalElements - (processedElements + 1)) / (processedElements + 1)).Seconds.ToString();
            var minutes = TimeSpan.FromTicks(
                DateTime.Now.Subtract(processStarted).Ticks * (totalElements - (processedElements + 1)) / (processedElements + 1)).Minutes.ToString();
            var hours = TimeSpan.FromTicks(
                DateTime.Now.Subtract(processStarted).Ticks * (totalElements - (processedElements + 1)) / (processedElements + 1)).Hours.ToString();

            return string.Format("Time left: {0}:{1}:{2}", AddZero(hours), AddZero(minutes), AddZero(seconds));
        }

        private string AddZero(string timeInput)
        {
            var length = timeInput.Length;

            if (length == 1)
                return ("0" + timeInput.ToString());
            else
                return (timeInput.ToString());
        }

        public string ReverseString(string stringInput)
        {
            var charArray = stringInput.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
