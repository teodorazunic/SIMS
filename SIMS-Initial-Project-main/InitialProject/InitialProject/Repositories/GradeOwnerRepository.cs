using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Model;

namespace InitialProject.Repository
{
    internal class GradeOwnerRepository
    {
        private const string FilePath = "../../../Resources/Data/ownergrades.csv";
        private readonly ReservationRepository reservationRepository;
        private readonly AccommodationReviewRepository accommodationReviewRepository;
        private List<GradeOwner> ownerGrades;

        public GradeOwnerRepository()
        {
            reservationRepository = new ReservationRepository();
            ownerGrades = GetAll();
            accommodationReviewRepository = new AccommodationReviewRepository();
        }

        public List<GradeOwner> GetAll()
        {
            List<GradeOwner> grades = new List<GradeOwner>();
            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    GradeOwner grade = new GradeOwner();
                    grade.GuestUsername = fields[0];
                    grade.OwnerUsername = fields[1];
                    grade.ReservationId = Convert.ToInt32(fields[2]);
                    grade.HotelRating = Convert.ToInt32(fields[3]);
                    grade.OwnerRating = Convert.ToInt32(fields[4]);
                    grade.Comment = fields[5];
                    grades.Add(grade);
                }
            }
            return grades;
        }


        public int CountGradesFromOwnerRating()
        {
            int count = 0;
            List<AccommodationReview> grades = accommodationReviewRepository.GetAll();
            foreach (AccommodationReview grade in grades)
            {
                    count++;
            }
            return count;
        }
        public int GetAverageOwnerRating()
        {
            int Grade = 0;
            List<AccommodationReview> grades = accommodationReviewRepository.GetAll();
            foreach (AccommodationReview grade in grades)
            {
                    Grade = Grade + grade.Staff;
            }
            return Grade / CountGradesFromOwnerRating();
        }

        public string SuperOwner()
        {
            if (CountGradesFromOwnerRating() >= 50)
            {
                if (GetAverageOwnerRating() < 9.5)
                {
                    return "OWNER Pera";
                }
                else
                {
                    return "***SUPER-OWNER*** Pera";
                }
            }
            else
            {
                return "OWNER Pera";
            }

        }
        public List<AccommodationReview> ShowReviewsForOwner()
        {
            List<Reservation> allReservation = reservationRepository.GetAll();
            List<AccommodationReview> ownerGrades = new List<AccommodationReview>();
            foreach (Reservation reservation in allReservation)
            {
                if (reservation.GradeStatus == "Graded")
                {
                    if (IsOwnerGradeExists(reservation.Id))
                    {
                        ownerGrades.Add(FindOwnerGradeByReservationId(reservation.Id));
                    }
                }
            }
            return ownerGrades;
        }

        internal AccommodationReview FindOwnerGradeByReservationId(int id)
        {
            List<AccommodationReview> grades = accommodationReviewRepository.GetAll();
           foreach (AccommodationReview grade in grades)
            {
                if (grade.ReservationId == id)
                {
                    return grade;
                }
            }
            return null;
        }

        public bool IsOwnerGradeExists(int id)
        {
            List<AccommodationReview> grades = accommodationReviewRepository.GetAll(); 
            foreach (AccommodationReview grade in grades)
            {
                if (grade.ReservationId == id)
                {
                    return true;
                }
            }
            return false;
        }

        public GradeOwner GetByReservationId(int id)
        {
            foreach (GradeOwner grade in ownerGrades)
            {
                if (grade.ReservationId == id)
                {
                    return grade;
                }
            }
            return null;
        }
    }
}

