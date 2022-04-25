using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApplication.Models;
using Wkhtmltoimage.Net.Implementation;
using Wkhtmltoimage.Net.Implementation.Interfaces;

namespace TestApplication.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IGenerateImage _generateImage;

    public HomeController(ILogger<HomeController> logger, IGenerateImage generateImage)
    {
        _logger = logger;
        _generateImage = generateImage;
    }

    public IActionResult Index()
    {
        var url = "https://en.wordwordapp.com/guessword?id=5&shareType=1";
        var destinationPath = "/Users/bg/testsharing/word5mod.jpg";

        var options = new ConvertOptions
        {
            ImageQuality = 10
        };

        _generateImage.SetConvertOptions(options);
        var consoleOutput = _generateImage.GetImage(url, destinationPath);
        
        
        return View();
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