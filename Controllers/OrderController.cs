using AzureStorageApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly QueueService _queueService;

        public OrderController(QueueService queueService)
        {
            _queueService = queueService;
        }

        public async Task<IActionResult> Send(string message)
        {
            await _queueService.SendMessageAsync(message);
            ViewBag.Status = "Message sent to queue!";
            return View();
        }
    }
}
