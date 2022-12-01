using MembershipSystem.ChainOfResponsibility.ChainOfResponsibility;
using System.Diagnostics;

namespace MembershipSystem.ChainOfResponsibilityControllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppIdentityDbContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            AppIdentityDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendEmail()
        {
            var products = await _context.Products.ToListAsync();

            ExcelProcessHandler<Product> excelProcessHandler = new();
            ZipFileProcessHandler<Product> zipFileProcessHandler = new();
            SendEmailProcessHandler sendEmailProcessHandler = new("product.zip", "cihatsolak@hotmail.com");

            excelProcessHandler
                .SetNext(zipFileProcessHandler)
                .SetNext(sendEmailProcessHandler);

            excelProcessHandler.Handle(products);

            return View(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}