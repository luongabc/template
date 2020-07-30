using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Entity
{
    public class News
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Avatar { get; set; }
        public String Sapo { get; set; }
        public String Description { set; get; }
        public Boolean Active { get; set; } 
        public int CategoryId { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
