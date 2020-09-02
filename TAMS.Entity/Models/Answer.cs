using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAMS.Entity
{
    public class Answer
    {
        public int Id { get; set; }

        public string TextAnswer { get; set; }

        public int IdQuestion { get; set; }

        public bool result { get; set; }
    }
}
