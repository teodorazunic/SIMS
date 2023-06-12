using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Forms;
using InitialProject.Repository;
using InitialProject.Serializer;
using InitialProject.WPF.Views.Guest2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repositories
{
    public class TourReservationRepositery : ITourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourreservations.csv";

        private const string FilePath1 = "../../../Resources/Data/guestontour.csv";

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
                .Where(reservation => reservation.GuestId.Id == GuestId)
                .ToList();

            return reservationsByGuest;

        }


        public List<int> GetVoucherStatistics( int id)
        {
            
            List<int> vouchers = new List<int>();
            List<TourReservations> reservations = GetAllReservationsByTourId(id);
            int usedVoucher = 0;
            int unusedVoucher = 0;
            foreach (var reservation in reservations )
            {
                if (reservation.UsedVoucher == true )
                {
                    usedVoucher++;
                }
                else
                {
                    unusedVoucher++;
                }
            }

            vouchers.Add(usedVoucher);
            vouchers.Add(unusedVoucher);
            return vouchers;


        }


        public int[] ShowStatistic(int id)
        {
            int[] statistic = new int[3];
            statistic[0] = 0;
            statistic[1] = 0;
            statistic[2] = 0;

            List<GuestOnTour> guestOnTour = ReadFromGuestOnTour(FilePath1);

            for (int i = 0; i < guestOnTour.Count(); i++)
            {
                if (guestOnTour[i].StartingKeyPoint.Tour.Id == id)
                {
                    if (guestOnTour[i].Age < 18)
                    {
                        statistic[0]++;
                    }
                    else if (guestOnTour[i].Age > 18 && guestOnTour[i].Age < 50)
                    {
                        statistic[1]++;
                    }
                    else if (guestOnTour[i].Age > 50)
                    {
                        statistic[2]++;
                    }


                }
            }
            return statistic;
        }



        public List<TourReservations> GetAllReservationsByTourId(int TourId)
        {
            List<TourReservations> allReservations = _serializer.FromCSV(FilePath);

            List<TourReservations> reservationsByTour = allReservations
                .Where(reservation => reservation.Tour.Id == TourId)
                .ToList();

            return reservationsByTour;
        }

        public TourReservations GetTourReservationByTourId(int TourId)
        {
            _tourreservations = _serializer.FromCSV(FilePath);
            return _tourreservations.Find(t => t.Tour.Id == TourId);
        }

        public List<TourReservations> CheckReservedTourStatus()
        {
            List<TourReservations> endedTours = new List<TourReservations>();
            Tour tours = new Tour();
            foreach (var tour in endedTours)
            {
                tours = _tourRepository.GetTourById(tour.Tour.Id);
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
                if (endedTour[i].GuestId.Id == GuestId)
                {
                    Tour tour = _tourRepository.GetTourById(endedTour[i].Tour.Id);
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
        
        public TourReservations FindMostAttendantTourByYear(string filename, int year)
        {
            TourReservations tourReservation = new TourReservations();
            List<TourReservations> tourReservations = ReadFromTourReservationsCsv(filename);
            List<Tour> tours = _tourRepository.GetAllTours();
            int maxGuests = 0;
            for (int i = 0; i < tours.Count; i++)
            {
                for (int j = 0; j < tourReservations.Count; j++)
                {
                    if (tours[i].Id == tourReservations[j].Tour.Id && tourReservations[j].NumberOfGuests > maxGuests && tours[i].Start.Year ==year)
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

        public Tour FindTourByTourReservationid(int id)
        {
            List<Tour> allTours = _tourRepository.GetAllTours();
            Tour tour = new Tour();
            for(int i = 0;i < allTours.Count; i++)
            {
                if (allTours[i].Id == id)
                {
                    tour = allTours[i];
                }
            }
            return tour;
        }

        public List<GuestOnTour> ReadFromGuestOnTour(string FileName)
        {
            List<GuestOnTour> guestsOnTour = new List<GuestOnTour>();

            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    GuestOnTour guest = new GuestOnTour();


                    guest.Id = Convert.ToInt32(fields[0]);
                    guest.GuestId = Convert.ToInt32(fields[1]);
                    guest.GuestName = fields[2];
                    guest.NumberOfGuests = Convert.ToInt32(fields[3]);
                    guest.StartingKeyPoint = new KeyPoint() { Id = Convert.ToInt32(fields[4]), Name = fields[5], Tour = new Tour() { Id = Convert.ToInt32(fields[6]) }, Status = fields[7] };
                    guest.Status = Convert.ToBoolean(fields[8]);
                    guest.Age = Convert.ToInt32(fields[9]);
                    guestsOnTour.Add(guest);


                }
                return guestsOnTour;
            }
        }

        public List<TourReservations> ReadFromTourReservationsCsv(string filename)
        {
            List<TourReservations> tourReservations = new List<TourReservations>();

            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    TourReservations tourReservation = new TourReservations();
                    tourReservation.TourReservationId = Convert.ToInt32(fields[0]);
                    tourReservation.Tour.Id = Convert.ToInt32(fields[1]);
                    tourReservation.GuestId.Id = Convert.ToInt32(fields[2]);
                    tourReservation.NumberOfGuests = Convert.ToInt32(fields[3]);
                    tourReservation.UsedVoucher = Convert.ToBoolean(fields[4]);
                    tourReservations.Add(tourReservation);


                }
            }
            return tourReservations;
        }

        
    }
}
