using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class Tour : ISerializable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Location Location { get; set; }

        public string Description { get; set; }

        public Language Language { get; set; }

        public int MaxGuests { get; set; }

        public DateTime Start { get; set; }

        public int Duration { get; set; }

        public string Image { get; set; }

        public List <KeyPoint> KeyPoints { get; set; }

        public int GuideId { get; set; }

        public string Status { get; set; }

        public Tour( int id, string name, Location location, string description, Language language, int maxGuests, DateTime start, int duration, string image, string status)
        {
            Id = id;
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            Start = start;
            Duration = duration;
            Image = image;
            Status = status;
        }

        public Tour(int id, string name, Location location, string description, Language language, int maxGuests, DateTime start, int duration, string image, List<KeyPoint> keyPoints, int guideId)
        {
            Id = id;
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            Start = start;
            Duration = duration;
            Image = image;
            KeyPoints = keyPoints;
            GuideId = guideId;
        }

        public Tour(){}

        public string[] ToCSV()
        {
            string[] csvValues = {Id.ToString(), Name, Location.City, Location.Country, Description, Language.Name, MaxGuests.ToString(), Start.ToString("dd-MMM-y HH:mm:ss tt"), Duration.ToString(), Image, Status};
            return csvValues;
        }



        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = new Location() { City = values[2], Country = values[3] };
            Description = values[4];
            Language = new Language() { Name = values[5] };
            MaxGuests = Convert.ToInt32(values[6]);
            Start = Convert.ToDateTime(values[7]);
            Duration = Convert.ToInt32(values[8]);
            Image = values[9];
            Status = values[10];
        }

    }
}
