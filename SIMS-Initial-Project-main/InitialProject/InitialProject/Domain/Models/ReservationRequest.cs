using InitialProject.Serializer;
using System;

namespace InitialProject.Domain.Models
{
    public enum RequestStatus
    {
        pending,
        approved,
        rejected
    }
    public class ReservationRequest : ISerializable
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }

        public int GuestId { get; set; }

        public string GuestComment { get; set; }

        public string OwnerComment { get; set; }

        public RequestStatus Status { get; set; }

        public ReservationRequest(int guestId, int reservationId, string guestComment, string ownerComment, RequestStatus status)
        {
            GuestId = guestId;
            ReservationId = reservationId;
            GuestComment = guestComment;
            OwnerComment = ownerComment;
            Status = status;
        }

        public ReservationRequest() { }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestId.ToString(), ReservationId.ToString(), GuestComment, OwnerComment, Status.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            ReservationId = Convert.ToInt32(values[2]);
            GuestComment = values[3];
            OwnerComment = values[4];
            Status = (RequestStatus)Enum.Parse(typeof(RequestStatus), values[5], false);
        }
    }
}
