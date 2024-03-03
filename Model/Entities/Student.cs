namespace Model.Entities;

public partial class Student
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Sex { get; set; }

    public int Status { get; set; }

    public DateTime Jointime { get; set; }
}
