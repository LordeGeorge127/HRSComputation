using Microsoft.AspNetCore.Mvc;

namespace HRSCompute.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
