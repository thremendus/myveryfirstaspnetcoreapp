using AspNetCoreCourseBeginner.Models;
using AspNetCoreCourseBeginner.Services;
using AspNetCoreCourseBeginner.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreCourseBeginner.Controllers
{
    //This attribute will make sure the user is authorized (logged in)
    //Before he can access these methods.
    //Best practice is to put the Authorize at the class level becuase,
    //if you do it at the method level you might miss one
    //ALSO: This is the attribute Challenging the user to log-in.
    //The settings to challenge a user to login are located in the StartUp.cs.
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IRestaurantData _restaurantData;
        private readonly IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }

        //However, I want not-authorized users to see my home page.
        //I can override the Authorize attribute by putting this attribute of AllowAnonymous.
        //Now anyone can caccess my homepage.
        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = new HomeIndexViewModel
            {
                Restaurants = _restaurantData.GetAll(),
                MessageOfTheDay = _greeter.GetMessageOfTheDay()
            };
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _restaurantData.GetRestaurantById(id);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        [HttpGet] //The name of this is Route Constraints
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //By letting MVC do the binding to the restaurant object it will try to map EVERY SINGLE property on your object..
        //a hacker can map properties that you don't wnat the user to map.
        //This is called OverPosting
        //The best way to avoid this is by creating Input Models (command objects) that only contains the properties that we need/want.
        public IActionResult Create(RestaurantEditModel restaurantEditModel)
        {
            // ....

            if (ModelState.IsValid)
            {
                var newRestaurant = new Restaurant
                {
                    Name = restaurantEditModel.Name,
                    CusineType = restaurantEditModel.CusineType
                };

                _restaurantData.Add(newRestaurant);
                return RedirectToAction(nameof(Details), new { id = newRestaurant.Id });
            }
            else
            {
                return View();
            }

        }
    }
}
