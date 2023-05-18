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
using InitialProject.Application.Services;
using InitialProject.Domain.Models;
using InitialProject.Repository;
using System.Collections.Generic;
using LiveCharts;
using LiveCharts.Wpf;
using InitialProject.Domain.Model;

namespace InitialProject.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for AccommodationStatistics.xaml
    /// </summary>
    public partial class AccommodationStatistics : Window
    {
        private readonly ReservationRepository reservationRepository;
        private readonly AccommodationRepository accommodationRepository;
        private string _xTitle;
        private List<string> _xLabels;
        private SeriesCollection _data;
        private int _accommodationId;
        private int _year;
        private Frame ShowSmallFrame;
        public event PropertyChangedEventHandler? PropertyChanged;
        private User LogedUser { get; }
        public AccommodationStatistics(User user)
        {
            InitializeComponent();
            DataContext = this;
            reservationRepository = new ReservationRepository();
            accommodationRepository = new AccommodationRepository();
            LogedUser = user;
            DataChart = new SeriesCollection();
        }

        private void YearStatistic(object sender, RoutedEventArgs e)
        {
            DataChart.Clear();
            DateTime dateTime = DateTime.Now;
            List<string> labels = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                labels.Add((dateTime.Year - 4 + i).ToString());
            }
            HideButton.IsEnabled = true;
            ShowAllButton.IsEnabled = true;
            ShowButton.IsEnabled = true;
            ResultButton.Visibility = Visibility.Hidden;
            Label1.Visibility = Visibility.Hidden;
            Label2.Visibility = Visibility.Hidden;
            YearCB.Visibility = Visibility.Hidden;
            XTitle = "Years";
            XLabels = labels;
        }

        private void MounthStatistic(object sender, RoutedEventArgs e)
        {
            DataChart.Clear();
            List<string> months = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            HideButton.IsEnabled = true;
            ShowAllButton.IsEnabled = false;
            ShowButton.IsEnabled = true;
            ResultButton.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            YearCB.Visibility = Visibility.Visible;
            XTitle = "Months";
            XLabels = months;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            HotelCB.ItemsSource = accommodationRepository.FillForComboBoxHotels(LogedUser);
        }

        private void OnLoadYear(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            for (int i = 0; i < 5; i++)
            {
                YearCB.Items.Add(dateTime.Year - i);
            }
        }

        private void ShowStatisticForHotel(object sender, RoutedEventArgs e)
        {
            if (XTitle == "Years")
            {
                DataChart.Add(reservationRepository.ShowHotelDataInChart(AccommodationId));
            }
            else if (XTitle == "Months")
            {
                ColumnSeries columnSeries = new ColumnSeries();
                columnSeries.Title = AccommodationId + YearForStatistic.ToString();
                columnSeries.Values = new ChartValues<int>(reservationRepository.ShowHotelDataPerMonth(AccommodationId, YearForStatistic));
                DataChart.Add(columnSeries);
            }
            else { }

        }

        private void HideStatisticForHotel(object sender, RoutedEventArgs e)
        {
            var seriesToRemove = DataChart.FirstOrDefault(s => s.Title == AccommodationId.ToString());
            if (seriesToRemove != null)
            {
                DataChart.Remove(seriesToRemove);
            }
        }

        private void ShowAllStatistic(object sender, RoutedEventArgs e)
        {
            List<Accommodation> hotels = accommodationRepository.GetHotelByOwner(LogedUser.Id);
            foreach (Accommodation accommodation in hotels)
            {
                DataChart.Add(reservationRepository.ShowHotelDataInChart(accommodation.Id));
            }
        }
        private void Detect(object sender, RoutedEventArgs e)
        {
            List<int> monthValues = reservationRepository.ShowHotelDataPerMonth(AccommodationId, YearForStatistic);
            int i = 0;
            int max = monthValues[i];
            int month = i;
            for (i = 1; i < monthValues.Count; i++)
            {
                if (monthValues[i] > max)
                {
                    max = monthValues[i];
                    month = i;
                }
            }
            MessageBox.Show(AccommodationId + " was the busiest in year: " + YearForStatistic.ToString() + " month: " + ConvertIntToMonth(month) + " with number of reservations: " + max.ToString());
        }


        public string ConvertIntToMonth(int i)
        {
            switch (i)
            {
                case 0: return "Jan";
                case 1: return "Feb";
                case 2: return "Mar";
                case 3: return "Apr";
                case 4: return "May";
                case 5: return "Jun";
                case 6: return "Jul";
                case 7: return "Aug";
                case 8: return "Sep";
                case 9: return "Oct";
                case 10: return "Nov";
                case 11: return "Dec";
                default: return "";
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string XTitle
        {
            get => _xTitle;
            set
            {
                if (_xTitle != value)
                {
                    _xTitle = value;
                    OnPropertyChanged();
                }

            }
        }

        public List<string> XLabels
        {
            get => _xLabels;
            set
            {
                if (_xLabels != value)
                {
                    _xLabels = value;
                    OnPropertyChanged();
                }

            }
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

        public int AccommodationId
        {
            get => _accommodationId;
            set
            {
                if (_accommodationId != value)
                {
                    _accommodationId = value;
                    OnPropertyChanged();
                }

            }
        }

        public int YearForStatistic
        {
            get => _year;
            set
            {
                if (_year != value)
                {
                    _year = value;
                    OnPropertyChanged();
                }

            }
        }

    }
}
