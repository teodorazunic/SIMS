using InitialProject.Domain.Model;
using System.Collections.Generic;

namespace InitialProject.Domain.Models
{
    public class AccommodationDateRange
    {

        public Accommodation Accommodation { get; set; }
        public List<ReservationDate> ReservationDates { get; set; }

        public AccommodationDateRange(Accommodation accommodation, List<ReservationDate> reservationDates)
        {
            Accommodation = accommodation;
            ReservationDates = reservationDates;
        }
    }
}
