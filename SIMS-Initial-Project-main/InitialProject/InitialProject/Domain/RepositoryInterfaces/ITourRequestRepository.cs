using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;
using InitialProject.Domain.Models;

namespace InitialProject.Domain.RepositoryInterfaces
{
    internal interface ITourRequestRepository
    {

        public int NextId();

        public TourRequest Save(TourRequest request);

        public List<TourRequest> GetAllTourRequests();

        public List<TourRequest> findByLanguage(List<TourRequest> tourRequests, string language);

        public List<TourRequest> findByCity(List<TourRequest> tourRequests, string city);

        public List<TourRequest> findByCountry(List<TourRequest> tourRequests, string country);

        public List<TourRequest> findByMaxGuests(List<TourRequest> tourRequests, int maxGuests);

        public List<TourRequest> SearchRequests(TourRequest tourRequest);


    }
}
