using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AngelHillLibrary.Models
{
  public class Author
  {
      private string _authorName;
      private int _id;

      public Author(string authorName, int id = 0)
      {
        _authorName = authorName;
        _id = id;
      }

      public string GetAuthorName()
      {
        return _authorName;
      }

      public int GetId()
      {
        return _id;
      }

      public override int GetHashCode()
      {
          return this.GetId().GetHashCode();
      }

      public override bool Equals(System.Object otherAuthor)
      {
        if (!(otherAuthor is Author))
        {
          return false;
        }
        else
        {
          Author newAuthor = (Author) otherAuthor;
          bool idEquality = this.GetId().Equals(newAuthor.GetId());
          bool authorNameEquality = this.GetAuthorName().Equals(newAuthor.GetAuthorName());
          return (idEquality && authorNameEquality);
        }
      }

      public static void ClearAll()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM authors;";
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
        cmd.CommandText = @"INSERT INTO authors (author_name) VALUES (@authorName);";
        MySqlParameter authorName = new MySqlParameter();
        authorName.ParameterName = "@authorName";
        authorName.Value = this._authorName;
        cmd.Parameters.Add(authorName);
        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public static List<Author> GetAll()
      {
        List<Author> allAuthors = new List<Author> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM authors;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int AuthorId = rdr.GetInt32(0);
          string AuthorName = rdr.GetString(1);
          Author newAuthor = new Author(AuthorName, AuthorId);
          allAuthors.Add(newAuthor);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allAuthors;
      }

      public static Author Find(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM authors WHERE id = (@searchId);";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int AuthorId = 0;
        string AuthorName = "";
        while(rdr.Read())
        {
          AuthorId = rdr.GetInt32(0);
          AuthorName = rdr.GetString(1);
        }
        Author newAuthor = new Author(AuthorName, AuthorId);
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return newAuthor;
      }

      public void Delete()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = new MySqlCommand("DELETE FROM authors WHERE id = @authorNameId;", conn);
        MySqlParameter authorNameIdParameter = new MySqlParameter();
        authorNameIdParameter.ParameterName = "@authorNameId";
        authorNameIdParameter.Value = this.GetId();
        cmd.Parameters.Add(authorNameIdParameter);
        cmd.ExecuteNonQuery();
        if (conn != null)
        {
          conn.Close();
        }
      }

      public void Edit(string newAuthorName)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE authors SET author_name = @newAuthorName WHERE id = @searchId;";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add(searchId);
        MySqlParameter authorName = new MySqlParameter();
        authorName.ParameterName = "@newAuthorName";
        authorName.Value = newAuthorName;
        cmd.Parameters.Add(authorName);
        cmd.ExecuteNonQuery();
        _authorName = newAuthorName;
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }

      }
  }
}
