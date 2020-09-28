using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMS.Controllers;
using TAMS.DAL;
using TAMS.DAL.BL;
using TAMS.DAL.Model.Entity;
using TAMS.DAL.ModelEntity;
using TAMS.Entity;
using TAMS.Entity.Models;

namespace TAMS.Areas.Admin.Controllers
{
    public class TestController : Controller
    {
        private int numItem = 3;
        // GET: Admin/Test
        public ActionResult Index()
        {
            Tuple<List<FormTest>, int> data= TestContext.Get_FormTests("",numItem, 1);
            List<FormTest> test = data.Item1;
            ViewData["listTest"] = test;
            int totalPage= data.Item2 / numItem;
            if (data.Item2 % numItem > 0) totalPage++;
            ViewData["totalPage"] = totalPage;
            return View();
        }
        public ActionResult Search(string search)
        {
            Tuple<List<FormTest>, int> data = TestContext.Get_FormTests(search, numItem, 1);
            List<FormTest> test = data.Item1;
            ViewData["listTest"] = test;
            int totalPage = data.Item2 / numItem;
            if (data.Item2 % numItem > 0) totalPage++;
            ViewData["totalPage"] = totalPage;
            return View("Index");
        }
        public ActionResult FormTestsPage(string search,int page)
        {
            Tuple<List<FormTest>, int> data = TestContext.Get_FormTests(search,numItem, page);
            List<FormTest> test = data.Item1;
            int totalPage = data.Item2 / numItem;
            if (data.Item2 % numItem > 0) totalPage++;
            ViewData["listTest"] = test;
            ViewData["totalPage"] = totalPage;
            return View("index");   
        }
        public ActionResult FormTest(string search,int IdForm,int Page)
        {
            var  test = TestContext.Get_TestOfUserByForm(search,IdForm,Page,20);
            FormTest formTest = TestContext.Get_FormTest(IdForm);
            int numPage = test.Item2 / 20;
            if (test.Item2 % 20>0) numPage++;
            ViewData["listTest"] = Tuple.Create(test.Item1,numPage);
            ViewData["FormTest"] = formTest;
            return View();
        }
        public ActionResult ViewTest(int IdTest)
        {
            ETest test = TestContext.GetTestOfUser(IdTest);
            Tuple<List<EQuestion>, List<EUserResult>> data = BLTest.GetContentOfTest(test);
            List<Answer> listAnserIsTrue = AnswerContext.GetByTest(IdTest);
            List<EQuestion> eQuestions = data.Item1.OrderBy(i => i.Id).ToList();
            List<EUserResult> userResults = data.Item2.OrderBy(i => i.IdQuestion).ToList();
            for (int i=0;i< data.Item2.Count;i++)
            {
                if (data.Item2[i].IdAnswer == listAnserIsTrue[i].Id)
                {
                    data.Item2[i].isTrue = listAnserIsTrue[i].result;
                }
            }
            ViewData["ContentTest"] = Tuple.Create(eQuestions, userResults);
            ViewData["Test"] = test;
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            BLTest.CreateFormTest(null);
            return View();
        }
        public IEnumerable GetCategoryQuestion()
        {
            List<CategoryQuestion> categoryQuestions= AdminContext.GetDataCategory();
            return JsonConvert.SerializeObject(categoryQuestions);
        }
        [HttpPost]
        public ActionResult Create(FormTest test, int Hours, int Minutes)
        {
            TimeSpan time = new TimeSpan(Hours, Minutes, 0);
            test.Time = time;
            if (TestContext.Create(test) == 0) return View();
            test=TestContext.GetByName(test.Name);
            return RedirectToAction("ContentFormTest", new { IdFormTest =test.Id });
        }
        //public ActionResult Detail(int IdTest)
        //{
        //    Test test = TestContext.Get_Test(IdTest);
        //    ViewData["Test"] = test;
        //    if (test.IdUser == 0) return View();
        //        ViewData["User"] = UserContext.Get(test.IdUser).First();
        //        ViewData["ContentTest"] = BLTest.GetContentOfTest(test);
        //    return View();
        //}
        //public ActionResult Delete(int IdTest)
        //{
        //    UserResultContext.DeleteByTest(IdTest);
        //    TestContext.Delete(IdTest);
        //    return RedirectToAction("Index");
        //}
        public int CountQuestionByCategoryQuestion(string Category)
        {
            return AdminContext.GetDataQuestion("", Category,"",1,1).Item2;
        }
        public ActionResult ContentFormTest(int IdFormTest)
        {
            return View();
        }
        [HttpPost]
        public int ContentFormTest(List<CategoryQuestionOfTest>categoryQuestionOfTests)
        {
            int count =DALCategoryQuestionOfTest.addCategoriesQuetionForTest(categoryQuestionOfTests);
            return count;
        }
        
        public ActionResult EditContentFormTest(int IdFormTest)
        {
            return View();
        }
        public IEnumerable GetCategoryQuestionOfTest(int IdFormTest)
        {
            List<CategoryQuestionOfTest> res = QuestionOfTestContext.GetCategoryQuestionOfTest(IdFormTest);
            if (res.Count == 0) return null;
            return JsonConvert.SerializeObject(res);
        }
    }
}