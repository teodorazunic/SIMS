using InitialProject.Serializer;
using System;

namespace InitialProject.Domain.Models
{
    public class ForumComment : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int ForumId { get; set; }
        public string CommentText { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsSpecialGuestComment { get; set; }
        public bool IsSpecialOwnerComment { get; set; }

        public ForumComment(int userId, string username, int forumId, string commentText, DateTime dateCreated, bool isSpecialGuestComment, bool isSpecialOwnerComment)
        {
            UserId = userId;
            Username = username;
            ForumId = forumId;
            CommentText = commentText;
            DateCreated = dateCreated;
            IsSpecialGuestComment = isSpecialGuestComment;
            IsSpecialOwnerComment = isSpecialOwnerComment;
        }

        public ForumComment() { }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), UserId.ToString(), Username, ForumId.ToString(), CommentText, DateCreated.ToString(), IsSpecialGuestComment.ToString(), IsSpecialOwnerComment.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            Username = values[2];
            ForumId = Convert.ToInt32(values[3]);
            CommentText = values[4];
            DateCreated = Convert.ToDateTime(values[5]);
            IsSpecialGuestComment = Convert.ToBoolean(values[6]);
            IsSpecialOwnerComment = Convert.ToBoolean(values[7]);
        }
    }
}
