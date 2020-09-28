
using System;
using System.Collections.Generic;
using System.Data;
using TAMS.DAL;
using TAMS.Entity;
using TAMS.Entity.Models;

namespace TAMS.DAL.ModelEntity
{
    public class TestContext: BaseContext
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
        public static ETest GetTestOfUser(int Id)
        {
            using (var context = MasterDBContext())
            {
                ETest res;
                var cmd = context.StoredProcedure("TestOfUser_Get")
                    .Parameter("IdTest", Id);
                res = cmd.QuerySingle<ETest>();
                return res;
            }
        }
        public static ETest SearchTestOfUser(int IdTest, int IdUser)
        {
            using (var context = MasterDBContext())
            {
                ETest res;
                var cmd = context.StoredProcedure("TestOfUser_Search")
                    .Parameter("IdTest", IdTest)
                    .Parameter("IdUser", IdUser);
                res = cmd.QuerySingle<ETest>();
                return res;
            }
        }
        public static int UpdateTimeStart(int idTestOfUser)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("TestOfUser_UpdateTimeStart")
                    .Parameter("idTestOfUser", idTestOfUser)
                    .Execute();
            }
        }
        public static int UpdateScore(int Id)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("Test_CountScore")
                    .Parameter("idTestOfUser", Id)
                    .Execute();
            }
        }
        public static Tuple<List<ETest>,int> Get_TestOfUserByForm(string search, int idFormTest,int Page,int size)
        {
            using (var context = MasterDBContext())
            {
                var con = context.StoredProcedure("TestOfUser_GetByForm")
                .Parameter("search", search)
                .Parameter("IdForm", idFormTest)
                .Parameter("Page", Page)
                .Parameter("size", size)
                .ParameterOut("TotalItem", FluentData.DataTypes.Int32);
                return Tuple.Create(con.QueryMany<ETest>(), con.ParameterValue<int>("TotalItem"));
            }
        }
        public static FormTest Get_FormTest(int IdFormTest)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("FormTest_GetById")
                    .Parameter("IdFormTest", IdFormTest)
                    .QuerySingle<FormTest>();
            }
        }
        public static Tuple<List<FormTest>, int> Get_FormTests(string search, int SizePage, int Page)
        {
            using (var context = MasterDBContext())
            {
                var cmd = context.StoredProcedure("FormTest_GetPage")
                    .Parameter("SizePage", SizePage)
                    .Parameter("Page", Page)
                    .Parameter("search", search)
                    .ParameterOut("TotalItem", FluentData.DataTypes.Int32);
                List<FormTest> tests = cmd.QueryMany<FormTest>();
                int total = cmd.ParameterValue<int>("TotalItem");
                Tuple<List<FormTest>, int> tuple = Tuple.Create(tests, total);
                return tuple;
            }
        }
        //public static int Update_ModifyTimeTest(int IdTest)
        //{
        //    using(var context = MasterDBContext())
        //    {
        //        return context.StoredProcedure("Test_UpdateModifyTime")
        //            .Parameter("idTest", IdTest)
        //            .Execute();
        //    }
        //}
        public static int SaveResultOfUser(List<UserResult> userResults, int IdTest)
        {
            int count = 0;
            using (var context = MasterDBContext())
            {
                //getUserTest
                DateTime now = DateTime.Now;
                ETest test = GetTestOfUser(IdTest);
                if (test == null) return 0;
                TimeSpan? v = (TimeSpan)(now - test.TimeStart) - new TimeSpan(0, 0, 10);
                if (v > test.Time) return 0;
                if (test.Status != baseEmun.StaticTest.Doing.ToString()) return 0;
                foreach (UserResult item    in userResults)
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
        public static int Create(FormTest test)
        {
            using (var context = MasterDBContext())
            {
                string time = test.Time.ToString();
                return context.StoredProcedure("FormTest_Create")
                    .Parameter("Name", test.Name)
                    .Parameter("Time", time)
                    .Parameter("Description", test.Description)
                    .Execute();
            }
        }
        public static List<ETest> GetByUser(int IdUser)
        {
            using (var context = MasterDBContext())
            {
                //Get test of user
                var cmd = context.StoredProcedure("TestOfUser_GetByUser ")
                    .Parameter("IdUser", IdUser);
                List<ETest> tests = cmd.QueryMany<ETest>();
                return tests;
            }
        }
        public static FormTest GetByName(String Name)
        {
            using (var context = MasterDBContext())
            {
                //Get test of user
                var cmd = context.StoredProcedure("FormTest_GetByName")
                    .Parameter("Name", Name);
                FormTest tests = cmd.QuerySingle<FormTest>();
                return tests;
            }
        }
        public static int UpdateStatus(int IdTest)
        {
            using (var context = MasterDBContext())
            {
                int count = context.StoredProcedure("TestOfUser_UpdateStatus")
                    .Parameter("Id", IdTest)
                    .Execute();
                return count;
            }
        }

        //public static int Delete(int IdTest)
        //{
        //    using(var context = MasterDBContext())
        //    {
        //        return context.StoredProcedure("Test_Delete")
        //            .Parameter("IdTest", IdTest)
        //            .Execute();
        //    }
        //}

        //Category of test
        
    }
}
