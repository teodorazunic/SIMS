using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Repositories;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace InitialProject.Forms
{
    /// <summary>
    /// Interaction logic for ReservationRequestForm.xaml
    /// </summary>
    public partial class ReservationRequestForm : Window
    {
        public User LoggedInUser { get; set; }

        public ReservationAccommodation ReservationAccommodation { get; set; }

        private readonly ReservationRequestRepository _repository;

        private string _comment = "";
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ReservationRequestForm(User loggedInUser, ReservationAccommodation selectedReservationAccommodation)
        {
            InitializeComponent();
            DataContext = this;
            ReservationAccommodation = selectedReservationAccommodation;
            LoggedInUser = loggedInUser;
            _repository = new ReservationRequestRepository();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            ReservationRequest reservationRequest = new ReservationRequest();
            reservationRequest.GuestId = LoggedInUser.Id;
            reservationRequest.GuestComment = Comment;
            reservationRequest.Status = RequestStatus.pending;
            reservationRequest.ReservationId = ReservationAccommodation.SelectedReservation.Id;

            string message = _repository.CreateReservationRequest(reservationRequest);
            
            MessageBox.Show(message);
            
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
