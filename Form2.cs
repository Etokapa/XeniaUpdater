﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace XeniaUpdater
{
    public partial class Form2 : Form
    {
        //CHANGE THE updaterBranch STRING TO THE CORRECT BRANCH BEFORE PUSING UPDATE!
        //Sets the branch the updater should update itself using, can be "Release" or "Debug"
        private static string updaterBranch = "Debug";

        //Gets the current name of the executable, for the chance that it could be renamed
        string currentExecutableFullPathName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        string currentExecutableName = System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName;


        public Form2()
        {
            InitializeComponent();
            label4.Text = $"Branch: {updaterBranch}";
            label4.AutoSize = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Process.Start("https://xenia.jp");
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/xenia-project");
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Chopper1337/XeniaUpdater");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFileAsync(new Uri("https://github.com/Chopper1337/XeniaUpdater/raw/main/bin/" + updaterBranch + "/XeniaUpdater.exe"), "XeniaUpdater.Latest.exe");
            }

            if (File.Exists($"XeniaUpdater.Latest.exe"))
            {
                MessageBox.Show($"Latest version downloaded, please restart!", "Latest version downloaded :)");

                using (FileStream strm = File.Create("UpdateDownloaded.bat"))
                using (StreamWriter sw = new StreamWriter(strm))
                {
                    sw.WriteLine(
                        "@echo off\n" +
                        $"rem File generated by {currentExecutableName} (XeniaUpdater), do not modify or delete :)\n" +
                        "echo Updating Xenia Updater :)\n" +
                        "timeout 2\n" +
                        $"del \"{currentExecutableFullPathName}\"\n" +
                        $"echo XeniaUpdater has been updated under the name {currentExecutableName}.\necho This CMD window will not appear on next start and can be closed :)\n" +
                        $"move XeniaUpdater.Latest.exe \"{currentExecutableFullPathName}\"\n" +
                        $"{currentExecutableName}\n" +
                        "del UpdateDownloaded.bat");
                }
                this.Close();
            }
        }
    }
}
