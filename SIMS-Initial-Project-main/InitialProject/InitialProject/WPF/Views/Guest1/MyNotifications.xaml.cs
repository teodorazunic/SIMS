using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Repositories;
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

        public User LoggedInUser { get; set; }

        private readonly NotificationRepository _repository;

        private Notification _selectedNotification;

        public Notification SelectedNotification
        {
            get => _selectedNotification;
            set
            {
                if (value != _selectedNotification)
                {
                    _selectedNotification = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MyNotifications(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new NotificationRepository();
            Notifications.ItemsSource = _repository.GetAllForUser(user.Id);
        }

        public void MarkRead(object sender, RoutedEventArgs e)
        {

            if (SelectedNotification != null)
            {
                SelectedNotification.HasRead = true;
                _repository.Update(SelectedNotification);


                Notifications.ItemsSource = _repository.GetAllForUser(LoggedInUser.Id);
                Notifications.Items.Refresh();

            }
        }

    }
}
