using InitialProject.Domain.Models;
using InitialProject.Forms;
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

namespace InitialProject.WPF.Views.Guide
{
    /// <summary>
    /// Interaction logic for CreateByCity.xaml
    /// </summary>
    public partial class CreateByCity : Window
    {
        public CreateByCity(string location, User user)
        {
            InitializeComponent();
            PopularCity = location;
            txtCity.Text = location;
            LoggedInUser = user;

            notificatinsRepository = new TourNotificatinsRepository();
            requestRepository = new TourRequestRepository();

        }

        public string PopularCity { get; set; }

        public User LoggedInUser { get; set; }


        TourRepository repository = new TourRepository();
        KeyPointRepository keyPointRepository = new KeyPointRepository();
        List<KeyPoint> keyPoints = new List<KeyPoint>();
        TourNotificatinsRepository notificatinsRepository = new TourNotificatinsRepository();
        TourRequestRepository requestRepository = new TourRequestRepository();

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            int id = repository.NextId();
            String name = txtName.Text;
            Location location = new Location(PopularCity, txtCountry.Text);
            String description = txtDescription.Text;
            string language = txtLanguage.Text;
            int maxGuests = Convert.ToInt32(txtMaxGuests);
            DateTime start = Convert.ToDateTime(datePicker1.Text);
            int duration = Convert.ToInt32(txtDuration.Text);
            string image = txtImage.Text;
            string status = "Pending";
            int guideId = LoggedInUser.Id;


            Tour tour = new Tour(id, name, location, description, language, maxGuests, start, duration, image, status, guideId);
            Tour saveTour = repository.Save(tour);
            MessageBox.Show("Succesfully added tour!");


            TourNotification sending = new TourNotification();
            string text = "We have created a tour with your requested location:" + PopularCity;
            sending.Text = text;

            List<TourNotification> notifications = new List<TourNotification>();
            List<TourRequest> tourRequests = requestRepository.CheckNeverUsedCity(language);
            if (tourRequests != null)
            {
                for (int i = 0; i < tourRequests.Count(); i++)
                {
                    int guestId = tourRequests[i].GuestId;
                    sending.GuestId.Id = guestId;
                    notifications.Add(sending);
                }

                foreach (var notification in notifications)
                {
                    notificatinsRepository.Save(notification);
                }

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int id = keyPointRepository.NextId();
            string name = txtKeyPoint.Text;
            Tour tourId = new Tour();
            tourId.Id = repository.NextId();


            KeyPoint keypoint = new KeyPoint(name, id, tourId);
            keyPoints.Add(keypoint);
            KeyPoint saveKeyPoint = keyPointRepository.SaveKeyPoint(keypoint);
            txtKeyPoint.Text = "";

        }
    }
}
