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
        private int pageSize = 7;

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
            if (fileAvatar == null) news.Avatar = null;
            else news.Avatar = fileAvatar.FileName.ToString();
            NewsContext newsBL = new NewsContext();
            if (newsBL.Update(news) >= 0)
            {
                string SaveLocation = Server.MapPath("~/Content/imgs") + "\\" + news.Avatar;
                if (fileAvatar != null) { 
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
            Tuple<List<News>, int> getDatas = newsBL.GetNewsOfPage(-1, 1, this.pageSize);
            int NumPage = getDatas.Item2 / this.pageSize;
            int div = getDatas.Item2 % this.pageSize;
            if (div > 0) NumPage++;
            ViewData["ListNews"] = Tuple.Create(getDatas.Item1, NumPage);
            ViewData["ListCategories"] = categoryBL.GetAllCategories();
            return View();
        }
        public ActionResult index(int page)
        {
            NewsContext newsBL = new NewsContext();
            CategoryContext categoryBL = new CategoryContext();
            Tuple<List<News>, int> getDatas = newsBL.GetNewsOfPage(-1, page, this.pageSize);
            int NumPage = getDatas.Item2 / this.pageSize;
            int div = getDatas.Item2 % this.pageSize;
            if (div > 0) NumPage++;
            ViewData["ListNews"] = Tuple.Create(getDatas.Item1, NumPage);
            ViewData["ListCategories"] = categoryBL.GetAllCategories();
            return View();
        }
        [HttpGet]
        public IEnumerable GetNewByPage(int page)
        {
            NewsContext newsBL = new NewsContext();
            return JsonConvert.SerializeObject(newsBL.GetNewsOfPage(-1,page,this.pageSize));
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
            if(fileAvatar==null) return RedirectToAction("Index");
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