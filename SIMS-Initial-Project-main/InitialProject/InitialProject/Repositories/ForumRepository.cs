using InitialProject.Repository;
using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repositories
{
    public class ForumRepository : IForumRepositoryInterface
    {
        private const string FilePath = "../../../Resources/Data/forums.csv";

        private readonly Serializer<Forum> _serializer;

        private List<Forum> _forums;

        private NotificationRepository _notificationRepository;

        private AccommodationRepository _accommodationRepository;

        public ForumRepository()
        {
            _serializer = new Serializer<Forum>();
            _forums = _serializer.FromCSV(FilePath);
            _notificationRepository = new NotificationRepository();
            _accommodationRepository = new AccommodationRepository();
        }
        public string CloseForum(int forumId)
        {
            Forum forum = _forums.Find(f => f.Id == forumId);
            forum.IsDeleted = true;
            this.UpdateForum(forum);
            return "Successfully closed forum!";
        }

        public string CreateForum(Forum forum)
        {
            forum.Id = this.GetLastId() + 1;
            _forums.Add(forum);
            _serializer.ToCSV(FilePath, _forums);

            this.CreateNotificationForOwners(forum);

            return "Successfully created forum";
        }

        public void CreateNotificationForOwners(Forum forum)
        {
            List<Accommodation> accommodations = _accommodationRepository.FindAllByLocation(forum.Location);
            foreach (Accommodation a in accommodations)
            {
                Notification notification = new Notification();
                notification.Message = "New forum was created: " + forum.Id;
                notification.UserId = a.OwnerId;
                notification.HasRead = false;
                _notificationRepository.Save(notification);
            }
        }

        public List<Forum> GetAllForumsByCreatorId(int creatorId)
        {
            return _forums.FindAll(forum => forum.CreatorId == creatorId);
        }

        public Forum GetForumById(int id)
        {
            return _forums.Find(forum => forum.Id == id);
        }

        public int GetLastId()
        {
            if (_forums != null && _forums.Count > 0)
            {
                return _forums.Max(f => f.Id);
            }
            return 0;
        }

        public void UpdateForum(Forum forum)
        {
            Forum forumToRemove = _forums.Find(f => f.Id == forum.Id);
            _forums.Remove(forumToRemove);
            _forums.Add(forum);
            _serializer.ToCSV(FilePath, _forums);
        }

        public List<Forum> GetAll() {  return _forums; }
    }
}
