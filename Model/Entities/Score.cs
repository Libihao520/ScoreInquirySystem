namespace Model.Entities;

public partial class Score
{
    public long Id { get; set; }

    public long Studentid { get; set; }

    public long Examinationid { get; set; }

    public int Subject { get; set; }

    public decimal Value { get; set; }
}
