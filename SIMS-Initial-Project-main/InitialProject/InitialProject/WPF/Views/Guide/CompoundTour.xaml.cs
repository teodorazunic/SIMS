﻿using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.Views.Guide
{
    /// <summary>
    /// Interaction logic for CompoundTour.xaml
    /// </summary>
    public partial class CompoundTour : Page
    {
        private const string FilePath = "../../../Resources/Data/tourrequests.csv";

        public static ObservableCollection<TourRequest> TourRequests { get; set; }

        private readonly TourRepository tourRepository;

        public User LoggedInUser { get; set; }
        private TourRequest _selectedTourRequest;

        public TourRequest SelectedTourRequest
        {
            get => _selectedTourRequest;
            set
            {
                if (value != _selectedTourRequest)
                {
                    _selectedTourRequest = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly TourRequestRepository _tourRequestsRepository;

        public CompoundTour(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _tourRequestsRepository = new TourRequestRepository();
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestsRepository.GetCompoundTourRequests());
            tourRepository = new TourRepository();
            myRectangle.Fill = System.Windows.Media.Brushes.AliceBlue;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = Convert.ToDateTime(datePicker.Text);
            List<Tour> tours = tourRepository.GetAllTours();
            int count = 0;

            if (SelectedTourRequest != null)
            {
                if (SelectedTourRequest.StartDate < selectedDate && SelectedTourRequest.EndDate > selectedDate)
                {
                    foreach (Tour tour in tours)
                    {
                        if (tour.Start == selectedDate)
                        {
                            count++;
                        }

                    }

                    if (count > 0)
                    {
                        txtLab.Text = "You are not available for this tour.";
                        //MessageBox.Show("You are not available for this tour");
                    }
                    else

                        txtLab.Text = "Accepted request.";
                        //MessageBox.Show("Accepted request.");
                        _tourRequestsRepository.AcceptCompoundTour(SelectedTourRequest);

                    }
                }
            }
        }
    }

