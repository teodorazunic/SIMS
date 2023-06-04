using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace InitialProject.Repository
{
    public class UserRepository: IUserRepository
    {
        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> _serializer;

        private List<User> _users;

        private readonly TourRepository _tourRepository;

        public UserRepository()
        {
            _serializer = new Serializer<User>();
            _users = _serializer.FromCSV(FilePath);
            _tourRepository = new TourRepository();
        }

        public User GetByUsername(string username)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public void Delete(User user)
        {
            _users = _serializer.FromCSV(FilePath);
            User founded = _users.Find(c => c.Id == user.Id);
            _users.Remove(founded);
            _serializer.ToCSV(FilePath, _users);
        }

       

        public List<User> ReadFromUsersCsv(string filename)
        {
            List<User> users = new List<User>();

            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    User user = new User();
                    user.Id = Convert.ToInt32(fields[0]);
                    user.Username = fields[1];
                    user.Password = fields[2];
                    user.Role = (UserRole)Enum.Parse(typeof(UserRole), fields[3], false);

                    users.Add(user);


                }
            }
            return users;
        }

        private void WriteUsersToCSV(List<User> users)
        {
            using (var writer = new StreamWriter(FilePath))
            {
                foreach (User user in users)
                {
                    writer.WriteLine($"{user.Id},{user.Username},{user.Password},{user.Role}");
                }
            }
        }

    }
}
