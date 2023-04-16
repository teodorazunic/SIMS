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
using InitialProject.Domain.Models;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for ReviewFromGuests.xaml
    /// </summary>
    public partial class ReviewFromGuests : Window
    {
        private readonly GradeOwnerRepository gradeOwnerRepository;
        private User LogedOwner { get; set; }
        public ReviewFromGuests(User user)
        {
            
            gradeOwnerRepository = new GradeOwnerRepository();
            LogedOwner = user;
            InitializeComponent();
        }

        private void ShowReviewsFromGuests(object sender, RoutedEventArgs e)
        {
            ReviewData.ItemsSource = gradeOwnerRepository.ShowReviewsForOwner();
        }
    }
}
    