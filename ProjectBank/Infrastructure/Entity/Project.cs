namespace ProjectBank.Infrastructure.Entity;

public class Project
{
    public Project(string title)
    {
        Title = title;
    }

    public int Id { get; set; }

    public int? AuthorId { get; set; }

    public Account? Author { get; set; }

    public Degree? Degree { get; set; }
    
    [StringLength(100)]
    public string Title { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(250)]
    [Url]
    public string? ImageUrl { get; set; }

    [StringLength(250)]
    [Url]
    public string? FileUrl { get; set; }

    
    public float? Ects { get; set; }

    public DateTime LastUpdated { get; set; }

    public ICollection<Account> Accounts { get; set; } = null!; //vi har den kun for at lave relation mellem many to many

    public ICollection<Keyword> Keywords { get; set; } = null!;

}