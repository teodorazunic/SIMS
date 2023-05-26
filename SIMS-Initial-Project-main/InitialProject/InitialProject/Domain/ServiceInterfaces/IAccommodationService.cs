using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using System;
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

        public List<AccommodationDateRange> AnywhereAnytime(int numberOfGuests, DateTime? dateFrom, DateTime? dateTo, int numberOfDays);

    }
}
