using ProjectBank.Core.Enum;

namespace ProjectBank.Client.Scripts;

public class DegreeOptions
{
    public Degree Value { get; set; }
    public bool Selected
    {
        get => _Selected;
        set
        {
            _Selected = value;
        }
    }
    public bool _Selected;
}