

using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using TAMS.DAL.BL;
using TAMS.DAL.ModelEntity;
using TAMS.Entity;
using TAMS.Entity.Models;
namespace TAMS.Controllers
{
    public class InfoUserController : SessionController
    {
        // GET: InfoUser
        public ActionResult Index()
        {
            var user = (User)Session[Common.USER_SESSION];
            List<ETest> listTest = BLTest.TestOfUser(user.Id);
            ViewData["user"] = user;
            ViewData["ListTest"] = listTest;
            return View();
        }
        public ActionResult Test(int IdTest)
        {
            var user = (User)Session[Common.USER_SESSION];
            ETest test = TestContext.SearchTestOfUser(IdTest,user.Id);
            if (test == null) return null;
            // new Test
            if (test.Status.ToUpper() == "NotStart".ToUpper())
            {
                int count= UserResultContext.AddTest(test.Id);
                if (count ==0) return RedirectToAction("Index");
            }
            else if (test.Status == Entity.baseEmun.StaticTest.Finish.ToString()) return RedirectToAction("Index");
            else if (test.TimeStart != null)
            {
                DateTime timeNow = DateTime.Now;
                if (timeNow - test.TimeStart > test.Time) {
                    BLTest.CheckIsFinish(test.Id);
                    return RedirectToAction("Index");
                }
                test.Time = test.Time - (TimeSpan)(timeNow - test.TimeStart);
            }
            ViewData["user"] = user;
            ViewData["Test"] = test;
            return View();
        }
        [HttpGet]
        public IEnumerable getQuestions(int id)
        {
            ETest test = TestContext.GetTestOfUser(id);
            if (test == null) return null;
            if (test.Status == Entity.baseEmun.StaticTest.Finish.ToString()) return null;
            if (test.TimeStart != null)
            {
                DateTime timeNow = DateTime.Now;
                if (timeNow - test.TimeStart > test.Time) return null;
                test.Time = test.Time - (TimeSpan)(timeNow - test.TimeStart);
            }
            return JsonConvert.SerializeObject(BLTest.GetContentOfTest(test));
        }

        [HttpPost]
        public ActionResult SaveResult(List<UserResult> userResults, int IdTest)
        {
            User user = (User)Session[Common.USER_SESSION];
            int countUpdate = TestContext.SaveResultOfUser(userResults, IdTest);
            return RedirectToAction("Index");
        }
        public ActionResult FinishTest(int IdTest)
        {
            TestContext.UpdateStatus(IdTest);
            BLTest.CheckIsFinish(IdTest);
            return RedirectToAction("Index");
        }
    }
}