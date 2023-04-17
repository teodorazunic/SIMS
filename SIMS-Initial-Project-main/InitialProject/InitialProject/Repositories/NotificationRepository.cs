using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repositories
{
    internal class NotificationRepository: INotificationRepository
    {

        private const string FilePath = "../../../Resources/Data/notifications.csv";

        private readonly Serializer<Notification> _serializer;

        private List<Notification> _notifications;

        public NotificationRepository()
        {
            _serializer = new Serializer<Notification>();
            _notifications = _serializer.FromCSV(FilePath);
        }

        public List<Notification> GetAllForUser(int userId) { return _notifications.FindAll(n => n.UserId == userId); }

        private int GetLastId()
        {
            if (_notifications != null && _notifications.Count > 0)
            {
                return _notifications.Max(notification => notification.Id);
            }
            return 0;
        }

        public void Save(Notification notification)
        {
            int lastId = GetLastId() + 1;
            notification.Id = lastId;
            _notifications.Add(notification);
            _serializer.ToCSV(FilePath, _notifications);
        }

        public void Update(Notification notification)
        {
            Notification notificationToUpdate = _notifications.Find(n => n.Id == notification.Id);
            notificationToUpdate.HasRead = true;
            _serializer.ToCSV(FilePath, _notifications);
        }
    }
}
