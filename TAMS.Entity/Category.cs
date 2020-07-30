using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Category
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Slug { get; set; }
        public int ParentId { get; set; }
        public  bool Active { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
