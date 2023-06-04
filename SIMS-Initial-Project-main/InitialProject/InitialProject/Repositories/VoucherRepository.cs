using System;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Models;
using InitialProject.Repository;

namespace InitialProject.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private const string FilePath = "../../../Resources/Data/vouchers.csv";

        private readonly Serializer<Voucher> _serializer;

        private List<Voucher> _vouchers;

        private readonly GuestOnTourRepository _guestOnTourRepository;

        public VoucherRepository()
        {
            _serializer = new Serializer<Voucher>();
            _vouchers = _serializer.FromCSV(FilePath);
            _guestOnTourRepository = new GuestOnTourRepository();

        }

        public void DeleteVoucher()
        {
            _vouchers = _serializer.FromCSV(FilePath);
            List<Voucher> Vouchers = _vouchers;
            foreach (Voucher voucher in Vouchers)
            {
                if (voucher.ValidUntil == DateTime.Now)
                {
                    ;
                }
            }
        }

        public void SendVouchers(int id)
        {
            List<GuestOnTour> guests = _guestOnTourRepository.GetAllGuestsOnTour();
            for(int i = 0; i < guests.Count; i++)
            {
                if (guests[i].StartingKeyPoint.TourId == id)
                {
                    Voucher voucher = new Voucher();
                    DateTime now = DateTime.Today;
                    voucher.Title = "Vaucer za otkazanu turu.";
                    voucher.GuestId = guests[i].GuestId;
                    voucher.ValidUntil = now.AddYears(2) ;
                    Save(voucher);
                }
            }
            
        }

        public void SendVouchersForDissmisal(List<Tour> tours)
        {
            List<GuestOnTour> guests = _guestOnTourRepository.GetAllGuestsOnTour();
            for (int i = 0; i < guests.Count; i++)
            {
                foreach (Tour tour in tours)
                {
                    if (guests[i].StartingKeyPoint.TourId == tour.Id)
                    {
                        Voucher voucher = new Voucher();
                        DateTime now = DateTime.Today;
                        voucher.Title = "Vaucer za otkazanu turu.";
                        voucher.GuestId = guests[i].GuestId;
                        voucher.ValidUntil = now.AddYears(2);
                        Save(voucher);
                    }
                }
            }

        }

        public Voucher Save(Voucher voucher)
        {
            voucher.VoucherId = NextId();
            _vouchers = _serializer.FromCSV(FilePath);
            _vouchers.Add(voucher);
            _serializer.ToCSV(FilePath, _vouchers);
            return voucher;
        }

        public List<Voucher> GetVoucherByGuestId(int guestId)
        {

            _vouchers = _serializer.FromCSV(FilePath);
            return _vouchers.FindAll(v => v.GuestId == guestId);
        }

        public Voucher GetVoucherByGuideId(int guideId)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            return _vouchers.Find(v => v.GuideId == guideId);
        }

        public Voucher GetVoucherById(int voucherId)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            return _vouchers.Find(v => v.VoucherId == voucherId);
        }
        
          public List<Voucher> GetAllVouchers()
        {
            _vouchers = _serializer.FromCSV(FilePath);
            return _vouchers;
        }

        public int NextId()
        {
            _vouchers = _serializer.FromCSV(FilePath);
            if (_vouchers.Count < 1)
            {
                return 1;
            }
            return _vouchers.Max(v => v.VoucherId) + 1;
        }
    }
}
