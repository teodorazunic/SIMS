using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class AccommodationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodations.csv";

        private readonly Serializer<Accommodation> _serializer;

        private List<Accommodation> _accommodations;

        public AccommodationRepository()
        {
            _serializer = new Serializer<Accommodation>();
            _accommodations = _serializer.FromCSV(FilePath);
        }

        public List<Accommodation> GetAllAccomodations()
        {
            _accommodations = _serializer.FromCSV(FilePath);
            return _accommodations;
        }

        public Accommodation GetAccommodationById(int id)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            return _accommodations.Find(a => a.Id == id);
        }
        public List<Accommodation> SearchAccommodation(string name, string city, string country, string type, int guestsNumber, int reservationDays)
        {
            _accommodations = _serializer.FromCSV(FilePath);

            List<Accommodation> filteredAccomodations = _accommodations;


           if (name != null && name != "")
            {
                filteredAccomodations = filteredAccomodations.FindAll(a => a.Name.ToLower().Contains(name.ToLower()));
            }

            if (city != null && city != "")
            {
                filteredAccomodations = filteredAccomodations.FindAll(a => a.Location.City.ToLower() == city.ToLower());
           }

            if (country != null && country != "")
            {
                filteredAccomodations = filteredAccomodations.FindAll(a => a.Location.Country.ToLower() == country.ToLower());
            }

            if (type != null && type != "")
            {
                AccommodationType accommodationType = (AccommodationType) Enum.Parse(typeof(AccommodationType), type);
                filteredAccomodations = filteredAccomodations.FindAll(a => a.Type == accommodationType);
            }

            if (guestsNumber > 0)
            {
                filteredAccomodations = filteredAccomodations.FindAll(a => a.GuestsNumber >= guestsNumber);
            }

            if (reservationDays > 0)
            {
                filteredAccomodations = filteredAccomodations.FindAll(a => a.ReservationDays <= reservationDays);
            }

            return filteredAccomodations;
        }

        public Accommodation Save(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            _accommodations = _serializer.FromCSV(FilePath);
            _accommodations.Add(accommodation);
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;
        }

        public int NextId()
        {
            _accommodations = _serializer.FromCSV(FilePath);
            if (_accommodations.Count < 1)
            {
                return 1;
            }
            return _accommodations.Max(a => a.Id) + 1;
        }

        public void Delete(Accommodation accommodation)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            Accommodation founded = _accommodations.Find(a => a.Id == accommodation.Id);
            _accommodations.Remove(founded);
            _serializer.ToCSV(FilePath, _accommodations);
        }

        public Accommodation Update(Accommodation accommodation)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            Accommodation current = _accommodations.Find(a => a.Id == accommodation.Id);
            int index = _accommodations.IndexOf(current);
            _accommodations.Remove(current);
            _accommodations.Insert(index, accommodation);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;
        }
    }
}
