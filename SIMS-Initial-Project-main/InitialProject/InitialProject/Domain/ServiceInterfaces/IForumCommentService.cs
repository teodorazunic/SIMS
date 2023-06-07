using InitialProject.Domain.Models;
using System.Collections.Generic;

namespace InitialProject.Domain.ServiceInterfaces
{
    public interface IForumCommentService
    {
        public string AddOwnerComment(ForumComment comment);
        public string AddGuestComment(ForumComment comment);

        public void ReportComment(ForumComment selectedComment);


        public List<ForumComment> GetAllForumComments(int forumId);
        
    }
}
