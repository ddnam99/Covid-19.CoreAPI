using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace Covid_19.CoreAPI.Models {
    public static class Helper {
        public static async Task<string> GetHTMLAsync(string url) {
            return await Task.Run(() => {
                var client = new RestClient(url);
                client.Timeout = -1;

                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                return WebUtility.HtmlDecode(response.Content);
            });
        }

        public static void LogError(string ErrorStr) {
            Message($"```{ErrorStr}```", "https://hooks.slack.com/services/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");
        }

        public static string Message(string Text, string Webhook, bool isblock = false) {
            var param = isblock ? Text : "{\"text\":\"" + Text.Replace("\\", "\\\\") + "\"}";

            var client = new RestClient(Webhook);
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", param, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return response.Content;
        }

        public static void SerializeObjectToFile(string path, object obj) {
            File.WriteAllText(path, JsonConvert.SerializeObject(obj, Formatting.Indented));
        }

        public static T DeserializeObjectFromFile<T>(string path) {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }

        public static string RemoveTag(string html) {
            Regex.Matches(html, "<.*?>", RegexOptions.Singleline).ToList()
                .ForEach(i => html = html.Replace(i.Value, string.Empty));

            return html;
        }
    }
}