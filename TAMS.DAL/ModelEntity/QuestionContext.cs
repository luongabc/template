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
        public static List<Question> GetByTest(int idTest)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("GetListQuestion_StartTest")
                    .Parameter("IdTest", idTest)
                    .QueryMany<Question>();
            }
        }
        public static List<Question> Get(int IdQuestion)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("Question_Get")
                    .Parameter("IdQuestion", IdQuestion)
                    .QueryMany<Question>();
            }
        }
        public static List<Question> GetByCategoryTest(int CategoryTest)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("[CategoryTest_GetQuestion]")
                    .Parameter("IdCategoryTest", CategoryTest)
                    .QueryMany<Question>();
            }
        }
    }
}
