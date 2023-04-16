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
using System.Windows.Shapes;
using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Repository;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for OwnerForm1.xaml
    /// </summary>
    public partial class OwnerForm1 : Window
    {
        public OwnerForm1()
        {
            InitializeComponent();
            Initializecbtypes();
        }

        AccommodationRepository repository = new AccommodationRepository();
        private AccommodationType type;


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AccommodationType Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        private void Initializecbtypes()
        {
            foreach (string types in (Enum.GetNames(typeof(AccommodationType))))
            {
                cbtypes.Items.Add(types);
            }
            cbtypes.SelectedIndex = 0;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String name = txtName.Text;
            Location location = new Location(txtCity.Text, txtCountry.Text);
            //AccommodationType type = (AccommodationType)cbtypes.SelectedValue;
            int max = Convert.ToInt32(numMax.Text);
            int min = Convert.ToInt32(numMin.Text);
            int cancelDays = Convert.ToInt32(deadlineDays.Text);
            String picture = txtPic.Text;

            Accommodation accommodation = new Accommodation(name, location, Type, max, min, cancelDays, picture, 1);
            Accommodation saveAccommodation = repository.Save(accommodation);
            MessageBox.Show("Accommodation successfully registered!");
        }

        private void cbtypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void Logout(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }
    }
}
