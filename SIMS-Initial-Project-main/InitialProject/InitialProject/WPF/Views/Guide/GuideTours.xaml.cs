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
    /// Interaction logic for GuideTours.xaml
    /// </summary>
    public partial class GuideTours : Window
    {
        private const string FilePath = "../../../Resources/Data/tour.csv";

        public User LoggedInUser { get; set; }

        private Tour _selectedTour;
        private KeyPoint _selectedKeyPoint;
        private GuestOnTour _selectedGuest;

        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {
                if (value != _selectedTour)
                {
                    _selectedTour = value;
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
        private readonly GuestOnTourRepository _guestOnTourRepository = new GuestOnTourRepository();



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GuideTours(User user)
        {

            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new TourRepository();
            _keyPointRepository = new KeyPointRepository();
            Tours.ItemsSource = _repository.GetTodaysTours(FilePath);
        }


        public void OnRowClick(object sender, RoutedEventArgs e)
        {
            SelectedTour = _repository.GetTourById(SelectedTour.Id);
            List<KeyPoint> keyPoints = new List<KeyPoint>();
            keyPoints = _keyPointRepository.GetKeyPointbyTourId(SelectedTour.Id);
            KeyPoints.ItemsSource = keyPoints;

        }

        public void OnRowClick1(object sender, RoutedEventArgs e)
        {
            SelectedKeyPoint = _keyPointRepository.GetKeyPointById(SelectedKeyPoint.Id);
            List<GuestOnTour> guestsOnTour = new List<GuestOnTour>();
            guestsOnTour = _guestOnTourRepository.GetGuestByKeyPointId(SelectedKeyPoint.Id);
            Guests.ItemsSource = guestsOnTour;

        }

        private void Activate(object sender, RoutedEventArgs e)
        {
            if (_selectedKeyPoint != null)
            {
                string message = _keyPointRepository.Activate(_selectedKeyPoint);

                MessageBox.Show(message);
            }
            KeyPoints.Items.Refresh();

            int numOfActiveKeyPoints = 0;
            foreach (KeyPoint point in KeyPoints.Items)
            {
                if (point.Status == "Active")
                    numOfActiveKeyPoints++;
            }


            if (numOfActiveKeyPoints == KeyPoints.Items.Count)
            {
                _selectedTour.Status = "Finished";
                _selectedTour = _repository.Update(_selectedTour);
                _selectedTour = null;
                foreach (KeyPoint kp in KeyPoints.Items)
                {
                    if (kp.Status == "Active")
                    {
                        kp.Status = "Inactive";
                        _keyPointRepository.Update(kp);
                    }
                }
                MessageBox.Show("Tour finished.");
                KeyPoints.Items.Refresh();
            }

        }

        private void StartTour(object sender, RoutedEventArgs e)
        {
            bool isTourStarted = _repository.IsStarted();
            if (isTourStarted == true) {
                MessageBox.Show("It is not possible to start the tour.");
            }
            else
            {
                _selectedTour.Status = "Started";
                _selectedTour = _repository.Update(_selectedTour);
                MessageBox.Show("Tour Started.");
            }


            Tours.Items.Refresh();
        }

        private void EndTour(object sender, RoutedEventArgs e)
        {
            _selectedTour.Status = "Ended";
            _selectedTour = _repository.Update(_selectedTour);
            MessageBox.Show("Tour ended.");

            foreach (KeyPoint kp in KeyPoints.Items)
            {
                if (kp.Status == "Active")
                {
                    kp.Status = "Inactive";
                    _keyPointRepository.Update(kp);
                }
            }
            KeyPoints.Items.Refresh();

        }
    }
}
