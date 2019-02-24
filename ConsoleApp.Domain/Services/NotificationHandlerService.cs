using ConsoleApp.Domain.Interfaces.Services;
using ConsoleApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Domain.Services
{
    public class NotificationHandlerService : INotificationHandlerService
    {
        private readonly ICollection<NotificationHandler> _notificationHandler;

        public NotificationHandlerService() => _notificationHandler = _notificationHandler ?? new List<NotificationHandler>();

        public void AddDomainNotification(NotificationHandler notification)
        {
            if(notification != null && !_notificationHandler.Any(x => x.Value == notification.Value))
                _notificationHandler.Add(notification);
        }

        public void RemoveNotification(NotificationHandler notification)
        {
            if (notification != null && _notificationHandler.Any(x => x.Value == notification.Value))
                _notificationHandler.Remove(notification);
        }

        public IEnumerable<NotificationHandler> GetNotifications() => _notificationHandler;

        public bool HasNotifications() => _notificationHandler.Any();

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
