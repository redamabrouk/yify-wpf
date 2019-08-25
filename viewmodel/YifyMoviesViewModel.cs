using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Yify.commands;
using Yify.model;

namespace Yify.viewmodel
{
    internal class YifyMoviesViewModel : BindableBase
    {
         readonly List<string> geners = new List<string> { "Action", "Adventure","Animation","Biography",
           "Comedy","Crime","Documentary","Drama","Family","Fantasy","Film-noir","Game-show","History",
           "Horror","Music","Musical","Mystery","News","Reality-tv","Romance","Sci-fi","Sport",
           "Talk-Show","Thriller","War","Western"};
        
        
        readonly List<string> orderbys = new List<string> { "Latest", "Oldest", "Seeds", "Peers", "Rating", "Likes", "Alphabetical", "Downloads" };
         
        
        readonly List<string> qualities = new List<string> { "All", "720p", "1080p", "3D" };



        Dictionary<string, string> ratings = new Dictionary<string, string> { { "All", "0" }, { "1+", "1" }, { "2+", "2" }, { "3+", "3" }, 
        { "4+","4" }, { "5+" ,"5"}, { "6+","6" }, { "7+","7" }, { "8+","8" }, { "9+","9" } };
        


        
        string yifylinkformat = "https://yts.lt/browse-movies/{0}/{1}/{2}/{3}/{4}{5}";

        public List<string> Geners { get { return geners; } }
        public List<string> Qualities { get { return qualities; } }
        public List<string> Orderbys { get { return orderbys; } }
        public Dictionary<string, string> Ratings { get { return ratings; } }

        public ICommand Search_Command { get; set; }
        private ObservableCollection<YifyMovieInfo> _Movies;

        public ObservableCollection<YifyMovieInfo> Movies
        {
            get { return _Movies; }
            set { _Movies = value; OnPropertyChanged(() => Movies); }
        }

        private string _SerachKeyword;

        public string SearchKeyWord
        {
            get { return _SerachKeyword; }
            set { _SerachKeyword = value; OnPropertyChanged(() => SearchKeyWord); }
        }

        private string _Quality;

        public string Quality
        {
            get { return _Quality; }
            set { _Quality = value; OnPropertyChanged(() => Quality); }
        }

        private string _Genre;

        public string Genre
        {
            get { return _Genre; }
            set { _Genre = value; OnPropertyChanged(() => Genre); }
        }
        private string _Rating;

        public string Rating
        {
            get { return _Rating; }
            set { _Rating =value ; OnPropertyChanged(() => Rating); }
        }

        private string _OrderBy;

        public string OrderBy
        {
            get { return _OrderBy; }
            set { _OrderBy = value; OnPropertyChanged(() => OrderBy); }
        }
        public YifyMoviesViewModel()
        {
             Search_Command =  new  SearchyCommand(this);
             _Movies = new ObservableCollection<YifyMovieInfo>();
        }
        string pagenumber = "";
        int totalpages = 0;
        internal async void Search()
        {
            string s=string.IsNullOrEmpty(SearchKeyWord)?"0":SearchKeyWord;
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                var client = new WebClient();

                string link = string.Format(yifylinkformat, s, Quality.ToLower(), Genre.ToLower(), Rating, OrderBy.ToLower(), pagenumber);
                string htmlcontent = await client.DownloadStringTaskAsync(new Uri(link));


                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlcontent);
                var moviesList = doc.DocumentNode.Descendants("div").Where(node => node.HasClass("browse-movie-wrap"));

                try
                {
                    Int32.TryParse(doc.DocumentNode.Descendants("li").Where(node => node.HasClass("pagination-bordered")).First().InnerText.Split(' ')[2], out totalpages);

                }
                catch (Exception)
                {
                    totalpages = 0;
                }
                Movies.Clear();
                foreach (var item in moviesList)
                {
                    Movies.Add(new YifyMovieInfo()
                    {
                        Url = item.Descendants("a").Where(node => node.Attributes.Contains("href")).First().Attributes["href"].Value,
                        ThumbPath = item.Descendants().Where(node => node.Attributes.Contains("src")).First().Attributes["src"].Value,
                        Title = item.Descendants().Where(node => node.HasClass("browse-movie-title")).First().InnerText,
                        Rating = item.Descendants().Where(node => node.HasClass("rating")).First().InnerText,
                        Year = item.Descendants().Where(node => node.HasClass("browse-movie-year")).First().InnerText
                    });
                }
            }
            else
                MessageBox.Show("Network not Available","Network Error",MessageBoxButton.OK,MessageBoxImage.Error);
        }
        
    }
}
