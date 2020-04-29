using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Covid_19.CoreAPI.Models {
    public class News {
        #region Properties
        private string title;
        private string url;
        private string src;
        private string poster;
        private string description;

        public string Title { get => title; private set => title = value; }
        public string Url { get => url; private set => url = value; }
        public string Src { get => src; private set => src = value.StartsWith("/") ? $"https://ncov.moh.gov.vn{value}" : value; }
        public string Poster { get => string.IsNullOrEmpty(poster) ? null : poster; private set => poster = value.StartsWith("/") ? $"https://ncov.moh.gov.vn{value}" : value; }
        public string Description { get => description; private set => description = value; }
        #endregion

        private News() { }

        public News(string title, string url, string src, string poster, string description) {
            Title = title;
            Url = url;
            Src = src;
            Poster = poster;
            Description = description;
        }

        public static async Task<List<News>> GetNewsAsync(int indexPage = 1) {
            var url = $"https://ncov.moh.gov.vn/web/guest/tin-tuc?p_p_id=101_INSTANCE_bQiShy2NRK1f&_101_INSTANCE_bQiShy2NRK1f_cur={indexPage}";
            var html = await Helper.GetHTMLAsync(url);

            return await ConvertHTMLAsync(html);
        }

        public static async Task<List<News>> GetVideosAsync(int indexPage = 1) {
            var url = $"https://ncov.moh.gov.vn/web/guest/video?p_p_id=101_INSTANCE_bQiShy2NRK1f&_101_INSTANCE_bQiShy2NRK1f_cur={indexPage}";
            var html = await Helper.GetHTMLAsync(url);

            return await ConvertHTMLAsync(html);
        }

        public static async Task<List<News>> GetKhuyenCaoAsync(int indexPage = 1) {
            var url = $"https://ncov.moh.gov.vn/web/guest/khuyen-cao?p_p_id=101_INSTANCE_bQiShy2NRK1f&_101_INSTANCE_bQiShy2NRK1f_cur={indexPage}";
            var html = await Helper.GetHTMLAsync(url);

            return await ConvertHTMLAsync(html);
        }

        public static async Task<List<News>> GetDieuCanBietAsync(int indexPage = 1) {
            var url = $"https://ncov.moh.gov.vn/web/guest/-ieu-can-biet?p_p_id=101_INSTANCE_bQiShy2NRK1f&_101_INSTANCE_bQiShy2NRK1f_cur={indexPage}";
            var html = await Helper.GetHTMLAsync(url);

            return await ConvertHTMLAsync(html);
        }

        public static async Task<List<News>> GetHoTroTrongNganhAsync(int indexPage = 1) {
            var url = $"https://ncov.moh.gov.vn/web/guest/ho-tro-trong-nganh?p_p_id=101_INSTANCE_bQiShy2NRK1f&_101_INSTANCE_bQiShy2NRK1f_cur={indexPage}";
            var html = await Helper.GetHTMLAsync(url);

            return await ConvertHTMLAsync(html);
        }

        public static async Task<List<News>> GetChiDaoDieuHanhAsync(int indexPage = 1) {
            var url = $"https://ncov.moh.gov.vn/web/guest/chi-dao-dieu-hanh?p_p_id=101_INSTANCE_iEPhEhL1XSde&_101_INSTANCE_iEPhEhL1XSde_cur={indexPage}";
            var html = await Helper.GetHTMLAsync(url);

            return await ConvertHTMLAsync(html);
        }

        private static async Task<List<News>> ConvertHTMLAsync(string html) {
            return await Task.Run(() => {
                var htmlNews = Regex.Match(html, "<div class=\"subscribe-action\">.*?<div class=\"clearfix", RegexOptions.Singleline).Value;
                return Regex.Matches(htmlNews, "<div class=.*?(|poster=\"(?<poster>.*?)\".*?)src=\"(?<src>.*?)\".*?href=\"(?<url>.*?)\">(?<title>.*?)</.*?Description.*?>(?<description>.*?)<", RegexOptions.Singleline)
                    .Select(i => new News() {
                        Title = Helper.RemoveTag(i.Groups["title"].Value),
                            Url = i.Groups["url"].Value,
                            Poster = i.Groups["poster"].Value,
                            Src = i.Groups["src"].Value,
                            Description = i.Groups["description"].Value
                    }).ToList();
            });
        }

        public override bool Equals(object obj) {
            return obj is News news &&
                Title == news.Title &&
                Url == news.Url &&
                Src == news.Src &&
                Poster == news.Poster &&
                Description == news.Description;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Title, Url, Src, Poster, Description);
        }
    }
}