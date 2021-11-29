namespace ProjectBank.Core;

public record KeywordDto(
    int Id,
    string Word);
public record KeywordDetailsDto(
    int Id,
    string Word,
    ISet<ProjectDto> Projects);

public record KeywordCreateDto([Required, StringLength(50)] string word);

public record KeywordUpdateDto(int Id, [Required, StringLength(50)] string word ) : KeywordCreateDto(word);