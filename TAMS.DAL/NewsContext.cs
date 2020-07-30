using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAMS.DAL
{
    public class NewsContext : BaseContext
    {
        private static NewsContext _instance;
        public static NewsContext Instance()
        {
            if (null == _instance)
            {
                _instance = new NewsContext();
            }
            return _instance;
        }

        public int Create(Entity.News obj)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("News_Create")
                    .Parameter("Name", obj.Name)
                    .Parameter("Avatar", obj.Avatar)
                    .Parameter("Sapo", obj.Sapo)
                    .Parameter("Description", obj.Description)
                    .Parameter("Active",obj.Active)
                    .Parameter("CategoryId",obj.CategoryId)
                    .QuerySingle<int>();
            }
        }

        public int Update(Entity.News obj)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("News_Update")
                    .Parameter("Id", obj.Id)
                    .Parameter("Name", obj.Name)
                    .Parameter("Avatar", obj.Avatar)
                    .Parameter("Sapo", obj.Sapo)
                    .Parameter("Description", obj.Description)
                    .Parameter("Active", obj.Active)
                    .Parameter("CategoryId", obj.CategoryId)
                    .QuerySingle<int>();
            }
        }

        public int Delete(int id)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("News_Delete")
                    .Parameter("Id", id)
                    .QuerySingle<int>();
            }
        }
        public List<News> GetAllNews()
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("News_GetAll")
                    .QueryMany<News>();
            }
        }
        public News GetNewsById(int id)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("News_Get")
                    .Parameter("Id", id)
                    .QuerySingle<News>();
            }
        }
        public List<Entity.News> GetByCateNewsId(int cateNewsId, int top)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("FE_News_GetByCateId")
                    .Parameter("CateNewsId", cateNewsId)
                    .Parameter("Top", top)
                    .QueryMany<Entity.News>();
            }
        }

        //public List<Entity.News> GetHighlight(int top, string lang)
        //{
        //    using (var context = MasterDBContext())
        //    {
        //        return context.StoredProcedure("FE_News_GetHighlight")
        //            .Parameter("Top", top)
        //            .Parameter("Lang", lang)
        //            .QueryMany<Entity.News>();
        //    }
        //}

        //public List<Entity.News> Search(int catenewsId, string lang, int pageIndex, int pageSize, out int totalRecord)
        //{
        //    totalRecord = 0;
        //    List<Entity.News> listReturn = new List<Entity.News>();
        //    using (var context = MasterDBContext())
        //    {
        //        var cmd = context.StoredProcedure("FE_News_Search")
        //            .Parameter("CateNewsId", catenewsId)
        //            .Parameter("Lang", lang)
        //            .Parameter("PageIndex", pageIndex)
        //            .Parameter("PageSize", pageSize)
        //            .ParameterOut("TotalRecord", FluentData.DataTypes.Int32);
        //        listReturn = cmd.QueryMany<Entity.News>();
        //        totalRecord = cmd.ParameterValue<int>("TotalRecord");
        //    }
        //    return listReturn;
        //}
        public Tuple<List<Entity.News>,int> GetNewsOfPage(int categoryId, int page,int sizePage)
        {
            int totalRecord = 0;
            List<Entity.News> listNews = new List<Entity.News>();
            using (var context = MasterDBContext())
            {
                var cmd = context.StoredProcedure("sp_News_GetByPaggingAndCategory")
                    .Parameter("CategoryId", categoryId)
                    .Parameter("PageIndex", page)
                    .Parameter("PageSize", sizePage)
                    .ParameterOut("TotalRecord", FluentData.DataTypes.Int32);
                listNews = cmd.QueryMany<Entity.News>();
                totalRecord = cmd.ParameterValue<int>("TotalRecord");
            }
            Tuple<List<Entity.News>, int> dataReturn = Tuple.Create(listNews, totalRecord);
            return dataReturn;
        }
    }
}
