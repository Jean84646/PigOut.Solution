using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using PigOut;

namespace PigOut.Models
{
  public class CuisineType
  {
    private string Cuisine;
    private string Description;

    public CuisineType(string newCuisine, string newDescription = "")
    {
      Cuisine = newCuisine;
      Description = newDescription;
    }
    public string GetCuisine()
    {
      return Cuisine;
    }
    public string GetDescription()
    {
      return Description;
    }

    public override bool Equals(System.Object otherCuisine)
    {
      if (!(otherCuisine is CuisineType))
      {
        return false;
      }
      else
      {
        CuisineType newCuisine = (CuisineType) otherCuisine;
        bool cuisineEqual = (this.GetCuisine() == newCuisine.GetCuisine());
        bool descriptionEqual = (this.GetDescription() == newCuisine.GetDescription());
        return (cuisineEqual && descriptionEqual);
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cuisine (cuisine_id, description) VALUES (@cuisinePara, @descriptionPara);";
      MySqlParameter paraCuisine = new MySqlParameter();
      paraCuisine.ParameterName = "@cuisinePara";
      paraCuisine.Value = this.Cuisine;
      cmd.Parameters.Add(paraCuisine);
      MySqlParameter paraDescription = new MySqlParameter();
      paraDescription.ParameterName = "@descriptionPara";
      paraDescription.Value = this.Description;
      cmd.Parameters.Add(paraDescription);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }
    public static List<CuisineType> GetAll()
    {
      List<CuisineType> allCuisines = new List<CuisineType> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cuisine;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string cuisine = rdr.GetString(0);
        string description = rdr.GetString(1);
        CuisineType newCuisine = new CuisineType(cuisine, description);
        allCuisines.Add(newCuisine);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCuisines;
    }
    public static List<CuisineType> FindByCuisine(string myCuisine)
    {
      List<CuisineType> foundCuisines = new List<CuisineType> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cuisine WHERE cuisine_id = @Cuisine;";
      MySqlParameter searchCuisine = new MySqlParameter();
      searchCuisine.ParameterName = "@Cuisine";
      searchCuisine.Value = myCuisine;
      cmd.Parameters.Add(searchCuisine);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string cuisine = rdr.GetString(0);
        string description = rdr.GetString(1);
        CuisineType newCuisine = new CuisineType(cuisine, description);
        foundCuisines.Add(newCuisine);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundCuisines;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cuisine;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
