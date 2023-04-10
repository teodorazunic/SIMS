using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class AccommodationReviewImageRepository: IAccommodationReviewImageRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationimagereview.csv";

        private readonly Serializer<AccommodationReviewImage> _serializer;

        private List<AccommodationReviewImage> _accommodationReviewImages;

        public AccommodationReviewImageRepository()
        {
            _serializer = new Serializer<AccommodationReviewImage>();
            _accommodationReviewImages = _serializer.FromCSV(FilePath);
        }

        private int GetLastId()
        {
            if (_accommodationReviewImages != null && _accommodationReviewImages.Count > 0)
            {
                return _accommodationReviewImages.Max(reviewImage => reviewImage.Id);
            }
            return 0;
        }

        public void SaveImages(List<AccommodationReviewImage> images)
        {
            int lastId = GetLastId() + 1;
            foreach (var image in images)
            {
                image.Id = lastId;
                lastId++;
            }
            _accommodationReviewImages.AddRange(images);
            _serializer.ToCSV(FilePath, _accommodationReviewImages);
        }
    }
}
