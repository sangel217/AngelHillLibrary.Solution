using Microsoft.AspNetCore.Mvc;
using AngelHillLibrary.Models;
using System.Collections.Generic;

namespace AngelHillLibrary.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
        return View();
    }
  }
}
