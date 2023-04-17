using InitialProject.Domain.Models;
using InitialProject.Repositories;
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

namespace InitialProject.WPF.Views.Guide
{
    /// <summary>
    /// Interaction logic for Reviews.xaml
    /// </summary>
    public partial class Reviews : Window
    {
        private const string FilePath = "../../../Resources/Data/tour.csv";

        public User LoggedInUser { get; set; }

        private Tour _selectedTour;

        private GuestOnTour _selectedGuest;

        private GradeGuide _selectedGradeGuide;

    


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


        public Reviews(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new TourRepository();
            _guestOnTourRepository = new GuestOnTourRepository();
            Tours.ItemsSource = _repository.GetEndedTours(FilePath);
        }

        private readonly TourRepository _repository;
        private readonly GuestOnTourRepository _guestOnTourRepository = new GuestOnTourRepository();
        private readonly GradeGuideRepository _gradeGuideRepository = new GradeGuideRepository();


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnRowClick(object sender, RoutedEventArgs e)
        {
            SelectedTour = _repository.GetTourById(SelectedTour.Id);
            List<GuestOnTour> guestsOnTour = new List<GuestOnTour>();
            guestsOnTour = _guestOnTourRepository.GetGuestByTourId(SelectedTour.Id);
            Guests.ItemsSource = guestsOnTour;

        }

        public void OnRowClick1(object sender, RoutedEventArgs e)
        {
            _selectedGuest = (GuestOnTour)Guests.SelectedItem;
            List<GradeGuide> gradeGuide = new List<GradeGuide>();
            gradeGuide = _gradeGuideRepository.GetGradeByGuestId(_selectedGuest.GuestId);
            Grades.ItemsSource = gradeGuide;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _selectedGradeGuide = (GradeGuide)Grades.SelectedItem;

            if (_selectedGradeGuide != null)
            {
               _selectedGradeGuide.Validation = "Invalid";
               _gradeGuideRepository.Update(_selectedGradeGuide);
                MessageBox.Show("Uspesno prijavljena recenzija.");
            }
            Grades.Items.Refresh();
        }

       
    }
}
