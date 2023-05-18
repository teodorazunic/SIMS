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

        public string FindBestLocation()
        {
            DateTime lastYear = DateTime.Now.AddYears(1);
            List<TourRequest> tourRequests = new List<TourRequest>();
            tourRequests = GetAllTourRequests().Where(t => t.StartDate >= lastYear).ToList();
            //int locationId = tourRequests.GroupBy(t => t.Location).Count();
            Dictionary<string, int> locationCounts = new Dictionary<string, int>();

            foreach (var tourRequest in tourRequests)
            {
                var location = tourRequest.Location.City;

                if (locationCounts.ContainsKey(location))
                {
                    locationCounts[location]++;
                }
                else
                {
                    locationCounts[location] = 1;
                }
            }

            string mostFrequentLocation = null;
            int maxCount = 0;

            foreach (var kvp in locationCounts)
            {
                if (kvp.Value > maxCount)
                {
                    mostFrequentLocation = kvp.Key;
                    maxCount = kvp.Value;
                }
            }

            return mostFrequentLocation;

            

        }

        public List<TourRequest> CheckNeverUsedLanguage(string language)
        {
            List<TourRequest> tourRequests = _serializer.FromCSV(FilePath);
            List<TourRequest> deniedAndPending = new List<TourRequest>();
            List<TourRequest> finalRequests = new List<TourRequest>();

            foreach(var tourRequest in tourRequests)
            {
                if(tourRequest.Status == "Pending" || tourRequest.Status == "Denied")
                {
                    deniedAndPending.Add(tourRequest);
                }

                    
            }

            foreach(var tourRequest in deniedAndPending)
            {
                if(tourRequest.Language == language)
                {
                    finalRequests.Add(tourRequest);
                }
            }

            return finalRequests;
        }

        public List<TourRequest> CheckNeverUsedCity(string city)
        {
            List<TourRequest> tourRequests = _serializer.FromCSV(FilePath);
            List<TourRequest> deniedAndPending = new List<TourRequest>();
            List<TourRequest> finalRequests = new List<TourRequest>();

            foreach (var tourRequest in tourRequests)
            {
                if (tourRequest.Status == "Pending" || tourRequest.Status == "Denied")
                {
                    deniedAndPending.Add(tourRequest);
                }


            }

            foreach (var tourRequest in deniedAndPending)
            {
                if (tourRequest.Location.City == city)
                {
                    finalRequests.Add(tourRequest);
                }
            }

            return finalRequests;
        }

        public string FindMostWantedCity()
        {
            DateTime lastYear = DateTime.Now.AddYears(-1);
            List<TourRequest> tourRequests = new List<TourRequest>();
            tourRequests = GetAllTourRequests().Where(t => t.StartDate >= lastYear).ToList();
            string bestCity = tourRequests.GroupBy(l => l.Location.City).OrderByDescending(g => g.Count())
                                  .Select(g =>  g.Key).FirstOrDefault();

            return bestCity;
          

        }

        public string FindMostWantedLanguage()
        {
            DateTime lastYear = DateTime.Now.AddYears(-1);
            List<TourRequest> tourRequests = new List<TourRequest>();
            tourRequests = GetAllTourRequests().Where(t => t.StartDate >= lastYear).ToList();
            string bestLanguage = tourRequests.GroupBy(l => l.Language).OrderByDescending(g => g.Count())
                                  .Select(g => g.Key).FirstOrDefault();

            return bestLanguage;


        }

        public List<DateTime> AvailableDates(DateTime start, DateTime end)
        {
            _requests = _serializer.FromCSV(FilePath);
            DateTime finalStart = new DateTime();
            DateTime finalEnd = new DateTime();
            List<DateTime> availabledates = new List<DateTime>();
            foreach (var request in _requests)
            {
                if(request.StartDate ==  start && request.EndDate == end) { 
                    finalEnd = request.EndDate;
                    finalStart = request.StartDate;
                }
                else if (request.StartDate < start && request.EndDate < end)
                {
                    finalEnd = request.EndDate;
                    finalStart = start;

                }
                else if(request.StartDate > start && request.EndDate > end)
                {
                    finalEnd = end;
                    finalStart = request.StartDate;
                }
                else if(request.StartDate < start && request.EndDate > end)
                {
                    finalStart = start;
                    finalEnd = end;
                }
                else if(request.StartDate > start && request.EndDate < end)
                {
                    finalEnd =request.EndDate;
                    finalStart= request.StartDate;
                }
            }

           
            availabledates.Add(finalEnd);
            availabledates.Add(finalStart);
            return availabledates;

        }

        public int Statistic(string city, string country, string years, string language )
        {

            List<TourRequest> allRequests =GetAllTourRequests();
            List<TourRequest> dobri = new List<TourRequest>();
            List<TourRequest> requests = new List<TourRequest>();

            

            if (years != null || years != "All years")
            {
                for (int i = 0; i < allRequests.Count; i++)
                {
                    if (allRequests[i].StartDate.Year.ToString() == years)
                    {
                        requests.Add(allRequests[i]);
                    }
                }
            }
            requests = allRequests;

            if (city != null || city != "")
            {
                for (int i = 0; i < requests.Count; i++)
                {
                    if (requests[i].Location.City == city)
                    {
                        dobri.Add(requests[i]);
                    }
                }
                return dobri.Count();

            }
            else if (language != null || language != "")
            {
                for (int i = 0; i < requests.Count; i++)
                {

                    if (requests[i].Language == language)
                    {
                        dobri.Add(requests[i]);
                    }
                }

                return dobri.Count();


            }
            else if (country != null || country != "")
            {
                for (int i = 0; i < requests.Count; i++)
                {

                    if (requests[i].Location.Country == country)
                    {
                        dobri.Add(requests[i]);
                    }
                }

                return dobri.Count();
            }

            return dobri.Count();

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

        public List<TourRequest> findByDate(List<TourRequest> tourRequests, DateTime start, DateTime end)
        {
            if (start != DateTime.MinValue)
            {
                tourRequests = tourRequests.FindAll(a => a.StartDate >= start);
            }

            if (end != DateTime.MaxValue)
            {
                tourRequests = tourRequests.FindAll(a => a.EndDate <=  end);
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

        public List<TourRequest> SearchByDate(TourRequest tourRequest)
        {
            _requests = _serializer.FromCSV(FilePath);

            List<TourRequest> filteredRequests = _requests;

            filteredRequests = this.findByDate(filteredRequests, tourRequest.StartDate, tourRequest.EndDate);
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
        
         public void CancelRequest()
        {
            DateTime currentDate = DateTime.Now;
            _requests = _serializer.FromCSV(FilePath);
            foreach(var request in _requests)
            {
                if ((request.StartDate - currentDate).TotalHours < 48)
                {
                    request.Status = "Denied";
                }
            }
            _serializer.ToCSV(FilePath, _requests);
        }
    }
}
