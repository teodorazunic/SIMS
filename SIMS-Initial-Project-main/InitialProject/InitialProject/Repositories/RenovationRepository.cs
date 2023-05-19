using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using InitialProject.WPF.Views.Owner;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace InitialProject.Repositories
{
    public class RenovationRepository
    {
        private const string FilePath = "../../../Resources/Data/renovations.csv";

        private readonly Serializer<Renovation> _serializer;
        private List<Renovation> _renovations;

    

        public RenovationRepository()
        {
            _serializer = new Serializer<Renovation>();
            _renovations = _serializer.FromCSV(FilePath);
        }


        public List<Renovation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Renovation Save(Renovation renovation)
        {
            renovation.Id = NextId();
            _renovations = _serializer.FromCSV(FilePath);
            _renovations.Add(renovation);
            _serializer.ToCSV(FilePath, _renovations);
            return renovation;
        }

        public int NextId()
        {
            _renovations = _serializer.FromCSV(FilePath);
            if (_renovations.Count < 1)
            {
                return 1;
            }
            return _renovations.Max(c => c.Id) + 1;
        }

        public void Delete(Renovation renovation)
        {
            _renovations = _serializer.FromCSV(FilePath);
            Renovation founded = _renovations.Find(r => r.Id == renovation.Id);
            _renovations.Remove(founded);
            _serializer.ToCSV(FilePath, _renovations);
        }

        public Renovation Update(Renovation renovaton)
        {
            _renovations = _serializer.FromCSV(FilePath);
            Renovation current = _renovations.Find(r => r.Id == renovaton.Id);
            int index = _renovations.IndexOf(current);
            _renovations.Remove(current);
            _renovations.Insert(index, renovaton);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _renovations);
            return renovaton;
        }


        public List<Renovation> ReadFromReservationsCsv(string FileName)
        {
            List<Renovation> reservations = new List<Renovation>();

            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    Renovation renovation= new Renovation();
                    renovation.Id = Convert.ToInt32(fields[0]);
                    renovation.Accommodation.Id = Convert.ToInt32(fields[1]);
                    renovation.StartDate = Convert.ToDateTime(fields[2]);
                    renovation.EndDate = Convert.ToDateTime(fields[3]);
                    reservations.Add(renovation);

                }
            }
            return reservations;
        }




    }


}

