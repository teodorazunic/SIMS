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
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;



namespace InitialProject.WPF.Views.Guest2
{
    /// <summary>
    /// Interaction logic for GuestRequestStatistics.xaml
    /// </summary>
    public partial class GuestRequestStatistics : Window
    {
        public User LoggedInUser { get; set; }
        private SeriesCollection _data;
        private readonly TourRequestRepository _requestsRepository;

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public SeriesCollection SeriesCollection1 { get; set; }
        public string[] Labels1 { get; set; }
        public Func<double, string> Formatter1 { get; set; }
        private int AcceptedCount { get; set; }
        private int DeniedCount { get; set; }

        private int languageCount1 { get; set; }

        private int languageCount2 { get; set; }
        private string languageSrpski { get; set; }

        private string languageEngleski { get; set; }

        private string location1 { get; set; }
        private int locationCount1 { get; set; }
        private string location2 { get; set; }
        private int locationCount2 { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GuestRequestStatistics(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            DataContext = this;
            //DataChart = new SeriesCollection();
            _requestsRepository = new TourRequestRepository();
            languageSrpski = "srpski";
            languageEngleski = "engleski";
            languageCount1 = 0;
            languageCount1 = _requestsRepository.CalculateTourRequestsByLanguage(LoggedInUser.Id, languageSrpski);
            languageCount2 = 0;
            languageCount2 = _requestsRepository.CalculateTourRequestsByLanguage(LoggedInUser.Id, languageEngleski);

            location1 = "Srbija";
            locationCount1 = 0;
            locationCount1 = _requestsRepository.CalculateTourRequestsByLocation(LoggedInUser.Id, location1);
            location2 = "Crna Gora";
            locationCount2 = 0;
            locationCount2 = _requestsRepository.CalculateTourRequestsByLocation(LoggedInUser.Id, location2);
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "srpski",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(languageCount1)}
                }
            };

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "engleski",
                Values = new ChartValues<ObservableValue> { new ObservableValue(languageCount2) }
            });


            Labels = new[] { "Broj zahteva spram jezika"};
            Formatter = value => value.ToString("N");


            SeriesCollection1 = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Srbija",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(locationCount1)}
                }
            };

            SeriesCollection1.Add(new ColumnSeries
            {
                Title = "Crna Gora",
                Values = new ChartValues<ObservableValue> { new ObservableValue(locationCount2) }
            });


            Labels1 = new[] { "Broj zahteva spram lokacije"};
            Formatter1 = value => value.ToString("N");


            DataContext = this;

            List<int> counts = new List<int>();
            counts = _requestsRepository.CalculateTourRequestStatusCounts(LoggedInUser.Id);
            AcceptedCount = counts[0];
            DeniedCount = counts[1];
            //UpdatePieChartData();
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

        }

        private void Years(object sender, RoutedEventArgs e)
        {
            {

                ComboBox.Items.Add("2023");
                ComboBox.Items.Add("2022");
                ComboBox.Items.Add("2021");
                ComboBox.Items.Add("2020");


            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedYear = comboBox.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedYear))
            {
                int year = Convert.ToInt32(selectedYear);
                List<int> yearCounts = _requestsRepository.CalculateTourRequestStatusByYear(LoggedInUser.Id, year);
                AcceptedCount = yearCounts[0];
                DeniedCount = yearCounts[1];

                DataChart.Clear();
                DataChart.Add(new PieSeries
                {
                    Title = "Accepted",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(AcceptedCount) }
                });
                DataChart.Add(new PieSeries
                {
                    Title = "Denied",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(DeniedCount) }
                });
            }
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

        private void ShowNotifications(object sender, RoutedEventArgs e)
        {
            TourNotifications tourNotifications = new TourNotifications(LoggedInUser);
            tourNotifications.Show();
            Close();
        }

        private void Statistics(object sender, RoutedEventArgs e)
        {
            GuestRequestStatistics guestRequestStatistics = new GuestRequestStatistics(LoggedInUser);
            guestRequestStatistics.Show();
            Close();
        }
    }
}
