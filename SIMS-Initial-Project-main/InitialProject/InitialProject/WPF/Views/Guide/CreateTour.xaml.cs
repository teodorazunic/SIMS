using InitialProject.Domain.Models;
using InitialProject.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace InitialProject.WPF.Views.Guide
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour : Page
    {
        public CreateTour()
        {
            InitializeComponent();
        }

        public static string image;
        

        public void NumbersOnly(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");

            if (regex.IsMatch(e.Text))
            {
                e.Handled = true;
                txtMaxGuests.Background = Brushes.Red;

            }
            else
            {
                txtMaxGuests.ClearValue(TextBox.BackgroundProperty);

            }
        }


        TourRepository repository = new TourRepository();
        KeyPointRepository keyPointRepository = new KeyPointRepository();
        List<KeyPoint> keyPoints = new List<KeyPoint>();


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id = repository.NextId();
            String name = txtName.Text;
            Location location = new Location(txtCountry.Text, txtCity.Text);
            String description = txtDescription.Text;
            string language = txtLanguage.Text;
            int maxGuests = Convert.ToInt32(txtMaxGuests.Text);
            DateTime start = Convert.ToDateTime(datePicker1.Text);
            int duration = Convert.ToInt32(txtDuration.Text);
           
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

            KeyPointsList.Items.Add(name);

        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Image Files (.png;.jpg;.jpeg)|.png;.jpg;.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                Image imageControl = new Image();
                string imagePath = openFileDialog.FileName;

                // Set the TextBox text to the image path (URL)
                image = imagePath;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string datum = datePicker1.Text;
            datePicker1.Text = "";
           
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            
               Tutorialxaml tutorialxaml = new Tutorialxaml();
               NavigationService.Navigate(tutorialxaml);
            
        }
    }
}
