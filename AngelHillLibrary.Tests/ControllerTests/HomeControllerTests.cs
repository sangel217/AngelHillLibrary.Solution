using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngelHillLibrary.Controllers;
using AngelHillLibrary.Models;

namespace AngelHillLibrary.Tests
{
    [TestClass]
    public class HomeControllerTest
    {

        [TestMethod]
        public void Index_ReturnsCorrectView_True ()
        {
            //Arrange
            HomeController controller = new HomeController ();

            //Act
            ActionResult indexView = controller.Index ();

            //Assert
            Assert.IsInstanceOfType (indexView, typeof (ViewResult));
        }
    }
}
