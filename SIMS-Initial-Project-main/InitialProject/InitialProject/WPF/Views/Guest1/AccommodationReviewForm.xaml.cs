using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels.Guest1;
using System.Windows;

namespace InitialProject.Forms
{
    /// <summary>
    /// Interaction logic for AccommodationReviewForm.xaml
    /// </summary>
    public partial class AccommodationReviewForm : Window
    {
        private AccommodationReviewFormViewModel _accommodationReviewFormViewModel;

        public AccommodationReviewForm(User loggedInUser, Accommodation selectedAccommodation)
        {
            InitializeComponent();
            Title = "Add a review";
            _accommodationReviewFormViewModel = new AccommodationReviewFormViewModel(loggedInUser, selectedAccommodation);
            DataContext = _accommodationReviewFormViewModel;
        }

        private void SaveReview(object sender, RoutedEventArgs e)
        {
            _accommodationReviewFormViewModel.SaveReview();
            Close();
        }

        private void SetImage(object sender, RoutedEventArgs e)
        {
            ReviewImages.ItemsSource = _accommodationReviewFormViewModel.SetImage();
            ReviewImages.Items.Refresh();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
