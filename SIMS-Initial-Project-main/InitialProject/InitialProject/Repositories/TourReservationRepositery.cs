using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Forms;
using InitialProject.Repository;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repositories
{
    public class TourReservationRepositery : ITourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourreservations.csv";

        private readonly Serializer<TourReservations> _serializer;

        private TourRepository _tourRepository;

        private List<TourReservations> _tourreservations;

        public List<TourReservations> GetAllReservationsByGuestId(int GuestId)
        {
            List<TourReservations> allReservations = _serializer.FromCSV(FilePath);

            List<TourReservations> reservationsByGuest = allReservations
                .Where(reservation => reservation.GuestId == GuestId)
                .ToList();

            return reservationsByGuest;
        }

        public List<TourReservations> GetAllReservationsByTourId(int TourId)
        {
            List<TourReservations> allReservations = _serializer.FromCSV(FilePath);

            List<TourReservations> reservationsByTour = allReservations
                .Where(reservation => reservation.TourId == TourId)
                .ToList();

            return reservationsByTour;
        }

        public TourReservations SaveReservation(TourReservations tourreservations)
        {

            tourreservations.TourReservationId = NextId();
            _tourreservations = _serializer.FromCSV(FilePath);
            _tourreservations.Add(tourreservations);
            _serializer.ToCSV(FilePath, _tourreservations);
            return tourreservations;
        }

        public int NextId()
        {
            _tourreservations = _serializer.FromCSV(FilePath);
            if (_tourreservations.Count < 1)
            {
                return 1;
            }
            return _tourreservations.Max(t=> t.TourReservationId) + 1;
        }
    }
}
