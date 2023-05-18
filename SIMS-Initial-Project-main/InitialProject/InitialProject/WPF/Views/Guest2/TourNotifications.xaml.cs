using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InitialProject.Domain.Models;
using InitialProject.Forms;
using InitialProject.Repositories;
using InitialProject.Repository;
using InitialProject.View;
using InitialProject.WPF.Views;
using InitialProject.WPF.Views.Guest2;

namespace InitialProject.WPF.Views.Guest2
{
    /// <summary>
    /// Interaction logic for TourNotifications.xaml
    /// </summary>
    public partial class TourNotifications : Window
    {
        public User LoggedInUser { get; set; }

        public static ObservableCollection<TourNotification> Notifications { get; set; }

        private readonly TourNotificatinsRepository _repository;

        public TourNotifications(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new TourNotificatinsRepository();
            Notifications = new ObservableCollection<TourNotification>(_repository.GetAllNotificationsByGuestId(LoggedInUser.Id));
            List<string> allNotifications = new List<string>();
            foreach (TourNotification notification in Notifications)
            {
                allNotifications.Add(notification.Text);
            }
            NotificationList.ItemsSource = allNotifications;

        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }


        private void ShowVouchers(object sender, RoutedEventArgs e)
        {
            Vouchers vouchers = new Vouchers(LoggedInUser);
            vouchers.Show();
            Close();
        }

        private void Ratings(object sender, RoutedEventArgs e)
        {
            ShowPastTours ratings = new ShowPastTours(LoggedInUser);
            ratings.Show();
            Close();
        }

        private void Active(object sender, RoutedEventArgs e)
        {
            ActiveTour activeTour = new ActiveTour(LoggedInUser);
            activeTour.Show();
            Close();
        }

        private void Requests(object sender, RoutedEventArgs e)
        {
            TourRequestOverview tourRequestOverview = new TourRequestOverview(LoggedInUser);
            tourRequestOverview.Show();
            Close();
        }

        private void Home(object sender, RoutedEventArgs e)
        {
            TourOverview tourOverview = new TourOverview(LoggedInUser);
            tourOverview.Show();
            Close();
        }

        private void NotificationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NotificationList.SelectedItem != null)
            {
                //var selectedItem = (ListItem)NotificationList.SelectedItem;
                TourOverview tourOverview = new TourOverview(LoggedInUser);
                tourOverview.Show();
                Close();
            }
        }
    }
}
