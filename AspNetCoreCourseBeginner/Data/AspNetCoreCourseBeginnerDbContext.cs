using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreCourseBeginner.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreCourseBeginner.Data
{
    public class AspNetCoreCourseBeginnerDbContext : DbContext
    {
        public AspNetCoreCourseBeginnerDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
