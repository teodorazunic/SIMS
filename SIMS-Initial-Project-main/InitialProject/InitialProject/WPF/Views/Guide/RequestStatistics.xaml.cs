using InitialProject.Domain.Models;
using InitialProject.Repositories;
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

namespace InitialProject.WPF.Views.Guide
{
    /// <summary>
    /// Interaction logic for RequestStatistics.xaml
    /// </summary>
    public partial class RequestStatistics : Window
    {

        private readonly TourRequestRepository _tourRequestRepository;
        public User LoggedInUser { get; set; }

        public RequestStatistics(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _tourRequestRepository = new TourRequestRepository();
        }

        private void FillCity(object sender, RoutedEventArgs e)
        {
            cbCity.Items.Add("Novi Sad");
            cbCity.Items.Add("Ruma");
            cbCity.Items.Add("Beograd");
            cbCity.Items.Add("");


        }

        //private void FillCountry(object sender, RoutedEventArgs e)
        //{
        //    cbCountry.Items.Add("Srbija");
        //    cbCountry.Items.Add("Makedonija");
        //    cbCountry.Items.Add("");

        //}

        private void FillLanguage(object sender, RoutedEventArgs e)
        {
            cbLanguage.Items.Add("srpski");
            cbLanguage.Items.Add("engleski");
            cbLanguage.Items.Add("spanski");
            cbLanguage.Items.Add("");

        }

        private void Fill(object sender, RoutedEventArgs e)
        {
            cbYears.Items.Add("2023");
            cbYears.Items.Add("2022");
            cbYears.Items.Add("All years");


        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string city = cbCity.Text;
           
            string years = cbYears.Text;
            string language = cbLanguage.Text;

            int broj = _tourRequestRepository.Statistic(city, years, language);

            txtNumber.Text = broj.ToString();

        }
    }
}
