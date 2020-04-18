using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Covid_19.CoreAPI.Models {
    public class News {
        public string Title { get; private set; }
        public string Url { get; private set; }
        public string Src { get; private set; }
        public string Description { get; private set; }

        private News() { }

        public News(string title, string url, string src, string description) {
            Title = title;
            Url = url;
            Src = src;
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
                return Regex.Matches(htmlNews, "<div class=.*?src=\"(?<src>.*?)\".*?href=\"(?<url>.*?)\">(?<title>.*?)</.*?Description.*?>(?<description>.*?)<", RegexOptions.Singleline)
                    .Select(i => new News() {
                        Title = Helper.RemoveTag(i.Groups["title"].Value),
                            Url = i.Groups["url"].Value,
                            Src = i.Groups["src"].Value.StartsWith("/") ? $"https://ncov.moh.gov.vn{i.Groups["src"].Value}" : i.Groups["src"].Value,
                            Description = i.Groups["description"].Value
                    }).ToList();
            });
        }

        public override bool Equals(object obj) {
            return obj is News news &&
                Title == news.Title &&
                Url == news.Url &&
                Src == news.Src &&
                Description == news.Description;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Title, Url, Src, Description);
        }
    }
}