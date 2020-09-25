using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomCounter
{
    public partial class Form1 : Form
    {
        private delegate void SafeCallDelegate(string text);
        Thread countThread;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            countThread = new Thread(Count);
            countThread.Start();
        }

        private void Count(object obj)
        {
            int nextStep = 1;
            int step;
            for (;;)
            {
                try
                {
                    step = int.Parse(counterStep.Text);
                    if (step != 0)
                    {
                        nextStep = step;
                    } else {
                        nextStep = 1;
                    }
                }
                catch { }
                finally { }

                int calcCount = int.Parse(result.Text);
                calcCount += nextStep;

                WriteTextSafe(calcCount.ToString());
                logging(calcCount.ToString());

                Thread.Sleep(1000);
            }
        }

        private void WriteTextSafe(string text)
        {
            if (result.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                result.Invoke(d, new object[] { text });
            }
            else
            {
                result.Text = text;
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            countThread.Abort();
        }

        private void logging(string text)
        {
            string filename = @"\counter_logs.txt";
            string path = Directory.GetCurrentDirectory() + filename;
            DateTime curDate = DateTime.Now;

            FileStream file = new FileStream(path, FileMode.Append);
            StreamWriter fnew = new StreamWriter(file, Encoding.GetEncoding(1251));

            fnew.WriteLine(curDate.ToLocalTime() + " - " + text);
            fnew.Close();
        }
    }
}
