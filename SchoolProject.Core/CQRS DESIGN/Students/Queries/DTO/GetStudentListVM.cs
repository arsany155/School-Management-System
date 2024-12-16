using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.CQRS_DESIGN.Students.Queries.DTO
{
    public class GetStudentListVM
    {
        public int StudID { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? DepartmentName { get; set; }
    }
}
