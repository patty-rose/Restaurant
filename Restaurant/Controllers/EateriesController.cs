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

    [HttpPost]
    public ActionResult Create(Eatery eatery)//takes Item as argument
    {
      _db.Eateries.Add(eatery);//adds Item to the ItemsDbSet
      _db.SaveChanges();//save changes to database object called DB or _db
      return RedirectToAction("Index");//return Index view
    }//Add() is a method we run on our DBSet property of our DBContext, while SaveChanges() is a method we run on the DBContext itself.
    //Together, they update the DBSet and then sync those changes to the database which the DBContext represents.

    public ActionResult Details(int id)//matches "id" object that we created using ActionLink in Views/Items/Index
    {
      Eatery thisEatery = _db.Eateries.FirstOrDefault(eatery => eatery.EateryId == id);//LINQ method with a lambda-- start looking at _db.Items(our items table), then find any items where the ItemId matches our id argument
      return View(thisEatery);
    }
    
    //GET action routes to page with a form for updating item
    public ActionResult Edit(int id)
    {
      Eatery thisEatery = _db.Eateries.FirstOrDefault(eatery => eatery.EateryId == id);
      ViewBag.CuisineId = new SelectList(_db.Cuisines, "CuisineId", "CuisineType");
      return View(thisEatery);
    }//finding specific eatery and passing it into view

    [HttpPost]//POST actually updates item
    public ActionResult Edit(Eatery eatery)
    {
      _db.Entry(eatery).State = EntityState.Modified;//We find and update all of the properties of the item we are editing by passing the item (our route parameter) itself into the Entry() method. Then we need to update its State property to EntityState.Modified. This is so Entity knows that the entry has been modified, as it is not explicitly tracking it (we never actually retrieved the item from the database).
      _db.SaveChanges();//once entry's state is marked as modified we can save and then redirect to Index view
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisEatery = _db.Eateries.FirstOrDefault(eatery => eatery.EateryId == id);
      return View(thisEatery);
    }


    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)//POST action is named DeleteConfirmed instead of Delete since both GET + POST action methods take id as a parameter. C# will not allow us to have two methods with the same signature(method name and parameters). The POST attribute is not considered part of the method signature
    {
      var thisEatery = _db.Eateries.FirstOrDefault(eatery => eatery.EateryId == id);
      _db.Eateries.Remove(thisEatery);//built in Remove method
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}