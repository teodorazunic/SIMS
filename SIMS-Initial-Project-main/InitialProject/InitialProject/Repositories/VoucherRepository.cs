using System;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Models;

namespace InitialProject.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private const string FilePath = "../../../Resources/Data/vouchers.csv";

        private readonly Serializer<Voucher> _serializer;

        private List<Voucher> _vouchers;

        public VoucherRepository()
        {
            _serializer = new Serializer<Voucher>();
            _vouchers = _serializer.FromCSV(FilePath);

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
