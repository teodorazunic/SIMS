using InitialProject.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class UserRepository
    {
        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> _serializer;

        private List<User> _users;

        public UserRepository()
        {
            _serializer = new Serializer<User>();
            _users = _serializer.FromCSV(FilePath);
        }

        public User GetByUsername(string username)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Username == username);
        }
        
        public List<Tour> SearchTour(string name, string city, string country, int duration, string language, int numberOfPeople)
        {
            _tours = _serializer.FromCSV(FilePath);

            List<Tour> filteredTours = _tours;


            if (name != null && name != "")
            {
                filteredTours = filteredTours.FindAll(t => t.Name.Contains(name));
            }

            if (city != null && city != "")
            {
                filteredTours = filteredTours.FindAll(t => t.Location.City == city);
            }

            if (country != null && country != "")
            {
                filteredTours = filteredTours.FindAll(t => t.Location.Country == country);
            }

            if (duration > 0)
            {
                filteredTours = filteredTours.FindAll(t => t.Duration == duration);
            }

            if (language != null && language != "")
            {
                filteredTours = filteredTours.FindAll(t => t.Language.Name == language);
            }

            if (numberOfPeople > 0)
            {
                filteredTours = filteredTours.FindAll(t => t.MaxGuests >= numberOfPeople);
            }

            return filteredTours;
        }
    }
}
