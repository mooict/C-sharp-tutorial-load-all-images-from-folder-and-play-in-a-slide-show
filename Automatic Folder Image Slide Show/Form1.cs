using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO; // this one is needed for reading external files

namespace Automatic_Folder_Image_Slide_Show
{

    // created by MOOICT.COM 2021
    // for edcuational purposes only
    public partial class Form1 : Form
    {

        List<string> filteredFiles;
        FolderBrowserDialog FolderBrowser = new FolderBrowserDialog();

        int counter = -1;
        int timerInterval = 1000;
        bool isPlaying = false;

        public Form1()
        {
            InitializeComponent();

            radioButton1.Checked = true;
            slideShowTimer.Interval = timerInterval;
            btnPlay.Enabled = false;

        }

        private void BrowseForImages(object sender, EventArgs e)
        {

            counter = -1;
            isPlaying = false;
            slideShowTimer.Stop();
            btnPlay.Text = "Play";

            DialogResult result = FolderBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {
                filteredFiles = Directory.GetFiles(FolderBrowser.SelectedPath, "*.*")
                .Where(file => file.ToLower().EndsWith("jpg") || file.ToLower().EndsWith("gif")
                || file.ToLower().EndsWith("png") || file.ToLower().EndsWith("bmp")).ToList();

                lblFileInfo.Text = "Folder loaded - Now Press Play!";

                btnPlay.Enabled = true;

            }



        }

        private void PlayStopSlideShow(object sender, EventArgs e)
        {
            if (isPlaying == false)
            {
                btnPlay.Text = "Stop";
                slideShowTimer.Start();
                isPlaying = true;
            }
            else
            {
                btnPlay.Text = "Play";
                isPlaying = false;
                slideShowTimer.Stop();
            }
        }

        private void PlaySlideShowTimerEvent(object sender, EventArgs e)
        {
            counter += 1;

            if (counter >= filteredFiles.Count)
            {
                counter = -1;
            }
            else
            {
                imageViewer.Image = Image.FromFile(filteredFiles[counter]);
                lblFileInfo.Text = filteredFiles[counter].ToString();
            }
        }

        private void SpeedChangeEvent(object sender, EventArgs e)
        {

            RadioButton tempRadioButton = sender as RadioButton;

            switch (tempRadioButton.Text.ToString())
            {
                case "1x":
                    timerInterval = 3000;
                    break;
                case "2x":
                    timerInterval = 2000;
                    break;
                case "3x":
                    timerInterval = 1000;
                    break;
            }

            slideShowTimer.Interval = timerInterval;



        }
    }
}
