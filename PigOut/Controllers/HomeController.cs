using Microsoft.AspNetCore.Mvc;

namespace PigOut.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}
