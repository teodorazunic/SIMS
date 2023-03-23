using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.VisualBasic;
using System;
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

        private DateTime _dateFrom = DateTime.Now;
        private DateTime _dateTo = DateTime.Now;
        private int _daysNumber = 1;

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

        public DateTime ReservationDateFrom
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

        public DateTime ReservationDateTo
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

            if (ReservationDateFrom < DateTime.Now || ReservationDateTo < DateTime.Now)
            {
                MessageBox.Show("Datum ne moze biti u proslosti");
            }
            else

            if (ReservationDateFrom > ReservationDateTo)
            {
                MessageBox.Show("Datum odlaska mora biti pre datuma polaska");
            }
            else
            {

                string message = _reservationRepository.GetReservationsForGuest(LoggedInUser.Id, SelectedAccommodation.Id, ReservationDateFrom, ReservationDateTo, DaysNumber);
                if (message == "Datumi su slobodni")
                {
                    SaveReservation saveReservation = new SaveReservation(SelectedAccommodation.Id, LoggedInUser, ReservationDateFrom, ReservationDateTo, DaysNumber);
                    saveReservation.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show(message);
                }

            }
        }
    }
}
