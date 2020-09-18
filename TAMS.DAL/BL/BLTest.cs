using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAMS.DAL.ModelEntity;
using TAMS.Entity.Models;
using TAMS.Entity;
namespace TAMS.DAL.BL
{
    public class BLTest
    {
        ////test show for user
        public static List<ETest> TestOfUser(int IdUser)
        {
            List<ETest> formTests = TestContext.GetByUser(IdUser);
            return formTests;
        }
        //public static int CreateFormTest(Test test)
        //{
        //    if (test.Name == null|
        //        test.IdCategory<1||
        //        test.NumQuestion<1||
        //        test.Time==null) return 0;
        //    test.TimeStart = null;
        //    test.Score = null;
        //    test.Id = -1;
        //    test.IdUser = -1;
        //    return TestContext.Create(test);
        //}
        ////if is Finish then count score
        public static ETest CheckIsFinish(int Id)
        {
            ETest test = TestContext.GetTestOfUser(Id);
            if (test == null) return null;
            if ((test.TimeStart + test.Time) < DateTime.Now)
            {
                TestContext.ChangeStatusTest(test.Id);
            }
            if(test.Status.ToUpper()==TAMS.Entity.baseEmun.StaticTest.Finish.ToString().ToUpper())TestContext.UpdateScore(test.Id);
            return TestContext.GetTestOfUser(test.Id);
        }
        public static Tuple<List<EQuestion>, List<UserResult>> GetContentOfTest(ETest test)
        {
            List<EQuestion> questions = QuestionContext.GetByTest(test.Id);
            //get list answers
            List<UserResult> answers = UserResultContext.GetByTest(test.Id);
            Tuple<List<EQuestion>, List<UserResult>> list = Tuple.Create(questions, answers);
            return list;
        }

    }
}
