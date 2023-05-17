using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain;
using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.Serializer;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface ITourNotificationsRepository
    {
        public TourNotification Save(TourNotification notifications);

        public int NextId();

        public List<TourNotification> GetAllNotificationsByGuestId(int guestId);

    }
}
