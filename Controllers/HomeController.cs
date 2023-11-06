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
    public string SessionName = "_Name";

    public HomeController(ILogger<HomeController> logger)
    {
        ViewData["header"] = null;
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewData["header"] = null;
        HttpContext.Session.SetString(SessionAuth, "False");
        return View("~/Views/Home/Landing.cshtml");
    }

    public IActionResult Go()
    {
        ViewData["header"] = null;
        return View("~/Views/Home/Index.cshtml");
    }


    public IActionResult Privacy()
    {
        ViewData["header"] = null;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        ViewData["header"] = null;
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public ActionResult SetName(string name)
    {
        ViewData["header"] = null;
        HttpContext.Session.SetString(SessionName, name);
        Console.WriteLine("s");
        return View("~/Views/Home/Landing.cshtml");
    }

    public async Task<IActionResult> ErrorPage(string s)
    {
        ViewData["header"] = null;
        ViewData["error"] = s;
        return View("/Views/Home/Error.cshtml");
    }



    [HttpPost]
    public ActionResult PrivateLogin(string password)
    {
        ViewData["header"] = null;
        if (pass == password)
        {
            HttpContext.Session.SetString(SessionAuth, "True");
        }
        else
        {
            HttpContext.Session.SetString(SessionAuth, "False");
            return View("~/Views/Home/Landing.cshtml");
        }

        return View("~/Views/Home/Go.cshtml");
    }
}
