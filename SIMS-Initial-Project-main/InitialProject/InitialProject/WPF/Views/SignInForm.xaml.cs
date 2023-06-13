﻿using InitialProject.Domain;
using InitialProject.Domain.Models;
using InitialProject.Domain.ServiceInterfaces;
using InitialProject.Forms;
using InitialProject.Repository;
using InitialProject.View;
using InitialProject.WPF.Guide;
using InitialProject.WPF.Views.Guide;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly IUserService _userService;

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

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _userService = Injector.CreateInstance<IUserService>();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _userService.GetByUsername(Username);
            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {
                    if (user.Role == UserRole.guest1)
                    {
                        AccommodationOverview accommodationOverview = new AccommodationOverview(user);
                        accommodationOverview.Show();
                        Close();
                    }
                    else if (user.Role == UserRole.guest2)
                    {
                        MainWindow tourOverview = new MainWindow(user);
                        tourOverview.Show();
                        Close();

                    }
                    else if (user.Role == UserRole.owner)
                    {

                        OwnerOverview ownerOverview = new OwnerOverview(user);
                        ownerOverview.Show();
                        Close();
                    }
                    else if (user.Role == UserRole.guide)
                    {
                        MainWindow mainWindow = new MainWindow(user);
                        mainWindow.Show();


                        CreateTour page1 = new CreateTour();
                        Frame mainFrame = mainWindow.FindName("page") as Frame;
                        mainFrame.NavigationService.Navigate(page1);

                        Close();

                    }
                    else
                    {
                        CommentsOverview commentsOverview = new CommentsOverview(user);
                        commentsOverview.Show();
                        Close();
                    }
                }
                else
                {
                    txtBlock.Text = "Wrong password!";
                    //MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                txtBlock.Text = "Wrong username!";
                //MessageBox.Show("Wrong username!");
            }

        }
    }
}
