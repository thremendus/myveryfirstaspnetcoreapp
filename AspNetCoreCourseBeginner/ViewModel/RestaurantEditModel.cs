using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreCourseBeginner.Models;

namespace AspNetCoreCourseBeginner.ViewModel
{
    public class RestaurantEditModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }
        public CusineType CusineType { get; set; }
    }
}
