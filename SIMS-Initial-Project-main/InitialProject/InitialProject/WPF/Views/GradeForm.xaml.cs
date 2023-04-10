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
using System.Windows.Shapes;
using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Repository;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for GradeForm.xaml
    /// </summary>
    public partial class GradeForm : Window
    {


        GuestGrade NewGrade = new GuestGrade();

        private readonly GradeGuestRepository _repository;
        private readonly ReservationRepository reservationRepository;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public GradeForm()
        {
            InitializeComponent();
            Title = "Grade guest";
            DataContext = this;
            _repository = new GradeGuestRepository();
            reservationRepository = new ReservationRepository();
        }

        private void GuestLoaded(object sender, RoutedEventArgs e)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.GetAll();

            for (int i = 0; i < reservations.Count; i++)
            {
                DateTime currentDate = DateTime.Now;
                if (reservations[i].DateTo < currentDate || reservations[i].DateTo.AddDays(5) > currentDate)
                {
                    GuestsCB.Items.Add(reservations[i].GuestId.ToString());
                }
            }
        }

         private void Clean(object sender, RoutedEventArgs e)
        {
            CB1.Items.Add("1");
            CB1.Items.Add("2");
            CB1.Items.Add("3");
            CB1.Items.Add("4");
            CB1.Items.Add("5");
        }

        private void Respect(object sender, RoutedEventArgs e)
        {
            CB2.Items.Add("1");
            CB2.Items.Add("2");
            CB2.Items.Add("3");
            CB2.Items.Add("4");
            CB2.Items.Add("5");
        }

        private void SaveGrade_Click(object sender, RoutedEventArgs e)
        {

            GuestGrade newGrade = new GuestGrade(
                GuestsCB.Text,
                Convert.ToInt32(CB1.Text),
                Convert.ToInt32(CB2.Text),
                CommentText.Text);
            _repository.Save(newGrade);

            CommentText.Clear();
            object selectedItem = GuestsCB.SelectedItem;
            if (GuestsCB.Items.Contains(selectedItem))
            {
                GuestsCB.Items.Remove(selectedItem);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.GetAll();

            for (int i = 0; i < reservations.Count; i++)
            {
                DateTime currentDate = DateTime.Now;
                if (reservations[i].DateTo < currentDate || reservations[i].DateTo.AddDays(5) > currentDate)
                {
                    MessageBox.Show("There are " + (5 - (currentDate.Day - reservations[i].DateTo.Day)).ToString() + " days left to grade guest " + reservations[i].GuestId);
                }
            }
        }

    }
}
