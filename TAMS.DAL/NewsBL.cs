using Data.WorkDb;
using project_Entity.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace project_BLL.Models
{
    public class NewsBL
    {
        private int numNewsOfPage;
        
        public NewsBL()
        {
            numNewsOfPage = 7;
        }
        public NewsBL(int number)
        {
            numNewsOfPage = number;
        }
        public News GetNew(int Id)
        {
            if (Id > 0)
            {
                return NewsDB.GetNew(Id);
            }
            else return null;
        }
        public List<News> GetMoreNews( String search)
        {
            if (search == "") search = null;
            return NewsDB.GetNews(search);

        }
        public int DeleteNew(int Id)
        {
            if (Id > 0)
            {
                return NewsDB.Delete(Id);
            }
            return -1;
        }
        public int Update(News news, HttpPostedFileBase fileAvatar)
        {

            if (news.Id>0 && fileAvatar != null && fileAvatar.ContentLength > 0)
            {
                String name = fileAvatar.FileName.ToString();
                if (name.IndexOf(".png") == name.Length - 4
                    || name.IndexOf(".jpg") == name.Length - 4)
                {
                    if (news.Name != null
                        && news.Sapo != null
                        && news.Description != null
                        && news.CategoryId > 0)
                    {
                        news.Avatar = name;
                        return NewsDB.Edit(news);
                    }
                }
            }
            return -1;
        }
        public int Create(News news,HttpPostedFileBase fileAvatar)
        {
            if(fileAvatar!=null && fileAvatar.ContentLength>0)
            {
                String name = fileAvatar.FileName.ToString();
                if (name.IndexOf(".png")==name.Length-4
                    || name.IndexOf(".jpg") == name.Length - 4)
                {
                    if (news.Name != null
                        && news.Sapo != null
                        && news.Description != null
                        && news.CategoryId > 0)
                    {
                        news.Avatar = name;
                        return NewsDB.Create(news);
                    }
                }
            }
            return -1;
        }
        public int getNumPage()
        {
            int numberNews = NewsDB.GetNews(null).Count;
            return numberNews / this.numNewsOfPage;
        }
        public List<News> GetNewsOfCategory(int numberStart, int CategoryId)
        {
            if( CategoryId > 0)
            {
                return NewsDB.GetNewsOfCategory(numberStart, numberStart+this.numNewsOfPage, CategoryId);
            }
            return null;
        }
        public Tuple<List<News>,int> GetNewsOfPage(int pageIndex,int pageSize)
        {
            if (pageIndex > 0&& pageSize>0)
            {
                return NewsDB.GetNewsPage(pageIndex, pageSize);
            }
            return null;
        }
        public Tuple<List<News>, int> GetNewsOfPageCategory(int category,int pageIndex, int pageSize)
        {
            if (pageIndex > 0 && pageSize > 0)
            {
                return NewsDB.GetNewsPageCategory(category, pageIndex, pageSize);
            }
            return null;
        }
    }
}
