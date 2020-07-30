using Entity;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMS.DAL;

namespace project_14_7_2020.Areas.Admin.Controllers
{
    public class ManagerNewsController : Controller
    {
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            NewsContext newsContext = new NewsContext();
            ViewData["news"] = newsContext.GetNewsById(Id);
            CategoryContext categoryContext = new CategoryContext();
            ViewData["ListCategories"] = categoryContext.GetAllCategories();
            return View();
        }

        [HttpGet]
        public ActionResult View(int Id)
        {
            NewsContext newsBL = new NewsContext();
            return View(newsBL.GetNewsById(Id));
        }

        [HttpPost]
        public ActionResult Edit(News news, HttpPostedFileBase fileAvatar)
        {
            String name = fileAvatar.FileName.ToString();
            news.Avatar = name;
            NewsContext newsBL = new NewsContext();
            if (newsBL.Update(news) >= 0)
            {
                string SaveLocation = Server.MapPath("~/Content/imgs") + "\\" + name;
                try
                {
                    fileAvatar.SaveAs(SaveLocation);
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
                return RedirectToAction("Index");
            }
            else return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            NewsContext newsContext = new NewsContext();
            newsContext.Delete(id); 
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult index()
        {
            NewsContext newsBL = new NewsContext();
            CategoryContext categoryBL = new CategoryContext();
            ViewData["ListNews"] = newsBL.GetAllNews();
            ViewData["ListCategories"] = categoryBL.GetAllCategories();
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(News news, HttpPostedFileBase fileAvatar)
        {
            NewsContext newsBL = new NewsContext();
            String nameFile= System.IO.Path.GetFileName(fileAvatar.FileName);
            news.Avatar = nameFile;
            if (newsBL.Create(news) > 0)
            {
                string SaveLocation = Server.MapPath("~/Content/imgs") + "\\" + nameFile;
                try
                {
                    fileAvatar.SaveAs(SaveLocation);
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
            return RedirectToAction("Index");
        }

    }
}