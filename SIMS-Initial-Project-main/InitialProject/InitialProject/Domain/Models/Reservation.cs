using InitialProject.Serializer;
using System;

namespace InitialProject.Domain.Model
{
    public class Reservation : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public string GuestUserName { get; set; }
        public int AccommodationId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int DaysNumber { get; set; }
        public int GuestsNumber { get; set; }
        public string GradeStatus { get; set; }

        public Reservation(int guestId, int accommodationId, DateTime dateFrom, DateTime dateTo, int daysNumber, int guestsNumber)
        {
            GuestId = guestId;
            AccommodationId = accommodationId;
            DateFrom = dateFrom;
            DateTo = dateTo;
            DaysNumber = daysNumber;
            GuestsNumber = guestsNumber;
            GradeStatus = "NotGraded";
        }

        public Reservation(int id, string guestUserName, int accommodationId, DateTime dateFrom, DateTime dateTo, int daysNumber, int guestsNumber)
        {
            Id = id;
            AccommodationId = accommodationId;
            GuestUserName = guestUserName;
            DateFrom = dateFrom;
            DateTo = dateTo;
            DaysNumber = daysNumber;
            GuestsNumber = guestsNumber;
            GradeStatus = "NotGraded";
        }
        public Reservation() { }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestId.ToString(), AccommodationId.ToString(), DateFrom.ToString(),
                DateTo.ToString(), DaysNumber.ToString(), GuestsNumber.ToString(), GradeStatus, GuestUserName };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            AccommodationId = Convert.ToInt32(values[2]);
            DateFrom = Convert.ToDateTime(values[3]);
            DateTo = Convert.ToDateTime(values[4]);
            DaysNumber = Convert.ToInt32(values[5]);
            GuestsNumber = Convert.ToInt32(values[6]);
            GradeStatus = values[7];
            GuestUserName = values[8];
        }
    }
}
