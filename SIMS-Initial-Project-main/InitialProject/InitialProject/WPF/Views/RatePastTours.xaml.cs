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
using InitialProject.Repositories;
using InitialProject.Repository;

namespace InitialProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for RatePastTours.xaml
    /// </summary>
    public partial class RatePastTours : Window
    {
        public User LoggedInUser { get; set; }
        GradeGuideRepository _repository = new GradeGuideRepository();
        
        public RatePastTours(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            _repository = new GradeGuideRepository();

        }

        private void Save(object sender, RoutedEventArgs e)
        {
            
            int gradeId = _repository.NextId();
            string text1 = txtKnowledge.Text;
            int knowledge = 0;

            if (int.TryParse(text1, out int number1))
            {
                if (number1 >= 1 && number1 <= 5)
                {
                    knowledge = number1;
                }
                else
                { 
                    MessageBox.Show("Enter a number between 1 and 5");
                }
            }
            else
            {
                MessageBox.Show("Enter a number between 1 and 5");
            }

            string text2 = txtLanguage.Text;
            int language = 0;

            if (int.TryParse(text2, out int number2))
            {
                if (number2 >= 1 && number2 <= 5)
                {
                    language = number2;
                }
                else
                {
                    MessageBox.Show("Enter a number between 1 and 5");
                }
            }
            else
            {
                MessageBox.Show("Enter a number between 1 and 5");
            }

            string text3 = txtOverall.Text;
            int overall = 0;

            if (int.TryParse(text3, out int number3))
            {
                if (number3 >= 1 && number3 <= 5)
                {
                    overall = number3;
                }
                else
                {
                    MessageBox.Show("Enter a number between 1 and 5");
                }
            }
            else
            {
                MessageBox.Show("Enter a number between 1 and 5");
            }

            string comment = txtComment.Text;
            string url = txtUrl.Text;

            if (knowledge >= 1 && knowledge <= 5 && language >= 1 && language <= 5 && overall >= 1 && overall <= 5)
            {
                GradeGuide gradeGuide = new GradeGuide(gradeId, LoggedInUser.Id, knowledge, language, overall, comment, "Valid", url);
                GradeGuide saveGrade = _repository.SaveGrade(gradeGuide);
                MessageBox.Show("You have rated this tour.");
                Close();

            }
            else
            { 
                MessageBox.Show("Couldn't save the tour.");
            }
        }
    }
}
