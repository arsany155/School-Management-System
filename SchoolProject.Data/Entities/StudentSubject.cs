﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class StudentSubject
    {
        [Key]

        public int StudID { get; set; }

        [Key]
        public int SubID { get; set; }

        public decimal? grade { get; set; }

        [ForeignKey("StudID")]
        [InverseProperty("StudentSubjects")]
        public virtual Student? Student { get; set; }

        [ForeignKey("SubID")]
        public virtual Subject? Subject { get; set; }
    }
}
