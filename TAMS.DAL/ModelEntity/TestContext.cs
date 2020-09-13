
using System;
using System.Collections.Generic;
using TAMS.DAL;
using TAMS.Entity;

namespace TAMS.DAL.ModelEntity
{
    public class TestContext:BaseContext
    {
        //using  IdTest =-1 get all
        public static ETest Get_TestOfUser(int IdTest, int IdUser)
        {
            using (var context = MasterDBContext())
            {
                var cmd = context.StoredProcedure("TestOfUser_Get")
                    .Parameter("IdTest", IdTest)
                    .Parameter("IdUser", IdUser);
                return cmd.QuerySingle<ETest>();
            }
        }
        //public static List<Test> Get_FormByCategory(int Id)
        //{
        //    using (var context = MasterDBContext())
        //    {
        //        return context.StoredProcedure("Test_GetByCategory")
        //            .Parameter("IdCategory", Id)
        //            .QueryMany<Test>();
        //    }
        //}
        //public static List<Test> Get_ByForm(int idForm)
        //{
        //    using (var context = MasterDBContext())
        //    {
        //        return context.StoredProcedure("Test_GetByForm")
        //            .Parameter("IdForm",idForm)
        //            .QueryMany<Test>();
        //    }
        //}
        //public static Tuple<List<Test>, int> Get_FormTests(int NumItem,int Page)
        //{
        //    using (var context = MasterDBContext())
        //    {
        //        var cmd = context.StoredProcedure("Test_GetByUser")
        //            .Parameter("NumItem", NumItem)
        //            .Parameter("Page", Page)
        //            .ParameterOut("TotalItem", FluentData.DataTypes.Int32)
        //            .Parameter("IdUser", (int)(Entity.baseEmun.Test.UserFormTest));
        //        List<Test> tests = cmd.QueryMany<Test>();
        //        int total = cmd.ParameterValue<int>("TotalItem");
        //        Tuple<List<Test>, int> tuple = Tuple.Create(tests, total);
        //        return tuple;
        //    }
        //}
        //public static int Update_ModifyTimeTest(int IdTest)
        //{
        //    using(var context = MasterDBContext())
        //    {
        //        return context.StoredProcedure("Test_UpdateModifyTime")
        //            .Parameter("idTest", IdTest)
        //            .Execute();
        //    }
        //}
        //public static int SaveResultOfUser(List<UserResult> userResults, int IdTest)
        //{
        //    int count = 0;
        //    using (var context = MasterDBContext())
        //    {
        //        //getUserTest
        //        DateTime now = DateTime.Now;
        //        Test test = Get_Test(IdTest);
        //        if (test == null) return 0;
        //        TimeSpan? v = (TimeSpan)(now - test.TimeStart)- new TimeSpan(0,0,10);
        //        if (v > test.Time) return 0;
        //        if (test.Status != (int)baseEmun.StaticTest.Doing) return 0;
        //        foreach (UserResult item in userResults)
        //        {
        //            if (context.StoredProcedure("UserResult_Update")
        //                .Parameter("IdAnswer", item.IdAnswer)
        //                .Parameter("TextAnswer", item.TextAnswer)
        //                .Parameter("result", item.result)
        //                .Parameter("IdQuestion", item.IdQuestion)
        //                .Parameter("IdUserTest", test.Id)
        //                .Execute() > 0
        //            ) count++;
        //        }
        //    }
        //    return count;
        //}
        public static int ChangeStatusTest(int IdTest)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("Test_UpdateStatus")
                    .Parameter("Id", IdTest)
                    .Execute();
            }
        }
        //public static int Create(Test test)
        //{
        //    using(var context = MasterDBContext())
        //    {
        //        string time = test.Time.ToString();
        //        int count = QuestionContext.GetByCategoryTest(test.IdCategory).Count;
        //        if (count < test.NumQuestion) return 0;
        //        return context.StoredProcedure("Test_Create")
        //            .Parameter("Name", test.Name)
        //            .Parameter("Time", time)
        //            .Parameter("Description", test.Description)
        //            .Parameter("IdCategory", test.IdCategory)
        //            .Parameter("NumQuestion", test.NumQuestion)
        //            .Parameter("IdFormTest", test.Id)
        //            .Parameter("IdUser", test.IdUser)
        //            .Execute();
        //    }
        //}
        public static List<ETest> GetByUser(int IdUser)
        {
            using (var context = MasterDBContext())
            {
                //Get test of user
                var cmd = context.StoredProcedure("Test_GetByUser")
                    .Parameter("IdUser", IdUser);
                List<ETest> tests = cmd.QueryMany<ETest>();
                return tests;
            }
        }
        //public static int UpdateScore(int IdTest,int Score)
        //{
        //    using (var context = MasterDBContext())
        //    {
        //        int count = context.StoredProcedure("Test_UpdateScore")
        //            .Parameter("IdTest", IdTest)
        //            .Parameter("Score", Score)
        //            .Execute();
        //        return count;
        //    }
        //}
        //public static int Delete(int IdTest)
        //{
        //    using(var context = MasterDBContext())
        //    {
        //        return context.StoredProcedure("Test_Delete")
        //            .Parameter("IdTest", IdTest)
        //            .Execute();
        //    }
        //}
    }
}
