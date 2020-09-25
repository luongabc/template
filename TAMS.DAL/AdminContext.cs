using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAMS.Entity;
using TAMS.Entity.Models;
namespace TAMS.DAL
{
    public class AdminContext:BaseContext
    {
        public static int AddQuestion(EQuestion obj)
        {
            using (var context = MasterDBContext())
            {
                 return context.StoredProcedure("dbo.AddQuestion")
                   .Parameter("Text", obj.Text)
                   .Parameter("CategoryName", obj.CategoryName)
                   .Parameter("CategoryAnswer", obj.CategoryAnswer)
                   .Execute();
            }
        }
        public static int AddCategoryQuestion(CategoryQuestion obj)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.AddCategoryQuestion")
                  .Parameter("Name", obj.Name)
                  .Parameter("CreateDate", obj.CreateDate)
                  .Parameter("ModifyDate", obj.ModifyDate)
                  .Execute();
            }
        }
        public static CategoryQuestion GetByIdCategory(int Id)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.GetByIdCategory")
                    .Parameter("Id", Id)
                    .QuerySingle<CategoryQuestion>();
            }
        }
        public static void UpdateCategory(CategoryQuestion obj)

        {


            using (var context = MasterDBContext())
            {
                context.StoredProcedure("dbo.UpdateCategory")
                  .Parameter("Name", obj.Name)
                  .Parameter("Id", obj.Id)
                  .Parameter("ModifyDate", obj.ModifyDate)                
                  .Execute();


            }

        }
        public static void DeleteCategory(int Id)
        {
            using (var context = MasterDBContext())
            {
                context.StoredProcedure("dbo.DeleteCategory")
                     .Parameter("Id", Id)
                     .Execute();
            }
        }
        public static List<CategoryQuestion> GetDataCategory()
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.GetDataCategory")
                      .QueryMany<CategoryQuestion>();
            }

        }
        
        public static int AddAnswer(List<Answer> obj)
        {
            using (var context = MasterDBContext())
            {
                int count = 0;
                for (int i = 0; i < obj.Count; i++)
                {
                    count += context.StoredProcedure("dbo.AddAnswer")
                       .Parameter("TextAnswer", obj[i].TextAnswer)
                       .Parameter("result", obj[i].result)
                       .Execute();
                }
                return count;
            }
        }
        public static Tuple<List<EQuestion>,int> GetDataQuestion(string Search,string FinterCategoryQuestion, string FilterCategoryAnswer ,int sizePage,int Page)
        {
            using (var context = MasterDBContext())
            { 
                int total = 0;
                var con= context.StoredProcedure("dbo.Question_GetPage")
                  .Parameter("PageIndex", Page)
                  .Parameter("PageSize", sizePage)
                  .Parameter("Search", Search)
                  .Parameter("FilterCategoryAnswer", FilterCategoryAnswer)
                  .Parameter("FinterCategoryQuestion", FinterCategoryQuestion)
                  .ParameterOut("Total", FluentData.DataTypes.Int32);
                List<EQuestion> eQuestions= con.QueryMany<EQuestion>();
                total = con.ParameterValue<int>("Total");
                return Tuple.Create(eQuestions, total);
            }
        }
        public static int DeleteQuestion(int Id)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.DeleteQuestion")
                   .Parameter("Id", Id)
                  .Execute();
            }
        }
        public static EQuestion GetByIdQuestion(int Id)
        {
            using(var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.GetByIdQuestion")
                    .Parameter("Id", Id)
                    .QuerySingle<EQuestion>();
            }
        }
        public static List<Answer> GetByIdAnswer(int IdQuestion)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.GetByIdAnswer")
                    .Parameter("IdQuestion", IdQuestion)
                    .QueryMany<Answer>();
            }
        }
        public static void UpdateAnswer(List<Answer> obj)
        {
            using (var context = MasterDBContext())
            {
                for (int i = 0; i < obj.Count; i++)
                {
                    context.StoredProcedure("dbo.UpdateAnswer")
                    .Parameter("IdQuestion", obj[i].IdQuestion)
                    .Parameter("TextAnswer", obj[i].TextAnswer)
                    .Parameter("result", obj[i].result)
                    .Execute();
                }
            }
        }
        public static void UpdateQuestion(EQuestion obj)
        {
            using (var context = MasterDBContext())
            {
                context.StoredProcedure("dbo.Question_Update")
               .Parameter("Id", obj.Id)
               .Parameter("Text", obj.Text)
               .Parameter("CategoryName", obj.CategoryName)
               .Parameter("ModifyDate",obj.ModifyDate)
               .Parameter("CategoryAnswer", obj.CategoryAnswer)

               .Execute();
            }
        }
        public static int CountAnswer (int IdQuestion)
        {
            int Count = 0;
            using (var context = MasterDBContext())
            {
                var cmd = context.StoredProcedure("dbo.CountAnswer")
                     .Parameter("IdQuestion", IdQuestion)
                    .ParameterOut("Count", FluentData.DataTypes.Int32);
                cmd.Execute();
                Count = cmd.ParameterValue<int>("Count");
            }
            return Count;
        }
        public static int CountQuestion()
        {
            int Count = 0;
            using (var context = MasterDBContext())
            {
                var cmd = context.StoredProcedure("dbo.CountQuestion")
                   
                    .ParameterOut("Count", FluentData.DataTypes.Int32);
                cmd.Execute();
                Count = cmd.ParameterValue<int>("Count");
            }
            return Count;
        }
        public static void DeleteAnswer(int IdQuestion)
        {
            using (var context = MasterDBContext())
            {
                context.StoredProcedure("dbo.DeleteAnswer")
                   .Parameter("IdQuestion", IdQuestion)
                  .Execute();
            }
        }
        public static bool Search(string text)
        {
            using (var context = MasterDBContext())
            {
                List<EQuestion> questions=context.StoredProcedure("dbo.Question_Search")
                  .Parameter("TextQuestion", text)
                  .QueryMany<EQuestion>();
                if (questions.Count > 0) return true;
                return false;
            }
        }
    }
}
