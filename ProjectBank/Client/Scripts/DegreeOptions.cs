using ProjectBank.Core.Enum;

namespace ProjectBank.Client.Scripts;

public class DegreeOptions
{
    public Degree Value { get; set; }
    public bool Selected
    {
        get => _selected;
        set => _selected = value;
    }

    private bool _selected;
}