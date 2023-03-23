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
    /// Interaction logic for SaveReservation.xaml
    /// </summary>
    public partial class SaveReservation : Window
    {

        private readonly AccommodationRepository _repository;
        private readonly ReservationRepository _reservationRepository;
        public static Accommodation SelectedAccommodation { get; set; }
        public User LoggedInUser { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int DaysNumber { get; set; }

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

        public SaveReservation(int accommodationId, User LoggedUser, DateTime dateFrom, DateTime dateTo, int daysNumber)
        {
            InitializeComponent();
            DataContext = this;
            _repository = new AccommodationRepository();
            _reservationRepository = new ReservationRepository();
            SelectedAccommodation = _repository.GetAccommodationById(accommodationId);
            DateFrom = dateFrom;
            DateTo = dateTo;
            DaysNumber = daysNumber;
            LoggedInUser = LoggedUser;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Check(object sender, RoutedEventArgs e)
        {
            Reservation newReservation = new Reservation();
            newReservation.GuestId = LoggedInUser.Id;
            newReservation.DateFrom = DateFrom;
            newReservation.DateTo = DateTo;
            newReservation.AccommodationId = SelectedAccommodation.Id;
            newReservation.DaysNumber = DaysNumber;

           string message = _reservationRepository.checkGuests(newReservation, GuestsNumber);

            if(message == "Rezervacija je uspesno sacuvana!")
            {
                MessageBox.Show(message);
                Close();
            } else  {
                MessageBox.Show(message);
            }
        }
    }
}
