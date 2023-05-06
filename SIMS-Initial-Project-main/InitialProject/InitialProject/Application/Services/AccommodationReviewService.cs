using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.ServiceInterfaces;
using System.Collections.Generic;

namespace InitialProject.Application.Services
{
    public class AccommodationReviewService: IAccommodationReviewService
    {
        private readonly IAccommodationReviewRepository _accommodationReviewRepository;

        public AccommodationReviewService(IAccommodationReviewRepository accommodationReviewRepository)
        {
            _accommodationReviewRepository = accommodationReviewRepository;
        }

        public string SaveReview(AccommodationReview accommodationReview, List<AccommodationReviewImage> images, RenovationRecommendation renovationRecommendation)
        {
            return _accommodationReviewRepository.SaveReview(accommodationReview, images, renovationRecommendation);
        }

        public bool checkIfReviewed(int accommodationId, int guestId)
        {
            return _accommodationReviewRepository.checkIfReviewed(accommodationId, guestId);
        }

        public AccommodationReview GetReviewByReservationId(int reservationId)
        {
            return _accommodationReviewRepository.GetReviewByReservationId(reservationId);
        }
    }
}
