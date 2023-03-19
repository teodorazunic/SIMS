using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
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
    }
}
