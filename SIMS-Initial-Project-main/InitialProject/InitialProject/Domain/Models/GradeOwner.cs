using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Model
{
    public class GradeOwner : ISerializable
    {
        public string GuestUsername { get; set; }
        public string OwnerUsername { get; set; }
        public int ReservationId { get; set; }
        public int HotelRating { get; set; }
        public int OwnerRating { get; set; }
        public string Comment { get; set; }

        public GradeOwner() { }

        public GradeOwner(string guestUsername, string ownerUsername, int reservationId, int hotelRating, int ownerRating, string comment)
        {
            GuestUsername = guestUsername;
            OwnerUsername = ownerUsername;
            ReservationId = reservationId;
            HotelRating = hotelRating;
            OwnerRating = ownerRating;
            Comment = comment;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { GuestUsername, OwnerUsername, ReservationId.ToString(), HotelRating.ToString(), OwnerRating.ToString(), Comment };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            GuestUsername = values[0]; 
            OwnerUsername = values[1];
            ReservationId = Convert.ToInt32(values[2]);
            HotelRating = Convert.ToInt32(values[3]);
            OwnerRating = Convert.ToInt32(values[4]);
            Comment = values[5];
        }

    }


}

