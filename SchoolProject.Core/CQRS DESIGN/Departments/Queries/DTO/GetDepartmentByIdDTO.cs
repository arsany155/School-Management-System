using SchoolProject.Core.Wrapper;

namespace SchoolProject.Core.CQRS_DESIGN.Departments.Queries.DTO
{
    public class GetDepartmentByIdDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public PaginationResult<StudentDTO>? Students { get; set; }
        public List<SubjectDTO>? Subjects { get; set; }
        public List<InstructorDTO>? Instructors { get; set; }
    }

    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public StudentDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class SubjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class InstructorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
