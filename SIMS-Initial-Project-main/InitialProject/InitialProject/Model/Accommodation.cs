using InitialProject.Serializer;
using System;
using System.Linq;

namespace InitialProject.Model
{
    public enum AccommodationType
    {
        apartment,
        house,
        cottage
    }
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public AccommodationType Type { get; set; }
        public int GuestsNumber { get; set; }
        public int ReservationDays { get; set; }


        public Accommodation() { }

        public Accommodation(string name, Location location, AccommodationType type, int guestsNumber, int reservationDays)
        {
            Name = name;
            Location = location;
            Type = type;
            GuestsNumber = guestsNumber;
            ReservationDays = reservationDays;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Location.ToString(), Type.ToString(), GuestsNumber.ToString(), ReservationDays.ToString() };
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

        }
    }
}
