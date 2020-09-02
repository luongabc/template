using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public int AddQuestion(Entity.Question obj)
        {
            using (var context = MasterDBContext())
            {
                 return context.StoredProcedure("dbo.AddQuestion")
                   .Parameter("Text", obj.Text)
                   .Parameter("CategoryName", obj.CategoryName)
                   .Execute();
            }
        }
        public int AddCategoryQuestion(Entity.CategoryQuestion obj)
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
        public Entity.CategoryQuestion GetByIdCategory(int Id)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.GetByIdCategory")
                    .Parameter("Id", Id)
                    .QuerySingle<Entity.CategoryQuestion>();
            }
        }
        public void UpdateCategory(Entity.CategoryQuestion obj)

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
        public void DeleteCategory(int Id)
        {
            using (var context = MasterDBContext())
            {
                context.StoredProcedure("dbo.DeleteCategory")
                     .Parameter("Id", Id)
                     .Execute();
            }
        }
        public List<Entity.CategoryQuestion> GetDataCategory()

        {


            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.GetDataCategory")
                      .QueryMany<Entity.CategoryQuestion>();


            }

        }
        
        public int AddAnswer(List<Entity.Answer> obj)
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
        public List<Entity.Question> GetDataQuestion(int Id)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.GetDataQuestion")
                  .Parameter("PageIndex", Id)
                    .Parameter("PageSize", 10)
                  .QueryMany<Entity.Question>();


            }
        }
        public int DeleteQuestion(int Id)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.DeleteQuestion")
                   .Parameter("Id", Id)
                  .Execute();
            }
        }
        public Entity.Question GetByIdQuestion(int Id)
        {
            using(var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.GetByIdQuestion")
                    .Parameter("Id", Id)
                    .QuerySingle<Entity.Question>();
            }
        }
        public List<Entity.Answer> GetByIdAnswer(int IdQuestion)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("dbo.GetByIdAnswer")
                    .Parameter("IdQuestion", IdQuestion)
                    .QueryMany<Entity.Answer>();
            }
        }
        public void UpdateAnswer(List<Entity.Answer> obj)
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
        public void UpdateQuestion(Entity.Question obj)
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
        public int CountAnswer (int IdQuestion)
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
        public int CountQuestion()
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
        public void DeleteAnswer(int IdQuestion)
        {
            using (var context = MasterDBContext())
            {
                context.StoredProcedure("dbo.DeleteAnswer")
                   .Parameter("IdQuestion", IdQuestion)
                  .Execute();


            }
        }
    }
}
