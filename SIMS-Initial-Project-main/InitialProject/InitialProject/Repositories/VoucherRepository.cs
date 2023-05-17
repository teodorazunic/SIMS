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
using System.Xml.Linq;
using InitialProject.WPF.Views.Guest2;

namespace InitialProject.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private const string FilePath = "../../../Resources/Data/vouchers.csv";

        private readonly Serializer<TourVoucher> _serializer;

        private List<TourVoucher> _vouchers;

        private readonly GuestOnTourRepository _guestOnTourRepository;

        public VoucherRepository()
        {
            _serializer = new Serializer<TourVoucher>();
            _vouchers = _serializer.FromCSV(FilePath);
            _guestOnTourRepository = new GuestOnTourRepository();

        }

        public void DeleteExpiredVoucher()
        {
            _vouchers = _serializer.FromCSV(FilePath);
            List<TourVoucher> vouchers = new List<TourVoucher>(_vouchers); 
            List<TourVoucher> toDelete = new List<TourVoucher>(); 

            foreach (TourVoucher voucher in vouchers)
            {
                if (voucher.ValidUntil <= DateTime.Now)
                {
                    toDelete.Add(voucher); 
                }
            }

            foreach (TourVoucher voucher in toDelete)
            {
                _vouchers.Remove(voucher); 
            }

            _serializer.ToCSV(FilePath, _vouchers);
        }

        public void DeleteUsedVoucher(int voucherId)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            TourVoucher used = _vouchers.Find(v => v.VoucherId == voucherId);
            _vouchers.Remove(used);
            _serializer.ToCSV(FilePath, _vouchers);
        }

        public void SendVouchers(int id)
        {
            List<GuestOnTour> guests = _guestOnTourRepository.GetAllGuestsOnTour();
            for(int i = 0; i < guests.Count; i++)
            {
                if (guests[i].StartingKeyPoint.Tour.Id == id)
                {
                    TourVoucher voucher = new TourVoucher();
                    DateTime voucherDate = voucher.ValidUntil;
                    voucher.Title = "Vaucer za otkazanu turu.";
                    voucher.GuestId.Id = guests[i].GuestId;
                    voucher.ValidUntil = voucherDate.AddYears(1) ;
                    Save(voucher);
                }
            }
            
        }

        public TourVoucher Save(TourVoucher voucher)
        {
            voucher.VoucherId = NextId();
            _vouchers = _serializer.FromCSV(FilePath);
            _vouchers.Add(voucher);
            _serializer.ToCSV(FilePath, _vouchers);
            return voucher;
        }

        public List<TourVoucher> GetVoucherByGuestId(int guestId)
        {

            _vouchers = _serializer.FromCSV(FilePath);
            return _vouchers.FindAll(v => v.GuestId.Id == guestId);
        }

        public TourVoucher GetVoucherByGuideId(int guideId)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            return _vouchers.Find(v => v.GuideId.Id == guideId);
        }

        public TourVoucher GetVoucherById(int voucherId)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            return _vouchers.Find(v => v.VoucherId == voucherId);
        }
        
          public List<TourVoucher> GetAllVouchers()
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
