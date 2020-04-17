using System.Net;
using System.Threading.Tasks;
using RestSharp;

namespace Covid_19.CoreAPI.Models {
    public static class Helper {
        public static async Task<string> GetHTMLAsync (string url) {
            return await Task.Run (() => {
                var client = new RestClient (url);
                client.Timeout = -1;

                var request = new RestRequest (Method.GET);
                IRestResponse response = client.Execute (request);

                return WebUtility.HtmlDecode (response.Content);
            });
        }

        public static void LogError (string ErrorStr) {
            Message ($"```{ErrorStr}```", "https://hooks.slack.com/services/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");
        }

        public static string Message (string Text, string Webhook, bool isblock = false) {
            var param = isblock ? Text : "{\"text\":\"" + Text.Replace ("\\", "\\\\") + "\"}";

            var client = new RestClient (Webhook);
            client.Timeout = -1;

            var request = new RestRequest (Method.POST);
            request.AddHeader ("Content-Type", "application/json");
            request.AddParameter ("application/json", param, ParameterType.RequestBody);
            IRestResponse response = client.Execute (request);

            return response.Content;
        }
    }
}