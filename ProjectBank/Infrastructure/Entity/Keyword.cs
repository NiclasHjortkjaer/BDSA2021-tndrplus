namespace ProjectBank.Infrastructure.Entity;

public class Keyword
{
    public Keyword(string word)
    {
        Word = word;
    }

    public int Id { get; set; }

    [StringLength(50)]
    public string Word { get; }

    public ICollection<Project> Projects { get; set; } = null!;
}