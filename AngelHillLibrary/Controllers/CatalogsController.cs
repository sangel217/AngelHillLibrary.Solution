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
      List<Catalog> allTitles = Catalog.GetAll();
      return View(allTitles);
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

    [HttpGet("/catalogs/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Catalog selectedCatalog = Catalog.Find(id);
      List<Author> catalogAuthors = selectedCatalog.GetAuthors();
      List<Author> allAuthors = Author.GetAll();
      model.Add("catalog", selectedCatalog);
      model.Add("catalogAuthors", catalogAuthors);
      model.Add("allAuthors", allAuthors);
      return View(model);
    }

    [HttpPost("/catalogs/{catalogId}/authors/new")]
    public ActionResult AddAuthor(int catalogId, int authorId)
    {
      Catalog catalog = Catalog.Find(catalogId);
      Author author = Author.Find(authorId);
      catalog.AddAuthor(author);
      return RedirectToAction("Show",  new { id = catalogId });
    }

    [HttpPost("/catalogs/{catalogId}/delete")]
    public ActionResult Delete(int catalogId)
    {
      Catalog catalog = Catalog.Find(catalogId);
      catalog.Delete();
      return RedirectToAction("Index");
    }

    [HttpGet("/catalogs/{catalogId}/edit")]
    public ActionResult Edit(int catalogId)
    {
      Catalog catalog = Catalog.Find(catalogId);
      return View(catalog);
    }

    [HttpPost("catalogs/{id}")]
    public ActionResult Update(int id, string newTitle)
    {
      Catalog catalog = Catalog.Find(id);
      catalog.Edit(newTitle);
      return RedirectToAction("Index");
    }

    [HttpGet("/search_by_title")]
    public ActionResult SearchByTitle()
    {
    	return View();
    }


    [HttpPost("/search_by_title")]
    public ActionResult Index(string title)
    {
    	List<Catalog> searchTitle = Catalog.SearchTitle(title);
    	return View(searchTitle);
    }



  }
}
