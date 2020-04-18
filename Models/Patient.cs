using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Covid_19.CoreAPI.Models {
    public class Patient {
        public string PatientCode { get; private set; }
        public string Age { get; private set; }
        public string Gender { get; private set; }
        public string Address { get; private set; }
        public string Status { get; private set; }
        public string Nationality { get; private set; }

        private Patient() { }

        public Patient(string patientCode, string age, string gender, string address, string status, string nationality) {
            PatientCode = patientCode;
            Age = age;
            Gender = gender;
            Address = address;
            Status = status;
            Nationality = nationality;
        }

        public static async Task<List<Patient>> GetPatientsAsync(string html) {
            return await Task.Run(() => {
                var pattern = "<table id=\"sailorTable\"(.*?)<tbody>(?<data>.*?)</tbody>";
                var statisticalHTML = Regex.Matches(html, pattern, RegexOptions.Singleline) [1].Groups["data"].Value;

                return Regex.Matches(statisticalHTML, "<tr>(.*?)</tr>", RegexOptions.Singleline).Select(i => {
                    var regex = Regex.Matches(i.Value, "<td>(?<value>.*?)</td>", RegexOptions.Singleline);

                    return new Patient() {
                        PatientCode = regex[0].Groups["value"].Value,
                            Age = regex[1].Groups["value"].Value,
                            Gender = regex[2].Groups["value"].Value,
                            Address = regex[3].Groups["value"].Value,
                            Status = regex[4].Groups["value"].Value,
                            Nationality = regex[4].Groups["value"].Value
                    };
                }).ToList();
            });
        }

        public override bool Equals(object obj) {
            return obj is Patient patient &&
                PatientCode == patient.PatientCode &&
                Age == patient.Age &&
                Gender == patient.Gender &&
                Address == patient.Address &&
                Status == patient.Status &&
                Nationality == patient.Nationality;
        }

        public override int GetHashCode() {
            return HashCode.Combine(PatientCode, Age, Gender, Address, Status, Nationality);
        }
    }
}