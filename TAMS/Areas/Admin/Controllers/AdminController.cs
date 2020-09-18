using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMS.DAL;
using TAMS.Entity.Models;
using TAMS.Entity;
namespace TAMS.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        //    // GET: Admin/Admin
        //    public ActionResult Index()
        //    {
        //        return View();
        //    }


        //    public ActionResult CategoryQuestion()
        //    {
        //        return View();
        //    }
        //    public void AddCategoryQuestion(CategoryQuestion obj)
        //    {
        //        AdminContext.AddCategoryQuestion(obj);
        //    }
        //    public void UpdateCategory(CategoryQuestion obj)
        //    {
        //        AdminContext.UpdateCategory(obj);
        //    }

        public IEnumerable GetDataCategory()
        {
            return JsonConvert.SerializeObject(AdminContext.GetDataCategory());
        }
        //    public IEnumerable GetByIdCategory(int Id)
        //    {

        //        return JsonConvert.SerializeObject(AdminContext.GetByIdCategory(Id));
        //    }
        //    public void DeleteCategory(int Id)
        //    {

        //        AdminContext.DeleteCategory(Id);
        //    }
        public ActionResult Question()
        {
            return View();
        }

        [HttpPost]
        public int AddQuestion(List<Answer> Answer, EQuestion Qs)
        {
            if (Answer.Count == 0) return 0;
            if (Answer.Count > 1 && Qs.CategoryAnswer.ToUpper() == "Text".ToUpper()) return 0;
            if (AdminContext.AddQuestion(Qs) < 0) return 0;
            return AdminContext.AddAnswer(Answer);
        }
        public ActionResult ManageQuestion()
        {
            return View();
        }
        public IEnumerable GetDataQuestion(int Page,int Size,string Search,string FilterQuestion,string FilterAnswer)
        {
            return JsonConvert.SerializeObject(AdminContext.GetDataQuestion(Search,FilterQuestion,FilterAnswer,Size,Page));
        }
        public int DeleteQuestion(int Id)
        {
            return AdminContext.DeleteQuestion(Id);
        }
        public void DeleteAnswer(int IdQuestion)
        {
            AdminContext.DeleteAnswer(IdQuestion);
        }
        public IEnumerable GetByIdQuestion(int Id)
        {
            return JsonConvert.SerializeObject(AdminContext.GetByIdQuestion(Id));
        }
        public IEnumerable GetByIdAnswer(int IdQuestion)
        {
            return JsonConvert.SerializeObject(AdminContext.GetByIdAnswer(IdQuestion));

        }
        public IEnumerable UpdateQuestion(EQuestion obj)
        {
            AdminContext.UpdateQuestion(obj);
            return JsonConvert.SerializeObject(obj);
        }
        //    public IEnumerable UpdateAnswer(List<Answer> obj)
        //    {
        //        AdminContext.UpdateAnswer(obj);
        //        return JsonConvert.SerializeObject(obj);
        //    }
        //    public IEnumerable CountAnswer(int IdQuestion)
        //    {
        //        return JsonConvert.SerializeObject(AdminContext.CountAnswer(IdQuestion));
         //   }
            public IEnumerable CountQuestion()
            {
                return JsonConvert.SerializeObject(AdminContext.CountQuestion());
            }

        public ActionResult addQuestionFromExcel(HttpPostedFileBase file)
    {
        string[] name = file.FileName.Split('.');
        if (name[name.Length - 1] == "xlsx" ||
            name[name.Length - 1] == "xls")
        {
            string namefile = Path.GetFileName(file.FileName);
            string path = Server.MapPath("/Content/File/" + namefile);
            file.SaveAs(path);
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(Path.Combine(Server.MapPath("~/Content/File"), namefile), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0); ;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1); ;
            Microsoft.Office.Interop.Excel.Range range = xlWorkSheet.UsedRange;
            int rw = range.Rows.Count;
            int cl = range.Columns.Count;
            for (int row = 1; row <= rw; row++)
            {
                EQuestion question = new EQuestion();
                Object TextQs = (Object)(range.Cells[row, 1] as Microsoft.Office.Interop.Excel.Range).Value2;
                if (TextQs == null || TextQs.GetType() != ("").GetType()) break;
                Object TypeQs = (Object)(range.Cells[row, 3] as Microsoft.Office.Interop.Excel.Range).Value2;
                if (TypeQs == null || TypeQs.GetType() != ("").GetType()) break;
                Object CategoryAnswer = (Object)(range.Cells[row, 2] as Microsoft.Office.Interop.Excel.Range).Value2;
                if (CategoryAnswer == null || CategoryAnswer.GetType() != ("").GetType()) break;
                question.Text = (string)TextQs;
                question.CategoryName = (string)TypeQs;
                question.CategoryAnswer = (string)CategoryAnswer;
                List<Answer> answers = new List<Answer>();
                for (int col = 4; col <= cl; col += 2)
                {
                    Answer answer = new Answer();
                    Object textAs = (Object)(range.Cells[row, col] as Microsoft.Office.Interop.Excel.Range).Value2;
                    if (textAs == null || textAs.GetType() != ("").GetType()) break;
                    Object resultAs = (Object)(range.Cells[row, col + 1] as Microsoft.Office.Interop.Excel.Range).Value2;
                    if (resultAs == null || resultAs.GetType() != true.GetType()) break;
                    answer.TextAnswer = (String)textAs;
                    answer.result = (bool)resultAs;
                    answers.Add(answer);
                }
                if (answers.Count <= 0) continue;
                if (AdminContext.Search(question.Text) == true) continue;
                AdminContext.AddQuestion(question);
                AdminContext.AddAnswer(answers);
            }
            xlWorkBook.Close();
        }
        return RedirectToAction("Index");
    }
}
}