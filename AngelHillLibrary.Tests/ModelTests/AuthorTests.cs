using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using AngelHillLibrary.Models;

namespace AngelHillLibrary.Tests
{
  [TestClass]
  public class AuthorTest : IDisposable
  {
    public AuthorTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=angel_hill_library_test;";
    }

    public void Dispose()
    {
      Author.ClearAll();
    }

    [TestMethod]
    public void AuthorConstructor_CreatesInstanceOfAuthor_Author()
    {
        string name = "Test Author";
        Author newAuthor = new Author(name);
        Assert.AreEqual(typeof(Author), newAuthor.GetType());
    }

    [TestMethod]
    public void GetAuthorName_ReturnsAuthorName_String()
    {
        string authorName = "Test Author";
        Author newAuthor = new Author(authorName);
        string actualResult = newAuthor.GetAuthorName();
        Assert.AreEqual(authorName, actualResult);
    }

    [TestMethod]
    public void GetId_ReturnsAuthorId_Int()
    {
        string authorName = "Test Author";
        Author newAuthor = new Author(authorName);
        int actualResult = newAuthor.GetId();
        Assert.AreEqual(0, actualResult);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfAuthorNamesAreTheSame_Author()
    {
      //Arrange, Act
      Author firstAuthor = new Author("Test Author");
      Author secondAuthor = new Author("Test Author");

      //Assert
      Assert.AreEqual(firstAuthor, secondAuthor);
    }

    [TestMethod]
    public void Save_SavesAuthorToDatabase_AuthorList()
    {
      //Arrange
      Author testAuthor = new Author("Test Author");
      testAuthor.Save();

      //Act
      List<Author> result = Author.GetAll();
      List<Author> testList = new List<Author>{testAuthor};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Save_DatabaseAssignsIdToAuthor_Id()
    {
      //Arrange
      Author testAuthor = new Author("Test author");
      testAuthor.Save();

      //Act
      Author savedAuthor = Author.GetAll()[0];

      int result = savedAuthor.GetId();
      int testId = testAuthor.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void GetAll_AuthorsEmptyAtFirst_List()
    {
      //Arrange, Act
      int result = Author.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
     public void GetAll_ReturnsAllAuthorObjects_AuthorList()
     {
         string title01 = "Book";
         string title02 = "Book2";
         Author newAuthor1 = new Author(title01);
         newAuthor1.Save();
         Author newAuthor2 = new Author(title02);
         newAuthor2.Save();
         List<Author> newList = new List<Author> { newAuthor1, newAuthor2 };

         List<Author> actualResult = Author.GetAll();
         CollectionAssert.AreEqual(newList, actualResult);
     }

     [TestMethod]
     public void Find_ReturnsAuthorInDatabase_Author()
     {
       //Arrange
       Author testAuthor = new Author("Test Author");
       testAuthor.Save();

       //Act
       Author foundAuthor = Author.Find(testAuthor.GetId());

       //Assert
       Assert.AreEqual(testAuthor, foundAuthor);
     }

     [TestMethod]
     public void Delete_DeletesAuthorFromDatabase_Author()
     {
       //Arrange
       string testAuthorName = "Test Author";
       Author testAuthor = new Author(testAuthorName);
       testAuthor.Save();

       //Act
       Author foundAuthor = Author.Find(testAuthor.GetId());
       foundAuthor.Delete();
       List<Author> newList = new List<Author>{};
       List<Author> testList = Author.GetAll();
       //Assert
       CollectionAssert.AreEqual(newList, testList);
     }

     [TestMethod]
     public void Edit_UpdatesAuthorInDatabase_String()
     {
       Author testAuthor = new Author("Test Author");
       testAuthor.Save();
       string secondAuthorName = "Updated Author";

       testAuthor.Edit(secondAuthorName);
       string result = Author.Find(testAuthor.GetId()).GetAuthorName();

       Assert.AreEqual(secondAuthorName, result);
     }
  }
}
