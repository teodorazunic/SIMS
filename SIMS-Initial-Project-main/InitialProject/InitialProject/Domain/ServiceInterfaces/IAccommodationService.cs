using InitialProject.Domain.Model;
using System.Collections.Generic;

namespace InitialProject.Domain.ServiceInterfaces
{
    public interface IAccommodationService
    {
        public List<Accommodation> GetAllAccomodations();

        public Accommodation GetAccommodationById(int id);

        public List<Accommodation> SearchAccommodation(Accommodation accommodation, string type);

        public Accommodation Save(Accommodation accommodation);

        public void Delete(Accommodation accommodation);

        public Accommodation Update(Accommodation accommodation);
    }
}
