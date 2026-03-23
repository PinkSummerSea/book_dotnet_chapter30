using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AutoLot.Mvc.Models;

namespace AutoLot.Mvc.Controllers;

[Route("[controller]/[action]")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    [Route("/")]
    [Route("/[controller]")]
    [Route("/[controller]/[action]")]
    public IActionResult Index([FromServices] IOptionsMonitor<DealerInfo> dealerMonitor)
    {
        var vm = dealerMonitor.CurrentValue;
        return PartialView(vm);
    }
    
    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public async Task<IActionResult> RazorSyntaxAsync([FromServices]ICarDataService dataService)
    {
        var car = await dataService.FindAsync(1);
        return View(car);
    }
}
