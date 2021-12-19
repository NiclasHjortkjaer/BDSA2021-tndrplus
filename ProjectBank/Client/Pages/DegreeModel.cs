using ProjectBank.Core;

namespace ProjectBank.Client;

public class DegreeModel
{
    public string Value { get; set; }
    public Degree Selected { get; set; } = Degree.Unspecified;
    public List<DegreeOptions> Options { get; set; } = new List<DegreeOptions>()
    {
        new DegreeOptions() {Value=Degree.Unspecified, Selected=true},
        new DegreeOptions() {Value=Degree.Bachelor, Selected=false},
        new DegreeOptions() {Value=Degree.Master, Selected=false},
        new DegreeOptions() {Value=Degree.PHD, Selected=false},
    };
}