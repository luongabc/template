
using System;
using System.Collections.Generic;
using TAMS.DAL;
using TAMS.Entity;

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
    }
}
    