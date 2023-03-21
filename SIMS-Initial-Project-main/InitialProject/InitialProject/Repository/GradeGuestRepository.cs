using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;
using InitialProject.Serializer;

namespace InitialProject.Repository
{
    class GradeGuestRepository
    {
        private const string FilePath = "../../../Resources/Data/guestgrades.csv";

        private readonly Serializer<GuestGrade> _serializer;

        private List<GuestGrade> _grades;

        public GradeGuestRepository()
        {
            _serializer = new Serializer<GuestGrade>();
            _grades = _serializer.FromCSV(FilePath);
        }

        public GuestGrade Save(GuestGrade grade)
        {
            _grades = _serializer.FromCSV(FilePath);
            _grades.Add(grade);
            _serializer.ToCSV(FilePath, _grades);
            return grade;
        }
    }


}

