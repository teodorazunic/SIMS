﻿using InitialProject.Domain.Models;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
        }

        public User LoggedInUser { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e) { 
        
            CreateTour createTour = new CreateTour();
            page.Content = createTour;
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Cancel createTour = new Cancel(LoggedInUser);
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
            page.Content =createTourByStatistics;
        }
    }
}
