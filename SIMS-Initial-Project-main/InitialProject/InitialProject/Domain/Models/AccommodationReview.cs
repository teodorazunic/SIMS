using InitialProject.Serializer;

namespace InitialProject.Domain.Model
{
    public class AccommodationReview : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int AccommodationId { get; set; }
        public int Cleanliness { get; set; }
        public int Staff { get; set; }
        public string Comment { get; set; }
        public int ReservationId { get; set; }

        public AccommodationReview(int guestId, int accommodationId, int cleanliness, int staff, string comment)
        {
            GuestId = guestId;
            AccommodationId = accommodationId;
            Cleanliness = cleanliness;
            Staff = staff;
            Comment = comment;
        }

        public AccommodationReview() { }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestId.ToString(), AccommodationId.ToString(), Cleanliness.ToString(), Staff.ToString(), Comment, ReservationId.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            GuestId = int.Parse(values[1]);
            AccommodationId = int.Parse(values[2]);
            Cleanliness = int.Parse(values[3]);
            Staff = int.Parse(values[4]);
            Comment = values[5];
            ReservationId = int.Parse(values[6]);
        }

    }
}

