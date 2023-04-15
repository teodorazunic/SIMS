using System;
using InitialProject.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Models;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface ITourReservationRepository
    {
        public List<TourReservations> GetAllReservationsByTourId(int TourId);

        public List<TourReservations> GetAllReservationsByGuestId(int GuestId);

        public void SaveReservation(TourReservations tourreservations);

        public int NextId();


    }
}
