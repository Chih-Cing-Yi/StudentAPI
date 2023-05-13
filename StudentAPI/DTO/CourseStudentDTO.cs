namespace StudentAPI.DTO
{
    public class CourseStudentDTO
    {
        public int CourseId { get; set; }

        public int StudentId { get; set; }
        public string StudentName { get; set; } = null!;
    }
}
