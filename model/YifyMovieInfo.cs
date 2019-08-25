using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yify.model
{
    internal class YifyMovieInfo : BindableBase
    {
        private string _Title;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; OnPropertyChanged(() => Title); }
        }

        private string _Url;

        public string Url
        {
            get { return _Url; }
            set { _Url = value; OnPropertyChanged(() => Url); }
        }

        private string _thumbnailPath;

        public string ThumbPath
        {
            get { return _thumbnailPath; }
            set { _thumbnailPath = value; OnPropertyChanged(() => ThumbPath); }
        }
        private string _Rating;

        public string Rating
        {
            get { return _Rating; }
            set { _Rating = value; OnPropertyChanged(() => Rating); }
        }
        private string _Year;

        public string Year
        {
            get { return _Year; }
            set { _Year = value; OnPropertyChanged(() => Year); }
        }

    }
}
