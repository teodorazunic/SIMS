using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.Views.Guide
{
    /// <summary>
    /// Interaction logic for Dissmisal.xaml
    /// </summary>
    public partial class Dissmisal : Page
    {
        public Dissmisal(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            DataContext = this;
            _repository = new UserRepository();
            tourRepository = new TourRepository();
            voucherRepository = new VoucherRepository();
        }
        private const string FilePath = "../../../Resources/Data/users.csv";

        public static ObservableCollection<User> Users { get; set; }

        private readonly UserRepository _repository;

        private readonly TourRepository tourRepository;

        private readonly VoucherRepository voucherRepository;
        public User LoggedInUser { get; set; }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }





        private void Button_Click(object sender, RoutedEventArgs e)
        {

            User user = _repository.GetByUsername(Username);



            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {

                    if (user.Role == UserRole.guide)
                    {

                        Window parentWindow = Window.GetWindow(this);
                        SignInForm signIn = new SignInForm();
                        //tourRepository.CancelFutureTours();
                        voucherRepository.SendVouchersForDissmisal(tourRepository.CancelFutureTours());



                        signIn.Show();
                        parentWindow.Close();

                    }
                }
                else
                {
                    txtBlock.Text = "Wrong password!";
                }
            }
            else
            {
                txtBlock.Text = "Wrong username!";
            }
        }
    }
}
