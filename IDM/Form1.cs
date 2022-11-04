using AltoHttp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void selectPathbtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser  = new FolderBrowserDialog();
            if(folderBrowser.ShowDialog() == DialogResult.OK)
            {
                pathBox.Text = folderBrowser.SelectedPath;
            }
        }
        private void downloadBtn_Click(object sender, EventArgs e)
        {
            Download(urlBox.Text, pathBox.Text);
        }
        HttpDownloader downloader;

        private void Download (string url,string path)
        {
            if (pathBox.Text == "")
            {
                MessageBox.Show("الرجاء ادخال مسار التحميل");
                return;
            }

            downloader = new HttpDownloader(url,$"{path}\\{Path.GetFileName(url)}");
            downloader.DownloadCompleted += Downloader_DownloadCompleted;
            downloader.ProgressChanged += Downloader_ProgressChanged;
            downloader.Start();
        }

        private void Downloader_ProgressChanged(object sender, AltoHttp.ProgressChangedEventArgs e)
        {
            progressBar1.Value = (int) e.Progress;
            label2.Text = $"{e.Progress.ToString("0.00")}%";
            label4.Text = String.Format("{0} KB/S" , (e.SpeedInBytes /1024).ToString("0.00"));
            label6.Text = String.Format("{0} MB", (downloader.TotalBytesReceived / 1024 / 1024).ToString("0.00"));
            label1.Text = "جاري التحميل";
        }

        private void Downloader_DownloadCompleted(object sender, EventArgs e)
        {
            label1.Text = "تم التحميل";
        }

        private void puseBtn_Click(object sender, EventArgs e)
        {
            downloader.Pause();
        }

        private void resumeBtn_Click(object sender, EventArgs e)
        {
            downloader.Resume();
        }
    }
}
