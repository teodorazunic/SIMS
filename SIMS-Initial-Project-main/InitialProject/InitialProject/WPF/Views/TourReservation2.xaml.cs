using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourReservation2.xaml
    /// </summary>
    public partial class TourReservation2 : Window
    {
        public static ObservableCollection<Tour> Tours { get; set; }
        
        public static ObservableCollection<TourReservations> TourReservations{ get; set; }

        private readonly TourRepository _repository;
        
        private readonly TourReservationRepositery _reservationRepository;

        public static Tour SelectedTour { get; set; }
        public User LoggedInUser { get; set; }
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TourReservation2(int id, User LoggedUser)
        {
            DataContext = this;
            InitializeComponent();
            _repository = new TourRepository();
            _reservationRepository = new TourReservationRepositery();
            Tours = new ObservableCollection<Tour>(_repository.GetAllTours());
            SelectedTour = _repository.GetTourById(id);
            LoggedInUser = LoggedUser;
            cityTextBlock.Text = SelectedTour.Location.City;

        }

       private void CheckGuests(object sender, RoutedEventArgs e)
        {
            string message = _repository.CheckMaxGuests(SelectedTour.Id, GuestsNumber);

            if (SelectedTour.MaxGuests == 0)
            {
                Tour newTour = _repository.GetTourByCity(SelectedTour.Location.City, SelectedTour.Id);
                if (newTour != null && newTour.MaxGuests > 0)
                {
                    SelectedTour = newTour;
                    cityTextBlock.Text = SelectedTour.Location.City;
                    string suggestion = "This tour has no available slots left. You might want to check out " + SelectedTour.Name + "!";
                    MessageBox.Show(suggestion);
                }
                else
                {
                    string suggestion = "This tour has no available slots left and there are no other tours available in this location.";
                    MessageBox.Show(suggestion);
                }
            }
            else
            {
                TourReservations tourReservations = new TourReservations();
                tourReservations.TourId = SelectedTour.Id;
                tourReservations.GuestId = LoggedInUser.Id;
                tourReservations.NumberOfGuests = GuestsNumber;
                tourReservations.UsedVoucher = false;
                _reservationRepository.SaveReservation(tourReservations);
                _repository.UpdateMaxGuests(SelectedTour.Id, GuestsNumber);
                MessageBox.Show(message);
            }
            
        }
    }
}
