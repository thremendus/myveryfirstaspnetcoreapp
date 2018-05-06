using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreCourseBeginner.Models;

namespace AspNetCoreCourseBeginner.Services
{
    //public class InMemoryRestaurantData : IRestaurantData
    //{

    //    #region fields

    //    private readonly List<Restaurant> _restaurants;

    //    #endregion

    //    public InMemoryRestaurantData()
    //    {
    //        _restaurants = new List<Restaurant>
    //        {
    //            new Restaurant
    //            {
    //                Id = 1,
    //                Name = "My Pizza Place"
    //            },

    //            new Restaurant
    //            {
    //                Id = 2,
    //                Name = "My Pineapple Place"
    //            },
                
    //            new Restaurant
    //            {
    //                Id = 3,
    //                Name = "The Super King"
    //            }
    //        };
    //    }


    //    public IEnumerable<Restaurant> GetAll()
    //    {
    //        return _restaurants.OrderBy(x => x.Name);
    //    }

    //    public Restaurant GetRestaurantById(int id)
    //    {
    //        return _restaurants.FirstOrDefault(x => x.Id == id);
    //    }

    //    public Restaurant Add(Restaurant restaurant)
    //    {
    //        restaurant.Id = _restaurants.Max(r => r.Id) + 1;

    //        _restaurants.Add(restaurant);

    //        return restaurant;
    //    }
    //}
}
