using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.ServiceInterfaces;
using InitialProject.Repository;
using System;
using System.Collections.Generic;

namespace InitialProject.Application.Services
{
    public class AccommodationService : IAccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IReservationRepository _reservationRepository;

        public AccommodationService(IAccommodationRepository accommodationRepository, IReservationRepository reservationRepository)
        {
            _accommodationRepository = accommodationRepository;
            _reservationRepository = reservationRepository;
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

        public List<AccommodationDateRange> AnywhereAnytime(int numberOfGuests, DateTime? dateFrom, DateTime? dateTo, int numberOfDays)
        {
            List<AccommodationDateRange> filteredList = new List<AccommodationDateRange>();

            List<Accommodation> filteredAccommodations = _accommodationRepository.AnywhereAnytime(numberOfGuests, numberOfDays);

            DateTime dateTimeFrom;
            DateTime dateTimeTo;

            if (dateFrom == null && dateTo == null)
            {
                dateTimeFrom = DateTime.Now;
                dateTimeTo = DateTime.Now.AddDays(numberOfDays);
            } else
            {
                dateTimeFrom = (DateTime) dateFrom;
                dateTimeTo = (DateTime) dateTo; 
            }

            foreach (Accommodation acc in filteredAccommodations)
            {
                List<ReservationDate> availableDates = _reservationRepository.GetReservationsForGuest(0, acc.Id, dateTimeFrom, dateTimeTo, numberOfDays);
                filteredList.Add(new AccommodationDateRange(acc, availableDates));
            }

            return filteredList;
        }
    }
}