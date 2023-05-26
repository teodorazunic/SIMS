using InitialProject.Serializer;
using System;

namespace InitialProject.Domain.Models
{
    public class Forum: ISerializable
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public string Question { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsVeryUseful { get; set; }
        public int CreatorId { get; set; }

        public Forum(Location location, string question, bool isDeleted, bool isVeryUseful, int creatorId)
        {
            Location = location;
            Question = question;
            IsDeleted = isDeleted;
            IsVeryUseful = isVeryUseful;
            CreatorId = creatorId;
        }

        public Forum() { }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Location.City, Location.Country, Question, IsDeleted.ToString(), IsVeryUseful.ToString(), CreatorId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Location = new Location() { City = values[1], Country = values[2] };
            Question = values[3];
            IsDeleted = Convert.ToBoolean(values[4]);
            IsVeryUseful = Convert.ToBoolean(values[5]);
            CreatorId = Convert.ToInt32(values[6]);
        }
    }
}