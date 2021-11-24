namespace ProjectBank.Core;

public record AccountDto(
    int Id,
    Degree CurrentDegree
);

public record AccountCreateDto
{
    public Degree CurrentDegree { get; init; }
}

public record AccountUpdateDto : AccountCreateDto
{
    public int Id {get; init;}
}