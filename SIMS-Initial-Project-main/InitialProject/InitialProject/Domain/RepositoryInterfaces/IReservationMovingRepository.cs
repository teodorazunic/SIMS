using InitialProject.Model;
using System.Collections.Generic;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IReservationMovingRepository
    {
        public List<ReservationMoving> GetAllForGuest(int GuestId);

        public string CreateReservationRequest(ReservationMoving reservationRequest);
    }
}
