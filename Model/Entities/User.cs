namespace Model.Entities;

public partial class User
{
    public string? Name { get; set; }

    public string? Password { get; set; }

    public DateTime? Createdate { get; set; }

    public bool? Isdeleted { get; set; }

    public int Id { get; set; }
}
