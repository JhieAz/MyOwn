using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PROGRESSBAAR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Task ProcessData(List<string> list, IProgress<ProgressReport> progress)
        {
            int index = 1;
            int totalIProgress = list.Count;
            var progressReport = new ProgressReport();
            return Task.Run(() =>
            {
                for (int i = 0; i < totalIProgress; i++)
                {
                    progressReport.PercentComplete = index++ * 100 / totalIProgress;
                    progress.Report(progressReport);
                    Thread.Sleep(10);
                }
            });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 1000; i++)
                list.Add(i.ToString());
            label1.Text = "Working...";
            var progress = new Progress<ProgressReport>();
            progress.ProgressChanged += (o, report) => {
                label1.Text = string.Format("Processing....{0}%", report.PercentComplete);
                progressBar1.Value = report.PercentComplete;
                progressBar1.Update();
        };
            await ProcessData(list, progress);
            label1.Text = "Done!";
            
        }
    }
}