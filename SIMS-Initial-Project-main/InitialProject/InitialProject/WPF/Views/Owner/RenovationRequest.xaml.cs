using System;
using System.Collections.Generic;
using System.Linq;
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
using InitialProject.Application.Services;
using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.ServiceInterfaces;
using InitialProject.Repository;

namespace InitialProject.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for RenovationRequest.xaml
    /// </summary>
    public partial class RenovationRequest : Window
    {
        private readonly AccommodationRepository accommodationRepository;
        private readonly ReservationRepository reservationRepository;
        private User LogedUser { get; }


        public RenovationRequest(User user)
        {
            InitializeComponent();
            LogedUser = user;
            accommodationRepository = new AccommodationRepository();
            reservationRepository = new ReservationRepository();
        }


        private void OnLoad(object sender, RoutedEventArgs e)
        {
            AccommodationCB.ItemsSource = accommodationRepository.FillForComboBoxHotels(LogedUser);
        }



        private void ShowDates(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)AccommodationCB.SelectedItem;
            int selectedValue = Convert.ToInt32(selectedItem.Content);

            List<DateTime> alternativeDates = reservationRepository.FindAlternativeDates(selectedValue, Convert.ToDateTime(StartDate.Text), Convert.ToDateTime(EndDate.Text), Convert.ToInt32(NumberOfDays.Text));

            List<string> alternatives = new List<string>();
            foreach (DateTime date in alternativeDates)
            {
                alternatives.Add(date.ToShortDateString() + " to " + (date.AddDays(Convert.ToInt32(NumberOfDays.Text))).ToShortDateString());
            }
            ListDates.ItemsSource = alternatives;
        }


        /*   private void ShowDates(object sender, RoutedEventArgs e)
           {
               if (AccommodationCB.SelectedItem is ComboBoxItem selectedItem)
               {
                   string accommodationIdString = selectedItem.Content.ToString();
                   if (int.TryParse(accommodationIdString, out int accommodationId))
                   {
                       List<DateTime> alternativeDates = reservationRepository.FindAlternativeDates(accommodationId, Convert.ToDateTime(StartDate.Text), Convert.ToDateTime(EndDate.Text), Convert.ToInt32(NumberOfDays.Text));
                       List<string> alternatives = new List<string>();
                       foreach (DateTime date in alternativeDates)
                       {
                           alternatives.Add(date.ToShortDateString() + " to " + date.AddDays(Convert.ToInt32(NumberOfDays.Text)).ToShortDateString());
                       }
                       ListDates.ItemsSource = alternatives;
                   }

               }
           }*/


        /*       private void ShowDates(object sender, RoutedEventArgs e)
               {
                   ComboBoxItem comboBoxItem = AccommodationCB.SelectedItem as ComboBoxItem;
                   int selectedAccommodationId = 0;
                   if (comboBoxItem != null)
                   {
                       if (comboBoxItem.DataContext is Accommodation accommodation)
                       {
                           selectedAccommodationId = accommodation.Id;
                       }
                   }
                   List<DateTime> alternativeDates = reservationRepository.FindAlternativeDates(selectedAccommodationId, Convert.ToDateTime(StartDate.Text), Convert.ToDateTime(EndDate.Text), Convert.ToInt32(NumberOfDays.Text));
                   List<string> alternatives = new List<string>();
                   foreach (DateTime date in alternativeDates)
                   {
                       alternatives.Add(date.ToShortDateString() + " to " + (date.AddDays(Convert.ToInt32(NumberOfDays.Text))).ToShortDateString());
                   }
                   ListDates.ItemsSource = alternatives;
               }*/

        private void Accept(object sender, RoutedEventArgs e)
        {
            object selectedItem = ListDates.SelectedItem;
            string newStart;
            string newEnd;
            string line = selectedItem.ToString();
            string[] fields = line.Split(" to ");
            newStart = fields[0];
            newEnd = fields[1];

            StartDate.Text = newStart;
            EndDate.Text = newEnd;
        }

        private void Reserve(object sender, RoutedEventArgs e)
        {
            reservationRepository.ReserveRenovation(AccommodationCB, Convert.ToDateTime(StartDate.Text), Convert.ToDateTime(EndDate.Text));
        }



    }
}

