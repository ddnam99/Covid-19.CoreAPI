namespace Covid_19.CoreAPI.Models
{
    public class Timeline
    {
        public string TimeStamp { get; private set; }
        public string Content { get; private set; }

        public Timeline(string timeStamp, string content)
        {
            TimeStamp = timeStamp;
            Content = content;
        }
    }
}