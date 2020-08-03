using Entity;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMS.DAL;

namespace project_14_7_2020.Areas.Admin.Controllers
{
    public class ManagerCategoryController : Controller
    {
        // GET: ManagerCategory
        public ActionResult Index()
        {
            CategoryContext categoryContext = new CategoryContext();
            ViewData["Data"] = categoryContext.GetCategoryByPage(1, 8);
            return View();
        }
        public IEnumerable GetCategoriesByPage(int page)
        {
            CategoryContext categoryContext = new CategoryContext();
            return JsonConvert.SerializeObject(categoryContext.GetCategoryByPage(page, 8));
        }

        [HttpGet]
        public ActionResult Create()
        {
            CategoryContext categoryContext = new CategoryContext();
            return View(categoryContext.GetAllCategories());
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            CategoryContext categoryContext = new CategoryContext();
            categoryContext.Create(category);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            CategoryContext categoryContext = new CategoryContext();
            categoryContext.Delete(Id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (category.Id < 0) return View();
            CategoryContext categoryContext = new CategoryContext();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            CategoryContext categoryContext = new CategoryContext();
            return View(categoryContext.GetCategoryById(Id)[0]);
        }
    }
}