using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Covid_19.CoreAPI.Models {
    public class Statistical {
        public string Name { get; private set; }
        public string Confirmed { get; private set; }
        public string Treating { get; private set; }
        public string Recovered { get; private set; }
        public string Deaths { get; private set; }

        private Statistical() { }

        public Statistical(string name, string confirmed, string treating, string recovered, string deaths) {
            Name = name;
            Confirmed = confirmed;
            Treating = treating;
            Recovered = recovered;
            Deaths = deaths;
        }

        public static async Task < (Statistical VietNam, Statistical Global) > GetVietNamGlobalAsync(string html) {
            return await Task.Run(() => {
                var pattern = "<section class=\"bg-xanh123\">(?<VNTG>.*?)</section>";
                var statisticalsHTML = Regex.Match(html, pattern, RegexOptions.Singleline).Value;

                var result = statisticalsHTML.Split("<hr").ToList().Select(i => {
                    var regex = Regex.Matches(i, "<span(.*?)>(?<value>.*?)<", RegexOptions.Singleline);
                    return new Statistical() {
                        Name = regex[0].Groups["value"].Value,
                            Confirmed = regex[1].Groups["value"].Value,
                            Treating = regex[2].Groups["value"].Value,
                            Recovered = regex[3].Groups["value"].Value,
                            Deaths = regex[4].Groups["value"].Value
                    };
                }).ToList();

                return (result[0], result[1]);
            });
        }

        public static async Task<List<Statistical>> GetProvincesAsync(string html) {
            return await Task.Run(() => {
                var pattern = "<table id=\"sailorTable\"(.*?)<tbody>(?<data>.*?)</tbody>";
                var statisticalHTML = Regex.Match(html, pattern, RegexOptions.Singleline).Groups["data"].Value;

                return Regex.Matches(statisticalHTML, "<tr>(.*?)</tr>", RegexOptions.Singleline).Select(i => {
                    var regex = Regex.Matches(i.Value, "<td>(?<value>.*?)</td>", RegexOptions.Singleline);

                    return new Statistical() {
                        Name = regex[0].Groups["value"].Value,
                            Confirmed = regex[1].Groups["value"].Value,
                            Treating = regex[2].Groups["value"].Value,
                            Recovered = regex[3].Groups["value"].Value,
                            Deaths = regex[4].Groups["value"].Value
                    };
                }).ToList();
            });
        }

        public override bool Equals(object obj) {
            return obj is Statistical statistical &&
                Name == statistical.Name &&
                Confirmed == statistical.Confirmed &&
                Treating == statistical.Treating &&
                Recovered == statistical.Recovered &&
                Deaths == statistical.Deaths;
        }
    }
}