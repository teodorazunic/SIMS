using InitialProject.Domain.Models;
using InitialProject.Repository;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repositories
{
    internal class GuestOnTourRepository
    {
        private const string FilePath = "../../../Resources/Data/guestontour.csv";

        private readonly Serializer<GuestOnTour> _serializer;

        private KeyPointRepository _keyPointRepository;

        private TourRepository _tourRepository;

        private GradeGuideRepository _gradeGuideRepository;

        private List<GuestOnTour> _guestsOnTour;

        public GuestOnTourRepository()
        {
            _serializer = new Serializer<GuestOnTour>();
            _guestsOnTour = _serializer.FromCSV(FilePath);
            _keyPointRepository = new KeyPointRepository();
            _tourRepository = new TourRepository();
            _gradeGuideRepository = new GradeGuideRepository();

        }

        public List<GuestOnTour> GetAllGuestsOnTour()
        {
            _guestsOnTour = _serializer.FromCSV(FilePath);
            return _guestsOnTour;
        }

        public GuestOnTour GetGuestById(int id)
        {
            _guestsOnTour = _serializer.FromCSV(FilePath);
            return _guestsOnTour.Find(t => t.Id == id);
        }

        public List<GuestOnTour> GetGuestByKeyPointId(int id)
        {
            KeyPoint keyPoint = _keyPointRepository.GetKeyPointById(id);
            _guestsOnTour = _serializer.FromCSV(FilePath);
            List<GuestOnTour> sameIdGuests = new List<GuestOnTour>();

            foreach (GuestOnTour gt in _guestsOnTour)
            {
                if (id == gt.StartingKeyPoint.Id)
                {
                    sameIdGuests.Add(gt);
                }

            }
            return sameIdGuests;
        }

        public List<GuestOnTour> GetGuestByTourId(int id)
        {
            Tour tour = _tourRepository.GetTourById(id);
            _guestsOnTour = _serializer.FromCSV(FilePath);
            List<GuestOnTour> sameIdGuests = new List<GuestOnTour>();

            foreach (GuestOnTour gt in _guestsOnTour)
            {
                if (id == gt.StartingKeyPoint.TourId)
                {
                    sameIdGuests.Add(gt);
                }

            }
            return sameIdGuests;
        }
    }
}
