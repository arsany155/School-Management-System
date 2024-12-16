namespace SchoolProject.Core.CQRS_DESIGN.Students.Queries.DTO
{
    public class GetStudentPaginatedListVM
    {
        public int StudID { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? DepartmentName { get; set; }

        //public GetStudentPaginatedListVM()
        //{

        //}
        //public GetStudentPaginatedListVM(int studID, string? name, string? address, string departmentName)
        //{
        //    StudID = studID;
        //    Name = name;
        //    Address = address;
        //    DepartmentName = departmentName;
        //}
    }
}
