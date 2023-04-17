using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels.Guest1;
using System.Windows;

namespace InitialProject.Forms
{
    /// <summary>
    /// Interaction logic for ReservationRequestForm.xaml
    /// </summary>
    public partial class ReservationRequestForm : Window
    {
        private ReservationRequestFormViewModel _reservationRequestFormViewModel;

        public ReservationRequestForm(User loggedInUser, ReservationAccommodation selectedReservationAccommodation)
        {
            InitializeComponent();
            _reservationRequestFormViewModel = new ReservationRequestFormViewModel(loggedInUser, selectedReservationAccommodation);
            DataContext = _reservationRequestFormViewModel;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            _reservationRequestFormViewModel.Save();
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
