using ProjectBank.Core.Enum;

namespace ProjectBank.Core.RepositoryInterface;

public interface IFileRepository
{
    Task<(Status status, Uri uri)> CreateFileAsync(string name, string contentType, Stream stream);
}