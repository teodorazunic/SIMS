using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InitialProject.Domain.Model;
using System.Windows;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Serializer;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Forms;
using InitialProject.Domain.Models;

namespace InitialProject.Repositories
{
    internal class ReservationMovingRepository : IReservationMovingRepository
    {
        private const string FilePath = "../../../Resources/Data/movingrequests.csv";
        private readonly Serializer<ReservationMoving> _serializer;
        private readonly ReservationRepository reservationRepository;
        private readonly AccommodationRepository accommodationRepository;
        private readonly NotificationRepository notificationRepository;
        private List<ReservationMoving> _reservations;

        public ReservationMovingRepository()
        {
            _serializer = new Serializer<ReservationMoving>();
            _reservations = _serializer.FromCSV(FilePath);
            reservationRepository = new ReservationRepository();
            accommodationRepository = new AccommodationRepository();
            notificationRepository = new NotificationRepository();
        }

        public List<ReservationMoving> GetAllPending()
        {
            return _reservations.FindAll(req => req.Status == RequestStatus.pending);
        }

        public List<ReservationMoving> GetAll()
        {
            return _reservations;
        }

        public ReservationMoving GetById(int id)
        {
            List<ReservationMoving> reservations = GetAll();
            foreach (ReservationMoving reservation in reservations)
            {
                if (reservation.ReservationId == id)
                    return reservation;
            }
            return null;
        }

        public void Delete(ReservationMoving reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            ReservationMoving founded = _reservations.Find(c => c.ReservationId == reservation.ReservationId);
            founded.Status = RequestStatus.rejected;
            _serializer.ToCSV(FilePath, _reservations);

            Notification notification = new Notification();
            notification.Message = "Your reservation request: " + reservation.Id + " was rejected";
            notification.UserId = reservation.GuestId;
            notification.HasRead = false;
            notificationRepository.Save(notification);
        }

        public ReservationMoving Save(ReservationMoving moveReservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            _reservations.Add(moveReservation);
            _serializer.ToCSV(FilePath, _reservations);
            return moveReservation;
        }

        public void MoveReservation(int guestId, int requestId, int id, DateTime newStartDate, DateTime newEndDate)
        {

            ReservationMoving request = _reservations.Find(req => req.Id == requestId);
            request.Status = RequestStatus.approved;
            _serializer.ToCSV(FilePath, _reservations);

            Notification notification = new Notification();
            notification.Message = "Your reservation request: " + requestId + " was approved";
            notification.UserId = guestId;
            notification.HasRead = false;
            notificationRepository.Save(notification);

            List<Reservation> reservations = reservationRepository.GetAll();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.Id == id)
                {
                    reservation.DateFrom = newStartDate;
                    reservation.DateTo = newEndDate;
                    reservationRepository.Update(reservation);
                    MessageBox.Show("Reservation succesfuly changed.");
                }
            }
        }


        public string TextForReservationInfo(int reservationId, int accommodationId, DateTime newStartDate, DateTime newEndDate)
        {
            string InfoText;
            string Available;
            List<Reservation> reservations = reservationRepository.GetAll();
            List<Reservation> reservationData = new List<Reservation>();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.Id != reservationId)
                    reservationData.Add(reservation);

            }
            if (IsAvailable(reservationData, accommodationId, newStartDate, newEndDate))
            {
                Available = "is available";
            }
            else
            {
                Available = "not available";
            }
            InfoText = "Accommodation with ID " + accommodationId + " " + Available + " for requested period";
            return InfoText;
        }

        public bool IsAvailable(List<Reservation> reservations, int accommodationId, DateTime startDate, DateTime endDate)
        {
            Accommodation accommodation = accommodationRepository.GetAccommodationById(accommodationId);

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                int reservationsForDate = reservations.Where(r => r.AccommodationId == accommodationId && date >= r.DateFrom && date <= r.DateTo).Sum(r => r.GuestsNumber);

                if (reservationsForDate >= accommodation.GuestsNumber)
                {
                    return false;
                }
            }
            return true;
        }
        private int GetLastId()
        {
            if (_reservations != null && _reservations.Count > 0)
                return _reservations.Max(reservationRequest => reservationRequest.Id);

            return 0;
        }

        public List<ReservationMoving> GetAllForGuest(int GuestId)
        {
            return _reservations.FindAll(reservationsRequest => reservationsRequest.GuestId == GuestId);
        }

        public string CreateReservationRequest(ReservationMoving reservationRequest)
        {
            int reservationRequestId = this.GetLastId();
            reservationRequest.Id = reservationRequestId + 1;

            _reservations.Add(reservationRequest);
            _serializer.ToCSV(FilePath, _reservations);

            return "Zahtev je uspesno poslat!";
        }
    }

}
