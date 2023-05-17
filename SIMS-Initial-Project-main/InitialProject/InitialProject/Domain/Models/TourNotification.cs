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
    public class TourNotification : ISerializable
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public User GuestId = new User();

        public TourNotification() { }

        public TourNotification(int id, string text, User guestId) 
        {
            Id = id;
            Text = text;
            GuestId = guestId;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Text, GuestId.Id.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Text = values[1];
            GuestId = new User() { Id = Convert.ToInt32(values[2])};
        }
    }
}
