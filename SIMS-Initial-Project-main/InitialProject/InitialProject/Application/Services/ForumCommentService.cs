using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.ServiceInterfaces;
using System.Collections.Generic;

namespace InitialProject.Application.Services
{
    public class ForumCommentService: IForumCommentService
    {
        private IForumCommentRepositoryInterface _repository;
        public ForumCommentService(IForumCommentRepositoryInterface repository) { 
            _repository = repository;
        }

        public string AddGuestComment(ForumComment comment)
        {
            return _repository.AddGuestComment(comment);
        }

        public List<ForumComment> GetAllForumComments(int forumId)
        {
            return _repository.GetAllForumComments(forumId);
        }
    }
}
