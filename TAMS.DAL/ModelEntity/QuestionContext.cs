using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAMS.Entity;

namespace TAMS.DAL.ModelEntity
{
    public class QuestionContext:BaseContext
    {
        public static List<EQuestion> GetByTest(int idTest)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("QuestionOfTest_GetByTest")
                    .Parameter("IdTest", idTest)
                    .QueryMany<EQuestion>();
            }
        }
        public static List<EQuestion> Get(int IdQuestion)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("Question_Get")
                    .Parameter("IdQuestion", IdQuestion)
                    .QueryMany<EQuestion>();
            }
        }
    }
}
