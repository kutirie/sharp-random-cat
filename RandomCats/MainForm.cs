using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomCats
{
    public partial class MainForm : Form
    {
        WebClient w; string urlPic;

        public MainForm()
        {
            InitializeComponent();
            w = new WebClient();
            w.Encoding = Encoding.UTF8;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            comboSize.Text = "Normal";
            LoadCat();
        }

        private void comboSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboSize.SelectedIndex == 0)
            {
                pictureBoxCat.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (comboSize.SelectedIndex == 1)
            {
                pictureBoxCat.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void dgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadCat();
        }

        void LoadCat()
        {
            string html = w.DownloadString("http://random.cat");
            string imgUrl = Regex.Match(html, "http://(.*).(jpg|jpeg|gif|png)").Value;
            urlPic = imgUrl;
            pictureBoxCat.ImageLocation = imgUrl;
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (urlPic.Contains(".jpg"))
            {
                saveCat.Filter = "JPG (*.jpg)|*.jpg";
            }
            else if (urlPic.Contains(".png"))
            {
                saveCat.Filter = "PNG (*.png)|*.png";
            }
            else if (urlPic.Contains(".jpeg"))
            {
                saveCat.Filter = "JPEG (*.jpeg)|*.jpeg";
            }
            else if (urlPic.Contains(".gif"))
            {
                saveCat.Filter = "GIF (*.gif)|*.gif";
            }

            var download = saveCat.ShowDialog();

            if (download == DialogResult.OK)
            {
                w.DownloadFile(urlPic, saveCat.FileName);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void pictureBoxCat_LoadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage <= 50)
            {
                progressBar1.Visible = true;
                menuCat.Enabled = false;
            }
            else if (e.ProgressPercentage >= 90)
            {
                progressBar1.Visible = false;
                menuCat.Enabled = true;
            }
            progressBar1.Value = e.ProgressPercentage;
        }

        private void clipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBoxCat.Image);
        }
    }
}
