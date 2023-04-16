using InitialProject.Domain.Model;
using System.Collections.Generic;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IAccommodationReviewImageRepository
    {
        public void SaveImages(List<AccommodationReviewImage> images);
    }
}
