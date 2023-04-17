using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels.Guest1;
using System.Windows;

namespace InitialProject.Forms
{
    /// <summary>
    /// Interaction logic for ReservationRequests.xaml
    /// </summary>
    public partial class ReservationRequests : Window
    {
        private ReservationRequestsViewModel _reservationRequestFormViewModel;
        public ReservationRequests(User user)
        {
            InitializeComponent();
            _reservationRequestFormViewModel = new ReservationRequestsViewModel(user);
            DataContext = _reservationRequestFormViewModel;
            ReservationRequestsList.ItemsSource = _reservationRequestFormViewModel.GetAllForGuest();
        }
    }
}
