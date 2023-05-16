using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Repositories;
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
using System.Xml.Linq;

namespace InitialProject.WPF.Views.Guide
{
    /// <summary>
    /// Interaction logic for Requests.xaml
    /// </summary>
    public partial class Requests : Window
    {
        private const string FilePath = "../../../Resources/Data/tourrequests.csv";

        private readonly TourRequestRepository _tourRequestsRepository;

        public static ObservableCollection<TourRequest> TourRequests { get; set; }

        public User LoggedInUser { get; set; }

        private string _language = "";
        private string _city = "";
        private string _country = "";
        private int _maxGuests = 0;

        private TourRequest _selectedTourRequest;

        public TourRequest SelectedTourRequest
        {
            get => _selectedTourRequest;
            set
            {
                if (value != _selectedTourRequest)
                {
                    _selectedTourRequest = value;
                    OnPropertyChanged();
                }
            }
        }
        public string RequestLanguage
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

        public string RequestCity
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

        public string RequestCountry
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

        public int RequestMaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Requests(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _tourRequestsRepository = new TourRequestRepository();
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestsRepository.GetAllTourRequests());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TourRequest requestToSearch = new TourRequest();
            requestToSearch.Language = RequestLanguage;
            requestToSearch.MaxGuests = RequestMaxGuests;

            Location location = new Location();
            location.City = RequestCity;
            location.Country = RequestCountry;
            requestToSearch.Location = location;

            ObservableCollection<TourRequest> tourRequests = new ObservableCollection<TourRequest>(_tourRequestsRepository.SearchRequests(requestToSearch));
            TourRequests.Clear();
            foreach (var request in tourRequests) TourRequests.Add(request);
        }
    }
}
