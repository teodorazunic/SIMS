﻿using InitialProject.Domain.Models;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourForm.xaml
    /// </summary>
    public partial class TourForm : Window
    {
        public User LoggedInUser { get; set; }


        
        public TourForm()
        {
            InitializeComponent();
            
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
            Language language = new Language(txtLanguage.Text);
            int maxGuests = Convert.ToInt32(txtMaxGuests.Text);
            DateTime start = Convert.ToDateTime(datePicker1.Text);
            int duration = Convert.ToInt32(txtDuration.Text);
            string image = txtImage.Text;
            string status = "Pending";

            Tour tour = new Tour(id, name, location, description, language, maxGuests, start, duration, image, status );
            Tour saveTour = repository.Save(tour);
            TourSuccessfully.Text = "Successfully added tour!";
        }

       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int id = keyPointRepository.NextId();
            string name = txtKeyPoint.Text;
            int tourId = repository.NextId();
            

            KeyPoint keypoint = new KeyPoint(name, id, tourId);
            keyPoints.Add(keypoint);
            KeyPoint saveKeyPoint = keyPointRepository.SaveKeyPoint(keypoint);
            txtKeyPoint.Text = "";

            KeyPointsList.Items.Add(name);

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //myFrame.Source = new Uri("CancelTourPagw.xaml", UriKind.Relative);
        }
        private void NavigateToPage(string pageName)
        {
            mainFrame.Source = new Uri("CreateTourPage.xaml", UriKind.Relative);
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            NavigateToPage("CreateTourPage");
        }

        
    }
}
