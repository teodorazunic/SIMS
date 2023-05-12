using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.Views.Guide
{
    /// <summary>
    /// Interaction logic for Cancel.xaml
    /// </summary>
    public partial class Cancel : Page
    {
        public Cancel(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new TourRepository();
            _voucherRepository = new VoucherRepository();
            Tours.ItemsSource = _repository.GetPendingTours(FilePath);
        }
        private const string FilePath = "../../../Resources/Data/tour.csv";

        public User LoggedInUser { get; set; }

        private Tour _selectedTour;

        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {
                if (value != _selectedTour)
                {
                    _selectedTour = value;
                    OnPropertyChanged();
                }
            }
        }

        private readonly TourRepository _repository;

        private readonly VoucherRepository _voucherRepository;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTour != null)
            {
                string message = _repository.CancelTour(_selectedTour);
                _voucherRepository.SendVouchers(_selectedTour.Id);
                MessageBox.Show(message);

            }
            Tours.Items.Refresh();
        }
    }
}
