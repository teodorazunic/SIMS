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
using InitialProject.WPF.ViewModels.Guest1;

namespace InitialProject.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for ForumPage.xaml
    /// </summary>
    public partial class ForumPage : Window
    {
        private AllForumsViewModel viewModel;
        public ForumPage(User user)
        {
            InitializeComponent();
            viewModel = new AllForumsViewModel(user);
            DataContext = viewModel;
        }

        public void ShowMyForums(object sender, RoutedEventArgs e)
        {
            ForumsList.ItemsSource = viewModel.ShowMyForums();
        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            viewModel.OnLoad();
        }

        public void OnForumClick(object sender, RoutedEventArgs e)
        {
            OpenForum view = new OpenForum(viewModel.User, viewModel.Selected);
            view.Show();
        }

        public void ShowAllForums(object sender, RoutedEventArgs e)
        {
            ForumsList.ItemsSource = viewModel.ShowAllForums();
        }
    }

}

