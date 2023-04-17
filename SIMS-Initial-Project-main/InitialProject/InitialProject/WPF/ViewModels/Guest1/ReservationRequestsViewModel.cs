using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using InitialProject.Model;
using System.Collections.Generic;

namespace InitialProject.WPF.ViewModels.Guest1
{
    public class ReservationRequestsViewModel: MainViewModel
    {
        public User LoggedInUser { get; set; }

        private readonly IReservationMovingRepository _repository;

        public ReservationRequestsViewModel(User user)
        {
            LoggedInUser = user;
            _repository = Injector.CreateInstance<IReservationMovingRepository>();
        }

        public List<ReservationMoving> GetAllForGuest()
        {
            return _repository.GetAllForGuest(LoggedInUser.Id); 
        }

    }
}
