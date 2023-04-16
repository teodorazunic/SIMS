using InitialProject.Serializer;

namespace InitialProject.Domain.Model
{

    public class AccommodationReviewImage : ISerializable
    {
        public int Id { get; set; }
        public int AccommodationReviewId { get; set; }
        public string ImageUrl { get; set; }

        public AccommodationReviewImage(int accommodationReviewId, string imageUrl)
        {
            AccommodationReviewId = accommodationReviewId;
            ImageUrl = imageUrl;
        }

        public AccommodationReviewImage() { }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), AccommodationReviewId.ToString(), ImageUrl };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationReviewId = int.Parse(values[1]);
            ImageUrl = values[2];
        }

    }
}
