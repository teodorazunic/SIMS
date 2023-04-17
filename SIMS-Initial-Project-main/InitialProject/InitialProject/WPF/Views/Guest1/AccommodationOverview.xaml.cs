using InitialProject.Domain;
using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repository;
using System.Collections.Generic;
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
        private readonly IAccommodationRepository _repository;

        public List<string> AccommodationTypes = new List<string>() { "", "apartment", "house", "cottage" };
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public User LoggedInUser { get; set; }

        private string _name = "";
        private string _city = "";
        private string _country = "";
        private int _typeIndex = -1;
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

        public int AccommodationTypeIndex
        {
            get => _typeIndex;
            set
            {
                if (value != _typeIndex)
                {
                    _typeIndex = value;
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
            _repository = Injector.CreateInstance<IAccommodationRepository>();
            Accommodations = new ObservableCollection<Accommodation>(_repository.GetAllAccomodations());
        }

        public void SearchAccommodation(object sender, RoutedEventArgs e)
        {
            string AccommodationType = "";
            if (AccommodationTypeIndex > 0)
            {
                AccommodationType = AccommodationTypes[AccommodationTypeIndex];
            }

            Accommodation accommodationToSearch = new Accommodation();
            accommodationToSearch.Name = AccommodationName;
            accommodationToSearch.GuestsNumber = AccommodationGuestsNumber;
            accommodationToSearch.ReservationDays = AccommodationReservationNumber;

            Location location = new Location();
            location.City = AccommodationCity;
            location.Country = AccommodationCountry;
            accommodationToSearch.Location = location;

            ObservableCollection<Accommodation> accommodations = new ObservableCollection<Accommodation>(_repository.SearchAccommodation(accommodationToSearch, AccommodationType));
            Accommodations.Clear();
            foreach (var accommodation in accommodations) Accommodations.Add(accommodation);
        }

        public void OnRowClick(object sender, MouseButtonEventArgs e)
        {
            CheckAccommodation checkAccommodation = new CheckAccommodation(SelectedAccommodation.Id, LoggedInUser);
            checkAccommodation.Show();
            Close();
        }

        public void ShowReservations(object sender, RoutedEventArgs e)
        {
            MyReservations myReservations = new MyReservations(LoggedInUser);
            myReservations.Show();
            Close();
        }

        public void ShowNotifications(object sender, RoutedEventArgs e)
        {
            MyNotifications myNotifications = new MyNotifications(LoggedInUser);
            myNotifications.Show();
            Close();
        }

        public void ShowRequests(object sender, RoutedEventArgs e)
        {
            ReservationRequests myReservationRequests = new ReservationRequests(LoggedInUser);
            myReservationRequests.Show();
            Close();
        }

        public void Logout(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }
    }
}
