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
        public CategoryQuestionOfTest()
        {

        }
        public CategoryQuestionOfTest(int IdCategoryQuestion,int IdTest,int ScoreQuestion,int Numquestion)
        {
            this.IdCategoryQuestion = IdCategoryQuestion;
            this.IdTest = IdTest;
            this.ScoreQuestion = ScoreQuestion;
            this.Numquestion = Numquestion;
        }
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
        public int ScoreQuestion { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Numquestion { get; set; }

    }
}
