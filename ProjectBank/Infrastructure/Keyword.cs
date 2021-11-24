namespace Infrastructure;

public class Keyword
{
    public Keyword(string word)
    {
        Word = word;
    }

    public int Id { get; set; }

    [StringLength(50)]
    public string Word { get; set; }

    public ICollection<Project> Projects { get; set; } = null!;
}