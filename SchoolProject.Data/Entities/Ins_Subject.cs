using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Ins_Subject
    {
        [Key]
        public int InsId { get; set; }

        [Key]
        public int SubjectId { get; set; }


        [ForeignKey(nameof(InsId))]
        [InverseProperty("Ins_Subjects")]
        public Instructor? instructor { get; set; }


        [ForeignKey(nameof(SubjectId))]
        [InverseProperty("Ins_Subjects")]
        public Subject? Subject { get; set; }



    }
}
