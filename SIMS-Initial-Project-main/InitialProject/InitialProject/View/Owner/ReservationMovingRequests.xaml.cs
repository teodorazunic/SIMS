using System;
using System.Collections.Generic;
using System.Linq;
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
using InitialProject.Forms;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for ReservationMovingRequests.xaml
    /// </summary>
    public partial class ReservationMovingRequests : Window
    {
        private readonly ReservationMovingRepository reservationMovingRepository;
        public ReservationMoving SelectedReservation { get; set; }
        public ReservationMovingRequests()
        {
            InitializeComponent();
            reservationMovingRepository = new ReservationMovingRepository();
            SelectedReservation = new ReservationMoving();
        }


        private void OnLoad(object sender, RoutedEventArgs e)
        {
            DataPanel.ItemsSource = reservationMovingRepository.GetAll();
        }


        private void AcceptMoveReservation(object sender, RoutedEventArgs e)
        {
            if (this.SelectedReservation != null)
            {
                SelectedReservation = (ReservationMoving)DataPanel.SelectedItem;
                reservationMovingRepository.MoveReservation(SelectedReservation.ReservationId, SelectedReservation.NewStartDate, SelectedReservation.NewEndDate);
                DataPanel.ItemsSource = reservationMovingRepository.GetAll();
            }
            else { }
        }

        private void DeclineMoveReservation(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                SelectedReservation = (ReservationMoving)DataPanel.SelectedItem;
                reservationMovingRepository.Delete(SelectedReservation);
                DataPanel.ItemsSource = reservationMovingRepository.GetAll();
            }
            else { }
        }

        private void ReservationInfo(object sender, SelectionChangedEventArgs e)
        {
            SelectedReservation = (ReservationMoving)DataPanel.SelectedItem;
            ReservationInfoLabel.Content = reservationMovingRepository.TextForReservationInfo(SelectedReservation.ReservationId, SelectedReservation.AccommodationId, SelectedReservation.NewStartDate, SelectedReservation.NewEndDate);
        }
    }
}
