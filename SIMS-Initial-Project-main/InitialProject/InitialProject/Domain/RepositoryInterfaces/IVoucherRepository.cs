using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Models;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IVoucherRepository
    {
        public TourVoucher GetVoucherById(int voucherId);

        public List<TourVoucher> GetVoucherByGuestId(int guestId);

        public TourVoucher GetVoucherByGuideId(int guideId);

        public void DeleteExpiredVoucher();

        public void DeleteUsedVoucher(int voucherId);

        public int NextId();
        
        public List<TourVoucher> GetAllVouchers();
    }
}
