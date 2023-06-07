using InitialProject.Domain.Models;
using System.Collections.Generic;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IForumCommentRepositoryInterface
    {
        public string AddGuestComment(ForumComment comment);
        public string AddOwnerComment(ForumComment comment);
        public ForumComment Update(ForumComment comment);



        public List<ForumComment> GetAllForumComments(int forumId);

    }
}
