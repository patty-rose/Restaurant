using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;//to access SelectList
using Microsoft.EntityFrameworkCore;//to access EntityState
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;//to use LINQ's ToList() method

namespace Restaurant.Controllers
{
  public class CuisinesController : Controller
  {
    private readonly RestaurantContext _db; // our database declared as an object, so it connect

    public CuisinesController(RestaurantContext db) // 
    {
      _db = db; // database object
    }

    [HttpGet("/Cuisines")]
    public ActionResult Index()
    {
      List<Cuisine> model = _db.Cuisines.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.CuisineId = new SelectList(_db.Cuisines, "CuisineId", "CuisineType");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Cuisine cuisine)
    {
      _db.Cuisines.Add(cuisine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Cuisine thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
      return View(thisCuisine);
    }

    public ActionResult Delete(int id)
    {
      var thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
      return View(thisCuisine);
    }
    //POST action is named DeleteConfirmed instead of Delete since both GET + POST action methods take id as a parameter. C# will not allow us to have two methods with the same signature(method name and parameters). The POST attribute is not considered part of the method signature 

    [HttpPost, ActionName("Delete")] //Note that our annotation includes [ActionName("Delete")]. This is so we can still utilize the proper Delete action even though we've named our method DeleteConfirmed.
    public ActionResult DeleteConfirmed(int id)
    {
      var thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
      foreach(Eatery eatery in thisCuisine.Eateries)
      {
        _db.Eateries.Remove(eatery);
      }
      _db.Cuisines.Remove(thisCuisine);//built in Remove method
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    //GET action routes to page with a form for updating item
    public ActionResult Edit(int id)
    {
      Cuisine thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
      ViewBag.CuisineId = new SelectList(_db.Cuisines, "CuisineId", "CuisineType");
      return View(thisCuisine);
    }//finding specific eatery and passing it into view

    [HttpPost]//POST actually updates item
    public ActionResult Edit(Cuisine cuisine)
    {
      _db.Entry(cuisine).State = EntityState.Modified;//We find and update all of the properties of the item we are editing by passing the item (our route parameter) itself into the Entry() method. Then we need to update its State property to EntityState.Modified. This is so Entity knows that the entry has been modified, as it is not explicitly tracking it (we never actually retrieved the item from the database).
      _db.SaveChanges();//once entry's state is marked as modified we can save and then redirect to Index view
      return RedirectToAction("Index");
    }
  }
}