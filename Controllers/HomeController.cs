using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious2.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDelicious2.Controllers
{
    public class HomeController : Controller
    {
        private YourContext dbContext;
    // here we can "inject" our context service into the constructor
        public HomeController(YourContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            List<Dishes> AllDishes = dbContext.dishes.ToList();
            ViewBag.allDishes = AllDishes;
            return View();
        }
        [HttpGet("new")]
        public IActionResult newDish_Form()
        {
            return View("createDish");
        }

        [HttpGet]
        [Route("{dish_id}")]
        public IActionResult dishpage(int dish_id)
        {
            List<Dishes> DishPage = dbContext.dishes.Where(dish => dish.id == dish_id).ToList();
            ViewBag.dishpage = DishPage;
            return View("DishPage");
        }

        [HttpGet]
        [Route("edit/{dish_id}")]
        public IActionResult showeditpage(int dish_id)
        {
            Dishes retrievedDish = dbContext.dishes.FirstOrDefault(dish => dish.id == dish_id);
            ViewBag.dishpage = retrievedDish;
            return View("EditDish");
        }

        [HttpGet("delete/{dish_id}")]
        public IActionResult delete_dish(Dishes deleted_dish, int dish_id)
        {
            Dishes retrievedDish = dbContext.dishes.FirstOrDefault(dish => dish.id == dish_id);
            dbContext.dishes.Remove(retrievedDish);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("edit/{dish_id}")]
        public IActionResult editdish(Dishes edited_dish, int dish_id)
        {
            Dishes retrievedDish = dbContext.dishes.FirstOrDefault(dish => dish.id == dish_id);
            ViewBag.dishpage = retrievedDish;

            if(ModelState.IsValid)
            {
                retrievedDish.Name = edited_dish.Name;
                retrievedDish.Chef = edited_dish.Chef;
                retrievedDish.Description = edited_dish.Description;
                retrievedDish.Tastiness = edited_dish.Tastiness;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else 
            {
                return View("EditDish");
            }
            
        }

        [HttpPost("create")]
        public IActionResult create(Dishes dish)
        {
            if(ModelState.IsValid)
            {
                Dishes NewDish = new Dishes
                {
                    Name = dish.Name,
                    Chef = dish.Chef,
                    Tastiness = dish.Tastiness,
                    Calories = dish.Calories,
                    Description = dish.Description,
                };
                dbContext.Add(NewDish);
                dbContext.SaveChanges();
                System.Console.WriteLine(NewDish);
                
                return RedirectToAction("Index");
            }
            else
            {
                return View("createDish");
            }
        }

    }
}
