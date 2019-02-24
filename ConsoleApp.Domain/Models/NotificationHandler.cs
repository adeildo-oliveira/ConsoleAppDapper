namespace ConsoleApp.Domain.Models
{
    public class NotificationHandler
    {
        public NotificationHandler(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; private set; }
        public string Value { get; private set; }
    }
}
