namespace StudentAPI.DTO
{
    public class StudentClassDTO
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; } = null!;

        public int StudentAge { get; set; }

        public string StudentAddress { get; set; } = null!;

        public int ClassId { get; set; }
        public string ClassName { get; set; } = null!;
    }
}
