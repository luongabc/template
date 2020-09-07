using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAMS.Entity;

namespace TAMS.DAL
{
    public class AdminContext:BaseContext
    {
        private static AdminContext _instance;
        public static AdminContext Instance()
        {
            if (null == _instance)
            {
                _instance = new AdminContext();
            }
            return _instance;
        }


        public static int AddQuestion(Entity.Question obj)
        {
            using (var context = MasterDBContext())
            {
                 return context.StoredProcedure("dbo.AddQuestion")
                   .Parameter("Text", obj.Text)
                   .Parameter("CategoryName", obj.CategoryName)
                   .Execute();
            }
        }
        public static int AddCategoryQuestion(Entity.CategoryQuestion obj)
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
        public static Entity.CategoryQuestion GetByIdCategory(int Id)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.GetByIdCategory")
                    .Parameter("Id", Id)
                    .QuerySingle<Entity.CategoryQuestion>();
            }
        }
        public static void UpdateCategory(Entity.CategoryQuestion obj)

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
        public static List<Entity.CategoryQuestion> GetDataCategory()

        {


            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.GetDataCategory")
                      .QueryMany<Entity.CategoryQuestion>();


            }

        }
        
        public static int AddAnswer(List<Entity.Answer> obj)
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
        public static List<Entity.Question> GetDataQuestion(int Id)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.GetDataQuestion")
                  .Parameter("PageIndex", Id)
                    .Parameter("PageSize", 10)
                  .QueryMany<Entity.Question>();


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
        public static Entity.Question GetByIdQuestion(int Id)
        {
            using(var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.GetByIdQuestion")
                    .Parameter("Id", Id)
                    .QuerySingle<Entity.Question>();
            }
        }
        public static List<Entity.Answer> GetByIdAnswer(int IdQuestion)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.GetByIdAnswer")
                    .Parameter("IdQuestion", IdQuestion)
                    .QueryMany<Entity.Answer>();
            }
        }
        public static void UpdateAnswer(List<Entity.Answer> obj)
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
        public static void UpdateQuestion(Entity.Question obj)
        {
            using (var context = MasterDBContext())
            {
                context.StoredProcedure("dbo.UpdateQuestion")
               .Parameter("Id", obj.Id)
               .Parameter("Text", obj.Text)
               .Parameter("CategoryName", obj.CategoryName)
               .Parameter("ModifyDate",obj.ModifyDate)

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
                List<Question> questions=context.StoredProcedure("dbo.Question_Search")
                  .Parameter("TextQuestion", text)
                  .QueryMany<Question>();
                if (questions.Count > 0) return true;
                return false;
            }
        }
    }
}
