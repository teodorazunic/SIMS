using System;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class TourReservations : ISerializable
    {
        public int TourId { get; set; }

        public int GuestId { get; set; }

        public int NumberOfGuests { get; set; }

        public bool UsedVoucher { get; set; }

        public TourReservations() { }

        public TourReservations(int tourId, int guestId, int numberOfGuests, bool usedVoucher)
        {
            TourId = tourId;
            GuestId = guestId;
            NumberOfGuests = numberOfGuests;
            UsedVoucher = usedVoucher;
        }

        public void FromCSV(string[] values)
        {
            throw new NotImplementedException();
        }

        public string[] ToCSV()
        {
            throw new NotImplementedException();
        }
    }
}
