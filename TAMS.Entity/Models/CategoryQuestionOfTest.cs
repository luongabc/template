namespace TAMS.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CategoryQuestionOfTest")]
    public partial class CategoryQuestionOfTest
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCategoryQuestion { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdTest { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime CreateDate { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime ModifyDate { get; set; }

        [Key]
        [Column(Order = 4)]
        public double ScoreQuestion { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Numquestion { get; set; }

        public virtual CategoryQuestion CategoryQuestion { get; set; }

        public virtual FormTest FormTest { get; set; }
    }
}
