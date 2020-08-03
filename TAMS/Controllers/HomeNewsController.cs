using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

using Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMS.DAL;

namespace project_14_7_2020.Controllers
{
    public class HomeNewsController : Controller
    {
        // GET: HomeNews
        private int numPage = 6;
        public ActionResult Index()
        {
            NewsContext newsContext = new NewsContext();
            var data= newsContext.GetNewsOfPage(-1,1, this.numPage);
            int div = data.Item2 % this.numPage;
            int numPage = data.Item2 / this.numPage;
            if (div > 0) numPage++;
            ViewData["ListNews"] = Tuple.Create(data.Item1, numPage);
            CategoryContext categoryContext = new CategoryContext();
            ViewData["ListCategory"] = categoryContext.GetAllCategories();
            return View();
        }
        [HttpGet]
        public ActionResult GetPageNewsOfCategory(int CategoryId = 13)
        {
            NewsContext newsContext = new NewsContext();
            CategoryContext categoryContext = new CategoryContext();
            var dataSet = newsContext.GetNewsOfPage(CategoryId, 1, this.numPage);
            int NumPage = dataSet.Item2 % this.numPage;
            if (NumPage > 0) NumPage = dataSet.Item2 / this.numPage + 1;
            Tuple<List<News>, int,int> dataReturn = Tuple.Create(dataSet.Item1, NumPage,CategoryId);
            ViewData["ListNews"] = dataReturn;
            ViewData["ListCategory"] = categoryContext.GetAllCategories();
            return View();
        }
        [HttpGet]
        public IEnumerable GetNewsOfPageCategory(int CategoryId, int Page)
        {
            NewsContext newsContext = new NewsContext();
            var dataSet = newsContext.GetNewsOfPage(CategoryId, Page, this.numPage);
            int NumPage = dataSet.Item2 % this.numPage;
            if (NumPage > 0) NumPage = dataSet.Item2 / this.numPage + 1;
            Tuple<List<News>, int> dataReturn = Tuple.Create(dataSet.Item1, NumPage);
            return JsonConvert.SerializeObject(dataReturn);
        }
    }
}