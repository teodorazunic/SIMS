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
        public Voucher GetVoucherById(int voucherId);

        public List<Voucher> GetVoucherByGuestId(int guestId);

        public Voucher GetVoucherByGuideId(int guideId);

        public void DeleteVoucher();

        public int NextId();
        
        public List<Voucher> GetAllVouchers();
    }
}
