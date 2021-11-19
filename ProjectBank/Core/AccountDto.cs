namespace Core;

public record AccountDto(
    int Id,
    string GivenName,
    string SurName,
    string Email,
    string Password,
    Degree CurrentDegree
);
public record AccountCreateDto
{
    [StringLength(50)]
    public string GivenName { get; init; }
    
    [StringLength(50)]
    public string SurName { get; init; }

    [Email]
    public string Email { get; init;}

    [Password]
    public string Password { get; init; }

    public Degree CurrentDegree { get; init; }
}
public record AccountUpdateDto : AccountCreateDto{
    public int Id {get; init;}
}