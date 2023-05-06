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
        private readonly GradeGuestRepository gradeGuestRepository;


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public GradeForm()
        {
            InitializeComponent();
            Title = "Grade guest";
            DataContext = this;
            gradeGuestRepository = new GradeGuestRepository();
            reservationRepository = new ReservationRepository();
        }

        private void GuestLoaded(object sender, RoutedEventArgs e)
        {
            List<Reservation> reservations = new List<Reservation>();

            reservations = reservationRepository.GetAll();

            for (int i = 0; i < reservations.Count; i++)
            {
                if (gradeGuestRepository.FindGuestsForGrade(i) != null)
                    GuestsCB.Items.Add(gradeGuestRepository.FindGuestsForGrade(i));
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

            object selectedItem = GuestsCB.SelectedItem;
            Reservation oldReservation = new Reservation();
            int id;
            string line = selectedItem.ToString();
            string[] fields = line.Split(' ');
            id = Convert.ToInt32(fields[0]);

            GuestGrade newGrade = new GuestGrade(
                GuestsCB.Text,
                Convert.ToInt32(CB1.Text),
                Convert.ToInt32(CB2.Text),
                CommentText.Text,
                id);
            gradeGuestRepository.Save(newGrade);

            CommentText.Clear();
            oldReservation = reservationRepository.GetReservationById(id);
            GuestsCB.Items.Remove(selectedItem);
            reservationRepository.LogicalDelete(oldReservation);
        }
            private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.GetAll();

            for (int i = 0; i < reservations.Count; i++)
            {
                if (gradeGuestRepository.ShowMessageForGrade(i) != null)
                {
                    MessageBox.Show(gradeGuestRepository.ShowMessageForGrade(i));
                }
                gradeGuestRepository.FindAndDeleteExpiredReservation(i);
            }
        }

        private void GuestsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
 }
