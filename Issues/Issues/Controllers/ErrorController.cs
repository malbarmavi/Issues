using System.Web.Mvc;

namespace Issues.Controllers
{
  public class ErrorController : Controller
  {
    public ActionResult Index()
    {
      return RedirectToAction("Index", "Home");
    }

    public ActionResult AccessDenied()
    {
      return View();
    }
  }
}