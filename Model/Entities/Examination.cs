namespace Model.Entities;

public partial class Examination
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime Createdatetime { get; set; }
}
