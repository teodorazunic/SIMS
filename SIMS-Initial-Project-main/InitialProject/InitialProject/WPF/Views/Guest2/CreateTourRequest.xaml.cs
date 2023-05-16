using InitialProject.Domain.Models;
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
using InitialProject.Domain.Models;
using InitialProject.Repository;
using InitialProject.Repositories;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;
using InitialProject.WPF.Views.Guest2;

namespace InitialProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for CreateTourRequest.xaml
    /// </summary>
    public partial class CreateTourRequest : Window
    {

        public User LoggedInUser { get; set; }
        TourRequestRepository repository = new TourRequestRepository();

        public CreateTourRequest(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
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

        private void SaveRequest(object sender, RoutedEventArgs e)
        {

            //TourRequest finalRequest = new TourRequest();
            /*finalRequest.GuestId = LoggedInUser.Id;
            finalRequest.Location.City = txtCity.Text;
            finalRequest.Location.Country = txtCountry.Text;
            finalRequest.Description = txtDescription.Text;
            finalRequest.Language = txtLanguage.Text;
            finalRequest.MaxGuests = Convert.ToInt32(txtMaxGuests.Text);
            finalRequest.StartDate = Convert.ToDateTime(datePicker1.Text);
            finalRequest.EndDate = Convert.ToDateTime(datePicker2.Text);*/
            int id = repository.NextId();
            int guestId = LoggedInUser.Id;
            Location location = new Location(txtCity.Text, txtCountry.Text);
            string description = txtDescription.Text;
            string language = txtLanguage.Text;
            int maxGuests = Convert.ToInt32(txtMaxGuests.Text);
            DateTime startDate = Convert.ToDateTime(datePicker1.Text);
            DateTime endDate = Convert.ToDateTime(datePicker2.Text);
            string status = "Pending";
            string type = "regular";

            TourRequest tourRequest = new TourRequest(id, guestId, location, description, language, maxGuests, startDate, endDate, status, type);
            TourRequest saveRequest = repository.Save(tourRequest);
            //MessageBox.Show("Succesfully added tour!");
            finalTB.Text = "Your request has been sent.";
            SendButton.IsEnabled = false;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            TourRequestOverview tourRequestOverview = new TourRequestOverview(LoggedInUser);
            tourRequestOverview.Show();
            Close();
        }
    }
}
