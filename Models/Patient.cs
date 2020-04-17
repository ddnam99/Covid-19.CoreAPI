namespace Covid_19.CoreAPI.Models
{
    public class Patient
    {
        public string PatientCode { get; private set; }
        public string Age { get; private set; }
        public string Gender { get; private set; }
        public string Address { get; private set; }
        public string Status { get; private set; }
        public string Nationality { get; private set; }

        public Patient(string patientCode, string age, string gender, string address, string status, string nationality)
        {
            PatientCode = patientCode;
            Age = age;
            Gender = gender;
            Address = address;
            Status = status;
            Nationality = nationality;
        }

        public override bool Equals(object obj)
        {
            return obj is Patient patient &&
                   PatientCode == patient.PatientCode &&
                   Age == patient.Age &&
                   Gender == patient.Gender &&
                   Address == patient.Address &&
                   Status == patient.Status &&
                   Nationality == patient.Nationality;
        }
    }
}