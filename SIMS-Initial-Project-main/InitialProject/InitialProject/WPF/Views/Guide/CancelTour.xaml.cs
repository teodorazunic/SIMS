using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Forms;
using InitialProject.Repository;
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

namespace InitialProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for CancelTour.xaml
    /// </summary>
    public partial class CancelTour : Window
    {
        //public static ObservableCollection<Tour> Tours { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public CancelTour(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new TourRepository();
            //Tours = new ObservableCollection<Tour>(_repository.GetPendingTours(FilePath));
            Tours.ItemsSource = _repository.GetPendingTours();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedTour != null)
            {
                string message = _repository.CancelTour(_selectedTour);

                MessageBox.Show(message);
                 
                
            }
            Tours.Items.Refresh();
        }
    }
}
