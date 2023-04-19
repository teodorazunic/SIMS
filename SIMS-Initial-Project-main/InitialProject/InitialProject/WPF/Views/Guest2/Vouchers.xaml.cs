using InitialProject.Domain.Models;
using InitialProject.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace InitialProject.WPF.Views.Guest2
{
    /// <summary>
    /// Interaction logic for Vouchers.xaml
    /// </summary>
    public partial class Vouchers : Window
    {
        public ObservableCollection<Voucher> AvailableVouchers { get; set; }
        private readonly VoucherRepository _repository;

        public User LoggedInUser { get; set; }

        private string _title = "";
        private DateTime _validUntil = DateTime.MinValue;

        public string Title
        {
            get => _title;
            set
            {
                if (value != _title)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime ValidUntil
        {
            get => _validUntil;
            set
            {
                if (value != _validUntil)
                {
                    _validUntil = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Vouchers(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new VoucherRepository();
            AvailableVouchers = new ObservableCollection<Voucher>(_repository.GetVoucherByGuestId(LoggedInUser.Id));
            //dbVouchers.ItemsSource = _repository.GetVoucherByGuestId(LoggedInUser.Id);
           
        }
    }
}
