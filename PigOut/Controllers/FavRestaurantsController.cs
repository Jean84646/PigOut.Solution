using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PigOut.Models;
using MySql.Data.MySqlClient;
using System;

namespace PigOut.Controllers
{
  public class FavRestaurantsController : Controller
  {
    [HttpGet("/favrestaurants")]
    public ActionResult Index()
    {
      List<FavRestaurant> allRestaurants = FavRestaurant.GetAll();
      return View(allRestaurants);
    }
    [HttpGet("/favrestaurants/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/favrestaurants")]
    public ActionResult Create(string restName, string restType, string restLocation, string restDescription)
    {
      string restaurantDescription = "";
      string restaurantLocation = "";
      if(!string.IsNullOrWhiteSpace(Request.Form["restDescription"]))
      {
        restaurantDescription = restDescription;
      }
      if(!string.IsNullOrWhiteSpace(Request.Form["restLocation"]))
      {
        restaurantLocation = restLocation;
      }
      FavRestaurant createRestaurant = new FavRestaurant(restName,  restType,  restaurantLocation, restaurantDescription);
      createRestaurant.Save();
      return RedirectToAction("Index");
    }
    [HttpGet("/favrestaurants/{id}")]
    public ActionResult EditForm(int id)
    {
      return View(FavRestaurant.FindById(id));
    }
    [HttpPost("/favrestaurants/delete")]
    public ActionResult Delete(string deleteId)
    {
      int id = int.Parse(deleteId);
      FavRestaurant.FindById(id).Delete();
      return RedirectToAction("Index");
    }
    [HttpPost("/favrestaurants/edit")]
    public ActionResult Edit(string editDescription, string EditId)
    {
      int id = int.Parse(EditId);
      FavRestaurant.FindById(id).EditDescription(editDescription);
      return RedirectToAction("Index");
    }
    [HttpGet("/favrestaurants/cuisines/{cuisine}")]
    public ActionResult FindByCuisine(string cuisine)
    {
      List<FavRestaurant> matchCuisines = FavRestaurant.FindByCuisine(cuisine);
      return View("Index", matchCuisines);
    }
    [HttpGet("/favrestaurants/search")]
    public ActionResult SearchForm()
    {
      return View();
    }
    [HttpPost("/favrestaurants/search")]
    public ActionResult SearchRestaurant(string nameofRestaurant)
    {
      List<FavRestaurant> foundRestaurants = FavRestaurant.FindByName(nameofRestaurant);
      return View("Index", foundRestaurants);
    }

  }
}
