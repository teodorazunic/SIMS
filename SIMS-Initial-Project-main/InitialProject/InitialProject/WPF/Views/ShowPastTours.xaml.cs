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
using System.Windows.Shapes;
using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.Repository;
using InitialProject.View;

namespace InitialProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for ShowPastTours.xaml
    /// </summary>
    public partial class ShowPastTours : Window

    {
        public static ObservableCollection<TourReservations> PastTours { get; set; }
        private readonly TourReservationRepositery _tourReservationRepositery;
        private readonly GradeGuideRepository _gradeGuideRepository;
        

        private int _id = 0;
        private int _guests = 0;
        private Tour _selectedTour;

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

        public int TourId
        {
            get => _id;
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public int NumberOfGuests
        {
            get => _guests;
            set
            {
                if (value != _guests)
                {
                    _guests = value;
                    OnPropertyChanged();
                }
            }
        }

        public User LoggedInUser { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ShowPastTours(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _tourReservationRepositery = new TourReservationRepositery();
            _gradeGuideRepository = new GradeGuideRepository();
            PastTours = new ObservableCollection<TourReservations>(_tourReservationRepositery.GetAllReservationsByGuestId(LoggedInUser.Id));
        }

        public void OnRowClick(object sender, MouseButtonEventArgs e)
        {
            RatePastTours tourRatings = new RatePastTours(LoggedInUser);
            tourRatings.Show();
            //Close();
        }
    }
}
