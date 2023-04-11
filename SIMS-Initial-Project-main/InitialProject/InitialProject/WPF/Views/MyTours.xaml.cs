﻿using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace InitialProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for MyTours.xaml
    /// </summary>
    public partial class MyTours : Window
    {

        public User LoggedInUser { get; set; }

        private readonly TourRepository _repository;

        private Tour  _selectedTour;

        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {
                if (value != _selectedTour)
                {
                    _selectedTour = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MyTours(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new TourRepository();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                Tour tour = SelectedTour;
                string message = _repository.CancelTour(tour);

                MessageBox.Show(message);

                Reservations.Items.Refresh();

            }

        }
    }
}
