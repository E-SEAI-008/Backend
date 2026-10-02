public class StudentProfile
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsEnrolled { get; set; }
    public List<string> Courses { get; set; } = new();
}