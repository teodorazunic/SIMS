using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain;
using InitialProject.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace InitialProject.WPF.ViewModels.Guest1
{
    public class MyReservationsViewModel: MainViewModel
    {

        public User LoggedInUser { get; set; }

        private readonly IReservationRepository _repository;

        private ReservationAccommodation _selectedReservationAccommodation;

        public ReservationAccommodation SelectedReservationAccommodation
        {
            get => _selectedReservationAccommodation;
            set
            {
                if (value != _selectedReservationAccommodation)
                {
                    _selectedReservationAccommodation = value;
                    OnPropertyChanged();
                }
            }
        }

        public MyReservationsViewModel(User user)
        {
            LoggedInUser = user;
            _repository = Injector.CreateInstance<IReservationRepository>();
        }

        public List<ReservationAccommodation> GetReservationsByGuestId()
        {
            return _repository.GetAllByGuestId(LoggedInUser.Id);
        }

        public List<ReservationAccommodation> Delete()
        {

            if (SelectedReservationAccommodation != null)
            {
                Reservation reservation = SelectedReservationAccommodation.SelectedReservation;
                string message = _repository.DeleteReservation(reservation);

                MessageBox.Show(message);
            }
            return _repository.GetAllByGuestId(LoggedInUser.Id);
        }
        public void SendRequest()
        {
            if (SelectedReservationAccommodation != null)
            {
                ReservationAccommodation reservationAccommodation = SelectedReservationAccommodation;

                ReservationRequestForm form = new ReservationRequestForm(LoggedInUser, reservationAccommodation);
                form.Show();
            }
        }
    }
}
