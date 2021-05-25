using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VAPW09
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private double pi = 0;

        private void start_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
                start.Enabled = false;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            long n = 999_999_999;
            int reprtEach = 1000;
            long inside = 0;
            Random r = new Random();

            for (long i = 0; i < n; i++)
            {
                if (Math.Pow(r.NextDouble(), 2) + Math.Pow(r.NextDouble(), 2) <= 1)
                {
                    inside++;
                }
                if (i % reprtEach == 0 && i > 0)
                {
                    int percent = (int)(i * 100.0 / n);
                    worker.ReportProgress(percent);
                }
            }

            pi = (4.0 * inside) / n;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            number.Text = $"progress {e.ProgressPercentage}%";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            number.Text = pi.ToString();
            start.Enabled = true;
        }
    }
}
