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
using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels.Owner;

namespace InitialProject.WPF.Views.Owner
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
