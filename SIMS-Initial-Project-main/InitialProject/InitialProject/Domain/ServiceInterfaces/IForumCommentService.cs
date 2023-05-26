using InitialProject.Domain.Models;
using System.Collections.Generic;

namespace InitialProject.Domain.ServiceInterfaces
{
    public interface IForumCommentService
    {
        public string AddGuestComment(ForumComment comment);

        public List<ForumComment> GetAllForumComments(int forumId);

    }
}
