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
using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels.Guest1;

namespace InitialProject.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for OpenForum.xaml
    /// </summary>
    public partial class OpenForum : Window
    {
        private ForumPageViewModel viewModel;
        public ForumComment SelectedComment { get; set; }

        public ForumService forumService { get; set; }


        public OpenForum(User user, Forum selectedForum)
        {
            InitializeComponent();
            viewModel = new ForumPageViewModel(user, selectedForum);
            DataContext = viewModel;
                        DataContext = this;

            forumService = new ForumService();

        }

        public void AddComment(object sender, RoutedEventArgs e)
        {
            viewModel.AddComment1();
            Comments.ItemsSource = viewModel.GetAllForumComments();
            MessageBox.Show("Successfully added comment!");
        }


        public void OnLoad(object sender, RoutedEventArgs e)
        {
            Comments.ItemsSource = viewModel.GetAllForumComments();
        }

        private void ReportComment(object sender, RoutedEventArgs e)
        {

        }
    }
}
