namespace InitialProject.Domain.Model
{
    public class ReservationAccommodation
    {
        public Accommodation SelectedAccommodation { get; set; }

        public Reservation SelectedReservation { get; set; }

        public ReservationAccommodation(Accommodation accommodation, Reservation reservation)
        {
            SelectedAccommodation = accommodation;
            SelectedReservation = reservation;
        }

        public ReservationAccommodation() { }

    }
}
