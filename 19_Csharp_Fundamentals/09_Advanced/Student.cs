public class Student
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Year { get; private set; }

    public Student(string id, string name, int year)
    {
        Id = id;
        Name = name;
        Year = year;
    }

    public void Promote() => Year++;
}