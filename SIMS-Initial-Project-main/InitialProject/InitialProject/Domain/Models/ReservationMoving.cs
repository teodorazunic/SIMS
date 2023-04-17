using System;
using InitialProject.Serializer;

namespace InitialProject.Model
{
    public enum RequestStatus
    {
        pending,
        approved,
        rejected
    }
    public class ReservationMoving : ISerializable
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int AccommodationId { get; set; }
        public int GuestId { get; set; }
        public string GuestUsername { get; set; }
        public DateTime OldStartDate { get; set; }
        public DateTime OldEndDate { get; set; }
        public DateTime NewStartDate { get; set; }
        public DateTime NewEndDate { get; set; }
        public string GuestComment { get; set; }
        public string OwnerComment { get; set; }
        public RequestStatus Status { get; set; }

        public ReservationMoving() { }
        public ReservationMoving(int reservationId, int accommodationId, string guestUsername, DateTime oldStartDate, DateTime oldEndDate, DateTime newStartDate, DateTime newEndDate,
            int guestId, string guestComment, string ownerComment, RequestStatus status)
        {
            ReservationId = reservationId;
            AccommodationId = accommodationId;
            GuestUsername = guestUsername;
            OldStartDate = oldStartDate;
            OldEndDate = oldEndDate;
            NewStartDate = newStartDate;
            NewEndDate = newEndDate;
            GuestId = guestId;
            ReservationId = reservationId;
            GuestComment = guestComment;
            OwnerComment = ownerComment;
            Status = status;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), ReservationId.ToString(), AccommodationId.ToString(), GuestId.ToString(), GuestUsername, OldStartDate.ToString(), OldEndDate.ToString(), NewStartDate.ToString(), NewEndDate.ToString(), GuestComment, OwnerComment, Status.ToString() };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ReservationId = Convert.ToInt32(values[1]);
            AccommodationId = Convert.ToInt32(values[2]);
            GuestId = Convert.ToInt32(values[3]);
            GuestUsername = values[4];
            OldStartDate = Convert.ToDateTime(values[5]);
            OldEndDate = Convert.ToDateTime(values[6]);
            NewStartDate = Convert.ToDateTime(values[7]);
            NewEndDate = Convert.ToDateTime(values[8]);
            GuestComment = values[9];
            OwnerComment = values[10];
            Status = (RequestStatus)Enum.Parse(typeof(RequestStatus), values[11], false);
        }
    }
}

