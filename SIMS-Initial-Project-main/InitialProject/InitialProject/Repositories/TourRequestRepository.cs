using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.Guest2;

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
