using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;

namespace InitialProject.Forms
{
    /// <summary>
    /// Interaction logic for MyReservations.xaml
    /// </summary>
    public partial class MyReservations : Window
    {

        public User LoggedInUser { get; set; }

        private readonly ReservationRepository _repository;

        private ReservationAccommodation _selectedReservationAccommodation;

        public ReservationAccommodation SelectedReservationAccommodation
        {
            get => _selectedReservationAccommodation;
            set
            {
                if (value != _selectedReservationAccommodation)
                {
                    _selectedReservationAccommodation = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MyReservations(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new ReservationRepository();
            Reservations.ItemsSource = _repository.GetAllByGuestId(user.Id);
        }

        public void Delete(object sender, RoutedEventArgs e)
        {

            if (SelectedReservationAccommodation != null)
            {
                Reservation reservation = SelectedReservationAccommodation.SelectedReservation;
                string message = _repository.DeleteReservation(reservation);

                MessageBox.Show(message);

                Reservations.ItemsSource = _repository.GetAllByGuestId(LoggedInUser.Id);
                Reservations.Items.Refresh();

            }
        }
        public void SendRequest(object sender, RoutedEventArgs e)
        {

            if (SelectedReservationAccommodation != null)
            {
                ReservationAccommodation reservationAccommodation = SelectedReservationAccommodation;
                
                ReservationRequestForm form = new ReservationRequestForm(LoggedInUser, reservationAccommodation);
                form.Show();
                Close();
            }
        }


    }
}
