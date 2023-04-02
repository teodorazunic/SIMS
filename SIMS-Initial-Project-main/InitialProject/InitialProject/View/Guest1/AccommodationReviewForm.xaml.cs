using InitialProject.Model;
using InitialProject.Repository;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace InitialProject.Forms
{
    /// <summary>
    /// Interaction logic for AccommodationReviewForm.xaml
    /// </summary>
    public partial class AccommodationReviewForm : Window
    {

        public static Accommodation SelectedAccommodation { get; set; }
        public User LoggedInUser { get; set; }

        private readonly AccommodationReviewRepository _repository;

        private List<AccommodationReviewImage> Images;

        private int _cleanliness = 5;
        private int _staff = 5;
        private string _comment = "";
        private string _image = "";

        public int Cleanliness
        {
            get => _cleanliness;
            set
            {
                if (value != _cleanliness)
                {
                    _cleanliness = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Staff
        {
            get => _staff;
            set
            {
                if (value != _staff)
                {
                    _staff = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Image
        {
            get => _image;
            set
            {
                if (value != _image)
                {
                    _image = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AccommodationReviewForm(User loggedInUser, Accommodation selectedAccommodation)
        {
            InitializeComponent();
            Title = "Add a review";
            DataContext = this;
            _repository = new AccommodationReviewRepository();
            Images = new List<AccommodationReviewImage>();
            SelectedAccommodation = selectedAccommodation;
            LoggedInUser = loggedInUser;
        }

        private void SaveReview(object sender, RoutedEventArgs e)
        {
            AccommodationReview accommodationReview = new AccommodationReview(LoggedInUser.Id, SelectedAccommodation.Id, Cleanliness, Staff, Comment);

            string message = _repository.SaveReview(accommodationReview, Images);

            MessageBox.Show(message);
            AccommodationOverview overview = new AccommodationOverview(LoggedInUser);
            overview.Show();
            Close();
        }

        private void SetImage(object sender, RoutedEventArgs e)
        {

            if (Image == null || Image == "")
            {
                MessageBox.Show("Upisite validan url!");
                return;
            }

            AccommodationReviewImage accommodationReviewImage = new AccommodationReviewImage();
            accommodationReviewImage.ImageUrl = Image;

            Images.Add(accommodationReviewImage);
            ReviewImages.ItemsSource = Images;
            ReviewImages.Items.Refresh();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
