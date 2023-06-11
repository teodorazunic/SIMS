using DevExpress.Data.Browsing;
using InitialProject.Domain.Models;
using InitialProject.Repositories;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.Views.Guide
{
    /// <summary>
    /// Interaction logic for TourStatistics.xaml
    /// </summary>
    public partial class TourStatistics : Page
    {
        private readonly TourReservationRepositery _tourReservationRepositery;

        private const string FilePath = "../../../Resources/Data/tourreservations.csv";

        public List<TourReservations> reservations = new List<TourReservations>();

        TourReservationRepositery reservationRepositery = new TourReservationRepositery();

        public User LoggedInUser { get; set; }

        public TourReservations selectedTour;

        private SeriesCollection _data;

        private SeriesCollection data;

      

        private int AcceptedCount { get; set; }
        private int DeniedCount { get; set; }

        private int Age1 { get; set; }
        private int Age2 { get; set; }
        private int Age3 { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SeriesCollection DataChart
        {
            get => _data;
            set
            {
                if (_data != value)
                {
                    _data = value;
                    OnPropertyChanged();
                }

            }
        }

        public SeriesCollection DataChart1
        {
            get => data;
            set
            {
                if (data != value)
                {
                    data = value;
                    OnPropertyChanged();
                }

            }
        }


        public TourStatistics(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _tourReservationRepositery = new TourReservationRepositery();
            List<int> vouchers = new List<int>();
            vouchers = reservationRepositery.GetVoucherStatistics(reservations);
            //txtUsed.Text = vouchers[0].ToString();
            //txtUnused.Text = vouchers[1].ToString();

            
            AcceptedCount = vouchers[0];
            DeniedCount = vouchers[1];
            
            
            DataChart = new SeriesCollection
            {
                new PieSeries
            {
                Title = "Accepted",
                Values = new ChartValues<ObservableValue> {new ObservableValue(AcceptedCount)}
            },
                new PieSeries
            {
                Title = "Denied",
                Values = new ChartValues<ObservableValue> { new ObservableValue(DeniedCount)}
            }
            };



            Age1 = 5;
            Age2 = 10;
            Age3 = 4;

            DataChart1 = new SeriesCollection
            {
                new PieSeries
            {
                Title = "18",
                Values = new ChartValues<ObservableValue> {new ObservableValue(Age1)}
            },
                new PieSeries
            {
                Title = "18-55",
                Values = new ChartValues<ObservableValue> { new ObservableValue(Age2)}
            },
                new PieSeries
            {
                Title = "55",
                Values = new ChartValues<ObservableValue> { new ObservableValue(Age3)}
            }

            };
            





        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox comboBox = (ComboBox)sender;
            string selectedYear = comboBox.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedYear))
            {


                if (ComboBox.SelectedItem.ToString() == "All years")
                {

                    TourReservations tourReservation = new TourReservations();
                    List<TourReservations> tourReservations = new List<TourReservations>();
                    tourReservation = _tourReservationRepositery.FindMostAttendantTour(FilePath);
                    tourReservations.Add(tourReservation);
                    MostVisitedTour.ItemsSource = tourReservations;


                }
                else
                {
                    TourReservations tourReservation = new TourReservations();
                    List<TourReservations> tourReservations = new List<TourReservations>();
                    tourReservation = _tourReservationRepositery.FindMostAttendantTourByYear(FilePath, selectedYear);
                    tourReservations.Add(tourReservation);
                    MostVisitedTour.ItemsSource = tourReservations;

                }

            }
        }






        private void Fill(object sender, RoutedEventArgs e)
        {
            ComboBox.Items.Add("2023");
            ComboBox.Items.Add("2022");
            ComboBox.Items.Add("2021");
            ComboBox.Items.Add("All years");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        

        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    selectedTour = (TourReservations)MostVisitedTour.SelectedItem;
        //    int[] Info = new int[3];
        //    Info = _tourReservationRepositery.ShowStatistic(selectedTour.Tour.Id);
        //    //txt1.Text = Info[0].ToString();
        //    //txt2.Text = Info[1].ToString();
        //    //txt3.Text = Info[2].ToString();

        //    Age1 = 5;
        //    Age2 = 6;
        //    Age3 = 2;
           

        //}
    }
}
