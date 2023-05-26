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
using InitialProject.Application.Services;
using InitialProject.Domain.Models;
using InitialProject.Repository;

namespace InitialProject.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for RenovationList.xaml
    /// </summary>
    public partial class RenovationList : Window
    {
        ReservationRepository reservationRepository;
        public RenovationList()
        {
            reservationRepository = new ReservationRepository();
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            List<Renovation> renovation = new List<Renovation>();
            renovation = reservationRepository.ShowAllRenovation();
            DataPanel.ItemsSource = renovation;
        }

        private void CancelRenovation(object sender, RoutedEventArgs e)
        {
            Renovation renovationRequest = (Renovation)DataPanel.SelectedItem;
            reservationRepository.CancelRenovation(renovationRequest);
            DataPanel.ItemsSource = reservationRepository.ShowAllRenovation();
        }
    }
}
