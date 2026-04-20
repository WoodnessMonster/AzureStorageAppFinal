using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;

namespace AzureStorageApp.Services
{
    public class TableService
    {
        private readonly TableServiceClient _tableServiceClient;

        public TableService(IConfiguration configuration)
        {
            string connString = configuration.GetConnectionString("AzureStorage");
            _tableServiceClient = new TableServiceClient(connString);
        }

        public TableClient GetCustomerProfilesTable()
        {
            return _tableServiceClient.GetTableClient("CustomerProfiles");
        }

        public TableClient GetProductsTable()
        {
            return _tableServiceClient.GetTableClient("Products");
        }
    }
}
