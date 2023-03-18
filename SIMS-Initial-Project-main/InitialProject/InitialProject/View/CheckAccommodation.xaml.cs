using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace InitialProject.Forms
{
    /// <summary>
    /// Interaction logic for CheckAccommodation.xaml
    /// </summary>
    public partial class CheckAccommodation : Window
    {
        private readonly AccommodationRepository _repository;
        private readonly ReservationRepository _reservationRepository;
        public static Accommodation SelectedAccommodation { get; set; }
        public User LoggedInUser { get; set; }

        private string _dateFrom = "May 3 2023";
        private string _dateTo = "May 3 2023";
        private int _daysNumber = 0;
        private int _guestsNumber = 0;
        public int GuestsNumber
        {
            get => _guestsNumber;
            set
            {
                if (value != _guestsNumber)
                {
                    _guestsNumber = value;
                    OnPropertyChanged();
                }
            }
        }
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

        public string DateFrom
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

        public string DateTo
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CheckAccommodation(int accommodationId, User LoggedUser)
        {
            InitializeComponent();
            DataContext = this;
            _repository = new AccommodationRepository();
            _reservationRepository = new ReservationRepository();
            SelectedAccommodation = _repository.GetAccommodationById(accommodationId);
            LoggedInUser = LoggedUser;
        }

        public void CheckReservation(object sender, RoutedEventArgs e)
        {
            
            string message = _reservationRepository.GetReservationsForGuest(LoggedInUser.Id,SelectedAccommodation.Id, DateFrom, DateTo, DaysNumber, GuestsNumber);
        }
    }
}
