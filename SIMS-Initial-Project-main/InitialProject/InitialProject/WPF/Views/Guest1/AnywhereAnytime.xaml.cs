using InitialProject.Domain;
using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Forms;
using InitialProject.WPF.ViewModels.Guest1;
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

namespace InitialProject.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for AnywhereAnytime.xaml
    /// </summary>
    public partial class AnywhereAnytime : Window
    {
        private AnywhereAnytimeViewModel _viewModel;
        public AnywhereAnytime(User user)
        {
            InitializeComponent();
            _viewModel = new AnywhereAnytimeViewModel(user);
            DataContext = _viewModel;
        }

        public void OnSearch(object sender, RoutedEventArgs e)
        {
            FilteredAccommodations.ItemsSource = _viewModel.OnSearch();
        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            FilteredAccommodations.ItemsSource = _viewModel.OnLoad();
        }

        public void OnAccommodationClick(object sender, RoutedEventArgs e)
        {
            AccommodationDateRange selectedRow = _viewModel.FilteredAccommodations.Find(el => el.Accommodation.Id == _viewModel.Selected.Id);
            SaveReservation saveReservation = new SaveReservation(selectedRow.ReservationDates, _viewModel.Selected.Id, _viewModel.LoggedInUser, _viewModel.DaysNumber, _viewModel.GuestsNumber);
            saveReservation.Show();
            Close();
        }
    }
}
