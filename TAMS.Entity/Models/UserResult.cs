namespace TAMS.Entity
{
    public  class UserResult
    {
        public int? IdAnswer { get; set; }

        public string TextAnswer { get; set; }

        public int IdQuestion { get; set; }

        public int IdTest { get; set; }

        public bool? result { get; set; }

        public int IdUser { get; set; }

        public virtual Answer Answer { get; set; }

        public virtual Question Question { get; set; }

        public virtual Test Test { get; set; }

        public virtual User User { get; set; }
    }
}
