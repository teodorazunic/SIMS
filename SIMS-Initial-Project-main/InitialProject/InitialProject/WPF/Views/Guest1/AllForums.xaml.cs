using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels.Guest1;
using System.Windows;

namespace InitialProject.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for AllForums.xaml
    /// </summary>
    public partial class AllForums : Window
    {

        private AllForumsViewModel viewModel;
        public AllForums(User user)
        {
            InitializeComponent();
            viewModel = new AllForumsViewModel(user);
            DataContext = viewModel;
        }

        public void CreateForum(object sender, RoutedEventArgs e)
        {
            CreateNewForum view = new CreateNewForum(viewModel.User); ;
            view.Show();
            Close();
        }

        public void ShowAllForums(object sender, RoutedEventArgs e)
        {
            ForumsList.ItemsSource = viewModel.ShowAllForums();
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
            ForumPage view = new ForumPage(viewModel.User, viewModel.Selected);
            view.Show();
            Close();
        }
    }
}
