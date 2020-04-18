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

        public static async Task<List<Timeline>> GetTimelinesAsync(string html) {
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