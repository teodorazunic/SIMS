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
            _tourRepository = new TourRepository();
            _tourreservations = _serializer.FromCSV(FilePath);
        }

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

        public TourReservations GetTourReservationByTourId(int TourId)
        {
            _tourreservations = _serializer.FromCSV(FilePath);
            return _tourreservations.Find(t => t.TourId == TourId);
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

        public List<Tour> GetEndedToursByGuestId(int GuestId)
        {
            List<Tour> selectedTour = _tourRepository.GetAllTours();
            List<TourReservations> endedTour = CheckReservedTourStatus();
            for(int i = 0; i < endedTour.Count(); i++)
            {
                if (endedTour[i].GuestId == GuestId)
                {
                    Tour tour = _tourRepository.GetTourById(endedTour[i].TourId);
                    selectedTour.Add(tour);
                }
            }
            return selectedTour;
        }

        public List<TourReservations> GetNotRatedTours(int GuestId) 
        {
            List<TourReservations> notRated = new List<TourReservations>();
            notRated = GetAllReservationsByGuestId(GuestId);
            for(int i = 0; i < notRated.Count(); i++)
            {
                if (notRated[i].Status == "Not")
                {
                    notRated.Add(notRated[i]);
                }
            }
            return notRated;
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
        
        public TourReservations FindMostAttendantTourByYear(string filename, string year)
        {
            TourReservations tourReservation = new TourReservations();
            List<TourReservations> tourReservations = ReadFromTourReservationsCsv(filename);
            List<Tour> tours = _tourRepository.GetAllTours();
            int maxGuests = 0;
            for (int i = 0; i < tours.Count; i++)
            {
                for (int j = 0; j < tourReservations.Count; j++)
                {
                    if (tours[i].Id == tourReservations[j].TourId && tourReservations[j].NumberOfGuests > maxGuests && tours[i].Start.Year == Convert.ToInt32(year))
                    {
                        maxGuests = tourReservations[j].NumberOfGuests;
                        tourReservation = tourReservations[j];

                    }
                }
            }

            return tourReservation;

        }

        public TourReservations FindMostAttendantTour(string filename)
        {
            TourReservations tourReservation = new TourReservations();
            List<TourReservations> tourReservations = ReadFromTourReservationsCsv(filename);
            int maxGuests = 0;

            for(int i =0; i < tourReservations.Count; i++)
            {
                if (tourReservations[i].NumberOfGuests > maxGuests)
                {
                    maxGuests = tourReservations[i].NumberOfGuests;
                    tourReservation = tourReservations[i];

                }
            }

            return tourReservation;
        }

        
    }
}
