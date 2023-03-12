using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Tour
    {
        public string Name { get; set; }

        public int Id { get; set; }


        public Location Location { get; set; }

        public string Description { get; set; }

        Language Language { set; get; }

        public int MaxGuests { get; set; }

        public KeyPoint KeyPoint { get; set; }

        public DateTime Start { get; set; }

        public int Duration { get; set; }



        public Tour(string name, int id, Location location, string description, Language language, int maxGuests, KeyPoint keyPoint, DateTime start, int duration)
        {
            Name = name;
            Id = id;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            KeyPoint = keyPoint;
            Start = start;
            Duration = duration;
        }

        public Tour()
        {
        }





        //fale slike



    }
}
