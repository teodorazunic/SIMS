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
    public interface IKeyPointRepository
    {
        public string Activate(KeyPoint keyPoint);

        public KeyPoint Update(KeyPoint keyPoint);

        public int getId();

        public KeyPoint GetKeyPointById(int id);

        public List<KeyPoint> GetAllKeyPoints();

        public List<KeyPoint> GetKeyPointbyTourId(int id);

        public KeyPoint SaveKeyPoint(KeyPoint keyPoint);

        public int NextId();

        public List<KeyPoint> ReadFromKeyPointsCsv(string FileName);

        public KeyPoint GetLastActiveKeyPoint(List<KeyPoint> keyPoints);


    }
}
