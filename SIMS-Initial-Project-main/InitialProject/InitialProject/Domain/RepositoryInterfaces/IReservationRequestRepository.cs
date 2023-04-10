using InitialProject.Domain.Models;
using System.Collections.Generic;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IReservationRequestRepository
    {
        public List<ReservationRequest> GetAllForGuest(int GuestId);

        public string CreateReservationRequest(ReservationRequest reservationRequest);
    }
}
