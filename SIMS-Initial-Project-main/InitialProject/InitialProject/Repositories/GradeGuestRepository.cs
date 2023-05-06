using System;
using System.Collections.Generic;
using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;

namespace InitialProject.Repository
{
    class GradeGuestRepository : IGradeGuestRepository
    {
        private const string FilePath = "../../../Resources/Data/guestgrades.csv";

        private readonly Serializer<GuestGrade> _serializer;
        private readonly ReservationRepository reservationRepository;
        private readonly AccommodationReviewRepository _accommodationReviewRepository;

        private List<GuestGrade> _grades;

        public GradeGuestRepository()
        {
            _serializer = new Serializer<GuestGrade>();
            reservationRepository = new ReservationRepository();
            _accommodationReviewRepository = new AccommodationReviewRepository();
            _grades = _serializer.FromCSV(FilePath);
        }

        public string FindGuestsForGrade(int i)
        {
            List<Reservation> reservations = reservationRepository.GetAll();
            DateTime dateTimeNow = DateTime.Now;
            if (reservations[i].DateTo < dateTimeNow && reservations[i].DateTo.AddDays(5) > dateTimeNow && reservations[i].GradeStatus == "NotGraded")
            {
                string reservationForm = reservations[i].Id.ToString() + " " + reservations[i].GuestUserName;
                return reservationForm;
            }
            else return null;
        }

        public void FindAndDeleteExpiredReservation(int i)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.GetAll();
            DateTime dateTimeNow = DateTime.Now;

            if (reservations[i].DateTo < dateTimeNow && reservations[i].DateTo.AddDays(5) < dateTimeNow)
            {
                reservationRepository.LogicalDeleteExpire(reservations[i]);
            }
        }

        public string ShowMessageForGrade(int i)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.GetAll();
            DateTime dateTimeNow = DateTime.Now;
            string message = null;

            if (reservations[i].DateTo < dateTimeNow && reservations[i].DateTo.AddDays(5) > dateTimeNow && reservations[i].GradeStatus == "NotGraded")
            {
                if (dateTimeNow < reservations[i].DateTo)
                {
                    message = "You have " + (5 - DateTime.DaysInMonth(dateTimeNow.Year, dateTimeNow.Month) - (dateTimeNow.Day - reservations[i].DateTo.Day)).ToString() + " days left to grade " + reservations[i].GuestUserName;
                }
                else
                {
                    message = "You have " + (5 - (dateTimeNow.Day - reservations[i].DateTo.Day)).ToString() + " days left to grade " + reservations[i].GuestUserName;
                }
            }
            return message;
        }

        public GuestGrade Save(GuestGrade grade)
        {
            _grades = _serializer.FromCSV(FilePath);
            _grades.Add(grade);
            _serializer.ToCSV(FilePath, _grades);
            return grade;
        }

        public List<GuestGrade> GetAllGradesForGuest(string guestUsername)
        {
            List<GuestGrade> guestGrades = _grades.FindAll(g => g.GuestUserName == guestUsername);
            List<GuestGrade> filteredGrades = new List<GuestGrade>();

            foreach (GuestGrade grade in guestGrades)
            {
                Reservation reservation = reservationRepository.GetReservationById(grade.ReservationId);
                AccommodationReview accommodationReview = _accommodationReviewRepository.GetReviewByReservationId(grade.ReservationId);

                if (reservation != null && accommodationReview != null && reservation.GradeStatus == "Graded")
                {
                    filteredGrades.Add(grade);
                }
            }

            return filteredGrades;
        }
    }


}

