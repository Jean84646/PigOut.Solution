using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PigOut.Models;
using MySql.Data.MySqlClient;
using System;

namespace PigOut.Controllers
{
  public class CuisinesController : Controller
  {
    [HttpGet("/cuisines")]
    public ActionResult Index()
    {
      List<CuisineType> allCuisines = CuisineType.GetAll();
      return View(allCuisines);
    }
    [HttpGet("/cuisines/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/cuisines")]
    public ActionResult Create(string cuisineName, string cuisineDescription)
    {
      string cuDescription = "";
      if (!string.IsNullOrWhiteSpace(cuisineDescription))
      {
        cuDescription = cuisineDescription;
      }
      CuisineType createCuisine = new CuisineType(cuisineName, cuDescription);
      createCuisine.Save();
      return RedirectToAction("Index");
    }
  }
}
