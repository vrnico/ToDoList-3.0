using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class CategoriesController : Controller
    {
        [Produces("text/html")]
        [HttpGet("/categories")]
        public ActionResult Categories()
        {
            List<Category> allCategories = Category.GetAll();
            return View(allCategories);
        }

        [Produces("text/html")]
        [HttpGet("/categories/new")]
        public ActionResult CreateForm()
        {
            return View();
        }

        [Produces("text/html")]
        [HttpPost("/categories")]
        public ActionResult Create()
        {
            Category newCategory = new Category(Request.Form["category-name"]);
            List<Category> allCategories = Category.GetAll();
            return View("Categories", allCategories);
        }
        [Produces("text/html")]
        [HttpGet("/categories/{id}")]
        public ActionResult Details(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category selectedCategory = Category.Find(id);
            List<Item> categoryItems = selectedCategory.GetItems();
            model.Add("category", selectedCategory);
            model.Add("items", categoryItems);
            return View(model);
        }

        [Produces("text/html")]
        [HttpPost("/items")]
        public ActionResult CreateItem()
        {
          Dictionary<string, object> model = new Dictionary<string, object>();
          System.Console.WriteLine(":)" + Request.Form["category-id"]);
          Category foundCategory = Category.Find(Int32.Parse(Request.Form["category-id"]));
          string itemDescription = Request.Form["item-description"];
          Item newItem = new Item(itemDescription);
          foundCategory.AddItem(newItem);
          List<Item> categoryItems = foundCategory.GetItems();
          model.Add("items", categoryItems);
          model.Add("category", foundCategory);
          return View("Details", model);
        }
    }
}
