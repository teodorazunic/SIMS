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
    }
}
