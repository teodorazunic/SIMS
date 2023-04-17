using InitialProject.Domain.Models;
using InitialProject.Serializer;
using System;
using System.Linq;

namespace InitialProject.Domain.Model
{
    public enum AccommodationType
    {
        apartment,
        house,
        cottage
    }
    public class Accommodation : ISerializable
    {
        int max;
        int min;
        int cancelDays;
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public AccommodationType Type { get; set; }
        public int GuestsNumber { get; set; }
        public int ReservationDays { get; set; }
        public int CancellationDeadlineDays { get; set; }
        public string PictureUrl { get; set; }

        public int OwnerId { get; set; }

        public Accommodation() { }

        public Accommodation(string name, Location location, AccommodationType type, int guestsNumber, int reservationDays, int cancellationDeadlineDays, string picture, int ownerId)

        {
            Name = name;
            Location = location;
            Type = type;
            GuestsNumber = guestsNumber;
            ReservationDays = reservationDays;
            CancellationDeadlineDays = cancellationDeadlineDays;
            PictureUrl = picture;
            OwnerId = ownerId;
        }

        public Accommodation(string name, Location location, AccommodationType type, int max, int min, int cancelDays)
        {
            Name = name;
            Location = location;
            Type = type;
            this.max = max;
            this.min = min;
            this.cancelDays = cancelDays;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Location.City, Location.Country, Type.ToString(), GuestsNumber.ToString(), ReservationDays.ToString(), CancellationDeadlineDays.ToString(), PictureUrl };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = new Location() { City = values[2], Country = values[3] };
            Type = (AccommodationType)Enum.Parse(typeof(AccommodationType), values[4], false);
            GuestsNumber = Convert.ToInt32(values[5]);
            ReservationDays = Convert.ToInt32(values[6]);
            CancellationDeadlineDays = Convert.ToInt32(values[7]);
            OwnerId = Convert.ToInt32(values[8]);
            //PictureUrl = values[9];
        }
    }
}
