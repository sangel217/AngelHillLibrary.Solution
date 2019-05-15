using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngelHillLibrary.Controllers;
using AngelHillLibrary.Models;

namespace AngelHillLibrary.Tests
{
    [TestClass]
    public class CatalogControllerTest
    {
      [TestMethod]
      public void Index_HasCorrectModelType_CatalogList()
      {
          //Arrange
          CatalogsController controller = new CatalogsController();
          ViewResult indexView = controller.Index() as ViewResult;

          //Act
          var result = indexView.ViewData.Model;

          //Assert
          Assert.IsInstanceOfType(result, typeof(List<Catalog>));
      }

      [TestMethod]
      public void New_ReturnsCorrectView_True()
      {
        CatalogsController controller = new CatalogsController();
        ActionResult newView = controller.New();
        Assert.IsInstanceOfType(newView, typeof (ViewResult));
      }

      [TestMethod]
      public void Show_ReturnsCorrectView_True()
      {
        CatalogsController controller = new CatalogsController();
        ActionResult showView = controller.Show(1);
        Assert.IsInstanceOfType(showView, typeof(ViewResult));
      }

    }
}
