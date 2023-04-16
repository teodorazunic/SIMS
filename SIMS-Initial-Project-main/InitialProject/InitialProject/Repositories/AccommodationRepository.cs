using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InitialProject.Repository
{
    public class AccommodationRepository: IAccommodationRepository
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


        public Accommodation GetAccommodationByName(string name)
        {
            List<Accommodation> accommodationList = GetAllAccomodations();
            foreach (Accommodation accommodation in accommodationList)
            {
                if (accommodation.Name == name)
                    return accommodation;
            }
            return null;
        }
        public List<Accommodation> findByName(List<Accommodation> accommodations, string name)
        {
            if (name != null && name != "")
            {
                return accommodations.FindAll(a => a.Name.ToLower().Contains(name.ToLower()));
            }

            return accommodations;
        }
        private List<Accommodation> findByCity(List<Accommodation> accommodations, string city)
        {
            if (city != null && city != "")
            {
                return accommodations.FindAll(a => a.Location.City.ToLower().Contains(city.ToLower()));
            }

            return accommodations;
        }

        private List<Accommodation> findByCountry(List<Accommodation> accommodations, string country)
        {
            if (country != null && country != "")
            {
                return accommodations.FindAll(a => a.Location.Country.ToLower().Contains(country.ToLower()));
            }

            return accommodations;
        }

        private List<Accommodation> findByType(List<Accommodation> accommodations, string type)
        {
            if (type != null && type != "")
            {
                AccommodationType accommodationType = (AccommodationType)Enum.Parse(typeof(AccommodationType), type);
                return accommodations.FindAll(a => a.Type == accommodationType);
            }

            return accommodations;
        }

        private List<Accommodation> findByGuestsNumber(List<Accommodation> accommodations, int guestsNumber)
        {
            if (guestsNumber > 0)
            {
                return accommodations.FindAll(a => a.GuestsNumber >= guestsNumber);
            }

            return accommodations;
        }

        private List<Accommodation> findByReservationDays(List<Accommodation> accommodations, int reservationDays)
        {
            if (reservationDays > 0)
            {
                return accommodations.FindAll(a => a.ReservationDays <= reservationDays);
            }

            return accommodations;
        }

        public List<Accommodation> SearchAccommodation(Accommodation accommodation, string type)
        {
            _accommodations = _serializer.FromCSV(FilePath);

            List<Accommodation> filteredAccomodations = _accommodations;

            filteredAccomodations = this.findByName(filteredAccomodations, accommodation.Name);
            filteredAccomodations = this.findByCity(filteredAccomodations, accommodation.Location.City);
            filteredAccomodations = this.findByCountry(filteredAccomodations, accommodation.Location.Country);
            filteredAccomodations = this.findByType(filteredAccomodations, type);
            filteredAccomodations = this.findByGuestsNumber(filteredAccomodations, accommodation.GuestsNumber);
            filteredAccomodations = this.findByReservationDays(filteredAccomodations, accommodation.ReservationDays);

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

        private int NextId()
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
            _accommodations.Insert(index, accommodation);
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;
        }
    }
}
