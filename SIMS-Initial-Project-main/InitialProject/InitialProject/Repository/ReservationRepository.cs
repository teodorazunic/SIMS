using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.IO;

namespace InitialProject.Repository
{
    public class ReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/reservations.csv";

        private readonly Serializer<Reservation> _serializer;

        private AccommodationRepository _accommodationRepository;

        private List<Reservation> _reservations;

        public ReservationRepository()
        {
            _serializer = new Serializer<Reservation>();
            _accommodationRepository = new AccommodationRepository();
            _reservations = _serializer.FromCSV(FilePath);
        }

        public List<Reservation> GetAllReservations()
        {
            _reservations = _serializer.FromCSV(FilePath);
            return _reservations;
        }

        public String GetReservationsForGuest(int guestId, int accommodationId, string dateFrom, string dateTo, int daysNumber, int guestsNumber)
        {
            _reservations = _serializer.FromCSV(FilePath);

            DateTime dateFromDT = DateTime.Parse(dateFrom);
            DateTime dateToDT = DateTime.Parse(dateTo);

            List<Reservation> accommodationReservations = _reservations.FindAll(reservation => reservation.AccommodationId == accommodationId
             && ((reservation.DateFrom>= dateFromDT && reservation.DateFrom <= dateToDT) || (reservation.DateTo >= dateFromDT && reservation.DateTo <= dateToDT)));

            if(accommodationReservations.Count > 0)
            {
                return "Smestaj je vec rezervisan, mozete rezervisati smestaj za datum" + dateToDT.AddDays(1) + " - " + dateToDT.AddDays(daysNumber+1);
            }

            Reservation reservation = new Reservation(guestId, accommodationId, dateFromDT, dateToDT, daysNumber, guestsNumber);

            return this.SaveReservation(reservation);
        }

        public String SaveReservation(Reservation reservation)
        {
            Accommodation accommodation = _accommodationRepository.GetAccommodationById(reservation.AccommodationId);

            if (reservation.GuestsNumber > accommodation.GuestsNumber)
            {
                return "Broj gostiju prekoracuje dozvoljeni broj gostiju";
            }

            _reservations = _serializer.FromCSV(FilePath);
            _reservations.Add(reservation);
            _serializer.ToCSV(FilePath, _reservations);
            return "Rezervacija je uspesno sacuvana";
        }


        public List<Reservation> ReadFromReservationsCsv(string FileName)
        {
            List<Reservation> reservations = new List<Reservation>();

            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    Reservation reservation = new Reservation();
                    reservation.Id = Convert.ToInt32(fields[0]);
                    reservation.GuestId = Convert.ToInt32(fields[1]);
                    reservation.AccommodationId = Convert.ToInt32(fields[2]);
                    reservation.DateFrom = Convert.ToDateTime(fields[3]);
                    reservation.DateTo = Convert.ToDateTime(fields[4]);
                    reservation.DaysNumber = Convert.ToInt32(fields[5]);
                    reservation.GuestsNumber = Convert.ToInt32(fields[6]);
                    reservations.Add(reservation);

                }
            }
            return reservations;
        }
    }
}
