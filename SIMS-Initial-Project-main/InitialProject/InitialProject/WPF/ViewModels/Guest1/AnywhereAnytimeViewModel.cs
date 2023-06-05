using InitialProject.Domain;
using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.ServiceInterfaces;
using System;
using System.Collections.Generic;

namespace InitialProject.WPF.ViewModels.Guest1
{
    public class AnywhereAnytimeViewModel : MainViewModel
    {

        private IAccommodationService _accommodationService;
        public Accommodation Selected { get; set; }

        public List<AccommodationDateRange> FilteredAccommodations { get; set; }

        public User LoggedInUser { get; set; }

        private int _daysNumber = 1;
        private DateTime? _dateFrom = null;
        private DateTime? _dateTo = null;
        private int _guestsNumber = 1;

        public int DaysNumber
        {
            get => _daysNumber;
            set
            {
                if (value != _daysNumber)
                {
                    _daysNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public int GuestsNumber
        {
            get => Convert.ToInt32(_guestsNumber);
            set
            {
                if (value != _guestsNumber)
                {
                    _guestsNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? ReservationDateFrom
        {
            get => _dateFrom;
            set
            {
                if (value != _dateFrom)
                {
                    _dateFrom = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? ReservationDateTo
        {
            get => _dateTo;
            set
            {
                if (value != _dateTo)
                {
                    _dateTo = value;
                    OnPropertyChanged();
                }
            }
        }
        public AnywhereAnytimeViewModel(User user)
        {
            LoggedInUser = user;
            _accommodationService = Injector.CreateInstance<IAccommodationService>();
        }

        public List<Accommodation> OnLoad()
        {
            return new List<Accommodation>();
        }

        public List<Accommodation> OnSearch()
        {
            List<Accommodation> accommodations = new List<Accommodation>();
            FilteredAccommodations = _accommodationService.AnywhereAnytime(GuestsNumber, ReservationDateFrom, ReservationDateTo, DaysNumber);
            if (FilteredAccommodations.Count > 0)
            {
                foreach (AccommodationDateRange range in FilteredAccommodations)
                {
                    accommodations.Add(range.Accommodation);
                }
            }
            return accommodations;
        }
    }
}
