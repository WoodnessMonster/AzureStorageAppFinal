using Azure;
using Azure.Storage.Files.Shares;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace AzureStorageApp.Services
{
    public class FileService
    {
        private readonly ShareClient _shareClient;

        public FileService(IConfiguration configuration)
        {
            string connString = configuration.GetConnectionString("AzureStorage");
            _shareClient = new ShareClient(connString, "logs");
            _shareClient.CreateIfNotExists();
        }

        public async Task UploadLogAsync(string fileName, string content)
        {
            var directory = _shareClient.GetRootDirectoryClient();
            var fileClient = directory.GetFileClient(fileName);

            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
            await fileClient.CreateAsync(stream.Length);
            await fileClient.UploadRangeAsync(new HttpRange(0, stream.Length), stream);
        }

        public async Task<IEnumerable<string>> ListLogsAsync()
        {
            var directory = _shareClient.GetRootDirectoryClient();
            var files = directory.GetFilesAndDirectories();
            return files.Select(f => f.Name);
        }
    }
}
