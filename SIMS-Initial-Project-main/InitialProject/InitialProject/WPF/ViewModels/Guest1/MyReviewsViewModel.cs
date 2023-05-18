using InitialProject.Domain;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.Guest1
{
    public class MyReviewsViewModel
    {

        private readonly IGradeGuestRepository _repository;

        private List<GuestGrade> GuestGrades;

        public MyReviewsViewModel(User user)
        {
            _repository = Injector.CreateInstance<IGradeGuestRepository>();
            GuestGrades = _repository.GetAllGradesForGuest(user.Username);
        }

        public List<GuestGrade> ShowReviewsFromOwner() { return GuestGrades; }


    }
}
