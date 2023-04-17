using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
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

namespace InitialProject.WPF.Views.Guide
{
    /// <summary>
    /// Interaction logic for Reviews.xaml
    /// </summary>
    public partial class Reviews : Window
    {
        private const string FilePath = "../../../Resources/Data/tour.csv";

        public User LoggedInUser { get; set; }

        private Tour _selectedTour;

        private KeyPoint _selectedKeyPoint;

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


        public Reviews(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new TourRepository();
            _keyPointRepository = new KeyPointRepository();
            Tours.ItemsSource = _repository.GetEndedTours(FilePath);
        }

        private readonly TourRepository _repository;
        private readonly KeyPointRepository _keyPointRepository = new KeyPointRepository();
        private readonly GuestOnTourRepository _guestOnTourRepository = new GuestOnTourRepository();


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnRowClick(object sender, RoutedEventArgs e)
        {
            SelectedTour = _repository.GetTourById(SelectedTour.Id);
            List<GuestOnTour> guestsOnTour = new List<GuestOnTour>();
            guestsOnTour = _guestOnTourRepository.GetGuestByTourId(SelectedTour.Id);
            Guests.ItemsSource = guestsOnTour;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
