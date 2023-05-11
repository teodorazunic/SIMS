using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.Guest2;
using InitialProject.Domain.Model;

namespace InitialProject.Repositories
{
    public class TourRequestRepository : ITourRequestRepository
    {

        private const string FilePath = "../../../Resources/Data/tourrequest.csv";

        private readonly Serializer<TourRequest> _serializer;

        private List<TourRequest> _requests;

        public TourRequestRepository()
        {
            _serializer = new Serializer<TourRequest>();
            _requests = _serializer.FromCSV(FilePath);

        }

        public List<TourRequest> GetAllTourRequests()
        {
            _requests = _serializer.FromCSV(FilePath);
            return _requests;
        }

        public List<TourRequest> findByLanguage(List<TourRequest> tourRequests, string language)
        {
            if (language != null && language != "")
            {
                return tourRequests.FindAll(a => a.Language.ToLower().Contains(language.ToLower()));
            }

            return tourRequests;
        }
        public List<TourRequest> findByCity(List<TourRequest> tourRequests, string city)
        {
            if (city != null && city != "")
            {
                return tourRequests.FindAll(a => a.Location.City.ToLower().Contains(city.ToLower()));
            }

            return tourRequests;
        }

        public List<TourRequest> findByCountry(List<TourRequest> tourRequests, string country)
        {
            if (country != null && country != "")
            {
                return tourRequests.FindAll(a => a.Location.Country.ToLower().Contains(country.ToLower()));
            }

            return tourRequests;
        }

        public List<TourRequest> findByMaxGuests(List<TourRequest> tourRequests, int maxGuests)
        {
            if (maxGuests > 0)
            {
                return tourRequests.FindAll(a => a.MaxGuests == maxGuests);
            }

            return tourRequests;
        }

        public List<TourRequest> SearchRequests(TourRequest tourRequest)
        {
            _requests = _serializer.FromCSV(FilePath);

            List<TourRequest> filteredRequests = _requests;

            filteredRequests = this.findByLanguage(filteredRequests, tourRequest.Language);
            filteredRequests = this.findByCity(filteredRequests, tourRequest.Location.City);
            filteredRequests = this.findByCountry(filteredRequests, tourRequest.Location.Country);
            filteredRequests = this.findByMaxGuests(filteredRequests, tourRequest.MaxGuests);
            return filteredRequests;
        }

        public int NextId()
        {
            _requests = _serializer.FromCSV(FilePath);
            if (_requests.Count < 1)
            {
                return 1;
            }
            return _requests.Max(t => t.Id) + 1;
        }

        public TourRequest Save(TourRequest request)
        {
            request.Id = NextId();
            _requests = _serializer.FromCSV(FilePath);
            _requests.Add(request);
            _serializer.ToCSV(FilePath, _requests);
            return request;
        }
    }
}
