using System;

namespace Covid_19.CoreAPI.Models {
    public class News {
        public string Title { get; private set; }
        public string Url { get; private set; }
        public string TimeStamp { get; private set; }
        public string Description { get; private set; }

        private News() { }

        public News(string title, string url, string timeStamp, string description) {
            Title = title;
            Url = url;
            TimeStamp = timeStamp;
            Description = description;
        }

        public override bool Equals(object obj) {
            return obj is News news &&
                Title == news.Title &&
                Url == news.Url &&
                TimeStamp == news.TimeStamp &&
                Description == news.Description;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Title, Url, TimeStamp, Description);
        }
    }
}