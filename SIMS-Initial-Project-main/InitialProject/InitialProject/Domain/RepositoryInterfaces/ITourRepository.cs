using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Serializer;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {

        public List<Tour> GetTodaysTours(string filename);

        public List<Tour> GetPendingTours(string filename);

        public List<Tour> GetEndedTours(string filename);

        public Tour GetStartedTour(List<Tour> tours);

        public string CancelTour(Tour tour);

        public List<Tour> ReadFromToursCsv(string filename);

        public List<Tour> GetAllTours();

        public Tour GetTourById(int id);

        public Tour GetTourByCity(string city, int id);

        public void UpdateMaxGuests(int id, int guests);

        public string CheckMaxGuests(int id, int guests);

        public List<Tour> SearchTour(string name, string city, string country, int duration, string language, int numberOfPeople);

        public bool IsStarted();

        public Tour Save(Tour tour);

        public int NextId();

        public Tour Update(Tour tour);


    }
}
