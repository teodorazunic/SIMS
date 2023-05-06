using InitialProject.Serializer;

namespace InitialProject.Domain.Models
{
    public class RenovationRecommendation : ISerializable
    {
        public int Id { get; set; }
        public int AccommodationReviewId { get; set; }
        public string WhatWasBadComment { get; set; }
        public string WhatToRenovateComment { get; set; }
        public int UrgencyRenovationLevel { get; set; }

        public RenovationRecommendation(int accommodationReviewId, string whatWasBadComment, string whatToRenovateComment, int urgencyRenovationLevel)
        {
            AccommodationReviewId = accommodationReviewId;
            WhatWasBadComment = whatWasBadComment;
            WhatToRenovateComment = whatToRenovateComment;
            UrgencyRenovationLevel = urgencyRenovationLevel;
        }

        public RenovationRecommendation() { }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), AccommodationReviewId.ToString(), WhatWasBadComment, WhatToRenovateComment, UrgencyRenovationLevel.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationReviewId = int.Parse(values[1]);
            WhatWasBadComment = values[2];
            WhatToRenovateComment = values[3];
            UrgencyRenovationLevel = int.Parse((values[4]));
        }
    }
}
