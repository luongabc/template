using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAMS.Entity.Models;
namespace TAMS.Entity
{
    public class ETest : TestOfUser
    {
        public string UserName { get; set; }
        public string Name { get; set; }

        public TimeSpan Time { get; set; }

        public int TotalQuestion { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }
        public int IdForm { get; set; }

    }
}
