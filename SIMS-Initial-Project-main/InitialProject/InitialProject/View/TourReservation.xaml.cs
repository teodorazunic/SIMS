using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Serializer;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourReservation.xaml
    /// </summary>
    public partial class TourReservation : Window

    {
        public static Tour SelectedTour { get; set; }
        public readonly TourRepository _repository;
        private int _atendeeNumber = 0;
        private Serializer<Tour> serializer;

        public int AtendeeNumber
        {
            get => _atendeeNumber;
            set
            {
                if (value != _atendeeNumber)
                {
                    _atendeeNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        internal Serializer<Tour> Serializer { get => serializer; set => serializer = value; }

        public TourReservation()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new TourRepository();
            this.serializer = new Serializer<Tour>();
            LoadTours();
        }

        private void LoadTours()
        {
            List<Tour> tours = this.serializer.FromCSV("tours.csv");
            foreach (Tour tour in tours)
            {
                cmbTours.Items.Add($"{tour.Name} ({tour.Id})");
            }
        }


        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = cmbTours.SelectedItem as string;
            int Ids = 0;
            if (!string.IsNullOrEmpty(selectedItem))
            {
                int startIndex = selectedItem.IndexOf("(") + 1;
                int endIndex = selectedItem.IndexOf(")");
                if (startIndex > 0 && endIndex > 0 && endIndex > startIndex)
                {
                    string tourIdString = selectedItem.Substring(startIndex, endIndex - startIndex);
                    if (int.TryParse(tourIdString, out int tourId))
                    {
                        Ids = tourId;

                    }
                    SelectedTour = _repository.GetTourById(Ids);
                    if (int.TryParse(attendeesTextBox.Text, out int result))
                    {
                        _atendeeNumber = result;
                        OnPropertyChanged();
                        if (_atendeeNumber > SelectedTour.MaxGuests)
                        {
                            MessageBox.Show("There are more atendees than available slots.");
                        }
                        else
                        {
                            _repository.UpdateMaxGuests(SelectedTour.Id, _atendeeNumber);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid number.");
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
