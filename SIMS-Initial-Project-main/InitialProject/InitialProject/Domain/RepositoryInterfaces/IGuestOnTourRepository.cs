using System;
using System.Collections.Generic;
using InitialProject.Domain.Model;
using InitialProject.Serializer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Models;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IGuestOnTourRepository
    {

        public List<GuestOnTour> GetAllGuestsOnTour();

        public GuestOnTour GetGuestById(int id);

        public List<GuestOnTour> GetGuestByKeyPointId(int id);

        public string GetGuestStatusByTourId(int id);

        public GuestOnTour Update(GuestOnTour guestOnTour);

        public List<GuestOnTour> GetGuestByTourId(int id);

        public GuestOnTour Save(GuestOnTour guest);

        public int NextId();


    }
}
