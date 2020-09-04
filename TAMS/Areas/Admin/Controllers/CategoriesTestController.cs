
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using TAMS.Controllers;
using TAMS.DAL.ModelEntity;
using TAMS.Entity;

namespace TAMS.Areas.Admin.Controllers
{
    public class CategoriesTestController : SessionController
    {
        // GET: Admin/CategoriesTest
        public ActionResult Index()
        {
            Tuple<List<CategoryTest>, int> tuple= CategoriesTestContext.Get(-1, 5, 1);
            var numPage = tuple.Item2 / 5;
            if (tuple.Item2 % 5 != 0) numPage++;
            ViewData["listCategoriesTest"] = tuple.Item1;
            ViewData["totalPage"] = numPage;
            ViewData["Page"] = 1;
            return View();
        }
        public int GetLengthMax(String post)
        {
            try
            {
                string[] res = post.Split('-');
                if (res.Length != 2) return 0;
                int idCategoryTest = Convert.ToInt32(res[1]);
                List<Test> tests= TestContext.Get_FormByCategory(idCategoryTest);
                int max = 0;
                for(int i = 0; i < tests.Count; i++)
                {
                    if (max < tests[i].NumQuestion) max = tests[i].NumQuestion;
                }
                return max;
            }catch(Exception e)
            {

            }
            return 0;
        }
        public ActionResult Page(int page)
        {
            Tuple<List<CategoryTest>, int> tuple = CategoriesTestContext.Get(-1, 5, page);
            var numPage = tuple.Item2 / 5;
            if (tuple.Item2 % 5 != 0) numPage++;
            ViewData["listCategoriesTest"] = tuple.Item1;
            ViewData["totalPage"] = numPage;
            ViewData["Page"] = page;
            return View("~/Areas/Admin/Views/CategoriesTest/Index.cshtml");
        }
        [HttpGet]
        public ActionResult AddQuestion(int IdCategory)
        {
            List<CategoryTest> categoryTests = CategoriesTestContext.Get(IdCategory, 1, 1).Item1;
            if (categoryTests.Count == 0) RedirectToAction("Index");
            CategoryTest categoryTest= categoryTests[0];
            List<Question> questions = QuestionContext.Get(-1);
            List<Question> questionsOfCategoryTest = QuestionContext.GetByCategoryTest(IdCategory);
            for(int i = 0; i < questionsOfCategoryTest.Count; i++)
            {
                questions.Remove(questionsOfCategoryTest[i]);
            }
            ViewData["CategoryTest"] = categoryTest;
            ViewData["listQuestionOut"] = questions;
            ViewData["listQuestionIn"] = questionsOfCategoryTest;
            return View();
        }
        public int addQuestionForTest(int[] resPost,String post)
        {
            string[] res = post.Split('-');
            if (res.Length != 2) return 0;
            int idCategoryTest= Convert.ToInt32(res[1]);
            for(int i = 0; i < resPost.Length; i++)
            {
                QuestionOfTestContext.Add(idCategoryTest, resPost[i]);
            }
            return 1;
        }
        public int removeQuestionForTest(int[] resPost, String post)
        {
            string[] res = post.Split('-');
            if (res.Length != 2) return 0;
            int idCategoryTest = Convert.ToInt32(res[1]);
            for(int i = 0; i < resPost.Length; i++)
            {
                QuestionOfTestContext.Remove(idCategoryTest, resPost[i]);
            }
            return 1;
        }
        public ActionResult Delete(int IdCategory)
        {
            if (QuestionContext.GetByCategoryTest(IdCategory).Count >0) return RedirectToAction("AddQuestion", "CategoriesTest", new { IdCategory = IdCategory });
            CategoriesTestContext.Delete(IdCategory);
            return RedirectToAction("Index");
        }
        public ActionResult Create(string Name)
        {
            if(Name!="") CategoriesTestContext.Create(Name);
            return RedirectToAction("Index");
        }

    }
}