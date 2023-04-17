using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels.Guest1;
using System.Windows;

namespace InitialProject.Forms
{
    /// <summary>
    /// Interaction logic for MyReservations.xaml
    /// </summary>
    public partial class MyReservations : Window
    {
        private MyReservationsViewModel _myReservationsViewModel;

        public MyReservations(User user)
        {
            InitializeComponent();
            _myReservationsViewModel = new MyReservationsViewModel(user);
            DataContext = _myReservationsViewModel;
            Reservations.ItemsSource = _myReservationsViewModel.GetReservationsByGuestId();
        }

        public void Delete(object sender, RoutedEventArgs e)
        {
            Reservations.ItemsSource = _myReservationsViewModel.Delete();
            Reservations.Items.Refresh();
        }
        public void SendRequest(object sender, RoutedEventArgs e)
        {
            _myReservationsViewModel.SendRequest();
            Close();
        }
    }
}
