﻿using InitialProject.Model;
using InitialProject.Repository;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace InitialProject
{
    /// <summary>
    /// Interaction logic for TourOverview.xaml
    /// </summary>
    public partial class TourOverview : Window
    {
        public static ObservableCollection<Tour> Tours { get; set; }

        public User LoggedInUser { get; set; }

        private string _name = "";
        private string _city = "";
        private string _country = "";
        private int _duration = 0;
        private string _language = "";
        private int _numberOfPeople = 0;

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

        private readonly TourRepository _repository;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TourOverview(User user)
        {

            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new TourRepository();
            Tours = new ObservableCollection<Tour>(_repository.GetAllTours());
        }
        
        private void SearchTour(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Tour> tours = new ObservableCollection<Tour>(_repository.SearchTour(TourName, TourCity, TourCountry, TourDuration, TourLanguage, TourNumberOfPeople));
            Tours.Clear();
            foreach (var tour in tours) Tours.Add(tour);
        }
        
         private void Reservation_Button_Click(object sender, RoutedEventArgs e)
        {
            TourReservation tourReservation = new TourReservation();
            tourReservation.Show();

        }

        private void City_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
