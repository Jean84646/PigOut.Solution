using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PigOut.Models;
using MySql.Data.MySqlClient;
using System;

namespace PigOut.Controllers
{
  public class FavRestaurantsController : Controller
  {
    [HttpGet("/FavRestaurant")]
    public ActionResult Index()
    {
      List<FavRestaurant> allRestaurants = FavRestaurant.GetAll();
      return View(allRestaurants);
    }
  }
}
