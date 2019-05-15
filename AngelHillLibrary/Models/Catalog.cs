using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AngelHillLibrary.Models
{
  public class Catalog
  {
      private string _title;
      private int _id;

      public Catalog(string title, int id = 0)
      {
          _title = title;
          _id = id;
      }
      public string GetTitle()
      {
          return _title;
      }

      public int GetId()
      {
          return _id;
      }

      public override int GetHashCode()
      {
          return this.GetId().GetHashCode();
      }

      public override bool Equals(System.Object otherCatalog)
      {
        if (!(otherCatalog is Catalog))
        {
          return false;
        }
        else
        {
          Catalog newCatalog = (Catalog) otherCatalog;
          bool idEquality = this.GetId().Equals(newCatalog.GetId());
          bool titleEquality = this.GetTitle().Equals(newCatalog.GetTitle());
          return (idEquality && titleEquality);
        }
      }

      public static void ClearAll()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM catalogs;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
         conn.Dispose();
        }
      }

      public void Save()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO catalogs (book_title) VALUES (@title);";
        MySqlParameter title = new MySqlParameter();
        title.ParameterName = "@title";
        title.Value = this._title;
        cmd.Parameters.Add(title);
        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public static List<Catalog> GetAll()
      {
        List<Catalog> allCatalogs = new List<Catalog> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM catalogs;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int CatalogId = rdr.GetInt32(0);
          string CatalogTitle = rdr.GetString(1);
          Catalog newCatalog = new Catalog(CatalogTitle, CatalogId);
          allCatalogs.Add(newCatalog);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allCatalogs;
      }


      public static Catalog Find(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM catalogs WHERE id = (@searchId);";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int CatalogId = 0;
        string CatalogTitle = "";
        while(rdr.Read())
        {
          CatalogId = rdr.GetInt32(0);
          CatalogTitle = rdr.GetString(1);
        }
        Catalog newCatalog = new Catalog(CatalogTitle, CatalogId);
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return newCatalog;
      }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = new MySqlCommand("DELETE FROM catalogs WHERE id = @CatalogId;", conn);
      MySqlParameter catalogIdParameter = new MySqlParameter();
      catalogIdParameter.ParameterName = "@CatalogId";
      catalogIdParameter.Value = this.GetId();
      cmd.Parameters.Add(catalogIdParameter);
      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Edit(string newTitle)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE catalogs SET book_title = @newTitle WHERE id = @searchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);
      MySqlParameter title = new MySqlParameter();
      title.ParameterName = "@newTitle";
      title.Value = newTitle;
      cmd.Parameters.Add(title);
      cmd.ExecuteNonQuery();
      _title = newTitle;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

    }

    public List<Author> GetAuthors()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT author_id FROM catalogs_authors WHERE catalog_id = @CatalogId;";
      MySqlParameter catalogIdParameter = new MySqlParameter();
      catalogIdParameter.ParameterName = "@CatalogId";
      catalogIdParameter.Value = _id;
      cmd.Parameters.Add(catalogIdParameter);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<int> authorIds = new List<int> {};
      while(rdr.Read())
      {
        int authorId = rdr.GetInt32(0);
        authorIds.Add(authorId);
      }
      rdr.Dispose();
      List<Author> authors = new List<Author> {};
      foreach (int authorId in authorIds)
      {
        var authorQuery = conn.CreateCommand() as MySqlCommand;
        authorQuery.CommandText = @"SELECT * FROM authors WHERE id = @AuthorId;";
        MySqlParameter authorIdParameter = new MySqlParameter();
        authorIdParameter.ParameterName = "@AuthorId";
        authorIdParameter.Value = authorId;
        authorQuery.Parameters.Add(authorIdParameter);
        var authorQueryRdr = authorQuery.ExecuteReader() as MySqlDataReader;
        while(authorQueryRdr.Read())
        {
          int thisAuthorId = authorQueryRdr.GetInt32(0);
          string authorName = authorQueryRdr.GetString(1);
          Author foundAuthor = new Author(authorName, thisAuthorId);
          authors.Add(foundAuthor);
        }
        authorQueryRdr.Dispose();
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return authors;
    }


  }
}
