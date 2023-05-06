using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.ServiceInterfaces;
using System.Collections.Generic;

namespace InitialProject.Application.Services
{
    public class AccommodationReviewImageService: IAccommodationReviewImageService
    {
        private readonly IAccommodationReviewImageRepository _accommodationReviewImageRepository;

        public AccommodationReviewImageService(IAccommodationReviewImageRepository accommodationReviewImageRepository)
        {
            _accommodationReviewImageRepository = accommodationReviewImageRepository;
        }

        public void SaveImages(List<AccommodationReviewImage> images)
        {
            _accommodationReviewImageRepository.SaveImages(images);
        }
    }
}
