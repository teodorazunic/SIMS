using InitialProject.Domain.Models;
using System.Collections.Generic;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IForumRepositoryInterface
    {
        public List<Forum> GetAll();
        public string CreateForum(Forum forum);
        public string CloseForum(int forumId);
        public Forum GetForumById(int id);
        public List<Forum> GetAllForumsByCreatorId(int creatorId);
        public void UpdateForum(Forum forum);
    }
}
