using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;

namespace InitialProject.WPF.Views.Guide
{
    /// <summary>
    /// Interaction logic for TourStatistics.xaml
    /// </summary>
    public partial class TourStatistics : Window
    {
        private readonly TourReservationRepositery _tourReservationRepositery;

        private const string FilePath = "../../../Resources/Data/tourreservations.csv";

        public List<TourReservations> reservations = new List<TourReservations>();

        TourReservationRepositery reservationRepositery = new TourReservationRepositery();

        public User LoggedInUser { get; set; }

        
        public TourStatistics(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _tourReservationRepositery = new TourReservationRepositery();
            List<int> vouchers = new List<int>();
            vouchers = reservationRepositery.GetVoucherStatistics(reservations);
            txtUsed.Text = vouchers[0].ToString();
            txtUnused.Text = vouchers[1].ToString();

           

        }
     


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ComboBox.IsEnabled = false;
            TourReservations tourReservation = new TourReservations();
            List<TourReservations> tourReservations = new List<TourReservations>();
            tourReservation = _tourReservationRepositery.FindMostAttendantTour(FilePath);
            tourReservations.Add(tourReservation);
            MostVisitedTour.ItemsSource = tourReservations;
        }

        private void Fill(object sender, RoutedEventArgs e)
        {
            ComboBox.Items.Add("2023");
            ComboBox.Items.Add("2022");
            ComboBox.Items.Add("2021");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string year = ComboBox.SelectedItem.ToString();
            TourReservations tourReservation = new TourReservations();
            List<TourReservations> tourReservations = new List<TourReservations>();
            tourReservation = _tourReservationRepositery.FindMostAttendantTourByYear(FilePath, year);
            tourReservations.Add(tourReservation);
            MostVisitedTour.ItemsSource = tourReservations;

        }

        
    }
}
