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
using InitialProject.WPF.ViewModels.Guest1;

namespace InitialProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for MyReviews.xaml
    /// </summary>
    public partial class MyReviews : Window
    {
        private MyReviewsViewModel viewModel;
        public MyReviews(User user)
        {
            InitializeComponent();
            viewModel = new MyReviewsViewModel(user);
            DataContext = viewModel;
        }

        private void ShowReviewsFromOwner(object sender, RoutedEventArgs e)
        {
            ReviewData.ItemsSource = viewModel.ShowReviewsFromOwner();
        }
    }
}
    