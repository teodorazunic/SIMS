using InitialProject.Domain.Model;
using System.Collections.Generic;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IAccommodationReviewRepository
    {
        public string SaveReview(AccommodationReview accommodationReview, List<AccommodationReviewImage> images);
    }
}
