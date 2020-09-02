using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TAMS.Entity;

namespace TAMS.DAL
{
    public class LoginAdminContext : BaseContext
    {
        private static LoginAdminContext _instance;
        public static LoginAdminContext Instance()
        {
            if (null == _instance)
            {
                _instance = new LoginAdminContext();
            }
            return _instance;
        }
        

        public int Login(Entity.LoginAdmin obj)

        {
            var Password = Md5.MD5Hash(obj.Password);
              int a = 0;
            using (var context = MasterDBContext())
            {
                var cmd = context.StoredProcedure("dbo.LoginAdmin")
                   .Parameter("UserName", obj.UserName)
                   .Parameter("Password", Password)
                   .ParameterOut("Total", FluentData.DataTypes.Int32);
                   cmd.Execute();
             
                  a = cmd.ParameterValue<int>("Total");
            }
            return a;
        }
        public void UpdateProfile(Entity.LoginAdmin obj)

        {
            string _FileName = Path.GetFileName(obj.file.FileName);
            string _path = Path.Combine(HttpContext.Current.Server.MapPath("~/adminlte/dist/img"), _FileName);
            obj.file.SaveAs(_path);
            using (var context = MasterDBContext())
            {
               context.StoredProcedure("dbo.UpdateProfile")
                   .Parameter("UserName", obj.UserName)
                   .Parameter("Avatar", _FileName)
                   .Execute();

             
            }
          
        }
        public Entity.LoginAdmin GetDataProfile(String UserName)

        {
            
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.GetDataProfile")
                    .Parameter("UserName", UserName)
                    .QuerySingle<Entity.LoginAdmin>();


            }

        }
        public void ChangePassword(Entity.LoginAdmin obj)

        {
            var Password = Md5.MD5Hash(obj.Password);
            var PasswordNew = Md5.MD5Hash(obj.PasswordNew);

            using (var context = MasterDBContext())
            {
                context.StoredProcedure("dbo.ChangePassword")
                    .Parameter("Password", Password)
                    .Parameter("PassWordNew",PasswordNew)
                    .Execute();

            }

        }

    }
}
