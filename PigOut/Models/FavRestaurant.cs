using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using PigOut;

namespace PigOut.Models
{
  public class FavRestaurant
  {
    private int Id;
    private string Name;
    private string Description;
    private string Location;
    private string Cuisine;

    public FavRestaurant(string newName, string newCuisine, string newDescription = "", string newLocation = "", int newId =0)
    {
      Id = newId;
      Name = newName;
      Description = newDescription;
      Location = newLocation;
      Cuisine = newCuisine;
    }
    public int GetId()
    {
      return Id;
    }
    public string GetName()
    {
      return Name;
    }
    public string GetDescription()
    {
      return Description;
    }
    public string GetLocation()
    {
      return Location;
    }
    public string GetCuisine()
    {
      return Cuisine;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM fav_restaurant;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
