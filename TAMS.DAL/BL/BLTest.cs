using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAMS.DAL.ModelEntity;
using TAMS.Entity.Models;
using TAMS.Entity;
using System.Data;
using FastMember;
using System.Data.SqlClient;

namespace TAMS.DAL.BL
{
    public class BLTest : BaseContext
    {
        ////test show for user
        public static List<ETest> TestOfUser(int IdUser)
        {
            List<ETest> formTests = TestContext.GetByUser(IdUser);
            return formTests;
        }
        public static int CreateFormTest(Test test)
        {
            //    List<CategoryQuestionOfTest> categoryQuestionOfTests = new List<CategoryQuestionOfTest>();
            return 0;
        }
        
        public static ETest CheckIsFinish(int Id)
        {
            ////if is Finish then count score
            ETest test = TestContext.GetTestOfUser(Id);
            if (test == null) return null;
            if ((test.TimeStart + test.Time) < DateTime.Now)
            {
                TestContext.UpdateStatus(test.Id);
            }
            if(test.Status.ToUpper()==TAMS.Entity.baseEmun.StaticTest.Finish.ToString().ToUpper())TestContext.UpdateScore(test.Id);
            return TestContext.GetTestOfUser(test.Id);
        }
        public static Tuple<List<EQuestion>, List<EUserResult>> GetContentOfTest(ETest test)
        {
            List<EQuestion> questions = QuestionContext.GetByTest(test.Id);
            //get list answers
            List<EUserResult> answers = UserResultContext.GetByTest(test.Id);
            Tuple<List<EQuestion>, List<EUserResult>> list = Tuple.Create(questions, answers);
            return list;
        }

    }
}
