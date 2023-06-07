using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.ServiceInterfaces;
using InitialProject.Repositories;
using System.Collections.Generic;
using System.Windows;

namespace InitialProject.Application.Services
{
    public class ForumService : IForumService
    {
        private IForumRepositoryInterface _forumRepositoryInterface;

        public ForumService(IForumRepositoryInterface forumRepositoryInterface)
        {
            _forumRepositoryInterface = forumRepositoryInterface;
        }

        public ForumService()
        {
        }

        public string CloseForum(int forumId)
        {
            return _forumRepositoryInterface.CloseForum(forumId);
        }

        public List<Forum> GetAllForumsByCreatorId(int creatorId)
        {
            return _forumRepositoryInterface.GetAllForumsByCreatorId(creatorId);
        }

        public Forum GetForumById(int id)
        {
            return _forumRepositoryInterface.GetForumById(id);
        }

        public void UpdateForum(Forum forum)
        {
            _forumRepositoryInterface.UpdateForum(forum);
        }

        public List<Forum> GetAll()
        {
            return _forumRepositoryInterface.GetAll();
        }

        public string CreateForum(Forum forum)
        {
            return _forumRepositoryInterface.CreateForum(forum);
        }


    }
}
