using InitialProject.Domain;
using InitialProject.Domain.Models;
using InitialProject.Domain.ServiceInterfaces;
using System;
using System.Collections.Generic;

namespace InitialProject.WPF.ViewModels.Guest1
{
    public class ForumPageViewModel : MainViewModel
    {
        public User User { get; set; }
        public Forum Forum { get; set; }

        public bool IsUserFormCreator { get; set; }
        private IForumService ForumService { get; set; }
        private IForumCommentService ForumCommentService { get; set; }

        public List<ForumComment> ForumComments { get; set; }

        private string _comment = "";

        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public ForumPageViewModel(User user, Forum forum)
        {
            ForumService = Injector.CreateInstance<IForumService>();
            ForumCommentService = Injector.CreateInstance<IForumCommentService>();
            User = user;
            Forum = forum;
            this.GetAllForumComments();
            IsUserFormCreator = user.Id == Forum.CreatorId;
        }

        public void CloseForum()
        {
            Forum.IsDeleted = true;
            OnPropertyChanged("Forum");
            ForumService.CloseForum(Forum.Id);
        }

        public void AddComment()
        {
            ForumComment forumComment = new ForumComment();
            forumComment.CommentText = Comment;
            forumComment.ForumId = Forum.Id;
            forumComment.DateCreated = DateTime.Now;
            forumComment.UserId = User.Id;
            forumComment.Username = User.Username;
            forumComment.IsSpecialGuestComment = false;
            forumComment.IsSpecialOwnerComment = false;

            ForumCommentService.AddGuestComment(forumComment);

            this.GetAllForumComments();
        }

        public List<ForumComment> GetAllForumComments()
        {
            ForumComments = ForumCommentService.GetAllForumComments(Forum.Id);
            return ForumComments;
        }
    }
}
