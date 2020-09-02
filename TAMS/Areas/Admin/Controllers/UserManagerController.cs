using TAMS.DAL;
using TAMS.Entity;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TAMS.Areas.Admin.Controllers
{
    public class UserManagerController : Controller
    {
        // GET: Admin/User
        private int pageSize = 10;
        [HttpGet]
        public ActionResult Index()
        {
            UserContext usercontext = new UserContext();
            Tuple<List<User>, int> getDatas = usercontext.GetUserByPage(1, this.pageSize);
            int PageSize = getDatas.Item2 / pageSize;
            int div = getDatas.Item2 % pageSize;
            if (div > 0) PageSize++;
            Tuple<List<User>, int> result= Tuple.Create(getDatas.Item1, PageSize);
            ViewData["ListUser"] = result;
            return View();
        }
        public ActionResult Index(int pageIndex)
        {
            User user = new User();
            UserContext usercontext = new UserContext();
            Tuple<List<User>, int> getDatas = usercontext.GetUserByPage(pageIndex, this.pageSize);
            int PageSize = getDatas.Item2 / pageSize;
            int div = getDatas.Item2 % pageSize;
            if (div > 0) PageSize++;
            /* string search = usercontext.Search(user);
             ViewBag.Search = search;*/
            ViewData["ListUser"] = Tuple.Create(getDatas.Item1, PageSize);
            return View();
        }
        [HttpGet]
        public IEnumerable GetUserByPage(int page)
        {
            UserContext usercontext = new UserContext();
            return JsonConvert.SerializeObject(usercontext.GetUserByPage(page, this.pageSize));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User User, HttpPostedFileBase AvatarUpload)
        {
            String[] arr=AvatarUpload.FileName.Split('.');
            if(arr[arr.Length-1]=="png"||
                arr[arr.Length - 1] == "jpg")
            {
                var con = new UserContext();
                string namefile = Path.GetFileName(AvatarUpload.FileName);
                string path = Server.MapPath("/Content/Image" + "/" + namefile);
                User.Avatar = namefile;
                try
                {
                    AvatarUpload.SaveAs(path);
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
                string SaveFolder = Path.Combine(Server.MapPath("/Content/Image"), namefile);
                AvatarUpload.SaveAs(SaveFolder);
                if (ModelState.IsValid)
                {
                    if (con.IsEmail(User.Email))
                    {
                        ModelState.AddModelError("", "Email dã tồn tại.");
                    }
                    else if (con.IsExistUserName(User.UserName))
                    {
                        ModelState.AddModelError("", "UserName đã tồn tại.");
                    }
                    else
                    {
                        if (con.Insert(User) > 0)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Thêm user thành công.");
                        }
                    }
                }
                return View("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            UserContext userContext = new UserContext();
            ViewData["user"] = userContext.GetById(Id);
            return View();
        }
        [HttpPost]
        public ActionResult Edit(User User, HttpPostedFileBase AvatarUpload)
        {
            var con = new UserContext();
            if (AvatarUpload == null) User.Avatar = null;
            else User.Avatar = AvatarUpload.FileName.ToString();
            string[] arr = AvatarUpload.FileName.Split('.');
            if (arr[arr.Length-1]=="png"||
                arr[arr.Length - 1] == "jpg")
            {
                string path = Server.MapPath("/Content/Image") + "\\" + User.Avatar;
                try
                {
                    AvatarUpload.SaveAs(path);
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
                if (ModelState.IsValid)
                {
                    if (con.IsExistsId(User.Id))
                    {
                        if (con.Update(User) >= 0)
                        {
                            return RedirectToAction("Index", "UserManager");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Cập nhật thành công.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Người dùng không tồn tại.");
                    }
                }
                return View("Index");
            }
            return View();
            
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            UserContext usercon = new UserContext();
            usercon.Delete(id);
            return RedirectToAction("Index");
        }

    }
}