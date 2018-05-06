using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreCourseBeginner.Models;

namespace AspNetCoreCourseBeginner.Services
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant GetRestaurantById(int id);
        Restaurant Add(Restaurant restaurant);
        Restaurant UpdateRestaurant(Restaurant restaurant);
    }
}
