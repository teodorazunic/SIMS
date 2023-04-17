using InitialProject.Domain;
using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repository;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
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

        private readonly IAccommodationRepository _repository;
        private readonly IReservationRepository _reservationRepository;
        private readonly List<ReservationDate> _reservationDates;

        public static Accommodation SelectedAccommodation { get; set; }
        public User LoggedInUser { get; set; }
        public int DaysNumber { get; set; }

        private int _guestsNumber = 0;
        private int _reservationDateIndex;

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
        public int ReservationDateIndex
        {
            get => _reservationDateIndex;
            set
            {
                if (value != _reservationDateIndex)
                {
                    _reservationDateIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        public SaveReservation(List<ReservationDate> reservationDates,int accommodationId, User LoggedUser, int daysNumber)
        {
            InitializeComponent();
            DataContext = this;
            _repository = Injector.CreateInstance<IAccommodationRepository>();
            _reservationDates = reservationDates;
            _reservationRepository = Injector.CreateInstance<IReservationRepository>();
            SelectedAccommodation = _repository.GetAccommodationById(accommodationId);
            DaysNumber = daysNumber;
            LoggedInUser = LoggedUser;
            ReservationDates.ItemsSource = _reservationDates;
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
            newReservation.DateFrom = _reservationDates[ReservationDateIndex].DateFrom;
            newReservation.DateTo = _reservationDates[ReservationDateIndex].DateTo;
            newReservation.AccommodationId = SelectedAccommodation.Id;
            newReservation.DaysNumber = DaysNumber;

           string message = _reservationRepository.CheckGuests(newReservation, GuestsNumber);

            if(message == "Rezervacija je uspesno sacuvana!")
            {
                MessageBox.Show(message);
                Close();
            } else  {
                MessageBox.Show(message);
            }
        }
        public void Logout(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }
    }
}
