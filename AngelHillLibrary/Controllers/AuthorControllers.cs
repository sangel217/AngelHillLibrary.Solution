using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using AngelHillLibrary.Models;

namespace AngelHillLibrary.Controllers
{
  public class AuthorsController : Controller
  {
    [HttpGet("/authors")]
    public ActionResult Index()
    {
      List<Author> allAuthors = Author.GetAll();
      return View(allAuthors);
    }

    [HttpGet("/authors/new")]
    public ActionResult New()
    {
       return View();
    }

    [HttpPost("/authors")]
    public ActionResult Create(string authorName)
    {
      Author newAuthor = new Author(authorName);
      newAuthor.Save();
      List<Author> allAuthors = Author.GetAll();
      return View("Index", allAuthors);
    }


    [HttpPost("/authors/delete")]
    public ActionResult DeleteAll()
    {
      Author.ClearAll();
      return View();
    }

    [HttpGet("/authors/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Author selectedAuthor = Author.Find(id);
      List<Catalog> authorCatalogs = selectedAuthor.GetCatalogs();
      List<Catalog> allCatalogs = Catalog.GetAll();
      model.Add("selectedAuthor", selectedAuthor);
      model.Add("authorCatalogs", authorCatalogs);
      model.Add("allCatalogs", allCatalogs);
      return View(model);
    }

    [HttpGet("/catalogs/{catalogId}/authors/{authorId}/edit")]
    public ActionResult Edit(int catalogId, int authorId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Catalog catalog = Catalog.Find(catalogId);
      model.Add("catalog", catalog);
      Author author = Author.Find(authorId);
      model.Add("author", author);
      return View(model);
    }

    [HttpPost("/catalogs/{catalogId}/authors/{authorId}")]
    public ActionResult Update(int catalogId, int authorId, string newAuthorName)
    {
      Author author = Author.Find(authorId);
      author.Edit(newAuthorName);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Catalog catalog = Catalog.Find(catalogId);
      model.Add("catalog", catalog);
      model.Add("author", author);
      return View("Show", model);
    }

    [HttpPost("/authors/{authorId}/delete")]
    public ActionResult Delete(int authorId)
    {
      Author author = Author.Find(authorId);
      author.Delete();
      return RedirectToAction("Index");
    }

    [HttpPost("/authors/{authorId}/catalogs/new")]
    public ActionResult AddCatalog(int authorId, int catalogId)
    {
      Author author = Author.Find(authorId);
      Catalog catalog = Catalog.Find(catalogId);
      author.AddCatalog(catalog);
      return RedirectToAction("Show",  new { id = authorId });
    }
  }
}
