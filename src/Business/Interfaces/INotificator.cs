using Business.Notifications;
using System.Collections.Generic;

namespace Business.Interfaces
{
    public interface INotificator
    {
        bool HasNotifications();
        List<Notification> GetAll();
        void Add(Notification notification);
    }
}