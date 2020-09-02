using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAMS.Entity
{
    public class Question
    {
        public int Id { get; set; }

        public string Text { get; set; }
        public int IdCategory { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
        public int Mark { get; set; }
        public string CategoryName { get; set; }
        
        public string Name { get; set; }
        public string TextAnswer { get; set; }
        public int RowId { get; set; }
        public override bool Equals(Object obj)
        {
            Question qs = obj as Question;
            if (this.Id == (qs.Id)) return true;
            else return false;
        }
    }
}
