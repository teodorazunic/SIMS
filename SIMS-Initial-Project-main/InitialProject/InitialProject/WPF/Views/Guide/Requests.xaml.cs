using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.View;
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
        TourNotificatinsRepository tourNotificatinsRepository = new TourNotificatinsRepository();

        public static ObservableCollection<TourRequest> TourRequests { get; set; }

        public User LoggedInUser { get; set; }

        private string _language = "";
        private string _city = "";
        private string _country = "";
        private int _maxGuests = 0;
        private DateTime _startDate = DateTime.Now;
        private DateTime _endDate = DateTime.Now;

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

        public DateTime RequestStart
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime RequestEnd
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
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
            tourNotificatinsRepository = new TourNotificatinsRepository();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //List<DateTime> availableDates = new List<DateTime>();
            RequestStart = Convert.ToDateTime(startDate.Text);
            RequestEnd = Convert.ToDateTime(endDate.Text);

            //availableDates = _tourRequestsRepository.AvailableDates(start, end);
            //Dates.ItemsSource = availableDates;
            TourRequest requestToSearch = new TourRequest();
            requestToSearch.StartDate = RequestStart;
            requestToSearch.EndDate = RequestEnd;

            ObservableCollection<TourRequest> tourRequests = new ObservableCollection<TourRequest>(_tourRequestsRepository.SearchByDate(requestToSearch));
            TourRequests.Clear();
            foreach (var request in tourRequests) TourRequests.Add(request);


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DateTime choosenDate =Convert.ToDateTime(tourDate.Text);

            TourForm tourForm = new TourForm(LoggedInUser);
            tourForm.Show();
            //this.Close();


        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DateTime choosenDate = Convert.ToDateTime(tourDate.Text);
            CreateByRequest create = new CreateByRequest(choosenDate, SelectedTourRequest.Location, SelectedTourRequest.Description, SelectedTourRequest.Language, SelectedTourRequest.MaxGuests) ;
            create.Show();
            this.Close();


            TourNotification sending = new TourNotification();
            string text = "We have created a tour by your request.";
            sending.Text = text;

            sending.GuestId.Id = SelectedTourRequest.GuestId;
            tourNotificatinsRepository.Save(sending);
        }
    }
}
