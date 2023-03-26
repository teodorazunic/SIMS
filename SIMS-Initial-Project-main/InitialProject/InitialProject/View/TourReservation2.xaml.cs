using InitialProject.Model;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourReservation2.xaml
    /// </summary>
    public partial class TourReservation2 : Window
    {
        public static ObservableCollection<Tour> Tours { get; set; }

        private readonly TourRepository _repository;

        public static Tour SelectedTour { get; set; }
        public User LoggedInUser { get; set; }
        private int _guestsNumber = 0;

        public int GuestsNumber
        {
            get => _guestsNumber;
            set
            {
                if (value != _guestsNumber)
                {
                    _guestsNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TourReservation2(int id, User LoggedUser)
        {
            DataContext = this;
            InitializeComponent();
            _repository = new TourRepository();
            Tours = new ObservableCollection<Tour>(_repository.GetAllTours());
            SelectedTour = _repository.GetTourById(id);

        }

        private void CheckGuests(object sender, RoutedEventArgs e)
        {
            string message = _repository.CheckMaxGuests(SelectedTour.Id, GuestsNumber);

            if (message == "You were added to the tour!")
            {
                _repository.UpdateMaxGuests(SelectedTour.Id, GuestsNumber);
                MessageBox.Show(message);
                
            }
            else
            {
                MessageBox.Show(message);
            }
        }
    }
}
