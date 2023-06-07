using InitialProject.Domain;
using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repository;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace InitialProject.Repositories
{
    public class ForumCommentRepository : IForumCommentRepositoryInterface
    {
        private const string FilePath = "../../../Resources/Data/forumcomments.csv";

        private readonly Serializer<ForumComment> _serializer;

        private List<ForumComment> _forumComments;

        private ReservationRepository _reservationRepository;

        private UserRepository _userRepository;

        public ForumCommentRepository()
        {
            _serializer = new Serializer<ForumComment>();
            _forumComments = _serializer.FromCSV(FilePath);
            _reservationRepository = new ReservationRepository();
            _userRepository = new UserRepository();
        }

        public List<ForumComment> GetAllForumComments(int forumId)
        {
            return _forumComments.FindAll(fc => fc.ForumId == forumId);
        }

        public ForumComment Save(ForumComment comment)
        {
            comment.Id = NextId();
            _forumComments = _serializer.FromCSV(FilePath);
            _forumComments.Add(comment);
            _serializer.ToCSV(FilePath, _forumComments);
            return comment;
        }

        public int NextId()
        {
            _forumComments = _serializer.FromCSV(FilePath);
            if (_forumComments.Count < 1)
            {
                return 1;
            }
            return _forumComments.Max(c => c.Id) + 1;
        }
        public void Delete(ForumComment comment)
        {
            _forumComments = _serializer.FromCSV(FilePath);
            ForumComment founded = _forumComments.Find(c => c.Id == comment.Id);
            _forumComments.Remove(founded);
            _serializer.ToCSV(FilePath, _forumComments);
        }

        public List<ForumComment> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public ForumComment Update(ForumComment comment)
        {
            _forumComments = _serializer.FromCSV(FilePath);
            ForumComment current = _forumComments.Find(c => c.Id == comment.Id);
            int index = _forumComments.IndexOf(current);
            _forumComments.Remove(current);
            _forumComments.Insert(index, comment);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _forumComments);
            return comment;
        }


        public string AddGuestComment(ForumComment comment)
        {
            comment.Id = this.GetLastId() + 1;


            List<ReservationAccommodation> reservations = _reservationRepository.GetAllByGuestId(comment.UserId);
            User user = _userRepository.GetById(comment.UserId);

            if (user != null && reservations != null && reservations.Count > 0)
            {

                comment.IsSpecialGuestComment = true;

            }

            _forumComments.Add(comment);
            _serializer.ToCSV(FilePath, _forumComments);


            return "Successfully created comment!";
        }

        public string AddOwnerComment(ForumComment comment)
        {
            comment.Id = this.GetLastId() + 1;


            User user = _userRepository.GetById(comment.UserId);

            comment.IsSpecialOwnerComment = true;

            _forumComments.Add(comment);
            _serializer.ToCSV(FilePath, _forumComments);


            return "Successfully created comment!";
        }


        public int GetLastId()
        {
            if (_forumComments != null && _forumComments.Count > 0)
            {
                return _forumComments.Max(f => f.Id);
            }
            return 0;
        }
    }
}
