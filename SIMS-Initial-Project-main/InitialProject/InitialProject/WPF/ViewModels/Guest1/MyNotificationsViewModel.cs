using InitialProject.Domain;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InitialProject.WPF.ViewModels.Guest1
{
    public class MyNotificationsViewModel: MainViewModel
    {

        public User LoggedInUser { get; set; }

        private readonly INotificationRepository _repository;

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
        public MyNotificationsViewModel(User user)
        {
            LoggedInUser = user;
            _repository = Injector.CreateInstance<INotificationRepository>();
        }

        public List<Notification> GetNotificationsForUser()
        {
            return _repository.GetAllForUser(LoggedInUser.Id);
        }

        public List<Notification> MarkRead()
        {
            if (SelectedNotification != null)
            {
                SelectedNotification.HasRead = true;
                _repository.Update(SelectedNotification);

            }
            return _repository.GetAllForUser(LoggedInUser.Id);
        }
    }
}
