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
        public string City { get; set; }
        public string Country { get; set; }
        public AccommodationType Type { get; set; }
        public int GuestsNumber { get; set; }
        public int ReservationDays { get; set; }

        public int CancellationDeadlineDays { get; set; }





        public Accommodation() { }

        public Accommodation(string name, string city, string country, AccommodationType type, int guestsNumber, int reservationDays, int cancellationDeadlineDays)
        {
            Name = name;
            City = city;
            Country = country;
            Type = type;
            GuestsNumber = guestsNumber;
            ReservationDays = reservationDays;
            CancellationDeadlineDays = cancellationDeadlineDays;


        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, City, Country, Type.ToString(), GuestsNumber.ToString(), ReservationDays.ToString(), CancellationDeadlineDays.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            City = values[2];
            Country = values[3];
            Type = (AccommodationType) Enum.Parse(typeof(AccommodationType), values[4], false);
            GuestsNumber = Convert.ToInt32(values[5]);
            ReservationDays = Convert.ToInt32(values[6]);
            CancellationDeadlineDays = Convert.ToInt32(values[7]);

        }
    }
}
