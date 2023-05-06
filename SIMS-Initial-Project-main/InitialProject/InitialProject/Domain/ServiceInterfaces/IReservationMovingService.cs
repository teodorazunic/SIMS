using InitialProject.Model;
using System.Collections.Generic;

namespace InitialProject.Domain.ServiceInterfaces
{
    public interface IReservationMovingService
    {
        public List<ReservationMoving> GetAllForGuest(int GuestId);

        public string CreateReservationRequest(ReservationMoving reservationRequest);
    }
}
