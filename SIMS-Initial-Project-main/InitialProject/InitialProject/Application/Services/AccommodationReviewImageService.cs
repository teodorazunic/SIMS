using InitialProject.Domain.Model;
using InitialProject.Domain.ServiceInterfaces;
using InitialProject.Repository;
using System.Collections.Generic;

namespace InitialProject.Application.Services
{
    public class AccommodationReviewImageService: IAccommodationReviewImageService
    {
        private readonly AccommodationReviewImageRepository _accommodationReviewImageRepository;

        public AccommodationReviewImageService(AccommodationReviewImageRepository accommodationReviewImageRepository)
        {
            _accommodationReviewImageRepository = accommodationReviewImageRepository;
        }

        public void SaveImages(List<AccommodationReviewImage> images)
        {
            _accommodationReviewImageRepository.SaveImages(images);
        }
    }
}
