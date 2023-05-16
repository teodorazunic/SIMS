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
using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.Repository;

namespace InitialProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for ActiveTour.xaml
    /// </summary>
    public partial class ActiveTour : Window
    {

        public User LoggedInUser { get; set; }
        private KeyPoint _selectedKeyPoint;
        public TourReservations Reservation { get; set; }
        public List<KeyPoint> AllKeyPoints { get; set; }
        //List<KeyPoint> KeyPointsShowing = new List<KeyPoint>();
        //public static ObservableCollection<KeyPoint> KeyPoints { get; set; }

        private int _id = 0;
        private string _name = "";
        private int _tourId = 0;
        private string _status = "";

        public int Id
        {
            get => _id;
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged();
                }

            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }

            }
        }

        public int TourId
        {
            get => _tourId;
            set
            {
                if (value != _tourId)
                {
                    _tourId = value;
                    OnPropertyChanged();
                }

            }
        }

        public string Status
        {
            get => _status;
            set
            {
                if (value != _status)
                {
                    _status = value;
                    OnPropertyChanged();
                }

            }
        }

        public KeyPoint SelectedKeyPoint
        {
            get => _selectedKeyPoint;
            set
            {
                if (value != _selectedKeyPoint)
                {
                    _selectedKeyPoint = value;
                    OnPropertyChanged();
                }
            }

        }

        private readonly TourRepository _repository = new TourRepository();
        private readonly KeyPointRepository _keyPointRepository = new KeyPointRepository();
        private readonly TourReservationRepositery _tourReservationRepositery = new TourReservationRepositery();
        private readonly GuestOnTourRepository _guestOnTourRepository = new GuestOnTourRepository();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ActiveTour(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new TourRepository();
            _keyPointRepository = new KeyPointRepository();
            _tourReservationRepositery = new TourReservationRepositery();
            _guestOnTourRepository = new GuestOnTourRepository();

            List<TourReservations> guestsTours = new List<TourReservations>(_tourReservationRepositery.GetAllReservationsByGuestId(LoggedInUser.Id));
            List<Tour> tours = new List<Tour>();
            foreach (TourReservations tourReservation in guestsTours)
            {
                Tour tour = _repository.GetTourById(tourReservation.Tour.Id);

                tours.Add(tour);
            }

            Tour startedTour = new Tour();
            
            startedTour = _repository.GetStartedTour(tours);
            TourReservations finalTour = new TourReservations();
            finalTour = _tourReservationRepositery.GetTourReservationByTourId(startedTour.Id);
            Reservation = finalTour;
            List<KeyPoint> KeyPointsShowing = new List<KeyPoint>();
            KeyPointsShowing = _keyPointRepository.GetKeyPointbyTourId(startedTour.Id);
            KeyPoints.ItemsSource = KeyPointsShowing;
            AllKeyPoints = KeyPointsShowing;
            if(_guestOnTourRepository.GetGuestStatusByTourId(startedTour.Id) == "Here")
            {
                AddedTextBlock.Text = "The guide has added you to this tour.";
                JoinButton.IsEnabled = false;
            }

        }

        private void JoinTour(object sender, RoutedEventArgs e)
        {
            GuestOnTour addGuest = new GuestOnTour();
            addGuest.GuestId = LoggedInUser.Id;
            addGuest.GuestName = LoggedInUser.Username;
            addGuest.NumberOfGuests = Reservation.NumberOfGuests;
            addGuest.StartingKeyPoint = _keyPointRepository.GetLastActiveKeyPoint(AllKeyPoints);
            addGuest.Status = "Not here";
            _guestOnTourRepository.Save(addGuest);
            JoinButton.IsEnabled = false;

        }
    }
}
