using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using AngelHillLibrary.Models;

namespace AngelHillLibrary.Tests

{
  [TestClass]
  public class CatalogTest : IDisposable
  {
    public CatalogTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=angel_hill_library_test;";
    }

    public void Dispose()
    {
    //   Item.ClearAll();
      Catalog.ClearAll();
    }

    [TestMethod]
    public void CatalogConstructor_CreatesInstanceOfCatalog_Catalog()
    {
        string title = "Test Catalog";
        Catalog newCatalog = new Catalog(title);
        Assert.AreEqual(typeof(Catalog), newCatalog.GetType());
    }

    [TestMethod]
    public void GetTitle_ReturnsTitle_String()
    {
        string title = "Test Catalog";
        Catalog newCatalog = new Catalog(title);
        string actualResult = newCatalog.GetTitle();
        Assert.AreEqual(title, actualResult);
    }

    [TestMethod]
    public void GetId_ReturnsCatalogId_Int()
    {
        string title = "Test Catalog";
        Catalog newCatalog = new Catalog(title);
        int actualResult = newCatalog.GetId();
        Assert.AreEqual(0, actualResult);
    }

    [TestMethod]
    public void Save_SavesCatalogToDatabase_CatalogList()
    {
      //Arrange
      Catalog testCatalog = new Catalog("Test Catalog");
      testCatalog.Save();

      //Act
      List<Catalog> result = Catalog.GetAll();
      List<Catalog> testList = new List<Catalog>{testCatalog};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Save_DatabaseAssignsIdToCatalog_Id()
    {
      //Arrange
      Catalog testCatalog = new Catalog("Test catalog");
      testCatalog.Save();

      //Act
      Catalog savedCatalog = Catalog.GetAll()[0];

      int result = savedCatalog.GetId();
      int testId = testCatalog.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void GetAll_CatalogsEmptyAtFirst_List()
    {
      //Arrange, Act
      int result = Catalog.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfTitlesAreTheSame_Catalog()
    {
      //Arrange, Act
      Catalog firstCatalog = new Catalog("Test Catalog");
      Catalog secondCatalog = new Catalog("Test Catalog");

      //Assert
      Assert.AreEqual(firstCatalog, secondCatalog);
    }

    [TestMethod]
     public void GetAll_ReturnsAllCatalogObjects_CatalogList()
     {
         string title01 = "Book";
         string title02 = "Book2";
         Catalog newCatalog1 = new Catalog(title01);
         newCatalog1.Save();
         Catalog newCatalog2 = new Catalog(title02);
         newCatalog2.Save();
         List<Catalog> newList = new List<Catalog> { newCatalog1, newCatalog2 };

         List<Catalog> actualResult = Catalog.GetAll();
         CollectionAssert.AreEqual(newList, actualResult);
     }


    [TestMethod]
    public void Find_ReturnsCatalogInDatabase_Catalog()
    {
      //Arrange
      Catalog testCatalog = new Catalog("Test Catalog");
      testCatalog.Save();

      //Act
      Catalog foundCatalog = Catalog.Find(testCatalog.GetId());

      //Assert
      Assert.AreEqual(testCatalog, foundCatalog);
    }

    [TestMethod]
    public void Delete_DeletesCatalogFromDatabase_Catalog()
    {
      //Arrange
      string testTitle = "Test Catalog";
      Catalog testCatalog = new Catalog(testTitle);
      testCatalog.Save();

      //Act
      Catalog foundCatalog = Catalog.Find(testCatalog.GetId());
      foundCatalog.Delete();
      List<Catalog> newList = new List<Catalog>{};
      List<Catalog> testList = Catalog.GetAll();
      // Catalog resultCatalogs = new Catalog("");

      //Assert
      CollectionAssert.AreEqual(newList, testList);
    }

    [TestMethod]
    public void Edit_UpdatesCatalogInDatabase_String()
    {
      Catalog testCatalog = new Catalog("Test Catalog");
      testCatalog.Save();
      string secondTitle = "Updated Catalog";

      testCatalog.Edit(secondTitle);
      string result = Catalog.Find(testCatalog.GetId()).GetTitle();

      Assert.AreEqual(secondTitle, result);
    }




  }
}
