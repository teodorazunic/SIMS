using InitialProject.Domain.Models;
using InitialProject.Forms;
using InitialProject.Repository;
using InitialProject.WPF.Views;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for GuideOverview.xaml
    /// </summary>
    public partial class GuideOverview : Window
    {
        public GuideOverview()
        {
            InitializeComponent();
        }

        public User LoggedInUser { get; set; }

        private readonly TourRepository _repository;

        public GuideOverview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new TourRepository();
        }



        private void OpenTourForm(object sender, RoutedEventArgs e)
        {
            TourForm tourForm = new TourForm();
            tourForm.Show();
        }

        private void OpenGuideTours(object sender, RoutedEventArgs e)
        {
            GuideTours guideTours = new GuideTours(LoggedInUser);
            guideTours.Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
                MyTours myTours = new MyTours(LoggedInUser);
                myTours.Show();
                Close();
            }
        }
    }


