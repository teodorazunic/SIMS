using InitialProject.Model;
using System.Windows;

namespace InitialProject.Forms
{
    /// <summary>
    /// Interaction logic for ReservationStatus.xaml
    /// </summary>
    public partial class ReservationStatus : Window
    {

        public User LoggedInUser { get; set; }
        public string Message { get; set; }

        public int AccommodationId { get; set; }
       

        public ReservationStatus(int accommodationId, User user, string message)
        {
            InitializeComponent();
            Title = "Reservation";
            DataContext = this;
            LoggedInUser = user;
            Message = message;
            AccommodationId = accommodationId;
        }

        private void Back(object sender, RoutedEventArgs e) 
        {
            CheckAccommodation checkAccommodation = new CheckAccommodation(AccommodationId, LoggedInUser);
            checkAccommodation.Show();
            Close();
        } 
    }
}
