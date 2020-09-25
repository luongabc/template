using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAMS.Entity.Models;

namespace TAMS.DAL.ModelEntity
{
    public class UserResultContext: BaseContext
    {

        public static List<EUserResult> GetByTest(int idTest)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("UserResult_GetByIdTest")
                        .Parameter("IdTest", idTest)
                        .QueryMany<EUserResult>();
            }
        }
        //public static int DeleteByTest(int IdTest)
        //{
        //    using(var context = MasterDBContext())
        //    {
        //        return context.StoredProcedure("UserResults_Delete_Test")
        //            .Parameter("IdTest", IdTest)
        //            .Execute();
        //    }
        //}
        public static int AddTest(int IdTest)
        {
            using (var context = MasterDBContext())
            {
                int count= context.StoredProcedure("UserResult_Add")
                   .Parameter("IdTest", IdTest)
                   .Execute();
                if(count>0)
                {
                    TestContext.UpdateTimeStart(IdTest);
                    TestContext.UpdateStatus(IdTest);
                }
                return count;
            }
        }
        
        public static List<Answer> CountQuestionFail(int IdTest)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("Test_CountFailQuestion")
                    .Parameter("IdTest", IdTest)
                    .QueryMany<Answer>();
            }
        }
    }
}
