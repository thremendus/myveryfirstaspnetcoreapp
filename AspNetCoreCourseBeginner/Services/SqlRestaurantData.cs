using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreCourseBeginner.Data;
using AspNetCoreCourseBeginner.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreCourseBeginner.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly AspNetCoreCourseBeginnerDbContext _dbContext;

        public SqlRestaurantData(AspNetCoreCourseBeginnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _dbContext.Restaurants.OrderBy(r => r.Name);
        }

        public Restaurant GetRestaurantById(int id)
        {
            return _dbContext.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant restaurant)
        {
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant;
        }

        public Restaurant UpdateRestaurant(Restaurant restaurant)
        {
            _dbContext.Attach(restaurant).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return restaurant;
        }
    }
}
