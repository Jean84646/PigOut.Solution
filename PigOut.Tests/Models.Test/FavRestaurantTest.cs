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
    [TestMethod]
    public void Equals_ReturnsTrueIfNameAndDescriptionsAreTheSame_FavRestaurant()
    {
      // Arrange, Act
      FavRestaurant firstFavRestaurant = new FavRestaurant("testName", "testCuisine", "testDescription", "testLocation", 1);
      FavRestaurant secondFavRestaurant = new FavRestaurant("testName", "testCuisine", "testDescription", "testLocation", 1);

      // Assert
      Assert.AreEqual(firstFavRestaurant, secondFavRestaurant);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
    //Arrange
    FavRestaurant testFavRestaurant = new FavRestaurant("testName", "testCuisine", "testDescription", "testLocation");

    //Act
    testFavRestaurant.Save();
    FavRestaurant savedFavRestaurant = FavRestaurant.GetAll()[0];

    int result = savedFavRestaurant.GetId();
    int testId = testFavRestaurant.GetId();

    //Assert
    Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Save_SavesToDatabase_FavRestaurant()
    {
    //Arrange
    FavRestaurant testFavRestaurant = new FavRestaurant("testName", "testCuisine", "testDescription", "testLocation");

    //Act
    testFavRestaurant.Save();
    List<FavRestaurant> result = FavRestaurant.GetAll();
    List<FavRestaurant> testList = new List<FavRestaurant>{testFavRestaurant};
    Console.WriteLine(result[0].GetId());
    //Assert
    CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Find_FindIdInDatabase_FavRestaurant()
    {
      //Arrange
      FavRestaurant testFavRestaurant = new FavRestaurant("testName", "testCuisine", "testDescription", "testLocation");
      testFavRestaurant.Save();

      //Act
      FavRestaurant resultRestaurant = FavRestaurant.FindById(testFavRestaurant.GetId());

      //Assert
      Assert.AreEqual(testFavRestaurant, resultRestaurant);
    }
    [TestMethod]
    public void Find_FindCuisineInDatabase_FavRestaurant()
    {
      //Arrange
      List<FavRestaurant> testList = new List<FavRestaurant> {};
      FavRestaurant testFavRestaurant = new FavRestaurant("testName", "testCuisine", "testDescription", "testLocation");
      testFavRestaurant.Save();
      testList.Add(testFavRestaurant);

      //Act
      List<FavRestaurant> resultList = FavRestaurant.FindByCuisine(testFavRestaurant.GetCuisine());

      //Assert
      CollectionAssert.AreEqual(testList, resultList);
    }
    [TestMethod]
    public void Find_FindNameInDatabase_FavRestaurant()
    {
      //Arrange
      List<FavRestaurant> testList = new List<FavRestaurant> {};
      FavRestaurant testFavRestaurant = new FavRestaurant("testName", "testCuisine", "testDescription", "testLocation");
      testFavRestaurant.Save();
      testList.Add(testFavRestaurant);

      //Act
      List<FavRestaurant> resultList = FavRestaurant.FindByName(testFavRestaurant.GetName());

      //Assert
      CollectionAssert.AreEqual(testList, resultList);
    }
    [TestMethod]
    public void Edit_UpdatesDescriptionInDatabase_String()
    {
      //Arrange
      string firstDescription = "Food";
      FavRestaurant testFavRestaurant = new FavRestaurant("testName", "testCuisine", firstDescription, "testLocation");
      testFavRestaurant.Save();
      string secondDescription = "Americana and Asian Fusion";

      //Act
      testFavRestaurant.EditDescription(secondDescription);

      string result = FavRestaurant.FindById(testFavRestaurant.GetId()).GetDescription();

      //Assert
      Assert.AreEqual(secondDescription , result);
    }
    [TestMethod]
    public void Delete_DeleteFavRestaurantEntry()
    {
      // Arrange
      List<FavRestaurant> testList = new List<FavRestaurant> {};
      FavRestaurant testFavRestaurant = new FavRestaurant("testName", "testCuisine", "testDescription", "testLocation");
      testFavRestaurant.Save();

      // Act
      FavRestaurant.FindById(testFavRestaurant.GetId()).Delete();
      List<FavRestaurant> resultList = FavRestaurant.GetAll();

      // Assert
      CollectionAssert.AreEqual(testList, resultList);
    }
  }
}
