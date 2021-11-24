namespace Infrastructure;

public class Project
{
    public int Id { get; set; }

    public int? AuthorId { get; set; }
    public Account? Author { get; set; }

    public Degree Degree { get; set; }
    
    [StringLength(100)]
    public string Title { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(250)]
    [Url]
    public string? ImageUrl { get; set; }

    [StringLength(10000)]
    public string? Body { get; set; }

    
    public float? Ects { get; set; }
    
    public ICollection<Keyword> Keywords { get; set; } = null!;
    
}