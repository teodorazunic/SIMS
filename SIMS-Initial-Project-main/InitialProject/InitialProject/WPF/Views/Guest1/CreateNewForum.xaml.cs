using InitialProject.Domain.Models;
using InitialProject.WPF.ViewModels.Guest1;
using System.Windows;

namespace InitialProject.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for CreateNewForum.xaml
    /// </summary>
    public partial class CreateNewForum : Window
    {
        private CreateNewForumViewModel viewModel;
        public CreateNewForum(User user)
        {
            InitializeComponent();
            viewModel = new CreateNewForumViewModel(user);
            DataContext = viewModel;
            Locations.ItemsSource = viewModel.Locations;
        }

        public void Save(object sender, RoutedEventArgs e)
        {
            viewModel.Save();
            MessageBox.Show("Successfully created forum!");

            AllForums view = new AllForums(viewModel.User);
            view.Show();
            Close();
        }
    }
}
