using System;
using InitialProject.Domain.Models;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Repository;

namespace InitialProject.Repositories
{
    public class KeyPointsOnTourRepository
    {
        private const string FilePath = "../../../Resources/Data/keypointsontour.csv";

        private readonly Serializer<KeyPointsOnTour> _serializer;

        private List<KeyPointsOnTour> _keyPointsOnTour;

        public KeyPointsOnTourRepository()
        {
            _serializer = new Serializer<KeyPointsOnTour>();
            //_tourRepository = new TourRepository();
            _keyPointsOnTour = _serializer.FromCSV(FilePath);
        }

        public KeyPointsOnTour GetKeyPointById(int id)
        {
            _keyPointsOnTour = _serializer.FromCSV(FilePath);
            return _keyPointsOnTour.Find(t => t.TourId == id);
        }
    }
}
