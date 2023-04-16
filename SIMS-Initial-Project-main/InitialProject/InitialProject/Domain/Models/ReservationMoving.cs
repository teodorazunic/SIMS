using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Model
{
    public class ReservationMoving : ISerializable
    {
        public int ReservationId { get; set; }
        public int AccommodationId { get; set; }
        public string GuestUsername { get; set; }
        public DateTime OldStartDate { get; set; }
        public DateTime OldEndDate { get; set; }
        public DateTime NewStartDate { get; set; }
        public DateTime NewEndDate { get; set; }

        public ReservationMoving() { }
        public ReservationMoving(int reservationId, int accommodationId, string guestUsername, DateTime oldStartDate, DateTime oldEndDate, DateTime newStartDate, DateTime newEndDate)
        {
            ReservationId = reservationId;
            AccommodationId = accommodationId;
            GuestUsername = guestUsername;
            OldStartDate = oldStartDate;
            OldEndDate = oldEndDate;
            NewStartDate = newStartDate;
            NewEndDate = newEndDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ReservationId.ToString(), AccommodationId.ToString(), GuestUsername, OldStartDate.ToString(), OldEndDate.ToString(), NewStartDate.ToString(), NewEndDate.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ReservationId = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            GuestUsername = values[2];
            OldStartDate = Convert.ToDateTime(values[3]);
            OldEndDate = Convert.ToDateTime(values[4]);
            NewStartDate = Convert.ToDateTime(values[5]);
            NewEndDate = Convert.ToDateTime(values[6]);
        }
    }
}

