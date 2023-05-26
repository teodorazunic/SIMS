using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels.Guest1;
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

namespace InitialProject.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for ForumPage.xaml
    /// </summary>
    public partial class ForumPage : Window
    {
        private ForumPageViewModel viewModel;
        public ForumPage(User user, Forum selectedForum)
        {
            InitializeComponent();
            viewModel = new ForumPageViewModel(user, selectedForum);
            DataContext = viewModel;
        }

        public void AddComment(object sender, RoutedEventArgs e)
        {
            viewModel.AddComment();
            Comments.ItemsSource = viewModel.GetAllForumComments();
            MessageBox.Show("Successfully added comment!");
        }

        public void CloseForum(object sender, RoutedEventArgs e)
        {
            viewModel.CloseForum();
            MessageBox.Show("Successfully closed forum!");
        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            Comments.ItemsSource = viewModel.GetAllForumComments();
        }
    }
}
