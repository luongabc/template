using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAMS.Entity;

namespace TAMS.DAL.ModelEntity
{
    public class UserResultContext: BaseContext
    {
        
        public static List<UserResult> GetByTest(int idTest)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("UserResult_GetByIdTest")
                        .Parameter("IdTest", idTest)
                        .QueryMany<UserResult>();
            }
        }
        public static int DeleteByTest(int IdTest)
        {
            using(var context = MasterDBContext())
            {
                return context.StoredProcedure("UserResults_Delete_Test")
                    .Parameter("IdTest", IdTest)
                    .Execute();
            }
        }
        public static void Create_RandomQuestionsForTest(Test test)
        {
            using (var context = MasterDBContext())
            {
                context.StoredProcedure("UserResult_CreateRandom")
                   .Parameter("IdCategoryTest", test.IdCategory)
                   .Parameter("IdTest", test.Id)
                   .Parameter("Size", test.NumQuestion)
                   .Execute();
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
