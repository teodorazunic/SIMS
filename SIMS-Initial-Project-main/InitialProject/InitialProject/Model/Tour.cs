using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Tour : ISerializable 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Location Location { get; set; }

        public string Description { get; set; }

        public Language Language { get; set; }

        public int MaxGuests { get; set; }

        public KeyPoint KeyPoint { get; set; }

        public DateTime Start { get; set; }

        public int Duration { get; set; }

        public string Image { get; set; }

        public Tour( int id, string name, Location location, string description, Language language, int maxGuests, KeyPoint keyPoint, DateTime start, int duration, string image)
        {
            Id = id;
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            KeyPoint = keyPoint;
            Start = start;
            Duration = duration;
            Image = image;
        }

        public Tour(){}

        public string[] ToCSV()
        {
            string[] csvValues = {Id.ToString(), Name, Location.City, Location.Country, Description, Language.Name, MaxGuests.ToString(), KeyPoint.Atrraction, Start.ToString("d/M/yyyy HH:mm"), Duration.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = new Location() {City = values[2], Country = values[3]};
            Description = values[4];
            Language = new Language() { Name = values[5] };
            MaxGuests = Convert.ToInt32(values[6]);
            KeyPoint = new KeyPoint() { Atrraction = values[7] };
            Start = Convert.ToDateTime(values[8]);
            Duration = Convert.ToInt32(values[9]);
        }


        //fale slike


    }
}
