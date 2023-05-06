using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.ServiceInterfaces;
using InitialProject.Model;
using System.Collections.Generic;

namespace InitialProject.Application.Services
{
    public class ReservationMovingService : IReservationMovingService
    {

        private readonly IReservationMovingRepository _reservationMovingRepository;

        public ReservationMovingService(IReservationMovingRepository reservationMovingRepository)
        {
            _reservationMovingRepository = reservationMovingRepository;
        }

        public List<ReservationMoving> GetAllForGuest(int GuestId)
        {
            return _reservationMovingRepository.GetAllForGuest(GuestId);
        }

        public string CreateReservationRequest(ReservationMoving reservationRequest)
        {
            return _reservationMovingRepository.CreateReservationRequest(reservationRequest);
        }
    }
}
