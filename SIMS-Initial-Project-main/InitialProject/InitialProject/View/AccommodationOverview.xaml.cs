using InitialProject.Model;
using InitialProject.Repository;
using System.Collections.ObjectModel;
using System.Windows;

namespace InitialProject.Forms
{
    /// <summary>
    /// Interaction logic for AccommodationOverview.xaml
    /// </summary>
    public partial class AccommodationOverview : Window
    {

        public static ObservableCollection<Accommodation> Accommodations { get; set; }

        public User LoggedInUser { get; set; }

        private readonly AccommodationRepository _repository;

        public AccommodationOverview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new AccommodationRepository();
            Accommodations = new ObservableCollection<Accommodation>(_repository.GetAllAccomodations());
        }
    }
}
