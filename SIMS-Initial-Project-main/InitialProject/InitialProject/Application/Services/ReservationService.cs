using InitialProject.Domain.Model;
using InitialProject.Domain.ServiceInterfaces;
using InitialProject.Repository;
using System;
using System.Collections.Generic;

namespace InitialProject.Application.Services
{
    public class ReservationService: IReservationService
    {
        private readonly ReservationRepository _reservationRepository;

        public ReservationService(ReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public List<Reservation> GetAll() { return _reservationRepository.GetAll(); }

        public List<ReservationAccommodation> GetAllByGuestId(int GuestId) { return _reservationRepository.GetAllByGuestId(GuestId); }

        public List<ReservationDate> GetReservationsForGuest(int guestId, int accommodationId, DateTime fromDate, DateTime toDate, int daysNumber)
        {
            return _reservationRepository.GetReservationsForGuest(guestId, accommodationId, fromDate, toDate, daysNumber);
        }

        public string CheckGuests(Reservation reservation, int guestsNumber) { return _reservationRepository.CheckGuests(reservation, guestsNumber); }

        public string DeleteReservation(Reservation reservation) { return _reservationRepository.DeleteReservation(reservation); }

        public String SaveReservation(Reservation reservation) { return _reservationRepository.SaveReservation(reservation); }
    }
}
