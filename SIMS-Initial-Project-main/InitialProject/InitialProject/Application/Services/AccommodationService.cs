using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.ServiceInterfaces;
using System.Collections.Generic;

namespace InitialProject.Application.Services
{
    public class AccommodationService: IAccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;

        public AccommodationService(IAccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
        }

        public List<Accommodation> GetAllAccomodations()
        {
            return _accommodationRepository.GetAllAccomodations();
        }

        public Accommodation GetAccommodationById(int id)
        {
            return _accommodationRepository.GetAccommodationById(id);
        }

        public List<Accommodation> SearchAccommodation(Accommodation accommodation, string type)
        {
            return _accommodationRepository.SearchAccommodation(accommodation, type);
        }

        public Accommodation Save(Accommodation accommodation)
        {
            return _accommodationRepository.Save(accommodation);
        }

        public void Delete(Accommodation accommodation)
        {
            _accommodationRepository.Delete(accommodation);
        }

        public Accommodation Update(Accommodation accommodation)
        {
            return _accommodationRepository.Update(accommodation);
        }
    }
}
