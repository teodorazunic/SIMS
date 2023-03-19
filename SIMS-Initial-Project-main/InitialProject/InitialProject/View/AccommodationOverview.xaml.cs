using InitialProject.Model;
using InitialProject.Repository;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.Forms
{
    /// <summary>
    /// Interaction logic for AccommodationOverview.xaml
    /// </summary>
    public partial class AccommodationOverview : Window
    {
        private readonly AccommodationRepository _repository;
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public User LoggedInUser { get; set; }

        private string _name = "";
        private string _city = "";
        private string _country = "";
        private string _type = "";
        private int _guestsNumber = 0;
        private int _reservationNumber = 0;
        private Accommodation _selectedAccommodation;
        public Accommodation SelectedAccommodation
        {
            get => _selectedAccommodation;
            set
            {
                if (value != _selectedAccommodation)
                {
                    _selectedAccommodation = value;
                    OnPropertyChanged();
                }
            }
        }
        public string AccommodationName
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

        public string AccommodationCity
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

        public string AccommodationCountry
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

        public string AccommodationType
        {
            get => _type;
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }

        public int AccommodationGuestsNumber
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

        public int AccommodationReservationNumber
        {
            get => _reservationNumber;
            set
            {
                if (value != _reservationNumber)
                {
                    _reservationNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AccommodationOverview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new AccommodationRepository();
            Accommodations = new ObservableCollection<Accommodation>(_repository.GetAllAccomodations());
        }

        public void SearchAccommodation (object sender, RoutedEventArgs e) {
            ObservableCollection<Accommodation>  accommodations = new ObservableCollection<Accommodation>(_repository.SearchAccommodation(AccommodationName, AccommodationCity, AccommodationCountry, AccommodationType, AccommodationGuestsNumber, AccommodationReservationNumber));
            Accommodations.Clear();
            foreach (var accommodation in accommodations) Accommodations.Add(accommodation);
        }

        public void OnRowClick(object sender, MouseButtonEventArgs e)
        {
            CheckAccommodation checkAccommodation = new CheckAccommodation(SelectedAccommodation.Id, LoggedInUser);
            checkAccommodation.Show();
            Close();
        }
    }
}
