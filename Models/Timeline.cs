using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Covid_19.CoreAPI.Models {
    public class Timeline {
        public string TimeStamp { get; private set; }
        public string Content { get; private set; }
        private Timeline() { }
        public Timeline(string timeStamp, string content) {
            TimeStamp = timeStamp;
            Content = content;
        }
        public static async Task<List<Timeline>> GetTimelinesAsync(int indexPage = 1) {
            var url = $"https://ncov.moh.gov.vn/web/guest/dong-thoi-gian?p_p_id=101_INSTANCE_iEPhEhL1XSde&_101_INSTANCE_iEPhEhL1XSde_cur={indexPage}";
            var html = await Helper.GetHTMLAsync(url);

            return await ConvertHTMLAsync(html);
        }
        private static async Task<List<Timeline>> ConvertHTMLAsync(string html) {
            return await Task.Run(() => {
                var contents = Regex.Matches(html, "<div class=\"timeline-head\">.*?<h3>(?<timeStamp>.*?)</h3>.*?<p>(?<content>.*?)</p>", RegexOptions.Singleline);

                return contents.Select(i => new Timeline() {
                    TimeStamp = i.Groups["timeStamp"].Value,
                        Content = i.Groups["content"].Value
                }).ToList();
            });
        }

        public override bool Equals(object obj) {
            return obj is Timeline timeline &&
                TimeStamp == timeline.TimeStamp &&
                Content == timeline.Content;
        }

        public override int GetHashCode() {
            return HashCode.Combine(TimeStamp, Content);
        }
    }
}