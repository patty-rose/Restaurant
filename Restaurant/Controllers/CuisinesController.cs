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
  }
}