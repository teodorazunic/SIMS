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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.Views.Guide
{
    /// <summary>
    /// Interaction logic for CreateTourByStatistics.xaml
    /// </summary>
    public partial class CreateTourByStatistics : Page
    {
        public CreateTourByStatistics()
        {
            InitializeComponent();
            txtLanguage.Text = "Srpski";
            txtLocation.Text = "Novi Sad";
        }

        private void Fill(object sender, RoutedEventArgs e)
        {
            txtLocation.Text = "Novi Sad";
        }

        private void Fill1(object sender, RoutedEventArgs e)
        {
            txtLanguage.Text = "Srpski";
        }

    }
}
