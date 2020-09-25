using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAMS.Entity.Models;

namespace TAMS.DAL.ModelEntity
{
    public  class QuestionOfTestContext : BaseContext
    {
        public static List<CategoryQuestionOfTest> GetCategoryQuestionOfTest(int IdFormTest)
        {
            using (var context = MasterDBContext())
            {
                List<CategoryQuestionOfTest> categoryQuestionOfTests = context.StoredProcedure("CategoryQuestionOfTest_GetByIdFormTest")
                    .Parameter("IdFormTest", IdFormTest)
                    .QueryMany<CategoryQuestionOfTest>();
                return categoryQuestionOfTests;
            }
        }
        //public static int Add(int IdCategoryTest,int IdQuestion)
        //{
        //    using(var context = MasterDBContext())
        //    {
        //        return context.StoredProcedure("QuestionOfTest_Add")
        //            .Parameter("IdCategoryTest", IdCategoryTest)
        //            .Parameter("IdQuestion", IdQuestion)
        //            .Execute();
        //    }
        //}
        //public static int Remove(int IdCategoryTest, int IdQuestion)
        //{
        //    using (var context = MasterDBContext())
        //    {
        //        return context.StoredProcedure("QuestionOfTest_Remove")
        //            .Parameter("IdCategoryTest", IdCategoryTest)
        //            .Parameter("IdQuestion", IdQuestion)
        //            .Execute();
        //    }
        //}
    }
}
