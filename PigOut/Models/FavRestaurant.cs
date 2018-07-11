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

    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is FavRestaurant))
      {
        return false;
      }
      else
      {
        FavRestaurant newRestaurant = (FavRestaurant) otherRestaurant;
        bool idEqual = (this.GetId() == newRestaurant.GetId());
        bool nameEqual = (this.GetName() == newRestaurant.GetName());
        bool descriptionEqual = (this.GetDescription() == newRestaurant.GetDescription());
        bool locationEqual = (this.GetLocation() == newRestaurant.GetLocation());
        bool cuisineEqual = (this.GetCuisine() == newRestaurant.GetCuisine());
        return (idEqual && nameEqual && descriptionEqual && locationEqual && cuisineEqual);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO fav_restaurant (name, description, location, cuisine_id) VALUES (@inputName, @inputDescription, @inputLocation, @inputCuisine);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@inputName";
      newName.Value = this.Name;
      cmd.Parameters.Add(newName);
      MySqlParameter newDescription = new MySqlParameter();
      newDescription.ParameterName = "@inputDescription";
      newDescription.Value = this.Description;
      cmd.Parameters.Add(newDescription);
      MySqlParameter newLocation = new MySqlParameter();
      newLocation.ParameterName = "@inputLocation";
      newLocation.Value = this.Location;
      cmd.Parameters.Add(newLocation);
      MySqlParameter newCuisine = new MySqlParameter();
      newCuisine.ParameterName = "@inputCuisine";
      newCuisine.Value = this.Cuisine;
      cmd.Parameters.Add(newCuisine);
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }

    public static List<FavRestaurant> GetAll()
    {
      List<FavRestaurant> allRestaurants = new List<FavRestaurant> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM fav_restaurant;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string description = rdr.GetString(2);
        string location = rdr.GetString(3);
        string cuisine = rdr.GetString(4);
        FavRestaurant newRestaurant = new FavRestaurant(name, cuisine, description, location, id);
        allRestaurants.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allRestaurants;
    }

    public static FavRestaurant FindById(int byId)
    {
      int id = 0;
      string name = "";
      string description = "";
      string location = "";
      string cuisine = "";
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM fav_restaurant WHERE id = @idPara;";
      MySqlParameter paraId = new MySqlParameter();
      paraId.ParameterName = "@idPara";
      paraId.Value = byId;
      cmd.Parameters.Add(paraId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
        description = rdr.GetString(2);
        location = rdr.GetString(3);
        cuisine = rdr.GetString(4);
      }
      FavRestaurant newRestaurant = new FavRestaurant(name, cuisine, description, location, id);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newRestaurant;
    }

    public static List<FavRestaurant> FindByCuisine(string myCuisine)
    {
      List<FavRestaurant> foundRestaurants = new List<FavRestaurant> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM fav_restaurant WHERE cuisine_id = @Cuisine;";
      MySqlParameter searchCuisine = new MySqlParameter();
      searchCuisine.ParameterName = "@Cuisine";
      searchCuisine.Value = myCuisine;
      cmd.Parameters.Add(searchCuisine);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string description = rdr.GetString(2);
        string location = rdr.GetString(3);
        string cuisine = rdr.GetString(4);
        FavRestaurant newRestaurant = new FavRestaurant(name, cuisine, description, location, id);
        foundRestaurants.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundRestaurants;
    }

    public static List<FavRestaurant> FindByName(string byName)
    {
      List<FavRestaurant> foundRestaurants = new List<FavRestaurant> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM fav_restaurant WHERE name LIKE @Name;";
      MySqlParameter searchName = new MySqlParameter();
      searchName.ParameterName = "@Name";
      searchName.Value = byName + '%';
      cmd.Parameters.Add(searchName);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string description = rdr.GetString(2);
        string location = rdr.GetString(3);
        string cuisine = rdr.GetString(4);
        FavRestaurant newRestaurant = new FavRestaurant(name, cuisine, description, location, id);
        foundRestaurants.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundRestaurants;
    }

    public void EditDescription(string newDescription)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE fav_restaurant SET description = @descriptionPara WHERE id = @idPara;";
      MySqlParameter paraDescription = new MySqlParameter();
      paraDescription.ParameterName = "@descriptionPara";
      paraDescription.Value = newDescription;
      cmd.Parameters.Add(paraDescription);
      MySqlParameter paraId = new MySqlParameter();
      paraId.ParameterName = "@idPara";
      paraId.Value = this.Id;
      cmd.Parameters.Add(paraId);
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM fav_restaurant WHERE id=@thisId;";
      MySqlParameter deleteId = new MySqlParameter();
      deleteId.ParameterName = "@thisId";
      deleteId.Value = this.Id;
      cmd.Parameters.Add(deleteId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
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
