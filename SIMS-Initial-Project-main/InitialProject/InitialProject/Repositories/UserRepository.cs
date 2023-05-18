using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> _serializer;

        private List<User> _users;

        public UserRepository()
        {
            _serializer = new Serializer<User>();
            _users = _serializer.FromCSV(FilePath);
        }

        public void UpdateUser(User user)
        {
            User userFound = _users.Find(x => x.Id == user.Id);
            _users.Remove(userFound);
            _users.Add(user);
            _serializer.ToCSV(FilePath, _users);
        }

        public User GetById(int Id)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Id == Id);
        }

        public User GetByUsername(string username)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public User CheckIfSuperGuest(User user) {

            bool expired = false;

            if(user.DateSuperGuest.AddYears(1) < DateTime.Now) { expired = true; }

            if(expired)
            {
                user.IsSuperGuest = false;
                user.Points = 0;
                this.UpdateUser(user);
            }
            
            return user;
        
        }


    }
}
