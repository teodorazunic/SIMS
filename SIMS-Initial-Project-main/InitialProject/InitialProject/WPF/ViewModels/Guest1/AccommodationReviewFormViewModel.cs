using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain;
using InitialProject.Forms;
using System.Collections.Generic;
using System.Windows;

namespace InitialProject.WPF.ViewModels.Guest1
{
    public class AccommodationReviewFormViewModel : MainViewModel
    {
        public static Accommodation SelectedAccommodation { get; set; }
        public User LoggedInUser { get; set; }

        private readonly IAccommodationReviewRepository _repository;

        private List<AccommodationReviewImage> Images;

        private int _cleanliness = 5;
        private int _staff = 5;
        private string _comment = "";
        private string _image = "";
        private string _whatWasBadComment = "";
        private string _whatToRenovateComment = "";
        private int _urgencyRenovationLevel = 1;

        public string WhatWasBadComment
        {
            get => _whatWasBadComment;
            set
            {
                if (value != _whatWasBadComment)
                {
                    _whatWasBadComment = value;
                    OnPropertyChanged();
                }
            }
        }

        public string WhatToRenovateComment
        {
            get => _whatToRenovateComment;
            set
            {
                if (value != _whatToRenovateComment)
                {
                    _whatToRenovateComment = value;
                    OnPropertyChanged();
                }
            }
        }
        public int UrgencyRenovationLevel
        {
            get => _urgencyRenovationLevel;
            set
            {
                if (value != _urgencyRenovationLevel)
                {
                    _urgencyRenovationLevel = value;
                    OnPropertyChanged();
                }
            }
        }


        public int Cleanliness
        {
            get => _cleanliness;
            set
            {
                if (value != _cleanliness)
                {
                    _cleanliness = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Staff
        {
            get => _staff;
            set
            {
                if (value != _staff)
                {
                    _staff = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Image
        {
            get => _image;
            set
            {
                if (value != _image)
                {
                    _image = value;
                    OnPropertyChanged();
                }
            }
        }
        public AccommodationReviewFormViewModel(User loggedInUser, Accommodation selectedAccommodation)
        {
            _repository = Injector.CreateInstance<IAccommodationReviewRepository>();
            Images = new List<AccommodationReviewImage>();
            SelectedAccommodation = selectedAccommodation;
            LoggedInUser = loggedInUser;
        }

        public void SaveReview()
        {
            if ((Cleanliness < 1 || Cleanliness > 5) || ((Staff < 1 || Staff > 5)))
            {
                MessageBox.Show("Ocena mora imati vrednost 1 - 5");
                return;
            }
            AccommodationReview accommodationReview = new AccommodationReview(LoggedInUser.Id, SelectedAccommodation.Id, Cleanliness, Staff, Comment);

            RenovationRecommendation renovationRecommendation = new RenovationRecommendation();
            renovationRecommendation.WhatWasBadComment = WhatWasBadComment;
            renovationRecommendation.WhatToRenovateComment = WhatToRenovateComment;
            renovationRecommendation.UrgencyRenovationLevel = UrgencyRenovationLevel;

            string message = _repository.SaveReview(accommodationReview, Images, renovationRecommendation);

            MessageBox.Show(message);
            AccommodationOverview overview = new AccommodationOverview(LoggedInUser);
            overview.Show();
        }

        public List<AccommodationReviewImage> SetImage()
        {

            if (Image == null || Image == "")
            {
                MessageBox.Show("Upisite validan url!");
            }
            else
            {

                AccommodationReviewImage accommodationReviewImage = new AccommodationReviewImage();
                accommodationReviewImage.ImageUrl = Image;
                Images.Add(accommodationReviewImage);
            }

            return Images;
        }
    }
}
