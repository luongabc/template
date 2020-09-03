
using System;
using System.Collections.Generic;
using TAMS.DAL;
using TAMS.Entity;

namespace TAMS.DAL.ModelEntity
{
    public class TestContext:BaseContext
    {
        //using  IdTest =-1 get all
        public static Test Get_Test(int IdTest)
        {
            using(var context = MasterDBContext())
            {

                var cmd= context.StoredProcedure("Test_Get")
                    .Parameter("IdTest", IdTest);
                return cmd.QuerySingle<Test>();
            }
        }
        public static List<Test> Get_ByForm(int idForm)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("Test_GetByForm")
                    .Parameter("IdForm",idForm)
                    .QueryMany<Test>();
            }
        }
        public static Tuple<List<Test>, int> Get_FormTests(int NumItem,int Page)
        {
            using (var context = MasterDBContext())
            {
                var cmd = context.StoredProcedure("Test_GetByUser")
                    .Parameter("NumItem", NumItem)
                    .Parameter("Page", Page)
                    .ParameterOut("TotalItem", FluentData.DataTypes.Int32)
                    .Parameter("IdUser", (int)(Entity.baseEmun.Test.UserFormTest));
                List<Test> tests = cmd.QueryMany<Test>();
                int total = cmd.ParameterValue<int>("TotalItem");
                Tuple<List<Test>, int> tuple = Tuple.Create(tests, total);
                return tuple;
            }
        }
        public static int Update_ModifyTimeTest(int IdTest)
        {
            using(var context = MasterDBContext())
            {
                return context.StoredProcedure("Test_UpdateModifyTime")
                    .Parameter("idTest", IdTest)
                    .Execute();
            }
        }
        public static int SaveResultOfUser(List<UserResult> userResults, int IdTest)
        {
            int count = 0;
            using (var context = MasterDBContext())
            {
                //getUserTest
                DateTime now = DateTime.Now;
                Test test = Get_Test(IdTest);
                if (test == null) return 0;
                TimeSpan? v = (TimeSpan)(now - test.TimeStart)- new TimeSpan(0,0,10);
                if (v > test.Time) return 0;
                if (test.Status != (int)baseEmun.StaticTest.Doing) return 0;
                foreach (UserResult item in userResults)
                {
                    if (context.StoredProcedure("UserResult_Update")
                        .Parameter("IdAnswer", item.IdAnswer)
                        .Parameter("TextAnswer", item.TextAnswer)
                        .Parameter("result", item.result)
                        .Parameter("IdQuestion", item.IdQuestion)
                        .Parameter("IdUserTest", test.Id)
                        .Execute() > 0
                    ) count++;
                }
            }
            return count;
        }
        public static int ChangeStatusTest(int IdTest, int newStatus)
        {
            using (var context = MasterDBContext()) { 
                return context.StoredProcedure("Test_UpdateStatus")
                    .Parameter("Id", IdTest)
                    .Parameter("Status", newStatus)
                    .Execute();
            }
        }
        public static int Create(Test test)
        {
            using(var context = MasterDBContext())
            {
                string time = test.Time.ToString();
                int count = QuestionContext.GetByCategoryTest(test.IdCategory).Count;
                if (count < test.NumQuestion) return 0;
                return context.StoredProcedure("Test_Create")
                    .Parameter("Name", test.Name)
                    .Parameter("Time", time)
                    .Parameter("Description", test.Description)
                    .Parameter("IdCategory", test.IdCategory)
                    .Parameter("NumQuestion", test.NumQuestion)
                    .Parameter("IdFormTest", test.Id)
                    .Parameter("IdUser", test.IdUser)
                    .Execute();
            }
        }
        public static Tuple<List<Test>, int> GetByUser(int IdUser,int NumItem,int Page)
        {
            using (var context = MasterDBContext())
            {
                //Get test of user
                var cmd = context.StoredProcedure("Test_GetByUser")
                    .Parameter("NumItem", NumItem)
                    .Parameter("Page", Page)
                    .ParameterOut("TotalItem", FluentData.DataTypes.Int32)
                    .Parameter("IdUser", IdUser);
                List<Test> tests = cmd.QueryMany<Test>();
                int total = cmd.ParameterValue<int>("TotalItem");
                Tuple<List<Test>, int> tuple = Tuple.Create(tests, total);
                return tuple;
            }
        }
        public static int UpdateScore(int IdTest,int Score)
        {
            using (var context = MasterDBContext())
            {
                int count = context.StoredProcedure("Test_UpdateScore")
                    .Parameter("IdTest", IdTest)
                    .Parameter("Score", Score)
                    .Execute();
                return count;
            }
        }
        public static int Delete(int IdTest)
        {
            using(var context = MasterDBContext())
            {
                return context.StoredProcedure("Test_Delete")
                    .Parameter("IdTest", IdTest)
                    .Execute();
            }
        }
    }
}
