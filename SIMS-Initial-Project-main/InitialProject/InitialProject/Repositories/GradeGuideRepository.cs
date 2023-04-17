using InitialProject.Domain.Models;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Serializer;
using InitialProject.WPF.Views.Guest2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Repositories
{
    internal class GradeGuideRepository
    {
        private const string FilePath = "../../../Resources/Data/guidegrades.csv";

        private List<GradeGuide> guideGrades;

        private readonly Serializer<GradeGuide> _serializer;

        private GuestOnTourRepository _guestOnTourRepository;

        private List<GradeGuide> _gradeGuide;

        private TourReservationRepositery _tourReservationRepositery;

        //private List<TourReservations> _tourReservations;

        //private TourRepository _tourRepository;

        private List<Tour> _tours;

        public GradeGuideRepository()
        {
            _serializer = new Serializer<GradeGuide>();
            _gradeGuide = _serializer.FromCSV(FilePath);

        }

        public List<GradeGuide> GetGradeByGuestId(int id)
        {
            
            List<GradeGuide> sameIdGrade = new List<GradeGuide>();
            List<GradeGuide> grades = GetAll();

            for (int i = 0; i < grades.Count; i++)
            {
                if (id == grades[i].GuestId)
                {
                    sameIdGrade.Add(grades[i]);
                }

            }
            return sameIdGrade;
        }

        public int NextId()
        {
            _gradeGuide = _serializer.FromCSV(FilePath);
            if (_gradeGuide.Count < 1)
            {
                return 1;
            }
            return _gradeGuide.Max(g => g.Id) + 1;
        }

        public GradeGuide Update(GradeGuide gradeGuide)
        {
            _gradeGuide = _serializer.FromCSV(FilePath);
            GradeGuide current = _gradeGuide.Find(c => c.Id == gradeGuide.Id);
            int index = _gradeGuide.IndexOf(current);
            _gradeGuide.Remove(current);
            _gradeGuide.Insert(index, gradeGuide);       
            _serializer.ToCSV(FilePath, _gradeGuide);
            return gradeGuide;
        }

        public GradeGuide SaveGrade(GradeGuide gradeGuide)
        {
            _gradeGuide = _serializer.FromCSV(FilePath);
            _gradeGuide.Add(gradeGuide);
            _serializer.ToCSV(FilePath, _gradeGuide);
            return gradeGuide;
        }

        public List<TourReservations> ShowToursForGrading(int GuestId)
        {
            _gradeGuide = _serializer.FromCSV(FilePath);
            List<TourReservations> tourReservations = new List<TourReservations>();
            _tourReservationRepositery = new TourReservationRepositery();
            tourReservations = _tourReservationRepositery.GetAllReservationsByGuestId(GuestId);
            tourReservations = _tourReservationRepositery.CheckReservedTourStatus();

            return tourReservations;
            
        }


        //public bool UpdateCheck(GradeGuide gradeGuide)
        //{
        //    int id = gradeGuide.GuestId;
        //    List<GuestOnTour> guests = _guestOnTourRepository.GetAllGuestsOnTour();
        //    Tour tour = new Tour();
        //    for(int i = 0; i<guests.Count; i++)
        //    {
        //        if (guests[i].GuestId == id)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public List<GradeGuide> GetAll()
        {
            List<GradeGuide> grades = new List<GradeGuide>();
            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    GradeGuide grade = new GradeGuide();
                    grade.Id = Convert.ToInt32(fields[0]);
                    grade.GuestId = Convert.ToInt32(fields[1]);
                    grade.GuideKnowledge = Convert.ToInt32(fields[2]);
                    grade.GuideLanguage = Convert.ToInt32(fields[3]);
                    grade.Overall = Convert.ToInt32(fields[4]);
                    grade.Comment = fields[5];
                    grades.Add(grade);
                }
            }
            return grades;
        }

    }
}
