using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public int getId()
        {
            _reservations = this.GetAllReservations();
            return _reservations.Max(reservation => reservation.Id);
        }

        public List<Reservation> GetAllReservations()
        {
            _reservations = _serializer.FromCSV(FilePath);
            return _reservations;
        }

        public List<ReservationDate> GetReservationsForGuest(int guestId, int accommodationId, DateTime dateFrom, DateTime dateTo, int daysNumber)
        {
            _reservations = _serializer.FromCSV(FilePath);

            List<Reservation> accommodationReservations = _reservations.FindAll(reservation => reservation.AccommodationId == accommodationId
             && ((reservation.DateFrom >= dateFrom && reservation.DateFrom <= dateFrom.AddDays(daysNumber)) || (reservation.DateTo >= dateFrom && reservation.DateTo <= dateFrom.AddDays(daysNumber))));


            List<ReservationDate> reservationDates = new List<ReservationDate>();

            if (accommodationReservations.Count > 0)
            {
                DateTime reservedDateTo = dateFrom.AddDays(daysNumber);
                foreach (Reservation r in accommodationReservations)
                {
                    if (r.DateTo > reservedDateTo)
                    {
                        reservedDateTo = r.DateTo;
                    }
                }

                reservationDates.Add(new ReservationDate(reservedDateTo, reservedDateTo.AddDays(daysNumber + 1)));
            }
            else
            {

                for (DateTime date = dateFrom; date < dateTo.AddDays(-daysNumber); date = date.AddDays(1))
                {
                    ReservationDate reservationDate = new ReservationDate(date, date.AddDays(daysNumber));
                    reservationDates.Add(reservationDate);
                }
            }
            return reservationDates;
        }

        public string checkGuests(Reservation reservation, int guestsNumber)
        {
            Accommodation accommodation = _accommodationRepository.GetAccommodationById(reservation.AccommodationId);

            if (guestsNumber > accommodation.GuestsNumber)
            {
                return "Broj gostiju prekoracuje dozvoljeni broj gostiju";
            }
            else
            {
                reservation.GuestsNumber = guestsNumber;
                return this.SaveReservation(reservation);
            }
        }

        public String SaveReservation(Reservation reservation)
        {

            int reservationId = this.getId();
            reservation.Id = reservationId + 1;

            _reservations = _serializer.FromCSV(FilePath);
            _reservations.Add(reservation);
            _serializer.ToCSV(FilePath, _reservations);
            return "Rezervacija je uspesno sacuvana!";
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
