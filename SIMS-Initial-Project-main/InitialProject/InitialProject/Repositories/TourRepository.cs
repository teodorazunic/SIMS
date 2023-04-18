using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Forms;
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

        public List<Tour> GetPendingTours(string filename)
        {
            List<Tour> allTours = ReadFromToursCsv(filename);
            List<Tour> tours = new List<Tour>();

            foreach (Tour tour in allTours)
            {
                if (tour.Status == "Pending")
                {
                    tours.Add(tour);
                }
            }
            return tours;
        }

        public List<Tour> GetEndedTours(string filename)
        {
            List<Tour> allTours = ReadFromToursCsv(FilePath);
            List<Tour> tours = new List<Tour>();

            foreach (Tour tour in allTours)
            {
                if (tour.Status == "Ended")
                {
                    tours.Add(tour);
                }
            }
            return tours;
        }

        public string CancelTour(Tour tour)
        {
            DateTime currentDate = DateTime.Now;
            
            if ((tour.Start -currentDate).TotalHours < 48)
            {
                return "Unable to cancel tour";
            }

            tour.Status = "Ended";
            Update(tour);
            _serializer.ToCSV(FilePath, _tours);
            return "Succesfully canceled tour!";
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
                    tour.Start = Convert.ToDateTime(fields[7]);
                    tour.Duration = Convert.ToInt32(fields[8]);
                    tour.Image= fields[9];
                    tour.Status = fields[10];
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
        
         public Tour GetTourById(int id)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.Find(t => t.Id == id);
        }
        
        public Tour GetTourByCity(string city, int id)
        {
            _tours = _serializer.FromCSV(FilePath);

            Tour tour = GetTourById(id);
            if (tour != null && tour.Location.City == city && tour.MaxGuests > 0)
            {
                return tour;
            }

            return _tours.FirstOrDefault(t => t.Location.City == city && t.MaxGuests > 0 && t.Id != id);
        }

        public void UpdateMaxGuests(int id, int guests)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour tour = GetTourById(id);

            if (tour != null)
            {
                tour.MaxGuests = tour.MaxGuests - guests;
                _serializer.ToCSV(FilePath, _tours);
            }
        }

        public string CheckMaxGuests(int id, int guests)
        {
            Tour tour = GetTourById(id);
            if(guests > tour.MaxGuests)
            {
                return "Number of atendees exceeds max guests number";
            }
            else
            {
                return "You were added to the tour!";
            }
        }
        
        public List<Tour> SearchTour(string name, string city, string country, int duration, string language, int numberOfPeople)
        {
            _tours = _serializer.FromCSV(FilePath);

            List<Tour> filteredTours = _tours;


            if (name != null && name != "")
            {
                filteredTours = filteredTours.FindAll(t => t.Name.Contains(name));
            }

            if (city != null && city != "")
            {
                filteredTours = filteredTours.FindAll(t => t.Location.City == city);
            }

            if (country != null && country != "")
            {
                filteredTours = filteredTours.FindAll(t => t.Location.Country == country);
            }

            if (duration > 0)
            {
                filteredTours = filteredTours.FindAll(t => t.Duration == duration);
            }

            if (language != null && language != "")
            {
                filteredTours = filteredTours.FindAll(t => t.Language.Name == language);
            }

            if (numberOfPeople > 0)
            {
                filteredTours = filteredTours.FindAll(t => t.MaxGuests >= numberOfPeople);
            }

            return filteredTours;
        }

        public bool IsStarted()
        {
            int countStartedTours = 0;
            List<Tour> tours = ReadFromToursCsv(FilePath);
            foreach (Tour tour in tours)
            {
                if (tour.Status == "Started")
                    countStartedTours++;
            }
            if (countStartedTours != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

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

        public Tour Update(Tour tour)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour current = _tours.Find(c => c.Id == tour.Id);
            int index = _tours.IndexOf(current);
            _tours.Remove(current);
            _tours.Insert(index, tour);
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }

    
    }
}
