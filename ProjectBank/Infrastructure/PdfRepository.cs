namespace ProjectBank.Infrastructure;

public class PdfRepository : IPdfRepository
{
    private readonly BlobContainerClient _client;

    public PdfRepository(BlobContainerClient client)
    {
        _client = client;
    }

    public async Task<(Status status, Uri uri)> CreatePdfAsync(string name, string contentType, Stream stream)
    {
        // Create a URI to the blob
        var client = _client.GetBlockBlobClient(name);

        await client.UploadAsync(stream);

        await client.SetHttpHeadersAsync(new Azure.Storage.Blobs.Models.BlobHttpHeaders {  ContentType = contentType });

        return (Status.Created, client.Uri);
    }
}
