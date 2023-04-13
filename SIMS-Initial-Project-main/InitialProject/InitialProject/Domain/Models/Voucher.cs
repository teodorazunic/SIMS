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
        public string Title { get; set; }

        public DateOnly ValidUntil { get; set; }

        public int GuideId { get; set; }

        public int GuestId { get; set; }

        public Voucher() { }

        public Voucher(string title, DateOnly validUntil, int guideId, int guestId)
        {
            Title = title;
            ValidUntil = validUntil;
            GuideId = guideId;
            GuestId = guestId;
        }

        public void FromCSV(string[] values)
        {
            throw new NotImplementedException();
        }

        public string[] ToCSV()
        {
            throw new NotImplementedException();
        }
    }
}
