using TAMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAMS.DAL.ModelEntity;

namespace TAMS.DAL
{
    public class UserContext : BaseContext
    {
        private static UserContext _instance;
        public static UserContext Instance()
        {
            if (null == _instance)
            {
                _instance = new UserContext();
            }
            return _instance;
        }
        public int Insert(User obj)
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
        public int InsertForFacebook(User obj)
        {
            if (IsExistUserName(obj.UserName))
            {
                return obj.Id;
            }
            else
            {
                Insert(obj);
                return obj.Id;
            }
        }
        public int Update(User obj)
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
        public int Delete(int id)
        {
            using(var content = MasterDBContext())
            {
                List<Test> tests = TestContext.GetByUser(id,0,-1).Item1;
                for(int i=0; i < tests.Count; i++)
                {
                    UserResultContext.DeleteByTest(tests[i].Id);
                    TestContext.Delete(tests[i].Id);
                }
                var cmd = content.StoredProcedure("User_Delete")
                    .Parameter("Id", id)
                    .Execute();
                return cmd;
            }
        }
        public string Search(User obj)
        {
            using(var content = MasterDBContext())
            {
                return content.StoredProcedure("SP_USER_SEARCH")
                    .Parameter("UserName", obj.UserName)
                    .Parameter("Name", obj.Name)
                    .QuerySingle<string>();
            }
        }
        public User GetById(int Id)
        {
            using(var context = MasterDBContext())
            {
                return context.StoredProcedure("User_Get")
                    .Parameter("Id", Id)
                    .QuerySingle<User>();
            }
        }
        public User GetByUserName(string UserName)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("GetByUser_Name")
                    .Parameter("UserName", UserName)
                    .QuerySingle<User>();
            }
        }
        public User GetByEmail(string Email)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("GetByEmail")
                    .Parameter("Email", Email)
                    .QuerySingle<User>();
            }
        }
        public User GetByResetPasswordCode(long resetPasswordCode)
        {
            using(var context = MasterDBContext())
            {
                return context.StoredProcedure("GetByResetPasswordCode")
                    .Parameter("ResetPasswordCode", resetPasswordCode)
                    .QuerySingle<User>();
            }
        }
        public Tuple<List<User>, int> GetUserByPage(int pageIndex, int pageSize)
        { int toTalRecord = 0;
            List<User> listUser = new List<User>();
            using (var context = MasterDBContext())
            {
                var cmd = context.StoredProcedure("User_GetByPagging")
                    .Parameter("pageIndex", pageIndex)
                    .Parameter("pageSize", pageSize)
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
        public List<User> GetByAllUser()
        {
            using(var context = MasterDBContext())
            {
                return context.StoredProcedure("GetByAllUser")
                    .QueryMany<User>();
            }
        }
        public bool Login(string userName, string password)
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
        public bool IsExistUserName(string username)
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
        public bool IsEmail(string email)
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
        public bool IsResetPasswordCodeExist(long resetPasswordCode)
        {
            if(GetByResetPasswordCode(resetPasswordCode) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsExistsId(int Id)
        {
            if(GetById(Id) == null)
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
