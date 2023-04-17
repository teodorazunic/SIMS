using InitialProject.Domain.Models;
using System.Collections.Generic;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface INotificationRepository
    {
        public void Save(Notification notification);

        public List<Notification> GetAllForUser(int userId);

        public void Update(Notification notification);
    }
}
