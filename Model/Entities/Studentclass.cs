namespace Model.Entities;

public partial class Studentclass
{
    public long Id { get; set; }

    public long Studentid { get; set; }

    public long Classid { get; set; }

    public DateTime Jointime { get; set; }
}
