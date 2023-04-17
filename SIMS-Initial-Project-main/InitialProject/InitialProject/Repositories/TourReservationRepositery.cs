using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Forms;
using InitialProject.Repository;
using InitialProject.Serializer;
using InitialProject.WPF.Views.Guest2;
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
        
        public TourReservationRepositery()
        {
            _serializer = new Serializer<TourReservations>();
            _tourreservations = _serializer.FromCSV(FilePath);
        }

        public List<TourReservations> GetAllReservationsByGuestId(int GuestId)
        {
            _tourreservations = _serializer.FromCSV(FilePath);
            return _tourreservations.FindAll(t => t.GuestId == GuestId);

        }

        public List<TourReservations> GetAllReservationsByTourId(int TourId)
        {
            List<TourReservations> allReservations = _serializer.FromCSV(FilePath);

            List<TourReservations> reservationsByTour = allReservations
                .Where(reservation => reservation.TourId == TourId)
                .ToList();

            return reservationsByTour;
        }

        public List<TourReservations> CheckReservedTourStatus()
        {
            List<TourReservations> endedTours = new List<TourReservations>();
            Tour tours = new Tour();
            foreach (var tour in endedTours)
            {
                tours = _tourRepository.GetTourById(tour.TourId);
                if (tours.Status == "Ended")
                {
                    endedTours.Add(tour);
                }
            }
            return endedTours;
        }

        public void SaveReservation(TourReservations tourreservations)
        {

            tourreservations.TourReservationId = NextId();
            _tourreservations.Add(tourreservations);
            _serializer.ToCSV(FilePath, _tourreservations);
        }

        public int NextId()
        {
           
            if (_tourreservations.Count < 1)
            {
                return 1;
            }
            return _tourreservations.Max(t=> t.TourReservationId) + 1;
        }

        
    }
}
