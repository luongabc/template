
using System;
using System.Collections.Generic;
using TAMS.DAL;
using TAMS.Entity.Models;

namespace TAMS.DAL.ModelEntity
{
    public class UserContext:BaseContext
    {
        public static User Search(string username,string password)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("User_Search")
                    .Parameter("UserName", username)
                    .Parameter("Password", password)
                    .QuerySingle<User>();
            }

        }
        
        public static List<User> Get(int IdUser)
        {
            using(var context = MasterDBContext())
            {
                return context.StoredProcedure("User_Get")
                    .Parameter("IdUser", IdUser)
                    .QueryMany<User>();
            }
        }
        public static int Insert(User obj)
        {
            using (var context = MasterDBContext())
            {
                var result = context.StoredProcedure("SP_INSERT_UPDATE_USER")
                    .Parameter("Id", null)
                    .Parameter("Name", obj.Name)
                    .Parameter("UserName", obj.UserName)
                    .Parameter("Email", obj.Email)
                    .Parameter("Password", obj.Password)
                    .Parameter("Avatar", obj.Avatar)
                    .Parameter("ResetPasswordCode", string.Empty)
                    .Parameter("Birthday", obj.Birthday)
                    .Parameter("CreateDate", DateTime.Now)
                    .Parameter("ModifyDate", DateTime.Now)
                    .Execute();
                return result;
            }
        }
        public static int Create(User obj)
        {
            using (var context = MasterDBContext())
            {
                var result = context.StoredProcedure("User_Create")
                    .Parameter("Name", obj.Name)
                    .Parameter("UserName", obj.UserName)
                    .Parameter("Email", obj.Email)
                    .Parameter("Password", obj.Password)
                    .Parameter("Birthday", obj.Birthday)
                    .Execute();
                return result;
            }
        }
        public static int Check(User obj)
        {
            using (var context = MasterDBContext())
            {
                var result = context.StoredProcedure("User_Check")
                    .Parameter("UserName", obj.UserName)
                    .Parameter("Email", obj.Email)
                    .Execute();
                return result;
            }
        }
        //public static int InsertForFacebook(User obj)
        //{
        //    if (IsExistUserName(obj.UserName))
        //    {
        //        return obj.Id;
        //    }
        //    else
        //    {
        //        Insert(obj);
        //        return obj.Id;
        //    }
        //}
        public static int Update(User obj)
        {
            using (var context = MasterDBContext())
            {


                var cmd = context.StoredProcedure("SP_INSERT_UPDATE_USER")
                    .Parameter("Id", obj.Id)
                    .Parameter("Name", obj.Name)
                    .Parameter("UserName", obj.UserName)
                    .Parameter("Email", obj.Email)
                    .Parameter("Password", obj.Password)
                    .Parameter("Avatar", obj.Avatar)
                    .Parameter("ResetPasswordCode", obj.ResetPasswordCode)
                    .Parameter("Birthday", obj.Birthday)
                    .Parameter("CreateDate", DateTime.Now)
                    .Parameter("ModifyDate", DateTime.Now)
                    .Execute();
                return cmd;
            }
        }
        //public int Delete(int id)
        //{
        //    using (var content = MasterDBContext())
        //    {
        //        List<Test> tests = TestContext.GetByUser(id, 0, -1).Item1;
        //        for (int i = 0; i < tests.Count; i++)
        //        {
        //            UserResultContext.DeleteByTest(tests[i].Id);
        //            TestContext.Delete(tests[i].Id);
        //        }
        //        var cmd = content.StoredProcedure("User_Delete")
        //            .Parameter("Id", id)
        //            .Execute();
        //        return cmd;
        //    }
        //}
        public static string Search(User obj)
        {
            using (var content = MasterDBContext())
            {
                return content.StoredProcedure("SP_USER_SEARCH")
                    .Parameter("UserName", obj.UserName)
                    .Parameter("Name", obj.Name)
                    .QuerySingle<string>();
            }
        }
        public static User GetById(int Id)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("User_Get")
                    .Parameter("Id", Id)
                    .QuerySingle<User>();
            }
        }
        public static User GetByUserName(string UserName)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("GetByUser_Name")
                    .Parameter("UserName", UserName)
                    .QuerySingle<User>();
            }
        }
        public static User GetByEmail(string Email)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("GetByEmail")
                    .Parameter("Email", Email)
                    .QuerySingle<User>();
            }
        }
        public static User GetByResetPasswordCode(long resetPasswordCode)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("GetByResetPasswordCode")
                    .Parameter("ResetPasswordCode", resetPasswordCode)
                    .QuerySingle<User>();
            }
        }
        public static Tuple<List<User>, int> GetUserByPage(String search,int pageIndex, int pageSize)
        {
            int toTalRecord = 0;
            List<User> listUser = new List<User>();
            using (var context = MasterDBContext())
            {
                var cmd = context.StoredProcedure("User_GetByPagging")
                    .Parameter("pageIndex", pageIndex)
                    .Parameter("pageSize", pageSize)
                    .Parameter("search", search)
                    .ParameterOut("TotalRecord", FluentData.DataTypes.Int32);
                listUser = cmd.QueryMany<User>();
                toTalRecord = cmd.ParameterValue<int>("TotalRecord");
                int div = toTalRecord % pageSize;
                int numPage = toTalRecord / pageSize;
                if (div > 0) numPage++;
                Tuple<List<User>, int> dataReturn = Tuple.Create(listUser, numPage);
                return dataReturn;
            }
        }
        public static List<User> GetByAllUser()
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("GetByAllUser")
                    .QueryMany<User>();
            }
        }
        public static bool Login(string userName, string password)
        {
            using (var context = MasterDBContext())
            {
                var cmd = context.StoredProcedure("User_SearchAdmin")
                    .Parameter("UserName", userName)
                    .Parameter("Password", password);
                User result = cmd.QuerySingle<User>();
                if (result != null) return true;
                else return false;
            }
        }
        public static bool IsExistUserName(string username)
        {
            if (GetByUserName(username) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool IsEmail(string email)
        {
            if (GetByEmail(email) == null)
            {
                return false;

            }
            else
            {
                return true;
            }
        }
        public static bool IsResetPasswordCodeExist(long resetPasswordCode)
        {
            if (GetByResetPasswordCode(resetPasswordCode) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool IsExistsId(int Id)
        {
            if (GetById(Id) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public UserContext Configuration { get; }

        public bool ValidateOnSaveEnabled { get; set; }

    }
}
    