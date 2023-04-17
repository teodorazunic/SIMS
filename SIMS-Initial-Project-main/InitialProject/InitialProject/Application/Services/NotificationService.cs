using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.ServiceInterfaces;
using System.Collections.Generic;

namespace InitialProject.Application.Services
{
    public class NotificationService: INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public void Save(Notification notification)
        {
            _notificationRepository.Save(notification);
        }

        public List<Notification> GetAllForUser(int userId)
        {
            return _notificationRepository.GetAllForUser(userId);
        }

        public void Update(Notification notification) { _notificationRepository.Update(notification); }
    }
}
