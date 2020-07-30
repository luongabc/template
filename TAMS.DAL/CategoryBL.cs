using Data.WorkDb;
using project_Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_db.Models
{
    public  class CategoryBL
    {
        public List<Category> GetCategories()
        {
            return CategoriesBD.GetCategories();
        }
        public Category GetCategory(int Id)
        {
            return CategoriesBD.GetCategory(Id);
        }
        public int Delete(int Id)
        {
            if (Id > 0)
            {
                return CategoriesBD.Delete(Id);
            }
            return -1;
        }
        public int Create(Category category)
        {
            if (category.Name!=null
                && category.ParentId>-1
                && category.Slug!=null)
            {
                return CategoriesBD.Create(category);
            }
            return -1;
        }
        public int Update(Category category)
        {
            if (
                category.Id>0
                && category.Name != null
                && category.ParentId > -1
                && category.Slug != null)
            {
                return CategoriesBD.Edit(category);
            }
            return -1;
        }
    }
}
