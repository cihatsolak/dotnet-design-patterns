using MembershipSystem.Adapter.Services;
using System.Diagnostics;

namespace MembershipSystem.Adapter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IImageProcess _imageProcess;

        public HomeController(
            ILogger<HomeController> logger,
            IImageProcess imageProcess)
        {
            _logger = logger;
            _imageProcess = imageProcess;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddWatermark()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddWatermark(IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                MemoryStream imageMemoryStream = new();
                await formFile.CopyToAsync(imageMemoryStream);

                _imageProcess.AddWatermark("MembershipSystem.Adapter", formFile.FileName, imageMemoryStream);
            }

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}