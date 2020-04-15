using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace ParfRand
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //TODO: block another elementss then radio button is pressed
            InitializeComponent();           
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                var rand = new Random();
                int rnd = rand.Next(1946, 2005);
                string video_name = "Намедни " + rnd;
                try
                {
                    Run(video_name).Wait();
                }
                catch(AggregateException ex)
                {
                    foreach (var v in ex.InnerExceptions)
                    {
                        MessageBox.Show("Error: " + v.Message);
                    }
                }
            }
            
        }

        private async Task Run(string srch)
        {     
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyBQacjtsyAu3jFD-lkB9gDgr8r6la1Hl8c",
                ApplicationName = this.GetType().ToString()
            });
            var searchListReq = youtubeService.Search.List("snippet");
            searchListReq.Q = srch;
            var searchListResponse = searchListReq.Execute();
            var searchRes = searchListResponse.Items[0];
            string vidId = searchRes.Id.VideoId;
            /*MessageBox.Show(searchRes.Id.VideoId);
            Uri vidLink = new Uri("https://www.youtube.com/watch_popup?v=" + vidId);
            webBrowser1.Url = vidLink;
            webBrowser1.ScriptErrorsSuppressed = true;*/
            //TODO: open links in local browser
            string vidUrl = "https://www.youtube.com/watch_popup?v=" + vidId;
            System.Diagnostics.Process.Start(vidUrl);
        }
    }
}
