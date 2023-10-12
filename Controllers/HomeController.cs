using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QA_Feedback.Models;

namespace QA_Feedback.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public static string pass = "hello";
    public static bool auth = false;
    public string SessionAuth = "_Auth";



    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        HttpContext.Session.SetString(SessionAuth, "False");
        return View("~/Views/Home/Landing.cshtml");
    }

    public IActionResult Go()
    {
        return View("~/Views/Home/Index.cshtml");
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

    [HttpPost]
    public ActionResult PrivateLogin(string password)
    {
        if (pass == password)
        {
            HttpContext.Session.SetString(SessionAuth, "True");
        }
        else
        {
            HttpContext.Session.SetString(SessionAuth, "False");
        }

        return View("~/Views/Home/Go.cshtml");
    }
}
