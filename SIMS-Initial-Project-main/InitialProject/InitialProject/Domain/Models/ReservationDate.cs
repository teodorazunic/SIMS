using System;
using System.Xml.Linq;

namespace InitialProject.Domain.Model
{
    public class ReservationDate
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public string StringFormat => $"{DateFrom:dd. MMMM yyyy.} - {DateTo:dd. MMMM yyyy.}";

        public ReservationDate(DateTime dateFrom, DateTime dateTo)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public ReservationDate() { }
    }
}
