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
    /// Interaction logic for CreateByRequest.xaml
    /// </summary>
    public partial class CreateByRequest : Window
    {
        public DateTime RequestDate { get; set; }

        public Location RequestLocation { get; set; }

        public string RequestDescription { get; set; }

        public string RequestLanguage { get; set; }

        public int RequestGuests { get; set; }
        public CreateByRequest(DateTime date, Location location, string description,string language, int maxGuests)
        {
            InitializeComponent();
            RequestDate = date;
            RequestLocation = location;
            RequestDescription = description;
            RequestLanguage = language;
            RequestGuests = maxGuests;
            notificatinsRepository = new TourNotificatinsRepository();
            
        }

        TourRepository repository = new TourRepository();
        KeyPointRepository keyPointRepository = new KeyPointRepository();
        List<KeyPoint> keyPoints = new List<KeyPoint>();
        TourNotificatinsRepository notificatinsRepository = new TourNotificatinsRepository();


        private void Button_Click(object sender, RoutedEventArgs e)
        {


            int id = repository.NextId();
            String name = txtName.Text;
            Location location = new Location(RequestLocation.City, RequestLocation.Country);
            String description = RequestDescription;
            string language = RequestLanguage;
            int maxGuests = Convert.ToInt32(RequestGuests);
            DateTime start = Convert.ToDateTime(RequestDate);
            int duration = Convert.ToInt32(txtDuration.Text);
            string image = txtImage.Text;
            string status = "Pending";


            Tour tour = new Tour(id, name, location, description, language, maxGuests, start, duration, image, status);
            Tour saveTour = repository.Save(tour);
            MessageBox.Show("Succesfully added tour!");


            
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
