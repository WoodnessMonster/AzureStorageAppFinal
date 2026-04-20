using AzureStorageApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageApp.Controllers
{
    public class LogController : Controller
    {
        private readonly FileService _fileService;

        public LogController(FileService fileService)
        {
            _fileService = fileService;
        }
          
        
        [HttpGet]
        public IActionResult Write()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Write(string message)
        {
            string fileName = $"log_{DateTime.UtcNow:yyyyMMddHHmmss}.txt";
            await _fileService.UploadLogAsync(fileName, message);
            ViewBag.Status = $"Log file {fileName} uploaded!";
            return View();
        }
    }
}
