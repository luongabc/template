using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAMS.DAL
{
    public class CategoryContext : BaseContext
    {
        private static CategoryContext _instance;

        public static CategoryContext Instance()
        {
            if (_instance == null)
            {
                _instance = new CategoryContext();
            }
            return _instance;
        }
        public List<Category> GetCategoryById(int id)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("Category_GetOne")
                    .Parameter("Id", id)
                    .QueryMany<Category>();
            }
        }
        public Tuple<List<Category>,int> GetCategoryByPage(int page,int sizePage)
        {
            using(var context = MasterDBContext())
            {
                var cmd = context.StoredProcedure("Category_GetByPagging")
                   .Parameter("page", page)
                   .Parameter("sizePage", sizePage)
                   .ParameterOut("totalItem", FluentData.DataTypes.Int32);
                    
                List<Category> listCate =cmd.QueryMany<Category>();
                int totalItem = cmd.ParameterValue <int> ("totalItem");
                int div = totalItem % sizePage;
                int numPage = totalItem / sizePage;
                if (div > 0) numPage++;
                Tuple<List<Category>,int> DataReturn= Tuple.Create(listCate, numPage);
                return DataReturn;
            }    
        }
        public List<Category> GetAllCategories()
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("Category_GetAll")
                    .QueryMany<Category>();
            }
        }
        public int Create(Category obj)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("Category_Create")
                    .Parameter("Name", obj.Name)
                    .Parameter("Slug", obj.Slug)
                    .Parameter("ParentId", obj.ParentId)
                    .Parameter("Active", obj.Active)
                    .QuerySingle<int>();
            }
        }

        public int Update(Category obj)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("Category_Update")
                    .Parameter("Id",obj.Id)
                    .Parameter("Name", obj.Name)
                    .Parameter("Slug", obj.Slug)
                    .Parameter("ParentId", obj.ParentId)
                    .Parameter("Active", obj.Active)
                    .QuerySingle<int>();
            }
        }
        public int Delete(int Id)
        {
            using(var context = MasterDBContext()) {
                return context.StoredProcedure("Category_Delete")
                    .Parameter("Id", Id)
                    .QuerySingle<int>();
            }
        }

    }
}
