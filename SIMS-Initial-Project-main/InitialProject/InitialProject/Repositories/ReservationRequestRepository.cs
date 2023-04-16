using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repositories
{
    public class ReservationRequestRepository: IReservationRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/reservationrequest.csv";

        private readonly Serializer<ReservationRequest> _serializer;

        private List<ReservationRequest> _reservationRequests;

        public ReservationRequestRepository()
        {
            _serializer = new Serializer<ReservationRequest>();
            _reservationRequests = _serializer.FromCSV(FilePath);
        }

        private int GetLastId()
        {
            if (_reservationRequests != null && _reservationRequests.Count > 0)
                return _reservationRequests.Max(reservationRequest => reservationRequest.Id);

            return 0;
        }

        public List<ReservationRequest> GetAllForGuest(int GuestId) { 
            return _reservationRequests.FindAll(reservationsRequest => reservationsRequest.GuestId == GuestId); 
        }

        public string CreateReservationRequest(ReservationRequest reservationRequest)
        {

            int reservationRequestId = this.GetLastId();
            reservationRequest.Id = reservationRequestId + 1;

            _reservationRequests.Add(reservationRequest);
            _serializer.ToCSV(FilePath, _reservationRequests);

            return "Zahtev je uspesno poslat!";
        }
    }
}
