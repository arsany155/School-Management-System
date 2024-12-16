using SchoolProject.Data.CommonLoca;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Instructor : GeneralLocalizableEntity
    {
        public Instructor()
        {
            Instructors = new HashSet<Instructor>();
            Ins_Subjects = new HashSet<Ins_Subject>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InsId { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }
        public int? SupervisorId { get; set; }
        public decimal? Salary { get; set; }
        public int DID { get; set; }


        [ForeignKey(nameof(DID))]
        [InverseProperty("Instructors")]
        public Department? department { get; set; }


        [InverseProperty("Instructor")]
        public Department? departmentManager { get; set; }





        [ForeignKey("SupervisorId")]
        [InverseProperty("Instructors")]
        public Instructor? Supervisors { get; set; }


        [InverseProperty("Supervisors")]
        public ICollection<Instructor> Instructors { get; set; }





        [InverseProperty("instructor")]
        public ICollection<Ins_Subject> Ins_Subjects { get; set; }


    }
}
