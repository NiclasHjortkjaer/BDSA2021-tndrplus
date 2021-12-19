namespace ProjectBank.Core;

public interface IFileRepository
{
    Task<(Status status, Uri uri)> CreateFileAsync(string name, string contentType, Stream stream);
}