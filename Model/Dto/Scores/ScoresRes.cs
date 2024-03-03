using System.Text.Json.Serialization;

namespace Model.Dto.Scores;

public class ScoresRes
{
    public long Id { get; set; }

    public string name { get; set; }
    [JsonIgnore] public long Studentid { get; set; }

    [JsonIgnore] public long Examinationid { get; set; }

    public string Examination { get; set; }

    [JsonIgnore] public int Subject { get; set; }
    public string SubjectName { get; set; }
    
    public decimal Value { get; set; }
}