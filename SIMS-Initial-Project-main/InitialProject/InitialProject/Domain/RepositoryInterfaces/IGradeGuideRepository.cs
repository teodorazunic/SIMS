using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Models;

namespace InitialProject.Domain.RepositoryInterfaces
{
    internal interface IGradeGuideRepository
    {

        public List<GradeGuide> GetGradeByGuestId(int id);

        public int NextId();

        public GradeGuide Update(GradeGuide gradeGuide);

        public GradeGuide SaveGrade(GradeGuide gradeGuide);

        public List<TourReservations> ShowToursForGrading(int GuestId);

        public List<GradeGuide> GetAll();
    }
}
