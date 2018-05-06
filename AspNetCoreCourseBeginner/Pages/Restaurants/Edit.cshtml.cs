using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreCourseBeginner.Models;
using AspNetCoreCourseBeginner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreCourseBeginner.Pages.Restaurants
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public EditModel(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        public IActionResult OnGet(int id)
        {
            Restaurant = _restaurantData.GetRestaurantById(id);
            if (Restaurant == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            //TODO: Continue here!
            if (ModelState.IsValid)
            {
                _restaurantData.UpdateRestaurant(Restaurant);
                return RedirectToAction("Details", "Home", new { id = Restaurant.Id });
            }

            return Page();


        }
    }
}