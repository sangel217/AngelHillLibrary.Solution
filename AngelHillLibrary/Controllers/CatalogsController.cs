using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using AngelHillLibrary.Models;

namespace AngelHillLibrary.Controllers
{
  public class CatalogsController : Controller
  {

    [HttpGet("/catalogs")]
    public ActionResult Index()
    {
      List<Catalog> allCatalogs = Catalog.GetAll();
      return View(allCatalogs);
    }

    [HttpGet("/catalogs/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/catalogs")]
    public ActionResult Create(string title)
    {
      Catalog newCatalog = new Catalog(title);
      newCatalog.Save();
      List<Catalog> allTitles = Catalog.GetAll();
      return View("Index", allTitles);
    }


  }
}
