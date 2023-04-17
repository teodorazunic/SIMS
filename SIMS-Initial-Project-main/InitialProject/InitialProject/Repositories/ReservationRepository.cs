using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InitialProject.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/reservations.csv";

        private readonly Serializer<Reservation> _serializer;

        private AccommodationRepository _accommodationRepository;

        private readonly NotificationRepository _notificationRepository;

        private List<Reservation> _reservations;

        public ReservationRepository()
        {
            _serializer = new Serializer<Reservation>();
            _accommodationRepository = new AccommodationRepository();
            _reservations = _serializer.FromCSV(FilePath);
            _notificationRepository = new NotificationRepository();
        }

        public Reservation Update(Reservation reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            Reservation current = _reservations.Find(c => c.Id == reservation.Id);
            int index = _reservations.IndexOf(current);
            _reservations.Remove(current);
            _reservations.Insert(index, reservation);
            _serializer.ToCSV(FilePath, _reservations);
            return reservation;
        }

        public void LogicalDeleteExpire(Reservation reservation)
        {
            if (reservation.GradeStatus != "Graded" && reservation.GradeStatus != "Expired")
            {
                reservation.GradeStatus = "Expire";
                Update(reservation);
            }
        }

        public void LogicalDelete(Reservation reservation)
        {
            reservation.GradeStatus = "Graded";
            Update(reservation);
        }


        public int GetLastId()
        {
            return _reservations.Max(reservation => reservation.Id);
        }

        public List<Reservation> GetAll()
        {
            return _reservations;
        }

        public Reservation GetReservationById(int id)
        {
            _reservations = _serializer.FromCSV(FilePath);
            return _reservations.Find(r => r.Id == id);
        }


        public List<ReservationAccommodation> GetAllByGuestId(int GuestId)
        {
            List<Reservation> guestReservations = _reservations.FindAll(reservation => reservation.GuestId == GuestId);

            List<ReservationAccommodation> reservationAccommodations = new List<ReservationAccommodation>();

            foreach (Reservation reservation in guestReservations)
            {
                Accommodation accommodation = _accommodationRepository.GetAccommodationById(reservation.AccommodationId);
                ReservationAccommodation reservationAccommodation = new ReservationAccommodation(accommodation, reservation);
                reservationAccommodations.Add(reservationAccommodation);
            }

            return reservationAccommodations;
        }

        private bool IsAvailable(List<Reservation> accommodationReservations, DateTime startDate, DateTime endDate)
        {
            foreach (Reservation reservation in accommodationReservations)
            {
                if (reservation.DateFrom < endDate && startDate < reservation.DateTo)
                {
                    return false;
                }
            }
            return true;
        }

        public List<ReservationDate> GetReservationsForGuest(int guestId, int accommodationId, DateTime fromDate, DateTime toDate, int daysNumber)
        {
            List<Reservation> accommodationReservations = _reservations.FindAll(reservation => reservation.AccommodationId == accommodationId);
            List<ReservationDate> availableDates = new List<ReservationDate>();
            DateTime currentDate = fromDate;

            while (currentDate.AddDays(daysNumber) <= toDate)
            {
                DateTime endDate = currentDate.AddDays(daysNumber);

                if (IsAvailable(accommodationReservations, currentDate, endDate))
                {
                    availableDates.Add(new ReservationDate(currentDate, endDate));
                }

                currentDate = currentDate.AddDays(1);
            }

            if (availableDates.Count == 0)
            {
                DateTime firstAvailableDate = fromDate;

                while (!IsAvailable(accommodationReservations, firstAvailableDate, firstAvailableDate.AddDays(daysNumber)))
                {
                    firstAvailableDate = firstAvailableDate.AddDays(1);
                }

                availableDates.Add(new ReservationDate(firstAvailableDate, firstAvailableDate.AddDays(daysNumber)));
            }

            return availableDates;
        }

        public string CheckGuests(Reservation reservation, int guestsNumber)
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

        public string DeleteReservation(Reservation reservation)
        {
            DateTime currentDate = DateTime.Now;
            Accommodation reservationAccommodation = _accommodationRepository.GetAccommodationById(reservation.AccommodationId);

            if (reservationAccommodation.ReservationDays > 0 &&
                reservation.DateFrom.AddDays(-reservationAccommodation.ReservationDays) < currentDate)
            {
                return "Rezervaciju nije moguce otkazati";
            }
            else if ((reservation.DateFrom - currentDate).TotalHours < 24)
            {
                return "Rezervaciju nije moguce otkazati";
            }

            Notification notification = new Notification();
            notification.Message = "Guest has canceled reservation: " + reservation.Id;
            notification.HasRead = false;
            notification.UserId = _accommodationRepository.GetAccommodationById(reservation.AccommodationId).OwnerId;
            _notificationRepository.Save(notification);

            _reservations.Remove(reservation);
            _serializer.ToCSV(FilePath, _reservations);
            return "Uspesno obrisana rezervacija!";
        }

        public String SaveReservation(Reservation reservation)
        {

            int reservationId = this.GetLastId();
            reservation.Id = reservationId + 1;

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
                    reservation.GuestUserName = fields[2];
                    reservation.AccommodationId = Convert.ToInt32(fields[3]);
                    reservation.DateFrom = Convert.ToDateTime(fields[4]);
                    reservation.DateTo = Convert.ToDateTime(fields[5]);
                    reservation.DaysNumber = Convert.ToInt32(fields[6]);
                    reservation.GuestsNumber = Convert.ToInt32(fields[7]);
                    reservations.Add(reservation);

                }
            }
            return reservations;
        }
    }
}
