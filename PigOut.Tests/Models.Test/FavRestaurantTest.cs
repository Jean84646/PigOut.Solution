using Microsoft.VisualStudio.TestTools.UnitTesting;
using PigOut.Models;
using System;
using System.Collections.Generic;

namespace PigOut.Tests
{
  [TestClass]
  public class FavRestaurantTests : IDisposable
  {
    public void Dispose()
    {
      FavRestaurant.DeleteAll();
    }
    public FavRestaurantTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=restaurant_test;";
    }
    [TestMethod]
    public void GetTest_ReturnDataField()
    {
      // arrange
      int id = 1;
      string name = "Pig House";
      string description = "Best of Americana and Asian.";
      string location = "Seattle";
      string cuisine = "Asian Fusion";
      FavRestaurant testRestaurant = new FavRestaurant(name, cuisine, description, location, id);

      // act
      int resultId = testRestaurant.GetId();
      string resultName = testRestaurant.GetName();
      string resultDescription = testRestaurant.GetDescription();
      string resultLocation = testRestaurant.GetLocation();
      string resultCuisine = testRestaurant.GetCuisine();

      // assert
      Assert.AreEqual(id, resultId);
      Assert.AreEqual(name, resultName);
      Assert.AreEqual(description, resultDescription);
      Assert.AreEqual(location, resultLocation);
      Assert.AreEqual(cuisine, resultCuisine);
    }
  }
}
