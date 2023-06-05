using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IAccommodationRepository
    {

        public List<Accommodation> GetAllAccomodations();

        public Accommodation GetAccommodationById(int id);

        public List<Accommodation> SearchAccommodation(Accommodation accommodation, string type);

        public Accommodation Save(Accommodation accommodation);

        public void Delete(Accommodation accommodation);

        public Accommodation Update(Accommodation accommodation);

        public List<Accommodation> FindAllByLocation(Location location);

        public List<Accommodation> AnywhereAnytime(int numberOfGuests, int numberOfDays);

        public List<Location> GetAllLocations();

    }
}
