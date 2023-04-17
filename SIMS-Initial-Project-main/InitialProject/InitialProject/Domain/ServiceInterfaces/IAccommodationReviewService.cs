using InitialProject.Domain.Model;
using System.Collections.Generic;

namespace InitialProject.Domain.ServiceInterfaces
{
    public interface IAccommodationReviewService
    {
        public string SaveReview(AccommodationReview accommodationReview, List<AccommodationReviewImage> images);
        public bool checkIfReviewed(int accommodationId, int guestId);
    }
}
