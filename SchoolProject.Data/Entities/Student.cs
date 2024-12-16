﻿using SchoolProject.Data.CommonLoca;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Student : GeneralLocalizableEntity
    {

        public Student()
        {
            StudentSubjects = new HashSet<StudentSubject>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudID { get; set; }

        public string? NameAr { get; set; }

        public string? NameEn { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }


        [StringLength(500)]
        public string? Phone { get; set; }

        public int? DID { get; set; }


        [ForeignKey("DID")]
        public virtual Department? Department { get; set; }


        [InverseProperty("Student")]
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }

    }
}