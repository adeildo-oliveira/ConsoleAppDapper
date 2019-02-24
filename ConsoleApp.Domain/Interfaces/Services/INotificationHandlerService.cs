using ConsoleApp.Domain.Models;
using System;
using System.Collections.Generic;

namespace ConsoleApp.Domain.Interfaces.Services
{
    public interface INotificationHandlerService : IDisposable
    {
        void AddDomainNotification(NotificationHandler notification);
        void RemoveNotification(NotificationHandler notification);
        IEnumerable<NotificationHandler> GetNotifications();
        bool HasNotifications();
    }
}
