using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.ServiceInterfaces;
using InitialProject.Repositories;
using System.Collections.Generic;
using System.Windows;

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
        public string AddOwnerComment(ForumComment comment)
        {
            return _repository.AddOwnerComment(comment);
        }

        public List<ForumComment> GetAllForumComments(int forumId)
        {
            return _repository.GetAllForumComments(forumId);
        }

        public ForumComment Update(ForumComment comment)
        {
            return _repository.Update(comment);
        }



        }

    }

