namespace Covid_19.CoreAPI.Models
{
    public class Statistical
    {
        public string Name { get; private set;}
        public string Confirmed {get; private set;}
        public string Treating {get; private set;}
        public string Recovered {get; private set;}
        public string Deaths {get; private set;}

        public Statistical(string name, string confirmed, string treating, string recovered, string deaths)
        {
            Name = name;
            Confirmed = confirmed;
            Treating = treating;
            Recovered = recovered;
            Deaths = deaths;
        }
    }
}