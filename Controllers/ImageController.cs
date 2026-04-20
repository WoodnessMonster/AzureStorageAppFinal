using AzureStorageApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageApp.Controllers
{
    public class ImageController : Controller
    {
        private readonly BlobService _blobService;
        private readonly QueueService _queueService;

        public ImageController(BlobService blobService, QueueService queueService)
        {
            _blobService = blobService;
            _queueService = queueService;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                await _blobService.UploadImageAsync(file.FileName, stream);

                // Send a message to the queue
                await _queueService.SendMessageAsync($"Processing order for {file.FileName}");

                ViewBag.ImageUrl = _blobService.GetImageUrl(file.FileName);
            }

            return View();
        }
    }
}

