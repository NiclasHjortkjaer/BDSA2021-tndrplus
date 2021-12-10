namespace ProjectBank.Core;

public interface IImageRepository
{
    Task<(Status status, Uri uri)> CreateImageAsync(string name, string contentType, Stream stream);
}