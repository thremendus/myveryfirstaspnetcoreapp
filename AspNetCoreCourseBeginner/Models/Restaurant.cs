﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreCourseBeginner.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        [Display(Name = "Restaurant Name")]
        [Required, MaxLength(80)]
        public string Name { get; set; }
        public CusineType CusineType { get; set; }
    }
}
