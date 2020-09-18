namespace TAMS.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestOfUser")]
    public partial class TestOfUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TestOfUser()
        {
            UserResults = new HashSet<UserResult>();
        }

        public int IdTest { get; set; }

        public int IdUser { get; set; }

        public double Score { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public DateTime? TimeStart { get; set; }

        public int Id { get; set; }

        public virtual Test Test { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserResult> UserResults { get; set; }
    }
}
