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

namespace InitialProject.WPF.Views.Guest2
{
    /// <summary>
    /// Interaction logic for CreateCompoundRequest.xaml
    /// </summary>
    public partial class CreateCompoundRequest : Window
    {
        public User LoggedInUser { get; set; }
        TourRequestRepository repository = new TourRequestRepository();
        private int id = 0;
        //private int compoundTourId;
        private static int compoundTourIdCounter = 0;
        public CreateCompoundRequest(User user)
        {
            InitializeComponent();
            LoggedInUser = user;

        }

        private void SaveRequest(object sender, RoutedEventArgs e)
        {
            id = repository.GetLastId();
            int guestId = LoggedInUser.Id;
            Location location = new Location(txtCity.Text, txtCountry.Text);
            string description = txtDescription.Text;
            string language = txtLanguage.Text;
            int maxGuests = Convert.ToInt32(txtMaxGuests.Text);
            DateTime startDate = Convert.ToDateTime(datePicker1.Text);
            DateTime endDate = Convert.ToDateTime(datePicker2.Text);
            string status = "Pending";
            string type = "compound";

            TourRequest tourRequest = new TourRequest(id, guestId, location, description, language, maxGuests, startDate, endDate, status, type);
            TourRequest saveRequest = repository.Save(tourRequest);
            //MessageBox.Show("Succesfully added tour!");
            finalTB.Text = "Your request has been sent.";
            SendButton.IsEnabled = false;
            AddButton.IsEnabled = false;
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

        private void Cancel(object sender, RoutedEventArgs e)
        {
            TourRequestOverview tourRequestOverview = new TourRequestOverview(LoggedInUser);
            tourRequestOverview.Show();
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //int id = repository.NextId();
            if(compoundTourIdCounter > 0)
            {
                id = repository.GetLastId();
            }
            else
            {
                id = repository.NextId();
            }
            int guestId = LoggedInUser.Id;
            Location location = new Location(txtCity.Text, txtCountry.Text);
            string description = txtDescription.Text;
            string language = txtLanguage.Text;
            int maxGuests = Convert.ToInt32(txtMaxGuests.Text);
            DateTime startDate = Convert.ToDateTime(datePicker1.Text);
            DateTime endDate = Convert.ToDateTime(datePicker2.Text);
            string status = "Pending";
            string type = "compound";

            TourRequest tourRequest = new TourRequest(id, guestId, location, description, language, maxGuests, startDate, endDate, status, type);
            TourRequest saveRequest = repository.Save(tourRequest);
            txtCity.Text = "";
            txtCountry.Text = "";
            txtDescription.Text = "";
            txtLanguage.Text = "";
            txtMaxGuests.Text = "";
            compoundTourIdCounter++;
        }
    }
}
