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

        public String GetReservationsForGuest(int guestId, int accommodationId, DateTime dateFrom, DateTime dateTo, int daysNumber)
        {
            _reservations = _serializer.FromCSV(FilePath);

            Accommodation accommodation = _accommodationRepository.GetAccommodationById(accommodationId);

            if (accommodation.ReservationDays > daysNumber)
            {
                return "Minimalni dozvoljeni broj dana je " + accommodation.ReservationDays;
            }

            List<Reservation> accommodationReservations = _reservations.FindAll(reservation => reservation.AccommodationId == accommodationId
             && ((reservation.DateFrom >= dateFrom && reservation.DateFrom <= dateTo) || (reservation.DateTo >= dateFrom && reservation.DateTo <= dateTo)));

            if(accommodationReservations.Count > 0)
            {
                DateTime reservedDateTo = dateTo;
                foreach (Reservation r in accommodationReservations)
                {
                    if(r.DateTo > reservedDateTo)
                    {
                        reservedDateTo = r.DateTo;
                    }
                }
                String dateFromFormat = reservedDateTo.AddDays(1).ToString("dd MMMM yyyy");
                String dateToFormat = reservedDateTo.AddDays(daysNumber+1).ToString("dd MMMM yyyy");
                return "Smestaj je vec rezervisan, prvi slobodan datum je od " + dateFromFormat + " do " + dateToFormat;
            } else
            {
                return "Datumi su slobodni";
            }

     
        }

        public string checkGuests(Reservation reservation, int guestsNumber)
        {
            Accommodation accommodation = _accommodationRepository.GetAccommodationById(reservation.AccommodationId);

            if (guestsNumber > accommodation.GuestsNumber)
            {
                return "Broj gostiju prekoracuje dozvoljeni broj gostiju";
            } else
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
