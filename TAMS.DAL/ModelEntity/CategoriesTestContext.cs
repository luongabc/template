
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAMS.DAL;
using TAMS.Entity.Models;

namespace TAMS.DAL.ModelEntity
{
    public class CategoriesTestContext : BaseContext
    {
        public static Tuple<List<CategoryQuestion>,int> Get(int IdCategoryTest,int NumItem, int Page)
        {
            using(var context = MasterDBContext())
            {
                var cmd = context.StoredProcedure("CategoriesTest_Get")
                    .Parameter("IdCategoryTest", IdCategoryTest)
                    .Parameter("NumItem", NumItem)
                    .Parameter("Page", Page)
                    .ParameterOut("TotalItem", FluentData.DataTypes.Int32);
                List<CategoryTest> categoryTests = cmd.QueryMany<CategoryTest>();
                int total = cmd.ParameterValue <int >("TotalItem");
                Tuple<List<CategoryTest>, int> tuple =Tuple.Create(categoryTests, total);
                return tuple;
            }
        }
        public static int Delete(int IdCategoryTest)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("CategoriesTest_Delete")
                    .Parameter("IdCategoryTest", IdCategoryTest)
                    .Execute();
            }
        }
        public static int Create(string NameCategoryTest)
        {
            using (var context = MasterDBContext())
            {
                return context.StoredProcedure("CategoryTest_Create")
                    .Parameter("Name", NameCategoryTest)
                    .Execute();
            }
        }
        public static int Edit(CategoryTest categoryTest)
        {
            using (var context = MasterDBContext())
            {
                return 0;
            }
        }
    }
}
