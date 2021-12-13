namespace ProjectBank.Core;

public interface IPdfRepository
{
    Task<(Status status, Uri uri)> CreatePdfAsync(string name, string contentType, Stream stream);
}