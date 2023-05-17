using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.Repository;
using InitialProject.WPF.Views.Guest2;
using InitialProject.WPF.Views;
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
using System.Runtime.InteropServices;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourReservation2.xaml
    /// </summary>
    public partial class TourReservation2 : Window
    {
        public static ObservableCollection<Tour> Tours { get; set; }

        public ObservableCollection<TourVoucher> Vouchers { get; set; }
        //public Voucher SelectedVoucher { get; set; }

        public static ObservableCollection<TourReservations> TourReservations{ get; set; }

        private readonly TourRepository _repository;
        
        private readonly TourReservationRepositery _reservationRepository;

        private readonly KeyPointsOnTourRepository _keyPointsOnTourRepository;

        private readonly VoucherRepository _voucherRepository;

        private void Vouchers_Loaded(object sender, RoutedEventArgs e)
        {
            List<TourVoucher> vouchers = _voucherRepository.GetVoucherByGuestId(LoggedInUser.Id);
            for (int i = 0; i < vouchers.Count; i++)
            {
                //ComboBox.Items.Add(vouchers[i].Title);
                ComboBoxItem item = new ComboBoxItem();
                item.Content = vouchers[i].Title;
                item.Tag = vouchers[i].VoucherId;
                ComboBox.Items.Add(item);
            }
        }

        public static Tour SelectedTour { get; set; }
        public User LoggedInUser { get; set; }
        private int _guestsNumber = 0;
        private MessageBoxResult result;
        //private Voucher selectedVoucher;

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
            _keyPointsOnTourRepository = new KeyPointsOnTourRepository();
            _voucherRepository = new VoucherRepository();
            //ComboBox.ItemsSource = voucherTitles;
            Tours = new ObservableCollection<Tour>(_repository.GetAllTours());
            SelectedTour = _repository.GetTourById(id);
            LoggedInUser = LoggedUser;
            KeyPointsOnTour keyPointsOnTour = new KeyPointsOnTour();
            keyPointsOnTour = _keyPointsOnTourRepository.GetKeyPointById(SelectedTour.Id);
            nameTextBlock.Text = SelectedTour.Name;
            locationTB.Text = SelectedTour.Location.City;
            location2TB.Text = SelectedTour.Location.Country;
            descriptionTB.Text = SelectedTour.Description;
            languageTB.Text = SelectedTour.Language;
            timeTB.Text = SelectedTour.Start.ToString();
            durationTB.Text = SelectedTour.Duration.ToString();
            keypointsTB.Text = keyPointsOnTour.KeyPoints;

        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }
        private void Home(object sender, RoutedEventArgs e)
        {
            TourOverview tourOverview = new TourOverview(LoggedInUser);
            tourOverview.Show();
            Close();
        }

        private void ShowVouchers(object sender, RoutedEventArgs e)
        {
            Vouchers vouchers = new Vouchers(LoggedInUser);
            vouchers.Show();
            Close();
        }

        private void Ratings(object sender, RoutedEventArgs e)
        {
            ShowPastTours ratings = new ShowPastTours(LoggedInUser);
            ratings.Show();
            Close();
        }

        private void Active(object sender, RoutedEventArgs e)
        {
            ActiveTour activeTour = new ActiveTour(LoggedInUser);
            activeTour.Show();
            Close();
        }

        private void Requests(object sender, RoutedEventArgs e)
        {
            TourRequestOverview tourRequestOverview = new TourRequestOverview(LoggedInUser);
            tourRequestOverview.Show();
            Close();
        }

        private void CheckGuests(object sender, RoutedEventArgs e)
        {
            string message = _repository.CheckMaxGuests(SelectedTour.Id, GuestsNumber);
            if(message != "You were added to the tour." && SelectedTour.MaxGuests != 0)
            {
                MessageBox.Show(message);
            }
            
            if (SelectedTour.MaxGuests == 0)
            {
                Tour newTour = _repository.GetTourByCity(SelectedTour.Location.City, SelectedTour.Id);
                if (newTour != null && newTour.MaxGuests > 0)
                {
                    
                    string suggestion = "This tour has no available slots remaining. Would you like to see another tour on the same location?";
                    string title = "Alert";
                    result = MessageBox.Show(suggestion, title, MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case(MessageBoxResult.Yes):
                            SelectedTour = newTour;
                            nameTextBlock.Text = SelectedTour.Name;
                            locationTB.Text = SelectedTour.Location.City;
                            location2TB.Text = SelectedTour.Location.Country;
                            descriptionTB.Text = SelectedTour.Description;
                            languageTB.Text = SelectedTour.Language;
                            timeTB.Text = SelectedTour.Start.ToString();
                            durationTB.Text = SelectedTour.Duration.ToString();
                            break;
                        case (MessageBoxResult.No):
                            TourOverview tourOverview = new TourOverview(LoggedInUser);
                            tourOverview.Show();
                            Close();
                            break;
                    }
                    //MessageBox.Show(suggestion);
                }
                else
                {
                    string suggestion = "This tour has no available slots left and there are no other tours available in this location.";
                    string title = "Alert";
                    MessageBox.Show(suggestion, title);
                }
            }
            else if (SelectedTour.MaxGuests > 0 && SelectedTour.MaxGuests >= GuestsNumber)
            {
                TourReservations tourReservations = new TourReservations();
              
                tourReservations.Tour.Id = SelectedTour.Id;
                tourReservations.GuestId.Id = LoggedInUser.Id;
                tourReservations.NumberOfGuests = GuestsNumber;
                 if (ComboBox.SelectedItem != null)
                {
                    tourReservations.UsedVoucher = true;
                    ComboBoxItem selectedItem = ComboBox.SelectedItem as ComboBoxItem;
                    int voucherId = (int)selectedItem.Tag;
                    _voucherRepository.DeleteUsedVoucher(voucherId);
                }
                else 
                {
                    tourReservations.UsedVoucher = false;
                }
                tourReservations.Status = "Not";
                _reservationRepository.SaveReservation(tourReservations);
                _repository.UpdateMaxGuests(SelectedTour.Id, GuestsNumber);
                //MessageBox.Show(message);
                finalTB.Text = "You successfully booked this tour!";
                BookButton.IsEnabled = false;
            }
            
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            TourOverview tourOverview = new TourOverview(LoggedInUser);
            tourOverview.Show();
            Close();
        }
    }
}
