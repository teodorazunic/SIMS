using InitialProject.Application.Services;
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
            { typeof(IReservationMovingRepository), new ReservationMovingRepository() },

           /* { typeof(INotificationService), new NotificationService(Injector.CreateInstance<INotificationRepository>()) },
             { typeof(IAccommodationService), new AccommodationService(Injector.CreateInstance<IAccommodationRepository>()) },
             { typeof(IAccommodationReviewService), new AccommodationReviewService(Injector.CreateInstance<IAccommodationReviewRepository>()) },
             { typeof(IAccommodationReviewImageService), new AccommodationReviewImageService(Injector.CreateInstance<IAccommodationReviewImageRepository>()) },
             { typeof(IReservationService), new ReservationService(Injector.CreateInstance<IReservationRepository>()) },
             { typeof(IReservationMovingService), new ReservationMovingService(Injector.CreateInstance<IReservationMovingRepository>()) }
           */
    };

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
