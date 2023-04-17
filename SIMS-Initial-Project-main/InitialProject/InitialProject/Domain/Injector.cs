using InitialProject.Domain.RepositoryInterfaces;
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
