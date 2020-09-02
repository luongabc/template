using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TAMS.Entity
{
    public class LoginAdmin
    {
        public int Id { get; set; }

        public string Avatar { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string PasswordNew { get; set; }
       
        public DateTime? CreateDate { get; set; }
        public HttpPostedFileBase file { get; set; }
    }
}
