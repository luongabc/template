using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAMS.DAL.ModelEntity;
using TAMS.Entity;

namespace TAMS.DAL.BL
{
    public class BLTest
    {
        //test show for user
        public static List<Test> TestOfUser( int IdUser)
        {
            List<Test> formTests = TestContext.GetByUser(0, 0, -1).Item1;
            List<Test> tests = TestContext.GetByUser(IdUser,0,-1).Item1;
            for(int countFT= formTests.Count-1; countFT>=0;countFT--)
            {
                for(int countT= 0; countT < tests.Count; countT++)
                {
                    if (tests[countT].IdFormTest == formTests[countFT].Id)
                    {
                        formTests.Remove(formTests[countFT]);
                        break;
                    }
                }
            }
            for (int countT = 0; countT < tests.Count; countT++)
            {
                if (tests[countT].Status == (int)Entity.baseEmun.StaticTest.Doing)
                {
                    tests[countT] =CheckIsFinish(tests[countT]);
                    tests[countT].Time = (tests[countT].Time - (TimeSpan)(DateTime.Now - tests[countT].TimeStart));
                }
                formTests.Add(tests[countT]);
            }
            return formTests;
        }
        // 
        public static int CreateFormTest(Test test)
        {
            if (test.Name == null|
                test.IdCategory<1||
                test.NumQuestion<1||
                test.Time==null) return 0;
            test.TimeStart = null;
            test.Score = null;
            test.Id = -1;
            test.IdUser = -1;
            return TestContext.Create(test);
        }
        public static int CreateTest(int IdUser, Test formTest)
        {
            if (IdUser < 1) return 0;
            if (formTest.Name == null |
                formTest.IdCategory < 1 ||
                formTest.NumQuestion < 1 ||
                formTest.Time == null ||
                formTest.IdFormTest != ((int)(Entity.baseEmun.Test.FormTest))
                ) return 0;
            List<Test> tests = TestOfUser(IdUser);
            for (int i = 0; i < tests.Count; i++)
            {
                if (tests[i].IdFormTest == formTest.Id) return 0;
            }
            formTest.TimeStart = DateTime.Now;
            formTest.Score = null;
            formTest.IdFormTest = formTest.Id;
            formTest.IdUser = IdUser;
            return TestContext.Create(formTest);
        }   
        //if is Finish then count score
        public static Test CheckIsFinish(Test test)
        {
            if (test==null) return null;
            if ((test.TimeStart + test.Time) < DateTime.Now)
            {
                TestContext.ChangeStatusTest(test.Id, (int)Entity.baseEmun.StaticTest.Finish);
            }
            int countQuestionFail = UserResultContext.CountQuestionFail(test.Id).Count;
            TestContext.UpdateScore(test.Id, test.NumQuestion - countQuestionFail);
            return TestContext.Get_Test(test.Id);
        }
        public static Tuple<List<Question>, List<UserResult>> GetContentOfTest(Test test)
        {
            if (test.Status == (int)baseEmun.StaticTest.NotStart)
            {
                UserResultContext.Create_RandomQuestionsForTest(test);
                //update status to Doing
                TestContext.ChangeStatusTest(test.Id, (int)baseEmun.StaticTest.Doing);
            }
            List<Question> questions = QuestionContext.GetByTest(test.Id);
            //get list answers
            List<UserResult> answers = UserResultContext.GetByTest(test.Id);
            Tuple<List<Question>, List<UserResult>> list = Tuple.Create(questions, answers);
            return list;
        }

    }
}
