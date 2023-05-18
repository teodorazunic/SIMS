using InitialProject.Domain.Models;
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
    /// Interaction logic for CreateByLanguage.xaml
    /// </summary>
    public partial class CreateByLanguage : Window
    {
        public CreateByLanguage(string language, User user)
        {
            InitializeComponent();
            PopularLanguage = language;
            txtLanguage.Text = language;
            notificatinsRepository = new TourNotificatinsRepository();
            requestRepository = new TourRequestRepository();
            LoggedInUser = user;
        }



        public string PopularLanguage { get; set; }

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
            Location location = new Location(txtCity.Text, txtCountry.Text);
            String description = txtDescription.Text;
            string language = PopularLanguage;
            int maxGuests = Convert.ToInt32(txtMaxGuests.Text);
            DateTime start = Convert.ToDateTime(datePicker1.Text);
            int duration = Convert.ToInt32(txtDuration.Text);
            string image = txtImage.Text;
            string status = "Pending";


            Tour tour = new Tour(id, name, location, description, language, maxGuests, start, duration, image, status);
            Tour saveTour = repository.Save(tour);
            MessageBox.Show("Succesfully added tour!");

            List<TourNotification> notifications = new List<TourNotification>();

            TourNotification sending = new TourNotification();
            string text = "We have created a tour with your requested language:" + language ;
            sending.Text = text;
            

            List<TourRequest> tourRequests = requestRepository.CheckNeverUsedLanguage(language);
            if (tourRequests != null)
            {
                for (int i = 0; i < tourRequests.Count(); i++)
                {
                    int guestId = tourRequests[i].GuestId;
                    sending.GuestId.Id = guestId;
                    
                    notifications.Add(sending);
                    
                }

                foreach(var notification in notifications)
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

