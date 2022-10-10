using Microsoft.AspNetCore.Mvc;

namespace FilghtTicketApplication.Controllers
{
    public class BuyTicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
