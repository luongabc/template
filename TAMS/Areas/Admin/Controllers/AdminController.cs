using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TAMS.DAL;

namespace TAMS.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }
       
      
        public ActionResult CategoryQuestion()
        {
            return View();
        }
        public void AddCategoryQuestion(Entity.CategoryQuestion obj)
        {
            AdminContext adminContext = new AdminContext();
            adminContext.AddCategoryQuestion(obj);
        }
        public void UpdateCategory(Entity.CategoryQuestion obj)
        {
            AdminContext adminContext = new AdminContext();
            adminContext.UpdateCategory(obj);

        }
        
        public IEnumerable GetDataCategory()
        {
            AdminContext adminContext = new AdminContext();
            return JsonConvert.SerializeObject(adminContext.GetDataCategory());
        }
        public IEnumerable GetByIdCategory(int Id)
        {
            AdminContext adminContext = new AdminContext();
            return JsonConvert.SerializeObject(adminContext.GetByIdCategory(Id));
        }
        public void DeleteCategory(int Id)
        {
            AdminContext adminContext = new AdminContext();
            adminContext.DeleteCategory(Id);
        }
        public ActionResult Question ()
        {
            return View();
        }
        
        [HttpPost]
        public int AddQuestion(List<Entity.Answer> Answer, Entity.Question Qs)
        {
            if (Answer.Count == 0) return 0;
            AdminContext adminContext = new AdminContext();
            if(adminContext.AddQuestion(Qs)<0) return 0;
            return adminContext.AddAnswer(Answer);
        }
        public ActionResult ManageQuestion()
        {
            return View();
        }
        public IEnumerable GetDataQuestion(int Id)
        {
            AdminContext adminContext = new AdminContext();
         
            return JsonConvert.SerializeObject(adminContext.GetDataQuestion(Id));
        }
        public int DeleteQuestion(int Id)
        {
            AdminContext adminContext = new AdminContext();
            return adminContext.DeleteQuestion(Id);
        }
        public void DeleteAnswer(int IdQuestion)
        {
            AdminContext adminContext = new AdminContext();
            adminContext.DeleteAnswer(IdQuestion);
        }
        public IEnumerable GetByIdQuestion(int Id)
        {
            AdminContext adminContext = new AdminContext();

            return JsonConvert.SerializeObject(adminContext.GetByIdQuestion(Id));
        }
        public IEnumerable GetByIdAnswer(int IdQuestion)
        {
            AdminContext adminContext = new AdminContext();

            return JsonConvert.SerializeObject(adminContext.GetByIdAnswer(IdQuestion));

        }
        public IEnumerable UpdateQuestion(Entity.Question obj)
        {
            AdminContext adminContext = new AdminContext();
            adminContext.UpdateQuestion(obj);
            return JsonConvert.SerializeObject(obj);
        }
        public IEnumerable UpdateAnswer(List<Entity.Answer> obj)
        {
            AdminContext adminContext = new AdminContext();
            adminContext.UpdateAnswer(obj);
            return JsonConvert.SerializeObject(obj);
        }
        public IEnumerable CountAnswer(int IdQuestion)
        {
            AdminContext adminContext = new AdminContext();

            return JsonConvert.SerializeObject(adminContext.CountAnswer(IdQuestion));
        }
        public IEnumerable CountQuestion()
        {
            AdminContext adminContext = new AdminContext();

            return JsonConvert.SerializeObject(adminContext.CountQuestion());
        }
    }
}