using AzureStorageApp.Services;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly TableService _tableService;

        public CustomerController(TableService tableService)
        {
            _tableService = tableService;
        }

        public IActionResult Index()
        {
            var table = _tableService.GetCustomerProfilesTable();
            var customers = table.Query<TableEntity>().ToList();
            return View(customers);
        }

        [HttpPost]
        public IActionResult Add(string id, string name, string email)
        {
            var table = _tableService.GetCustomerProfilesTable();
            var entity = new TableEntity("Customer", id)
            {
                { "Name", name },
                { "Email", email }
            };
            table.AddEntity(entity);
            return RedirectToAction("Index");
        }
    }
}

