using InitialProject.Domain.Models;
using System.Collections.Generic;

namespace InitialProject.Domain.ServiceInterfaces
{
    public interface IForumService
    {
        public List<Forum> GetAll();
        public string CloseForum(int forumId);
        public Forum GetForumById(int id);
        public List<Forum> GetAllForumsByCreatorId(int creatorId);
        public void UpdateForum(Forum forum);
        public string CreateForum(Forum forum);
    }
}
