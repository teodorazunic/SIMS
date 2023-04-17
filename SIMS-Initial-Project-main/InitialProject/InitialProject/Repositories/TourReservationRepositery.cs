using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Forms;
using InitialProject.Repository;
using InitialProject.Serializer;
using InitialProject.View;
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

        private readonly Serializer<TourReservations> _serializer;

        private TourRepository _tourRepository;

        

        private List<TourReservations> _tourreservations;
        
        public TourReservationRepositery()
        {
            _serializer = new Serializer<TourReservations>();
            _tourreservations = _serializer.FromCSV(FilePath);
            _tourRepository = new TourRepository();
            
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
                    tourReservation.TourId = Convert.ToInt32(fields[1]);
                    tourReservation.GuestId = Convert.ToInt32(fields[2]);
                    tourReservation.NumberOfGuests = Convert.ToInt32(fields[3]);
                    tourReservation.UsedVoucher = Convert.ToBoolean(fields[4]);
                    tourReservations.Add(tourReservation);


                }
            }
            return tourReservations;
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
