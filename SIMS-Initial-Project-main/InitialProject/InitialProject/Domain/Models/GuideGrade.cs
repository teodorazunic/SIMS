using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    internal class GuideGrade : Serializer.ISerializable
    {

        public int GuestId { get; set; }

        public string GuestName { get; set; }

        public int KeyPointId { get; set; }

        public int Grade { get; set; }

        public GuideGrade(int guestId, string guestName, int keyPointId, int grade)
        {
            GuestId = guestId;
            GuestName = guestName;
            KeyPointId = keyPointId;
            Grade = grade;
        }

        public void FromCSV(string[] values)
        {
            GuestId = Convert.ToInt32(values[0]);
            GuestName = values[1];
            KeyPointId = Convert.ToInt32(values[2]);
            Grade = Convert.ToInt32(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { GuestId.ToString(),GuestName, KeyPointId.ToString(), Grade.ToString() };
            return csvValues;
        }
    }
}
