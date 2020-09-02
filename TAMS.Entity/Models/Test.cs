namespace TAMS.Entity
{
    using System;
    using System.Collections.Generic;
    public class Test :IComparable<Test>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TimeSpan Time { get; set; }

        public int NumQuestion { get; set; }

        public DateTime CreateDate { get; set; }

        public string Description { get; set; }

        public DateTime ModifyDate { get; set; }

        public int IdCategory { get; set; }

        public String UserName { get; set; }
        public String CategoryTestName { get; set; }
        public int IdUser { get; set; }

        public int Status { get; set; }

        public DateTime? TimeStart { get; set; }

        public int? Score { get; set; }
        public int IdFormTest { get; set; }
        public int CompareTo(Test other)
        {
            if (this.Score == null) return -1;
            if (other.Score == null) return 1;
                return (int)(other.Score  - this.Score);
        }
    }
}
