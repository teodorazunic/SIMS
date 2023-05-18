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
    /// Interaction logic for CreateByStatistics.xaml
    /// </summary>
    public partial class CreateByStatistics : Window
    {
        private readonly TourRequestRepository tourRequestRepository;
        public CreateByStatistics(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            tourRequestRepository = new TourRequestRepository();
            string mostWantedCity = tourRequestRepository.FindMostWantedCity();
            txtLocation.Text = mostWantedCity;

            string mostWantedLanguage = tourRequestRepository.FindMostWantedLanguage();
            txtLanguage.Text = mostWantedLanguage;
           
        }

        public User LoggedInUser { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateByLanguage createByLanguage = new CreateByLanguage(txtLanguage.Text, LoggedInUser);
            createByLanguage.Show();
            this.Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateByCity createByCity = new CreateByCity(txtLocation.Text, LoggedInUser);
            createByCity.Show();
            this.Close();

        }
    }
}
