using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.ServiceInterfaces;
using InitialProject.Repositories;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Linq;

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

        public void ReportComment(ForumComment selectedComment)
        {
            List<ForumComment> comments = GetAllForumComments(selectedComment.ForumId);
            foreach (ForumComment comment in comments)
            {
                if (comment.ForumId == selectedComment.ForumId)
                {
                    selectedComment.NumberOfReport++;
                    MessageBox.Show("Comment successfully reported");
                }
            }
            _repository.Update(selectedComment);
        }

        public ForumComment Update(ForumComment comment)
        {
            return _repository.Update(comment);
        }



        }

    }

