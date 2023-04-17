using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Models;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IGradeGuestRepository
    {

        public string FindGuestsForGrade(int i);

        public void FindAndDeleteExpiredReservation(int i);

        public string ShowMessageForGrade(int i);

       




    }
}
