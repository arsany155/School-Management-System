﻿using SchoolProject.Data.CommonLoca;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Subject : GeneralLocalizableEntity
    {
        public Subject()
        {
            StudentSubjects = new HashSet<StudentSubject>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
            Ins_Subjects = new HashSet<Ins_Subject>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubID { get; set; }

        [StringLength(500)]
        public string? SubjectNameAr { get; set; }


        [StringLength(500)]
        public string? SubjectNameEn { get; set; }

        public DateTime? Period { get; set; }

        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }

        [InverseProperty("Subject")]
        public virtual ICollection<Ins_Subject> Ins_Subjects { get; set; }
    }
}
