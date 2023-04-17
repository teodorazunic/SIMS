using InitialProject.Domain.Model;
using System.Collections.Generic;

namespace InitialProject.Domain.ServiceInterfaces
{
    internal interface IAccommodationReviewImageService
    {
        public void SaveImages(List<AccommodationReviewImage> images);

    }
}
