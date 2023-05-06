using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using System.Collections.Generic;

namespace InitialProject.Domain.ServiceInterfaces
{
    public interface IAccommodationReviewService
    {
        public string SaveReview(AccommodationReview accommodationReview, List<AccommodationReviewImage> images, RenovationRecommendation renovationRecommendation);
        public bool checkIfReviewed(int accommodationId, int guestId);
        public AccommodationReview GetReviewByReservationId(int reservationId);
    }
}
