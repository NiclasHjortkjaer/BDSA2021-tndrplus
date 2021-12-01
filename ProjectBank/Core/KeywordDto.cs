namespace ProjectBank.Core;

public record KeywordDto(
    int Id,
    string Word);
public record KeywordCreateDto(
    [Required, StringLength(50)] string Word);

public record KeywordUpdateDto(
    int Id, 
    [Required, StringLength(50)] string Word 
    ) : KeywordCreateDto(Word);