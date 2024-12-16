namespace SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Queries.DTO
{
    public class GetUserByIdDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}
