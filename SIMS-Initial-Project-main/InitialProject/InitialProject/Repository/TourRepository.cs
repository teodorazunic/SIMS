using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class TourRepository
    {
        private const string FilePath = "../../../Resources/Data/tour.csv";

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);

        }

        public List<Tour> GetAllTours()
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours;
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
