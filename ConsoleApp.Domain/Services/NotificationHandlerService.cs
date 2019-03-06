using ConsoleApp.Domain.Interfaces.Services;
using ConsoleApp.Domain.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Domain.Services
{
    public class NotificationHandlerService : INotificationHandlerService
    {
        private readonly ICollection<NotificationHandler> _notificationHandler;

        public NotificationHandlerService() => _notificationHandler = _notificationHandler ?? new List<NotificationHandler>();

        public virtual void AddValidationResult(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                _notificationHandler.Add(new NotificationHandler(error.PropertyName, error.ErrorMessage));
        }

        public virtual void AddNotification(NotificationHandler notification)
        {
            if(notification != null && !_notificationHandler.Any(x => x.Value == notification.Value))
                _notificationHandler.Add(notification);
        }

        public virtual void RemoveNotification(NotificationHandler notification)
        {
            if (notification != null && _notificationHandler.Any(x => x.Value == notification.Value))
                _notificationHandler.Remove(notification);
        }

        public virtual IEnumerable<NotificationHandler> GetNotifications() => _notificationHandler;

        public virtual bool HasNotifications() => _notificationHandler.Any();

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
