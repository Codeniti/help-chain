using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using System.Net;
using System;
using Windows.Storage;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Controls.Primitives;
using System.Threading;
using Microsoft.Phone.Tasks;

namespace Second_Library
{
    public partial class Post : PhoneApplicationPage
    {
        string getText = "";
        string getDate = "";
        string getName = "";
        string getRespects = "";
        string getViews = "";
        
        string myPostId = MainPage.movieId;
        public Post()
        {
            InitializeComponent();
            GetFeed(handleFeed, "");
        }

        public void GetFeed(Action<string> doSomethingWithFeed, string searchString)
        {

            //TitleBar.Text = "Searching movies for " + MovieName;
            
            Random rnd = new Random();
            int rNum = rnd.Next();
            string NewQuery = HttpUtility.UrlEncode(searchString);


            try
            {
                HttpWebRequest request = HttpWebRequest.CreateHttp("http://fawkesindia.com/services/app/getPost.php?pid=" + myPostId + "&devId=1");

                request.BeginGetResponse(
                    asyncCallback =>
                    {
                        string data = null;
                        try
                        {
                            using (WebResponse response = request.EndGetResponse(asyncCallback))
                            {
                                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                                {
                                    data = reader.ReadToEnd();
                                }
                            }
                        }
                        catch (Exception netExp)
                        {
                            

                        }

                        Deployment.Current.Dispatcher.BeginInvoke(() => doSomethingWithFeed(data));
                    }
                    , null);
            }
            catch (Exception netEx)
            {
                

            }


        }

        // this method will be called by GetFeed once the feed has been downloaded
        private void handleFeed(string feedString)
        {
            
            try
            {
                // build XML DOM from feed string
                XDocument doc = XDocument.Parse(feedString);
                
                    // show title of feed in TextBlock
                    
                    var xmltitles = doc.Descendants("Post").Attributes("Text").Select(a => a.Value);
                    // add each feed item to a ListBox

                    foreach (var Title in xmltitles)
                    {

                        getText = Title;
                        
                       
                    }

                    var xmlpub = doc.Descendants("Post").Attributes("Date").Select(b => b.Value);
                    foreach (var Publisher in xmlpub)
                    {
                        getDate = Publisher;
                    }
                    var xmlpages = doc.Descendants("Post").Attributes("Views").Select(b => b.Value);
                    foreach (var Pages in xmlpages)
                    {
                        getViews = Pages;
                    }



                    var xmlself = doc.Descendants("Post").Attributes("Name").Select(d => d.Value);
                    foreach (var SelfUri in xmlself)
                    {
                        getName = SelfUri;
                    }


                    var xmlrate = doc.Descendants("Post").Attributes("Respects").Select(d => d.Value);
                    foreach (var RatingsNow in xmlrate)
                    {
                        getRespects = RatingsNow;
                    }
                
                
            }
            catch (Exception ex)
            {
               
            }

            SetNames();
        }

        public void SetNames()
        {

            TextPost.Text = getText;
            Views.Text = getViews +" Views";
            Respects.Text = getRespects +" Respects";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ShareStatusTask shareStatusTask = new ShareStatusTask();

            shareStatusTask.Status = "I'm developing a Windows Phone application!";

            shareStatusTask.Show();
        }
    }
}