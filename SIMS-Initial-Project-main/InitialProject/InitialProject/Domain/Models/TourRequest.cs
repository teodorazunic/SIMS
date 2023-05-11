using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using InitialProject.Serializer;

namespace InitialProject.Domain.Models
{
    public class TourRequest : ISerializable
    {
        public int Id { get; set; }

        public int GuestId { get; set; }

        public Location Location { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }

        public int MaxGuests { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public TourRequest(int id, int guestId, Location location, string description, string language, int maxGuests, DateTime startDate, DateTime endDate, string status)
        {
            Id = id;
            GuestId = guestId;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            StartDate = startDate;
            EndDate = endDate;
            Status = "Pending";
        }

        public TourRequest(){ }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            Location = new Location() { City = values[2], Country = values[3] };
            Description = values[4];
            Language = values[5];
            MaxGuests = Convert.ToInt32(values[6]);
            StartDate = Convert.ToDateTime(values[7]);
            EndDate = Convert.ToDateTime(values[8]);
            Status = values[9];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestId.ToString(), Location.City, Location.Country, Description, Language, MaxGuests.ToString(), StartDate.ToString("dd-MMM-y HH:mm:ss tt"), EndDate.ToString("dd-MMM-y HH:mm:ss tt"), Status };
            return csvValues;
        }
    }
}
