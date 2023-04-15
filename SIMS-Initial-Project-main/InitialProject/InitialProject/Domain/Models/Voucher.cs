using System;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class Voucher : ISerializable
    {
        public int VoucherId { get; set; }
        
        public string Title { get; set; }

        public DateTime ValidUntil { get; set; }

        public int GuideId { get; set; }

        public int GuestId { get; set; }

        public Voucher() { }

        public Voucher(int voucherId, string title, DateTime validUntil, int guideId, int guestId)
        {
            VoucherId = voucherId;
            Title = title;
            ValidUntil = validUntil;
            GuideId = guideId;
            GuestId = guestId;
        }

        public void FromCSV(string[] values)
        {
            VoucherId = Convert.ToInt32(values[0]);
            Title = values[1];
            ValidUntil = Convert.ToDateTime(values[2]);
            GuideId = Convert.ToInt32(values[3]);
            GuestId = Convert.ToInt32(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { VoucherId.ToString(), Title, ValidUntil.ToString("dd-MMM-y HH:mm:ss tt"),GuideId.ToString(), GuestId.ToString() };
            return csvValues;
        }
    }
}
