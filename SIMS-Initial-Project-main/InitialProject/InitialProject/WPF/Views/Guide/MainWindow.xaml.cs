using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.WPF.Views.Guide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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

namespace InitialProject.WPF.Guide
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            guideRepository = new GuideRepository();
            rec.Fill = System.Windows.Media.Brushes.AliceBlue;
        }
        public User LoggedInUser { get; set; }

        private readonly GuideRepository guideRepository;

        private void superGuide(object sender, RoutedEventArgs e)
        {
            //superGuideLabel.Content = guideRepository.SuperGuide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateTour createTour = new CreateTour();
            page.Content = createTour;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CancelTour createTour = new CancelTour(LoggedInUser);
            page.Content = createTour;
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LiveTour liveTour = new LiveTour(LoggedInUser);
            page.Content = liveTour;

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Dissmisal dissmisal = new Dissmisal(LoggedInUser);
            page.Content = dissmisal;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Review review = new Review(LoggedInUser);
            page.Content = review;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            CreateTourByStatistics createTourByStatistics = new CreateTourByStatistics();
            page.Content = createTourByStatistics;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Request request = new Request(LoggedInUser);
            page.Content = request;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            CompoundTour compoundTour = new CompoundTour(LoggedInUser);
            page.Content = compoundTour;

        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Report report = new Report();
            page.Content = report;
        }

        

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            TourStatistics tourStatistics = new TourStatistics(LoggedInUser);
            page.Content = tourStatistics;
        }
    }
}
