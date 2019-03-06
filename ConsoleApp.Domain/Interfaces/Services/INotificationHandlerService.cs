using ConsoleApp.Domain.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace ConsoleApp.Domain.Interfaces.Services
{
    public interface INotificationHandlerService : IDisposable
    {
        void AddNotification(NotificationHandler notification);
        void RemoveNotification(NotificationHandler notification);
        IEnumerable<NotificationHandler> GetNotifications();
        void AddValidationResult(ValidationResult validationResult);
        bool HasNotifications();
    }
}
