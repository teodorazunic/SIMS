using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Model;
using InitialProject.Repositories;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System;

namespace InitialProject.WPF.ViewModels.Guest1
{
    public class ReservationRequestFormViewModel: MainViewModel
    {
        public User LoggedInUser { get; set; }

        public ReservationAccommodation ReservationAccommodation { get; set; }

        private readonly ReservationMovingRepository _repository;

        private string _comment = "";
        private DateTime _dateFrom = DateTime.Now;
        private DateTime _dateTo = DateTime.Now;
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

        public DateTime ReservationDateFrom
        {
            get => _dateFrom;
            set
            {
                if (value != _dateFrom)
                {
                    _dateFrom = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime ReservationDateTo
        {
            get => _dateTo;
            set
            {
                if (value != _dateTo)
                {
                    _dateTo = value;
                    OnPropertyChanged();
                }
            }
        }

        public ReservationRequestFormViewModel(User loggedInUser, ReservationAccommodation selectedReservationAccommodation)
        {
            ReservationAccommodation = selectedReservationAccommodation;
            LoggedInUser = loggedInUser;
            _repository = new ReservationMovingRepository();
        }

        public void Save()
        {
            ReservationMoving reservationRequest = new ReservationMoving();
            reservationRequest.GuestId = LoggedInUser.Id;
            reservationRequest.GuestUsername = LoggedInUser.Username;
            reservationRequest.GuestComment = Comment;
            reservationRequest.Status = RequestStatus.pending;
            reservationRequest.OldStartDate = ReservationAccommodation.SelectedReservation.DateFrom;
            reservationRequest.OldEndDate = ReservationAccommodation.SelectedReservation.DateTo;
            reservationRequest.ReservationId = ReservationAccommodation.SelectedReservation.Id;
            reservationRequest.AccommodationId = ReservationAccommodation.SelectedAccommodation.Id;
            reservationRequest.NewStartDate = ReservationDateFrom;
            reservationRequest.NewEndDate = ReservationDateTo;

            string message = _repository.CreateReservationRequest(reservationRequest);

            MessageBox.Show(message);
        }
    }
}
