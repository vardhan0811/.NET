using System;

namespace QuickBite.Domain.Exceptions
{
    public class RestaurantOfflineException : Exception
    {
        public string RestaurantId { get; }

        public RestaurantOfflineException(string restaurantId)
            : base($"Restaurant {restaurantId} is offline")
        {
            RestaurantId = restaurantId;
        }
    }
}
