

using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using TAMS.DAL.BL;
using TAMS.DAL.ModelEntity;
using TAMS.Entity;

namespace TAMS.Controllers
{
    public class InfoUserController : SessionController
    {
        // GET: InfoUser
        public ActionResult Index()
        {
            var user = (User)Session[Common.USER_SESSION];
            List<Test> listTest= BLTest.TestOfUser(user.Id);
            ViewData["user"] = user;
            ViewData["ListTest"] = listTest;
            foreach(Test test in listTest)
            {
                if(test.TimeStart!=null 
                    && test.Status==(int)Entity.baseEmun.StaticTest.Doing)
                test.Time = (test.Time - (TimeSpan)(DateTime.Now - test.TimeStart));
            }
            return View();
        }
        public ActionResult Test(int IdUser,int IdTest)
        {
            var user = (User)Session[Common.USER_SESSION];
            Test test = TestContext.Get_Test(IdTest);
            if (test == null) return null;
            // new Test
            if (test.IdFormTest == ((int)(Entity.baseEmun.Test.FormTest)))
            {
                BLTest.CreateTest(user.Id, test);
                List<Test> tests = BLTest.TestOfUser(user.Id);
                int i;
                for (i = 0; i < tests.Count; i++)
                {
                    if (tests[i].IdFormTest == test.Id)
                    {
                        test = tests[i];
                        break;
                    }
                }
                if (i == tests.Count) return RedirectToAction("Index");
            }
            if (test.Status == (int)Entity.baseEmun.StaticTest.Finish) return RedirectToAction("Index");
            if (test.TimeStart != null) {
                DateTime timeNow = DateTime.Now;
                if (timeNow - test.TimeStart > test.Time) return RedirectToAction("Index");
                test.Time = test.Time - (TimeSpan)(timeNow - test.TimeStart);
            }
            ViewData["user"] = user;
            ViewData["Test"] = test;
            return View();
        }
        [HttpGet]
        public IEnumerable getQuestions(int id)
        {
            Test test = TestContext.Get_Test(id);
            if (test == null) return null;
            if (test.IdFormTest == ((int)(Entity.baseEmun.Test.FormTest))) return null;
            if (test.Status == (int)Entity.baseEmun.StaticTest.Finish) return null;
            if (test.TimeStart != null)
            {
                DateTime timeNow = DateTime.Now;
                if (timeNow - test.TimeStart > test.Time) return null;
                test.Time = test.Time - (TimeSpan)(timeNow - test.TimeStart);
            }
            return JsonConvert.SerializeObject(BLTest.GetContentOfTest(test));
        }

        [HttpPost]
        public ActionResult SaveResult(List<UserResult> userResults,int IdTest)
        {
            User user = (User)Session[Common.USER_SESSION];
            int countUpdate = TestContext.SaveResultOfUser(userResults, IdTest);

            return RedirectToAction("Index");
        }
        public ActionResult FinishTest(int IdTest)
        {
            TestContext.ChangeStatusTest(IdTest, (int)Entity.baseEmun.StaticTest.Finish);
            BLTest.CheckIsFinish(IdTest);
            return RedirectToAction("Index");
        }
    }
}