
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMS.Controllers;
using TAMS.DAL.BL;
using TAMS.DAL.ModelEntity;
using TAMS.Entity;

namespace TAMS.Areas.Admin.Controllers
{
    public class TestController : SessionController
    {
        private int numItem = 3;
        // GET: Admin/Test
        public ActionResult Index()
        {
            Tuple<List<Test>, int> data= TestContext.Get_FormTests(numItem, 1);
            List<Test> test = data.Item1;
            ViewData["listTest"] = test;
            ViewData["totalPage"] = data.Item2 / numItem + 1;
            ViewData["page"] =1;
            return View();
        }
        public ActionResult Page(int page)
        {
            Tuple<List<Test>, int> data = TestContext.Get_FormTests(numItem, page);
            List<Test> test = data.Item1;
            ViewData["listTest"] = test;
            ViewData["totalPage"] = data.Item2 / numItem + 1;
            ViewData["page"] = page;
            return View("~/Areas/Admin/Views/Test/Index.cshtml");
        }
        public ActionResult FormTest(int IdForm)
        {
            List<Test> test = TestContext.Get_ByForm(IdForm);
            Test formTest = TestContext.Get_Test(IdForm); ;
            test.Sort();
            ViewData["listTest"] = test;
            ViewData["FormTest"] = formTest;
            return View();
        }

        public ActionResult ViewTest(int IdTest)
        {
            Test test = TestContext.Get_Test(IdTest);
            Tuple<List<Question>, List<UserResult>> data = BLTest.GetContentOfTest(test);
            List<Question> listQs = new List<Question>();
            List<UserResult> listAs = new List<UserResult>();
            foreach (Question question in data.Item1)
            {
                if (question.IdCategory == (int)(TAMS.Entity.baseEmun.CategoryQuestion.Text))
                {
                    listQs.Add(question);
                    foreach(UserResult answer in data.Item2)
                    {
                        if (answer.IdQuestion == question.Id)
                        {
                            listAs.Add(answer);
                            break;
                        }
                    }
                }
            }
            Tuple<List<Question>, List<UserResult>> list = Tuple.Create(listQs, listAs);
            ViewData["ContentTest"] = list;
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewData["listCategoryTest"] = CategoriesTestContext.Get(0,-1,0).Item1;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Test test,int Hours,int Minutes)
        {
            TimeSpan time = new TimeSpan(Hours,Minutes,0);
            test.Time = time;
            if (TestContext.Create(test) == 0) return View();
            return RedirectToAction("Index");
        }
        public ActionResult Detail(int IdTest)
        {
            Test test = TestContext.Get_Test(IdTest);
            ViewData["Test"] = test;
            if (test.IdUser == 0) return View();
                ViewData["User"] = UserContext.Get(test.IdUser).First();
                ViewData["ContentTest"] = BLTest.GetContentOfTest(test);
            return View();
        }
        public ActionResult Delete(int IdTest)
        {
            UserResultContext.DeleteByTest(IdTest);
            TestContext.Delete(IdTest);
            return RedirectToAction("Index");
        }
        public int CountQuestionByCategoryTest(int Id)
        {
            return QuestionContext.GetByCategoryTest(Id).Count;
        }
    }
}