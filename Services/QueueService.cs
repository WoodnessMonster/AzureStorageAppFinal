using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AzureStorageApp.Services
{
    public class QueueService
    {
        private readonly QueueClient _queueClient;

        public QueueService(IConfiguration configuration)
        {
            string connString = configuration.GetConnectionString("AzureStorage");
            _queueClient = new QueueClient(connString, "orders");
            _queueClient.CreateIfNotExists();
        }

        public async Task SendMessageAsync(string message)
        {
            await _queueClient.SendMessageAsync(message);
        }
    }
}
