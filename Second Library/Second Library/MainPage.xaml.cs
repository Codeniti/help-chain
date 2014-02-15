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

namespace Second_Library
{
    public partial class MainPage : PhoneApplicationPage
    {
        public static string movieId = "";
        public class CMyData
        {

            public string TitleName { get; set; }

            public string SelfUrl { get; set; }

            public string Publisher { get; set; }

            public string Rating { get; set; }

            public string Page { get; set; }

            public string fetchSelfUrl()
            {
                return SelfUrl;
            }
            public override string ToString()
            {
                return TitleName + Publisher + Page + Rating;
            }

        }

        ObservableCollection<CMyData> arrayTitle = new ObservableCollection<CMyData>();


        int countmax = 0;
        string[] xtitle = new string[20] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        string[] xpages = new string[20] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        string[] xself = new string[20] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        string[] xpublisher = new string[20] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        string[] xratings = new string[20] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        int i = 0;
        int tcount, xcount, rcount, icount, txcount, exCount = 0;
        // Constructor

        public MainPage()
        {
            InitializeComponent();
            GetFeed(handleFeed, "");
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            GridListBox.Visibility = Visibility.Visible;
            MovieBox.Visibility = Visibility.Collapsed;
        }


        public void GetFeed(Action<string> doSomethingWithFeed, string searchString)
        {

            //TitleBar.Text = "Searching movies for " + MovieName;
            Notify.Visibility = Visibility.Visible;
            PB.Visibility = Visibility.Visible;
            Random rnd = new Random();
            int rNum = rnd.Next();
            string NewQuery = HttpUtility.UrlEncode(searchString);


            try
            {
                HttpWebRequest request = HttpWebRequest.CreateHttp("http://fawkesindia.com/services/app/getPosts.php?cat=all");

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
                            exCount++;

                        }

                        Deployment.Current.Dispatcher.BeginInvoke(() => doSomethingWithFeed(data));
                    }
                    , null);
            }
            catch (Exception netEx)
            {
                exCount++;

            }


        }

        // this method will be called by GetFeed once the feed has been downloaded
        private void handleFeed(string feedString)
        {
            if (exCount > 0)
            {
                
                PB.Visibility = Visibility.Collapsed;
                Notify.Text = "Internet unavailable !!";
            }
            try
            {
                // build XML DOM from feed string
                XDocument doc = XDocument.Parse(feedString);
                if (doc.Document.Root.Attribute("Count").Value != "0")
                {
                    // show title of feed in TextBlock
                    countmax = doc.Root.Elements().Count();
                    var xmltitles = doc.Descendants("Posts").Attributes("Text").Select(a => a.Value);
                    // add each feed item to a ListBox

                    foreach (var Title in xmltitles)
                    {
                        if (tcount > countmax)
                        {
                            break;
                        }


                        xtitle[tcount] = Title;
                        tcount++;
                    }

                    var xmlpub = doc.Descendants("Posts").Attributes("Date").Select(b => b.Value);
                    foreach (var Publisher in xmlpub)
                    {
                        if (txcount > countmax)
                        {
                            break;
                        }
                        if (Publisher != "")
                        {
                            xpublisher[txcount] = "\nDate: " + Publisher;
                        }
                        else
                        {
                            xpublisher[txcount] = "";
                        }

                        txcount++;
                    }
                    var xmlpages = doc.Descendants("Posts").Attributes("Views").Select(b => b.Value);
                    foreach (var Pages in xmlpages)
                    {
                        if (xcount > countmax)
                        {
                            break;
                        }
                        if (Pages != "")
                        {
                            xpages[xcount] = "\nViews: " + Pages;
                        }
                        else
                        {
                            xpages[xcount] = "\nViews: N/A";
                        }
                        xcount++;
                    }



                    var xmlself = doc.Descendants("Posts").Attributes("Pid").Select(d => d.Value);
                    foreach (var SelfUri in xmlself)
                    {
                        if (icount > countmax)
                        {
                            break;
                        }
                        xself[icount] = SelfUri;
                        icount++;
                    }

                    var xmlrate = doc.Descendants("Posts").Attributes("Respect").Select(d => d.Value);
                    foreach (var RatingsNow in xmlrate)
                    {
                        if (icount > countmax)
                        {
                            break;
                        }
                        xratings[rcount] = " - Respect: " + RatingsNow;
                        rcount++;
                    }
                }
                else
                {
                    //Notify.Visibility = Visibility.Visible;
                    MovieBox.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                PB.Visibility = Visibility.Collapsed;
                Notify.Text = "Internet unavailable !!";
                MessageBox.Show(ex.ToString());
            }
            try
            {

                while (i < countmax)
                {

                    if (xtitle[i] != "")
                    {
                        CMyData task = new CMyData()
                        {
                            TitleName = xtitle[i] + xpublisher[i] + xpages[i] + xratings[i],
                            SelfUrl = xself[i],
                            Publisher = xpublisher[i],
                            Rating = xratings[i],
                            Page = xpages[i],
                        };
                        arrayTitle.Add(task);
                        i++;
                        //TitleBar.Text = "MOVIETALES - Search a Movie!!!";
                    }
                }
                MovieBox.ItemsSource = arrayTitle;
                //MyListBox1.ItemsSource = arrayTitle;
                PB.Visibility = Visibility.Collapsed;


            }
            catch (Exception ex)
            {
                PB.Visibility = Visibility.Collapsed;
                Notify.Text = "There was problem with query!!!";
            }

        }

        public void clearAll()
        {
            Notify.Text = "";
            arrayTitle.Clear();
            i = 0; tcount = 0; xcount = 0; icount = 0;
            Array.Clear(xtitle, 0, 10);
            Array.Clear(xpages, 0, 10);
            Array.Clear(xself, 0, 10);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            //Nav();
            Random rnd = new Random();
            int x = rnd.Next(50, 100);

            string url = "http://fawkesindia.com/services/app/setUser.php?did="+x+"&name=Anurag&loc=Bhopal";

            // HTTP web request
            

            MessageBox.Show("Anurag is new user and id is: "+x);
            clearAll();
            PB.Visibility = Visibility.Visible;
            Notify.Visibility = Visibility.Collapsed;
            MovieBox.Visibility = Visibility.Visible;
            GridListBox.Visibility = Visibility.Collapsed;

            GetFeed(handleFeed, "");
            WebClient webClient = new WebClient();
            // Register the callback
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(new System.Uri(url));
        }
        
            void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
            {
                if (e.Error == null)
                {
       
                }
            }


        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            GridListBox.Visibility = Visibility.Visible;
            MovieBox.Visibility = Visibility.Collapsed;
        }

        private void MovieBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (listBox != null && listBox.SelectedItem != null)
            {

                int indexc = listBox.SelectedIndex;
                movieId = fetchId(indexc);
                
                NavigationService.Navigate(new Uri("/Post.xaml", UriKind.Relative));
            }

        }

        public string fetchId(int listIndex)
        {
            /// Clear the list box that will show all the tasks.
            string myMovieId = "";

            myMovieId = arrayTitle[listIndex].fetchSelfUrl();
            return myMovieId;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            clearAll();
            PB.Visibility = Visibility.Visible;
            Notify.Visibility = Visibility.Collapsed;
            MovieBox.Visibility = Visibility.Visible;
            GridListBox.Visibility = Visibility.Collapsed;

            GetFeed(handleFeed, "");
        }
    }
}