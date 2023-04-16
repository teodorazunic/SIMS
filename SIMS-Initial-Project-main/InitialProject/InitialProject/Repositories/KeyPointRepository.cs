using InitialProject.Domain.Models;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    class KeyPointRepository
    {
        private const string FilePath = "../../../Resources/Data/keypoints.csv";

        private readonly Serializer<KeyPoint> _serializer;

        private TourRepository _tourRepository;

        private List<KeyPoint> _keyPoints;

        

        public KeyPointRepository()
        {
            _serializer = new Serializer<KeyPoint>();
            _tourRepository = new TourRepository();
            _keyPoints = _serializer.FromCSV(FilePath);
        }

        public string Activate(KeyPoint keyPoint)
        {
            if (keyPoint.Status == "Active")
            {
                return "Kljucna tacka je vec aktivna";
            }

            keyPoint.Status = "Active";
            Update(keyPoint);
            _serializer.ToCSV(FilePath, _keyPoints);
            return "Uspesno aktivirana kljucna tacka!";
        }

        public KeyPoint Update(KeyPoint keyPoint)
        {
            _keyPoints = _serializer.FromCSV(FilePath);
            KeyPoint current = _keyPoints.Find(c => c.Id == keyPoint.Id);
            int index = _keyPoints.IndexOf(current);
            _keyPoints.Remove(current);
            _keyPoints.Insert(index, keyPoint);
            _serializer.ToCSV(FilePath, _keyPoints);
            return keyPoint;
        }

        public int getId()
        {
            _keyPoints = this.GetAllKeyPoints();
            return _keyPoints.Max(keyPoint => keyPoint.Id);
        }

        public KeyPoint GetKeyPointById(int id)
        {
            _keyPoints = _serializer.FromCSV(FilePath);
            return _keyPoints.Find(t => t.Id == id);
        }

        public List<KeyPoint> GetAllKeyPoints()
        {
            _keyPoints = _serializer.FromCSV(FilePath);
            return _keyPoints;
        }

        public List<KeyPoint> GetKeyPointbyTourId(int id)
        {
            Tour tour = _tourRepository.GetTourById(id);
            _keyPoints = _serializer.FromCSV(FilePath);
            List<KeyPoint> sameIdKeyPoint = new List<KeyPoint>();
            
            foreach(KeyPoint kp in _keyPoints) {
                if (id == kp.TourId)
                {
                    sameIdKeyPoint.Add(kp);
                }

            }
            return sameIdKeyPoint;
        }

        public KeyPoint SaveKeyPoint(KeyPoint keyPoint)
        {

           int keyPointId = this.getId();
           keyPoint.Id = keyPointId + 1;

           _keyPoints = _serializer.FromCSV(FilePath);
            _keyPoints.Add(keyPoint);
            _serializer.ToCSV(FilePath, _keyPoints);
            return keyPoint;

        }

        public int NextId()
        {
            _keyPoints = _serializer.FromCSV(FilePath);
            if (_keyPoints.Count < 1)
            {
                return 1;
            }
            return _keyPoints.Max(t => t.Id) + 1;
        }


        public List<KeyPoint> ReadFromKeyPointsCsv(string FileName)
        {
            List<KeyPoint> keyPoints = new List<KeyPoint>();

            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    KeyPoint keyPoint = new KeyPoint();
                    keyPoint.Id = Convert.ToInt32(fields[0]);
                    keyPoint.Name = fields[1];
                    keyPoint.TourId = Convert.ToInt32(fields[2]);                       
                    keyPoints.Add(keyPoint);

                }
            }
            return keyPoints;
        }

    }
}
