using InitialProject.Domain.Models;
using InitialProject.Forms;
using InitialProject.Repository;
using InitialProject.View;
using System;
using System.Collections.Generic;
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
        public Dissmisal()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
        }

        private readonly UserRepository _repository;

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
                       
                        signIn.Show();
                        parentWindow.Close();

                    }
                }
                else
                {
                    txtBlock.Text= "Wrong password!";
                }
            }
            else
            {
                txtBlock.Text = "Wrong username!";
            }
        }
    }
}
