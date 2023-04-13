using System;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class TourGuests : User
    {

        public int Age { get; set; }

        public List<Tour> ReservedTours { get; set; }

        public List<Voucher> AvailableVouchers { get; set; }
        
        public TourGuests() { }

        public TourGuests(int age, List<Tour> reservedTours, List<Voucher> availableVouchers)
        {
            Age = age;
            ReservedTours = reservedTours;
            AvailableVouchers = availableVouchers;
        }
    }
}
