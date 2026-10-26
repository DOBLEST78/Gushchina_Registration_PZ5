using Gushchina_UserRegistration.Model;

namespace Gushchina_UserRegistration
{
    internal class DatabaseConnector
    {
        private static RestaurantDataEntities _restaurantContext;

        public static RestaurantDataEntities GetRestaurantContext()
        {
            if (_restaurantContext == null)
            {
                _restaurantContext = new RestaurantDataEntities();
            }

            return _restaurantContext;
        }
    }
}
