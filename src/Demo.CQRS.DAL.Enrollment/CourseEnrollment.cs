namespace Demo.CQRS.DAL.Enrollment
{
    public class CourseEnrollment
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public string ContactEmail { get; set; }
        public DateTime EnrollmentTimestamp { get; set; }
        public DateTime CanceledTimestamp { get; set; }
    }
}