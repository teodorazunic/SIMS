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
using System.Xml.Linq;
using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.View.Owner;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for OwnerOverview.xaml
    /// </summary>
    public partial class OwnerOverview : Window
    {

        public static ObservableCollection<Accommodation> Accommodations { get; set; }

        public Accommodation SelectedHotel { get; set; }

        public User LoggedInUser { get; set; }
        private readonly GradeOwnerRepository gradeOwnerRepository;

        private readonly AccommodationRepository _repository;

        public int DaysLeftForGrade = 5;

        private string _name = "";
        private string _city = "";
        private string _country = "";
        private int _typeIndex = -1;
        private Accommodation _selectedAccommodation;

        public OwnerOverview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new AccommodationRepository();
            Accommodations = new ObservableCollection<Accommodation>(_repository.GetAllAccomodations());
            gradeOwnerRepository = new GradeOwnerRepository();
        }

        public OwnerOverview()
        {
        }




        /*  private void OnLoad(object sender, RoutedEventArgs e)
          {
              List<Accommodation> accommodations = new List<Accommodation>();
              accommodations = _repository.GetAllAccomodations();
              DataPanel.ItemsSource = accommodations;
          }
        */

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Accommodation SelectedAccommodation
        {
            get => _selectedAccommodation;
            set
            {
                if (value != _selectedAccommodation)
                {
                    _selectedAccommodation = value;
                    OnPropertyChanged();
                }
            }
        }
        public string AccommodationName
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AccommodationCity
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AccommodationCountry
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        public int AccommodationTypeIndex
        {
            get => _typeIndex;
            set
            {
                if (value != _typeIndex)
                {
                    _typeIndex = value;
                    OnPropertyChanged();
                }
            }
        }


        private void GradeAlert(object sender, RoutedEventArgs e)
        {

        }

        private void OpenGradeForm(object sender, RoutedEventArgs e)
        {
            GradeForm createGradeForm = new GradeForm();
            createGradeForm.Show();
        }

        public void Logout(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OwnerForm1 createOwnerForm = new OwnerForm1();
            createOwnerForm.Show();
        }

        private void OpenReviewList(object sender, RoutedEventArgs e)
        {

            ReviewFromGuests createReviewForm = new ReviewFromGuests(LoggedInUser);
            createReviewForm.Show();

        }

        private void SuperOwnerLabel_Loaded(object sender, RoutedEventArgs e)
        {
            SuperOwnerLabel.Content = gradeOwnerRepository.SuperOwner();
        }

        private void GradeGuest(object sender, RoutedEventArgs e)
        {
            GradeForm createGradeForm = new GradeForm();
            createGradeForm.Show();
        }

        private void OpenMoveReservation(object sender, RoutedEventArgs e)
        {
            ReservationMovingRequests createMoveReservation = new ReservationMovingRequests();
            createMoveReservation.Show();
        }
    }
}
