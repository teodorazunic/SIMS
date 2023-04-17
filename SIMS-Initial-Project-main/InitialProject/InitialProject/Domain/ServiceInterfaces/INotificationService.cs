using InitialProject.Domain.Models;
using System.Collections.Generic;

namespace InitialProject.Domain.ServiceInterfaces
{
    public interface INotificationService
    {
        public void Save(Notification notification);

        public List<Notification> GetAllForUser(int userId);

        public void Update(Notification notification);
    }
}
