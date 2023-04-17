using InitialProject.Domain;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using InitialProject.WPF.ViewModels.Guest1;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace InitialProject.Forms
{
    /// <summary>
    /// Interaction logic for MyNotifications.xaml
    /// </summary>
    public partial class MyNotifications : Window
    {
        private MyNotificationsViewModel _myNotificationsViewModel;

        public MyNotifications(User user)
        {
            InitializeComponent();
            _myNotificationsViewModel = new MyNotificationsViewModel(user);
            DataContext = _myNotificationsViewModel;
            Notifications.ItemsSource = _myNotificationsViewModel.GetNotificationsForUser();
        }

        public void MarkRead(object sender, RoutedEventArgs e)
        {
            Notifications.ItemsSource = _myNotificationsViewModel.MarkRead();
            Notifications.Items.Refresh();
        }
    }
}
