using InitialProject.Domain.Models;
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



        public static ObservableCollection<Tour> Tours { get; set; }

        public User LoggedInUser { get; set; }

        private string _name = "";
        private string _city = "";
        private string _country = "";
        private int _duration = 0;
        private string _language = "";
        private int _numberOfPeople = 0;
        private Tour _selectedTour;

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



        public string TourName
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

        public string TourCity
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TourCountry
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        public int TourDuration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TourLanguage
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged();
                }
            }
        }

        public int TourNumberOfPeople
        {
            get => _numberOfPeople;
            set
            {
                if (value != _numberOfPeople)
                {
                    _numberOfPeople = value;
                    OnPropertyChanged();
                }
            }
        }

        
        private readonly TourRepository _repository = new TourRepository();
        private readonly KeyPointRepository _keyPointRepository = new KeyPointRepository();

        

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
            const string FilePath = "../../../Resources/Data/tour.csv";
            _keyPointRepository = new KeyPointRepository();
            //SelectedTour = _repository.GetTourById
            Tours = new ObservableCollection<Tour>(_repository.GetTodaysTours(FilePath));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Tour> tours = new List<Tour>();
            const string FilePath = "../../../Resources/Data/tour.csv";
            tours = _repository.GetTodaysTours(FilePath);
            GuideTours1.ItemsSource = tours;
        }

        public void OnRowClick(object sender, RoutedEventArgs e)
        {
            SelectedTour = _repository.GetTourById(SelectedTour.Id);
            List<KeyPoint> keyPoints = new List<KeyPoint>();
            keyPoints = _keyPointRepository.GetKeyPointbyTourId(SelectedTour.Id);
            KeyPoints.ItemsSource = keyPoints;


        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
