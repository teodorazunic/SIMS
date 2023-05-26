﻿using InitialProject.Application.Services;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.ServiceInterfaces;
using InitialProject.Repositories;
using InitialProject.Repository;
using System;
using System.Collections.Generic;

namespace InitialProject.Domain
{
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
    {
        { typeof(INotificationRepository), new NotificationRepository() },
         { typeof(IAccommodationRepository), new AccommodationRepository() },
          { typeof(IAccommodationReviewRepository), new AccommodationReviewRepository() },
           { typeof(IAccommodationReviewImageRepository), new AccommodationReviewImageRepository() },
            { typeof(IReservationRepository), new ReservationRepository() },
            { typeof(IGradeGuestRepository ), new GradeGuestRepository() },
            { typeof(IReservationMovingRepository), new ReservationMovingRepository() },
            { typeof(IGradeGuideRepository), new GradeGuideRepository() },
            { typeof(IGuestOnTourRepository), new GuestOnTourRepository() },
            { typeof(ITourRepository), new TourRepository() },
            { typeof(IKeyPointRepository), new KeyPointRepository() },
            { typeof(IVoucherRepository), new VoucherRepository() },
            { typeof(ITourReservationRepository), new TourReservationRepositery() },
            { typeof(ITourRequestRepository), new TourRequestRepository() },
            { typeof(ITourNotificationsRepository), new TourNotificatinsRepository() },
            { typeof(IUserRepository), new UserRepository() },

            { typeof(IForumCommentRepositoryInterface), new ForumCommentRepository() },
            { typeof(IForumRepositoryInterface), new ForumRepository() },

            { typeof(IUserService), null },
            { typeof(IForumService), null },
            { typeof(IForumCommentService), null },
            { typeof(IAccommodationService), null }

};

        static Injector()
        {
            _implementations[typeof(IUserService)] = new UserService(CreateInstance<IUserRepository>());
            _implementations[typeof(IForumService)] = new ForumService(CreateInstance<IForumRepositoryInterface>());
            _implementations[typeof(IForumCommentService)] = new ForumCommentService(CreateInstance<IForumCommentRepositoryInterface>());
            _implementations[typeof(IAccommodationService)] = new AccommodationService(CreateInstance<IAccommodationRepository>(), CreateInstance<IReservationRepository>());


        }

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}