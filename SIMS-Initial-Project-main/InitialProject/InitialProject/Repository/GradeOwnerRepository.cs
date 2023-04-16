﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;

namespace InitialProject.Repository
{
    internal class GradeOwnerRepository
    {
        private const string FilePath = "../../../Resources/Data/ownergrades.csv";
        private readonly ReservationRepository reservationRepository;

        private List<GradeOwner> ownerGrades;

        public GradeOwnerRepository()
        {
            reservationRepository = new ReservationRepository();
            ownerGrades = GetAll();
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


        public int CountGradesFromOwnerRating(string OwnerUserName)
        {
            int count = 0;
            List<GradeOwner> grades = GetAll();
            foreach (GradeOwner grade in grades)
            {
                if (grade.OwnerUsername == OwnerUserName)
                    count++;
            }
            return count;
        }
        public int GetAverageOwnerRating(string OwnerUserName)
        {
            int Grade = 0;
            List<GradeOwner> grades = GetAll();
            foreach (GradeOwner grade in grades)
            {
                if (grade.OwnerUsername == OwnerUserName)
                    Grade = Grade + grade.OwnerRating;
            }
            return Grade / CountGradesFromOwnerRating(OwnerUserName);
        }

        public string SuperOwner(string username)
        {
            if (CountGradesFromOwnerRating(username) >= 50)
            {
                if (GetAverageOwnerRating(username) < 9.5)
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
        public List<GradeOwner> ShowReviewsForOwner()
        {
            List<Reservation> allReservation = reservationRepository.GetAll();
            List<GradeOwner> ownerGrades = new List<GradeOwner>();
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

        internal GradeOwner FindOwnerGradeByReservationId(int id)
        {
            List<GradeOwner> grades = GetAll();
            foreach (GradeOwner grade in grades)
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
            List<GradeOwner> grades = GetAll();
            foreach (GradeOwner grade in grades)
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

