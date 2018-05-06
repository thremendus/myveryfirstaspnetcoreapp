using System.Collections.Generic;
using AspNetCoreCourseBeginner.Models;

namespace AspNetCoreCourseBeginner.ViewModel
{
    public class HomeIndexViewModel //Also known as DTO = Data Transfer Object
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }
        public string MessageOfTheDay { get; set; }
    }
}
