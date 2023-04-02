﻿using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class AccommodationReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationreview.csv";

        private readonly Serializer<AccommodationReview> _serializer;

        private AccommodationReviewImageRepository _imageRepository;

        private ReservationRepository _reservationRepository;

        private List<AccommodationReview> _accommodationReviews;

        public AccommodationReviewRepository()
        {
            _serializer = new Serializer<AccommodationReview>();
            _accommodationReviews = _serializer.FromCSV(FilePath);
            _imageRepository = new AccommodationReviewImageRepository();
            _reservationRepository = new ReservationRepository();
        }

        public int GetLastId()
        {
            if (_accommodationReviews != null && _accommodationReviews.Count > 0)
            {
                return _accommodationReviews.Max(review => review.Id);
            }
            return 0;
        }

        public string SaveReview(AccommodationReview accommodationReview, List<AccommodationReviewImage> images)
        {

            List<Reservation> allReservations = _reservationRepository.GetAll();

            if (allReservations != null)
            {
                Reservation reservation = allReservations.Find(r => r.AccommodationId == accommodationReview.AccommodationId
            && r.GuestId == accommodationReview.GuestId);

                if (reservation != null)
                {
                    DateTime currentDate = DateTime.Now;

                    if (reservation.DateTo.AddDays(5) < currentDate)
                    {
                        return "Nije moguce ostaviti recenziju";
                    }
                }

            }

            int accommodationReviewId = GetLastId() + 1;
            accommodationReview.Id = accommodationReviewId;

            if (images != null && images.Count > 0)
            {

                foreach (AccommodationReviewImage image in images)
                {
                    image.AccommodationReviewId = accommodationReviewId;
                }

                _imageRepository.SaveImages(images);
            }

            _accommodationReviews.Add(accommodationReview);
            _serializer.ToCSV(FilePath, _accommodationReviews);

            return "Uspesno sacuvana recenzija!";
        }
    }
}