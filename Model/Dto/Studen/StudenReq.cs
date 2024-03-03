namespace Model.Dto.Studen;

public class StudenReq:PageInput
{
    public string? Class { get; set; }
    public string? Name { get; set; }
    public bool? Sex { get; set; }
}