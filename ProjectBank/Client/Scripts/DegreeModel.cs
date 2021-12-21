using ProjectBank.Core.Enum;

namespace ProjectBank.Client.Scripts;

public class DegreeModel
{
    public string Value { get; set; } = null!;
    public Degree Selected { get; set; } = Degree.Unspecified;
    public List<DegreeOptions> Options { get; set; } = new List<DegreeOptions>()
    {
        new() {Value=Degree.Unspecified, Selected=true},
        new() {Value=Degree.Bachelor, Selected=false},
        new() {Value=Degree.Master, Selected=false},
        new() {Value=Degree.Phd, Selected=false},
    };
}