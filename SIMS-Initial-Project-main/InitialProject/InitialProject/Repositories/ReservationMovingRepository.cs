using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using System.Windows;
using InitialProject.Forms;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Serializer;

namespace InitialProject.Repositories
{
    internal class ReservationMovingRepository
    {
        private const string FilePath = "../../../Resources/Data/movingrequests.csv";
        private readonly Serializer<ReservationMoving> _serializer;
        private readonly ReservationRepository reservationRepository;
        private readonly AccommodationRepository accommodationRepository;
        private List<ReservationMoving> _reservations;

        public ReservationMovingRepository()
        {
            _serializer = new Serializer<ReservationMoving>();
            _reservations = _serializer.FromCSV(FilePath);
            reservationRepository = new ReservationRepository();
            accommodationRepository = new AccommodationRepository();
        }

        public List<ReservationMoving> GetAll()
        {
            List<ReservationMoving> reservations = new List<ReservationMoving>();

            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    ReservationMoving reservation = new ReservationMoving();
                    reservation.ReservationId = Convert.ToInt32(fields[0]);
                    reservation.AccommodationId = Convert.ToInt32(fields[1]);
                    reservation.GuestUsername = fields[2];
                    reservation.OldStartDate = Convert.ToDateTime(fields[3]);
                    reservation.OldEndDate = Convert.ToDateTime(fields[4]);
                    reservation.NewStartDate = Convert.ToDateTime(fields[5]);
                    reservation.NewEndDate = Convert.ToDateTime(fields[6]);
                    reservations.Add(reservation);
                }
            }
            return reservations;
        }

        public ReservationMoving GetById(int id)
        {
            List<ReservationMoving> reservations = GetAll();
            foreach (ReservationMoving reservation in reservations)
            {
                if (reservation.ReservationId == id)
                    return reservation;
            }
            return null;
        }

        public void Delete(ReservationMoving reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            ReservationMoving founded = _reservations.Find(c => c.ReservationId == reservation.ReservationId);
            _reservations.Remove(founded);
            _serializer.ToCSV(FilePath, _reservations);
        }

        public ReservationMoving Save(ReservationMoving moveReservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            _reservations.Add(moveReservation);
            _serializer.ToCSV(FilePath, _reservations);
            return moveReservation;
        }

        public void MoveReservation(int id, DateTime newStartDate, DateTime newEndDate)
        {
            List<Reservation> reservations = reservationRepository.GetAll();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.Id == id)
                {
                    reservation.DateFrom = newStartDate;
                    reservation.DateTo = newEndDate;
                    reservationRepository.Update(reservation);
                    Delete(GetById(id));
                    MessageBox.Show("Reservation succesfuly changed.");
                }
            }
        }


        public string TextForReservationInfo(int reservationId, int accommodationId, DateTime newStartDate, DateTime newEndDate)
        {
            string InfoText;
            string Available;
            List<Reservation> reservations = reservationRepository.GetAll();
            List<Reservation> reservationData = new List<Reservation>();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.Id != reservationId)
                    reservationData.Add(reservation);

            }
            if (IsAvailable(reservationData, accommodationId, newStartDate, newEndDate))
            {
                Available = "is available";
            }
            else
            {
                Available = "not available";
            }
            InfoText = "Accommodation with ID " + accommodationId + " " + Available + " for requested period";
            return InfoText;
        }

        public bool IsAvailable(List<Reservation> reservations, int accommodationId, DateTime startDate, DateTime endDate)
        {
            Accommodation accommodation = accommodationRepository.GetAccommodationById(accommodationId);

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                int reservationsForDate = reservations.Where(r => r.AccommodationId == accommodationId && date >= r.DateFrom && date <= r.DateTo).Sum(r => r.GuestsNumber);

                if (reservationsForDate >= accommodation.GuestsNumber)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
