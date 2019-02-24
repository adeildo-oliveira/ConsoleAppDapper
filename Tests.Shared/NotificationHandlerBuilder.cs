using ConsoleApp.Domain.Models;

namespace Tests.Shared
{
    public class NotificationHandlerBuilder : InMemoryBuilder<NotificationHandler>
    {
        private string _key;
        private string _value;

        public NotificationHandlerBuilder ComKey(string key)
        {
            _key = key;
            return this;
        }

        public NotificationHandlerBuilder ComValue(string value)
        {
            _value = value;
            return this;
        }

        public override NotificationHandler Instanciar() => new NotificationHandler(_key, _value);
    }
}
