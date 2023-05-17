using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;

namespace InitialProject.Repositories
{
    public class TourNotificatinsRepository : ITourNotificationsRepository
    {
        private const string FilePath = "../../../Resources/Data/tournotifications.csv";

        private readonly Serializer<TourNotification> _serializer;

        private List<TourNotification> _tournotifications;

        public TourNotificatinsRepository()
        {
            _serializer = new Serializer<TourNotification>();
            _tournotifications = _serializer.FromCSV(FilePath);

        }

        public TourNotification Save(TourNotification notifications)
        {
            notifications.Id = NextId();
            _tournotifications = _serializer.FromCSV(FilePath);
            _tournotifications.Add(notifications);
            _serializer.ToCSV(FilePath, _tournotifications);
            return notifications;
        }

        public int NextId()
        {
            _tournotifications = _serializer.FromCSV(FilePath);
            if (_tournotifications.Count < 1)
            {
                return 1;
            }
            return _tournotifications.Max(t => t.Id) + 1;
        }

        public List<TourNotification> GetAllNotificationsByGuestId(int GuestId) 
        {
            List<TourNotification> allNotifications = _serializer.FromCSV(FilePath);

            List<TourNotification> notificationsByGuest = allNotifications
                .Where(n => n.GuestId.Id == GuestId)
                .ToList();

            return notificationsByGuest;
        }
    }
}
