using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;//to access SelectList
using Microsoft.EntityFrameworkCore;//to access EntityState
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;//to use LINQ's ToList() method

  namespace Restaurant.Controllers
  {
    public class EateriesController : Controller
    {
      private readonly RestaurantContext _db; // our database declared as an object, so it connect

      public EateriesController(RestaurantContext db) // 
      {
        _db = db; // database object
      } //this constructor allows us to set the value of our new _db property to our RestaurantContext. This is achievable due to a dependency injection we set up in our AddDbContext method in the ConfigureServices method in our Startup.cs file.

      [HttpGet("/Eateries")]
      public ActionResult Index()
      {
        List<Eatery> model = _db.Eateries.ToList();
        return View(model);
      }//1. db is an instance of our DbContext class. It's holding a reference to our database.//2. Once there, it looks for an object named Eateries. This is the DbSet we declared in RestaurantContext.cs.//3. LINQ turns this DbSet into a list using the ToList() method, which comes from the System.Linq namespace.//4. This expression is what creates the model we'll use for the Index view.

      public ActionResult Create()
      {
        ViewBag.CuisineId = new SelectList(_db.Cuisines, "CuisineId", "CuisineType");
        return View();
      }//Creating a ViewBag.CuisineId property as a Select List object ensures we are creating new items within Cuisines that already exist.A SelectList will provide a list of the data needed to create an html <select> list of all the cuisines from our database. The displayed text of each <option> will be the Cuisines CuisineType property, and the value of the <option> will be the Cuisines CuisineId.
      //selectList arguments-- 1. the data that will populate <option> elements (list of cuisines from our DB) 2. the value of every <option> element (Cuisine's CuisineId) 3. displayed text (name of Cuisine)
   }
  }