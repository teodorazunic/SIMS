using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for OwnerOverview.xaml
    /// </summary>
    public partial class OwnerOverview : Window
    {

        public static ObservableCollection<Accommodation> Accommodations { get; set; }

        public Owner SelectedHotel { get; set; }

        public User LoggedInUser { get; set; }

        private readonly AccommodationRepository _repository;

        public int DaysLeftForGrade = 5;

        public OwnerOverview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new AccommodationRepository();
        }

        public OwnerOverview()
        {
        }

         private void OpenOwnerForm(object sender, RoutedEventArgs e)
         {
             OwnerForm1 createOwnerForm = new OwnerForm1();
             createOwnerForm.Show();
         }




        private void GradeAlert(object sender, RoutedEventArgs e)
        {

        }

        private void OpenGradeForm(object sender, RoutedEventArgs e)
        {
            GradeForm createGradeForm = new GradeForm();
            createGradeForm.Show();
        }
    }
}
