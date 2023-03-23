using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace InitialProject.Repository
{
    public class TourRepository
    {
        private const string FilePath = "../../../Resources/Data/tour.csv";

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);

        }

        public List<Tour> GetTodaysTours(string filename)
        {
            List<Tour> allTours = ReadFromToursCsv(filename);
            List<Tour> tours = new List<Tour>();
            DateTime dateTime = DateTime.Today;

            for (int i = 0; i < allTours.Count; i++)
            {
                if (allTours[i].Start == dateTime)
                {
                    Tour tour = allTours[i];
                    tours.Add(tour);
                }
            }
            return tours;
        }

        public List<Tour> ReadFromToursCsv(string filename)
        {
            List<Tour> tours = new List<Tour>();

            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    Tour tour = new Tour();
                    tour.Id = Convert.ToInt32(fields[0]);
                    tour.Name = fields[1];
                    tour.Location = new Location() { City = fields[2], Country = fields[3] };
                    tour.Description = fields[4];
                    tour.Language = new Language() { Name = fields[5] };
                    tour.MaxGuests = Convert.ToInt32(fields[6]);
                    tour.KeyPoint = new KeyPoint() { Atrraction = fields[7] };
                    tour.Start = Convert.ToDateTime(fields[8]);
                    tour.Duration = Convert.ToInt32(fields[9]);
                    tours.Add(tour);


                }
            }
            return tours;
        }


        public List<Tour> GetAllTours()
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours;
        }

        public Tour Save(Tour tour)
        {
            tour.Id = NextId();
            _tours = _serializer.FromCSV(FilePath);
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }

        public int NextId()
        {
            _tours = _serializer.FromCSV(FilePath);
            if (_tours.Count < 1)
            {
                return 1;
            }
            return _tours.Max(t => t.Id) + 1;
        }

    }
}
