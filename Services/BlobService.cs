using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace AzureStorageApp.Services
{
    public class BlobService
    {
        private readonly BlobContainerClient _containerClient;

        public BlobService(IConfiguration configuration)
        {
            string connString = configuration.GetConnectionString("AzureStorage");
            _containerClient = new BlobContainerClient(connString, "images");
            _containerClient.CreateIfNotExists();
        }

        public async Task UploadImageAsync(string fileName, Stream fileStream)
        {
            await _containerClient.UploadBlobAsync(fileName, fileStream);
        }

        public string GetImageUrl(string fileName)
        {
            return _containerClient.GetBlobClient(fileName).Uri.ToString();
        }
    }
}
