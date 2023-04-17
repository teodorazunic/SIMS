using InitialProject.Domain;
using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace InitialProject.Forms
{
    /// <summary>
    /// Interaction logic for ReservationRequests.xaml
    /// </summary>
    public partial class ReservationRequests : Window
    {
        public User LoggedInUser { get; set; }

        private readonly IReservationMovingRepository _repository;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ReservationRequests(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = Injector.CreateInstance<IReservationMovingRepository>();
            ReservationRequestsList.ItemsSource = _repository.GetAllForGuest(user.Id);
        }

    }
}
