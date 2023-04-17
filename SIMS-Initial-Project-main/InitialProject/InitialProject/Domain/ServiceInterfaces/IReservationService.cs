using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;

namespace InitialProject.Domain.ServiceInterfaces
{
    public interface IReservationService
    {
        public List<Reservation> GetAll();

        public List<ReservationAccommodation> GetAllByGuestId(int GuestId);

        public List<ReservationDate> GetReservationsForGuest(int guestId, int accommodationId, DateTime fromDate, DateTime toDate, int daysNumber);

        public string CheckGuests(Reservation reservation, int guestsNumber);

        public string DeleteReservation(Reservation reservation);

        public String SaveReservation(Reservation reservation);
    }
}
